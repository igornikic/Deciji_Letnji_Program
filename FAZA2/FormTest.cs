using System;
using System.Linq;
using System.Windows.Forms;
using NHibernate;
using Deciji_Letnji_Program.Entiteti;

namespace Deciji_Letnji_Program
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        // 1. Učitavanje jednog deteta
        private void cmdUcitajDete_Click(object sender, EventArgs e)
        {
            try
            {
                using (ISession s = DataLayer.GetSession())
                {
                    Dete d = s.Load<Dete>(1); 
                    MessageBox.Show($"Dete: {d.Ime} {d.Prezime}, adresa: {d.Adresa}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message);
            }
        }

        // 2. Dodavanje novog deteta
        private void cmdDodajDete_Click(object sender, EventArgs e)
        {
            try
            {
                using (ISession s = DataLayer.GetSession())
                {
                    Dete d = new Dete()
                    {
                        Ime = "Petar",
                        Prezime = "Petrović",
                        Pol = 'M',
                        DatumRodjenja = new DateTime(2012, 5, 10),
                        Adresa = "Bulevar Oslobođenja 15",
                        TelefonDeteta = "061123456",
                        EmailDeteta = "petar.petrovic@example.com",
                        PosebnePotrebe = "Nema"
                    };

                    s.Save(d);
                    s.Flush();
                    MessageBox.Show($"Novo dete je sačuvano sa ID = {d.ID}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message);
            }
        }

        // 3. Veza dete–roditelj (M:N)
        private void cmdVezaDeteRoditelj_Click(object sender, EventArgs e)
        {
            try
            {
                using (ISession s = DataLayer.GetSession())
                {
                    Dete d = s.Load<Dete>(1);
                    MessageBox.Show($"Dete: {d.Ime} {d.Prezime}");

                    foreach (var r in d.Roditelji)
                    {
                        MessageBox.Show($"Roditelj: {r.Ime} {r.Prezime}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message);
            }
        }

        // 4. Veza dete–povrede (1:N)
        private void cmdVezaDetePovrede_Click(object sender, EventArgs e)
        {
            try
            {
                using (ISession s = DataLayer.GetSession())
                {
                    Dete d = s.Load<Dete>(1);
                    MessageBox.Show($"Dete: {d.Ime} {d.Prezime}");

                    foreach (var p in d.Povrede)
                    {
                        MessageBox.Show($"Povreda ID={p.ID}, opis: {p.Opis}, datum: {p.Datum.ToShortDateString()}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message);
            }
        }

        // 5. Veza dete–aktivnosti (M:N preko Ucestvuje)
        private void cmdVezaDeteAktivnosti_Click(object sender, EventArgs e)
        {
            try
            {
                using (ISession s = DataLayer.GetSession())
                {
                    Dete d = s.Load<Dete>(1);
                    MessageBox.Show($"Dete: {d.Ime} {d.Prezime}");

                    foreach (var u in d.Ucestvuje)
                    {
                        MessageBox.Show($"Aktivnost: {u.Aktivnost?.Naziv}, Prisustvo: {u.Prisustvo}, Ocena: {u.OcenaAktivnosti}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message);
            }
        }

        // 6. Veza dete–prijava (1:N)
        private void cmdVezaDetePrijave_Click(object sender, EventArgs e)
        {
            try
            {
                using (ISession s = DataLayer.GetSession())
                {
                    Dete d = s.Load<Dete>(1);
                    MessageBox.Show($"Dete: {d.Ime} {d.Prezime}");

                    foreach (var p in d.Prijave)
                    {
                        MessageBox.Show($"Prijava ID={p.IdPrijave}, datum: {p.DatumPrijave.ToShortDateString()}, status: {p.Status}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message);
            }
        }

        // 7. Veza prijava–dete (N:1)
        private void cmdVezaPrijavaDete_Click(object sender, EventArgs e)
        {
            try
            {
                using (ISession s = DataLayer.GetSession())
                {
                    Prijava p = s.Load<Prijava>(1);
                    MessageBox.Show($"Prijava ID={p.IdPrijave}, status: {p.Status}");

                    if (p.Dete != null)
                        MessageBox.Show($"Ova prijava pripada detetu: {p.Dete.Ime} {p.Dete.Prezime}");
                    else
                        MessageBox.Show("Ova prijava nema povezano dete!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message);
            }
        }
    }
}
