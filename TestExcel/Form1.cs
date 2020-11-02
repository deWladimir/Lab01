using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace TestExcel
{
    public partial class Form1 : Form
    {
        static Dictionary<string, MyCell> nameToCell = new Dictionary<string, MyCell>(); //словник: ім’я клітинки - об’єкт типу Cell
        private int columnNum;
        private int rowNum;
        public Form1()
        {
            InitializeComponent();
            DataGridInit(6, 6);
            /*заповнюємо словник*/
            for (int i = 0; i < rowNum; ++i)
                for (int j = 0; j < columnNum; ++j)
                {
                    string name = Sys26.To26(j) + i.ToString();
                    MyCell cell = new MyCell(name);
                    cell.Row = i;
                    cell.Column = j;
                    nameToCell.Add(name, cell);
                }
        }

        private void DataGridInit(int rows, int columns)//створюємо таблицю заданих розмірів
        {
            columnNum = columns;
            rowNum = rows;

            for (int i = 0; i < columns; ++i)
            {
                DataGridViewColumn A = new DataGridViewColumn();
                A.HeaderText = Sys26.To26(i);
                A.Name = A.HeaderText;
                MyCell cell = new MyCell();
                A.CellTemplate = cell;
                dataGridView1.Columns.Add(A);
            }

            for (int i = 0; i < rows; ++i)
            {
                DataGridViewRow A = new DataGridViewRow();
                A.HeaderCell.Value = i.ToString();
                dataGridView1.Rows.Add(A);
            }
        }


        static class Sys26
        {
            /* переведення числа у систему літер */
            public static string To26(int i)
            {
                int k = 0;
                string res = "";
                int[] Arr = new int[2]; //кількість ствопчиків <26^2
                if (i > 25)
                {
                    Arr[k] = i / 26 - 1;
                    i = i % 26;
                    ++k;
                }
                Arr[k] = i;
                for (int j = 0; j <= k; ++j)
                {
                    res += ((char)('A' + Arr[j])).ToString();
                }
                return res;
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            /*натиснувши на обрану клітинку, починаємо її редагувати, у клітинці з’являється збережений раніше вираз  */
            int currRow = dataGridView1.CurrentCell.RowIndex;
            int currCol = dataGridView1.CurrentCell.ColumnIndex;
            string cellName = Sys26.To26(currCol) + currRow.ToString();
            dataGridView1[currCol, currRow].Value = nameToCell[cellName].Exp;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            /* закінчення редагування виразу */
            try
            {
                int currRow = dataGridView1.CurrentCell.RowIndex;
                int currCol = dataGridView1.CurrentCell.ColumnIndex;
                string cellName = Sys26.To26(currCol) + currRow.ToString();
                if (dataGridView1[currCol, currRow].Value == null)
                {
                    dataGridView1[currCol, currRow].Value = 0;
                }
                string temp = dataGridView1[currCol, currRow].Value.ToString();
                ClearPointers(cellName);
                string res = AnalyzeAdresses(cellName, temp);
                nameToCell[cellName].Exp = temp;
                nameToCell[cellName].Val = Calculator.Evaluate(res);
                NewValuesForDepend(cellName);
                RefreshCells();
            }
            /* оброблення винятків */
            catch (ArgumentNullException)
            {
                MessageBox.Show("Ви залишили клітинку порожньою! Не робіть так!");
                RefreshCells();
            }

            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Зациклення");
                RefreshCells();
            }

            catch (ArithmeticException ae)
            {
                MessageBox.Show(ae.Message);
                RefreshCells();
            }

            catch (Exception ex)
            {
                int currRow = dataGridView1.CurrentCell.RowIndex;
                int currCol = dataGridView1.CurrentCell.ColumnIndex;
                string cellName = Sys26.To26(currCol) + currRow.ToString();
                nameToCell[cellName].Exp = "";
                if (ex.Message == "Div by 0")
                {
                    MessageBox.Show("Ділення на нуль");
                    RefreshCells();
                    return;
                }
                MessageBox.Show("Неприпустимі операції або операнди");
                RefreshCells();
            }
        }
            private static void CheckForLoop(string cellName, string cellPtrName)
            {
                /* алгоритм пошуку зациклення */
                if (cellName == cellPtrName) throw new ArgumentOutOfRangeException();
                if (nameToCell[cellPtrName].Pointers.Contains(cellName)) throw new ArgumentOutOfRangeException();

                else
                {
                    foreach (string mc in nameToCell[cellPtrName].Pointers) CheckForLoop(cellName, mc);
                }
            }

            private static void NewValuesForDepend(string cellName)
            {
                /* після зміни значення обранної клітинки, треба порахувати нові значення для залежних від данної клітинок
                робимо це за алгоритмом нижче
                використовуємо рекурсію,
                бо від клітинок, залежних від данної, можуть бути також залежні*/

                Regex addr = new Regex(@"[A-Z]+[0-9]+");

                foreach (string mc in nameToCell[cellName].DependOnMe)
                {
                    MatchEvaluator MEvaluator = new MatchEvaluator(AdressToValue);
                    string res = addr.Replace(nameToCell[mc].Exp, MEvaluator);
                    nameToCell[mc].Val = Calculator.Evaluate(res);
                    NewValuesForDepend(mc);
                }
            }

            private static void ClearPointers(string cellName)
            {
                /* після зміни виразу обранної клітинки, треба для кожної клітинки, від яких залежить дана,
                 видалити зі списку залежних клітинок тієї кожної клітинки дану клітинку
                 також очищаємо список клітинок, від яких залежить дана */
                foreach (string mc in nameToCell[cellName].Pointers)
                {
                    nameToCell[mc].DependOnMe.Remove(cellName);
                }

                nameToCell[cellName].Pointers.Clear();
            }

            private static string AnalyzeAdresses(string cellName, string s)
            {
                /* аналізуємо вираз на наявність адрес,
                   повертаємо рядок, де адреса клітинки, змінена на її значення
                   використовуємо регулярні вирази*/
                Regex addr = new Regex(@"[A-Z]+[0-9]+");
                foreach (Match m in addr.Matches(s))
                {
                    /* для кожної адреси клітинки у вриазі:
                       додаємо до її списку залежних обрану користувачем клітинку
                       до списку клітинок, від яких залежить обрана, додаємо кожну клітинку з виразу*/

                    if (nameToCell.ContainsKey(m.ToString()))
                    {
                        CheckForLoop(cellName, m.ToString());
                        nameToCell[cellName].Pointers.Add(m.ToString());
                        nameToCell[m.ToString()].DependOnMe.Add(cellName);
                    }
                    /* якщо трапилась адреса клітинки, якої не існує, кидаємо виняток*/
                    else throw new ArithmeticException("Клітинки " + m.ToString() + " не існує");
                }
                /*за допомгою функцій регулярних виразів змінюмо вираз*/
                MatchEvaluator MEvaluator = new MatchEvaluator(AdressToValue);
                string res = addr.Replace(s, MEvaluator);
                return res;
            }

            private void RefreshCells()
            {   
                /* оновлюємо таблицю */
                for (int i = 0; i < rowNum; ++i)
                    for (int j = 0; j < columnNum; ++j)
                    {
                        string name = Sys26.To26(j) + i.ToString();
                        if (nameToCell[name].Val == 0 && nameToCell[name].Exp == "") dataGridView1[j, i].Value = "";
                        else dataGridView1[j, i].Value = Math.Round(nameToCell[name].Val,3).ToString();
                    }
                return;
            }

            private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
            {
                /* при виборі клітинки відображаємо її вираз у текстове поле */
                int row = dataGridView1.CurrentCell.RowIndex;
                int col = dataGridView1.CurrentCell.ColumnIndex;
                string name = Sys26.To26(col) + row.ToString();
                textBox1.Text = nameToCell[name].Exp;

            }



            private static string AdressToValue(Match s)
            {
                /* метод для MatchEvaluator, щоб замінити адресу клітинку на її значення  */
                if (nameToCell.ContainsKey(s.ToString()))
                {
                    return nameToCell[s.ToString()].Val.ToString();
                }
                else return "0";
            }

            private void addCol_Click(object sender, EventArgs e)
            {
                /* додати стовпчик */
                DataGridViewColumn A = new DataGridViewColumn();
                A.HeaderText = Sys26.To26(columnNum);
                A.Name = A.HeaderText;
                MyCell mc = new MyCell();
                A.CellTemplate = mc;
                ++columnNum;
                dataGridView1.Columns.Add(A);

                for (int i = 0; i < rowNum; ++i)
                {
                    string name = Sys26.To26(columnNum - 1) + i.ToString();
                    MyCell mycell = new MyCell(name);
                    mycell.Column = columnNum - 1;
                    mycell.Row = i;
                    nameToCell.Add(name, mycell);
                }
            }


  

        private void addRow_Click(object sender, EventArgs e)
        {
            /* додати рядок */
            DataGridViewRow A = new DataGridViewRow();
            A.HeaderCell.Value = rowNum.ToString();
            dataGridView1.Rows.Add(A);
            ++rowNum;

            for (int i = 0; i < columnNum; ++i)
            {
                string name = Sys26.To26(i) + (rowNum - 1).ToString();
                MyCell mycell = new MyCell(name);
                mycell.Column = i;
                mycell.Row = rowNum - 1;
                nameToCell.Add(name, mycell);
            }
        }

        private void delCol_Click(object sender, EventArgs e)
        {
            /* видалення стовпчика */
            for (int i = 0; i < rowNum; ++i)
            {
                string name = Sys26.To26(columnNum - 1) + i.ToString();

                if (name == "A0")
                {
                    MessageBox.Show("Заборонено видаляти останній стовпчик");
                    return;
                }

                if (nameToCell[name].DependOnMe.Count > 0)
                {
                    MessageBox.Show("Від клітинки " + name + " є залежні. Видалення неможливе!");
                    return;
                }
                else
                {
                    if (nameToCell[name].Exp == "" || nameToCell[name].Exp == "0") nameToCell.Remove(name);
                    else
                    {
                        foreach (string mc in nameToCell[name].Pointers)
                        {
                            nameToCell[mc].DependOnMe.Remove(name);
                        }
                        nameToCell.Remove(name);
                    }
                }
            }
            dataGridView1.Columns.RemoveAt(columnNum - 1);
            --columnNum;
        }

        private void delRow_Click(object sender, EventArgs e)
        {
            /* видалення рядка */
            for (int i = 0; i < columnNum; ++i)
            {
                if (rowNum == 1)
                {
                    MessageBox.Show("Заборонено видаляти останній рядок");
                    return;
                }

                string name = Sys26.To26(i) + (rowNum - 1).ToString();

                if (nameToCell[name].DependOnMe.Count > 0)
                {
                    MessageBox.Show("Від клітинки " + name + " є залежні. Видалення неможливе!");
                    return;
                }

                else
                {
                    if (nameToCell[name].Exp == "" || nameToCell[name].Exp == "0") nameToCell.Remove(name);
                    else
                    {
                        foreach (string mc in nameToCell[name].Pointers)
                        {
                            nameToCell[mc].DependOnMe.Remove(name);
                        }
                        nameToCell.Remove(name);
                    }
                }
            }

            dataGridView1.Rows.RemoveAt(rowNum - 1);
            --rowNum;
        }

        private void saveThisFile_Click(object sender, EventArgs e)
        {
            /* збереження файлу */
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "MyExcelFiles|*.excf";
            saveFileDialog.Title = "Save an *excf file";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            FileStream fs = (FileStream)saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(columnNum);
            sw.WriteLine(rowNum);
            foreach (MyCell mc in nameToCell.Values)
            {
                sw.WriteLine(mc.Name);
                sw.WriteLine(mc.Val);
                sw.WriteLine(mc.Exp);
                sw.WriteLine(mc.Column);
                sw.WriteLine(mc.Row);

                sw.WriteLine(mc.DependOnMe.Count);

                foreach (string name in mc.DependOnMe)
                {
                    sw.WriteLine(name);
                }

                sw.WriteLine(mc.Pointers.Count);

                foreach (string name in mc.Pointers)
                {
                    sw.WriteLine(name);
                }
            }

            sw.Close();
            fs.Close();
            MessageBox.Show("Таблиця успішно збережена");
        }

        private void openNewFile_Click(object sender, EventArgs e)
        {
            /* відкриття файлу */
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.Filter = "MyExcelFiles|*.excf";
            oFD.Title = "Open an *.excf file";
            if (oFD.ShowDialog() != DialogResult.OK) return;
            StreamReader sw = new StreamReader(oFD.FileName);
            nameToCell.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            columnNum = int.Parse(sw.ReadLine());
            rowNum = int.Parse(sw.ReadLine());
            DataGridInit(rowNum, columnNum);
            for (int i = 0; i < rowNum; ++i)
                for (int j = 0; j < columnNum; ++j)
                {
                    MyCell mc = new MyCell();
                    mc.Name = sw.ReadLine();
                    mc.Val = double.Parse(sw.ReadLine());
                    mc.Exp = sw.ReadLine();
                    mc.Column = int.Parse(sw.ReadLine());
                    mc.Row = int.Parse(sw.ReadLine());

                    int depSize = int.Parse(sw.ReadLine());

                    for (int dS = 0; dS < depSize; ++dS)
                    {
                        mc.DependOnMe.Add(sw.ReadLine());
                    }

                    int ptrSize = int.Parse(sw.ReadLine());

                    for (int pS = 0; pS < ptrSize; ++pS)
                    {
                        mc.Pointers.Add(sw.ReadLine());
                    }

                    nameToCell.Add(mc.Name, mc);

                    if (mc.Exp != "") dataGridView1[mc.Column, mc.Row].Value = mc.Val.ToString();
                }
            sw.Close();
        }

        private void aboutProg_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Функціонал програми:\n" +
                "\t-редагування арифметичного значення комірки;\n" +
                "\t-збереження поточного файлу;\n" +
                "\t-відкриття нового файлу в розширенні *excf;\n" +
                "\t-додавання та видалення рядків(в кінець або з кінця);\n" +
                "\t-додавання та видалення стовпчиків(в кінець або з кінця);\n\n" +
                "ЩОБ ЗМІНИТИ ВИРАЗ У КЛІТИНЦІ ДВІЧІ ПО НІЙ КЛАЦНІТЬ ЛКМ\n" +
                "ТА ПОЧНІТЬ ВВОДИТИ АРИФМЕТИЧНИЙ ВИРАЗ\n" +
                "ДЛЯ ЗАВЕРШЕННЯ КЛАЦНІТЬ ЗА МЕЖАМИ КЛІТИНКИ АБО НАТИСНІТЬ ENTER\n" +
                "ЩОБ ПЕРЕГЛЯНУТИ ВИРАЗ КЛІТИНКИ КЛАЦНІТЬ ПО НІЙ ЛКМ\n\n" +
                "Підтримувані арифметичні операції:\n" +
                "\t*, /, +, -\n" +
                "\tmod (формат: arg1 mod arg2) :остача від ділення;\n" +
                "\tdiv (формат: arg1 div arg2) :цілочисельне ділення;\n" +
                "\tmmin(arg1;arg2;...argN) :мінімальне значення серед N;\n" +
                "\tmmax(arg1;arg2;...argN) :максимальне значення серед N;\n\n" +
                "ФОРМАТ ВВЕДЕННЯ КЛІТИНОК ТА DOUBLE ЧИСЕЛ ДЛЯ ОБЧИСЛЕННЯ АРИФМЕТИЧНОГО ВИРАЗУ\n" +
                "\tЧисла типу DOUBLE вводяться ЧЕРЕЗ КОМУ (3,1471)\n" +
                "\tКлітинка - ВЕЛИКА(і) латинська(і) літера(и) та число (А0, BC3)" +
                "\tПРИКЛАД ВИРАЗУ У КЛІТИНЦІ\n" +
                "\tAO + 117 div B4 mod 0,05*C3");
        }
    }
}

