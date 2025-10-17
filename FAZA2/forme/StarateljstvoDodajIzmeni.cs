using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class StarateljstvoDodajIzmeni : Form
    {
        private readonly int _roditeljId;

        public StarateljstvoDodajIzmeni(int roditeljId)
        {
            InitializeComponent();
            _roditeljId = roditeljId;

            this.Load += StarateljstvoDodajIzmeni_Load;
            btnSacuvaj.Click += BtnSacuvaj_Click;
            btnOtkazi.Click += (s, e) => this.Close();
        }

        private async void StarateljstvoDodajIzmeni_Load(object sender, EventArgs e)
        {
            await UcitajDecuAsync();
        }

        private async Task UcitajDecuAsync()
        {
            try
            {
                var deca = await DTOManager.GetDecaZaDodavanjeStarateljstvaAsync(_roditeljId);
                comboBoxDeca.DataSource = deca;
                comboBoxDeca.DisplayMember = "PunoIme";
                comboBoxDeca.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            if (comboBoxDeca.SelectedItem == null)
            {
                MessageBox.Show("Morate izabrati dete.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int deteId = (int)comboBoxDeca.SelectedValue;

            try
            {
                await DTOManager.DodajStarateljstvoAsync(deteId, _roditeljId);
                MessageBox.Show("Starateljstvo uspešno dodato.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
