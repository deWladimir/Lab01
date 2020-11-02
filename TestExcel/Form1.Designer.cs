namespace TestExcel
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.delRow = new System.Windows.Forms.Button();
            this.addRow = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.delCol = new System.Windows.Forms.Button();
            this.addCol = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.savOrOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.saveThisFile = new System.Windows.Forms.ToolStripMenuItem();
            this.openNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutProg = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.delRow);
            this.splitContainer1.Panel1.Controls.Add(this.addRow);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.delCol);
            this.splitContainer1.Panel1.Controls.Add(this.addCol);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(850, 544);
            this.splitContainer1.SplitterDistance = 80;
            this.splitContainer1.TabIndex = 0;
            // 
            // delRow
            // 
            this.delRow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.delRow.Location = new System.Drawing.Point(777, 53);
            this.delRow.Name = "delRow";
            this.delRow.Size = new System.Drawing.Size(43, 23);
            this.delRow.TabIndex = 8;
            this.delRow.Text = "Del";
            this.delRow.UseVisualStyleBackColor = true;
            this.delRow.Click += new System.EventHandler(this.delRow_Click);
            // 
            // addRow
            // 
            this.addRow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addRow.Location = new System.Drawing.Point(725, 53);
            this.addRow.Name = "addRow";
            this.addRow.Size = new System.Drawing.Size(43, 23);
            this.addRow.TabIndex = 7;
            this.addRow.Text = "Add";
            this.addRow.UseVisualStyleBackColor = true;
            this.addRow.Click += new System.EventHandler(this.addRow_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(671, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Рядки";
            // 
            // delCol
            // 
            this.delCol.Cursor = System.Windows.Forms.Cursors.Hand;
            this.delCol.Location = new System.Drawing.Point(595, 53);
            this.delCol.Name = "delCol";
            this.delCol.Size = new System.Drawing.Size(43, 23);
            this.delCol.TabIndex = 5;
            this.delCol.Text = "Del";
            this.delCol.UseVisualStyleBackColor = true;
            this.delCol.Click += new System.EventHandler(this.delCol_Click);
            // 
            // addCol
            // 
            this.addCol.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addCol.Location = new System.Drawing.Point(543, 53);
            this.addCol.Name = "addCol";
            this.addCol.Size = new System.Drawing.Size(43, 23);
            this.addCol.TabIndex = 4;
            this.addCol.Text = "Add";
            this.addCol.UseVisualStyleBackColor = true;
            this.addCol.Click += new System.EventHandler(this.addCol_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(459, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Стовпчики";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Вираз у обранній клітинці";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Info;
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(190, 53);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(239, 22);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
           
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.savOrOpenFile,
            this.aboutProg});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(850, 28);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // savOrOpenFile
            // 
            this.savOrOpenFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveThisFile,
            this.openNewFile});
            this.savOrOpenFile.Name = "savOrOpenFile";
            this.savOrOpenFile.Size = new System.Drawing.Size(59, 24);
            this.savOrOpenFile.Text = "Файл";
            // 
            // saveThisFile
            // 
            this.saveThisFile.Name = "saveThisFile";
            this.saveThisFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveThisFile.Size = new System.Drawing.Size(245, 26);
            this.saveThisFile.Text = "Зберегти файл";
            this.saveThisFile.Click += new System.EventHandler(this.saveThisFile_Click);
            // 
            // openNewFile
            // 
            this.openNewFile.Name = "openNewFile";
            this.openNewFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openNewFile.Size = new System.Drawing.Size(245, 26);
            this.openNewFile.Text = "Відкрити файл";
            this.openNewFile.Click += new System.EventHandler(this.openNewFile_Click);
            // 
            // aboutProg
            // 
            this.aboutProg.Name = "aboutProg";
            this.aboutProg.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.aboutProg.Size = new System.Drawing.Size(124, 24);
            this.aboutProg.Text = "Про програму";
            this.aboutProg.Click += new System.EventHandler(this.aboutProg_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(850, 460);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "Відкрити файл";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 544);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "MyExcel";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button delRow;
        private System.Windows.Forms.Button addRow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button delCol;
        private System.Windows.Forms.Button addCol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem savOrOpenFile;
        private System.Windows.Forms.ToolStripMenuItem saveThisFile;
        private System.Windows.Forms.ToolStripMenuItem openNewFile;
        private System.Windows.Forms.ToolStripMenuItem aboutProg;
    }
}

