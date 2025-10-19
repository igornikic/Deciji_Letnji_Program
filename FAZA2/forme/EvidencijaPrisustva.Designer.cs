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
            this.SuspendLayout();

            // dataGridViewUcesca
            this.dataGridViewUcesca.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUcesca.Location = new System.Drawing.Point(20, 20);
            this.dataGridViewUcesca.Name = "dataGridViewUcesca";
            this.dataGridViewUcesca.Size = new System.Drawing.Size(500, 200);
            this.dataGridViewUcesca.TabIndex = 0;

            // lblDeca
            this.lblDeca.Text = "Dete:";
            this.lblDeca.Location = new System.Drawing.Point(20, 240);
            this.lblDeca.AutoSize = true;

            // cmbDeca
            this.cmbDeca.Location = new System.Drawing.Point(150, 240);
            this.cmbDeca.Width = 200;

            // txtPratilac
            this.cmbPratilac.Location = new System.Drawing.Point(150, 280);
            this.cmbPratilac.Width = 200;
            this.cmbPratilac.DropDownStyle = ComboBoxStyle.DropDownList;

            // lblPratilac
            this.lblPratilac.Text = "Pratilac:";
            this.lblPratilac.Location = new System.Drawing.Point(20, 280);
            this.lblPratilac.AutoSize = true;

            // chkPrisustvo
            this.chkPrisustvo.Text = "Prisutan";
            this.chkPrisustvo.Location = new System.Drawing.Point(150, 320);
            this.chkPrisustvo.Width = 100;

            // btnDodaj
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.Location = new System.Drawing.Point(150, 360);
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);

            // btnObrisi
            this.btnObrisi.Text = "Obriši";
            this.btnObrisi.Location = new System.Drawing.Point(260, 360);
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);

            // EvidencijaPrisustva
            this.ClientSize = new System.Drawing.Size(550, 420);
            this.Controls.Add(this.dataGridViewUcesca);
            this.Controls.Add(this.lblDeca);
            this.Controls.Add(this.cmbDeca);
            this.Controls.Add(this.lblPratilac);
            this.Controls.Add(this.cmbPratilac);
            this.Controls.Add(this.chkPrisustvo);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.btnObrisi);
            this.Text = "Evidencija prisustva";
            this.Load += new System.EventHandler(this.EvidencijaPrisustva_Load);
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