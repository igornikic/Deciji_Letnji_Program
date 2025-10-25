namespace Deciji_Letnji_Program.Forme
{
    partial class ZaposleniZaAktivnost
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewZaposleni;
        private System.Windows.Forms.ComboBox comboBoxLica;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Button btnUkloni;
        private System.Windows.Forms.Label lblZaposleni;
        private System.Windows.Forms.Label lblSlobodnaLica;
        private System.Windows.Forms.Button btnPrijaviPovredu;

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
            this.dataGridViewZaposleni = new System.Windows.Forms.DataGridView();
            this.comboBoxLica = new System.Windows.Forms.ComboBox();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnUkloni = new System.Windows.Forms.Button();
            this.lblZaposleni = new System.Windows.Forms.Label();
            this.lblSlobodnaLica = new System.Windows.Forms.Label();
            this.btnPrijaviPovredu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewZaposleni)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewZaposleni
            // 
            this.dataGridViewZaposleni.AllowUserToAddRows = false;
            this.dataGridViewZaposleni.AllowUserToDeleteRows = false;
            this.dataGridViewZaposleni.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewZaposleni.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewZaposleni.Location = new System.Drawing.Point(20, 50);
            this.dataGridViewZaposleni.MultiSelect = false;
            this.dataGridViewZaposleni.Name = "dataGridViewZaposleni";
            this.dataGridViewZaposleni.ReadOnly = true;
            this.dataGridViewZaposleni.RowHeadersWidth = 51;
            this.dataGridViewZaposleni.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewZaposleni.Size = new System.Drawing.Size(681, 200);
            this.dataGridViewZaposleni.TabIndex = 0;
            // 
            // comboBoxLica
            // 
            this.comboBoxLica.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLica.FormattingEnabled = true;
            this.comboBoxLica.Location = new System.Drawing.Point(20, 290);
            this.comboBoxLica.Name = "comboBoxLica";
            this.comboBoxLica.Size = new System.Drawing.Size(200, 24);
            this.comboBoxLica.TabIndex = 1;
            // 
            // btnDodaj
            // 
            this.btnDodaj.Location = new System.Drawing.Point(240, 284);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(100, 35);
            this.btnDodaj.TabIndex = 2;
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.UseVisualStyleBackColor = true;
            // 
            // btnUkloni
            // 
            this.btnUkloni.Location = new System.Drawing.Point(360, 285);
            this.btnUkloni.Name = "btnUkloni";
            this.btnUkloni.Size = new System.Drawing.Size(100, 35);
            this.btnUkloni.TabIndex = 3;
            this.btnUkloni.Text = "Ukloni";
            this.btnUkloni.UseVisualStyleBackColor = true;
            // 
            // lblZaposleni
            // 
            this.lblZaposleni.AutoSize = true;
            this.lblZaposleni.Location = new System.Drawing.Point(17, 20);
            this.lblZaposleni.Name = "lblZaposleni";
            this.lblZaposleni.Size = new System.Drawing.Size(187, 16);
            this.lblZaposleni.TabIndex = 4;
            this.lblZaposleni.Text = "Angažovana lica na aktivnosti:";
            // 
            // lblSlobodnaLica
            // 
            this.lblSlobodnaLica.AutoSize = true;
            this.lblSlobodnaLica.Location = new System.Drawing.Point(17, 265);
            this.lblSlobodnaLica.Name = "lblSlobodnaLica";
            this.lblSlobodnaLica.Size = new System.Drawing.Size(171, 16);
            this.lblSlobodnaLica.TabIndex = 5;
            this.lblSlobodnaLica.Text = "Slobodna angažovana lica:";
            // 
            // btnPrijaviPovredu
            // 
            this.btnPrijaviPovredu.Location = new System.Drawing.Point(481, 285);
            this.btnPrijaviPovredu.Name = "btnPrijaviPovredu";
            this.btnPrijaviPovredu.Size = new System.Drawing.Size(220, 35);
            this.btnPrijaviPovredu.TabIndex = 6;
            this.btnPrijaviPovredu.Text = "Prijavi povredu";
            this.btnPrijaviPovredu.UseVisualStyleBackColor = true;
            this.btnPrijaviPovredu.Click += new System.EventHandler(this.BtnPrijaviPovredu_Click);
            // 
            // ZaposleniZaAktivnost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 392);
            this.Controls.Add(this.lblSlobodnaLica);
            this.Controls.Add(this.lblZaposleni);
            this.Controls.Add(this.btnUkloni);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.comboBoxLica);
            this.Controls.Add(this.dataGridViewZaposleni);
            this.Controls.Add(this.btnPrijaviPovredu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ZaposleniZaAktivnost";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zaposleni na aktivnosti";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewZaposleni)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
