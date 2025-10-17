namespace Deciji_Letnji_Program.Forme
{
    partial class AktivnostPregled
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
            this.dataGridViewAktivnosti = new System.Windows.Forms.DataGridView();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnIzmeni = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.btnZaposleni = new System.Windows.Forms.Button();
            this.btnSpisakDece = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAktivnosti)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewAktivnosti
            // 
            this.dataGridViewAktivnosti.AllowUserToAddRows = false;
            this.dataGridViewAktivnosti.AllowUserToDeleteRows = false;
            this.dataGridViewAktivnosti.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewAktivnosti.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAktivnosti.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewAktivnosti.MultiSelect = false;
            this.dataGridViewAktivnosti.Name = "dataGridViewAktivnosti";
            this.dataGridViewAktivnosti.ReadOnly = true;
            this.dataGridViewAktivnosti.RowHeadersVisible = false;
            this.dataGridViewAktivnosti.RowTemplate.Height = 24;
            this.dataGridViewAktivnosti.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAktivnosti.Size = new System.Drawing.Size(620, 330);
            this.dataGridViewAktivnosti.TabIndex = 0;
            // 
            // btnDodaj
            // 
            this.btnDodaj.Location = new System.Drawing.Point(12, 355);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(100, 35);
            this.btnDodaj.TabIndex = 1;
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // btnIzmeni
            // 
            this.btnIzmeni.Location = new System.Drawing.Point(130, 355);
            this.btnIzmeni.Name = "btnIzmeni";
            this.btnIzmeni.Size = new System.Drawing.Size(100, 35);
            this.btnIzmeni.TabIndex = 2;
            this.btnIzmeni.Text = "Izmeni";
            this.btnIzmeni.UseVisualStyleBackColor = true;
            this.btnIzmeni.Click += new System.EventHandler(this.btnIzmeni_Click);
            // 
            // btnObrisi
            // 
            this.btnObrisi.Location = new System.Drawing.Point(250, 355);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(100, 35);
            this.btnObrisi.TabIndex = 3;
            this.btnObrisi.Text = "Obriši";
            this.btnObrisi.UseVisualStyleBackColor = true;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // btnZaposleni
            // 
            this.btnZaposleni.Location = new System.Drawing.Point(370, 355);
            this.btnZaposleni.Name = "btnZaposleni";
            this.btnZaposleni.Size = new System.Drawing.Size(120, 35);
            this.btnZaposleni.TabIndex = 4;
            this.btnZaposleni.Text = "Zaposleni";
            this.btnZaposleni.UseVisualStyleBackColor = true;
            this.btnZaposleni.Click += new System.EventHandler(this.btnZaposleni_Click);
            // 
            // btnSpisakDece
            // 
            this.btnSpisakDece.Location = new System.Drawing.Point(510, 355);
            this.btnSpisakDece.Name = "btnSpisakDece";
            this.btnSpisakDece.Size = new System.Drawing.Size(120, 35);
            this.btnSpisakDece.TabIndex = 5;
            this.btnSpisakDece.Text = "Spisak dece";
            this.btnSpisakDece.UseVisualStyleBackColor = true;
            this.btnSpisakDece.Click += new System.EventHandler(this.btnSpisakDece_Click);
            // 
            // AktivnostPregled
            // 
            this.ClientSize = new System.Drawing.Size(644, 402);
            this.Controls.Add(this.btnSpisakDece);
            this.Controls.Add(this.btnZaposleni);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.btnIzmeni);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.dataGridViewAktivnosti);
            this.Name = "AktivnostPregled";
            this.Text = "Pregled aktivnosti";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAktivnosti)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewAktivnosti;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Button btnIzmeni;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.Button btnZaposleni;
        private System.Windows.Forms.Button btnSpisakDece;
    }
}
