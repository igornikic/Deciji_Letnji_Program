namespace Deciji_Letnji_Program
{
    partial class Pocetna
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dete = new System.Windows.Forms.Button();
            this.roditelj = new System.Windows.Forms.Button();
            this.prijava = new System.Windows.Forms.Button();
            this.aktivnost = new System.Windows.Forms.Button();
            this.angazovanoLice = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dete
            // 
            this.dete.Location = new System.Drawing.Point(310, 12);
            this.dete.Name = "dete";
            this.dete.Size = new System.Drawing.Size(180, 30);  // visina smanjena sa 40 na 30
            this.dete.TabIndex = 0;
            this.dete.Text = "Dete";
            this.dete.UseVisualStyleBackColor = true;
            this.dete.Click += new System.EventHandler(this.dete_Click);
            // 
            // roditelj
            // 
            this.roditelj.Location = new System.Drawing.Point(310, 52); // pomereno 10px gore da ne bude preveliki razmak
            this.roditelj.Name = "roditelj";
            this.roditelj.Size = new System.Drawing.Size(180, 30);
            this.roditelj.TabIndex = 1;
            this.roditelj.Text = "Roditelj";
            this.roditelj.UseVisualStyleBackColor = true;
            this.roditelj.Click += new System.EventHandler(this.roditelj_Click);
            // 
            // prijava
            // 
            this.prijava.Location = new System.Drawing.Point(310, 92);
            this.prijava.Name = "prijava";
            this.prijava.Size = new System.Drawing.Size(180, 30);
            this.prijava.TabIndex = 2;
            this.prijava.Text = "Prijava";
            this.prijava.UseVisualStyleBackColor = true;
            this.prijava.Click += new System.EventHandler(this.prijava_Click);
            // 
            // aktivnost
            // 
            this.aktivnost.Location = new System.Drawing.Point(310, 132);
            this.aktivnost.Name = "aktivnost";
            this.aktivnost.Size = new System.Drawing.Size(180, 30);
            this.aktivnost.TabIndex = 3;
            this.aktivnost.Text = "Aktivnost";
            this.aktivnost.UseVisualStyleBackColor = true;
            this.aktivnost.Click += new System.EventHandler(this.aktivnost_Click);
            // 
            // angazovanoLice
            // 
            this.angazovanoLice.Location = new System.Drawing.Point(310, 172);
            this.angazovanoLice.Name = "angazovanoLice";
            this.angazovanoLice.Size = new System.Drawing.Size(180, 30);
            this.angazovanoLice.TabIndex = 4;
            this.angazovanoLice.Text = "Angažovano lice";
            this.angazovanoLice.UseVisualStyleBackColor = true;
            this.angazovanoLice.Click += new System.EventHandler(this.angazovanoLice_Click);
            // 
            // lokacija
            // 
            this.lokacija = new System.Windows.Forms.Button();
            this.lokacija.Location = new System.Drawing.Point(310, 212);
            this.lokacija.Name = "lokacija";
            this.lokacija.Size = new System.Drawing.Size(180, 30);
            this.lokacija.TabIndex = 5;
            this.lokacija.Text = "Lokacija";
            this.lokacija.UseVisualStyleBackColor = true;
            this.lokacija.Click += new System.EventHandler(this.lokacija_Click);
            // 
            // Pocetna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 400); // malo smanjen da bude proporcionalno
            this.Controls.Add(this.lokacija);
            this.Controls.Add(this.angazovanoLice);
            this.Controls.Add(this.aktivnost);
            this.Controls.Add(this.prijava);
            this.Controls.Add(this.roditelj);
            this.Controls.Add(this.dete);
            this.Name = "Pocetna";
            this.Text = "Početna";
            this.ResumeLayout(false);
        }



        #endregion

        private System.Windows.Forms.Button dete;
        private System.Windows.Forms.Button roditelj;
        private System.Windows.Forms.Button prijava;
        private System.Windows.Forms.Button aktivnost;
        private System.Windows.Forms.Button angazovanoLice;
        private System.Windows.Forms.Button lokacija;

    }
}