using System;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Deciji_Letnji_Program.Forme
{
    public partial class RoditeljDodajIzmeni : Form
    {
        private int? RoditeljID;

        public RoditeljDodajIzmeni(int? id = null)
        {
            RoditeljID = id;
            InitializeComponent();

            this.Load += FormRoditeljDetalji_Load;
            btnSacuvaj.Click += BtnSacuvaj_Click;
        }

        private async void FormRoditeljDetalji_Load(object sender, EventArgs e)
        {
            if (RoditeljID.HasValue)
            {
                try
                {
                    var roditelj = await DTOManager.GetRoditeljAsync(RoditeljID.Value);
                    txtIme.Text = roditelj.Ime;
                    txtPrezime.Text = roditelj.Prezime;
                    // Ako imate decu povezanu sa roditeljem, možete ih prikazati ili omogućiti odabir
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            var roditelj = new RoditeljBasic
            {
                Ime = txtIme.Text,
                Prezime = txtPrezime.Text,
                // Povežite decu sa roditeljima ako su odabrana
            };

            try
            {
                if (RoditeljID.HasValue)
                {
                    roditelj.Id = RoditeljID.Value;
                    await DTOManager.UpdateRoditeljAsync(roditelj);
                    MessageBox.Show("Roditelj uspešno ažuriran!", "Uspešno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await DTOManager.AddRoditeljAsync(roditelj);
                    MessageBox.Show("Roditelj uspešno dodat!", "Uspešno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
