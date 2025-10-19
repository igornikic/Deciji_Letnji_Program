using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class EvaluacijaDodajIzmeni : Form
    {
        private int? evaluacijaID;
        private bool _loading = false;

        public EvaluacijaDodajIzmeni(int? id = null)
        {
            evaluacijaID = id;
            InitializeComponent();

            Load += EvaluacijaDodajIzmeni_Load;
            btnSacuvaj.Click += BtnSacuvaj_Click;
        }

        private async void EvaluacijaDodajIzmeni_Load(object sender, EventArgs e)
        {
            try
            {
                _loading = true;

                // Učitaj sve aktivnosti
                var aktivnosti = await DTOManager.GetAllAktivnostiAsync();
                cmbAktivnosti.DisplayMember = "Naziv";
                cmbAktivnosti.ValueMember = "Id";
                cmbAktivnosti.DataSource = aktivnosti;

                cmbAngazovanaLica.Enabled = false;
                cmbAktivnosti.SelectedIndexChanged += CmbAktivnosti_SelectedIndexChanged;

                if (evaluacijaID.HasValue)
                {
                    // Učitavanje postojeće evaluacije
                    var eval = await DTOManager.GetEvaluacijaAsync(evaluacijaID.Value);
                    if (eval == null)
                    {
                        MessageBox.Show("Evaluacija nije pronađena.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                        return;
                    }

                    cmbAktivnosti.SelectedValue = eval.Aktivnost.Id;
                    await UcitajAngazovanaLicaAsync(eval.Aktivnost.Id);
                    cmbAngazovanaLica.SelectedValue = eval.AngazovanoLice.JMBG;

                    numOcena.Value = eval.Ocena;
                    dateDatum.Value = eval.Datum;
                    txtOpis.Text = eval.Opis;
                }
                else
                {
                    // Nova evaluacija
                    cmbAktivnosti.SelectedIndex = -1;
                    cmbAngazovanaLica.DataSource = null;
                    numOcena.Value = 1;
                    dateDatum.Value = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom učitavanja: " + ex.Message);
            }
            finally
            {
                _loading = false;
            }
        }

        private async void CmbAktivnosti_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading) return;

            if (!(cmbAktivnosti.SelectedValue is int aktivnostId))
            {
                cmbAngazovanaLica.Enabled = false;
                cmbAngazovanaLica.DataSource = null;
                return;
            }

            await UcitajAngazovanaLicaAsync(aktivnostId);
            cmbAngazovanaLica.Enabled = true;
        }

        private async Task UcitajAngazovanaLicaAsync(int aktivnostId)
        {
            // Vraća sva lica angažovana na aktivnosti
            var lica = await DTOManager.GetAngazovanaLicaZaAktivnostAsync(aktivnostId);

            cmbAngazovanaLica.DataSource = lica
                .Select(l => new
                {
                    JMBG = l.JMBG,
                    PunoIme = $"{l.Ime} {l.Prezime}"
                })
                .ToList();

            cmbAngazovanaLica.DisplayMember = "PunoIme";
            cmbAngazovanaLica.ValueMember = "JMBG";
            cmbAngazovanaLica.SelectedIndex = -1;
            cmbAngazovanaLica.Enabled = lica.Count > 0;
        }




        private async void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(cmbAktivnosti.SelectedValue is int aktivnostId))
                {
                    MessageBox.Show("Morate izabrati aktivnost.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!(cmbAngazovanaLica.SelectedValue is string jmbg))
                {
                    MessageBox.Show("Morate izabrati angažovano lice.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int ocena = (int)numOcena.Value;
                DateTime datum = dateDatum.Value;
                string opis = txtOpis.Text;

                var evaluacija = new EvaluacijaBasic
                {
                    Ocena = ocena,
                    Datum = datum,
                    Opis = opis,
                    Aktivnost = new AktivnostBasic { Id = aktivnostId },
                    AngazovanoLice = new AngazovanoLiceBasic { JMBG = jmbg }
                };

                await DTOManager.AddEvaluacijaAsync(evaluacija);
                MessageBox.Show("Evaluacija uspešno dodata.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom čuvanja: " + ex.Message);
            }
        }

    }
}
