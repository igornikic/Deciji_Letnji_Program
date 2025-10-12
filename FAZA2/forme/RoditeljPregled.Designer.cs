namespace Deciji_Letnji_Program.Forme
{
    partial class RoditeljPregled
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
            this.dataGridViewRoditelji = new System.Windows.Forms.DataGridView();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnIzmeni = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRoditelji)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewRoditelji
            // 
            this.dataGridViewRoditelji.AllowUserToAddRows = false;
            this.dataGridViewRoditelji.AllowUserToDeleteRows = false;
            this.dataGridViewRoditelji.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewRoditelji.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRoditelji.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewRoditelji.MultiSelect = false;
            this.dataGridViewRoditelji.Name = "dataGridViewRoditelji";
            this.dataGridViewRoditelji.ReadOnly = true;
            this.dataGridViewRoditelji.RowHeadersVisible = false;
            this.dataGridViewRoditelji.RowTemplate.Height = 24;
            this.dataGridViewRoditelji.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRoditelji.Size = new System.Drawing.Size(620, 330);
            this.dataGridViewRoditelji.TabIndex = 0;
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
            // FormRoditeljPregled
            // 
            this.ClientSize = new System.Drawing.Size(644, 402);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.btnIzmeni);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.dataGridViewRoditelji);
            this.Name = "RoditeljPregled";
            this.Text = "Pregled roditelja";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRoditelji)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewRoditelji;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Button btnIzmeni;
        private System.Windows.Forms.Button btnObrisi;
    }
}
