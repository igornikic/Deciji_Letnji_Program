using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class PrijavaDodajIzmeni : Form
    {
        private int? prijavaID;
        private bool _loading = false;

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
                _loading = true;

                cmbStatus.Items.Clear();
                cmbStatus.Items.Add("na čekanju");
                cmbStatus.Items.Add("odobreno");
                cmbStatus.Items.Add("odbijeno");

                var aktivnosti = await DTOManager.GetAllAktivnostiAsync();
                cmbAktivnosti.DisplayMember = "Naziv";
                cmbAktivnosti.ValueMember = "Id";
                cmbAktivnosti.DataSource = aktivnosti;

                cmbAktivnosti.SelectedIndexChanged += CmbAktivnosti_SelectedIndexChanged;
                cmbRoditelji.SelectedIndexChanged += CmbRoditelji_SelectedIndexChanged;

                cmbRoditelji.Enabled = false;
                cmbDeca.Enabled = false;

                if (prijavaID.HasValue)
                {
                    var prijava = await DTOManager.GetPrijavaAsync(prijavaID.Value);

                    dateTimePickerDatum.Value = prijava.DatumPrijave;
                    cmbStatus.SelectedItem = prijava.Status;
                    cmbStatus.Enabled = true;

                    cmbAktivnosti.SelectedValue = prijava.Aktivnost.Id;

                    await UcitajRoditeljeAsync(prijava.Aktivnost.Id);
                    cmbRoditelji.SelectedValue = prijava.Roditelj.Id;

                    await UcitajDecuAsync(prijava.Roditelj.Id, prijava.Aktivnost.Id);
                    cmbDeca.SelectedValue = prijava.Dete.Id;

                    cmbRoditelji.Enabled = false;
                    cmbDeca.Enabled = false;
                    dateTimePickerDatum.Enabled = false;
                    cmbAktivnosti.Enabled = false;
                }
                else
                {
                    // Nova prijava
                    cmbStatus.SelectedItem = "na čekanju";
                    cmbStatus.Enabled = false; // ne moze menjati status
                    cmbAktivnosti.SelectedIndex = -1;
                    cmbRoditelji.DataSource = null;
                    cmbDeca.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom učitavanja podataka: " + ex.Message);
            }
            finally
            {
                _loading = false;
            }
        }

        private async void CmbAktivnosti_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading) return;

            if (cmbAktivnosti.SelectedValue == null)
            {
                cmbRoditelji.Enabled = false;
                cmbDeca.Enabled = false;
                cmbRoditelji.DataSource = null;
                cmbDeca.DataSource = null;
                return;
            }

            if (!(cmbAktivnosti.SelectedValue is int aktivnostId))
            {
                MessageBox.Show("Greška: nevalidan ID aktivnosti.");
                return;
            }

            bool moze = await DTOManager.MozePrijavaAsync(aktivnostId);
            if (!moze)
            {
                MessageBox.Show("Aktivnost je već popunjena.");
                cmbRoditelji.Enabled = false;
                cmbDeca.Enabled = false;
                cmbRoditelji.DataSource = null;
                cmbDeca.DataSource = null;
                return;
            }

            await UcitajRoditeljeAsync(aktivnostId);
            cmbRoditelji.Enabled = true;

            cmbDeca.DataSource = null;
            cmbDeca.Enabled = false;
        }

        private async void CmbRoditelji_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading) return;

            if (cmbRoditelji.SelectedValue == null || cmbAktivnosti.SelectedValue == null)
            {
                cmbDeca.DataSource = null;
                cmbDeca.Enabled = false;
                return;
            }

            if (!(cmbRoditelji.SelectedValue is int roditeljId) || !(cmbAktivnosti.SelectedValue is int aktivnostId))
            {
                MessageBox.Show("Greška: nevalidan ID roditelja ili aktivnosti.");
                return;
            }

            await UcitajDecuAsync(roditeljId, aktivnostId);
            cmbDeca.Enabled = true;
        }

        private async Task UcitajRoditeljeAsync(int aktivnostId)
        {
            var roditelji = await DTOManager.GetRoditeljiZaAktivnostAsync(aktivnostId);
            cmbRoditelji.DisplayMember = "ImePrezime";
            cmbRoditelji.ValueMember = "ID";
            cmbRoditelji.DataSource = roditelji;
            cmbRoditelji.SelectedIndex = -1;
        }

        private async Task UcitajDecuAsync(int roditeljId, int aktivnostId)
        {
            var deca = await DTOManager.GetDecaZaRoditeljaIAktivnostAsync(roditeljId, aktivnostId);
            cmbDeca.DisplayMember = "PunoIme";
            cmbDeca.ValueMember = "ID";
            cmbDeca.DataSource = deca;
            cmbDeca.SelectedIndex = -1;
        }

        private async void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(cmbAktivnosti.SelectedValue is int aktivnostId)
                    || !(cmbRoditelji.SelectedValue is int roditeljId)
                    || !(cmbDeca.SelectedValue is int deteId))
                {
                    MessageBox.Show("Sva polja su obavezna.");
                    return;
                }

                DateTime datum = dateTimePickerDatum.Value;
                string status = cmbStatus.SelectedItem?.ToString() ?? "na čekanju";

                if (prijavaID.HasValue)
                {
                    var prijava = new PrijavaBasic
                    {
                        IdPrijave = prijavaID.Value,
                        DatumPrijave = datum,
                        Status = status,
                        Aktivnost = new AktivnostBasic { Id = aktivnostId },
                        Roditelj = new RoditeljBasic { Id = roditeljId },
                        Dete = new DeteBasic { Id = deteId }
                    };

                    await DTOManager.UpdatePrijavaAsync(prijava);
                    MessageBox.Show("Prijava uspešno ažurirana.");
                }
                else
                {
                    await DTOManager.AddPrijavaAsync(aktivnostId, roditeljId, deteId, datum, status);
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
