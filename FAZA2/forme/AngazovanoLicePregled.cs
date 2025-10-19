using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class AngazovanoLicePregled : Form
    {
        public AngazovanoLicePregled()
        {
            InitializeComponent();
            this.Load += AngazovanoLicePregled_Load;

            btnDodaj.Click += BtnDodaj_Click;
            btnIzmeni.Click += BtnIzmeni_Click;
            btnObrisi.Click += BtnObrisi_Click;
        }

        private async void AngazovanoLicePregled_Load(object sender, EventArgs e)
        {
            await UcitajAngazovanaLicaAsync();
        }

        private async Task UcitajAngazovanaLicaAsync()
        {
            try
            {
                var lista = await DTOManager.GetAllAngazovanaLicaAsync();
                dataGridViewLica.DataSource = lista;

                dataGridViewLica.Columns["JMBG"].HeaderText = "JMBG";
                dataGridViewLica.Columns["Ime"].HeaderText = "Ime";
                dataGridViewLica.Columns["Prezime"].HeaderText = "Prezime";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDodaj_Click(object sender, EventArgs e)
        {
            var forma = new AngazovanoLiceDodajIzmeni();
            forma.ShowDialog();
            _ = UcitajAngazovanaLicaAsync();
        }

        private void BtnIzmeni_Click(object sender, EventArgs e)
        {
            if (dataGridViewLica.CurrentRow == null)
                return;

            string jmbg = dataGridViewLica.CurrentRow.Cells["JMBG"].Value.ToString();
            var forma = new AngazovanoLiceDodajIzmeni(jmbg);
            forma.ShowDialog();
            _ = UcitajAngazovanaLicaAsync();
        }

        private async void BtnObrisi_Click(object sender, EventArgs e)
        {
            if (dataGridViewLica.CurrentRow == null)
            {
                MessageBox.Show("Morate izabrati lice za brisanje.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string jmbg = dataGridViewLica.CurrentRow.Cells["JMBG"].Value.ToString();

            var potvrda = MessageBox.Show("Da li ste sigurni da želite da obrišete ovo lice?",
                                          "Potvrda", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (potvrda == DialogResult.Yes)
            {
                try
                {
                    await DTOManager.DeleteAngazovanoLiceAsync(jmbg);
                    MessageBox.Show("Lice uspešno obrisano.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await UcitajAngazovanaLicaAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
