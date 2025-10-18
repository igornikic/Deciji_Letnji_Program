namespace Deciji_Letnji_Program
{
    partial class FormTest
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
            this.cmdUcitajDete = new System.Windows.Forms.Button();
            this.cmdDodajDete = new System.Windows.Forms.Button();
            this.cmdVezaDeteRoditelj = new System.Windows.Forms.Button();
            this.cmdVezaDetePovrede = new System.Windows.Forms.Button();
            this.cmdVezaDeteAktivnosti = new System.Windows.Forms.Button();
            this.cmdVezaDetePrijave = new System.Windows.Forms.Button();
            this.cmdVezaPrijavaDete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdUcitajDete
            // 
            this.cmdUcitajDete.Location = new System.Drawing.Point(30, 30);
            this.cmdUcitajDete.Size = new System.Drawing.Size(250, 30);
            this.cmdUcitajDete.Text = "Učitavanje jednog deteta";
            this.cmdUcitajDete.Click += new System.EventHandler(this.cmdUcitajDete_Click);
            // 
            // cmdDodajDete
            // 
            this.cmdDodajDete.Location = new System.Drawing.Point(30, 70);
            this.cmdDodajDete.Size = new System.Drawing.Size(250, 30);
            this.cmdDodajDete.Text = "Dodavanje novog deteta";
            this.cmdDodajDete.Click += new System.EventHandler(this.cmdDodajDete_Click);
            // 
            // cmdVezaDeteRoditelj
            // 
            this.cmdVezaDeteRoditelj.Location = new System.Drawing.Point(30, 110);
            this.cmdVezaDeteRoditelj.Size = new System.Drawing.Size(250, 30);
            this.cmdVezaDeteRoditelj.Text = "Veza dete–roditelj (M:N)";
            this.cmdVezaDeteRoditelj.Click += new System.EventHandler(this.cmdVezaDeteRoditelj_Click);
            // 
            // cmdVezaDetePovrede
            // 
            this.cmdVezaDetePovrede.Location = new System.Drawing.Point(30, 150);
            this.cmdVezaDetePovrede.Size = new System.Drawing.Size(250, 30);
            this.cmdVezaDetePovrede.Text = "Veza dete–povrede (1:N)";
            this.cmdVezaDetePovrede.Click += new System.EventHandler(this.cmdVezaDetePovrede_Click);
            // 
            // cmdVezaDeteAktivnosti
            // 
            this.cmdVezaDeteAktivnosti.Location = new System.Drawing.Point(30, 190);
            this.cmdVezaDeteAktivnosti.Size = new System.Drawing.Size(250, 30);
            this.cmdVezaDeteAktivnosti.Text = "Veza dete–aktivnosti (M:N)";
            this.cmdVezaDeteAktivnosti.Click += new System.EventHandler(this.cmdVezaDeteAktivnosti_Click);
            // 
            // cmdVezaDetePrijave
            // 
            this.cmdVezaDetePrijave.Location = new System.Drawing.Point(30, 230);
            this.cmdVezaDetePrijave.Size = new System.Drawing.Size(250, 30);
            this.cmdVezaDetePrijave.Text = "Veza dete–prijava (1:N)";
            this.cmdVezaDetePrijave.Click += new System.EventHandler(this.cmdVezaDetePrijave_Click);
            // 
            // cmdVezaPrijavaDete
            // 
            this.cmdVezaPrijavaDete.Location = new System.Drawing.Point(30, 270);
            this.cmdVezaPrijavaDete.Size = new System.Drawing.Size(250, 30);
            this.cmdVezaPrijavaDete.Text = "Veza prijava–dete (N:1)";
            this.cmdVezaPrijavaDete.Click += new System.EventHandler(this.cmdVezaPrijavaDete_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(320, 340);
            this.Controls.Add(this.cmdVezaPrijavaDete);
            this.Controls.Add(this.cmdVezaDetePrijave);
            this.Controls.Add(this.cmdVezaDeteAktivnosti);
            this.Controls.Add(this.cmdVezaDetePovrede);
            this.Controls.Add(this.cmdVezaDeteRoditelj);
            this.Controls.Add(this.cmdDodajDete);
            this.Controls.Add(this.cmdUcitajDete);
            this.Name = "Form1";
            this.Text = "Deciji Letnji Program – NHibernate test";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button cmdUcitajDete;
        private System.Windows.Forms.Button cmdDodajDete;
        private System.Windows.Forms.Button cmdVezaDeteRoditelj;
        private System.Windows.Forms.Button cmdVezaDetePovrede;
        private System.Windows.Forms.Button cmdVezaDeteAktivnosti;
        private System.Windows.Forms.Button cmdVezaDetePrijave;
        private System.Windows.Forms.Button cmdVezaPrijavaDete;
    }
}
