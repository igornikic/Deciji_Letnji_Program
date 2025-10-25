using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class DetePregled : Form
    {
        public DetePregled()
        {
            InitializeComponent();
            this.Load += FormDetePregled_Load;

            btnDodaj.Click += BtnDodaj_Click;
            btnIzmeni.Click += BtnIzmeni_Click;
            btnObrisi.Click += BtnObrisi_Click;
            btnKontakti.Click += BtnKontakti_Click;
            btnAktivnosti.Click += BtnAktivnosti_Click;
            btnKomentar.Click += BtnKomentar_Click;
            btnPovrede.Click += BtnPovrede_Click;
        }

        private async void FormDetePregled_Load(object sender, EventArgs e)
        {
            await UcitajDecuAsync();
        }

        private async Task UcitajDecuAsync()
        {
            try
            {
                var lista = await DTOManager.GetAllDecaAsync();
                dataGridViewDeca.DataSource = lista;
                dataGridViewDeca.Columns["Id"].HeaderText = "ID";
                dataGridViewDeca.Columns["Ime"].HeaderText = "Ime";
                dataGridViewDeca.Columns["Prezime"].HeaderText = "Prezime";
                dataGridViewDeca.Columns["DatumRodjenja"].HeaderText = "Datum rođenja";
                dataGridViewDeca.Columns["Pol"].HeaderText = "Pol";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDodaj_Click(object sender, EventArgs e)
        {
            var forma = new DeteDodajIzmeni();
            forma.ShowDialog();
            _ = UcitajDecuAsync();
        }

        private void BtnIzmeni_Click(object sender, EventArgs e)
        {
            if (dataGridViewDeca.CurrentRow == null)
                return;

            int id = (int)dataGridViewDeca.CurrentRow.Cells["Id"].Value;
            var forma = new DeteDodajIzmeni(id);
            forma.ShowDialog();
            _ = UcitajDecuAsync();
        }

        private async void BtnObrisi_Click(object sender, EventArgs e)
        {
            if (dataGridViewDeca.CurrentRow == null)
            {
                MessageBox.Show("Morate izabrati dete za brisanje.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dataGridViewDeca.CurrentRow.Cells["Id"].Value;

            var potvrda = MessageBox.Show("Da li ste sigurni da želite da obrišete ovo dete?",
                                          "Potvrda", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (potvrda == DialogResult.Yes)
            {
                try
                {
                    await DTOManager.DeleteDeteAsync(id);
                    MessageBox.Show("Dete uspešno obrisano.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await UcitajDecuAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnKontakti_Click(object sender, EventArgs e)
        {
            if (dataGridViewDeca.CurrentRow == null)
            {
                MessageBox.Show("Morate izabrati dete.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int deteId = (int)dataGridViewDeca.CurrentRow.Cells["Id"].Value;

            var forma = new KontaktPregled(deteId);
            forma.ShowDialog();
        }
        private void BtnAktivnosti_Click(object sender, EventArgs e)
        {
            if (dataGridViewDeca.CurrentRow == null)
            {
                MessageBox.Show("Morate izabrati dete.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int deteId = (int)dataGridViewDeca.CurrentRow.Cells["Id"].Value;

            var forma = new AktivnostPregled(deteId);
            forma.ShowDialog();
        }

        private void btnIzmeni_Click_1(object sender, EventArgs e)
        {

        }

        private void BtnKomentar_Click(object sender, EventArgs e)
        {
            if (dataGridViewDeca.CurrentRow == null)
            {
                MessageBox.Show("Morate izabrati dete.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int deteId = (int)dataGridViewDeca.CurrentRow.Cells["Id"].Value;

            var forma = new DodajKomentarOcenu(deteId);
            forma.ShowDialog();
        }

        private void BtnPovrede_Click(object sender, EventArgs e)
        {
            if (dataGridViewDeca.CurrentRow == null)
            {
                MessageBox.Show("Morate izabrati dete.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int deteId = (int)dataGridViewDeca.CurrentRow.Cells["Id"].Value;
            var forma = new PovredaPregled(deteId);
            forma.ShowDialog();
        }
    }
}
