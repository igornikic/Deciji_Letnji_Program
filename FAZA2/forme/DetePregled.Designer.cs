namespace Deciji_Letnji_Program.Forme
{
    partial class DetePregled
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
            this.dataGridViewDeca = new System.Windows.Forms.DataGridView();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnIzmeni = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.btnKontakti = new System.Windows.Forms.Button();
            this.btnAktivnosti = new System.Windows.Forms.Button();
            this.btnKomentar = new System.Windows.Forms.Button();
            this.btnPovrede = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDeca)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewDeca
            // 
            this.dataGridViewDeca.AllowUserToAddRows = false;
            this.dataGridViewDeca.AllowUserToDeleteRows = false;
            this.dataGridViewDeca.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDeca.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDeca.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewDeca.MultiSelect = false;
            this.dataGridViewDeca.Name = "dataGridViewDeca";
            this.dataGridViewDeca.ReadOnly = true;
            this.dataGridViewDeca.RowHeadersVisible = false;
            this.dataGridViewDeca.RowHeadersWidth = 51;
            this.dataGridViewDeca.RowTemplate.Height = 24;
            this.dataGridViewDeca.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDeca.Size = new System.Drawing.Size(828, 330);
            this.dataGridViewDeca.TabIndex = 0;
            // 
            // btnDodaj
            // 
            this.btnDodaj.Location = new System.Drawing.Point(12, 355);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(100, 35);
            this.btnDodaj.TabIndex = 1;
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.UseVisualStyleBackColor = true;
            // 
            // btnIzmeni
            // 
            this.btnIzmeni.Location = new System.Drawing.Point(130, 355);
            this.btnIzmeni.Name = "btnIzmeni";
            this.btnIzmeni.Size = new System.Drawing.Size(100, 35);
            this.btnIzmeni.TabIndex = 2;
            this.btnIzmeni.Text = "Izmeni";
            this.btnIzmeni.UseVisualStyleBackColor = true;
            this.btnIzmeni.Click += new System.EventHandler(this.btnIzmeni_Click_1);
            // 
            // btnObrisi
            // 
            this.btnObrisi.Location = new System.Drawing.Point(250, 355);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(100, 35);
            this.btnObrisi.TabIndex = 3;
            this.btnObrisi.Text = "Obriši";
            this.btnObrisi.UseVisualStyleBackColor = true;
            // 
            // btnKontakti
            // 
            this.btnKontakti.Location = new System.Drawing.Point(370, 355);
            this.btnKontakti.Name = "btnKontakti";
            this.btnKontakti.Size = new System.Drawing.Size(100, 35);
            this.btnKontakti.TabIndex = 4;
            this.btnKontakti.Text = "Kontakti";
            this.btnKontakti.UseVisualStyleBackColor = true;
            // 
            // btnAktivnosti
            // 
            this.btnAktivnosti.Location = new System.Drawing.Point(490, 355);
            this.btnAktivnosti.Name = "btnAktivnosti";
            this.btnAktivnosti.Size = new System.Drawing.Size(100, 35);
            this.btnAktivnosti.TabIndex = 5;
            this.btnAktivnosti.Text = "Aktivnosti";
            this.btnAktivnosti.UseVisualStyleBackColor = true;
            // 
            // btnKomentar
            // 
            this.btnKomentar.Location = new System.Drawing.Point(610, 355);
            this.btnKomentar.Name = "btnKomentar";
            this.btnKomentar.Size = new System.Drawing.Size(120, 35);
            this.btnKomentar.TabIndex = 6;
            this.btnKomentar.Text = "Ostavi komentar";
            this.btnKomentar.UseVisualStyleBackColor = true;
            // 
            // btnPovrede
            // 
            this.btnPovrede.Location = new System.Drawing.Point(740, 355);
            this.btnPovrede.Name = "btnPovrede";
            this.btnPovrede.Size = new System.Drawing.Size(100, 35);
            this.btnPovrede.TabIndex = 7;
            this.btnPovrede.Text = "Povrede";
            this.btnPovrede.UseVisualStyleBackColor = true;
            // 
            // DetePregled
            // 
            this.ClientSize = new System.Drawing.Size(863, 402);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.btnIzmeni);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.dataGridViewDeca);
            this.Controls.Add(this.btnKontakti);
            this.Controls.Add(this.btnAktivnosti);
            this.Controls.Add(this.btnKomentar);
            this.Controls.Add(this.btnPovrede);
            this.Name = "DetePregled";
            this.Text = "Pregled dece";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDeca)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewDeca;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Button btnIzmeni;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.Button btnKontakti;
        private System.Windows.Forms.Button btnAktivnosti;
        private System.Windows.Forms.Button btnKomentar;
        private System.Windows.Forms.Button btnPovrede;
    }
}
