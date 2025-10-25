using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deciji_Letnji_Program.Forme
{
    public partial class ZaposleniZaAktivnost : Form
    {
        private int AktivnostID;

        public ZaposleniZaAktivnost(int aktivnostId)
        {
            InitializeComponent();
            AktivnostID = aktivnostId;

            this.Load += async (s, e) =>
            {
                await UcitajAngazovanaLicaAsync();
                await UcitajNeangazovanaLicaAsync();
            };

            btnDodaj.Click += BtnDodaj_Click;
            btnUkloni.Click += BtnUkloni_Click;
        }

        private async Task UcitajAngazovanaLicaAsync()
        {
            try
            {
                var lica = await DTOManager.GetAngazovanaLicaZaAktivnostAsync(AktivnostID);
                dataGridViewZaposleni.DataSource = lica;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom učitavanja angažovanih lica: " + ex.Message);
            }
        }

        private async Task UcitajNeangazovanaLicaAsync()
        {
            try
            {
                var lica = await DTOManager.GetSlobodnaAngazovanaLicaAsync(AktivnostID);
                comboBoxLica.DataSource = lica;
                comboBoxLica.DisplayMember = "Ime";
                comboBoxLica.ValueMember = "JMBG";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom učitavanja slobodnih lica: " + ex.Message);
            }
        }

        private async void BtnDodaj_Click(object sender, EventArgs e)
        {
            if (comboBoxLica.SelectedItem is DTOs.AngazovanoLicePregled lice)
            {
                try
                {
                    await DTOManager.AddAngazovanoLiceNaAktivnostAsync(lice.JMBG, AktivnostID);
                    MessageBox.Show($"Lice je dodato na aktivnost.");

                    await UcitajAngazovanaLicaAsync();
                    await UcitajNeangazovanaLicaAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška prilikom dodavanja lica: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Izaberite lice koje želite da dodate.");
            }
        }

        private async void BtnUkloni_Click(object sender, EventArgs e)
        {
            if (dataGridViewZaposleni.CurrentRow == null)
            {
                MessageBox.Show("Izaberite lice koje želite da uklonite.");
                return;
            }

            var lice = dataGridViewZaposleni.CurrentRow.DataBoundItem as DTOs.AngazovanoLicePregled;
            if (lice == null) return;

            var potvrda = MessageBox.Show($"Da li ste sigurni da želite da uklonite lice sa aktivnosti?",
                                          "Potvrda", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (potvrda != DialogResult.Yes) return;

            try
            {
                await DTOManager.UkloniAngazovanoLiceSaAktivnostiAsync(lice.JMBG, AktivnostID);
                MessageBox.Show("Lice je uspešno uklonjeno sa aktivnosti.");

                await UcitajAngazovanaLicaAsync();
                await UcitajNeangazovanaLicaAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom uklanjanja lica: " + ex.Message);
            }
        }

        private void BtnPrijaviPovredu_Click(object sender, EventArgs e)
        {
            if (dataGridViewZaposleni.CurrentRow == null)
            {
                MessageBox.Show("Izaberite lice koje prijavljuje povredu.");
                return;
            }

            var lice = dataGridViewZaposleni.CurrentRow.DataBoundItem as DTOs.AngazovanoLicePregled;
            if (lice == null) return;

            var povredaForm = new PovredaDodajIzmeni(AktivnostID, lice.JMBG);
            povredaForm.ShowDialog();
        }


    }
}
