using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class ObrokPregled : Form
    {
        public ObrokPregled()
        {
            InitializeComponent();
            this.Load += ObrokPregled_Load;

            btnDodaj.Click += BtnDodaj_Click;
            btnIzmeni.Click += BtnIzmeni_Click;
            btnObrisi.Click += BtnObrisi_Click;
        }

        private async void ObrokPregled_Load(object sender, EventArgs e)
        {
            await UcitajObrokeAsync();
        }

        private async Task UcitajObrokeAsync()
        {
            try
            {
                var lista = await DTOManager.GetAllObrociAsync();
                dataGridViewObroci.DataSource = lista;

                // Podesi nazive kolona prema svojstvima DTO klase
                dataGridViewObroci.Columns["ID"].HeaderText = "ID";
                dataGridViewObroci.Columns["Tip"].HeaderText = "Tip obroka";
                dataGridViewObroci.Columns["Uzrast"].HeaderText = "Uzrast";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDodaj_Click(object sender, EventArgs e)
        {
            var forma = new ObrokDodajIzmeni(); // pretpostavimo da postoji forma za dodavanje/izmenu
            forma.ShowDialog();
            _ = UcitajObrokeAsync();
        }

        private void BtnIzmeni_Click(object sender, EventArgs e)
        {
            if (dataGridViewObroci.CurrentRow == null)
                return;

            int id = (int)dataGridViewObroci.CurrentRow.Cells["ID"].Value;
            var forma = new ObrokDodajIzmeni(id);
            forma.ShowDialog();
            _ = UcitajObrokeAsync();
        }

        private async void BtnObrisi_Click(object sender, EventArgs e)
        {
            if (dataGridViewObroci.CurrentRow == null)
            {
                MessageBox.Show("Morate izabrati obrok za brisanje.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dataGridViewObroci.CurrentRow.Cells["ID"].Value;

            var potvrda = MessageBox.Show("Da li ste sigurni da želite da obrišete ovaj obrok?",
                                          "Potvrda", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (potvrda == DialogResult.Yes)
            {
                try
                {
                    await DTOManager.DeleteObrokAsync(id);
                    MessageBox.Show("Obrok uspešno obrisan.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await UcitajObrokeAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
