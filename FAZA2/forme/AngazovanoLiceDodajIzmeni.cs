using System;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Deciji_Letnji_Program.Forme
{
    public partial class AngazovanoLiceDodajIzmeni : Form
    {
        private string JMBG;

        public AngazovanoLiceDodajIzmeni(string jmbg = null)
        {
            JMBG = jmbg;
            InitializeComponent();

            this.Load += FormAngazovanoLice_Load;
            btnSacuvaj.Click += BtnSacuvaj_Click;
        }

        private async void FormAngazovanoLice_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(JMBG))
            {
                try
                {
                    var lice = await DTOManager.GetAngazovanoLiceAsync(JMBG);
                    if (lice == null)
                    {
                        MessageBox.Show("Lice nije pronađeno.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }

                    txtJMBG.Text = lice.JMBG;
                    txtJMBG.Enabled = false;
                    txtIme.Text = lice.Ime;
                    txtPrezime.Text = lice.Prezime;
                    rbMusko.Checked = lice.Pol == 'M';
                    rbZensko.Checked = lice.Pol == 'Z';
                    txtAdresa.Text = lice.Adresa;
                    txtTelefon.Text = lice.BrojTelefona;
                    txtEmail.Text = lice.Email;
                    txtStrucnaSprema.Text = lice.StrucnaSprema;
                    cbVolonter.Checked = lice.Volonter == 'Y';
                    cbTrener.Checked = lice.Trener == 'Y';
                    cbAnimator.Checked = lice.Animator == 'Y';
                    cbZdravstveniRadnik.Checked = lice.ZdravstveniRadnik == 'Y';
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtJMBG.Text) || string.IsNullOrWhiteSpace(txtIme.Text) || string.IsNullOrWhiteSpace(txtPrezime.Text))
            {
                MessageBox.Show("JMBG, Ime i Prezime su obavezna polja.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtJMBG.Text.Length != 13)
            {
                MessageBox.Show("JMBG mora imati tačno 13 karaktera.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var lice = new AngazovanoLiceBasic
            {
                JMBG = txtJMBG.Text,
                Ime = txtIme.Text,
                Prezime = txtPrezime.Text,
                Pol = rbMusko.Checked ? 'M' : 'Z',
                Adresa = txtAdresa.Text,
                BrojTelefona = txtTelefon.Text,
                Email = txtEmail.Text,
                StrucnaSprema = txtStrucnaSprema.Text,
                Volonter = cbVolonter.Checked ? 'Y' : 'N',
                Trener = cbTrener.Checked ? 'Y' : 'N',
                Animator = cbAnimator.Checked ? 'Y' : 'N',
                ZdravstveniRadnik = cbZdravstveniRadnik.Checked ? 'Y' : 'N'
            };

            try
            {
                if (!string.IsNullOrEmpty(JMBG))
                {
                    await DTOManager.UpdateAngazovanoLiceAsync(lice);
                    MessageBox.Show("Angažovano lice uspešno ažurirano!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await DTOManager.AddAngazovanoLiceAsync(lice);
                    MessageBox.Show("Angažovano lice uspešno dodato!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
