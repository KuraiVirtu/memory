namespace Memoria
{
    partial class FormRecordes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.comboBox9 = new System.Windows.Forms.ComboBox();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox10 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdGraBar = new System.Windows.Forms.RadioButton();
            this.rdGraCir = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox8 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.comboBox7 = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdDifTod = new System.Windows.Forms.RadioButton();
            this.rdDifMed = new System.Windows.Forms.RadioButton();
            this.rdDifFac = new System.Windows.Forms.RadioButton();
            this.rdDifDif = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdOrgMax = new System.Windows.Forms.RadioButton();
            this.rdOrgMed = new System.Windows.Forms.RadioButton();
            this.rdOrgTot = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(699, 527);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Voltar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(6, 33);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(740, 393);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Jogador";
            this.columnHeader1.Width = 157;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Pontuação";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader2.Width = 175;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Tempo";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader3.Width = 88;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Dificuldade";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 96;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Dificuldade:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Todas",
            "Fácil",
            "Média",
            "Difícil"});
            this.comboBox1.Location = new System.Drawing.Point(75, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(91, 21);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.TabStop = false;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(172, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Jogadores";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Todos"});
            this.comboBox2.Location = new System.Drawing.Point(234, 6);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(125, 21);
            this.comboBox2.TabIndex = 9;
            this.comboBox2.TabStop = false;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(331, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Classificações";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 49);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 472);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.comboBox2);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(752, 446);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Quadro de Honra";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.comboBox9);
            this.tabPage2.Controls.Add(this.listView2);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.comboBox10);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(752, 446);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Quadro da Vergonha";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // comboBox9
            // 
            this.comboBox9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox9.FormattingEnabled = true;
            this.comboBox9.Items.AddRange(new object[] {
            "Todas",
            "Fácil",
            "Média",
            "Difícil"});
            this.comboBox9.Location = new System.Drawing.Point(75, 6);
            this.comboBox9.Name = "comboBox9";
            this.comboBox9.Size = new System.Drawing.Size(91, 21);
            this.comboBox9.TabIndex = 12;
            this.comboBox9.TabStop = false;
            this.comboBox9.SelectedIndexChanged += new System.EventHandler(this.comboBox9_SelectedIndexChanged);
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listView2.FullRowSelect = true;
            this.listView2.Location = new System.Drawing.Point(6, 33);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(740, 393);
            this.listView2.TabIndex = 10;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Jogador";
            this.columnHeader5.Width = 157;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Pontuação";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader6.Width = 175;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Tempo";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader7.Width = 88;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Dificuldade";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader8.Width = 96;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Dificuldade:";
            // 
            // comboBox10
            // 
            this.comboBox10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox10.FormattingEnabled = true;
            this.comboBox10.Items.AddRange(new object[] {
            "Todos"});
            this.comboBox10.Location = new System.Drawing.Point(234, 6);
            this.comboBox10.Name = "comboBox10";
            this.comboBox10.Size = new System.Drawing.Size(125, 21);
            this.comboBox10.TabIndex = 14;
            this.comboBox10.TabStop = false;
            this.comboBox10.SelectedIndexChanged += new System.EventHandler(this.comboBox9_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(172, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Jogadores";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox4);
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.comboBox3);
            this.tabPage3.Controls.Add(this.zedGraphControl1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(752, 446);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Estatistica";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdGraBar);
            this.groupBox4.Controls.Add(this.rdGraCir);
            this.groupBox4.Location = new System.Drawing.Point(607, 402);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(139, 38);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Gráfico de:";
            // 
            // rdGraBar
            // 
            this.rdGraBar.AutoSize = true;
            this.rdGraBar.Checked = true;
            this.rdGraBar.Location = new System.Drawing.Point(5, 19);
            this.rdGraBar.Name = "rdGraBar";
            this.rdGraBar.Size = new System.Drawing.Size(55, 17);
            this.rdGraBar.TabIndex = 13;
            this.rdGraBar.TabStop = true;
            this.rdGraBar.Tag = "1";
            this.rdGraBar.Text = "Barras";
            this.rdGraBar.UseVisualStyleBackColor = true;
            this.rdGraBar.CheckedChanged += new System.EventHandler(this.escolheCalculo);
            // 
            // rdGraCir
            // 
            this.rdGraCir.AutoSize = true;
            this.rdGraCir.Location = new System.Drawing.Point(72, 19);
            this.rdGraCir.Name = "rdGraCir";
            this.rdGraCir.Size = new System.Drawing.Size(60, 17);
            this.rdGraCir.TabIndex = 26;
            this.rdGraCir.TabStop = true;
            this.rdGraCir.Tag = "2";
            this.rdGraCir.Text = "Circular";
            this.rdGraCir.UseVisualStyleBackColor = true;
            this.rdGraCir.CheckedChanged += new System.EventHandler(this.escolheCalculo);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.comboBox8);
            this.groupBox3.Controls.Add(this.comboBox4);
            this.groupBox3.Controls.Add(this.comboBox5);
            this.groupBox3.Controls.Add(this.comboBox6);
            this.groupBox3.Controls.Add(this.comboBox7);
            this.groupBox3.Location = new System.Drawing.Point(607, 242);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(139, 154);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Jogadores:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 130);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "5.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "4.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "3.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "2.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "1.";
            // 
            // comboBox8
            // 
            this.comboBox8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox8.FormattingEnabled = true;
            this.comboBox8.Items.AddRange(new object[] {
            ""});
            this.comboBox8.Location = new System.Drawing.Point(27, 127);
            this.comboBox8.Name = "comboBox8";
            this.comboBox8.Size = new System.Drawing.Size(106, 21);
            this.comboBox8.TabIndex = 21;
            this.comboBox8.SelectedIndexChanged += new System.EventHandler(this.updateArrays);
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            ""});
            this.comboBox4.Location = new System.Drawing.Point(27, 19);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(106, 21);
            this.comboBox4.TabIndex = 17;
            this.comboBox4.SelectedIndexChanged += new System.EventHandler(this.updateArrays);
            // 
            // comboBox5
            // 
            this.comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            ""});
            this.comboBox5.Location = new System.Drawing.Point(27, 46);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(106, 21);
            this.comboBox5.TabIndex = 18;
            this.comboBox5.SelectedIndexChanged += new System.EventHandler(this.updateArrays);
            // 
            // comboBox6
            // 
            this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Items.AddRange(new object[] {
            ""});
            this.comboBox6.Location = new System.Drawing.Point(27, 73);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(106, 21);
            this.comboBox6.TabIndex = 19;
            this.comboBox6.SelectedIndexChanged += new System.EventHandler(this.updateArrays);
            // 
            // comboBox7
            // 
            this.comboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.Items.AddRange(new object[] {
            ""});
            this.comboBox7.Location = new System.Drawing.Point(27, 100);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new System.Drawing.Size(106, 21);
            this.comboBox7.TabIndex = 20;
            this.comboBox7.SelectedIndexChanged += new System.EventHandler(this.updateArrays);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdDifTod);
            this.groupBox2.Controls.Add(this.rdDifMed);
            this.groupBox2.Controls.Add(this.rdDifFac);
            this.groupBox2.Controls.Add(this.rdDifDif);
            this.groupBox2.Location = new System.Drawing.Point(607, 125);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(139, 111);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dificuldade:";
            // 
            // rdDifTod
            // 
            this.rdDifTod.AutoSize = true;
            this.rdDifTod.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rdDifTod.Checked = true;
            this.rdDifTod.Location = new System.Drawing.Point(78, 19);
            this.rdDifTod.Name = "rdDifTod";
            this.rdDifTod.Size = new System.Drawing.Size(55, 17);
            this.rdDifTod.TabIndex = 16;
            this.rdDifTod.TabStop = true;
            this.rdDifTod.Tag = "0";
            this.rdDifTod.Text = "Todas";
            this.rdDifTod.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rdDifTod.UseVisualStyleBackColor = true;
            this.rdDifTod.CheckedChanged += new System.EventHandler(this.escolheCalculo);
            // 
            // rdDifMed
            // 
            this.rdDifMed.AutoSize = true;
            this.rdDifMed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rdDifMed.Location = new System.Drawing.Point(79, 65);
            this.rdDifMed.Name = "rdDifMed";
            this.rdDifMed.Size = new System.Drawing.Size(54, 17);
            this.rdDifMed.TabIndex = 13;
            this.rdDifMed.Tag = "2";
            this.rdDifMed.Text = "Média";
            this.rdDifMed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rdDifMed.UseVisualStyleBackColor = true;
            this.rdDifMed.CheckedChanged += new System.EventHandler(this.escolheCalculo);
            // 
            // rdDifFac
            // 
            this.rdDifFac.AutoSize = true;
            this.rdDifFac.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rdDifFac.Location = new System.Drawing.Point(86, 42);
            this.rdDifFac.Name = "rdDifFac";
            this.rdDifFac.Size = new System.Drawing.Size(47, 17);
            this.rdDifFac.TabIndex = 14;
            this.rdDifFac.Tag = "1";
            this.rdDifFac.Text = "Fácil";
            this.rdDifFac.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rdDifFac.UseVisualStyleBackColor = true;
            this.rdDifFac.CheckedChanged += new System.EventHandler(this.escolheCalculo);
            // 
            // rdDifDif
            // 
            this.rdDifDif.AutoSize = true;
            this.rdDifDif.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rdDifDif.Location = new System.Drawing.Point(81, 88);
            this.rdDifDif.Name = "rdDifDif";
            this.rdDifDif.Size = new System.Drawing.Size(52, 17);
            this.rdDifDif.TabIndex = 15;
            this.rdDifDif.Tag = "3";
            this.rdDifDif.Text = "Difícil";
            this.rdDifDif.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rdDifDif.UseVisualStyleBackColor = true;
            this.rdDifDif.CheckedChanged += new System.EventHandler(this.escolheCalculo);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdOrgMax);
            this.groupBox1.Controls.Add(this.rdOrgMed);
            this.groupBox1.Controls.Add(this.rdOrgTot);
            this.groupBox1.Location = new System.Drawing.Point(607, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(139, 87);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Organização:";
            // 
            // rdOrgMax
            // 
            this.rdOrgMax.AutoSize = true;
            this.rdOrgMax.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rdOrgMax.Location = new System.Drawing.Point(72, 65);
            this.rdOrgMax.Name = "rdOrgMax";
            this.rdOrgMax.Size = new System.Drawing.Size(61, 17);
            this.rdOrgMax.TabIndex = 12;
            this.rdOrgMax.TabStop = true;
            this.rdOrgMax.Tag = "3";
            this.rdOrgMax.Text = "Máximo";
            this.rdOrgMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rdOrgMax.UseVisualStyleBackColor = true;
            this.rdOrgMax.CheckedChanged += new System.EventHandler(this.escolheCalculo);
            // 
            // rdOrgMed
            // 
            this.rdOrgMed.AutoSize = true;
            this.rdOrgMed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rdOrgMed.Location = new System.Drawing.Point(79, 42);
            this.rdOrgMed.Name = "rdOrgMed";
            this.rdOrgMed.Size = new System.Drawing.Size(54, 17);
            this.rdOrgMed.TabIndex = 10;
            this.rdOrgMed.TabStop = true;
            this.rdOrgMed.Tag = "2";
            this.rdOrgMed.Text = "Média";
            this.rdOrgMed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rdOrgMed.UseVisualStyleBackColor = true;
            this.rdOrgMed.CheckedChanged += new System.EventHandler(this.escolheCalculo);
            // 
            // rdOrgTot
            // 
            this.rdOrgTot.AutoSize = true;
            this.rdOrgTot.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rdOrgTot.Checked = true;
            this.rdOrgTot.Location = new System.Drawing.Point(84, 19);
            this.rdOrgTot.Name = "rdOrgTot";
            this.rdOrgTot.Size = new System.Drawing.Size(49, 17);
            this.rdOrgTot.TabIndex = 11;
            this.rdOrgTot.TabStop = true;
            this.rdOrgTot.Tag = "1";
            this.rdOrgTot.Text = "Total";
            this.rdOrgTot.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rdOrgTot.UseVisualStyleBackColor = true;
            this.rdOrgTot.CheckedChanged += new System.EventHandler(this.escolheCalculo);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(596, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Dados:";
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "Pontos",
            "Níveis",
            "Tempo",
            "Nº Jogos"});
            this.comboBox3.Location = new System.Drawing.Point(640, 6);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(106, 21);
            this.comboBox3.TabIndex = 9;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(6, 6);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(584, 434);
            this.zedGraphControl1.TabIndex = 0;
            // 
            // FormRecordes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRecordes";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormRecordes_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdGraBar;
        private System.Windows.Forms.RadioButton rdGraCir;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox8;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.ComboBox comboBox7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdDifTod;
        private System.Windows.Forms.RadioButton rdDifMed;
        private System.Windows.Forms.RadioButton rdDifFac;
        private System.Windows.Forms.RadioButton rdDifDif;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdOrgMax;
        private System.Windows.Forms.RadioButton rdOrgMed;
        private System.Windows.Forms.RadioButton rdOrgTot;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox3;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.ComboBox comboBox9;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox10;
        private System.Windows.Forms.Label label11;


    }
}