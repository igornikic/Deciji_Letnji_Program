using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class PrijavaPregled : Form
    {
        public PrijavaPregled()
        {
            InitializeComponent();
            this.Load += PrijavaPregled_Load;

            btnDodaj.Click += BtnDodaj_Click;
            btnIzmeni.Click += BtnIzmeni_Click;
            btnObrisi.Click += BtnObrisi_Click;
        }

        private async void PrijavaPregled_Load(object sender, EventArgs e)
        {
            await UcitajPrijaveAsync();
        }

        private async Task UcitajPrijaveAsync()
        {
            try
            {
                var lista = await DTOManager.GetAllPrijaveAsync();
                dataGridViewPrijave.DataSource = lista;

                dataGridViewPrijave.Columns["IdPrijave"].HeaderText = "ID";
                dataGridViewPrijave.Columns["DatumPrijave"].HeaderText = "Datum prijave";
                dataGridViewPrijave.Columns["Status"].HeaderText = "Status";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom učitavanja prijava: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDodaj_Click(object sender, EventArgs e)
        {
            var forma = new PrijavaDodajIzmeni();
            forma.ShowDialog();
            _ = UcitajPrijaveAsync();
        }

        private void BtnIzmeni_Click(object sender, EventArgs e)
        {
            if (dataGridViewPrijave.CurrentRow == null)
                return;

            int id = (int)dataGridViewPrijave.CurrentRow.Cells["IdPrijave"].Value;
            var forma = new PrijavaDodajIzmeni(id);
            forma.ShowDialog();
            _ = UcitajPrijaveAsync();
        }

        private async void BtnObrisi_Click(object sender, EventArgs e)
        {
            if (dataGridViewPrijave.CurrentRow == null)
            {
                MessageBox.Show("Morate izabrati prijavu za brisanje.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dataGridViewPrijave.CurrentRow.Cells["IdPrijave"].Value;

            var potvrda = MessageBox.Show("Da li ste sigurni da želite da obrišete ovu prijavu?",
                                          "Potvrda", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (potvrda == DialogResult.Yes)
            {
                try
                {
                    await DTOManager.DeletePrijavaAsync(id);
                    MessageBox.Show("Prijava uspešno obrisana.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await UcitajPrijaveAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška prilikom brisanja: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
