namespace Deciji_Letnji_Program.Forme
{
    partial class PrijavaDodajIzmeni
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
            this.lblDatum = new System.Windows.Forms.Label();
            this.dateTimePickerDatum = new System.Windows.Forms.DateTimePicker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblAktivnost = new System.Windows.Forms.Label();
            this.cmbAktivnosti = new System.Windows.Forms.ComboBox();
            this.lblRoditelj = new System.Windows.Forms.Label();
            this.cmbRoditelji = new System.Windows.Forms.ComboBox();
            this.lblDete = new System.Windows.Forms.Label();
            this.cmbDeca = new System.Windows.Forms.ComboBox();
            this.btnSacuvaj = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDatum
            // 
            this.lblDatum.AutoSize = true;
            this.lblDatum.Location = new System.Drawing.Point(20, 20);
            this.lblDatum.Name = "lblDatum";
            this.lblDatum.Size = new System.Drawing.Size(93, 16);
            this.lblDatum.TabIndex = 0;
            this.lblDatum.Text = "Datum prijave:";
            // 
            // dateTimePickerDatum
            // 
            this.dateTimePickerDatum.Location = new System.Drawing.Point(150, 20);
            this.dateTimePickerDatum.Name = "dateTimePickerDatum";
            this.dateTimePickerDatum.Size = new System.Drawing.Size(200, 22);
            this.dateTimePickerDatum.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(20, 60);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 16);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Status:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.Location = new System.Drawing.Point(150, 60);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(200, 24);
            this.cmbStatus.TabIndex = 3;
            // 
            // lblAktivnost
            // 
            this.lblAktivnost.AutoSize = true;
            this.lblAktivnost.Location = new System.Drawing.Point(20, 100);
            this.lblAktivnost.Name = "lblAktivnost";
            this.lblAktivnost.Size = new System.Drawing.Size(64, 16);
            this.lblAktivnost.TabIndex = 4;
            this.lblAktivnost.Text = "Aktivnost:";
            // 
            // cmbAktivnosti
            // 
            this.cmbAktivnosti.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAktivnosti.Location = new System.Drawing.Point(150, 100);
            this.cmbAktivnosti.Name = "cmbAktivnosti";
            this.cmbAktivnosti.Size = new System.Drawing.Size(200, 24);
            this.cmbAktivnosti.TabIndex = 5;
            // 
            // lblRoditelj
            // 
            this.lblRoditelj.AutoSize = true;
            this.lblRoditelj.Location = new System.Drawing.Point(20, 140);
            this.lblRoditelj.Name = "lblRoditelj";
            this.lblRoditelj.Size = new System.Drawing.Size(56, 16);
            this.lblRoditelj.TabIndex = 6;
            this.lblRoditelj.Text = "Roditelj:";
            // 
            // cmbRoditelji
            // 
            this.cmbRoditelji.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoditelji.Location = new System.Drawing.Point(150, 140);
            this.cmbRoditelji.Name = "cmbRoditelji";
            this.cmbRoditelji.Size = new System.Drawing.Size(200, 24);
            this.cmbRoditelji.TabIndex = 7;
            // 
            // lblDete
            // 
            this.lblDete.AutoSize = true;
            this.lblDete.Location = new System.Drawing.Point(20, 180);
            this.lblDete.Name = "lblDete";
            this.lblDete.Size = new System.Drawing.Size(39, 16);
            this.lblDete.TabIndex = 8;
            this.lblDete.Text = "Dete:";
            // 
            // cmbDeca
            // 
            this.cmbDeca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeca.Location = new System.Drawing.Point(150, 180);
            this.cmbDeca.Name = "cmbDeca";
            this.cmbDeca.Size = new System.Drawing.Size(200, 24);
            this.cmbDeca.TabIndex = 9;
            // 
            // btnSacuvaj
            // 
            this.btnSacuvaj.Location = new System.Drawing.Point(150, 265);
            this.btnSacuvaj.Name = "btnSacuvaj";
            this.btnSacuvaj.Size = new System.Drawing.Size(100, 23);
            this.btnSacuvaj.TabIndex = 10;
            this.btnSacuvaj.Text = "Sačuvaj";
            // 
            // PrijavaDodajIzmeni
            // 
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.lblDatum);
            this.Controls.Add(this.dateTimePickerDatum);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblAktivnost);
            this.Controls.Add(this.cmbAktivnosti);
            this.Controls.Add(this.lblRoditelj);
            this.Controls.Add(this.cmbRoditelji);
            this.Controls.Add(this.lblDete);
            this.Controls.Add(this.cmbDeca);
            this.Controls.Add(this.btnSacuvaj);
            this.Name = "PrijavaDodajIzmeni";
            this.Text = "Dodaj/Ažuriraj prijavu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblDatum;
        private System.Windows.Forms.DateTimePicker dateTimePickerDatum;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblAktivnost;
        private System.Windows.Forms.ComboBox cmbAktivnosti;
        private System.Windows.Forms.Label lblRoditelj;
        private System.Windows.Forms.ComboBox cmbRoditelji;
        private System.Windows.Forms.Label lblDete;
        private System.Windows.Forms.ComboBox cmbDeca;
        private System.Windows.Forms.Button btnSacuvaj;
    }
}
