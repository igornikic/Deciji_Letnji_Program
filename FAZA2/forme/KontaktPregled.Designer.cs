namespace Deciji_Letnji_Program.Forme
{
    partial class KontaktPregled
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
            this.dataGridViewKontakti = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKontakti)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewKontakti
            // 
            this.dataGridViewKontakti.AllowUserToAddRows = false;
            this.dataGridViewKontakti.AllowUserToDeleteRows = false;
            this.dataGridViewKontakti.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewKontakti.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewKontakti.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewKontakti.MultiSelect = false;
            this.dataGridViewKontakti.Name = "dataGridViewKontakti";
            this.dataGridViewKontakti.ReadOnly = true;
            this.dataGridViewKontakti.RowHeadersVisible = false;
            this.dataGridViewKontakti.RowTemplate.Height = 24;
            this.dataGridViewKontakti.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewKontakti.Size = new System.Drawing.Size(460, 300);
            this.dataGridViewKontakti.TabIndex = 0;
            // 
            // KontaktPregled
            // 
            this.ClientSize = new System.Drawing.Size(484, 331);
            this.Controls.Add(this.dataGridViewKontakti);
            this.Name = "KontaktPregled";
            this.Text = "Kontakti roditelja";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKontakti)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewKontakti;
    }
}
