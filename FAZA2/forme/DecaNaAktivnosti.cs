using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class DecaNaAktivnosti : Form
    {
        private readonly int _aktivnostId;

        public DecaNaAktivnosti(int aktivnostId)
        {
            InitializeComponent();
            _aktivnostId = aktivnostId;
            this.Load += DecaNaAktivnosti_Load;
        }

        private async void DecaNaAktivnosti_Load(object sender, EventArgs e)
        {
            await UcitajDecuAsync();
        }

        private async Task UcitajDecuAsync()
        {
            try
            {
                var deca = await DTOManager.GetDecaNaAktivnostiAsync(_aktivnostId);
                dataGridViewDeca.DataSource = deca;

                dataGridViewDeca.Columns["Id"].HeaderText = "ID";
                dataGridViewDeca.Columns["Ime"].HeaderText = "Ime";
                dataGridViewDeca.Columns["Prezime"].HeaderText = "Prezime";
                dataGridViewDeca.Columns["DatumRodjenja"].HeaderText = "Datum rođenja";
                dataGridViewDeca.Columns["Pol"].HeaderText = "Pol";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DugmeDodelaObroka_Click(object sender, EventArgs e)
        {
            if (dataGridViewDeca.Rows.Count == 0)
            {
                MessageBox.Show("Na ovoj aktivnosti nema dece kojima se može dodeliti obrok.",
                    "Obaveštenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dataGridViewDeca.CurrentRow == null)
            {
                MessageBox.Show("Molimo izaberite dete sa spiska pre dodele obroka.",
                    "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = Convert.ToInt32(dataGridViewDeca.CurrentRow.Cells["Id"].Value);
                var formaDeca = new DodelaObroka(id);
                formaDeca.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške pri otvaranju forme za dodelu obroka: " + ex.Message,
                    "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}