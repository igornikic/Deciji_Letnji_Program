using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program.Forme
{
    public partial class KontaktPregled : Form
    {
        private int DeteID;

        public KontaktPregled(int deteId)
        {
            InitializeComponent();
            DeteID = deteId;
            this.Load += KontaktPregled_Load;
        }

        private async void KontaktPregled_Load(object sender, EventArgs e)
        {
            await UcitajKontakteAsync();
        }

        private async Task UcitajKontakteAsync()
        {
            try
            {
                // Uzimamo telefone
                var telefoni = await DTOManager.GetTelefoniRoditeljaZaDeteAsync(DeteID);

                // Uzimamo emailove
                var emailovi = await DTOManager.GetEmailoviRoditeljaZaDeteAsync(DeteID);

                var kontakti = new List<KontaktPregledModel>();

                foreach (var t in telefoni)
                {
                    kontakti.Add(new KontaktPregledModel
                    {
                        Id = t.Id,
                        Tip = "Telefon",
                        Vrednost = t.Telefon
                    });
                }

                foreach (var e in emailovi)
                {
                    kontakti.Add(new KontaktPregledModel
                    {
                        Id = e.Id,
                        Tip = "Email",
                        Vrednost = e.Email
                    });
                }

                dataGridViewKontakti.DataSource = kontakti;

                dataGridViewKontakti.Columns["Id"].Visible = false;
                dataGridViewKontakti.Columns["Tip"].HeaderText = "Tip kontakta";
                dataGridViewKontakti.Columns["Vrednost"].HeaderText = "Kontakt";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom učitavanja kontakata: " + ex.Message,
                    "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class KontaktPregledModel
        {
            public int Id { get; set; }
            public string Tip { get; set; }
            public string Vrednost { get; set; }
        }
    }
}
