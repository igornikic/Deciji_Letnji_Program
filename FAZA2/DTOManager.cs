using Deciji_Letnji_Program.Entiteti;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program
{
    internal class DTOManager
    {

        #region Dete

        public static async Task<List<DetePregled>> GetAllDecaAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var deca = await session.Query<Dete>()
                        .Select(d => new DetePregled(
                            d.ID,
                            d.Ime,
                            d.Prezime,
                            d.DatumRodjenja,
                            d.Pol))
                        .ToListAsync();

                    return deca;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Doslo je do greske prilikom ucitavanja dece: " + ex.Message, ex);
            }
        }

        public static async Task<DeteBasic> GetDeteAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var dete = await session.GetAsync<Dete>(id);

                    if (dete == null)
                        return null;

                    DeteBasic db = new DeteBasic
                    {
                        Id = dete.ID,
                        Ime = dete.Ime,
                        Prezime = dete.Prezime,
                        DatumRodjenja = dete.DatumRodjenja,
                        Pol = dete.Pol,
                        Adresa = dete.Adresa,
                        TelefonDeteta = dete.TelefonDeteta,
                        EmailDeteta = dete.EmailDeteta,
                        PosebnePotrebe = dete.PosebnePotrebe,
                    };

                    return db;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Doslo je do greske prilikom ucitavanja deteta: " + ex.Message, ex);
            }
        }

        public static async Task AddDeteAsync(DeteBasic dete)
        {
            try
            {
                // Validate non-nullable fields and phone/email format
                if (string.IsNullOrEmpty(dete.Ime) || string.IsNullOrEmpty(dete.Prezime) || dete.DatumRodjenja == null || dete.Pol == '\0')
                    throw new Exception("Ime, Prezime, Datum Rodjenja, and Pol are required fields.");


                using (ISession session = DataLayer.GetSession())
                {
                    Dete novoDete = new Dete
                    {
                        Ime = dete.Ime,
                        Prezime = dete.Prezime,
                        DatumRodjenja = dete.DatumRodjenja,
                        Pol = dete.Pol,
                        Adresa = dete.Adresa,
                        TelefonDeteta = dete.TelefonDeteta,
                        EmailDeteta = dete.EmailDeteta,
                        PosebnePotrebe = dete.PosebnePotrebe
                    };

                    await session.SaveAsync(novoDete);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Doslo je do greske prilikom dodavanja deteta: " + ex.Message, ex);
            }
        }

        public static async Task UpdateDeteAsync(DeteBasic dete)
        {
            try
            {
                // Validacija non-nullable
                if (string.IsNullOrEmpty(dete.Ime) || string.IsNullOrEmpty(dete.Prezime) || dete.DatumRodjenja == null || dete.Pol == '\0')
                    throw new Exception("Ime, Prezime, Datum Rodjenja, and Pol are required fields.");

                // Telefon validacija – dozvoljeno prazno ili validan format
                if (!string.IsNullOrWhiteSpace(dete.TelefonDeteta) &&
                    !Regex.IsMatch(dete.TelefonDeteta, @"^[+0-9 -]{6,20}$"))
                {
                    throw new Exception("Format telefona nije ispravan. Dozvoljeni su brojevi, plus, razmak i crtica.");
                }

                // Email validacija – dozvoljeno prazno ili validan format
                if (!string.IsNullOrWhiteSpace(dete.EmailDeteta) &&
                    !Regex.IsMatch(dete.EmailDeteta, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                {
                    throw new Exception("Format email adrese nije ispravan.");
                }


                using (ISession session = DataLayer.GetSession())
                {
                    var postojeci = await session.GetAsync<Dete>(dete.Id);
                    if (postojeci == null)
                        throw new Exception("Dete ne postoji u bazi.");

                    postojeci.Ime = dete.Ime;
                    postojeci.Prezime = dete.Prezime;
                    postojeci.DatumRodjenja = dete.DatumRodjenja;
                    postojeci.Pol = dete.Pol;
                    postojeci.Adresa = dete.Adresa;
                    postojeci.TelefonDeteta = dete.TelefonDeteta;
                    postojeci.EmailDeteta = dete.EmailDeteta;
                    postojeci.PosebnePotrebe = dete.PosebnePotrebe;

                    await session.UpdateAsync(postojeci);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Doslo je do greske prilikom azuriranja deteta: " + ex.Message, ex);
            }
        }

        public static async Task DeleteDeteAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var dete = await session.GetAsync<Dete>(id);

                    if (dete == null)
                        throw new Exception("Dete sa zadatim ID-jem ne postoji.");

                    await session.DeleteAsync(dete);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Doslo je do greske prilikom brisanja deteta: " + ex.Message, ex);
            }
        }

        public static async Task<List<TelefonRoditeljaPregled>> GetTelefoniRoditeljaAsync(int roditeljId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var roditelj = await session.GetAsync<Roditelj>(roditeljId);
                    if (roditelj == null)
                        throw new Exception("Roditelj ne postoji.");

                    var telefoni = roditelj.Deca
                        .SelectMany(d => d.TelefoniRoditelja)
                        .Select(t => new TelefonRoditeljaPregled(
                            t.ID,
                            t.Telefon
                        ))
                        .ToList();

                    return telefoni;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Greška prilikom učitavanja telefona roditelja: " + ex.Message, ex);
            }
        }



        public static async Task<List<EmailRoditeljaPregled>> GetEmailoviRoditeljaAsync(int roditeljId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var roditelj = await session.GetAsync<Roditelj>(roditeljId);
                    if (roditelj == null)
                        throw new Exception("Roditelj ne postoji.");

                    var emailovi = roditelj.Deca
                        .SelectMany(d => d.EmailoviRoditelja)
                        .Select(e => new EmailRoditeljaPregled(
                            e.ID,
                            e.Email
                        ))
                        .ToList();

                    return emailovi;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Greška prilikom učitavanja emailova roditelja: " + ex.Message, ex);
            }
        }
        public static async Task<List<AktivnostPregled>> GetAktivnostiZaDeteAsync(int deteId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var dete = await session.GetAsync<Dete>(deteId);

                    if (dete == null)
                        throw new Exception("Dete sa zadatim ID-jem ne postoji.");

                    var aktivnosti = dete.Ucestvuje
                        .Select(u => u.Aktivnost)
                        .Select(a => new AktivnostPregled(
                            a.IdAktivnosti,
                            a.Tip,
                            a.Naziv,
                            a.Datum
                        ))
                        .ToList();

                    return aktivnosti;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja aktivnosti za dete: " + ex.Message, ex);
            }
        }

        public static async Task<List<DetePregled>> GetDecaNaAktivnostiAsync(int aktivnostId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var deca = await session.Query<Prijava>()
                        .Where(p => p.Aktivnost.IdAktivnosti == aktivnostId && p.Status == "odobreno")
                        .Select(p => new DetePregled(
                            p.Dete.ID,
                            p.Dete.Ime,
                            p.Dete.Prezime,
                            p.Dete.DatumRodjenja,
                            p.Dete.Pol
                        ))
                        .ToListAsync();

                    return deca;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Greška prilikom dohvatanja dece za aktivnost: " + ex.Message, ex);
            }
        }


        #endregion

        #region Starateljstvo
        public static async Task DodajStarateljstvoAsync(int deteId, int roditeljId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var dete = await session.GetAsync<Dete>(deteId);
                    var roditelj = await session.GetAsync<Roditelj>(roditeljId);

                    if (dete == null || roditelj == null)
                        throw new Exception("Dete ili roditelj ne postoje.");

                    // Pošto je Roditelj vlasnik veze (nije inverse), dodajemo dete u roditelja
                    if (!roditelj.Deca.Contains(dete))
                    {
                        roditelj.Deca.Add(dete);
                    }

                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Greška prilikom povezivanja deteta sa roditeljem: " + ex.Message, ex);
            }
        }

        public static async Task<List<DetePregled>> GetDecaZaDodavanjeStarateljstvaAsync(int roditeljId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var roditelj = await session.GetAsync<Roditelj>(roditeljId);
                    if (roditelj == null)
                        throw new Exception("Roditelj ne postoji.");

                    var svaDeca = await session.Query<Dete>().ToListAsync();

                    // Filtriramo decu koja vec nisu pod starateljstvom tog roditelja
                    var dostupnaDeca = svaDeca
                        .Where(d => !roditelj.Deca.Contains(d))
                        .Select(d => new DetePregled(
                            d.ID,
                            d.Ime,
                            d.Prezime,
                            d.DatumRodjenja,
                            d.Pol))
                        .ToList();

                    return dostupnaDeca;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Greška prilikom učitavanja dostupne dece za starateljstvo: " + ex.Message, ex);
            }
        }



        #endregion

        #region Roditelj

        public static async Task<List<RoditeljPregled>> GetAllRoditeljiAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var roditelji = await session.Query<Roditelj>()
                        .Select(r => new RoditeljPregled(
                            r.ID,
                            r.Ime,
                            r.Prezime))
                        .ToListAsync();

                    return roditelji;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja roditelja: " + ex.Message, ex);
            }
        }

        public static async Task<RoditeljBasic> GetRoditeljAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var roditelj = await session.GetAsync<Roditelj>(id);

                    if (roditelj == null)
                        return null;

                    RoditeljBasic rb = new RoditeljBasic
                    {
                        Id = roditelj.ID,
                        Ime = roditelj.Ime,
                        Prezime = roditelj.Prezime,
                        //Deca = roditelj.Deca.Select(d => new DeteBasic
                        //{
                        //    Id = d.ID,
                        //    Ime = d.Ime,
                        //    Prezime = d.Prezime,
                        //    DatumRodjenja = d.DatumRodjenja,
                        //    Pol = d.Pol,
                        //    Adresa = d.Adresa,
                        //    TelefonDeteta = d.TelefonDeteta,
                        //    EmailDeteta = d.EmailDeteta,
                        //    PosebnePotrebe = d.PosebnePotrebe
                        //}).ToList(),
                    };

                    return rb;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja roditelja: " + ex.Message, ex);
            }
        }

        public static async Task AddRoditeljAsync(RoditeljBasic roditelj)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    Roditelj noviRoditelj = new Roditelj
                    {
                        Ime = roditelj.Ime,
                        Prezime = roditelj.Prezime,
                    };

                    await session.SaveAsync(noviRoditelj);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom dodavanja roditelja: " + ex.Message, ex);
            }
        }

        public static async Task UpdateRoditeljAsync(RoditeljBasic roditelj)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var postojeciRoditelj = await session.GetAsync<Roditelj>(roditelj.Id);
                    if (postojeciRoditelj == null)
                        throw new Exception("Roditelj ne postoji u bazi.");

                    postojeciRoditelj.Ime = roditelj.Ime;
                    postojeciRoditelj.Prezime = roditelj.Prezime;

                    await session.UpdateAsync(postojeciRoditelj);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom ažuriranja roditelja: " + ex.Message, ex);
            }
        }

        public static async Task DeleteRoditeljAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var roditelj = await session.GetAsync<Roditelj>(id);

                    if (roditelj == null)
                        throw new Exception("Roditelj sa zadatim ID-jem ne postoji.");

                    await session.DeleteAsync(roditelj);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom brisanja roditelja: " + ex.Message, ex);
            }
        }

        #endregion

        #region Prijava

        public static async Task<List<RoditeljPregled>> GetRoditeljiZaAktivnostAsync(int aktivnostId)
        {
            using (ISession session = DataLayer.GetSession())
            {
                // Za roditelje ne filtriramo posebno po aktivnosti, vraćamo sve
                // jer kasnije dete ograničava izbor
                var roditelji = await session.Query<Roditelj>()
                    .Select(r => new RoditeljPregled(r.ID, r.Ime, r.Prezime))
                    .ToListAsync();

                return roditelji;
            }
        }

        public static async Task<List<DetePregled>> GetDecaZaRoditeljaIAktivnostAsync(int roditeljId, int aktivnostId)
        {
            using (ISession session = DataLayer.GetSession())
            {
                var aktivnost = await session.GetAsync<Aktivnost>(aktivnostId);
                if (aktivnost == null) throw new Exception("Aktivnost ne postoji.");

                var roditelj = await session.GetAsync<Roditelj>(roditeljId);
                if (roditelj == null) throw new Exception("Roditelj ne postoji.");

                // Starosna grupa
                var starosna = aktivnost.StarosnaGrupa.Split('-');
                int minGod = int.Parse(starosna[0]);
                int maxGod = int.Parse(starosna[1]);
                var danas = DateTime.Now;

                var deca = roditelj.Deca
                    .Where(d =>
                    {
                        int godine = danas.Year - d.DatumRodjenja.Year;
                        if (d.DatumRodjenja.Date > danas.AddYears(-godine)) godine--;

                        // starosna provera
                        bool okGodine = godine >= minGod && godine <= maxGod;
                        bool okOgranicenja = string.IsNullOrEmpty(aktivnost.Ogranicenja)
                                             || string.IsNullOrEmpty(d.PosebnePotrebe)
                                             || !aktivnost.Ogranicenja.ToLower().Contains(d.PosebnePotrebe.ToLower());

                        return okGodine && okOgranicenja;

                    })
                    .Select(d => new DetePregled(d.ID, d.Ime, d.Prezime, d.DatumRodjenja, d.Pol))
                    .ToList();

                return deca;
            }
        }

        public static async Task<bool> MozePrijavaAsync(int aktivnostId)
        {
            using (ISession session = DataLayer.GetSession())
            {
                var aktivnost = await session.GetAsync<Aktivnost>(aktivnostId);
                if (aktivnost == null) throw new Exception("Aktivnost ne postoji.");

                int brojPrijava = await session.Query<Prijava>()
                    .Where(p => p.Aktivnost.IdAktivnosti == aktivnostId)
                    .CountAsync();

                return brojPrijava < aktivnost.MaxUcesnika;
            }
        }

        public static async Task<List<PrijavaPregled>> GetAllPrijaveAsync()
    {
        try
        {
            using (ISession session = DataLayer.GetSession())
            {
                var prijave = await session.Query<Prijava>()
                    .Select(p => new PrijavaPregled(
                        p.IdPrijave,
                        p.DatumPrijave,
                        p.Status
                    ))
                    .ToListAsync();

                return prijave;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Došlo je do greške prilikom učitavanja prijava: " + ex.Message, ex);
        }
    }

    public static async Task<PrijavaBasic> GetPrijavaAsync(int id)
    {
        try
        {
            using (ISession session = DataLayer.GetSession())
            {
                var prijava = await session.GetAsync<Prijava>(id);
                if (prijava == null)
                    return null;

                return new PrijavaBasic
                {
                    IdPrijave = prijava.IdPrijave,
                    DatumPrijave = prijava.DatumPrijave,
                    Status = prijava.Status,
                    Aktivnost = new AktivnostBasic
                    {
                        Id = prijava.Aktivnost.IdAktivnosti,
                        Naziv = prijava.Aktivnost.Naziv,
                        StarosnaGrupa = prijava.Aktivnost.StarosnaGrupa,
                        MaxUcesnika = prijava.Aktivnost.MaxUcesnika,
                        Ogranicenja = prijava.Aktivnost.Ogranicenja
                    },
                    Roditelj = new RoditeljBasic
                    {
                        Id = prijava.Roditelj.ID,
                        Ime = prijava.Roditelj.Ime,
                        Prezime = prijava.Roditelj.Prezime
                    },
                    Dete = new DeteBasic
                    {
                        Id = prijava.Dete.ID,
                        Ime = prijava.Dete.Ime,
                        Prezime = prijava.Dete.Prezime,
                        DatumRodjenja = prijava.Dete.DatumRodjenja,
                        PosebnePotrebe = prijava.Dete.PosebnePotrebe
                    }
                };
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Došlo je do greške prilikom učitavanja prijave: " + ex.Message, ex);
        }
    }

        public static async Task AddPrijavaAsync(int aktivnostId, int roditeljId, int deteId, DateTime datum, string status)
        {
            using (ISession session = DataLayer.GetSession())
            {
                var aktivnost = await session.LoadAsync<Aktivnost>(aktivnostId);
                var roditelj = await session.LoadAsync<Roditelj>(roditeljId);
                var dete = await session.LoadAsync<Dete>(deteId);

                // Bez obzira šta UI pošalje — status je uvek "na čekanju"
                status = "na čekanju";

                Prijava novaPrijava = new Prijava
                {
                    DatumPrijave = datum,
                    Status = status,
                    Aktivnost = aktivnost,
                    Roditelj = roditelj,
                    Dete = dete
                };

                await session.SaveAsync(novaPrijava);
                await session.FlushAsync();
            }
        }



        public static async Task UpdatePrijavaAsync(PrijavaBasic prijava)
    {
        try
        {
            using (ISession session = DataLayer.GetSession())
            using (ITransaction tx = session.BeginTransaction())
            {
                var postojeca = await session.GetAsync<Prijava>(prijava.IdPrijave);
                if (postojeca == null)
                    throw new Exception("Prijava ne postoji u bazi.");

                postojeca.DatumPrijave = prijava.DatumPrijave;
                postojeca.Status = prijava.Status;

                if (prijava.Aktivnost != null)
                    postojeca.Aktivnost = await session.LoadAsync<Aktivnost>(prijava.Aktivnost.Id);
                if (prijava.Roditelj != null)
                    postojeca.Roditelj = await session.LoadAsync<Roditelj>(prijava.Roditelj.Id);
                if (prijava.Dete != null)
                    postojeca.Dete = await session.LoadAsync<Dete>(prijava.Dete.Id);

                await session.UpdateAsync(postojeca);
                await tx.CommitAsync();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Došlo je do greške prilikom ažuriranja prijave: " + ex.Message, ex);
        }
    }

    public static async Task DeletePrijavaAsync(int id)
    {
        try
        {
            using (ISession session = DataLayer.GetSession())
            using (ITransaction tx = session.BeginTransaction())
            {
                var prijava = await session.GetAsync<Prijava>(id);
                if (prijava == null)
                    throw new Exception("Prijava sa zadatim ID-jem ne postoji.");

                await session.DeleteAsync(prijava);
                await tx.CommitAsync();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Došlo je do greške prilikom brisanja prijave: " + ex.Message, ex);
        }
    }

    /// <summary>
    /// Broj prijava za konkretnu aktivnost — koristi se za proveru max učesnika.
    /// </summary>
    public static async Task<int> GetBrojPrijavaZaAktivnostAsync(int aktivnostId)
    {
        using (ISession session = DataLayer.GetSession())
        {
            return await session.Query<Prijava>()
                .Where(p => p.Aktivnost.IdAktivnosti == aktivnostId)
                .CountAsync();
        }
    }

    #endregion

    #region Povreda

    public static async Task<List<PovredaPregled>> GetAllPovredeAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var povrede = await session.Query<Povreda>()
                        .Select(p => new PovredaPregled(
                            p.ID,
                            p.Datum,
                            p.Opis))
                        .ToListAsync();

                    return povrede;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja povreda: " + ex.Message, ex);
            }
        }

        public static async Task<PovredaBasic> GetPovredaAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var povreda = await session.GetAsync<Povreda>(id);

                    if (povreda == null)
                        return null;

                    PovredaBasic pb = new PovredaBasic
                    {
                        Id = povreda.ID,
                        Datum = povreda.Datum,
                        Opis = povreda.Opis,
                        PreduzeteMere = povreda.PreduzeteMere,
                        // Opcionalno učitavanje povezanih entiteta:
                        //Dete = povreda.Dete != null ? new DeteBasic
                        //{
                        //    Id = povreda.Dete.ID,
                        //    Ime = povreda.Dete.Ime,
                        //    Prezime = povreda.Dete.Prezime
                        //} : null,

                        //Aktivnost = povreda.Aktivnost != null ? new AktivnostBasic
                        //{
                        //    Id = povreda.Aktivnost.ID,
                        //    Naziv = povreda.Aktivnost.Naziv
                        //} : null,

                        //OdgovornoOsoblje = povreda.OdgovornoLice != null ? new AngazovanoLiceBasic
                        //{
                        //    JMBG = povreda.OdgovornoLice.JMBG,
                        //    Ime = povreda.OdgovornoLice.Ime,
                        //    Prezime = povreda.OdgovornoLice.Prezime
                        //} : null
                    };

                    return pb;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja povrede: " + ex.Message, ex);
            }
        }

        public static async Task AddPovredaAsync(PovredaBasic povreda)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    Povreda novaPovreda = new Povreda
                    {
                        Datum = povreda.Datum,
                        Opis = povreda.Opis,
                        PreduzeteMere = povreda.PreduzeteMere,
                        Dete = await session.GetAsync<Dete>(povreda.Dete?.Id),
                        Aktivnost = await session.GetAsync<Aktivnost>(povreda.Aktivnost?.Id),
                        OdgovornoOsoblje = await session.GetAsync<AngazovanoLice>(povreda.OdgovornoOsoblje?.JMBG)
                    };

                    await session.SaveAsync(novaPovreda);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom dodavanja povrede: " + ex.Message, ex);
            }
        }

        public static async Task UpdatePovredaAsync(PovredaBasic povreda)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var postojeca = await session.GetAsync<Povreda>(povreda.Id);
                    if (postojeca == null)
                        throw new Exception("Povreda ne postoji u bazi.");

                    postojeca.Datum = povreda.Datum;
                    postojeca.Opis = povreda.Opis;
                    postojeca.PreduzeteMere = povreda.PreduzeteMere;

                    if (povreda.Dete != null)
                        postojeca.Dete = await session.GetAsync<Dete>(povreda.Dete.Id);
                    if (povreda.Aktivnost != null)
                        postojeca.Aktivnost = await session.GetAsync<Aktivnost>(povreda.Aktivnost.Id);
                    if (povreda.OdgovornoOsoblje != null)
                        postojeca.OdgovornoOsoblje = await session.GetAsync<AngazovanoLice>(povreda.OdgovornoOsoblje.JMBG);

                    await session.UpdateAsync(postojeca);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom ažuriranja povrede: " + ex.Message, ex);
            }
        }

        public static async Task DeletePovredaAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var povreda = await session.GetAsync<Povreda>(id);

                    if (povreda == null)
                        throw new Exception("Povreda sa zadatim ID-jem ne postoji.");

                    await session.DeleteAsync(povreda);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom brisanja povrede: " + ex.Message, ex);
            }
        }

        #endregion

        #region Aktivnost

        public static async Task<List<AktivnostPregled>> GetAllAktivnostiAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var aktivnosti = await session.Query<Aktivnost>()
                        .Select(a => new AktivnostPregled(
                            a.IdAktivnosti,
                            a.Tip,
                            a.Naziv,
                            a.Datum))
                        .ToListAsync();

                    return aktivnosti;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja aktivnosti: " + ex.Message, ex);
            }
        }

        public static async Task<AktivnostBasic> GetAktivnostAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var aktivnost = await session.GetAsync<Aktivnost>(id);

                    if (aktivnost == null)
                        return null;

                    AktivnostBasic ab = new AktivnostBasic
                    {
                        Id = aktivnost.IdAktivnosti,
                        Tip = aktivnost.Tip,
                        Naziv = aktivnost.Naziv,
                        Datum = aktivnost.Datum,
                        StarosnaGrupa = aktivnost.StarosnaGrupa,
                        MaxUcesnika = aktivnost.MaxUcesnika,
                        Ogranicenja = aktivnost.Ogranicenja,
                        PrevoznoSredstvo = aktivnost.PrevoznoSredstvo,
                        PlanPuta = aktivnost.PlanPuta,
                        PotrebnaOprema = aktivnost.PotrebnaOprema,
                        Vodic = aktivnost.Vodic,
                        Sport = aktivnost.Sport,
                        PosebnaOprema = aktivnost.PosebnaOprema,
                        //Lokacija = aktivnost.Lokacija != null ? new LokacijaBasic
                        //{
                        //    Naziv = aktivnost.Lokacija.Naziv,
                        //    Tip = aktivnost.Lokacija.Tip,
                        //    Adresa = aktivnost.Lokacija.Adresa,
                        //    Kapacitet = aktivnost.Lokacija.Kapacitet,
                        //    DostupnaOprema = aktivnost.Lokacija.DostupnaOprema
                        //} : null,
                        //Evaluacija = aktivnost.Evaluacija != null ? new EvaluacijaBasic
                        //{
                        //    Id = aktivnost.Evaluacija.ID,
                        //    Ocena = aktivnost.Evaluacija.Ocena,
                        //    Datum = aktivnost.Evaluacija.Datum,
                        //    Opis = aktivnost.Evaluacija.Opis
                        //} : null
                    };

                    return ab;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja aktivnosti: " + ex.Message, ex);
            }
        }

        public static async Task AddAktivnostAsync(AktivnostBasic aktivnost)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var nova = new Aktivnost
                    {
                        Tip = aktivnost.Tip,
                        Naziv = aktivnost.Naziv,
                        Datum = aktivnost.Datum,
                        StarosnaGrupa = aktivnost.StarosnaGrupa,
                        MaxUcesnika = aktivnost.MaxUcesnika,
                        Ogranicenja = aktivnost.Ogranicenja,
                        PrevoznoSredstvo = aktivnost.PrevoznoSredstvo,
                        PlanPuta = aktivnost.PlanPuta,
                        PotrebnaOprema = aktivnost.PotrebnaOprema,
                        Vodic = aktivnost.Vodic,
                        Sport = aktivnost.Sport,
                        PosebnaOprema = aktivnost.PosebnaOprema
                    };

                    if (aktivnost.Lokacija != null)
                    {
                        nova.Lokacija = await session.LoadAsync<Lokacija>(aktivnost.Lokacija.Naziv);
                    }

                    if (aktivnost.Evaluacija != null)
                    {
                        var eval = new Evaluacija
                        {
                            Ocena = aktivnost.Evaluacija.Ocena,
                            Datum = aktivnost.Evaluacija.Datum,
                            Opis = aktivnost.Evaluacija.Opis,
                            Aktivnost = nova
                        };

                        nova.Evaluacija = eval;
                    }

                    await session.SaveAsync(nova);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom dodavanja aktivnosti: " + ex.Message, ex);
            }
        }

        public static async Task UpdateAktivnostAsync(AktivnostBasic aktivnost)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var postojeca = await session.GetAsync<Aktivnost>(aktivnost.Id);

                    if (postojeca == null)
                        throw new Exception("Aktivnost ne postoji.");

                    postojeca.Tip = aktivnost.Tip;
                    postojeca.Naziv = aktivnost.Naziv;
                    postojeca.Datum = aktivnost.Datum;
                    postojeca.StarosnaGrupa = aktivnost.StarosnaGrupa;
                    postojeca.MaxUcesnika = aktivnost.MaxUcesnika;
                    postojeca.Ogranicenja = aktivnost.Ogranicenja;
                    postojeca.PrevoznoSredstvo = aktivnost.PrevoznoSredstvo;
                    postojeca.PlanPuta = aktivnost.PlanPuta;
                    postojeca.PotrebnaOprema = aktivnost.PotrebnaOprema;
                    postojeca.Vodic = aktivnost.Vodic;
                    postojeca.Sport = aktivnost.Sport;
                    postojeca.PosebnaOprema = aktivnost.PosebnaOprema;

                    if (aktivnost.Lokacija != null)
                    {
                        postojeca.Lokacija = await session.LoadAsync<Lokacija>(aktivnost.Lokacija.Naziv);
                    }

                    await session.UpdateAsync(postojeca);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom ažuriranja aktivnosti: " + ex.Message, ex);
            }
        }

        public static async Task DeleteAktivnostAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var aktivnost = await session.GetAsync<Aktivnost>(id);

                    if (aktivnost == null)
                        throw new Exception("Aktivnost sa datim ID-jem ne postoji.");

                    await session.DeleteAsync(aktivnost);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom brisanja aktivnosti: " + ex.Message, ex);
            }
        }

        #endregion

        #region Evaluacija

        public static async Task<List<EvaluacijaPregled>> GetAllEvaluacijeAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var evaluacije = await session.Query<Evaluacija>()
                        .Select(e => new EvaluacijaPregled(
                            e.ID,
                            e.Ocena,
                            e.Datum,
                            e.Opis))
                        .ToListAsync();

                    return evaluacije;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja evaluacija: " + ex.Message, ex);
            }
        }

        public static async Task<EvaluacijaBasic> GetEvaluacijaAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var evaluacija = await session.GetAsync<Evaluacija>(id);
                    if (evaluacija == null)
                        return null;

                    EvaluacijaBasic eb = new EvaluacijaBasic
                    {
                        Id = evaluacija.ID,
                        Ocena = evaluacija.Ocena,
                        Datum = evaluacija.Datum,
                        Opis = evaluacija.Opis,
                        // Opcionalno: možete učitati i osnovne podatke o aktivnosti i angažovanom licu ako treba
                        //Aktivnost = evaluacija.Aktivnost != null
                        //    ? new AktivnostBasic
                        //    {
                        //        Id = evaluacija.Aktivnost.IdAktivnosti,
                        //        Naziv = evaluacija.Aktivnost.Naziv
                        //    }
                        //    : null,

                        //AngazovanoLice = evaluacija.AngazovanoLice != null
                        //    ? new AngazovanoLiceBasic
                        //    {
                        //        JMBG = evaluacija.AngazovanoLice.JMBG,
                        //        Ime = evaluacija.AngazovanoLice.Ime,
                        //        Prezime = evaluacija.AngazovanoLice.Prezime
                        //    }
                        //    : null
                    };

                    return eb;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja evaluacije: " + ex.Message, ex);
            }
        }

        public static async Task AddEvaluacijaAsync(EvaluacijaBasic evaluacija)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    Evaluacija novaEvaluacija = new Evaluacija
                    {
                        Ocena = evaluacija.Ocena,
                        Datum = evaluacija.Datum,
                        Opis = evaluacija.Opis
                    };

                    // Veze prema drugim entitetima (ako postoje)
                    if (evaluacija.Aktivnost != null)
                        novaEvaluacija.Aktivnost = await session.LoadAsync<Aktivnost>(evaluacija.Aktivnost.Id);

                    if (evaluacija.AngazovanoLice != null)
                        novaEvaluacija.AngazovanoLice = await session.LoadAsync<AngazovanoLice>(evaluacija.AngazovanoLice.JMBG);

                    await session.SaveAsync(novaEvaluacija);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom dodavanja evaluacije: " + ex.Message, ex);
            }
        }

        public static async Task UpdateEvaluacijaAsync(EvaluacijaBasic evaluacija)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var postojecaEvaluacija = await session.GetAsync<Evaluacija>(evaluacija.Id);
                    if (postojecaEvaluacija == null)
                        throw new Exception("Evaluacija ne postoji u bazi.");

                    postojecaEvaluacija.Ocena = evaluacija.Ocena;
                    postojecaEvaluacija.Datum = evaluacija.Datum;
                    postojecaEvaluacija.Opis = evaluacija.Opis;

                    if (evaluacija.Aktivnost != null)
                        postojecaEvaluacija.Aktivnost = await session.LoadAsync<Aktivnost>(evaluacija.Aktivnost.Id);

                    if (evaluacija.AngazovanoLice != null)
                        postojecaEvaluacija.AngazovanoLice = await session.LoadAsync<AngazovanoLice>(evaluacija.AngazovanoLice.JMBG);

                    await session.UpdateAsync(postojecaEvaluacija);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom ažuriranja evaluacije: " + ex.Message, ex);
            }
        }

        public static async Task DeleteEvaluacijaAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var evaluacija = await session.GetAsync<Evaluacija>(id);
                    if (evaluacija == null)
                        throw new Exception("Evaluacija sa zadatim ID-jem ne postoji.");

                    await session.DeleteAsync(evaluacija);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom brisanja evaluacije: " + ex.Message, ex);
            }
        }

        #endregion

        #region Obrok

        public static async Task<List<ObrokPregled>> GetAllObrociAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var obroci = await session.Query<Obrok>()
                        .Select(o => new ObrokPregled(
                            o.ID,
                            o.Tip,
                            o.Uzrast))
                        .ToListAsync();

                    return obroci;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja obroka: " + ex.Message, ex);
            }
        }

        public static async Task<ObrokBasic> GetObrokAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var obrok = await session.GetAsync<Obrok>(id);

                    if (obrok == null)
                        return null;

                    ObrokBasic ob = new ObrokBasic
                    {
                        Id = obrok.ID,
                        Tip = obrok.Tip,
                        Uzrast = obrok.Uzrast,
                        Jelovnik = obrok.Jelovnik,
                        PosebneOpcije = obrok.PosebneOpcije,
                        //Lokacija = obrok.Lokacija != null ? new LokacijaBasic
                        //{
                        //    Naziv = obrok.Lokacija.Naziv,
                        //    Tip = obrok.Lokacija.Tip,
                        //    Adresa = obrok.Lokacija.Adresa,
                        //    Kapacitet = obrok.Lokacija.Kapacitet,
                        //    DostupnaOprema = obrok.Lokacija.DostupnaOprema
                        //} : null,
                        //Aktivnost = obrok.Aktivnost != null ? new AktivnostBasic
                        //{
                        //    Id = obrok.Aktivnost.IdAktivnosti,
                        //    Tip = obrok.Aktivnost.Tip,
                        //    Naziv = obrok.Aktivnost.Naziv,
                        //    Datum = obrok.Aktivnost.Datum
                        //} : null
                    };

                    return ob;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja obroka: " + ex.Message, ex);
            }
        }

        public static async Task AddObrokAsync(ObrokBasic obrok)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    Obrok noviObrok = new Obrok
                    {
                        Tip = obrok.Tip,
                        Uzrast = obrok.Uzrast,
                        Jelovnik = obrok.Jelovnik,
                        PosebneOpcije = obrok.PosebneOpcije,
                        Lokacija = obrok.Lokacija != null
                            ? await session.GetAsync<Lokacija>(obrok.Lokacija.Naziv)
                            : null,
                        Aktivnost = obrok.Aktivnost != null
                            ? await session.GetAsync<Aktivnost>(obrok.Aktivnost.Id)
                            : null
                    };

                    await session.SaveAsync(noviObrok);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom dodavanja obroka: " + ex.Message, ex);
            }
        }

        public static async Task UpdateObrokAsync(ObrokBasic obrok)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var postojeci = await session.GetAsync<Obrok>(obrok.Id);
                    if (postojeci == null)
                        throw new Exception("Obrok ne postoji u bazi.");

                    postojeci.Tip = obrok.Tip;
                    postojeci.Uzrast = obrok.Uzrast;
                    postojeci.Jelovnik = obrok.Jelovnik;
                    postojeci.PosebneOpcije = obrok.PosebneOpcije;

                    if (obrok.Lokacija != null)
                        postojeci.Lokacija = await session.GetAsync<Lokacija>(obrok.Lokacija.Naziv);

                    if (obrok.Aktivnost != null)
                        postojeci.Aktivnost = await session.GetAsync<Aktivnost>(obrok.Aktivnost.Id);

                    await session.UpdateAsync(postojeci);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom ažuriranja obroka: " + ex.Message, ex);
            }
        }

        public static async Task DeleteObrokAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var obrok = await session.GetAsync<Obrok>(id);
                    if (obrok == null)
                        throw new Exception("Obrok sa zadatim ID-jem ne postoji.");

                    await session.DeleteAsync(obrok);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom brisanja obroka: " + ex.Message, ex);
            }
        }

        #endregion

        #region AngazovanoLice

        public static async Task<List<AngazovanoLicePregled>> GetAllAngazovanaLicaAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var lica = await session.Query<AngazovanoLice>()
                        .Select(al => new AngazovanoLicePregled(
                            al.JMBG,
                            al.Ime,
                            al.Prezime))
                        .ToListAsync();

                    return lica;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja angažovanih lica: " + ex.Message, ex);
            }
        }

        public static async Task<AngazovanoLiceBasic> GetAngazovanoLiceAsync(string jmbg)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var lice = await session.GetAsync<AngazovanoLice>(jmbg);

                    if (lice == null)
                        return null;

                    AngazovanoLiceBasic alb = new AngazovanoLiceBasic
                    {
                        JMBG = lice.JMBG,
                        Ime = lice.Ime,
                        Prezime = lice.Prezime,
                        Pol = lice.Pol,
                        Adresa = lice.Adresa,
                        BrojTelefona = lice.BrojTelefona,
                        Email = lice.Email,
                        StrucnaSprema = lice.StrucnaSprema,
                        Volonter = lice.Volonter,
                        Trener = lice.Trener,
                        Animator = lice.Animator,
                        ZdravstveniRadnik = lice.ZdravstveniRadnik,
                        Evaluacija = lice.Evaluacija != null ? new EvaluacijaBasic
                        {
                            Id = lice.Evaluacija.ID,
                            Ocena = lice.Evaluacija.Ocena,
                            Datum = lice.Evaluacija.Datum,
                            Opis = lice.Evaluacija.Opis
                        } : null
                    };

                    return alb;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja angažovanog lica: " + ex.Message, ex);
            }
        }

        public static async Task AddAngazovanoLiceAsync(AngazovanoLiceBasic lice)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lice.JMBG) || string.IsNullOrWhiteSpace(lice.Ime) || string.IsNullOrWhiteSpace(lice.Prezime))
                    throw new Exception("JMBG, Ime i Prezime su obavezni.");

                using (ISession session = DataLayer.GetSession())
                {
                    var novoLice = new AngazovanoLice
                    {
                        JMBG = lice.JMBG,
                        Ime = lice.Ime,
                        Prezime = lice.Prezime,
                        Pol = lice.Pol,
                        Adresa = lice.Adresa,
                        BrojTelefona = lice.BrojTelefona,
                        Email = lice.Email,
                        StrucnaSprema = lice.StrucnaSprema,
                        Volonter = lice.Volonter,
                        Trener = lice.Trener,
                        Animator = lice.Animator,
                        ZdravstveniRadnik = lice.ZdravstveniRadnik
                    };

                    await session.SaveAsync(novoLice);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom dodavanja angažovanog lica: " + ex.Message, ex);
            }
        }

        public static async Task UpdateAngazovanoLiceAsync(AngazovanoLiceBasic lice)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var postojeci = await session.GetAsync<AngazovanoLice>(lice.JMBG);
                    if (postojeci == null)
                        throw new Exception("Angažovano lice ne postoji u bazi.");

                    postojeci.Ime = lice.Ime;
                    postojeci.Prezime = lice.Prezime;
                    postojeci.Pol = lice.Pol;
                    postojeci.Adresa = lice.Adresa;
                    postojeci.BrojTelefona = lice.BrojTelefona;
                    postojeci.Email = lice.Email;
                    postojeci.StrucnaSprema = lice.StrucnaSprema;
                    postojeci.Volonter = lice.Volonter;
                    postojeci.Trener = lice.Trener;
                    postojeci.Animator = lice.Animator;
                    postojeci.ZdravstveniRadnik = lice.ZdravstveniRadnik;

                    await session.UpdateAsync(postojeci);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom ažuriranja angažovanog lica: " + ex.Message, ex);
            }
        }

        public static async Task DeleteAngazovanoLiceAsync(string jmbg)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var lice = await session.GetAsync<AngazovanoLice>(jmbg);

                    if (lice == null)
                        throw new Exception("Angažovano lice sa zadatim JMBG-om ne postoji.");

                    await session.DeleteAsync(lice);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom brisanja angažovanog lica: " + ex.Message, ex);
            }
        }

        #endregion

        #region Lokacija

        public static async Task<List<LokacijaPregled>> GetAllLokacijeAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var lokacije = await session.Query<Lokacija>()
                        .Select(l => new LokacijaPregled(
                            l.Naziv,
                            l.Tip))
                        .ToListAsync();

                    return lokacije;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja lokacija: " + ex.Message, ex);
            }
        }

        public static async Task<LokacijaBasic> GetLokacijaAsync(string naziv)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var lokacija = await session.GetAsync<Lokacija>(naziv);

                    if (lokacija == null)
                        return null;

                    LokacijaBasic lb = new LokacijaBasic
                    {
                        Naziv = lokacija.Naziv,
                        Tip = lokacija.Tip,
                        Adresa = lokacija.Adresa,
                        Kapacitet = lokacija.Kapacitet,
                        DostupnaOprema = lokacija.DostupnaOprema
                    };

                    return lb;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja lokacije: " + ex.Message, ex);
            }
        }

        public static async Task AddLokacijaAsync(LokacijaBasic lokacija)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    Lokacija novaLokacija = new Lokacija
                    {
                        Naziv = lokacija.Naziv,
                        Tip = lokacija.Tip,
                        Adresa = lokacija.Adresa,
                        Kapacitet = lokacija.Kapacitet,
                        DostupnaOprema = lokacija.DostupnaOprema
                    };

                    await session.SaveAsync(novaLokacija);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom dodavanja lokacije: " + ex.Message, ex);
            }
        }

        public static async Task UpdateLokacijaAsync(LokacijaBasic lokacija)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var postojeca = await session.GetAsync<Lokacija>(lokacija.Naziv);

                    if (postojeca == null)
                        throw new Exception("Lokacija ne postoji u bazi.");

                    postojeca.Tip = lokacija.Tip;
                    postojeca.Adresa = lokacija.Adresa;
                    postojeca.Kapacitet = lokacija.Kapacitet;
                    postojeca.DostupnaOprema = lokacija.DostupnaOprema;

                    await session.UpdateAsync(postojeca);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom ažuriranja lokacije: " + ex.Message, ex);
            }
        }

        public static async Task DeleteLokacijaAsync(string naziv)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var lokacija = await session.GetAsync<Lokacija>(naziv);

                    if (lokacija == null)
                        throw new Exception("Lokacija sa zadatim nazivom ne postoji.");

                    await session.DeleteAsync(lokacija);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom brisanja lokacije: " + ex.Message, ex);
            }
        }

        #endregion

        #region TelefonRoditelja

        public static async Task<List<TelefonRoditeljaPregled>> GetAllTelefoniRoditeljaAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var telefoni = await session.Query<TelefonRoditelja>()
                        .Select(t => new TelefonRoditeljaPregled(
                            t.ID,
                            t.Telefon))
                        .ToListAsync();

                    return telefoni;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja telefona roditelja: " + ex.Message, ex);
            }
        }

        public static async Task<TelefonRoditeljaBasic> GetTelefonRoditeljaAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var telefon = await session.GetAsync<TelefonRoditelja>(id);
                    if (telefon == null)
                        return null;

                    var dto = new TelefonRoditeljaBasic
                    {
                        Id = telefon.ID,
                        Telefon = telefon.Telefon
                    };

                    return dto;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja telefona roditelja: " + ex.Message, ex);
            }
        }

        public static async Task AddTelefonRoditeljaAsync(TelefonRoditeljaBasic telefonDto, int deteId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var dete = await session.GetAsync<Dete>(deteId);
                    if (dete == null)
                        throw new Exception("Dete ne postoji.");

                    TelefonRoditelja noviTelefon = new TelefonRoditelja
                    {
                        Telefon = telefonDto.Telefon,
                        Dete = dete
                    };

                    await session.SaveAsync(noviTelefon);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom dodavanja telefona roditelja: " + ex.Message, ex);
            }
        }

        public static async Task UpdateTelefonRoditeljaAsync(TelefonRoditeljaBasic telefonDto)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var telefon = await session.GetAsync<TelefonRoditelja>(telefonDto.Id);
                    if (telefon == null)
                        throw new Exception("Telefon roditelja ne postoji.");

                    telefon.Telefon = telefonDto.Telefon;

                    await session.UpdateAsync(telefon);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom ažuriranja telefona roditelja: " + ex.Message, ex);
            }
        }

        public static async Task DeleteTelefonRoditeljaAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var telefon = await session.GetAsync<TelefonRoditelja>(id);
                    if (telefon == null)
                        throw new Exception("Telefon roditelja sa zadatim ID-jem ne postoji.");

                    await session.DeleteAsync(telefon);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom brisanja telefona roditelja: " + ex.Message, ex);
            }
        }

        public static async Task<List<TelefonRoditeljaPregled>> GetTelefoniRoditeljaZaDeteAsync(int deteId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var telefoni = await session.Query<TelefonRoditelja>()
                        .Where(t => t.Dete.ID == deteId)
                        .Select(t => new TelefonRoditeljaPregled(
                            t.ID,
                            t.Telefon))
                        .ToListAsync();

                    return telefoni;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja telefona roditelja za dete: " + ex.Message, ex);
            }
        }

        #endregion

        #region EmailRoditelja

        public static async Task<List<EmailRoditeljaPregled>> GetAllEmailoviRoditeljaAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var emailovi = await session.Query<EmailRoditelja>()
                        .Select(e => new EmailRoditeljaPregled(
                            e.ID,
                            e.Email))
                        .ToListAsync();

                    return emailovi;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja emailova roditelja: " + ex.Message, ex);
            }
        }

        public static async Task<EmailRoditeljaBasic> GetEmailRoditeljaAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var email = await session.GetAsync<EmailRoditelja>(id);
                    if (email == null)
                        return null;

                    var dto = new EmailRoditeljaBasic
                    {
                        Id = email.ID,
                        Email = email.Email
                    };

                    return dto;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja emaila roditelja: " + ex.Message, ex);
            }
        }

        public static async Task AddEmailRoditeljaAsync(EmailRoditeljaBasic emailDto, int deteId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var dete = await session.GetAsync<Dete>(deteId);
                    if (dete == null)
                        throw new Exception("Dete ne postoji.");

                    EmailRoditelja noviEmail = new EmailRoditelja
                    {
                        Email = emailDto.Email,
                        Dete = dete
                    };

                    await session.SaveAsync(noviEmail);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom dodavanja emaila roditelja: " + ex.Message, ex);
            }
        }

        public static async Task UpdateEmailRoditeljaAsync(EmailRoditeljaBasic emailDto)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var email = await session.GetAsync<EmailRoditelja>(emailDto.Id);
                    if (email == null)
                        throw new Exception("Email roditelja ne postoji.");

                    email.Email = emailDto.Email;

                    await session.UpdateAsync(email);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom ažuriranja emaila roditelja: " + ex.Message, ex);
            }
        }

        public static async Task DeleteEmailRoditeljaAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var email = await session.GetAsync<EmailRoditelja>(id);
                    if (email == null)
                        throw new Exception("Email roditelja sa zadatim ID-jem ne postoji.");

                    await session.DeleteAsync(email);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom brisanja emaila roditelja: " + ex.Message, ex);
            }
        }

        public static async Task<List<EmailRoditeljaPregled>> GetEmailoviRoditeljaZaDeteAsync(int deteId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var emailovi = await session.Query<EmailRoditelja>()
                        .Where(e => e.Dete.ID == deteId)
                        .Select(e => new EmailRoditeljaPregled(
                            e.ID,
                            e.Email))
                        .ToListAsync();

                    return emailovi;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja emailova roditelja za dete: " + ex.Message, ex);
            }
        }

        #endregion

        #region Ucestvuje

        public static async Task<List<UcestvujePregled>> GetAllUcescaAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var ucesca = await session.Query<Ucestvuje>()
                        .Select(u => new UcestvujePregled(
                            u.ID,
                            u.OcenaAktivnosti.ToString(),
                            u.Komentari))
                        .ToListAsync();

                    return ucesca;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja učešća: " + ex.Message, ex);
            }
        }

        public static async Task<UcestvujeBasic> GetUcesceAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var ucesce = await session.GetAsync<Ucestvuje>(id);

                    if (ucesce == null)
                        return null;

                    var dto = new UcestvujeBasic
                    {
                        ID = ucesce.ID,
                        Prisustvo = ucesce.Prisustvo,
                        OcenaAktivnosti = ucesce.OcenaAktivnosti,
                        Komentari = ucesce.Komentari,
                        Pratilac = ucesce.Pratilac,
                        Dete = new DeteBasic
                        {
                            Id = ucesce.Dete.ID,
                            Ime = ucesce.Dete.Ime,
                            Prezime = ucesce.Dete.Prezime
                        },
                        Roditelj = new RoditeljBasic
                        {
                            Id = ucesce.Roditelj.ID,
                            Ime = ucesce.Roditelj.Ime,
                            Prezime = ucesce.Roditelj.Prezime
                        },
                        Aktivnost = new AktivnostBasic
                        {
                            Id = ucesce.Aktivnost.IdAktivnosti,
                            Naziv = ucesce.Aktivnost.Naziv,
                            Tip = ucesce.Aktivnost.Tip
                        }
                    };

                    return dto;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom učitavanja učešća: " + ex.Message, ex);
            }
        }

        public static async Task AddUcesceAsync(UcestvujeBasic ucesce)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var dete = await session.GetAsync<Dete>(ucesce.Dete.Id);
                    var roditelj = await session.GetAsync<Roditelj>(ucesce.Roditelj.Id);
                    var aktivnost = await session.GetAsync<Aktivnost>(ucesce.Aktivnost.Id);

                    if (dete == null || roditelj == null || aktivnost == null)
                        throw new Exception("Dete, roditelj ili aktivnost nisu pronađeni.");

                    var novoUcesce = new Ucestvuje
                    {
                        Prisustvo = ucesce.Prisustvo,
                        OcenaAktivnosti = ucesce.OcenaAktivnosti,
                        Komentari = ucesce.Komentari,
                        Pratilac = ucesce.Pratilac,
                        Dete = dete,
                        Roditelj = roditelj,
                        Aktivnost = aktivnost
                    };

                    await session.SaveAsync(novoUcesce);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom dodavanja učešća: " + ex.Message, ex);
            }
        }

        public static async Task UpdateUcesceAsync(UcestvujeBasic ucesce)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var postojeci = await session.GetAsync<Ucestvuje>(ucesce.ID);

                    if (postojeci == null)
                        throw new Exception("Učešće nije pronađeno.");

                    postojeci.Prisustvo = ucesce.Prisustvo;
                    postojeci.OcenaAktivnosti = ucesce.OcenaAktivnosti;
                    postojeci.Komentari = ucesce.Komentari;
                    postojeci.Pratilac = ucesce.Pratilac;

                    // Po želji ažurirati i reference
                    if (ucesce.Dete != null)
                        postojeci.Dete = await session.GetAsync<Dete>(ucesce.Dete.Id);
                    if (ucesce.Roditelj != null)
                        postojeci.Roditelj = await session.GetAsync<Roditelj>(ucesce.Roditelj.Id);
                    if (ucesce.Aktivnost != null)
                        postojeci.Aktivnost = await session.GetAsync<Aktivnost>(ucesce.Aktivnost.Id);

                    await session.UpdateAsync(postojeci);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom ažuriranja učešća: " + ex.Message, ex);
            }
        }

        public static async Task DeleteUcesceAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var ucesce = await session.GetAsync<Ucestvuje>(id);

                    if (ucesce == null)
                        throw new Exception("Učešće sa zadatim ID-jem ne postoji.");

                    await session.DeleteAsync(ucesce);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Došlo je do greške prilikom brisanja učešća: " + ex.Message, ex);
            }
        }

        #endregion

    }
}
