using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class DodelaObroka : Form
    {
        private readonly int deteId;
        private DeteBasic selektovanoDete;

        public DodelaObroka(int deteId)
        {
            InitializeComponent();
            this.deteId = deteId;
            this.Load += DodelaObroka_Load;
        }

        private async void DodelaObroka_Load(object sender, EventArgs e)
        {
            await UcitajDeteAsync();
            await UcitajObrokeAsync();
            await UcitajObrokeDetetaAsync(deteId);

            if (selektovanoDete != null)
                this.Text = $"Dodela obroka za: {selektovanoDete.Ime} {selektovanoDete.Prezime}";
        }

        private async Task UcitajDeteAsync()
        {
            try
            {
                selektovanoDete = await DTOManager.GetDeteAsync(deteId);
            }
            catch
            {
                selektovanoDete = null;
            }
        }

        private async Task UcitajObrokeAsync()
        {
            try
            {
                var sviObroci = await DTOManager.GetAllObrociAsync();
                var filtriraniObroci = new List<Deciji_Letnji_Program.DTOs.ObrokPregled>();

                if (selektovanoDete != null && !string.IsNullOrEmpty(selektovanoDete.PosebnePotrebe))
                {
                    var potrebe = selektovanoDete.PosebnePotrebe.ToLower();

                    foreach (var obrok in sviObroci)
                    {
                        var obrokDetalji = await DTOManager.GetObrokAsync(obrok.Id);
                        var opcije = obrokDetalji.PosebneOpcije?.ToLower() ?? "";

                        if (potrebe.Contains("bez glutena") && !opcije.Contains("bez glutena"))
                            continue;

                        if (potrebe.Contains("bez mlečnih proizvoda") && !opcije.Contains("bez mlečnih proizvoda"))
                            continue;

                        if (potrebe.Contains("vegetarijanski") && !opcije.Contains("vegetarijanski"))
                            continue;

                        filtriraniObroci.Add(obrok);
                    }
                }
                else
                {
                    filtriraniObroci = sviObroci;
                }

                comboBoxObrok.DataSource = filtriraniObroci;
                comboBoxObrok.DisplayMember = "Jelovnik";
                comboBoxObrok.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task UcitajObrokeDetetaAsync(int deteId)
        {
            try
            {
                var obrociDeteta = await DTOManager.GetObrociZaDeteAsync(deteId);
                dataGridViewObroci.DataSource = obrociDeteta;

                dataGridViewObroci.Columns["Id"].HeaderText = "ID";
                dataGridViewObroci.Columns["Tip"].HeaderText = "Tip";
                dataGridViewObroci.Columns["Jelovnik"].HeaderText = "Jelovnik";
                dataGridViewObroci.Columns["Uzrast"].HeaderText = "Uzrast";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDodeliObrok_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxObrok.SelectedItem == null)
                {
                    MessageBox.Show("Morate izabrati obrok.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var obrokId = (int)comboBoxObrok.SelectedValue;

                await DTOManager.DodeliObrokDetetuAsync(deteId, obrokId);
                MessageBox.Show("Obrok je uspešno dodeljen detetu.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

                dataGridViewObroci.DataSource = null;
                await UcitajObrokeDetetaAsync(deteId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
