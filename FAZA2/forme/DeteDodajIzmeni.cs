using System;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Deciji_Letnji_Program.Forme
{
    public partial class DeteDodajIzmeni : Form
    {
        private int? DeteID;

        public DeteDodajIzmeni(int? id = null)
        {
            DeteID = id;
            InitializeComponent();

            this.Load += FormDeteDetalji_Load;
            btnSacuvaj.Click += BtnSacuvaj_Click;
        }

        private async void FormDeteDetalji_Load(object sender, EventArgs e)
        {
            if (DeteID.HasValue)
            {
                try
                {
                    var dete = await DTOManager.GetDeteAsync(DeteID.Value);
                    txtIme.Text = dete.Ime;
                    txtPrezime.Text = dete.Prezime;
                    dateTimePickerRodjenje.Value = dete.DatumRodjenja;
                    rbMusko.Checked = dete.Pol == 'M';
                    rbZensko.Checked = dete.Pol == 'Z';
                    txtAdresa.Text = dete.Adresa;
                    txtTelefon.Text = dete.TelefonDeteta;
                    txtEmail.Text = dete.EmailDeteta;
                    txtPosebnePotrebe.Text = dete.PosebnePotrebe;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIme.Text) || string.IsNullOrWhiteSpace(txtPrezime.Text))
            {
                MessageBox.Show("Ime i Prezime su obavezna polja.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var dete = new DeteBasic
            {
                Ime = txtIme.Text,
                Prezime = txtPrezime.Text,
                DatumRodjenja = dateTimePickerRodjenje.Value,
                Pol = rbMusko.Checked ? 'M' : 'Z',
                Adresa = txtAdresa.Text,
                TelefonDeteta = txtTelefon.Text,
                EmailDeteta = txtEmail.Text,
                PosebnePotrebe = txtPosebnePotrebe.Text
            };

            try
            {
                if (DeteID.HasValue)
                {
                    dete.Id = DeteID.Value;
                    await DTOManager.UpdateDeteAsync(dete);
                    MessageBox.Show("Dete uspešno ažurirano!", "Uspešno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await DTOManager.AddDeteAsync(dete);
                    MessageBox.Show("Dete uspešno dodato!", "Uspešno", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
