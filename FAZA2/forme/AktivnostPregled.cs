using Deciji_Letnji_Program.Entiteti;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class AktivnostPregled : Form
    {
        private readonly int? deteId;
        private readonly string nazivLokacije;

        public AktivnostPregled(int? deteId = null)
        {
            InitializeComponent();
            this.deteId = deteId;
            this.nazivLokacije = null;
            this.Load += AktivnostPregled_Load;
        }

        public AktivnostPregled(string nazivLokacije)
        {
            InitializeComponent();
            this.nazivLokacije = nazivLokacije;
            this.deteId = null;
            this.Load += AktivnostPregled_Load;
        }

        private async void AktivnostPregled_Load(object sender, EventArgs e)
        {
            await UcitajAktivnostiAsync();
        }

        private async Task UcitajAktivnostiAsync()
        {
            try
            {
                List<DTOs.AktivnostPregled> lista;

                if (deteId.HasValue)
                    lista = await DTOManager.GetAktivnostiZaDeteAsync(deteId.Value);
                else if (!string.IsNullOrEmpty(nazivLokacije))
                    lista = await DTOManager.GetAktivnostiNaLokacijiAsync(nazivLokacije);
                else
                    lista = await DTOManager.GetAllAktivnostiAsync();

                dataGridViewAktivnosti.DataSource = lista;
                dataGridViewAktivnosti.Columns["Id"].HeaderText = "ID";
                dataGridViewAktivnosti.Columns["Tip"].HeaderText = "Tip";
                dataGridViewAktivnosti.Columns["Naziv"].HeaderText = "Naziv";
                dataGridViewAktivnosti.Columns["Datum"].HeaderText = "Datum";

                // Ako prikazujemo aktivnosti za dete ili lokaciju — sakrij sve dugmice osim zatvori
                if (deteId.HasValue || !string.IsNullOrEmpty(nazivLokacije))
                {
                    btnDodaj.Visible = false;
                    btnIzmeni.Visible = false;
                    btnObrisi.Visible = false;
                    btnSpisakDece.Visible = false;
                    btnZaposleni.Visible = false;
                    btnPrikaziObroke.Visible = false; // Sakrij dugme za obroke
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            var forma = new AktivnostDodajIzmeni();
            forma.ShowDialog();
            _ = UcitajAktivnostiAsync();
        }

        private void btnIzmeni_Click(object sender, EventArgs e)
        {
            if (dataGridViewAktivnosti.CurrentRow == null) return;

            int id = (int)dataGridViewAktivnosti.CurrentRow.Cells["Id"].Value;
            var forma = new AktivnostDodajIzmeni(id);
            forma.ShowDialog();
            _ = UcitajAktivnostiAsync();
        }

        private async void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dataGridViewAktivnosti.CurrentRow == null)
            {
                MessageBox.Show("Morate izabrati aktivnost.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dataGridViewAktivnosti.CurrentRow.Cells["Id"].Value;

            var potvrda = MessageBox.Show("Da li ste sigurni da želite da obrišete ovu aktivnost?",
                                          "Potvrda", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (potvrda == DialogResult.Yes)
            {
                try
                {
                    await DTOManager.DeleteAktivnostAsync(id);
                    MessageBox.Show("Aktivnost uspešno obrisana.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await UcitajAktivnostiAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnZaposleni_Click(object sender, EventArgs e)
        {
            if (dataGridViewAktivnosti.CurrentRow == null)
            {
                MessageBox.Show("Morate izabrati aktivnost.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dataGridViewAktivnosti.CurrentRow.Cells["Id"].Value;

            // TODO: Otvori formu sa zaposlenima za aktivnost
            // var formaZaposleni = new ZaposleniZaAktivnost(id);
            // formaZaposleni.ShowDialog();
        }

        private void btnSpisakDece_Click(object sender, EventArgs e)
        {
            if (dataGridViewAktivnosti.CurrentRow == null)
            {
                MessageBox.Show("Morate izabrati aktivnost.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dataGridViewAktivnosti.CurrentRow.Cells["Id"].Value;

            var formaDeca = new DecaNaAktivnosti(id);
            formaDeca.ShowDialog();
        }

        // Novo dugme za prikazivanje obroka
        private async void btnPrikaziObroke_Click(object sender, EventArgs e)
        {
            if (dataGridViewAktivnosti.CurrentRow == null)
            {
                MessageBox.Show("Morate izabrati aktivnost.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dataGridViewAktivnosti.CurrentRow.Cells["Id"].Value;

            try
            {
                // Pozivamo metodu iz DTOManagera koja vraća obroke za selektovanu aktivnost
                var obroci = await DTOManager.GetObrociZaAktivnostAsync(id);

                if (obroci != null && obroci.Count > 0)
                {
                    // Ovdje možete otvoriti novu formu za prikaz obroka
                    var formaObroci = new ObrokPregled(id);
                    formaObroci.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Nema obroka za ovu aktivnost.", "Obaveštenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške pri učitavanju obroka: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}