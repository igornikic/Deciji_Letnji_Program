namespace Deciji_Letnji_Program.Forme
{
    partial class EvaluacijaPregled
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
            this.dataGridViewEvaluacije = new System.Windows.Forms.DataGridView();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnIzmeni = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEvaluacije)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewEvaluacije
            // 
            this.dataGridViewEvaluacije.AllowUserToAddRows = false;
            this.dataGridViewEvaluacije.AllowUserToDeleteRows = false;
            this.dataGridViewEvaluacije.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEvaluacije.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEvaluacije.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewEvaluacije.MultiSelect = false;
            this.dataGridViewEvaluacije.Name = "dataGridViewEvaluacije";
            this.dataGridViewEvaluacije.ReadOnly = true;
            this.dataGridViewEvaluacije.RowHeadersVisible = false;
            this.dataGridViewEvaluacije.RowTemplate.Height = 24;
            this.dataGridViewEvaluacije.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEvaluacije.Size = new System.Drawing.Size(640, 320);
            this.dataGridViewEvaluacije.TabIndex = 0;
            // 
            // btnDodaj
            // 
            this.btnDodaj.Location = new System.Drawing.Point(12, 350);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(100, 35);
            this.btnDodaj.TabIndex = 1;
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.UseVisualStyleBackColor = true;
            // 
            // btnIzmeni
            // 
            this.btnIzmeni.Location = new System.Drawing.Point(130, 350);
            this.btnIzmeni.Name = "btnIzmeni";
            this.btnIzmeni.Size = new System.Drawing.Size(100, 35);
            this.btnIzmeni.TabIndex = 2;
            this.btnIzmeni.Text = "Izmeni";
            this.btnIzmeni.UseVisualStyleBackColor = true;
            // 
            // btnObrisi
            // 
            this.btnObrisi.Location = new System.Drawing.Point(250, 350);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(100, 35);
            this.btnObrisi.TabIndex = 3;
            this.btnObrisi.Text = "Obriši";
            this.btnObrisi.UseVisualStyleBackColor = true;
            // 
            // EvaluacijaPregled
            // 
            this.ClientSize = new System.Drawing.Size(664, 400);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.btnIzmeni);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.dataGridViewEvaluacije);
            this.Name = "EvaluacijaPregled";
            this.Text = "Pregled evaluacija";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEvaluacije)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEvaluacije;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Button btnIzmeni;
        private System.Windows.Forms.Button btnObrisi;
    }
}
