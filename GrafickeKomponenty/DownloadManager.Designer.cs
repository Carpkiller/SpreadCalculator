namespace SpreadCalculator.GrafickeKomponenty
{
    partial class DownloadManager
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
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxKomodita = new System.Windows.Forms.ComboBox();
            this.comboBoxPocetRokov = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.comboBoxRok = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxMesiac2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxMesiac1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxVsetko = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(127, 180);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(331, 180);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Zrusit";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Komodita";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pocet rokov";
            // 
            // comboBoxKomodita
            // 
            this.comboBoxKomodita.FormattingEnabled = true;
            this.comboBoxKomodita.Location = new System.Drawing.Point(137, 22);
            this.comboBoxKomodita.Name = "comboBoxKomodita";
            this.comboBoxKomodita.Size = new System.Drawing.Size(228, 21);
            this.comboBoxKomodita.TabIndex = 4;
            this.comboBoxKomodita.SelectedIndexChanged += new System.EventHandler(this.comboBoxKomodita_SelectedIndexChanged);
            // 
            // comboBoxPocetRokov
            // 
            this.comboBoxPocetRokov.FormattingEnabled = true;
            this.comboBoxPocetRokov.Items.AddRange(new object[] {
            "5",
            "10",
            "15"});
            this.comboBoxPocetRokov.Location = new System.Drawing.Point(111, 72);
            this.comboBoxPocetRokov.Name = "comboBoxPocetRokov";
            this.comboBoxPocetRokov.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPocetRokov.TabIndex = 5;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(1, 221);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(467, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // comboBoxRok
            // 
            this.comboBoxRok.FormattingEnabled = true;
            this.comboBoxRok.Items.AddRange(new object[] {
            "5",
            "10",
            "15"});
            this.comboBoxRok.Location = new System.Drawing.Point(111, 125);
            this.comboBoxRok.Name = "comboBoxRok";
            this.comboBoxRok.Size = new System.Drawing.Size(121, 21);
            this.comboBoxRok.TabIndex = 8;
            this.comboBoxRok.SelectedIndexChanged += new System.EventHandler(this.comboBoxRok_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Posledny rok";
            // 
            // comboBoxMesiac2
            // 
            this.comboBoxMesiac2.FormattingEnabled = true;
            this.comboBoxMesiac2.Items.AddRange(new object[] {
            "5",
            "10",
            "15"});
            this.comboBoxMesiac2.Location = new System.Drawing.Point(366, 97);
            this.comboBoxMesiac2.Name = "comboBoxMesiac2";
            this.comboBoxMesiac2.Size = new System.Drawing.Size(90, 21);
            this.comboBoxMesiac2.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(254, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Kontraktny mesiac 2 ";
            // 
            // comboBoxMesiac1
            // 
            this.comboBoxMesiac1.FormattingEnabled = true;
            this.comboBoxMesiac1.Items.AddRange(new object[] {
            "5",
            "10",
            "15"});
            this.comboBoxMesiac1.Location = new System.Drawing.Point(366, 67);
            this.comboBoxMesiac1.Name = "comboBoxMesiac1";
            this.comboBoxMesiac1.Size = new System.Drawing.Size(90, 21);
            this.comboBoxMesiac1.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(254, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Kontraktny mesiac 1 ";
            // 
            // checkBoxVsetko
            // 
            this.checkBoxVsetko.AutoSize = true;
            this.checkBoxVsetko.Location = new System.Drawing.Point(321, 128);
            this.checkBoxVsetko.Name = "checkBoxVsetko";
            this.checkBoxVsetko.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxVsetko.Size = new System.Drawing.Size(59, 17);
            this.checkBoxVsetko.TabIndex = 13;
            this.checkBoxVsetko.Text = "Vsetko";
            this.checkBoxVsetko.UseVisualStyleBackColor = true;
            // 
            // DownloadManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 245);
            this.Controls.Add(this.checkBoxVsetko);
            this.Controls.Add(this.comboBoxMesiac2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxMesiac1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxRok);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.comboBoxPocetRokov);
            this.Controls.Add(this.comboBoxKomodita);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "DownloadManager";
            this.Text = "DownloadManager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxKomodita;
        private System.Windows.Forms.ComboBox comboBoxPocetRokov;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox comboBoxRok;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxMesiac2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxMesiac1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxVsetko;
    }
}