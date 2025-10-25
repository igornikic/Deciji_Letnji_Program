using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class ObrokDodajIzmeni : Form
    {
        private int? ObrokID;

        public ObrokDodajIzmeni(int? id = null)
        {
            ObrokID = id;
            InitializeComponent();

            this.Load += ObrokDodajIzmeni_Load;
            btnSacuvaj.Click += BtnSacuvaj_Click;

            numUzrastOd.ValueChanged += NumUzrastOd_ValueChanged;
            numUzrastDo.ValueChanged += NumUzrastDo_ValueChanged;
        }

        private async void ObrokDodajIzmeni_Load(object sender, EventArgs e)
        {
            try
            {
                var lokacije = await DTOManager.GetAllLokacijeAsync();
                cmbLokacija.DataSource = lokacije;
                cmbLokacija.DisplayMember = "Naziv";
                cmbLokacija.ValueMember = "Naziv";
                cmbLokacija.SelectedIndex = -1;

                var aktivnosti = await DTOManager.GetAllAktivnostiAsync();
                cmbAktivnost.DataSource = aktivnosti;
                cmbAktivnost.DisplayMember = "Naziv";
                cmbAktivnost.ValueMember = "Id";
                cmbAktivnost.SelectedIndex = -1;

                if (ObrokID.HasValue)
                {
                    var obrok = await DTOManager.GetObrokAsync(ObrokID.Value);

                    string tip = obrok.Tip?.Trim().ToLower();
                    if (tip == "doručak" || tip == "dorucak")
                        rbDorucak.Checked = true;
                    else if (tip == "ručak" || tip == "rucak")
                        rbRucak.Checked = true;
                    else if (tip == "večera" || tip == "vecera")
                        rbVecera.Checked = true;

                    if (!string.IsNullOrEmpty(obrok.Uzrast))
                    {
                        var parts = obrok.Uzrast.Split('-');
                        if (parts.Length == 2 &&
                            decimal.TryParse(parts[0], out decimal od) &&
                            decimal.TryParse(parts[1], out decimal doVrednost))
                        {
                            numUzrastOd.Value = od;
                            numUzrastDo.Value = doVrednost;
                        }
                    }

                    txtJelovnik.Text = obrok.Jelovnik ?? "";

                    cbBezglutenski.Checked = false;
                    cbVegetarijanski.Checked = false;
                    cbBezLaktoze.Checked = false;
                    cbPosno.Checked = false;

                    if (!string.IsNullOrEmpty(obrok.PosebneOpcije))
                    {
                        var opcije = obrok.PosebneOpcije
                            .Split(',')
                            .Select(o => o.Trim().ToLower())
                            .ToList();

                        if (opcije.Any(o => o.Contains("bezglut"))) cbBezglutenski.Checked = true;
                        if (opcije.Any(o => o.Contains("vegetar"))) cbVegetarijanski.Checked = true;
                        if (opcije.Any(o => o.Contains("laktoz"))) cbBezLaktoze.Checked = true;
                        if (opcije.Any(o => o.Contains("posno"))) cbPosno.Checked = true;
                    }

                    if (obrok.Lokacija != null)
                    {
                        cmbLokacija.SelectedItem = ((System.Collections.IList)cmbLokacija.DataSource)
                            .Cast<DTOs.LokacijaPregled>()
                            .FirstOrDefault(l => l.Naziv == obrok.Lokacija.Naziv);
                    }

                    if (obrok.Aktivnost != null)
                    {
                        cmbAktivnost.SelectedItem = ((System.Collections.IList)cmbAktivnost.DataSource)
                            .Cast<DTOs.AktivnostPregled>()
                            .FirstOrDefault(a => a.Id == obrok.Aktivnost.IdAktivnosti);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NumUzrastOd_ValueChanged(object sender, EventArgs e)
        {
            if (numUzrastOd.Value > numUzrastDo.Value)
                numUzrastOd.Value = numUzrastDo.Value;
        }

        private void NumUzrastDo_ValueChanged(object sender, EventArgs e)
        {
            if (numUzrastDo.Value < numUzrastOd.Value)
                numUzrastDo.Value = numUzrastOd.Value;
        }

        private async void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            var tip = rbDorucak.Checked ? "Doručak" :
                      rbRucak.Checked ? "Ručak" :
                      rbVecera.Checked ? "Večera" : null;

            if (tip == null)
            {
                MessageBox.Show("Odaberite tip obroka.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtJelovnik.Text))
            {
                MessageBox.Show("Unesite jelovnik.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (numUzrastOd.Value > numUzrastDo.Value)
            {
                MessageBox.Show("Uzrast: vrednost 'od' mora biti manja ili jednaka 'do'.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selektovanaLokacija = cmbLokacija.SelectedItem as DTOs.LokacijaPregled;
            var selektovanaAktivnost = cmbAktivnost.SelectedItem as DTOs.AktivnostPregled;

            var opcije = new StringBuilder();
            if (cbBezglutenski.Checked) opcije.Append("Bezglutenski, ");
            if (cbVegetarijanski.Checked) opcije.Append("Vegetarijanski, ");
            if (cbBezLaktoze.Checked) opcije.Append("Bez laktoze, ");
            if (cbPosno.Checked) opcije.Append("Posno, ");

            var posebneOpcije = opcije.ToString().TrimEnd(',', ' ');

            var obrok = new ObrokBasic
            {
                Tip = tip,
                Uzrast = $"{numUzrastOd.Value}-{numUzrastDo.Value}",
                Jelovnik = txtJelovnik.Text,
                PosebneOpcije = posebneOpcije,
                Lokacija = selektovanaLokacija != null ? new LokacijaBasic { Naziv = selektovanaLokacija.Naziv } : null,
                Aktivnost = selektovanaAktivnost != null ? new AktivnostBasic { Id = selektovanaAktivnost.Id } : null
            };

            try
            {
                if (ObrokID.HasValue)
                {
                    obrok.Id = ObrokID.Value;
                    await DTOManager.UpdateObrokAsync(obrok);
                    MessageBox.Show("Obrok uspešno ažuriran!", "Uspešno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await DTOManager.AddObrokAsync(obrok);
                    MessageBox.Show("Obrok uspešno dodat!", "Uspešno", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
