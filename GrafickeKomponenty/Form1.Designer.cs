namespace SpreadCalculator
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.koniecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxKomodity = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxKontrakt1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxKontrakt2 = new System.Windows.Forms.ComboBox();
            this.zg1 = new ZedGraph.ZedGraphControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.comboBoxMesiace2 = new System.Windows.Forms.ComboBox();
            this.comboBoxMesiace1 = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.comboBoxMesiace2Sez = new System.Windows.Forms.ComboBox();
            this.comboBoxKontrakt2Sez = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.textBoxRoky = new System.Windows.Forms.TextBox();
            this.comboBoxKontrakt1Sez = new System.Windows.Forms.ComboBox();
            this.comboBoxMesiace1Sez = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelKomodity2 = new System.Windows.Forms.Label();
            this.comboBoxKomodity2 = new System.Windows.Forms.ComboBox();
            this.checkBoxDruhyKontrakt = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(6, 398);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Nacitaj data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(148, 398);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Zobraz graf";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1370, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.koniecToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // koniecToolStripMenuItem
            // 
            this.koniecToolStripMenuItem.Name = "koniecToolStripMenuItem";
            this.koniecToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.koniecToolStripMenuItem.Text = "Koniec";
            // 
            // comboBoxKomodity
            // 
            this.comboBoxKomodity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxKomodity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKomodity.FormattingEnabled = true;
            this.comboBoxKomodity.Location = new System.Drawing.Point(84, 27);
            this.comboBoxKomodity.Name = "comboBoxKomodity";
            this.comboBoxKomodity.Size = new System.Drawing.Size(170, 21);
            this.comboBoxKomodity.TabIndex = 4;
            this.comboBoxKomodity.TextChanged += new System.EventHandler(this.comboBoxKomodity_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Futures :";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Kontrakt1 :";
            // 
            // comboBoxKontrakt1
            // 
            this.comboBoxKontrakt1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxKontrakt1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKontrakt1.FormattingEnabled = true;
            this.comboBoxKontrakt1.Items.AddRange(new object[] {
            "----------"});
            this.comboBoxKontrakt1.Location = new System.Drawing.Point(77, 8);
            this.comboBoxKontrakt1.Name = "comboBoxKontrakt1";
            this.comboBoxKontrakt1.Size = new System.Drawing.Size(81, 21);
            this.comboBoxKontrakt1.TabIndex = 6;
            this.comboBoxKontrakt1.TextChanged += new System.EventHandler(this.comboBoxKontrakt1_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Kontrakt2 :";
            // 
            // comboBoxKontrakt2
            // 
            this.comboBoxKontrakt2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxKontrakt2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKontrakt2.FormattingEnabled = true;
            this.comboBoxKontrakt2.Items.AddRange(new object[] {
            "----------"});
            this.comboBoxKontrakt2.Location = new System.Drawing.Point(77, 35);
            this.comboBoxKontrakt2.Name = "comboBoxKontrakt2";
            this.comboBoxKontrakt2.Size = new System.Drawing.Size(81, 21);
            this.comboBoxKontrakt2.TabIndex = 8;
            this.comboBoxKontrakt2.TextChanged += new System.EventHandler(this.comboBoxKontrakt2_TextChanged);
            // 
            // zg1
            // 
            this.zg1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zg1.Location = new System.Drawing.Point(302, 30);
            this.zg1.Name = "zg1";
            this.zg1.ScrollGrace = 0D;
            this.zg1.ScrollMaxX = 0D;
            this.zg1.ScrollMaxY = 0D;
            this.zg1.ScrollMaxY2 = 0D;
            this.zg1.ScrollMinX = 0D;
            this.zg1.ScrollMinY = 0D;
            this.zg1.ScrollMinY2 = 0D;
            this.zg1.Size = new System.Drawing.Size(1068, 525);
            this.zg1.TabIndex = 10;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 562);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1370, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "Ready";
            // 
            // comboBoxMesiace2
            // 
            this.comboBoxMesiace2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxMesiace2.DisplayMember = "----------";
            this.comboBoxMesiace2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMesiace2.FormattingEnabled = true;
            this.comboBoxMesiace2.Items.AddRange(new object[] {
            "----------"});
            this.comboBoxMesiace2.Location = new System.Drawing.Point(192, 35);
            this.comboBoxMesiace2.Name = "comboBoxMesiace2";
            this.comboBoxMesiace2.Size = new System.Drawing.Size(81, 21);
            this.comboBoxMesiace2.TabIndex = 13;
            this.comboBoxMesiace2.ValueMember = "----------";
            // 
            // comboBoxMesiace1
            // 
            this.comboBoxMesiace1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxMesiace1.DisplayMember = "----------";
            this.comboBoxMesiace1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMesiace1.FormatString = "-------";
            this.comboBoxMesiace1.FormattingEnabled = true;
            this.comboBoxMesiace1.Items.AddRange(new object[] {
            "----------"});
            this.comboBoxMesiace1.Location = new System.Drawing.Point(192, 8);
            this.comboBoxMesiace1.Name = "comboBoxMesiace1";
            this.comboBoxMesiace1.Size = new System.Drawing.Size(81, 21);
            this.comboBoxMesiace1.TabIndex = 12;
            this.comboBoxMesiace1.ValueMember = "----------";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(77, 72);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(38, 20);
            this.button3.TabIndex = 14;
            this.button3.Text = "<==";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(120, 72);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(38, 20);
            this.button4.TabIndex = 15;
            this.button4.Text = "==>";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(4, 98);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(267, 294);
            this.textBox1.TabIndex = 16;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 102);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(284, 453);
            this.tabControl1.TabIndex = 17;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.comboBoxMesiace2);
            this.tabPage1.Controls.Add(this.comboBoxKontrakt1);
            this.tabPage1.Controls.Add(this.comboBoxMesiace1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.comboBoxKontrakt2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(276, 427);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Spready";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(276, 427);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Grafy";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(276, 427);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Testy";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.comboBoxMesiace2Sez);
            this.tabPage4.Controls.Add(this.comboBoxKontrakt2Sez);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Controls.Add(this.button5);
            this.tabPage4.Controls.Add(this.textBoxRoky);
            this.tabPage4.Controls.Add(this.comboBoxKontrakt1Sez);
            this.tabPage4.Controls.Add(this.comboBoxMesiace1Sez);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(276, 427);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Sezonnost";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // comboBoxMesiace2Sez
            // 
            this.comboBoxMesiace2Sez.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxMesiace2Sez.DisplayMember = "----------";
            this.comboBoxMesiace2Sez.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMesiace2Sez.FormattingEnabled = true;
            this.comboBoxMesiace2Sez.Items.AddRange(new object[] {
            "----------"});
            this.comboBoxMesiace2Sez.Location = new System.Drawing.Point(192, 42);
            this.comboBoxMesiace2Sez.Name = "comboBoxMesiace2Sez";
            this.comboBoxMesiace2Sez.Size = new System.Drawing.Size(81, 21);
            this.comboBoxMesiace2Sez.TabIndex = 28;
            this.comboBoxMesiace2Sez.ValueMember = "----------";
            // 
            // comboBoxKontrakt2Sez
            // 
            this.comboBoxKontrakt2Sez.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxKontrakt2Sez.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKontrakt2Sez.FormattingEnabled = true;
            this.comboBoxKontrakt2Sez.Items.AddRange(new object[] {
            "----------"});
            this.comboBoxKontrakt2Sez.Location = new System.Drawing.Point(77, 42);
            this.comboBoxKontrakt2Sez.Name = "comboBoxKontrakt2Sez";
            this.comboBoxKontrakt2Sez.Size = new System.Drawing.Size(81, 21);
            this.comboBoxKontrakt2Sez.TabIndex = 26;
            this.comboBoxKontrakt2Sez.TextChanged += new System.EventHandler(this.comboBoxKontrakt2Sez_TextChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Kontrakt2 :";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(6, 123);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 25;
            this.button5.Text = "Zobraz graf";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBoxRoky
            // 
            this.textBoxRoky.Location = new System.Drawing.Point(173, 80);
            this.textBoxRoky.Name = "textBoxRoky";
            this.textBoxRoky.Size = new System.Drawing.Size(100, 20);
            this.textBoxRoky.TabIndex = 24;
            this.textBoxRoky.Text = "1";
            // 
            // comboBoxKontrakt1Sez
            // 
            this.comboBoxKontrakt1Sez.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxKontrakt1Sez.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKontrakt1Sez.FormattingEnabled = true;
            this.comboBoxKontrakt1Sez.Items.AddRange(new object[] {
            "----------"});
            this.comboBoxKontrakt1Sez.Location = new System.Drawing.Point(77, 15);
            this.comboBoxKontrakt1Sez.Name = "comboBoxKontrakt1Sez";
            this.comboBoxKontrakt1Sez.Size = new System.Drawing.Size(81, 21);
            this.comboBoxKontrakt1Sez.TabIndex = 16;
            this.comboBoxKontrakt1Sez.TextChanged += new System.EventHandler(this.comboBoxKontrakt1Sez_TextChanged);
            // 
            // comboBoxMesiace1Sez
            // 
            this.comboBoxMesiace1Sez.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxMesiace1Sez.DisplayMember = "----------";
            this.comboBoxMesiace1Sez.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMesiace1Sez.FormatString = "-------";
            this.comboBoxMesiace1Sez.FormattingEnabled = true;
            this.comboBoxMesiace1Sez.Items.AddRange(new object[] {
            "----------"});
            this.comboBoxMesiace1Sez.Location = new System.Drawing.Point(192, 15);
            this.comboBoxMesiace1Sez.Name = "comboBoxMesiace1Sez";
            this.comboBoxMesiace1Sez.Size = new System.Drawing.Size(81, 21);
            this.comboBoxMesiace1Sez.TabIndex = 20;
            this.comboBoxMesiace1Sez.ValueMember = "----------";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Kontrakt1 :";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Pocet rokov na porovnanie : ";
            // 
            // labelKomodity2
            // 
            this.labelKomodity2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelKomodity2.AutoSize = true;
            this.labelKomodity2.Location = new System.Drawing.Point(12, 69);
            this.labelKomodity2.Name = "labelKomodity2";
            this.labelKomodity2.Size = new System.Drawing.Size(48, 13);
            this.labelKomodity2.TabIndex = 19;
            this.labelKomodity2.Text = "Futures :";
            this.labelKomodity2.Visible = false;
            // 
            // comboBoxKomodity2
            // 
            this.comboBoxKomodity2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxKomodity2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKomodity2.FormattingEnabled = true;
            this.comboBoxKomodity2.Location = new System.Drawing.Point(84, 66);
            this.comboBoxKomodity2.Name = "comboBoxKomodity2";
            this.comboBoxKomodity2.Size = new System.Drawing.Size(170, 21);
            this.comboBoxKomodity2.TabIndex = 18;
            this.comboBoxKomodity2.Visible = false;
            this.comboBoxKomodity2.TextChanged += new System.EventHandler(this.comboBoxKomodity2_TextChanged);
            // 
            // checkBoxDruhyKontrakt
            // 
            this.checkBoxDruhyKontrakt.AutoSize = true;
            this.checkBoxDruhyKontrakt.Location = new System.Drawing.Point(274, 30);
            this.checkBoxDruhyKontrakt.Name = "checkBoxDruhyKontrakt";
            this.checkBoxDruhyKontrakt.Size = new System.Drawing.Size(15, 14);
            this.checkBoxDruhyKontrakt.TabIndex = 20;
            this.checkBoxDruhyKontrakt.UseVisualStyleBackColor = true;
            this.checkBoxDruhyKontrakt.CheckedChanged += new System.EventHandler(this.checkBoxDruhyKontrakt_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 584);
            this.Controls.Add(this.checkBoxDruhyKontrakt);
            this.Controls.Add(this.labelKomodity2);
            this.Controls.Add(this.comboBoxKomodity2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.zg1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxKomodity);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem koniecToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxKomodity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxKontrakt1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxKontrakt2;
        private ZedGraph.ZedGraphControl zg1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ComboBox comboBoxMesiace2;
        private System.Windows.Forms.ComboBox comboBoxMesiace1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBoxRoky;
        private System.Windows.Forms.ComboBox comboBoxKontrakt1Sez;
        private System.Windows.Forms.ComboBox comboBoxMesiace1Sez;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxMesiace2Sez;
        private System.Windows.Forms.ComboBox comboBoxKontrakt2Sez;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label labelKomodity2;
        private System.Windows.Forms.ComboBox comboBoxKomodity2;
        private System.Windows.Forms.CheckBox checkBoxDruhyKontrakt;
    }
}

