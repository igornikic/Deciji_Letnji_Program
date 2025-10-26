using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class LokacijaPregled : Form
    {
        private string selectedLocation;
        public LokacijaPregled()
        {
            InitializeComponent();
            this.Load += LokacijaPregled_Load;

            btnDodaj.Click += BtnDodaj_Click;
            btnIzmeni.Click += BtnIzmeni_Click;
            btnObrisi.Click += BtnObrisi_Click;
            btnAktivnosti.Click += BtnAktivnosti_Click;
            btnObroci.Click += BtnObroci_Click;

        }

        private async void LokacijaPregled_Load(object sender, EventArgs e)
        {
            await UcitajLokacijeAsync();
        }

        private async Task UcitajLokacijeAsync()
        {
            try
            {
                var lista = await DTOManager.GetAllLokacijeAsync();
                dataGridViewLokacije.DataSource = lista;

                dataGridViewLokacije.Columns["Naziv"].HeaderText = "Naziv";
                dataGridViewLokacije.Columns["Tip"].HeaderText = "Tip";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDodaj_Click(object sender, EventArgs e)
        {
            var forma = new LokacijaDodajIzmeni();
            forma.ShowDialog();
            _ = UcitajLokacijeAsync();
        }

        private void BtnIzmeni_Click(object sender, EventArgs e)
        {
            if (dataGridViewLokacije.CurrentRow == null)
                return;

            string naziv = dataGridViewLokacije.CurrentRow.Cells["Naziv"].Value.ToString();
            var forma = new LokacijaDodajIzmeni(naziv);
            forma.ShowDialog();
            _ = UcitajLokacijeAsync();
        }

        private async void BtnObrisi_Click(object sender, EventArgs e)
        {
            if (dataGridViewLokacije.CurrentRow == null)
            {
                MessageBox.Show("Morate izabrati lokaciju za brisanje.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string naziv = dataGridViewLokacije.CurrentRow.Cells["Naziv"].Value.ToString();

            var potvrda = MessageBox.Show("Da li ste sigurni da želite da obrišete ovu lokaciju?",
                                          "Potvrda", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (potvrda == DialogResult.Yes)
            {
                try
                {
                    await DTOManager.DeleteLokacijaAsync(naziv);
                    MessageBox.Show("Lokacija uspešno obrisana.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await UcitajLokacijeAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnAktivnosti_Click(object sender, EventArgs e)
        {
            if (dataGridViewLokacije.CurrentRow == null)
            {
                MessageBox.Show("Morate izabrati lokaciju za prikaz aktivnosti.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            selectedLocation = dataGridViewLokacije.CurrentRow.Cells["Naziv"].Value.ToString();

            var aktivnostPregledForm = new AktivnostPregled(nazivLokacije: selectedLocation);
            aktivnostPregledForm.ShowDialog();
        }
        private void BtnObroci_Click(object sender, EventArgs e)
        {
            if (dataGridViewLokacije.CurrentRow == null)
            {
                MessageBox.Show("Morate izabrati lokaciju.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nazivLokacije = dataGridViewLokacije.CurrentRow.Cells["Naziv"].Value.ToString();

            var forma = new ObrokPregled(nazivLokacije);
            forma.ShowDialog();
        }

    }
}
