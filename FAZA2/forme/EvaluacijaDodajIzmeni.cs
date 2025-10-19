using System;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class EvaluacijaDodajIzmeni : Form
    {
        private int? evaluacijaId;

        public EvaluacijaDodajIzmeni(int? id = null)
        {
            evaluacijaId = id;
            InitializeComponent();
            this.Load += EvaluacijaDodajIzmeni_Load;
            btnSacuvaj.Click += BtnSacuvaj_Click;
        }

        private async void EvaluacijaDodajIzmeni_Load(object sender, EventArgs e)
        {
            try
            {
                // Učitavanje listi za combo boxeve
                var aktivnosti = await DTOManager.GetAllAktivnostiAsync();
                var lica = await DTOManager.GetAllAngazovanaLicaAsync();

                comboAktivnost.DataSource = aktivnosti;
                comboAktivnost.DisplayMember = "Naziv";
                comboAktivnost.ValueMember = "Id";

                comboAngazovanoLice.DataSource = lica;
                comboAngazovanoLice.DisplayMember = "Ime"; // ili "ImePrezime" ako imaš spojeno
                comboAngazovanoLice.ValueMember = "JMBG";

                // Ako je edit režim – učitaj postojeće podatke
                if (evaluacijaId != null)
                {
                    var evaluacija = await DTOManager.GetEvaluacijaAsync(evaluacijaId.Value);
                    if (evaluacija == null)
                    {
                        MessageBox.Show("Evaluacija nije pronađena.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }

                    numOcena.Value = evaluacija.Ocena;
                    dateDatum.Value = evaluacija.Datum;
                    txtOpis.Text = evaluacija.Opis;

                    if (evaluacija.Aktivnost != null)
                        comboAktivnost.SelectedValue = evaluacija.Aktivnost.Id;

                    if (evaluacija.AngazovanoLice != null)
                        comboAngazovanoLice.SelectedValue = evaluacija.AngazovanoLice.JMBG;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju podataka: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            if (comboAktivnost.SelectedItem == null || comboAngazovanoLice.SelectedItem == null)
            {
                MessageBox.Show("Morate izabrati aktivnost i angažovano lice.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var novaEvaluacija = new EvaluacijaBasic
            {
                Id = evaluacijaId ?? 0,
                Ocena = (int)numOcena.Value,
                Datum = dateDatum.Value,
                Opis = txtOpis.Text,
                Aktivnost = new AktivnostBasic
                {
                    Id = (int)comboAktivnost.SelectedValue,
                    Naziv = comboAktivnost.Text
                },
                AngazovanoLice = new AngazovanoLiceBasic
                {
                    JMBG = comboAngazovanoLice.SelectedValue.ToString(),
                    Ime = comboAngazovanoLice.Text
                }
            };

            try
            {
                if (evaluacijaId == null)
                {
                    await DTOManager.AddEvaluacijaAsync(novaEvaluacija);
                    MessageBox.Show("Evaluacija uspešno dodata!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await DTOManager.UpdateEvaluacijaAsync(novaEvaluacija);
                    MessageBox.Show("Evaluacija uspešno ažurirana!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom čuvanja: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
