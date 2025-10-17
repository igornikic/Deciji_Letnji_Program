using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class KontaktDodajIzmeni : Form
    {
        private int? KontaktID;
        private int RoditeljID;
        private bool IsTelefon; // true - telefon, false - email

        public KontaktDodajIzmeni(int roditeljId, bool isTelefon, int? kontaktId = null)
        {
            InitializeComponent();
            RoditeljID = roditeljId;
            IsTelefon = isTelefon;
            KontaktID = kontaktId;

            this.Load += KontaktDodajIzmeni_Load;
            btnSacuvaj.Click += BtnSacuvaj_Click;
        }

        private async void KontaktDodajIzmeni_Load(object sender, EventArgs e)
        {
            lblNaslov.Text = IsTelefon ? "Telefon roditelja" : "Email roditelja";

            if (KontaktID.HasValue)
            {
                try
                {
                    if (IsTelefon)
                    {
                        var tel = await DTOManager.GetTelefonRoditeljaAsync(KontaktID.Value);
                        if (tel != null)
                            txtKontakt.Text = tel.Telefon;
                    }
                    else
                    {
                        var email = await DTOManager.GetEmailRoditeljaAsync(KontaktID.Value);
                        if (email != null)
                            txtKontakt.Text = email.Email;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void BtnSacuvaj_Click(object sender, EventArgs e)
        {
            var vrednost = txtKontakt.Text.Trim();
            if (string.IsNullOrWhiteSpace(vrednost))
            {
                MessageBox.Show("Polje ne sme biti prazno.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsTelefon && !ValidirajTelefon(vrednost))
            {
                MessageBox.Show("Neispravan format telefona.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsTelefon && !ValidirajEmail(vrednost))
            {
                MessageBox.Show("Neispravan format email adrese.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (IsTelefon)
                {
                    var dto = new TelefonRoditeljaBasic { Telefon = vrednost };
                    if (KontaktID.HasValue)
                    {
                        dto.Id = KontaktID.Value;
                        await DTOManager.UpdateTelefonRoditeljaAsync(dto);
                        MessageBox.Show("Telefon uspešno ažuriran.", "Uspešno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        await DTOManager.AddTelefonRoditeljaAsync(dto, RoditeljID);
                        MessageBox.Show("Telefon uspešno dodat.", "Uspešno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    var dto = new EmailRoditeljaBasic { Email = vrednost };
                    if (KontaktID.HasValue)
                    {
                        dto.Id = KontaktID.Value;
                        await DTOManager.UpdateEmailRoditeljaAsync(dto);
                        MessageBox.Show("Email uspešno ažuriran.", "Uspešno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        await DTOManager.AddEmailRoditeljaAsync(dto, RoditeljID);
                        MessageBox.Show("Email uspešno dodat.", "Uspešno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidirajTelefon(string telefon) => Regex.IsMatch(telefon, @"^\+?\d{6,}$");
        private bool ValidirajEmail(string email) => Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
