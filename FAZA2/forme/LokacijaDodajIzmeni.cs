using System;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class LokacijaDodajIzmeni : Form
    {
        private string naziv;

        public LokacijaDodajIzmeni(string naziv = null)
        {
            this.naziv = naziv;
            InitializeComponent();
            this.Load += LokacijaDodajIzmeni_Load;
            btnSacuvaj.Click += BtnSacuvaj_Click;
        }

        private async void LokacijaDodajIzmeni_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(naziv))
            {
                try
                {
                    var lokacija = await DTOManager.GetLokacijaAsync(naziv);
                    if (lokacija == null)
                    {
                        MessageBox.Show("Lokacija nije pronađena.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }

                    txtNaziv.Text = lokacija.Naziv;
                    txtNaziv.Enabled = false;

                    if (lokacija.Tip == "otvoreni prostor")
                        radioOtvoreni.Checked = true;
                    else if (lokacija.Tip == "zatvoreni prostor")
                        radioZatvoreni.Checked = true;

                    txtAdresa.Text = lokacija.Adresa;
                    numKapacitet.Value = lokacija.Kapacitet;
                    txtOprema.Text = lokacija.DostupnaOprema;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNaziv.Text) || (!radioOtvoreni.Checked && !radioZatvoreni.Checked))
            {
                MessageBox.Show("Naziv i tip su obavezna polja.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string tip = radioOtvoreni.Checked ? "otvoreni prostor" : "zatvoreni prostor";

            var lokacija = new LokacijaBasic
            {
                Naziv = txtNaziv.Text,
                Tip = tip,
                Adresa = txtAdresa.Text,
                Kapacitet = (int)numKapacitet.Value,
                DostupnaOprema = txtOprema.Text
            };

            try
            {
                if (!string.IsNullOrEmpty(naziv))
                {
                    await DTOManager.UpdateLokacijaAsync(lokacija);
                    MessageBox.Show("Lokacija uspešno ažurirana!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await DTOManager.AddLokacijaAsync(lokacija);
                    MessageBox.Show("Lokacija uspešno dodata!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
