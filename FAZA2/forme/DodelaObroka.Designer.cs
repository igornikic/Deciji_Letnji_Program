using System.Windows.Forms;

namespace Deciji_Letnji_Program.Forme
{
    partial class DodelaObroka
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox cmbTip;
        private Label lblTip;
        private Label lblStarosna;
        private NumericUpDown numStarosnaOd;
        private Label lblDo;
        private NumericUpDown numStarosnaDo;
        private Label lblSpecialneOpcije;
        private ComboBox cmbSpecialneOpcije;
        private Label lblJelovnik;
        private ComboBox cmbJelovnik;
        private Button btnSacuvaj;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbTip = new ComboBox();
            this.lblTip = new Label();
            this.lblSpecialneOpcije = new Label();
            this.cmbSpecialneOpcije = new ComboBox();
            this.lblJelovnik = new Label();
            this.cmbJelovnik = new ComboBox();
            this.btnSacuvaj = new Button();

            // 
            // lblTip
            // 
            this.lblTip.Text = "Tip obroka:";
            this.lblTip.Location = new System.Drawing.Point(20, 20);
            this.lblTip.AutoSize = true;

            // 
            // cmbTip
            // 
            this.cmbTip.Location = new System.Drawing.Point(150, 17);
            this.cmbTip.Width = 200;
            this.cmbTip.DropDownStyle = ComboBoxStyle.DropDownList;

            // 
            // lblSpecialneOpcije
            // 
            this.lblSpecialneOpcije.Text = "Posebne opcije:";
            this.lblSpecialneOpcije.Location = new System.Drawing.Point(20, 60);
            this.lblSpecialneOpcije.AutoSize = true;

            // 
            // cmbSpecialneOpcije
            // 
            this.cmbSpecialneOpcije.Location = new System.Drawing.Point(150, 57);
            this.cmbSpecialneOpcije.Width = 200;
            this.cmbSpecialneOpcije.DropDownStyle = ComboBoxStyle.DropDownList;

            // 
            // lblJelovnik
            // 
            this.lblJelovnik.Text = "Jelovnik:";
            this.lblJelovnik.Location = new System.Drawing.Point(20, 100);
            this.lblJelovnik.AutoSize = true;

            // 
            // cmbJelovnik
            // 
            this.cmbJelovnik.Location = new System.Drawing.Point(150, 97);
            this.cmbJelovnik.Width = 200;
            this.cmbJelovnik.DropDownStyle = ComboBoxStyle.DropDownList;

            // 
            // btnSacuvaj
            // 
            this.btnSacuvaj.Text = "Sačuvaj";
            this.btnSacuvaj.Location = new System.Drawing.Point(150, 140);
            this.btnSacuvaj.Width = 100;

            // 
            // DodelaObroka
            // 
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.cmbTip);
            this.Controls.Add(this.lblSpecialneOpcije);
            this.Controls.Add(this.cmbSpecialneOpcije);
            this.Controls.Add(this.lblJelovnik);
            this.Controls.Add(this.cmbJelovnik);
            this.Controls.Add(this.btnSacuvaj);
            this.Text = "Dodela Obroka Detetu";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

    }
}
