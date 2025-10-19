namespace Deciji_Letnji_Program.Forme
{
    partial class DodelaObroka
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
            this.comboBoxObrok = new System.Windows.Forms.ComboBox();
            this.btnDodeliObrok = new System.Windows.Forms.Button();
            this.dataGridViewObroci = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewObroci)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxObrok
            // 
            this.comboBoxObrok.FormattingEnabled = true;
            this.comboBoxObrok.Location = new System.Drawing.Point(12, 12);
            this.comboBoxObrok.Name = "comboBoxObrok";
            this.comboBoxObrok.Size = new System.Drawing.Size(400, 24);
            this.comboBoxObrok.TabIndex = 0;
            // 
            // btnDodeliObrok
            // 
            this.btnDodeliObrok.Location = new System.Drawing.Point(12, 50);
            this.btnDodeliObrok.Name = "btnDodeliObrok";
            this.btnDodeliObrok.Size = new System.Drawing.Size(200, 35);
            this.btnDodeliObrok.TabIndex = 1;
            this.btnDodeliObrok.Text = "Dodeli obrok";
            this.btnDodeliObrok.UseVisualStyleBackColor = true;
            this.btnDodeliObrok.Click += new System.EventHandler(this.btnDodeliObrok_Click);
            // 
            // dataGridViewObroci
            // 
            this.dataGridViewObroci.AllowUserToAddRows = false;
            this.dataGridViewObroci.AllowUserToDeleteRows = false;
            this.dataGridViewObroci.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewObroci.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewObroci.Location = new System.Drawing.Point(12, 100);
            this.dataGridViewObroci.MultiSelect = false;
            this.dataGridViewObroci.Name = "dataGridViewObroci";
            this.dataGridViewObroci.ReadOnly = true;
            this.dataGridViewObroci.RowHeadersVisible = false;
            this.dataGridViewObroci.RowTemplate.Height = 24;
            this.dataGridViewObroci.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewObroci.Size = new System.Drawing.Size(600, 280);
            this.dataGridViewObroci.TabIndex = 2;
            // 
            // DodelaObroka
            // 
            this.ClientSize = new System.Drawing.Size(624, 401);
            this.Controls.Add(this.dataGridViewObroci);
            this.Controls.Add(this.btnDodeliObrok);
            this.Controls.Add(this.comboBoxObrok);
            this.Name = "DodelaObroka";
            this.Text = "Dodela obroka";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewObroci)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxObrok;
        private System.Windows.Forms.Button btnDodeliObrok;
        private System.Windows.Forms.DataGridView dataGridViewObroci;
    }
}
