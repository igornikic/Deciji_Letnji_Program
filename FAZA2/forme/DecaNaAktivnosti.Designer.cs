using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    partial class DecaNaAktivnosti
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewDeca;
        private System.Windows.Forms.Button dugmeDodelaObroka;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewDeca = new System.Windows.Forms.DataGridView();
            this.dugmeDodelaObroka = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDeca)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewDeca
            // 
            this.dataGridViewDeca.AllowUserToAddRows = false;
            this.dataGridViewDeca.AllowUserToDeleteRows = false;
            this.dataGridViewDeca.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDeca.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDeca.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewDeca.MultiSelect = false;
            this.dataGridViewDeca.Name = "dataGridViewDeca";
            this.dataGridViewDeca.ReadOnly = true;
            this.dataGridViewDeca.RowHeadersVisible = false;
            this.dataGridViewDeca.RowHeadersWidth = 51;
            this.dataGridViewDeca.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDeca.Size = new System.Drawing.Size(560, 300);
            this.dataGridViewDeca.TabIndex = 0;
            // 
            // dugmeDodelaObroka
            // 
            this.dugmeDodelaObroka.Location = new System.Drawing.Point(12, 318);
            this.dugmeDodelaObroka.Name = "dugmeDodelaObroka";
            this.dugmeDodelaObroka.Size = new System.Drawing.Size(140, 40);
            this.dugmeDodelaObroka.TabIndex = 2;
            this.dugmeDodelaObroka.Text = "Dodela obroka";
            this.dugmeDodelaObroka.UseVisualStyleBackColor = true;
            this.dugmeDodelaObroka.Click += new System.EventHandler(this.DugmeDodelaObroka_Click);
            // 
            // DecaNaAktivnosti
            // 
            this.ClientSize = new System.Drawing.Size(584, 371);
            this.Controls.Add(this.dugmeDodelaObroka);
            this.Controls.Add(this.dataGridViewDeca);
            this.Name = "DecaNaAktivnosti";
            this.Text = "Deca na aktivnosti";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDeca)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
