namespace Deciji_Letnji_Program.Forme
{
    partial class EvaluacijaDodajIzmeni
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
            this.lblOcena = new System.Windows.Forms.Label();
            this.numOcena = new System.Windows.Forms.NumericUpDown();
            this.lblDatum = new System.Windows.Forms.Label();
            this.dateDatum = new System.Windows.Forms.DateTimePicker();
            this.lblOpis = new System.Windows.Forms.Label();
            this.txtOpis = new System.Windows.Forms.TextBox();
            this.lblAktivnost = new System.Windows.Forms.Label();
            this.comboAktivnost = new System.Windows.Forms.ComboBox();
            this.lblAngazovanoLice = new System.Windows.Forms.Label();
            this.comboAngazovanoLice = new System.Windows.Forms.ComboBox();
            this.btnSacuvaj = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.numOcena)).BeginInit();
            this.SuspendLayout();

            // lblOcena
            this.lblOcena.Location = new System.Drawing.Point(30, 20);
            this.lblOcena.Size = new System.Drawing.Size(100, 23);
            this.lblOcena.Text = "Ocena:";

            // numOcena
            this.numOcena.Location = new System.Drawing.Point(160, 20);
            this.numOcena.Minimum = 1;
            this.numOcena.Maximum = 10;
            this.numOcena.Size = new System.Drawing.Size(100, 22);
            this.numOcena.Value = 5;

            // lblDatum
            this.lblDatum.Location = new System.Drawing.Point(30, 60);
            this.lblDatum.Size = new System.Drawing.Size(100, 23);
            this.lblDatum.Text = "Datum:";

            // dateDatum
            this.dateDatum.Location = new System.Drawing.Point(160, 60);
            this.dateDatum.Size = new System.Drawing.Size(240, 22);

            // lblOpis
            this.lblOpis.Location = new System.Drawing.Point(30, 100);
            this.lblOpis.Size = new System.Drawing.Size(100, 23);
            this.lblOpis.Text = "Opis:";

            // txtOpis
            this.txtOpis.Location = new System.Drawing.Point(160, 100);
            this.txtOpis.Size = new System.Drawing.Size(240, 60);
            this.txtOpis.Multiline = true;

            // lblAktivnost
            this.lblAktivnost.Location = new System.Drawing.Point(30, 180);
            this.lblAktivnost.Size = new System.Drawing.Size(100, 23);
            this.lblAktivnost.Text = "Aktivnost:";

            // comboAktivnost
            this.comboAktivnost.Location = new System.Drawing.Point(160, 180);
            this.comboAktivnost.Size = new System.Drawing.Size(240, 24);
            this.comboAktivnost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // lblAngazovanoLice
            this.lblAngazovanoLice.Location = new System.Drawing.Point(30, 220);
            this.lblAngazovanoLice.Size = new System.Drawing.Size(130, 23);
            this.lblAngazovanoLice.Text = "Angažovano lice:";

            // comboAngazovanoLice
            this.comboAngazovanoLice.Location = new System.Drawing.Point(160, 220);
            this.comboAngazovanoLice.Size = new System.Drawing.Size(240, 24);
            this.comboAngazovanoLice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // btnSacuvaj
            this.btnSacuvaj.Location = new System.Drawing.Point(160, 270);
            this.btnSacuvaj.Size = new System.Drawing.Size(120, 35);
            this.btnSacuvaj.Text = "Sačuvaj";
            this.btnSacuvaj.UseVisualStyleBackColor = true;

            // EvaluacijaDodajIzmeni
            this.ClientSize = new System.Drawing.Size(450, 330);
            this.Controls.Add(this.lblOcena);
            this.Controls.Add(this.numOcena);
            this.Controls.Add(this.lblDatum);
            this.Controls.Add(this.dateDatum);
            this.Controls.Add(this.lblOpis);
            this.Controls.Add(this.txtOpis);
            this.Controls.Add(this.lblAktivnost);
            this.Controls.Add(this.comboAktivnost);
            this.Controls.Add(this.lblAngazovanoLice);
            this.Controls.Add(this.comboAngazovanoLice);
            this.Controls.Add(this.btnSacuvaj);
            this.Name = "EvaluacijaDodajIzmeni";
            this.Text = "Dodaj / Izmeni Evaluaciju";

            ((System.ComponentModel.ISupportInitialize)(this.numOcena)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblOcena;
        private System.Windows.Forms.NumericUpDown numOcena;
        private System.Windows.Forms.Label lblDatum;
        private System.Windows.Forms.DateTimePicker dateDatum;
        private System.Windows.Forms.Label lblOpis;
        private System.Windows.Forms.TextBox txtOpis;
        private System.Windows.Forms.Label lblAktivnost;
        private System.Windows.Forms.ComboBox comboAktivnost;
        private System.Windows.Forms.Label lblAngazovanoLice;
        private System.Windows.Forms.ComboBox comboAngazovanoLice;
        private System.Windows.Forms.Button btnSacuvaj;
    }
}
