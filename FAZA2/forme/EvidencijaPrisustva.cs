using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class EvidencijaPrisustva : Form
    {
        private readonly int aktivnostId;

        public EvidencijaPrisustva(int aktivnostId)
        {
            this.aktivnostId = aktivnostId;
            InitializeComponent();
        }

        private async void EvidencijaPrisustva_Load(object sender, EventArgs e)
        {
            await UcitajPodatkeAsync();
        }

        private async Task UcitajPodatkeAsync()
        {
            try
            {
                var ucesca = await DTOManager.GetUcescaZaAktivnostAsync(aktivnostId);
                var deca = await DTOManager.GetDecaNaAktivnostiAsync(aktivnostId);

                var decaKojaUcestvuju = ucesca
                    .Where(u => u.Dete != null)
                    .Select(u => u.Dete.Id)
                    .ToHashSet();

                var decaKojaNeUcestvuju = deca
                    .Where(d => !decaKojaUcestvuju.Contains(d.ID))
                    .ToList();

                cmbDeca.DataSource = decaKojaNeUcestvuju;
                cmbDeca.DisplayMember = "PunoIme";
                cmbDeca.ValueMember = "ID";
                cmbDeca.SelectedIndex = -1;

                cmbDeca.SelectedIndexChanged += async (s, e) =>
                {
                    if (cmbDeca.SelectedValue != null)
                        await UcitajRoditeljeAsync((int)cmbDeca.SelectedValue);
                };

                var prikaz = ucesca.Select(u => new
                {
                    ID = u.ID,
                    Dete = u.Dete != null ? $"{u.Dete.Ime} {u.Dete.Prezime}" : "N/A",
                    Prisustvo = u.Prisustvo,
                    OcenaAktivnosti = u.OcenaAktivnosti,
                    Komentari = u.Komentari,
                    Pratilac = u.Pratilac,
                    Roditelj = u.Roditelj != null ? $"{u.Roditelj.Ime} {u.Roditelj.Prezime}" : "N/A"
                }).ToList();

                dataGridViewUcesca.DataSource = prikaz;
                dataGridViewUcesca.Columns["Dete"].HeaderText = "Ime i prezime deteta";
                dataGridViewUcesca.Columns["Prisustvo"].HeaderText = "Prisustvo";
                dataGridViewUcesca.Columns["OcenaAktivnosti"].HeaderText = "Ocena aktivnosti";
                dataGridViewUcesca.Columns["Komentari"].HeaderText = "Komentari";
                dataGridViewUcesca.Columns["Pratilac"].HeaderText = "Pratilac";
                dataGridViewUcesca.Columns["Roditelj"].HeaderText = "Ime i prezime roditelja";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom učitavanja podataka: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task UcitajRoditeljeAsync(int deteId)
        {
            try
            {
                var roditelji = await DTOManager.GetRoditeljeZaDeteAsync(deteId);
                cmbPratilac.DataSource = roditelji;
                cmbPratilac.DisplayMember = "ImePrezime";
                cmbPratilac.ValueMember = "Id";
                cmbPratilac.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom učitavanja roditelja: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDodaj_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbDeca.SelectedValue == null)
                {
                    MessageBox.Show("Morate izabrati dete.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string pratilac = cmbPratilac.SelectedItem != null ? "Da" : "Ne";

                var ucesce = new UcestvujeBasic
                {
                    Aktivnost = new AktivnostBasic { Id = aktivnostId },
                    Dete = new DeteBasic { Id = (int)cmbDeca.SelectedValue },
                    Roditelj = cmbPratilac.SelectedItem != null ? new RoditeljBasic { Id = (int)cmbPratilac.SelectedValue } : null,
                    Pratilac = pratilac,
                    Prisustvo = chkPrisustvo.Checked ? "Da" : "Ne",
                    OcenaAktivnosti = null,
                    Komentari = null
                };

                await DTOManager.AddUcesceAsync(ucesce);
                MessageBox.Show("Učešće uspešno dodato.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

                cmbDeca.SelectedIndex = -1;
                cmbPratilac.DataSource = null;
                chkPrisustvo.Checked = false;

                await UcitajPodatkeAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom dodavanja učešća: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnObrisi_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewUcesca.CurrentRow == null)
                {
                    MessageBox.Show("Morate izabrati učešće za brisanje.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = (int)dataGridViewUcesca.CurrentRow.Cells["ID"].Value;
                var potvrda = MessageBox.Show("Da li ste sigurni da želite da obrišete ovo učešće?", "Potvrda", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (potvrda == DialogResult.Yes)
                {
                    await DTOManager.DeleteUcesceAsync(id);
                    MessageBox.Show("Učešće uspešno obrisano.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await UcitajPodatkeAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom brisanja učešća: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
