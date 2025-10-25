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
            this.lblTip = new System.Windows.Forms.Label();
            this.rbDorucak = new System.Windows.Forms.RadioButton();
            this.rbRucak = new System.Windows.Forms.RadioButton();
            this.rbVecera = new System.Windows.Forms.RadioButton();
            this.lblUzrast = new System.Windows.Forms.Label();
            this.numUzrastOd = new System.Windows.Forms.NumericUpDown();
            this.lblDo = new System.Windows.Forms.Label();
            this.numUzrastDo = new System.Windows.Forms.NumericUpDown();
            this.lblJelovnik = new System.Windows.Forms.Label();
            this.txtJelovnik = new System.Windows.Forms.TextBox();
            this.lblPosebneOpcije = new System.Windows.Forms.Label();
            this.cbBezglutenski = new System.Windows.Forms.CheckBox();
            this.cbVegetarijanski = new System.Windows.Forms.CheckBox();
            this.cbBezLaktoze = new System.Windows.Forms.CheckBox();
            this.lblLokacija = new System.Windows.Forms.Label();
            this.cmbLokacija = new System.Windows.Forms.ComboBox();
            this.lblAktivnost = new System.Windows.Forms.Label();
            this.cmbAktivnost = new System.Windows.Forms.ComboBox();
            this.cbPosno = new System.Windows.Forms.CheckBox();
            this.btnSacuvaj = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numUzrastOd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUzrastDo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Location = new System.Drawing.Point(30, 30);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(76, 16);
            this.lblTip.TabIndex = 0;
            this.lblTip.Text = "Tip obroka:";
            // 
            // rbDorucak
            // 
            this.rbDorucak.AutoSize = true;
            this.rbDorucak.Location = new System.Drawing.Point(150, 25);
            this.rbDorucak.Name = "rbDorucak";
            this.rbDorucak.Size = new System.Drawing.Size(79, 20);
            this.rbDorucak.TabIndex = 1;
            this.rbDorucak.Text = "Doručak";
            // 
            // rbRucak
            // 
            this.rbRucak.AutoSize = true;
            this.rbRucak.Location = new System.Drawing.Point(260, 25);
            this.rbRucak.Name = "rbRucak";
            this.rbRucak.Size = new System.Drawing.Size(67, 20);
            this.rbRucak.TabIndex = 2;
            this.rbRucak.Text = "Ručak";
            // 
            // rbVecera
            // 
            this.rbVecera.AutoSize = true;
            this.rbVecera.Location = new System.Drawing.Point(370, 25);
            this.rbVecera.Name = "rbVecera";
            this.rbVecera.Size = new System.Drawing.Size(72, 20);
            this.rbVecera.TabIndex = 3;
            this.rbVecera.Text = "Večera";
            // 
            // lblUzrast
            // 
            this.lblUzrast.AutoSize = true;
            this.lblUzrast.Location = new System.Drawing.Point(30, 80);
            this.lblUzrast.Name = "lblUzrast";
            this.lblUzrast.Size = new System.Drawing.Size(101, 16);
            this.lblUzrast.TabIndex = 4;
            this.lblUzrast.Text = "Uzrast (godine):";
            // 
            // numUzrastOd
            // 
            this.numUzrastOd.Location = new System.Drawing.Point(150, 77);
            this.numUzrastOd.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numUzrastOd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUzrastOd.Name = "numUzrastOd";
            this.numUzrastOd.Size = new System.Drawing.Size(60, 22);
            this.numUzrastOd.TabIndex = 5;
            this.numUzrastOd.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // lblDo
            // 
            this.lblDo.AutoSize = true;
            this.lblDo.Location = new System.Drawing.Point(220, 80);
            this.lblDo.Name = "lblDo";
            this.lblDo.Size = new System.Drawing.Size(23, 16);
            this.lblDo.TabIndex = 6;
            this.lblDo.Text = "do";
            // 
            // numUzrastDo
            // 
            this.numUzrastDo.Location = new System.Drawing.Point(250, 77);
            this.numUzrastDo.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numUzrastDo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUzrastDo.Name = "numUzrastDo";
            this.numUzrastDo.Size = new System.Drawing.Size(60, 22);
            this.numUzrastDo.TabIndex = 7;
            this.numUzrastDo.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // lblJelovnik
            // 
            this.lblJelovnik.AutoSize = true;
            this.lblJelovnik.Location = new System.Drawing.Point(30, 130);
            this.lblJelovnik.Name = "lblJelovnik";
            this.lblJelovnik.Size = new System.Drawing.Size(60, 16);
            this.lblJelovnik.TabIndex = 8;
            this.lblJelovnik.Text = "Jelovnik:";
            // 
            // txtJelovnik
            // 
            this.txtJelovnik.Location = new System.Drawing.Point(150, 127);
            this.txtJelovnik.Name = "txtJelovnik";
            this.txtJelovnik.Size = new System.Drawing.Size(300, 22);
            this.txtJelovnik.TabIndex = 9;
            // 
            // lblPosebneOpcije
            // 
            this.lblPosebneOpcije.AutoSize = true;
            this.lblPosebneOpcije.Location = new System.Drawing.Point(30, 180);
            this.lblPosebneOpcije.Name = "lblPosebneOpcije";
            this.lblPosebneOpcije.Size = new System.Drawing.Size(105, 16);
            this.lblPosebneOpcije.TabIndex = 10;
            this.lblPosebneOpcije.Text = "Posebne opcije:";
            // 
            // cbBezglutenski
            // 
            this.cbBezglutenski.AutoSize = true;
            this.cbBezglutenski.Location = new System.Drawing.Point(150, 177);
            this.cbBezglutenski.Name = "cbBezglutenski";
            this.cbBezglutenski.Size = new System.Drawing.Size(105, 20);
            this.cbBezglutenski.TabIndex = 11;
            this.cbBezglutenski.Text = "Bezglutenski";
            // 
            // cbVegetarijanski
            // 
            this.cbVegetarijanski.AutoSize = true;
            this.cbVegetarijanski.Location = new System.Drawing.Point(150, 207);
            this.cbVegetarijanski.Name = "cbVegetarijanski";
            this.cbVegetarijanski.Size = new System.Drawing.Size(115, 20);
            this.cbVegetarijanski.TabIndex = 12;
            this.cbVegetarijanski.Text = "Vegetarijanski";
            // 
            // cbBezLaktoze
            // 
            this.cbBezLaktoze.AutoSize = true;
            this.cbBezLaktoze.Location = new System.Drawing.Point(150, 237);
            this.cbBezLaktoze.Name = "cbBezLaktoze";
            this.cbBezLaktoze.Size = new System.Drawing.Size(98, 20);
            this.cbBezLaktoze.TabIndex = 13;
            this.cbBezLaktoze.Text = "Bez laktoze";
            // 
            // lblLokacija
            // 
            this.lblLokacija.AutoSize = true;
            this.lblLokacija.Location = new System.Drawing.Point(30, 300);
            this.lblLokacija.Name = "lblLokacija";
            this.lblLokacija.Size = new System.Drawing.Size(61, 16);
            this.lblLokacija.TabIndex = 15;
            this.lblLokacija.Text = "Lokacija:";
            // 
            // cmbLokacija
            // 
            this.cmbLokacija.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLokacija.Location = new System.Drawing.Point(150, 297);
            this.cmbLokacija.Name = "cmbLokacija";
            this.cmbLokacija.Size = new System.Drawing.Size(300, 24);
            this.cmbLokacija.TabIndex = 16;
            // 
            // lblAktivnost
            // 
            this.lblAktivnost.AutoSize = true;
            this.lblAktivnost.Location = new System.Drawing.Point(30, 340);
            this.lblAktivnost.Name = "lblAktivnost";
            this.lblAktivnost.Size = new System.Drawing.Size(64, 16);
            this.lblAktivnost.TabIndex = 17;
            this.lblAktivnost.Text = "Aktivnost:";
            // 
            // cmbAktivnost
            // 
            this.cmbAktivnost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAktivnost.Location = new System.Drawing.Point(150, 337);
            this.cmbAktivnost.Name = "cmbAktivnost";
            this.cmbAktivnost.Size = new System.Drawing.Size(300, 24);
            this.cmbAktivnost.TabIndex = 18;
            // 
            // cbPosno
            // 
            this.cbPosno.AutoSize = true;
            this.cbPosno.Location = new System.Drawing.Point(150, 267);
            this.cbPosno.Name = "cbPosno";
            this.cbPosno.Size = new System.Drawing.Size(68, 20);
            this.cbPosno.TabIndex = 14;
            this.cbPosno.Text = "Posno";
            // 
            // btnSacuvaj
            // 
            this.btnSacuvaj.Location = new System.Drawing.Point(187, 418);
            this.btnSacuvaj.Name = "btnSacuvaj";
            this.btnSacuvaj.Size = new System.Drawing.Size(140, 40);
            this.btnSacuvaj.TabIndex = 19;
            this.btnSacuvaj.Text = "Sačuvaj";
            // 
            // ObrokDodajIzmeni
            // 
            this.ClientSize = new System.Drawing.Size(520, 496);
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
            this.Controls.Add(this.lblLokacija);
            this.Controls.Add(this.cmbLokacija);
            this.Controls.Add(this.lblAktivnost);
            this.Controls.Add(this.cmbAktivnost);
            this.Controls.Add(this.btnSacuvaj);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ObrokDodajIzmeni";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dodavanje / Izmena obroka";
            ((System.ComponentModel.ISupportInitialize)(this.numUzrastOd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUzrastDo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private Label lblLokacija;
        private ComboBox cmbLokacija;
        private Label lblAktivnost;
        private ComboBox cmbAktivnost;
        private Button btnSacuvaj;
    }
}
