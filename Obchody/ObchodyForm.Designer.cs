﻿namespace SpreadCalculator.Obchody
{
    partial class ObchodyForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.upravitObchodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zmazatObchodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zobrazitObchodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 65);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(699, 229);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.upravitObchodToolStripMenuItem,
            this.zmazatObchodToolStripMenuItem,
            this.zobrazitObchodToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 92);
            // 
            // upravitObchodToolStripMenuItem
            // 
            this.upravitObchodToolStripMenuItem.Name = "upravitObchodToolStripMenuItem";
            this.upravitObchodToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.upravitObchodToolStripMenuItem.Text = "Upravit obchod";
            this.upravitObchodToolStripMenuItem.Click += new System.EventHandler(this.upravitObchodToolStripMenuItem_Click);
            // 
            // zmazatObchodToolStripMenuItem
            // 
            this.zmazatObchodToolStripMenuItem.Name = "zmazatObchodToolStripMenuItem";
            this.zmazatObchodToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.zmazatObchodToolStripMenuItem.Text = "Zmazat obchod";
            this.zmazatObchodToolStripMenuItem.Click += new System.EventHandler(this.zmazatObchodToolStripMenuItem_Click);
            // 
            // zobrazitObchodToolStripMenuItem
            // 
            this.zobrazitObchodToolStripMenuItem.Name = "zobrazitObchodToolStripMenuItem";
            this.zobrazitObchodToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.zobrazitObchodToolStripMenuItem.Text = "Zobrazit obchod";
            this.zobrazitObchodToolStripMenuItem.Click += new System.EventHandler(this.zobrazitObchodToolStripMenuItem_Click);
            // 
            // ObchodyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 306);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ObchodyForm";
            this.Text = "ObchodyForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem upravitObchodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zmazatObchodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zobrazitObchodToolStripMenuItem;
    }
}