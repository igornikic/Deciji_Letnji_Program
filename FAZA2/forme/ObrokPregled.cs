using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class ObrokPregled : Form
    {
        private readonly string nazivLokacijeFilter;
        private readonly int? aktivnostIdFilter; // ID aktivnosti za filtriranje obroka

        public ObrokPregled()
        {
            InitializeComponent();
            this.Load += ObrokPregled_Load;

            btnDodaj.Click += BtnDodaj_Click;
            btnIzmeni.Click += BtnIzmeni_Click;
            btnObrisi.Click += BtnObrisi_Click;
        }

        public ObrokPregled(string nazivLokacije)
        {
            InitializeComponent();
            this.nazivLokacijeFilter = nazivLokacije;
            this.Load += ObrokPregled_Load;

            btnDodaj.Visible = false;
            btnIzmeni.Visible = false;
            btnObrisi.Visible = false;
        }

        public ObrokPregled(int aktivnostId)
        {
            InitializeComponent();
            this.aktivnostIdFilter = aktivnostId;
            this.Load += ObrokPregled_Load;

            btnDodaj.Visible = false;
            btnIzmeni.Visible = false;
            btnObrisi.Visible = false;
        }

        private async void ObrokPregled_Load(object sender, EventArgs e)
        {
            await UcitajObrokeAsync();
        }

        private async Task UcitajObrokeAsync()
        {
            try
            {
                List<DTOs.ObrokPregled> lista;

                if (aktivnostIdFilter.HasValue)
                {
                    lista = await DTOManager.GetObrociZaAktivnostAsync(aktivnostIdFilter.Value);
                }
                else if (!string.IsNullOrEmpty(nazivLokacijeFilter))
                {
                    lista = await DTOManager.GetObrociZaLokacijuAsync(nazivLokacijeFilter);
                }
                else
                {
                    lista = await DTOManager.GetAllObrociAsync();
                }

                dataGridViewObroci.DataSource = lista;

                dataGridViewObroci.Columns["ID"].HeaderText = "ID";
                dataGridViewObroci.Columns["Tip"].HeaderText = "Tip obroka";
                dataGridViewObroci.Columns["Jelovnik"].HeaderText = "Jelovnik";
                dataGridViewObroci.Columns["Uzrast"].HeaderText = "Uzrast";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDodaj_Click(object sender, EventArgs e)
        {
            var forma = new ObrokDodajIzmeni();
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
