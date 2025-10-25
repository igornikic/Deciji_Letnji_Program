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
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnIzmeni = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.btnSpisakDece = new System.Windows.Forms.Button();
            this.btnZaposleni = new System.Windows.Forms.Button();
            this.btnPrikaziObroke = new System.Windows.Forms.Button();
            this.btnPrikaziPrisustvo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAktivnosti)).BeginInit();
            this.flowLayoutPanelButtons.SuspendLayout();
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
            this.dataGridViewAktivnosti.RowHeadersWidth = 51;
            this.dataGridViewAktivnosti.RowTemplate.Height = 24;
            this.dataGridViewAktivnosti.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAktivnosti.Size = new System.Drawing.Size(817, 330);
            this.dataGridViewAktivnosti.TabIndex = 0;
            // 
            // flowLayoutPanelButtons
            // 
            this.flowLayoutPanelButtons.Controls.Add(this.btnDodaj);
            this.flowLayoutPanelButtons.Controls.Add(this.btnIzmeni);
            this.flowLayoutPanelButtons.Controls.Add(this.btnObrisi);
            this.flowLayoutPanelButtons.Controls.Add(this.btnSpisakDece);
            this.flowLayoutPanelButtons.Controls.Add(this.btnZaposleni);
            this.flowLayoutPanelButtons.Controls.Add(this.btnPrikaziObroke);
            this.flowLayoutPanelButtons.Controls.Add(this.btnPrikaziPrisustvo);
            this.flowLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(0, 355);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.flowLayoutPanelButtons.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(842, 53);
            this.flowLayoutPanelButtons.TabIndex = 1;
            // 
            // btnDodaj
            // 
            this.btnDodaj.Location = new System.Drawing.Point(13, 13);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(100, 35);
            this.btnDodaj.TabIndex = 1;
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // btnIzmeni
            // 
            this.btnIzmeni.Location = new System.Drawing.Point(119, 13);
            this.btnIzmeni.Name = "btnIzmeni";
            this.btnIzmeni.Size = new System.Drawing.Size(100, 35);
            this.btnIzmeni.TabIndex = 2;
            this.btnIzmeni.Text = "Izmeni";
            this.btnIzmeni.UseVisualStyleBackColor = true;
            this.btnIzmeni.Click += new System.EventHandler(this.btnIzmeni_Click);
            // 
            // btnObrisi
            // 
            this.btnObrisi.Location = new System.Drawing.Point(225, 13);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(100, 35);
            this.btnObrisi.TabIndex = 3;
            this.btnObrisi.Text = "Obriši";
            this.btnObrisi.UseVisualStyleBackColor = true;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // btnSpisakDece
            // 
            this.btnSpisakDece.Location = new System.Drawing.Point(331, 13);
            this.btnSpisakDece.Name = "btnSpisakDece";
            this.btnSpisakDece.Size = new System.Drawing.Size(120, 35);
            this.btnSpisakDece.TabIndex = 5;
            this.btnSpisakDece.Text = "Spisak dece";
            this.btnSpisakDece.UseVisualStyleBackColor = true;
            this.btnSpisakDece.Click += new System.EventHandler(this.btnSpisakDece_Click);
            // 
            // btnZaposleni
            // 
            this.btnZaposleni.Location = new System.Drawing.Point(457, 13);
            this.btnZaposleni.Name = "btnZaposleni";
            this.btnZaposleni.Size = new System.Drawing.Size(120, 35);
            this.btnZaposleni.TabIndex = 4;
            this.btnZaposleni.Text = "Zaposleni";
            this.btnZaposleni.UseVisualStyleBackColor = true;
            this.btnZaposleni.Click += new System.EventHandler(this.btnZaposleni_Click);
            // 
            // btnPrikaziObroke
            // 
            this.btnPrikaziObroke.Location = new System.Drawing.Point(583, 13);
            this.btnPrikaziObroke.Name = "btnPrikaziObroke";
            this.btnPrikaziObroke.Size = new System.Drawing.Size(120, 35);
            this.btnPrikaziObroke.TabIndex = 6;
            this.btnPrikaziObroke.Text = "Prikazi Obroke";
            this.btnPrikaziObroke.UseVisualStyleBackColor = true;
            this.btnPrikaziObroke.Click += new System.EventHandler(this.btnPrikaziObroke_Click);
            // 
            // btnPrikaziPrisustvo
            // 
            this.btnPrikaziPrisustvo.Location = new System.Drawing.Point(709, 13);
            this.btnPrikaziPrisustvo.Name = "btnPrikaziPrisustvo";
            this.btnPrikaziPrisustvo.Size = new System.Drawing.Size(120, 35);
            this.btnPrikaziPrisustvo.TabIndex = 7;
            this.btnPrikaziPrisustvo.Text = "Prikazi Prisustvo";
            this.btnPrikaziPrisustvo.UseVisualStyleBackColor = true;
            this.btnPrikaziPrisustvo.Click += new System.EventHandler(this.btnPrikaziPrisustvo_Click);
            // 
            // AktivnostPregled
            // 
            this.ClientSize = new System.Drawing.Size(842, 408);
            this.Controls.Add(this.flowLayoutPanelButtons);
            this.Controls.Add(this.dataGridViewAktivnosti);
            this.Name = "AktivnostPregled";
            this.Text = "Pregled aktivnosti";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAktivnosti)).EndInit();
            this.flowLayoutPanelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewAktivnosti;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Button btnIzmeni;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.Button btnZaposleni;
        private System.Windows.Forms.Button btnSpisakDece;
        private System.Windows.Forms.Button btnPrikaziObroke;
        private System.Windows.Forms.Button btnPrikaziPrisustvo;
    }
}
