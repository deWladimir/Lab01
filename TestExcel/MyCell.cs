using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestExcel
{
    internal class MyCell : DataGridViewTextBoxCell
    {
            private double val;
            private string expr;
            private string name;
            private int row;
            private int column;
            private List<string> pointers = new List<string>();
            private List<string> dependOnMe = new List<string>();

            public MyCell()
            {
                val = 0;
                expr = "";
                name = "";
            }

            public MyCell(string s)
            {
                name = s;
                expr = "";
                val = 0;
            }
                
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public double Val
            {
                get { return val; }
                set { val = value; }
            }

            public string Exp
            {
                get { return expr; }
                set { expr = value; }
            }
            
            public int Row 
            {   get { return row; }
                set { row = value; }
            }
            
            public int Column
            {
                get { return column; }
                set { column = value; }
            }

            public List<string> Pointers
            {
                get { return pointers; }
            }

            public List<string> DependOnMe
            {
                get { return dependOnMe; }
            }

        }
    }

