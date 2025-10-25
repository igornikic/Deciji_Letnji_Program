using System;
using System.Windows.Forms;

namespace Deciji_Letnji_Program.Forme
{
    partial class PovredaDodajIzmeni
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox cmbDeca;
        private Label lblDete;
        private Label lblDatum;
        private DateTimePicker dateDatum;
        private Label lblOpis;
        private TextBox txtOpis;
        private Label lblMere;
        private TextBox txtMere;
        private Button btnDodaj;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblDete = new Label();
            this.cmbDeca = new ComboBox();
            this.lblDatum = new Label();
            this.dateDatum = new DateTimePicker();
            this.lblOpis = new Label();
            this.txtOpis = new TextBox();
            this.lblMere = new Label();
            this.txtMere = new TextBox();
            this.btnDodaj = new Button();

            this.SuspendLayout();
            // 
            // lblDete
            // 
            this.lblDete.Text = "Izaberi dete:";
            this.lblDete.Location = new System.Drawing.Point(20, 20);
            this.lblDete.AutoSize = true;
            // 
            // cmbDeca
            // 
            this.cmbDeca.Location = new System.Drawing.Point(140, 17);
            this.cmbDeca.Width = 200;
            this.cmbDeca.DropDownStyle = ComboBoxStyle.DropDownList;
            // 
            // lblDatum
            // 
            this.lblDatum.Text = "Datum povrede:";
            this.lblDatum.Location = new System.Drawing.Point(20, 60);
            this.lblDatum.AutoSize = true;
            // 
            // dateDatum
            // 
            this.dateDatum.Location = new System.Drawing.Point(140, 57);
            this.dateDatum.Width = 200;
            this.dateDatum.Value = DateTime.Now;
            // 
            // lblOpis
            // 
            this.lblOpis.Text = "Opis povrede:";
            this.lblOpis.Location = new System.Drawing.Point(20, 100);
            this.lblOpis.AutoSize = true;
            // 
            // txtOpis
            // 
            this.txtOpis.Location = new System.Drawing.Point(140, 97);
            this.txtOpis.Width = 200;
            this.txtOpis.Height = 60;
            this.txtOpis.Multiline = true;
            // 
            // lblMere
            // 
            this.lblMere.Text = "Preduzete mere:";
            this.lblMere.Location = new System.Drawing.Point(20, 175);
            this.lblMere.AutoSize = true;
            // 
            // txtMere
            // 
            this.txtMere.Location = new System.Drawing.Point(140, 172);
            this.txtMere.Width = 200;
            this.txtMere.Height = 60;
            this.txtMere.Multiline = true;
            // 
            // btnDodaj
            // 
            this.btnDodaj.Text = "Dodaj povredu";
            this.btnDodaj.Location = new System.Drawing.Point(140, 250);
            this.btnDodaj.Width = 200;
            this.btnDodaj.Height = 35;
            // 
            // PovredaDodajIzmeni (Form)
            // 
            this.ClientSize = new System.Drawing.Size(380, 310);
            this.Controls.Add(this.lblDete);
            this.Controls.Add(this.cmbDeca);
            this.Controls.Add(this.lblDatum);
            this.Controls.Add(this.dateDatum);
            this.Controls.Add(this.lblOpis);
            this.Controls.Add(this.txtOpis);
            this.Controls.Add(this.lblMere);
            this.Controls.Add(this.txtMere);
            this.Controls.Add(this.btnDodaj);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Prijava povrede";
            this.MaximizeBox = false;

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
