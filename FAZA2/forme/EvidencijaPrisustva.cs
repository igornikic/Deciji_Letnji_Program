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
            await UcitajUcescaAsync();
            await UcitajDecuAsync();
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



        private async Task UcitajUcescaAsync()
        {
            try
            {
                var ucesca = await DTOManager.GetUcescaZaAktivnostAsync(aktivnostId);
                dataGridViewUcesca.DataSource = ucesca;
                dataGridViewUcesca.Columns["ID"].HeaderText = "ID";
                dataGridViewUcesca.Columns["Prisustvo"].HeaderText = "Prisustvo";
                dataGridViewUcesca.Columns["OcenaAktivnosti"].HeaderText = "Ocena aktivnosti";
                dataGridViewUcesca.Columns["Komentari"].HeaderText = "Komentari";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom učitavanja učešća: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task UcitajDecuAsync()
        {
            try
            {
                var deca = await DTOManager.GetDecaNaAktivnostiAsync(aktivnostId);
                cmbDeca.DataSource = deca;
                cmbDeca.DisplayMember = "PunoIme"; 
                cmbDeca.ValueMember = "ID"; 
                cmbDeca.SelectedIndex = -1;    

                cmbDeca.SelectedIndexChanged += async (s, e) =>
                {
                    if (cmbDeca.SelectedValue != null)
                    {
                        await UcitajRoditeljeAsync((int)cmbDeca.SelectedValue);
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom učitavanja dece: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Komentari = null,
                };

                await DTOManager.AddUcesceAsync(ucesce);
                MessageBox.Show("Učešće uspešno dodato.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await UcitajUcescaAsync();
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
                    await UcitajUcescaAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom brisanja učešća: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
