namespace Deciji_Letnji_Program.Forme
{
    partial class LokacijaDodajIzmeni
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblNaziv = new System.Windows.Forms.Label();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.lblTip = new System.Windows.Forms.Label();
            this.radioOtvoreni = new System.Windows.Forms.RadioButton();
            this.radioZatvoreni = new System.Windows.Forms.RadioButton();
            this.lblAdresa = new System.Windows.Forms.Label();
            this.txtAdresa = new System.Windows.Forms.TextBox();
            this.lblKapacitet = new System.Windows.Forms.Label();
            this.numKapacitet = new System.Windows.Forms.NumericUpDown();
            this.lblOprema = new System.Windows.Forms.Label();
            this.txtOprema = new System.Windows.Forms.TextBox();
            this.btnSacuvaj = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.numKapacitet)).BeginInit();
            this.SuspendLayout();

            // lblNaziv
            this.lblNaziv.Location = new System.Drawing.Point(30, 20);
            this.lblNaziv.Name = "lblNaziv";
            this.lblNaziv.Size = new System.Drawing.Size(100, 23);
            this.lblNaziv.Text = "Naziv:";

            // txtNaziv
            this.txtNaziv.Location = new System.Drawing.Point(160, 20);
            this.txtNaziv.Size = new System.Drawing.Size(240, 22);

            // lblTip
            this.lblTip.Location = new System.Drawing.Point(30, 60);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(100, 23);
            this.lblTip.Text = "Tip:";

            // radioOtvoreni
            this.radioOtvoreni.Location = new System.Drawing.Point(160, 60);
            this.radioOtvoreni.Name = "radioOtvoreni";
            this.radioOtvoreni.Size = new System.Drawing.Size(150, 22);
            this.radioOtvoreni.Text = "Otvoreni prostor";
            this.radioOtvoreni.Checked = true;
            this.radioOtvoreni.UseVisualStyleBackColor = true;

            // radioZatvoreni
            this.radioZatvoreni.Location = new System.Drawing.Point(160, 90);
            this.radioZatvoreni.Name = "radioZatvoreni";
            this.radioZatvoreni.Size = new System.Drawing.Size(150, 22);
            this.radioZatvoreni.Text = "Zatvoreni objekat";
            this.radioZatvoreni.UseVisualStyleBackColor = true;

            // lblAdresa
            this.lblAdresa.Location = new System.Drawing.Point(30, 130);
            this.lblAdresa.Size = new System.Drawing.Size(100, 23);
            this.lblAdresa.Text = "Adresa:";

            // txtAdresa
            this.txtAdresa.Location = new System.Drawing.Point(160, 130);
            this.txtAdresa.Size = new System.Drawing.Size(240, 22);

            // lblKapacitet
            this.lblKapacitet.Location = new System.Drawing.Point(30, 170);
            this.lblKapacitet.Size = new System.Drawing.Size(100, 23);
            this.lblKapacitet.Text = "Kapacitet:";

            // numKapacitet
            this.numKapacitet.Location = new System.Drawing.Point(160, 170);
            this.numKapacitet.Maximum = 10000;
            this.numKapacitet.Minimum = 0;
            this.numKapacitet.Name = "numKapacitet";
            this.numKapacitet.Size = new System.Drawing.Size(120, 22);
            this.numKapacitet.Value = 0;

            // lblOprema
            this.lblOprema.Location = new System.Drawing.Point(30, 210);
            this.lblOprema.Size = new System.Drawing.Size(100, 23);
            this.lblOprema.Text = "Oprema:";

            // txtOprema
            this.txtOprema.Location = new System.Drawing.Point(160, 210);
            this.txtOprema.Size = new System.Drawing.Size(240, 22);

            // btnSacuvaj
            this.btnSacuvaj.Location = new System.Drawing.Point(160, 250);
            this.btnSacuvaj.Size = new System.Drawing.Size(120, 35);
            this.btnSacuvaj.Text = "Sačuvaj";
            this.btnSacuvaj.UseVisualStyleBackColor = true;

            // Dodavanje kontrola na formu
            this.Controls.Add(this.lblNaziv);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.radioOtvoreni);
            this.Controls.Add(this.radioZatvoreni);
            this.Controls.Add(this.lblAdresa);
            this.Controls.Add(this.txtAdresa);
            this.Controls.Add(this.lblKapacitet);
            this.Controls.Add(this.numKapacitet);
            this.Controls.Add(this.lblOprema);
            this.Controls.Add(this.txtOprema);
            this.Controls.Add(this.btnSacuvaj);

            // Form properties
            this.ClientSize = new System.Drawing.Size(450, 320);
            this.Name = "LokacijaDodajIzmeni";
            this.Text = "Dodaj / Izmeni Lokaciju";

            ((System.ComponentModel.ISupportInitialize)(this.numKapacitet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }



        #endregion

        private System.Windows.Forms.Label lblNaziv;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.RadioButton radioOtvoreni;
        private System.Windows.Forms.RadioButton radioZatvoreni;
        private System.Windows.Forms.Label lblAdresa;
        private System.Windows.Forms.TextBox txtAdresa;
        private System.Windows.Forms.Label lblKapacitet;
        private System.Windows.Forms.NumericUpDown numKapacitet;
        private System.Windows.Forms.Label lblOprema;
        private System.Windows.Forms.TextBox txtOprema;
        private System.Windows.Forms.Button btnSacuvaj;
    }
}
