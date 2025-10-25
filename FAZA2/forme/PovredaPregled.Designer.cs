namespace Deciji_Letnji_Program.Forme
{
    partial class PovredaPregled
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
            this.dataGridViewPovrede = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPovrede)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPovrede
            // 
            this.dataGridViewPovrede.AllowUserToAddRows = false;
            this.dataGridViewPovrede.AllowUserToDeleteRows = false;
            this.dataGridViewPovrede.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPovrede.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPovrede.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewPovrede.MultiSelect = false;
            this.dataGridViewPovrede.Name = "dataGridViewPovrede";
            this.dataGridViewPovrede.ReadOnly = true;
            this.dataGridViewPovrede.RowHeadersVisible = false;
            this.dataGridViewPovrede.RowTemplate.Height = 24;
            this.dataGridViewPovrede.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPovrede.Size = new System.Drawing.Size(760, 350);
            this.dataGridViewPovrede.TabIndex = 0;
            // 
            // PovredaPregled
            // 
            this.ClientSize = new System.Drawing.Size(784, 421);
            this.Controls.Add(this.dataGridViewPovrede);
            this.Name = "PovredaPregled";
            this.Text = "Pregled povreda deteta";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPovrede)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPovrede;
    }
}
