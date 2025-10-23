using System.Windows.Forms;

namespace Deciji_Letnji_Program.Forme
{
    partial class DodajKomentarOcenu
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox cmbAktivnosti;
        private TextBox txtKomentar;
        private NumericUpDown nudOcena;
        private Button btnDodaj;
        private Label lblAktivnost;
        private Label lblKomentar;
        private Label lblOcena;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbAktivnosti = new ComboBox();
            this.txtKomentar = new TextBox();
            this.nudOcena = new NumericUpDown();
            this.btnDodaj = new Button();
            this.lblAktivnost = new Label();
            this.lblKomentar = new Label();
            this.lblOcena = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudOcena)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAktivnost
            // 
            this.lblAktivnost.AutoSize = true;
            this.lblAktivnost.Location = new System.Drawing.Point(20, 20);
            this.lblAktivnost.Name = "lblAktivnost";
            this.lblAktivnost.Size = new System.Drawing.Size(65, 13);
            this.lblAktivnost.TabIndex = 0;
            this.lblAktivnost.Text = "Aktivnost:";
            // 
            // cmbAktivnosti
            // 
            this.cmbAktivnosti.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbAktivnosti.FormattingEnabled = true;
            this.cmbAktivnosti.Location = new System.Drawing.Point(100, 17);
            this.cmbAktivnosti.Name = "cmbAktivnosti";
            this.cmbAktivnosti.Size = new System.Drawing.Size(200, 21);
            this.cmbAktivnosti.TabIndex = 1;
            // 
            // lblKomentar
            // 
            this.lblKomentar.AutoSize = true;
            this.lblKomentar.Location = new System.Drawing.Point(20, 60);
            this.lblKomentar.Name = "lblKomentar";
            this.lblKomentar.Size = new System.Drawing.Size(61, 13);
            this.lblKomentar.TabIndex = 2;
            this.lblKomentar.Text = "Komentar:";
            // 
            // txtKomentar
            // 
            this.txtKomentar.Location = new System.Drawing.Point(100, 57);
            this.txtKomentar.Multiline = true;
            this.txtKomentar.Name = "txtKomentar";
            this.txtKomentar.Size = new System.Drawing.Size(200, 60);
            this.txtKomentar.TabIndex = 3;
            // 
            // lblOcena
            // 
            this.lblOcena.AutoSize = true;
            this.lblOcena.Location = new System.Drawing.Point(20, 130);
            this.lblOcena.Name = "lblOcena";
            this.lblOcena.Size = new System.Drawing.Size(43, 13);
            this.lblOcena.TabIndex = 4;
            this.lblOcena.Text = "Ocena:";
            // 
            // nudOcena
            // 
            this.nudOcena.Location = new System.Drawing.Point(100, 128);
            this.nudOcena.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudOcena.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.nudOcena.Name = "nudOcena";
            this.nudOcena.Size = new System.Drawing.Size(60, 20);
            this.nudOcena.TabIndex = 5;
            this.nudOcena.Value = nudOcena.Minimum;
            // 
            // btnDodaj
            // 
            this.btnDodaj.Location = new System.Drawing.Point(100, 170);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(100, 30);
            this.btnDodaj.TabIndex = 6;
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // DodajKomentarOcenu
            // 
            this.ClientSize = new System.Drawing.Size(340, 220);
            this.Controls.Add(this.lblAktivnost);
            this.Controls.Add(this.cmbAktivnosti);
            this.Controls.Add(this.lblKomentar);
            this.Controls.Add(this.txtKomentar);
            this.Controls.Add(this.lblOcena);
            this.Controls.Add(this.nudOcena);
            this.Controls.Add(this.btnDodaj);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DodajKomentarOcenu";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Dodaj komentar i ocenu";
            ((System.ComponentModel.ISupportInitialize)(this.nudOcena)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
