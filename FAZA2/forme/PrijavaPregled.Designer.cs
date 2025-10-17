namespace Deciji_Letnji_Program.Forme
{
    partial class PrijavaPregled
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
            this.dataGridViewPrijave = new System.Windows.Forms.DataGridView();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnIzmeni = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrijave)).BeginInit();
            this.SuspendLayout();

            // dataGridViewPrijave
            this.dataGridViewPrijave.AllowUserToAddRows = false;
            this.dataGridViewPrijave.AllowUserToDeleteRows = false;
            this.dataGridViewPrijave.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPrijave.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPrijave.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewPrijave.MultiSelect = false;
            this.dataGridViewPrijave.Name = "dataGridViewPrijave";
            this.dataGridViewPrijave.ReadOnly = true;
            this.dataGridViewPrijave.RowHeadersVisible = false;
            this.dataGridViewPrijave.RowTemplate.Height = 24;
            this.dataGridViewPrijave.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPrijave.Size = new System.Drawing.Size(620, 330);

            // btnDodaj
            this.btnDodaj.Location = new System.Drawing.Point(12, 355);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(100, 35);
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.UseVisualStyleBackColor = true;

            // btnIzmeni
            this.btnIzmeni.Location = new System.Drawing.Point(130, 355);
            this.btnIzmeni.Name = "btnIzmeni";
            this.btnIzmeni.Size = new System.Drawing.Size(100, 35);
            this.btnIzmeni.Text = "Izmeni";
            this.btnIzmeni.UseVisualStyleBackColor = true;

            // btnObrisi
            this.btnObrisi.Location = new System.Drawing.Point(250, 355);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(100, 35);
            this.btnObrisi.Text = "Obriši";
            this.btnObrisi.UseVisualStyleBackColor = true;

            // PrijavaPregled
            this.ClientSize = new System.Drawing.Size(644, 402);
            this.Controls.Add(this.dataGridViewPrijave);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.btnIzmeni);
            this.Controls.Add(this.btnObrisi);
            this.Name = "PrijavaPregled";
            this.Text = "Pregled prijava";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrijave)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPrijave;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Button btnIzmeni;
        private System.Windows.Forms.Button btnObrisi;
    }
}
