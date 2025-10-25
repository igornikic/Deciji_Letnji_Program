using System.Windows.Forms;

namespace Deciji_Letnji_Program.Forme
{
    partial class AktivnostDodajIzmeni
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
            this.cmbTip = new ComboBox();
            this.lblTip = new Label();
            this.lblNaziv = new Label();
            this.txtNaziv = new TextBox();
            this.lblDatum = new Label();
            this.dateDatum = new DateTimePicker();
            this.lblStarosna = new Label();
            this.numStarosnaOd = new NumericUpDown();
            this.lblDo = new Label();
            this.numStarosnaDo = new NumericUpDown();
            this.lblMaxUcesnika = new Label();
            this.numMaxUcesnika = new NumericUpDown();
            this.lblOgranicenja = new Label();
            this.txtOgranicenja = new TextBox();
            this.lblSport = new Label();
            this.txtSport = new TextBox();
            this.lblPosebnaOprema = new Label();
            this.txtPosebnaOprema = new TextBox();
            this.lblPotrebnaOprema = new Label();
            this.txtPotrebnaOprema = new TextBox();
            this.lblPrevoz = new Label();
            this.txtPrevoz = new TextBox();
            this.lblVodic = new Label();
            this.txtVodic = new TextBox();
            this.lblPlanPuta = new Label();
            this.txtPlanPuta = new TextBox();
            this.btnSacuvaj = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.numStarosnaOd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStarosnaDo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxUcesnika)).BeginInit();

            // 
            // lblTip
            // 
            this.lblTip.Text = "Tip aktivnosti:";
            this.lblTip.Location = new System.Drawing.Point(20, 20);
            this.lblTip.AutoSize = true;

            // 
            // cmbTip
            // 
            this.cmbTip.Location = new System.Drawing.Point(150, 17);
            this.cmbTip.Width = 200;
            this.cmbTip.DropDownStyle = ComboBoxStyle.DropDownList;

            // 
            // lblNaziv
            // 
            this.lblNaziv.Text = "Naziv:";
            this.lblNaziv.Location = new System.Drawing.Point(20, 60);
            this.lblNaziv.AutoSize = true;

            // 
            // txtNaziv
            // 
            this.txtNaziv.Location = new System.Drawing.Point(150, 57);
            this.txtNaziv.Width = 200;

            // 
            // lblDatum
            // 
            this.lblDatum.Text = "Datum:";
            this.lblDatum.Location = new System.Drawing.Point(20, 100);
            this.lblDatum.AutoSize = true;

            // 
            // dateDatum
            // 
            this.dateDatum.Location = new System.Drawing.Point(150, 97);
            this.dateDatum.Width = 200;

            // 
            // lblStarosna
            // 
            this.lblStarosna.Text = "Starosna grupa:";
            this.lblStarosna.Location = new System.Drawing.Point(20, 140);
            this.lblStarosna.AutoSize = true;

            // 
            // numStarosnaOd
            // 
            this.numStarosnaOd.Location = new System.Drawing.Point(150, 137);
            this.numStarosnaOd.Minimum = 1;
            this.numStarosnaOd.Maximum = 99;
            this.numStarosnaOd.Value = 7;
            this.numStarosnaOd.Width = 60;
            this.numStarosnaOd.ReadOnly = false;
            this.numStarosnaOd.Enabled = true;
            this.numStarosnaOd.TabStop = true;

            // 
            // lblDo
            // 
            this.lblDo.Text = "do";
            this.lblDo.Location = new System.Drawing.Point(220, 140);
            this.lblDo.AutoSize = true;

            // 
            // numStarosnaDo
            // 
            this.numStarosnaDo.Location = new System.Drawing.Point(250, 137);
            this.numStarosnaDo.Minimum = 1;
            this.numStarosnaDo.Maximum = 99;
            this.numStarosnaDo.Value = 15;
            this.numStarosnaDo.Width = 60;
            this.numStarosnaDo.ReadOnly = false;
            this.numStarosnaDo.Enabled = true;
            this.numStarosnaDo.TabStop = true;

            // 
            // lblMaxUcesnika
            // 
            this.lblMaxUcesnika.Text = "Maks. učesnika:";
            this.lblMaxUcesnika.Location = new System.Drawing.Point(20, 180);
            this.lblMaxUcesnika.AutoSize = true;

            // 
            // numMaxUcesnika
            // 
            this.numMaxUcesnika.Location = new System.Drawing.Point(150, 177);
            this.numMaxUcesnika.Minimum = 1;
            this.numMaxUcesnika.Maximum = 999;
            this.numMaxUcesnika.Value = 10;
            this.numMaxUcesnika.Width = 60;

            // 
            // lblOgranicenja
            // 
            this.lblOgranicenja.Text = "Ograničenja:";
            this.lblOgranicenja.Location = new System.Drawing.Point(20, 220);
            this.lblOgranicenja.AutoSize = true;

            // 
            // txtOgranicenja
            // 
            this.txtOgranicenja.Location = new System.Drawing.Point(150, 217);
            this.txtOgranicenja.Width = 200;

            // 
            // lblSport
            // 
            this.lblSport.Text = "Sport:";
            this.lblSport.Location = new System.Drawing.Point(20, 260);
            this.lblSport.AutoSize = true;

            // 
            // txtSport
            // 
            this.txtSport.Location = new System.Drawing.Point(150, 257);
            this.txtSport.Width = 200;

            // 
            // lblPosebnaOprema
            // 
            this.lblPosebnaOprema.Text = "Posebna oprema:";
            this.lblPosebnaOprema.Location = new System.Drawing.Point(20, 300);
            this.lblPosebnaOprema.AutoSize = true;

            // 
            // txtPosebnaOprema
            // 
            this.txtPosebnaOprema.Location = new System.Drawing.Point(150, 297);
            this.txtPosebnaOprema.Width = 200;

            // lblPotrebnaOprema
            this.lblPotrebnaOprema = new Label();
            this.lblPotrebnaOprema.Text = "Potrebna oprema:";
            this.lblPotrebnaOprema.Location = new System.Drawing.Point(20, 300);
            this.lblPotrebnaOprema.AutoSize = true;

            // txtPotrebnaOprema
            this.txtPotrebnaOprema = new TextBox();
            this.txtPotrebnaOprema.Location = new System.Drawing.Point(150, 297);
            this.txtPotrebnaOprema.Width = 200;

            // 
            // lblPrevoz
            // 
            this.lblPrevoz.Text = "Prevozno sredstvo:";
            this.lblPrevoz.Location = new System.Drawing.Point(20, 340);
            this.lblPrevoz.AutoSize = true;

            // 
            // txtPrevoz
            // 
            this.txtPrevoz.Location = new System.Drawing.Point(150, 337);
            this.txtPrevoz.Width = 200;

            // 
            // lblVodic
            // 
            this.lblVodic.Text = "Vodič:";
            this.lblVodic.Location = new System.Drawing.Point(20, 380);
            this.lblVodic.AutoSize = true;

            // 
            // txtVodic
            // 
            this.txtVodic.Location = new System.Drawing.Point(150, 377);
            this.txtVodic.Width = 200;

            // 
            // lblPlanPuta
            // 
            this.lblPlanPuta.Text = "Plan puta:";
            this.lblPlanPuta.Location = new System.Drawing.Point(20, 420);
            this.lblPlanPuta.AutoSize = true;

            // 
            // txtPlanPuta
            // 
            this.txtPlanPuta.Location = new System.Drawing.Point(150, 417);
            this.txtPlanPuta.Width = 200;

            // 
            // btnSacuvaj
            // 
            this.btnSacuvaj.Text = "Sačuvaj";
            this.btnSacuvaj.Location = new System.Drawing.Point(150, 460);
            this.btnSacuvaj.Width = 100;

            // 
            // AktivnostDodajIzmeni (Form)
            // 
            this.ClientSize = new System.Drawing.Size(400, 510);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.cmbTip);
            this.Controls.Add(this.lblNaziv);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.lblDatum);
            this.Controls.Add(this.dateDatum);
            this.Controls.Add(this.lblStarosna);
            this.Controls.Add(this.numStarosnaOd);
            this.Controls.Add(this.lblDo);
            this.Controls.Add(this.numStarosnaDo);
            this.Controls.Add(this.lblMaxUcesnika);
            this.Controls.Add(this.numMaxUcesnika);
            this.Controls.Add(this.lblOgranicenja);
            this.Controls.Add(this.txtOgranicenja);
            this.Controls.Add(this.lblSport);
            this.Controls.Add(this.txtSport);
            this.Controls.Add(this.lblPosebnaOprema);
            this.Controls.Add(this.txtPosebnaOprema);
            this.Controls.Add(this.lblPotrebnaOprema);
            this.Controls.Add(this.txtPotrebnaOprema);
            this.Controls.Add(this.lblPrevoz);
            this.Controls.Add(this.txtPrevoz);
            this.Controls.Add(this.lblVodic);
            this.Controls.Add(this.txtVodic);
            this.Controls.Add(this.lblPlanPuta);
            this.Controls.Add(this.txtPlanPuta);
            this.Controls.Add(this.btnSacuvaj);

            this.Text = "Dodavanje / Izmena aktivnosti";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            ((System.ComponentModel.ISupportInitialize)(this.numStarosnaOd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStarosnaDo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxUcesnika)).EndInit();
        }

        #endregion

        private ComboBox cmbTip;
        private Label lblTip;
        private Label lblNaziv;
        private TextBox txtNaziv;
        private Label lblDatum;
        private DateTimePicker dateDatum;
        private Label lblStarosna;
        private NumericUpDown numStarosnaOd;
        private Label lblDo;
        private NumericUpDown numStarosnaDo;
        private Label lblMaxUcesnika;
        private NumericUpDown numMaxUcesnika;
        private Label lblOgranicenja;
        private TextBox txtOgranicenja;
        private Label lblSport;
        private TextBox txtSport;
        private Label lblPosebnaOprema;
        private TextBox txtPosebnaOprema;
        private Label lblPotrebnaOprema;
        private TextBox txtPotrebnaOprema;
        private Label lblPrevoz;
        private TextBox txtPrevoz;
        private Label lblVodic;
        private TextBox txtVodic;
        private Label lblPlanPuta;
        private TextBox txtPlanPuta;
        private Button btnSacuvaj;
    }
}
