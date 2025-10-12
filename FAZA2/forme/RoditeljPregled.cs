using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class RoditeljPregled : Form
    {
        public RoditeljPregled()
        {
            InitializeComponent();
            this.Load += FormRoditeljPregled_Load;

            btnDodaj.Click += BtnDodaj_Click;
            btnIzmeni.Click += BtnIzmeni_Click;
            btnObrisi.Click += BtnObrisi_Click;
        }

        private async void FormRoditeljPregled_Load(object sender, EventArgs e)
        {
            await UcitajRoditeljeAsync();
        }

        private async Task UcitajRoditeljeAsync()
        {
            try
            {
                var lista = await DTOManager.GetAllRoditeljiAsync();
                dataGridViewRoditelji.DataSource = lista;
                dataGridViewRoditelji.Columns["Id"].HeaderText = "ID";
                dataGridViewRoditelji.Columns["Ime"].HeaderText = "Ime";
                dataGridViewRoditelji.Columns["Prezime"].HeaderText = "Prezime";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDodaj_Click(object sender, EventArgs e)
        {
            var forma = new RoditeljDodajIzmeni();
            forma.ShowDialog();
            _ = UcitajRoditeljeAsync();
        }

        private void BtnIzmeni_Click(object sender, EventArgs e)
        {
            if (dataGridViewRoditelji.CurrentRow == null)
                return;

            int id = (int)dataGridViewRoditelji.CurrentRow.Cells["Id"].Value;
            var forma = new RoditeljDodajIzmeni(id);
            forma.ShowDialog();
            _ = UcitajRoditeljeAsync();
        }

        private async void BtnObrisi_Click(object sender, EventArgs e)
        {
            if (dataGridViewRoditelji.CurrentRow == null)
                return;

            int id = (int)dataGridViewRoditelji.CurrentRow.Cells["Id"].Value;

            var potvrda = MessageBox.Show("Da li ste sigurni da želite da obrišete ovog roditelja?",
                                          "Potvrda", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (potvrda == DialogResult.Yes)
            {
                try
                {
                    await DTOManager.DeleteRoditeljAsync(id);
                    MessageBox.Show("Roditelj uspešno obrisan.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await UcitajRoditeljeAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
