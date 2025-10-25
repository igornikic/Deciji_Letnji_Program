using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class PovredaDodajIzmeni : Form
    {
        private readonly int AktivnostID;
        private readonly string OdgovornoLiceJMBG;

        public PovredaDodajIzmeni(int aktivnostId, string odgovornoLiceJMBG)
        {
            InitializeComponent();
            AktivnostID = aktivnostId;
            OdgovornoLiceJMBG = odgovornoLiceJMBG;

            this.Load += PovredaDodajIzmeni_Load;
            btnDodaj.Click += BtnDodaj_Click;
        }

        private async void PovredaDodajIzmeni_Load(object sender, EventArgs e)
        {
            try
            {
                var deca = await DTOManager.GetDecaNaAktivnostiAsync(AktivnostID);
                cmbDeca.DataSource = deca;
                cmbDeca.DisplayMember = "Ime";
                cmbDeca.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom učitavanja dece: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnDodaj_Click(object sender, EventArgs e)
        {
            var dete = cmbDeca.SelectedItem as DTOs.DetePregled;
            if (dete == null)
            {
                MessageBox.Show("Morate izabrati dete.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtOpis.Text))
            {
                MessageBox.Show("Opis povrede je obavezan.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var povreda = new PovredaBasic
            {
                Dete = new DeteBasic { Id = dete.ID },
                Aktivnost = new AktivnostBasic { Id = AktivnostID },
                OdgovornoOsoblje = new AngazovanoLiceBasic { JMBG = OdgovornoLiceJMBG },
                Datum = dateDatum.Value,
                Opis = txtOpis.Text,
                PreduzeteMere = txtMere.Text
            };

            try
            {
                await DTOManager.AddPovredaAsync(povreda);
                MessageBox.Show("Povreda je uspešno prijavljena!", "Uspešno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom dodavanja povrede: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
