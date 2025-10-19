using Deciji_Letnji_Program.Entiteti;
using System;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class DodelaObroka : Form
    {
        private int DeteteID;

        public DodelaObroka(int deteteID)
        {
            DeteteID = deteteID;
            InitializeComponent();
            this.Load += DodelaObroka_Load;
            btnSacuvaj.Click += BtnSacuvaj_Click;
        }

        private void DodelaObroka_Load(object sender, EventArgs e)
        {
            cmbTip.Items.AddRange(new string[] { "Doručak", "Ručak", "Večera" });
            cmbTip.SelectedIndex = 0;

            cmbSpecialneOpcije.Items.AddRange(new string[]
            {
        "Vegetarijanski", "Bez glutena", "Bez mlečnih proizvoda"
            });
            cmbSpecialneOpcije.SelectedIndex = -1;

            cmbJelovnik.Items.AddRange(new string[]
            {
        "Jelovnik 1", "Jelovnik 2", "Jelovnik 3"
            });
            cmbJelovnik.SelectedIndex = -1;

            // Event handleri za međusobnu kontrolu
            cmbSpecialneOpcije.SelectedIndexChanged += (s, ev) =>
            {
                if (cmbSpecialneOpcije.SelectedIndex != -1)
                {
                    cmbJelovnik.SelectedIndex = -1;
                    cmbJelovnik.Enabled = false;
                }
                else
                {
                    cmbJelovnik.Enabled = true;
                }
            };

            cmbJelovnik.SelectedIndexChanged += (s, ev) =>
            {
                if (cmbJelovnik.SelectedIndex != -1)
                {
                    cmbSpecialneOpcije.SelectedIndex = -1;
                    cmbSpecialneOpcije.Enabled = false;
                }
                else
                {
                    cmbSpecialneOpcije.Enabled = true;
                }
            };
        }


        private async void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            
        }

    }
}
