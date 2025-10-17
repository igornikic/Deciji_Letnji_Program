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

            // lblDatum
            this.lblDatum.Text = "Datum prijave:";
            this.lblDatum.Location = new System.Drawing.Point(20, 20);
            this.lblDatum.AutoSize = true;

            // dateTimePickerDatum
            this.dateTimePickerDatum.Location = new System.Drawing.Point(150, 20);
            this.dateTimePickerDatum.Width = 200;

            // lblStatus
            this.lblStatus.Text = "Status:";
            this.lblStatus.Location = new System.Drawing.Point(20, 60);
            this.lblStatus.AutoSize = true;

            // cmbStatus
            this.cmbStatus.Location = new System.Drawing.Point(150, 60);
            this.cmbStatus.Width = 200;

            // lblAktivnost
            this.lblAktivnost.Text = "Aktivnost:";
            this.lblAktivnost.Location = new System.Drawing.Point(20, 100);
            this.lblAktivnost.AutoSize = true;

            // cmbAktivnosti
            this.cmbAktivnosti.Location = new System.Drawing.Point(150, 100);
            this.cmbAktivnosti.Width = 200;
            this.cmbAktivnosti.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // lblRoditelj
            this.lblRoditelj.Text = "Roditelj:";
            this.lblRoditelj.Location = new System.Drawing.Point(20, 140);
            this.lblRoditelj.AutoSize = true;

            // cmbRoditelji
            this.cmbRoditelji.Location = new System.Drawing.Point(150, 140);
            this.cmbRoditelji.Width = 200;
            this.cmbRoditelji.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // lblDete
            this.lblDete.Text = "Dete:";
            this.lblDete.Location = new System.Drawing.Point(20, 180);
            this.lblDete.AutoSize = true;

            // cmbDeca
            this.cmbDeca.Location = new System.Drawing.Point(150, 180);
            this.cmbDeca.Width = 200;
            this.cmbDeca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // btnSacuvaj
            this.btnSacuvaj.Text = "Sačuvaj";
            this.btnSacuvaj.Location = new System.Drawing.Point(150, 230);
            this.btnSacuvaj.Width = 100;

            // Form
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
