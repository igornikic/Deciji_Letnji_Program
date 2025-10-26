using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deciji_Letnji_Program.Forme
{
    public partial class DodajKomentarOcenu : Form
    {
        private readonly int _deteId;
        private readonly int? _roditeljId;
        private List<DTOs.AktivnostPregled> aktivnostiZaDete;

        public DodajKomentarOcenu(int deteId)
        {
            _deteId = deteId;
            InitializeComponent();
            this.Load += DodajKomentarOcenu_Load;
        }

        public DodajKomentarOcenu(int deteId, int roditeljId) : this(deteId)
        {
            _roditeljId = roditeljId;
        }

        private void DodajKomentarOcenu_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                _ = UcitajAktivnostiAsync();
            }
        }

        private async Task UcitajAktivnostiAsync()
        {
            try
            {
                aktivnostiZaDete = await DTOManager.GetAktivnostiZaDeteAsync(_deteId);
                cmbAktivnosti.DataSource = null;
                cmbAktivnosti.DataSource = aktivnostiZaDete;
                cmbAktivnosti.DisplayMember = "Naziv";
                cmbAktivnosti.ValueMember = "Id";
                cmbAktivnosti.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom učitavanja aktivnosti: " + ex.Message,
                    "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDodaj_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbAktivnosti.SelectedItem == null)
                {
                    MessageBox.Show("Morate izabrati aktivnost.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var izabranaAktivnost = cmbAktivnosti.SelectedItem as DTOs.AktivnostPregled;
                var svaUcesca = await DTOManager.GetAllUcescaBasicAsync();

                var postojeca = svaUcesca.FirstOrDefault(u =>
                    u.Dete.Id == _deteId &&
                    u.Aktivnost.Id == izabranaAktivnost.Id
                );

                if (postojeca == null)
                {
                    MessageBox.Show("Ne postoji učešće za ovu aktivnost.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // LOGIKA ZA RODITELJA
                if (_roditeljId != null)
                {
                    if (postojeca.Roditelj == null || postojeca.Roditelj.Id != _roditeljId)
                    {
                        MessageBox.Show("Ovaj roditelj nije povezan sa izabranom aktivnošću.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (postojeca.Pratilac != "Da")
                    {
                        MessageBox.Show("Samo pratilac može ostaviti komentar i ocenu.", "Zabrana", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!string.IsNullOrWhiteSpace(postojeca.Komentari) || postojeca.OcenaAktivnosti != null)
                    {
                        MessageBox.Show("Komentar i ocena već postoje.", "Obaveštenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                // LOGIKA ZA DETE
                else
                {
                    if (postojeca.Prisustvo != "Da")
                    {
                        MessageBox.Show("Dete nije prisustvovalo aktivnosti i ne može se oceniti.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (!string.IsNullOrWhiteSpace(postojeca.Komentari) || postojeca.OcenaAktivnosti != null)
                    {
                        MessageBox.Show("Komentar i ocena za ovu aktivnost već postoje.", "Obaveštenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                // Azuriraj komentar i ocenu
                postojeca.OcenaAktivnosti = (int)nudOcena.Value;
                postojeca.Komentari = txtKomentar.Text;

                await DTOManager.UpdateUcesceAsync(postojeca);

                MessageBox.Show("Komentar i ocena uspešno dodati.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtKomentar.Clear();
                nudOcena.Value = nudOcena.Minimum;
                cmbAktivnosti.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške prilikom dodavanja komentara i ocene: " + ex.Message,
                    "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
