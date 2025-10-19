using System.Windows.Forms;

namespace Deciji_Letnji_Program.Forme
{
    partial class ObrokDodajIzmeni
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
            this.lblTip = new Label();
            this.rbDorucak = new RadioButton();
            this.rbRucak = new RadioButton();
            this.rbVecera = new RadioButton();

            this.lblUzrast = new Label();
            this.numUzrastOd = new NumericUpDown();
            this.lblDo = new Label();
            this.numUzrastDo = new NumericUpDown();

            this.lblJelovnik = new Label();
            this.txtJelovnik = new TextBox();

            this.lblPosebneOpcije = new Label();
            this.cbBezglutenski = new CheckBox();
            this.cbVegetarijanski = new CheckBox();
            this.cbBezLaktoze = new CheckBox();
            this.cbPosno = new CheckBox();

            this.btnSacuvaj = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.numUzrastOd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUzrastDo)).BeginInit();

            // 
            // lblTip
            // 
            this.lblTip.Text = "Tip obroka:";
            this.lblTip.Location = new System.Drawing.Point(30, 30);
            this.lblTip.AutoSize = true;

            // 
            // rbDorucak
            // 
            this.rbDorucak.Text = "Doručak";
            this.rbDorucak.Location = new System.Drawing.Point(150, 25);
            this.rbDorucak.AutoSize = true;

            // 
            // rbRucak
            // 
            this.rbRucak.Text = "Ručak";
            this.rbRucak.Location = new System.Drawing.Point(260, 25);
            this.rbRucak.AutoSize = true;

            // 
            // rbVecera
            // 
            this.rbVecera.Text = "Večera";
            this.rbVecera.Location = new System.Drawing.Point(370, 25);
            this.rbVecera.AutoSize = true;

            // 
            // lblUzrast
            // 
            this.lblUzrast.Text = "Uzrast (godine):";
            this.lblUzrast.Location = new System.Drawing.Point(30, 80);
            this.lblUzrast.AutoSize = true;

            // 
            // numUzrastOd
            // 
            this.numUzrastOd.Location = new System.Drawing.Point(150, 77);
            this.numUzrastOd.Minimum = 1;
            this.numUzrastOd.Maximum = 99;
            this.numUzrastOd.Value = 7;
            this.numUzrastOd.Width = 60;

            // 
            // lblDo
            // 
            this.lblDo.Text = "do";
            this.lblDo.Location = new System.Drawing.Point(220, 80);
            this.lblDo.AutoSize = true;

            // 
            // numUzrastDo
            // 
            this.numUzrastDo.Location = new System.Drawing.Point(250, 77);
            this.numUzrastDo.Minimum = 1;
            this.numUzrastDo.Maximum = 99;
            this.numUzrastDo.Value = 15;
            this.numUzrastDo.Width = 60;

            // 
            // lblJelovnik
            // 
            this.lblJelovnik.Text = "Jelovnik:";
            this.lblJelovnik.Location = new System.Drawing.Point(30, 130);
            this.lblJelovnik.AutoSize = true;

            // 
            // txtJelovnik
            // 
            this.txtJelovnik.Location = new System.Drawing.Point(150, 127);
            this.txtJelovnik.Width = 300;
            this.txtJelovnik.Height = 25;

            // 
            // lblPosebneOpcije
            // 
            this.lblPosebneOpcije.Text = "Posebne opcije:";
            this.lblPosebneOpcije.Location = new System.Drawing.Point(30, 180);
            this.lblPosebneOpcije.AutoSize = true;

            // 
            // cbBezglutenski
            // 
            this.cbBezglutenski.Text = "Bezglutenski";
            this.cbBezglutenski.Location = new System.Drawing.Point(150, 177);
            this.cbBezglutenski.AutoSize = true;

            // 
            // cbVegetarijanski
            // 
            this.cbVegetarijanski.Text = "Vegetarijanski";
            this.cbVegetarijanski.Location = new System.Drawing.Point(150, 207);
            this.cbVegetarijanski.AutoSize = true;

            // 
            // cbBezLaktoze
            // 
            this.cbBezLaktoze.Text = "Bez laktoze";
            this.cbBezLaktoze.Location = new System.Drawing.Point(150, 237);
            this.cbBezLaktoze.AutoSize = true;

            // 
            // cbPosno
            // 
            this.cbPosno.Text = "Posno";
            this.cbPosno.Location = new System.Drawing.Point(150, 267);
            this.cbPosno.AutoSize = true;

            // 
            // btnSacuvaj
            // 
            this.btnSacuvaj.Text = "Sačuvaj";
            this.btnSacuvaj.Location = new System.Drawing.Point(180, 320);
            this.btnSacuvaj.Width = 140;
            this.btnSacuvaj.Height = 40;

            // 
            // ObrokDodajIzmeni
            // 
            this.ClientSize = new System.Drawing.Size(520, 400);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.rbDorucak);
            this.Controls.Add(this.rbRucak);
            this.Controls.Add(this.rbVecera);
            this.Controls.Add(this.lblUzrast);
            this.Controls.Add(this.numUzrastOd);
            this.Controls.Add(this.lblDo);
            this.Controls.Add(this.numUzrastDo);
            this.Controls.Add(this.lblJelovnik);
            this.Controls.Add(this.txtJelovnik);
            this.Controls.Add(this.lblPosebneOpcije);
            this.Controls.Add(this.cbBezglutenski);
            this.Controls.Add(this.cbVegetarijanski);
            this.Controls.Add(this.cbBezLaktoze);
            this.Controls.Add(this.cbPosno);
            this.Controls.Add(this.btnSacuvaj);

            this.Text = "Dodavanje / Izmena obroka";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            ((System.ComponentModel.ISupportInitialize)(this.numUzrastOd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUzrastDo)).EndInit();
        }

        #endregion

        private Label lblTip;
        private RadioButton rbDorucak;
        private RadioButton rbRucak;
        private RadioButton rbVecera;
        private Label lblUzrast;
        private NumericUpDown numUzrastOd;
        private Label lblDo;
        private NumericUpDown numUzrastDo;
        private Label lblJelovnik;
        private TextBox txtJelovnik;
        private Label lblPosebneOpcije;
        private CheckBox cbBezglutenski;
        private CheckBox cbVegetarijanski;
        private CheckBox cbBezLaktoze;
        private CheckBox cbPosno;
        private Button btnSacuvaj;
    }
}
