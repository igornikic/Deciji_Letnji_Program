namespace Deciji_Letnji_Program.Forme
{
    partial class DecaNaAktivnosti
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewDeca;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewDeca = new System.Windows.Forms.DataGridView();
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
            this.dataGridViewDeca.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDeca.Size = new System.Drawing.Size(560, 300);
            this.dataGridViewDeca.TabIndex = 0;

            // 
            // DecaNaAktivnosti
            // 
            this.ClientSize = new System.Drawing.Size(584, 331);
            this.Controls.Add(this.dataGridViewDeca);
            this.Name = "DecaNaAktivnosti";
            this.Text = "Deca na aktivnosti";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDeca)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
