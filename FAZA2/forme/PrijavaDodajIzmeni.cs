using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class PrijavaDodajIzmeni : Form
    {
        private int? prijavaID;

        public PrijavaDodajIzmeni(int? id = null)
        {
            prijavaID = id;
            InitializeComponent();
            Load += PrijavaDodajIzmeni_Load;
            btnSacuvaj.Click += BtnSacuvaj_Click;
        }

        private async void PrijavaDodajIzmeni_Load(object sender, EventArgs e)
        {
            try
            {
                // Učitaj sve potrebne relacije
                //var aktivnosti = await DTOManager.GetAllAktivnostiAsync();
                var roditelji = await DTOManager.GetAllRoditeljiAsync();
                var deca = await DTOManager.GetAllDecaAsync();

                //cmbAktivnosti.DataSource = aktivnosti;
                //cmbAktivnosti.DisplayMember = "Naziv";
                //cmbAktivnosti.ValueMember = "Id";

                cmbRoditelji.DataSource = roditelji;
                cmbRoditelji.DisplayMember = "ImePrezime";
                cmbRoditelji.ValueMember = "Id";

                cmbDeca.DataSource = deca;
                cmbDeca.DisplayMember = "ImePrezime";
                cmbDeca.ValueMember = "Id";

                if (prijavaID.HasValue)
                {
                    var prijava = await DTOManager.GetPrijavaAsync(prijavaID.Value);
                    dateTimePickerDatum.Value = prijava.DatumPrijave;
                    txtStatus.Text = prijava.Status;

                    cmbAktivnosti.SelectedValue = prijava.Aktivnost.Id;
                    cmbRoditelji.SelectedValue = prijava.Roditelj.Id;
                    cmbDeca.SelectedValue = prijava.Dete.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom učitavanja podataka: " + ex.Message);
            }
        }

        private async void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            if (cmbAktivnosti.SelectedItem == null || cmbRoditelji.SelectedItem == null || cmbDeca.SelectedItem == null)
            {
                MessageBox.Show("Sva polja su obavezna.");
                return;
            }

            var prijava = new PrijavaBasic
            {
                DatumPrijave = dateTimePickerDatum.Value,
                Status = txtStatus.Text,
                //Aktivnost = (AktivnostBasic)cmbAktivnosti.SelectedItem,
                Roditelj = (RoditeljBasic)cmbRoditelji.SelectedItem,
                Dete = (DeteBasic)cmbDeca.SelectedItem
            };

            try
            {
                if (prijavaID.HasValue)
                {
                    prijava.IdPrijave = prijavaID.Value;
                    await DTOManager.UpdatePrijavaAsync(prijava);
                    MessageBox.Show("Prijava uspešno ažurirana.");
                }
                else
                {
                    await DTOManager.AddPrijavaAsync(prijava);
                    MessageBox.Show("Prijava uspešno dodata.");
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom čuvanja: " + ex.Message);
            }
        }
    }
}
