using System;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class AktivnostDodajIzmeni : Form
    {
        private int? AktivnostID;

        public AktivnostDodajIzmeni(int? id = null)
        {
            AktivnostID = id;
            InitializeComponent();

            this.Load += AktivnostDodajIzmeni_Load;
            btnSacuvaj.Click += BtnSacuvaj_Click;
            cmbTip.SelectedIndexChanged += CmbTip_SelectedIndexChanged;

            numStarosnaDo.ValueChanged += NumStarosnaDo_ValueChanged;
            numStarosnaOd.ValueChanged += NumStarosnaOd_ValueChanged;
        }

        private async void AktivnostDodajIzmeni_Load(object sender, EventArgs e)
        {
            cmbTip.Items.AddRange(new string[]
            {
                "Izlet",
                "Sportski trening",
                "Kulturni program",
                "Radionica"
            });

            if (AktivnostID.HasValue)
            {
                try
                {
                    var aktivnost = await DTOManager.GetAktivnostAsync(AktivnostID.Value);

                    cmbTip.SelectedItem = aktivnost.Tip;
                    txtNaziv.Text = aktivnost.Naziv;
                    dateDatum.Value = aktivnost.Datum ?? DateTime.Now;

                    if (!string.IsNullOrEmpty(aktivnost.StarosnaGrupa))
                    {
                        var parts = aktivnost.StarosnaGrupa.Split('-');
                        if (parts.Length == 2)
                        {
                            numStarosnaOd.Value = decimal.Parse(parts[0]);
                            numStarosnaDo.Value = decimal.Parse(parts[1]);
                        }
                    }

                    numMaxUcesnika.Value = aktivnost.MaxUcesnika;
                    txtOgranicenja.Text = aktivnost.Ogranicenja;

                    txtSport.Text = aktivnost.Sport;
                    txtPosebnaOprema.Text = aktivnost.PosebnaOprema;
                    txtPotrebnaOprema.Text = aktivnost.PotrebnaOprema;
                    txtPrevoz.Text = aktivnost.PrevoznoSredstvo;
                    txtVodic.Text = aktivnost.Vodic;
                    txtPlanPuta.Text = aktivnost.PlanPuta;

                    PrilagodiPolja(aktivnost.Tip);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                cmbTip.SelectedIndex = 0;
                PrilagodiPolja(cmbTip.SelectedItem.ToString());
            }
        }

        private void CmbTip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTip.SelectedItem != null)
            {
                PrilagodiPolja(cmbTip.SelectedItem.ToString());
            }
        }

        private void PrilagodiPolja(string tip)
        {
            // Uvek vidljiva polja (5 obaveznih)
            lblNaziv.Visible = txtNaziv.Visible =
            lblDatum.Visible = dateDatum.Visible =
            lblStarosna.Visible = numStarosnaOd.Visible = lblDo.Visible = numStarosnaDo.Visible =
            lblMaxUcesnika.Visible = numMaxUcesnika.Visible =
            lblOgranicenja.Visible = txtOgranicenja.Visible = true;

            // Reset vidljivosti specijalnih polja
            lblSport.Visible = txtSport.Visible = false;
            lblPosebnaOprema.Visible = txtPosebnaOprema.Visible = false;
            lblPotrebnaOprema.Visible = txtPotrebnaOprema.Visible = false;
            lblPrevoz.Visible = txtPrevoz.Visible = false;
            lblVodic.Visible = txtVodic.Visible = false;
            lblPlanPuta.Visible = txtPlanPuta.Visible = false;


            if (tip == "Sportski trening")
            {
                lblSport.Visible = txtSport.Visible = true;
                lblPosebnaOprema.Visible = txtPosebnaOprema.Visible = true;
            }
            else if (tip == "Izlet")
            {
                lblPotrebnaOprema.Visible = txtPotrebnaOprema.Visible = true;
                lblPrevoz.Visible = txtPrevoz.Visible = true;
                lblVodic.Visible = txtVodic.Visible = true;
                lblPlanPuta.Visible = txtPlanPuta.Visible = true;
            }
        }

        private void NumStarosnaDo_ValueChanged(object sender, EventArgs e)
        {
            if (numStarosnaDo.Value < numStarosnaOd.Value)
                numStarosnaDo.Value = numStarosnaOd.Value;
        }

        private void NumStarosnaOd_ValueChanged(object sender, EventArgs e)
        {
            if (numStarosnaOd.Value > numStarosnaDo.Value)
                numStarosnaOd.Value = numStarosnaDo.Value;
        }

        private async void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNaziv.Text))
            {
                MessageBox.Show("Naziv aktivnosti je obavezan.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (numStarosnaOd.Value > numStarosnaDo.Value)
            {
                MessageBox.Show("Starosna grupa: vrednost 'od' mora biti manja ili jednaka 'do'.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var aktivnost = new AktivnostBasic
            {
                Tip = cmbTip.SelectedItem.ToString(),
                Naziv = txtNaziv.Text,
                Datum = dateDatum.Value,
                StarosnaGrupa = $"{numStarosnaOd.Value}-{numStarosnaDo.Value}",
                MaxUcesnika = (int)numMaxUcesnika.Value,
                Ogranicenja = txtOgranicenja.Text,
                Sport = txtSport.Visible ? txtSport.Text : null,
                PotrebnaOprema = txtPotrebnaOprema.Visible ? txtPotrebnaOprema.Text : null,
                PosebnaOprema = txtPosebnaOprema.Visible ? txtPosebnaOprema.Text : null,
                PrevoznoSredstvo = txtPrevoz.Visible ? txtPrevoz.Text : null,
                Vodic = txtVodic.Visible ? txtVodic.Text : null,
                PlanPuta = txtPlanPuta.Visible ? txtPlanPuta.Text : null
            };

            try
            {
                if (AktivnostID.HasValue)
                {
                    aktivnost.Id = AktivnostID.Value;
                    await DTOManager.UpdateAktivnostAsync(aktivnost);
                    MessageBox.Show("Aktivnost uspešno ažurirana!", "Uspešno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await DTOManager.AddAktivnostAsync(aktivnost);
                    MessageBox.Show("Aktivnost uspešno dodata!", "Uspešno", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
