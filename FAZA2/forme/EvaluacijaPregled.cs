using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class EvaluacijaPregled : Form
    {
        public EvaluacijaPregled()
        {
            InitializeComponent();
            this.Load += EvaluacijaPregled_Load;

            btnDodaj.Click += BtnDodaj_Click;
            btnObrisi.Click += BtnObrisi_Click;
        }

        private async void EvaluacijaPregled_Load(object sender, EventArgs e)
        {
            await UcitajEvaluacijeAsync();
        }

        private async Task UcitajEvaluacijeAsync()
        {
            try
            {
                var lista = await DTOManager.GetAllEvaluacijeAsync();
                dataGridViewEvaluacije.DataSource = lista;

                dataGridViewEvaluacije.Columns["Id"].HeaderText = "ID";
                dataGridViewEvaluacije.Columns["Ocena"].HeaderText = "Ocena";
                dataGridViewEvaluacije.Columns["Datum"].HeaderText = "Datum";
                dataGridViewEvaluacije.Columns["Opis"].HeaderText = "Opis";

                // Poravnaj kolone i prikaz
                dataGridViewEvaluacije.Columns["Opis"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju evaluacija: " + ex.Message,
                                "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDodaj_Click(object sender, EventArgs e)
        {
            var forma = new EvaluacijaDodajIzmeni();
            forma.ShowDialog();
            _ = UcitajEvaluacijeAsync();
        }

        private void BtnIzmeni_Click(object sender, EventArgs e)
        {
            if (dataGridViewEvaluacije.CurrentRow == null)
                return;

            int id = (int)dataGridViewEvaluacije.CurrentRow.Cells["Id"].Value;
            var forma = new EvaluacijaDodajIzmeni(id);
            forma.ShowDialog();
            _ = UcitajEvaluacijeAsync();
        }

        private async void BtnObrisi_Click(object sender, EventArgs e)
        {
            if (dataGridViewEvaluacije.CurrentRow == null)
            {
                MessageBox.Show("Morate izabrati evaluaciju za brisanje.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dataGridViewEvaluacije.CurrentRow.Cells["Id"].Value;

            var potvrda = MessageBox.Show("Da li ste sigurni da želite da obrišete ovu evaluaciju?",
                                          "Potvrda", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (potvrda == DialogResult.Yes)
            {
                try
                {
                    await DTOManager.DeleteEvaluacijaAsync(id);
                    MessageBox.Show("Evaluacija uspešno obrisana.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await UcitajEvaluacijeAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška pri brisanju evaluacije: " + ex.Message,
                                    "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
