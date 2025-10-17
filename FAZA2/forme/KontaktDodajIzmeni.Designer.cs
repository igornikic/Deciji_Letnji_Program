namespace Deciji_Letnji_Program.Forme
{
    partial class KontaktDodajIzmeni
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblNaslov = new System.Windows.Forms.Label();
            this.txtKontakt = new System.Windows.Forms.TextBox();
            this.btnSacuvaj = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNaslov
            // 
            this.lblNaslov.AutoSize = true;
            this.lblNaslov.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNaslov.Location = new System.Drawing.Point(25, 30);
            this.lblNaslov.Name = "lblNaslov";
            this.lblNaslov.Size = new System.Drawing.Size(120, 23);
            this.lblNaslov.TabIndex = 0;
            this.lblNaslov.Text = "Kontakt label";
            // 
            // txtKontakt
            // 
            this.txtKontakt.Location = new System.Drawing.Point(25, 65);
            this.txtKontakt.Name = "txtKontakt";
            this.txtKontakt.Size = new System.Drawing.Size(300, 22);
            this.txtKontakt.TabIndex = 1;
            // 
            // btnSacuvaj
            // 
            this.btnSacuvaj.Location = new System.Drawing.Point(225, 110);
            this.btnSacuvaj.Name = "btnSacuvaj";
            this.btnSacuvaj.Size = new System.Drawing.Size(100, 35);
            this.btnSacuvaj.TabIndex = 2;
            this.btnSacuvaj.Text = "Sačuvaj";
            this.btnSacuvaj.UseVisualStyleBackColor = true;
            // 
            // KontaktDodajIzmeni
            // 
            this.ClientSize = new System.Drawing.Size(360, 170);
            this.Controls.Add(this.btnSacuvaj);
            this.Controls.Add(this.txtKontakt);
            this.Controls.Add(this.lblNaslov);
            this.Name = "KontaktDodajIzmeni";
            this.Text = "Kontakt roditelja";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNaslov;
        private System.Windows.Forms.TextBox txtKontakt;
        private System.Windows.Forms.Button btnSacuvaj;
    }
}
