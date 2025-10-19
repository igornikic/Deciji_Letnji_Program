using System.Windows.Forms;

namespace Deciji_Letnji_Program.Forme
{
    partial class EvaluacijaDodajIzmeni
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblAktivnost = new System.Windows.Forms.Label();
            this.cmbAktivnosti = new System.Windows.Forms.ComboBox();
            this.lblAngazovano = new System.Windows.Forms.Label();
            this.cmbAngazovanaLica = new System.Windows.Forms.ComboBox();
            this.lblOcena = new System.Windows.Forms.Label();
            this.numOcena = new System.Windows.Forms.NumericUpDown();
            this.lblDatum = new System.Windows.Forms.Label();
            this.dateDatum = new System.Windows.Forms.DateTimePicker();
            this.lblOpis = new System.Windows.Forms.Label();
            this.txtOpis = new System.Windows.Forms.TextBox();
            this.btnSacuvaj = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.numOcena)).BeginInit();
            this.SuspendLayout();

            // lblAktivnost
            this.lblAktivnost.Location = new System.Drawing.Point(30, 20);
            this.lblAktivnost.Text = "Aktivnost:";

            // cmbAktivnosti
            this.cmbAktivnosti.Location = new System.Drawing.Point(150, 20);
            this.cmbAktivnosti.Size = new System.Drawing.Size(250, 24);

            // lblAngazovano
            this.lblAngazovano.Location = new System.Drawing.Point(30, 60);
            this.lblAngazovano.Text = "Angažovano lice:";

            // cmbAngazovanaLica
            this.cmbAngazovanaLica.Location = new System.Drawing.Point(150, 60);
            this.cmbAngazovanaLica.Size = new System.Drawing.Size(250, 24);

            // lblOcena
            this.lblOcena.Location = new System.Drawing.Point(30, 100);
            this.lblOcena.Text = "Ocena (1–10):";

            // numOcena
            this.numOcena.Location = new System.Drawing.Point(150, 100);
            this.numOcena.Minimum = 1;
            this.numOcena.Maximum = 10;

            // lblDatum
            this.lblDatum.Location = new System.Drawing.Point(30, 140);
            this.lblDatum.Text = "Datum:";

            // dateDatum
            this.dateDatum.Location = new System.Drawing.Point(150, 140);
            this.dateDatum.Size = new System.Drawing.Size(250, 22);

            // lblOpis
            this.lblOpis.Location = new System.Drawing.Point(30, 180);
            this.lblOpis.Text = "Opis:";

            // txtOpis
            this.txtOpis.Location = new System.Drawing.Point(150, 180);
            this.txtOpis.Size = new System.Drawing.Size(250, 80);
            this.txtOpis.Multiline = true;

            // btnSacuvaj
            this.btnSacuvaj.Location = new System.Drawing.Point(150, 280);
            this.btnSacuvaj.Size = new System.Drawing.Size(120, 35);
            this.btnSacuvaj.Text = "Sačuvaj";

            // Form
            this.ClientSize = new System.Drawing.Size(450, 340);
            this.Controls.AddRange(new Control[]
            {
                lblAktivnost, cmbAktivnosti, lblAngazovano, cmbAngazovanaLica,
                lblOcena, numOcena, lblDatum, dateDatum, lblOpis, txtOpis, btnSacuvaj
            });
            this.Text = "Dodaj Evaluaciju";

            ((System.ComponentModel.ISupportInitialize)(this.numOcena)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblAktivnost;
        private System.Windows.Forms.ComboBox cmbAktivnosti;
        private System.Windows.Forms.Label lblAngazovano;
        private System.Windows.Forms.ComboBox cmbAngazovanaLica;
        private System.Windows.Forms.Label lblOcena;
        private System.Windows.Forms.NumericUpDown numOcena;
        private System.Windows.Forms.Label lblDatum;
        private System.Windows.Forms.DateTimePicker dateDatum;
        private System.Windows.Forms.Label lblOpis;
        private System.Windows.Forms.TextBox txtOpis;
        private System.Windows.Forms.Button btnSacuvaj;
    }
}
