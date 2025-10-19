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
            if (ObrokID.HasValue)
            {
                try
                {
                    var obrok = await DTOManager.GetObrokAsync(ObrokID.Value);

                    // Radio dugmad za tip
                    switch (obrok.Tip)
                    {
                        case "Doručak": rbDorucak.Checked = true; break;
                        case "Ručak": rbRucak.Checked = true; break;
                        case "Večera": rbVecera.Checked = true; break;
                    }

                    // Uzrast
                    if (!string.IsNullOrEmpty(obrok.Uzrast))
                    {
                        var parts = obrok.Uzrast.Split('-');
                        if (parts.Length == 2)
                        {
                            numUzrastOd.Value = decimal.Parse(parts[0]);
                            numUzrastDo.Value = decimal.Parse(parts[1]);
                        }
                    }

                    // Jelovnik
                    txtJelovnik.Text = obrok.Jelovnik;

                    // Posebne opcije
                    if (!string.IsNullOrEmpty(obrok.PosebneOpcije))
                    {
                        var opcije = obrok.PosebneOpcije.Split(',')
                            .Select(o => o.Trim().ToLower()).ToList();

                        cbBezglutenski.Checked = opcije.Contains("bezglutenski");
                        cbVegetarijanski.Checked = opcije.Contains("vegetarijanski");
                        cbBezLaktoze.Checked = opcije.Contains("bez laktoze");
                        cbPosno.Checked = opcije.Contains("posno");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

            // Sastavi string posebnih opcija
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
                PosebneOpcije = posebneOpcije
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
