using System.Windows.Forms;

namespace Deciji_Letnji_Program.Forme
{
    partial class EvidencijaPrisustva
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewUcesca = new System.Windows.Forms.DataGridView();
            this.lblDeca = new System.Windows.Forms.Label();
            this.lblPratilac = new System.Windows.Forms.Label();
            this.cmbDeca = new System.Windows.Forms.ComboBox();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.chkPrisustvo = new System.Windows.Forms.CheckBox();
            this.cmbPratilac = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUcesca)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewUcesca
            // 
            this.dataGridViewUcesca.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUcesca.Location = new System.Drawing.Point(20, 20);
            this.dataGridViewUcesca.Name = "dataGridViewUcesca";
            this.dataGridViewUcesca.RowHeadersWidth = 51;
            this.dataGridViewUcesca.Size = new System.Drawing.Size(756, 200);
            this.dataGridViewUcesca.TabIndex = 0;
            // 
            // lblDeca
            // 
            this.lblDeca.AutoSize = true;
            this.lblDeca.Location = new System.Drawing.Point(20, 240);
            this.lblDeca.Name = "lblDeca";
            this.lblDeca.Size = new System.Drawing.Size(39, 16);
            this.lblDeca.TabIndex = 1;
            this.lblDeca.Text = "Dete:";
            // 
            // lblPratilac
            // 
            this.lblPratilac.AutoSize = true;
            this.lblPratilac.Location = new System.Drawing.Point(20, 280);
            this.lblPratilac.Name = "lblPratilac";
            this.lblPratilac.Size = new System.Drawing.Size(55, 16);
            this.lblPratilac.TabIndex = 3;
            this.lblPratilac.Text = "Pratilac:";
            // 
            // cmbDeca
            // 
            this.cmbDeca.Location = new System.Drawing.Point(150, 240);
            this.cmbDeca.Name = "cmbDeca";
            this.cmbDeca.Size = new System.Drawing.Size(200, 24);
            this.cmbDeca.TabIndex = 2;
            // 
            // btnDodaj
            // 
            this.btnDodaj.Location = new System.Drawing.Point(150, 360);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(75, 23);
            this.btnDodaj.TabIndex = 6;
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // btnObrisi
            // 
            this.btnObrisi.Location = new System.Drawing.Point(260, 360);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(75, 23);
            this.btnObrisi.TabIndex = 7;
            this.btnObrisi.Text = "Obriši";
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // chkPrisustvo
            // 
            this.chkPrisustvo.Location = new System.Drawing.Point(150, 320);
            this.chkPrisustvo.Name = "chkPrisustvo";
            this.chkPrisustvo.Size = new System.Drawing.Size(100, 24);
            this.chkPrisustvo.TabIndex = 5;
            this.chkPrisustvo.Text = "Prisutan";
            // 
            // cmbPratilac
            // 
            this.cmbPratilac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPratilac.Location = new System.Drawing.Point(150, 280);
            this.cmbPratilac.Name = "cmbPratilac";
            this.cmbPratilac.Size = new System.Drawing.Size(200, 24);
            this.cmbPratilac.TabIndex = 4;
            // 
            // EvidencijaPrisustva
            // 
            this.ClientSize = new System.Drawing.Size(794, 420);
            this.Controls.Add(this.dataGridViewUcesca);
            this.Controls.Add(this.lblDeca);
            this.Controls.Add(this.cmbDeca);
            this.Controls.Add(this.lblPratilac);
            this.Controls.Add(this.cmbPratilac);
            this.Controls.Add(this.chkPrisustvo);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.btnObrisi);
            this.Name = "EvidencijaPrisustva";
            this.Text = "Evidencija prisustva";
            this.Load += new System.EventHandler(this.EvidencijaPrisustva_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUcesca)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DataGridView dataGridViewUcesca;
        private System.Windows.Forms.Label lblDeca;
        private System.Windows.Forms.ComboBox cmbDeca;
        private System.Windows.Forms.CheckBox chkPrisustvo;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.ComboBox cmbPratilac;
        private System.Windows.Forms.Label lblPratilac;
    }
}