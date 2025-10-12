namespace Deciji_Letnji_Program
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.roditelj = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dete
            // 
            this.dete.Location = new System.Drawing.Point(361, 12);
            this.dete.Name = "dete";
            this.dete.Size = new System.Drawing.Size(75, 23);
            this.dete.TabIndex = 0;
            this.dete.Text = "Dete";
            this.dete.UseVisualStyleBackColor = true;
            this.dete.Click += new System.EventHandler(this.dete_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(361, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // roditelj
            // 
            this.roditelj.Location = new System.Drawing.Point(361, 63);
            this.roditelj.Name = "roditelj";
            this.roditelj.Size = new System.Drawing.Size(75, 23);
            this.roditelj.TabIndex = 2;
            this.roditelj.Text = "Roditelj";
            this.roditelj.UseVisualStyleBackColor = true;
            this.roditelj.Click += new System.EventHandler(this.roditelj_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.roditelj);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dete);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button dete;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button roditelj;
    }
}

