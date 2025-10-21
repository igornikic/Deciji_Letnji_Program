using DatabaseAccess;
using Deciji_Letnji_Program.Entiteti;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Deciji_Letnji_Program.DTOs;

namespace Deciji_Letnji_Program
{
    public static class DataProvider
    {

        #region Dete

        public static async Task<Result<List<DetePregled>, ErrorMessage>> GetAllDecaAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var deca = await session.Query<Dete>()
                        .Select(d => new DetePregled
                        {
                            ID = d.ID,
                            Ime = d.Ime,
                            Prezime = d.Prezime,
                            DatumRodjenja = d.DatumRodjenja,
                            Pol = d.Pol,
                            Adresa = d.Adresa,
                            TelefonDeteta = d.TelefonDeteta,
                            EmailDeteta = d.EmailDeteta,
                            PosebnePotrebe = d.PosebnePotrebe
                        })
                        .ToListAsync();

                    if (deca == null || deca.Count == 0)
                        return GetError("Nema unete dece u bazi.", 404);

                    return deca;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja dece: " + ex.Message, 500);
            }
        }
        public static async Task<Result<DetePregled, ErrorMessage>> VratiDeteAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var dete = await session.GetAsync<Dete>(id);

                    if (dete == null)
                        return GetError($"Dete sa ID-em {id} ne postoji.", 404);

                    DetePregled dp = new DetePregled
                    {
                        ID = dete.ID,
                        Ime = dete.Ime,
                        Prezime = dete.Prezime,
                        DatumRodjenja = dete.DatumRodjenja,
                        Pol = dete.Pol,
                        Adresa = dete.Adresa,
                        TelefonDeteta = dete.TelefonDeteta,
                        EmailDeteta = dete.EmailDeteta,
                        PosebnePotrebe = dete.PosebnePotrebe
                    };

                    return dp;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja deteta: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> AddDeteAsync(DetePregled dete)
        {
            try
            {
                // Validate non-nullable fields and phone/email format
                if (string.IsNullOrEmpty(dete.Ime) || string.IsNullOrEmpty(dete.Prezime) || dete.DatumRodjenja == null || dete.Pol == '\0')
                {
                    return GetError("Ime, Prezime, Datum Rodjenja i Pol su obavezna polja.", 400);
                }

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
                return GetError("Doslo je do greske prilikom dodavanja deteta: " + ex.Message, 500 );
            }

            return true; // uspešno dodano
        }

        public static async Task<Result<bool, ErrorMessage>> UpdateDeteAsync(DetePregled dete)
        {
            try
            {
                // Validacija non-nullable
                if (string.IsNullOrEmpty(dete.Ime) || string.IsNullOrEmpty(dete.Prezime) || dete.DatumRodjenja == default || dete.Pol == '\0')
                    return GetError("Ime, Prezime, Datum Rodjenja i Pol su obavezna polja.", 400);

                // Telefon validacija – dozvoljeno prazno ili validan format
                if (!string.IsNullOrWhiteSpace(dete.TelefonDeteta) &&
                    !Regex.IsMatch(dete.TelefonDeteta, @"^[+0-9 -]{6,20}$"))
                {
                    return GetError("Format telefona nije ispravan. Dozvoljeni su brojevi, plus, razmak i crtica.", 400);
                }

                // Email validacija – dozvoljeno prazno ili validan format
                if (!string.IsNullOrWhiteSpace(dete.EmailDeteta) &&
                    !Regex.IsMatch(dete.EmailDeteta, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                {
                    return GetError("Format email adrese nije ispravan.", 400);
                }

                using (ISession session = DataLayer.GetSession())
                {
                    var postojeci = await session.GetAsync<Dete>(dete.ID);
                    if (postojeci == null)
                        return GetError("Dete sa datim ID-em ne postoji.", 404);

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

                return true; // uspešno ažurirano
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom ažuriranja deteta: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> DeleteDeteAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var dete = await session.GetAsync<Dete>(id);

                    if (dete == null)
                        return GetError($"Dete sa ID-em {id} ne postoji.", 404);

                    await session.DeleteAsync(dete);
                    await session.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom brisanja deteta: " + ex.Message, 500);
            }

            return true; // uspešno obrisano
        }

        public static async Task<Result<List<TelefonRoditeljaPregled>, ErrorMessage>> GetTelefoniRoditeljaAsync(int roditeljId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var roditelj = await session.GetAsync<Roditelj>(roditeljId);
                    if (roditelj == null)
                        return GetError($"Roditelj sa ID-em {roditeljId} ne postoji.", 404);

                    var telefoni = roditelj.Deca
                        .SelectMany(d => d.TelefoniRoditelja)
                        .Select(t => new TelefonRoditeljaPregled
                        {
                            Id = t.ID,
                            Telefon = t.Telefon
                        })
                        .ToList();

                    if (telefoni.Count == 0)
                        return GetError("Nema unetih telefona za ovog roditelja.", 404);

                    return telefoni;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja telefona roditelja: " + ex.Message, 500);
            }
        }


        public static async Task<Result<List<EmailRoditeljaPregled>, ErrorMessage>> GetEmailoviRoditeljaAsync(int roditeljId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var roditelj = await session.GetAsync<Roditelj>(roditeljId);
                    if (roditelj == null)
                        return GetError($"Roditelj sa ID-em {roditeljId} ne postoji.", 404);

                    var emailovi = roditelj.Deca
                        .SelectMany(d => d.EmailoviRoditelja)
                        .Select(e => new EmailRoditeljaPregled
                        {
                            Id = e.ID,
                            Email = e.Email
                        })
                        .ToList();

                    if (emailovi.Count == 0)
                        return GetError("Nema unetih emailova za ovog roditelja.", 404);

                    return emailovi;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja emailova roditelja: " + ex.Message, 500);
            }
        }
        //=============================================================================================================================
        //OVO NE RADI LEPO ZBOG TOGA STO NEMAMO KONTRUKTOR U DTOs KOJI PRIMA ENTITETE LOKACIJA I EVALUACIJA IZ BAZE VEC SAMO KOPIRA DTO
        //=============================================================================================================================

        //public static async Task<List<AktivnostPregled>> GetAktivnostiZaDeteAsync(int deteId)
        //{
        //    try
        //    {
        //        using (ISession session = DataLayer.GetSession())
        //        {
        //            var dete = await session.GetAsync<Dete>(deteId);

        //            if (dete == null)
        //                throw new Exception("Dete sa zadatim ID-jem ne postoji.");

        //            var aktivnosti = dete.Ucestvuje
        //                .Select(u => u.Aktivnost)
        //                .Select(a => new AktivnostPregled(
        //                    a.IdAktivnosti,
        //                    a.Tip,
        //                    a.Naziv,
        //                    a.Datum
        //                ))
        //                .ToList();

        //            return aktivnosti;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Došlo je do greške prilikom učitavanja aktivnosti za dete: " + ex.Message, ex);
        //    }
        //}

        public static async Task<Result<List<DetePregled>, ErrorMessage>> GetDecaNaAktivnostiAsync(int aktivnostId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var aktivnost = await session.GetAsync<Aktivnost>(aktivnostId);
                    if (aktivnost == null)
                        return GetError($"Aktivnost sa ID-em {aktivnostId} ne postoji.", 404);

                    var deca = await session.Query<Prijava>()
                        .Where(p => p.Aktivnost.IdAktivnosti == aktivnostId && p.Status == "odobreno")
                        .Select(p => new DetePregled
                        {
                            ID = p.Dete.ID,
                            Ime = p.Dete.Ime,
                            Prezime = p.Dete.Prezime,
                            DatumRodjenja = p.Dete.DatumRodjenja,
                            Pol = p.Dete.Pol,
                            Adresa = p.Dete.Adresa,
                            TelefonDeteta = p.Dete.TelefonDeteta,
                            EmailDeteta = p.Dete.EmailDeteta,
                            PosebnePotrebe = p.Dete.PosebnePotrebe
                        })
                        .ToListAsync();

                    if (deca.Count == 0)
                        return GetError("Nema odobrene dece za ovu aktivnost.", 404);

                    return deca;
                }
            }
            catch (Exception ex)
            {
                return GetError("Greška prilikom dohvatanja dece za aktivnost: " + ex.Message, 500);
            }
        }


        #endregion

        #region Starateljstvo
        public static async Task<Result<bool, ErrorMessage>> DodajStarateljstvoAsync(int deteId, int roditeljId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var dete = await session.GetAsync<Dete>(deteId);
                    if (dete == null)
                        return GetError($"Dete sa ID-em {deteId} ne postoji.", 404);

                    var roditelj = await session.GetAsync<Roditelj>(roditeljId);
                    if (roditelj == null)
                        return GetError($"Roditelj sa ID-em {roditeljId} ne postoji.", 404);

                    // Dodavanje deteta u roditelja, ako veza već ne postoji
                    if (!roditelj.Deca.Contains(dete))
                    {
                        roditelj.Deca.Add(dete);
                    }

                    await session.FlushAsync();
                }

                return true; // uspešno dodato
            }
            catch (Exception ex)
            {
                return GetError("Greška prilikom povezivanja deteta sa roditeljem: " + ex.Message, 500);
            }
        }

        public static async Task<Result<List<DetePregled>, ErrorMessage>> GetDecaZaDodavanjeStarateljstvaAsync(int roditeljId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var roditelj = await session.GetAsync<Roditelj>(roditeljId);
                    if (roditelj == null)
                        return GetError($"Roditelj sa ID-em {roditeljId} ne postoji.", 404);

                    var svaDeca = await session.Query<Dete>().ToListAsync();

                    // Filtriramo decu koja već nisu pod starateljstvom tog roditelja
                    var dostupnaDeca = svaDeca
                        .Where(d => !roditelj.Deca.Contains(d))
                        .Select(d => new DetePregled
                        {
                            ID = d.ID,
                            Ime = d.Ime,
                            Prezime = d.Prezime,
                            DatumRodjenja = d.DatumRodjenja,
                            Pol = d.Pol,
                            Adresa = d.Adresa,
                            TelefonDeteta = d.TelefonDeteta,
                            EmailDeteta = d.EmailDeteta,
                            PosebnePotrebe = d.PosebnePotrebe
                        })
                        .ToList();

                    return dostupnaDeca;
                }
            }
            catch (Exception ex)
            {
                return GetError("Greška prilikom učitavanja dostupne dece za starateljstvo: " + ex.Message, 500);
            }
        }



        #endregion

        #region Roditelj

        public static async Task<Result<List<RoditeljPregled>, ErrorMessage>> GetAllRoditeljiAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var roditelji = await session.Query<Roditelj>()
                        .Select(r => new RoditeljPregled
                        {
                            Id = r.ID,
                            Ime = r.Ime,
                            Prezime = r.Prezime
                            // Deca, Prijave i Ucestvuje ostavljamo prazno za osnovni pregled
                        })
                        .ToListAsync();

                    return roditelji;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja roditelja: " + ex.Message, 500);
            }
        }

        public static async Task<Result<RoditeljPregled, ErrorMessage>> GetRoditeljAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var roditelj = await session.GetAsync<Roditelj>(id);

                    if (roditelj == null)
                        return GetError($"Roditelj sa ID-em {id} ne postoji.", 404);

                    RoditeljPregled rp = new RoditeljPregled
                    {
                        Id = roditelj.ID,
                        Ime = roditelj.Ime,
                        Prezime = roditelj.Prezime,
                        Deca = roditelj.Deca?.Select(d => new DetePregled
                        {
                            ID = d.ID,
                            Ime = d.Ime,
                            Prezime = d.Prezime,
                            DatumRodjenja = d.DatumRodjenja,
                            Pol = d.Pol,
                            Adresa = d.Adresa,
                            TelefonDeteta = d.TelefonDeteta,
                            EmailDeteta = d.EmailDeteta,
                            PosebnePotrebe = d.PosebnePotrebe
                        }).ToList() ?? new List<DetePregled>()
                    };

                    return rp;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja roditelja: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> AddRoditeljAsync(RoditeljPregled roditelj)
        {
            try
            {
                // Validacija obaveznih polja
                if (string.IsNullOrWhiteSpace(roditelj.Ime) || string.IsNullOrWhiteSpace(roditelj.Prezime))
                    return GetError("Ime i Prezime roditelja su obavezna polja.", 400);

                using (ISession session = DataLayer.GetSession())
                {
                    Roditelj noviRoditelj = new Roditelj
                    {
                        Ime = roditelj.Ime,
                        Prezime = roditelj.Prezime
                    };

                    await session.SaveAsync(noviRoditelj);
                    await session.FlushAsync();
                }

                return true; // uspešno dodato
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom dodavanja roditelja: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> UpdateRoditeljAsync(RoditeljPregled roditelj)
        {
            try
            {
                // Validacija obaveznih polja
                if (string.IsNullOrWhiteSpace(roditelj.Ime) || string.IsNullOrWhiteSpace(roditelj.Prezime))
                    return GetError("Ime i Prezime roditelja su obavezna polja.", 400);

                using (ISession session = DataLayer.GetSession())
                {
                    var postojeciRoditelj = await session.GetAsync<Roditelj>(roditelj.Id);
                    if (postojeciRoditelj == null)
                        return GetError($"Roditelj sa ID-em {roditelj.Id} ne postoji.", 404);

                    postojeciRoditelj.Ime = roditelj.Ime;
                    postojeciRoditelj.Prezime = roditelj.Prezime;

                    await session.UpdateAsync(postojeciRoditelj);
                    await session.FlushAsync();
                }

                return true; // uspešno ažurirano
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom ažuriranja roditelja: " + ex.Message, 500);
            }
        }


        public static async Task<Result<bool, ErrorMessage>> DeleteRoditeljAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var roditelj = await session.GetAsync<Roditelj>(id);

                    if (roditelj == null)
                        return GetError($"Roditelj sa ID-em {id} ne postoji.", 404);

                    await session.DeleteAsync(roditelj);
                    await session.FlushAsync();
                }

                return true; // uspešno obrisano
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom brisanja roditelja: " + ex.Message, 500);
            }
        }

        #endregion

        #region Prijava

        public static async Task<Result<List<RoditeljPregled>, ErrorMessage>> GetRoditeljiZaAktivnostAsync(int aktivnostId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    // Proverimo da li aktivnost postoji
                    var aktivnost = await session.GetAsync<Aktivnost>(aktivnostId);
                    if (aktivnost == null)
                        return GetError($"Aktivnost sa ID-em {aktivnostId} ne postoji.", 404);

                    // Vraćamo sve roditelje (kasnije dete ograničava izbor)
                    var roditelji = await session.Query<Roditelj>()
                        .Select(r => new RoditeljPregled
                        {
                            Id = r.ID,
                            Ime = r.Ime,
                            Prezime = r.Prezime
                        })
                        .ToListAsync();

                    return roditelji;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja roditelja za aktivnost: " + ex.Message, 500);
            }
        }

        public static async Task<Result<List<DetePregled>, ErrorMessage>> GetDecaZaRoditeljaIAktivnostAsync(int roditeljId, int aktivnostId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var aktivnost = await session.GetAsync<Aktivnost>(aktivnostId);
                    if (aktivnost == null)
                        return GetError($"Aktivnost sa ID-em {aktivnostId} ne postoji.", 404);

                    var roditelj = await session.GetAsync<Roditelj>(roditeljId);
                    if (roditelj == null)
                        return GetError($"Roditelj sa ID-em {roditeljId} ne postoji.", 404);

                    var starosna = aktivnost.StarosnaGrupa.Split('-');
                    int minGod = int.Parse(starosna[0]);
                    int maxGod = int.Parse(starosna[1]);
                    var danas = DateTime.Now;

                    var deca = roditelj.Deca
                        .Where(d =>
                        {
                            int godine = danas.Year - d.DatumRodjenja.Year;
                            if (d.DatumRodjenja.Date > danas.AddYears(-godine)) godine--;

                            bool okGodine = godine >= minGod && godine <= maxGod;
                            bool okOgranicenja = string.IsNullOrEmpty(aktivnost.Ogranicenja)
                                                 || string.IsNullOrEmpty(d.PosebnePotrebe)
                                                 || !aktivnost.Ogranicenja.ToLower().Contains(d.PosebnePotrebe.ToLower());

                            return okGodine && okOgranicenja;
                        })
                        .Select(d => new DetePregled
                        {
                            ID = d.ID,
                            Ime = d.Ime,
                            Prezime = d.Prezime,
                            DatumRodjenja = d.DatumRodjenja,
                            Pol = d.Pol
                        })
                        .ToList();

                    return deca;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja dece za roditelja i aktivnost: " + ex.Message, 500);
            }
        }
        //====================================================
        //VEROVATNO SE BRISE
        //====================================================
        public static async Task<Result<bool, ErrorMessage>> MozePrijavaAsync(int aktivnostId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var aktivnost = await session.GetAsync<Aktivnost>(aktivnostId);
                    if (aktivnost == null)
                        return GetError($"Aktivnost sa ID-em {aktivnostId} ne postoji.", 404);

                    int brojPrijava = await session.Query<Prijava>()
                        .Where(p => p.Aktivnost.IdAktivnosti == aktivnostId)
                        .CountAsync();

                    bool moze = brojPrijava < aktivnost.MaxUcesnika;
                    return moze;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom provere mogućnosti prijave: " + ex.Message, 500);
            }
        }

        public static async Task<Result<List<PrijavaPregled>, ErrorMessage>> GetAllPrijaveAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var prijave = await session.Query<Prijava>()
                        .Select(p => new PrijavaPregled
                        {
                            IdPrijave = p.IdPrijave,
                            DatumPrijave = p.DatumPrijave,
                            Status = p.Status
                        })
                        .ToListAsync();

                    return prijave;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja prijava: " + ex.Message, 500);
            }
        }

        public static async Task<Result<PrijavaPregled, ErrorMessage>> GetPrijavaAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var prijava = await session.GetAsync<Prijava>(id);
                    if (prijava == null)
                        return GetError($"Prijava sa ID-em {id} ne postoji.", 404);

                    var pp = new PrijavaPregled
                    {
                        IdPrijave = prijava.IdPrijave,
                        DatumPrijave = prijava.DatumPrijave,
                        Status = prijava.Status,
                        Aktivnost = new AktivnostPregled
                        {
                            Id = prijava.Aktivnost.IdAktivnosti,
                            Naziv = prijava.Aktivnost.Naziv,
                            StarosnaGrupa = prijava.Aktivnost.StarosnaGrupa,
                            MaxUcesnika = prijava.Aktivnost.MaxUcesnika,
                            Ogranicenja = prijava.Aktivnost.Ogranicenja,
                            Tip = prijava.Aktivnost.Tip,
                        },
                        Roditelj = new RoditeljPregled
                        {
                            Id = prijava.Roditelj.ID,
                            Ime = prijava.Roditelj.Ime,
                            Prezime = prijava.Roditelj.Prezime
                        },
                        Dete = new DetePregled
                        {
                            ID = prijava.Dete.ID,
                            Ime = prijava.Dete.Ime,
                            Prezime = prijava.Dete.Prezime,
                            DatumRodjenja = prijava.Dete.DatumRodjenja,
                            Pol = prijava.Dete.Pol,
                            Adresa = prijava.Dete.Adresa,
                            TelefonDeteta = prijava.Dete.TelefonDeteta,
                            EmailDeteta = prijava.Dete.EmailDeteta,
                            PosebnePotrebe = prijava.Dete.PosebnePotrebe
                        }
                    };

                    return pp;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja prijave: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> AddPrijavaAsync(int aktivnostId, int roditeljId, int deteId, DateTime datum)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var aktivnost = await session.GetAsync<Aktivnost>(aktivnostId);
                    if (aktivnost == null)
                        return GetError("Aktivnost sa tim ID-em ne postoji.", 404);

                    var roditelj = await session.GetAsync<Roditelj>(roditeljId);
                    if (roditelj == null)
                        return GetError("Roditelj sa tim ID-em ne postoji.", 404);

                    var dete = await session.GetAsync<Dete>(deteId);
                    if (dete == null)
                        return GetError("Dete sa tim ID-em ne postoji.", 404);

                    Prijava novaPrijava = new Prijava
                    {
                        DatumPrijave = datum,
                        Status = "na čekanju", // status je uvek na čekanju
                        Aktivnost = aktivnost,
                        Roditelj = roditelj,
                        Dete = dete
                    };

                    await session.SaveAsync(novaPrijava);
                    await session.FlushAsync();
                }

                return true; // uspešno dodato
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom dodavanja prijave: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> UpdatePrijavaAsync(PrijavaPregled prijava)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                using (ITransaction tx = session.BeginTransaction())
                {
                    var postojeca = await session.GetAsync<Prijava>(prijava.IdPrijave);
                    if (postojeca == null)
                        return GetError("Prijava ne postoji u bazi.", 404);

                    // Ažuriranje polja
                    postojeca.DatumPrijave = prijava.DatumPrijave;
                    postojeca.Status = prijava.Status;

                    // Ažuriranje povezanih entiteta preko ID-a
                    if (prijava.Aktivnost != null)
                    {
                        var aktivnost = await session.GetAsync<Aktivnost>(prijava.Aktivnost.Id);
                        if (aktivnost == null)
                            return GetError("Aktivnost sa zadatim ID-em ne postoji.", 404);
                        postojeca.Aktivnost = aktivnost;
                    }

                    if (prijava.Roditelj != null)
                    {
                        var roditelj = await session.GetAsync<Roditelj>(prijava.Roditelj.Id);
                        if (roditelj == null)
                            return GetError("Roditelj sa zadatim ID-em ne postoji.", 404);
                        postojeca.Roditelj = roditelj;
                    }

                    if (prijava.Dete != null)
                    {
                        var dete = await session.GetAsync<Dete>(prijava.Dete.ID);
                        if (dete == null)
                            return GetError("Dete sa zadatim ID-em ne postoji.", 404);
                        postojeca.Dete = dete;
                    }

                    await session.UpdateAsync(postojeca);
                    await tx.CommitAsync();
                }

                return true; // uspešno ažurirano
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom ažuriranja prijave: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> DeletePrijavaAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                using (ITransaction tx = session.BeginTransaction())
                {
                    var prijava = await session.GetAsync<Prijava>(id);
                    if (prijava == null)
                        return GetError("Prijava sa zadatim ID-jem ne postoji.", 404);

                    await session.DeleteAsync(prijava);
                    await tx.CommitAsync();
                }

                return true; // uspešno obrisano
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom brisanja prijave: " + ex.Message, 500);
            }
        }

        /// <summary>
        /// Broj prijava za konkretnu aktivnost — koristi se za proveru max učesnika.
        /// </summary>
        public static async Task<Result<int, ErrorMessage>> GetBrojPrijavaZaAktivnostAsync(int aktivnostId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var aktivnost = await session.GetAsync<Aktivnost>(aktivnostId);
                    if (aktivnost == null)
                        return GetError("Aktivnost sa zadatim ID-em ne postoji.", 404);

                    int brojPrijava = await session.Query<Prijava>()
                        .Where(p => p.Aktivnost.IdAktivnosti == aktivnostId)
                        .CountAsync();

                    return brojPrijava;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom dohvatanja broja prijava: " + ex.Message, 500);
            }
        }

        #endregion

        //#region Povreda

        //public static async Task<List<PovredaPregled>> GetAllPovredeAsync()
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var povrede = await session.Query<Povreda>()
        //                    .Select(p => new PovredaPregled(
        //                        p.ID,
        //                        p.Datum,
        //                        p.Opis))
        //                    .ToListAsync();

        //                return povrede;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja povreda: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task<PovredaBasic> GetPovredaAsync(int id)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var povreda = await session.GetAsync<Povreda>(id);

        //                if (povreda == null)
        //                    return null;

        //                PovredaBasic pb = new PovredaBasic
        //                {
        //                    Id = povreda.ID,
        //                    Datum = povreda.Datum,
        //                    Opis = povreda.Opis,
        //                    PreduzeteMere = povreda.PreduzeteMere,
        //                    // Opcionalno učitavanje povezanih entiteta:
        //                    //Dete = povreda.Dete != null ? new DeteBasic
        //                    //{
        //                    //    Id = povreda.Dete.ID,
        //                    //    Ime = povreda.Dete.Ime,
        //                    //    Prezime = povreda.Dete.Prezime
        //                    //} : null,

        //                    //Aktivnost = povreda.Aktivnost != null ? new AktivnostBasic
        //                    //{
        //                    //    Id = povreda.Aktivnost.ID,
        //                    //    Naziv = povreda.Aktivnost.Naziv
        //                    //} : null,

        //                    //OdgovornoOsoblje = povreda.OdgovornoLice != null ? new AngazovanoLiceBasic
        //                    //{
        //                    //    JMBG = povreda.OdgovornoLice.JMBG,
        //                    //    Ime = povreda.OdgovornoLice.Ime,
        //                    //    Prezime = povreda.OdgovornoLice.Prezime
        //                    //} : null
        //                };

        //                return pb;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja povrede: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task AddPovredaAsync(PovredaBasic povreda)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                Povreda novaPovreda = new Povreda
        //                {
        //                    Datum = povreda.Datum,
        //                    Opis = povreda.Opis,
        //                    PreduzeteMere = povreda.PreduzeteMere,
        //                    Dete = await session.GetAsync<Dete>(povreda.Dete?.Id),
        //                    Aktivnost = await session.GetAsync<Aktivnost>(povreda.Aktivnost?.Id),
        //                    OdgovornoOsoblje = await session.GetAsync<AngazovanoLice>(povreda.OdgovornoOsoblje?.JMBG)
        //                };

        //                await session.SaveAsync(novaPovreda);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom dodavanja povrede: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task UpdatePovredaAsync(PovredaBasic povreda)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var postojeca = await session.GetAsync<Povreda>(povreda.Id);
        //                if (postojeca == null)
        //                    throw new Exception("Povreda ne postoji u bazi.");

        //                postojeca.Datum = povreda.Datum;
        //                postojeca.Opis = povreda.Opis;
        //                postojeca.PreduzeteMere = povreda.PreduzeteMere;

        //                if (povreda.Dete != null)
        //                    postojeca.Dete = await session.GetAsync<Dete>(povreda.Dete.Id);
        //                if (povreda.Aktivnost != null)
        //                    postojeca.Aktivnost = await session.GetAsync<Aktivnost>(povreda.Aktivnost.Id);
        //                if (povreda.OdgovornoOsoblje != null)
        //                    postojeca.OdgovornoOsoblje = await session.GetAsync<AngazovanoLice>(povreda.OdgovornoOsoblje.JMBG);

        //                await session.UpdateAsync(postojeca);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom ažuriranja povrede: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task DeletePovredaAsync(int id)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var povreda = await session.GetAsync<Povreda>(id);

        //                if (povreda == null)
        //                    throw new Exception("Povreda sa zadatim ID-jem ne postoji.");

        //                await session.DeleteAsync(povreda);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom brisanja povrede: " + ex.Message, ex);
        //        }
        //    }

        //    #endregion

        //    #region Aktivnost

        //    public static async Task<List<AktivnostPregled>> GetAllAktivnostiAsync()
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var aktivnosti = await session.Query<Aktivnost>()
        //                    .Select(a => new AktivnostPregled(
        //                        a.IdAktivnosti,
        //                        a.Tip,
        //                        a.Naziv,
        //                        a.Datum))
        //                    .ToListAsync();

        //                return aktivnosti;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja aktivnosti: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task<AktivnostBasic> GetAktivnostAsync(int id)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var aktivnost = await session.GetAsync<Aktivnost>(id);

        //                if (aktivnost == null)
        //                    return null;

        //                AktivnostBasic ab = new AktivnostBasic
        //                {
        //                    Id = aktivnost.IdAktivnosti,
        //                    Tip = aktivnost.Tip,
        //                    Naziv = aktivnost.Naziv,
        //                    Datum = aktivnost.Datum,
        //                    StarosnaGrupa = aktivnost.StarosnaGrupa,
        //                    MaxUcesnika = aktivnost.MaxUcesnika,
        //                    Ogranicenja = aktivnost.Ogranicenja,
        //                    PrevoznoSredstvo = aktivnost.PrevoznoSredstvo,
        //                    PlanPuta = aktivnost.PlanPuta,
        //                    PotrebnaOprema = aktivnost.PotrebnaOprema,
        //                    Vodic = aktivnost.Vodic,
        //                    Sport = aktivnost.Sport,
        //                    PosebnaOprema = aktivnost.PosebnaOprema,
        //                    //Lokacija = aktivnost.Lokacija != null ? new LokacijaBasic
        //                    //{
        //                    //    Naziv = aktivnost.Lokacija.Naziv,
        //                    //    Tip = aktivnost.Lokacija.Tip,
        //                    //    Adresa = aktivnost.Lokacija.Adresa,
        //                    //    Kapacitet = aktivnost.Lokacija.Kapacitet,
        //                    //    DostupnaOprema = aktivnost.Lokacija.DostupnaOprema
        //                    //} : null,
        //                    //Evaluacija = aktivnost.Evaluacija != null ? new EvaluacijaBasic
        //                    //{
        //                    //    Id = aktivnost.Evaluacija.ID,
        //                    //    Ocena = aktivnost.Evaluacija.Ocena,
        //                    //    Datum = aktivnost.Evaluacija.Datum,
        //                    //    Opis = aktivnost.Evaluacija.Opis
        //                    //} : null
        //                };

        //                return ab;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja aktivnosti: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task AddAktivnostAsync(AktivnostBasic aktivnost)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var nova = new Aktivnost
        //                {
        //                    Tip = aktivnost.Tip,
        //                    Naziv = aktivnost.Naziv,
        //                    Datum = aktivnost.Datum,
        //                    StarosnaGrupa = aktivnost.StarosnaGrupa,
        //                    MaxUcesnika = aktivnost.MaxUcesnika,
        //                    Ogranicenja = aktivnost.Ogranicenja,
        //                    PrevoznoSredstvo = aktivnost.PrevoznoSredstvo,
        //                    PlanPuta = aktivnost.PlanPuta,
        //                    PotrebnaOprema = aktivnost.PotrebnaOprema,
        //                    Vodic = aktivnost.Vodic,
        //                    Sport = aktivnost.Sport,
        //                    PosebnaOprema = aktivnost.PosebnaOprema
        //                };

        //                if (aktivnost.Lokacija != null)
        //                {
        //                    nova.Lokacija = await session.LoadAsync<Lokacija>(aktivnost.Lokacija.Naziv);
        //                }

        //                if (aktivnost.Evaluacija != null)
        //                {
        //                    var eval = new Evaluacija
        //                    {
        //                        Ocena = aktivnost.Evaluacija.Ocena,
        //                        Datum = aktivnost.Evaluacija.Datum,
        //                        Opis = aktivnost.Evaluacija.Opis,
        //                        Aktivnost = nova
        //                    };

        //                    nova.Evaluacija = eval;
        //                }

        //                await session.SaveAsync(nova);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom dodavanja aktivnosti: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task UpdateAktivnostAsync(AktivnostBasic aktivnost)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var postojeca = await session.GetAsync<Aktivnost>(aktivnost.Id);

        //                if (postojeca == null)
        //                    throw new Exception("Aktivnost ne postoji.");

        //                postojeca.Tip = aktivnost.Tip;
        //                postojeca.Naziv = aktivnost.Naziv;
        //                postojeca.Datum = aktivnost.Datum;
        //                postojeca.StarosnaGrupa = aktivnost.StarosnaGrupa;
        //                postojeca.MaxUcesnika = aktivnost.MaxUcesnika;
        //                postojeca.Ogranicenja = aktivnost.Ogranicenja;
        //                postojeca.PrevoznoSredstvo = aktivnost.PrevoznoSredstvo;
        //                postojeca.PlanPuta = aktivnost.PlanPuta;
        //                postojeca.PotrebnaOprema = aktivnost.PotrebnaOprema;
        //                postojeca.Vodic = aktivnost.Vodic;
        //                postojeca.Sport = aktivnost.Sport;
        //                postojeca.PosebnaOprema = aktivnost.PosebnaOprema;

        //                if (aktivnost.Lokacija != null)
        //                {
        //                    postojeca.Lokacija = await session.LoadAsync<Lokacija>(aktivnost.Lokacija.Naziv);
        //                }

        //                await session.UpdateAsync(postojeca);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom ažuriranja aktivnosti: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task DeleteAktivnostAsync(int id)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var aktivnost = await session.GetAsync<Aktivnost>(id);

        //                if (aktivnost == null)
        //                    throw new Exception("Aktivnost sa datim ID-jem ne postoji.");

        //                await session.DeleteAsync(aktivnost);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom brisanja aktivnosti: " + ex.Message, ex);
        //        }
        //    }

        //    #endregion

        //    #region Evaluacija

        //    public static async Task<List<EvaluacijaPregled>> GetAllEvaluacijeAsync()
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var evaluacije = await session.Query<Evaluacija>()
        //                    .Select(e => new EvaluacijaPregled(
        //                        e.ID,
        //                        e.Ocena,
        //                        e.Datum,
        //                        e.Opis))
        //                    .ToListAsync();

        //                return evaluacije;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja evaluacija: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task<EvaluacijaBasic> GetEvaluacijaAsync(int id)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var evaluacija = await session.GetAsync<Evaluacija>(id);
        //                if (evaluacija == null)
        //                    return null;

        //                EvaluacijaBasic eb = new EvaluacijaBasic
        //                {
        //                    Id = evaluacija.ID,
        //                    Ocena = evaluacija.Ocena,
        //                    Datum = evaluacija.Datum,
        //                    Opis = evaluacija.Opis,
        //                    // Opcionalno: možete učitati i osnovne podatke o aktivnosti i angažovanom licu ako treba
        //                    //Aktivnost = evaluacija.Aktivnost != null
        //                    //    ? new AktivnostBasic
        //                    //    {
        //                    //        Id = evaluacija.Aktivnost.IdAktivnosti,
        //                    //        Naziv = evaluacija.Aktivnost.Naziv
        //                    //    }
        //                    //    : null,

        //                    //AngazovanoLice = evaluacija.AngazovanoLice != null
        //                    //    ? new AngazovanoLiceBasic
        //                    //    {
        //                    //        JMBG = evaluacija.AngazovanoLice.JMBG,
        //                    //        Ime = evaluacija.AngazovanoLice.Ime,
        //                    //        Prezime = evaluacija.AngazovanoLice.Prezime
        //                    //    }
        //                    //    : null
        //                };

        //                return eb;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja evaluacije: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task AddEvaluacijaAsync(EvaluacijaBasic evaluacija)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                Evaluacija novaEvaluacija = new Evaluacija
        //                {
        //                    Ocena = evaluacija.Ocena,
        //                    Datum = evaluacija.Datum,
        //                    Opis = evaluacija.Opis
        //                };

        //                // Veze prema drugim entitetima (ako postoje)
        //                if (evaluacija.Aktivnost != null)
        //                    novaEvaluacija.Aktivnost = await session.LoadAsync<Aktivnost>(evaluacija.Aktivnost.Id);

        //                if (evaluacija.AngazovanoLice != null)
        //                    novaEvaluacija.AngazovanoLice = await session.LoadAsync<AngazovanoLice>(evaluacija.AngazovanoLice.JMBG);

        //                await session.SaveAsync(novaEvaluacija);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom dodavanja evaluacije: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task UpdateEvaluacijaAsync(EvaluacijaBasic evaluacija)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var postojecaEvaluacija = await session.GetAsync<Evaluacija>(evaluacija.Id);
        //                if (postojecaEvaluacija == null)
        //                    throw new Exception("Evaluacija ne postoji u bazi.");

        //                postojecaEvaluacija.Ocena = evaluacija.Ocena;
        //                postojecaEvaluacija.Datum = evaluacija.Datum;
        //                postojecaEvaluacija.Opis = evaluacija.Opis;

        //                if (evaluacija.Aktivnost != null)
        //                    postojecaEvaluacija.Aktivnost = await session.LoadAsync<Aktivnost>(evaluacija.Aktivnost.Id);

        //                if (evaluacija.AngazovanoLice != null)
        //                    postojecaEvaluacija.AngazovanoLice = await session.LoadAsync<AngazovanoLice>(evaluacija.AngazovanoLice.JMBG);

        //                await session.UpdateAsync(postojecaEvaluacija);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom ažuriranja evaluacije: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task DeleteEvaluacijaAsync(int id)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var evaluacija = await session.GetAsync<Evaluacija>(id);
        //                if (evaluacija == null)
        //                    throw new Exception("Evaluacija sa zadatim ID-jem ne postoji.");

        //                await session.DeleteAsync(evaluacija);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom brisanja evaluacije: " + ex.Message, ex);
        //        }
        //    }

        //    #endregion

        //    #region Obrok
        //    public static async Task<List<ObrokPregled>> GetObrociZaDeteAsync(int deteId)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                // Učitaj dete sa obrocima (lazy loading možda ne radi automatski pa se koristi explicitno)
        //                var dete = await session.GetAsync<Dete>(deteId);
        //                if (dete == null)
        //                    throw new Exception("Dete nije pronađeno.");

        //                // Ako koristiš lazy loading, moraš da eksplicitno učitaš obroke
        //                await NHibernateUtil.InitializeAsync(dete.Obroci);

        //                var obroci = dete.Obroci.Select(o => new ObrokPregled
        //                {
        //                    Id = o.ID,
        //                    Tip = o.Tip,
        //                    Jelovnik = o.Jelovnik,
        //                    Uzrast = o.Uzrast
        //                }).ToList();

        //                return obroci;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Greška prilikom učitavanja obroka deteta: " + ex.Message);
        //        }
        //    }

        //    public static async Task<List<ObrokPregled>> GetAllObrociAsync()
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var obroci = await session.Query<Obrok>()
        //                    .Select(o => new ObrokPregled(
        //                        o.ID,
        //                        o.Tip,
        //                        o.Jelovnik,
        //                        o.Uzrast
        //                        ))
        //                    .ToListAsync();

        //                return obroci;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja obroka: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task<ObrokBasic> GetObrokAsync(int id)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var obrok = await session.GetAsync<Obrok>(id);

        //                if (obrok == null)
        //                    return null;

        //                ObrokBasic ob = new ObrokBasic
        //                {
        //                    Id = obrok.ID,
        //                    Tip = obrok.Tip,
        //                    Uzrast = obrok.Uzrast,
        //                    Jelovnik = obrok.Jelovnik,
        //                    PosebneOpcije = obrok.PosebneOpcije,
        //                    //Lokacija = obrok.Lokacija != null ? new LokacijaBasic
        //                    //{
        //                    //    Naziv = obrok.Lokacija.Naziv,
        //                    //    Tip = obrok.Lokacija.Tip,
        //                    //    Adresa = obrok.Lokacija.Adresa,
        //                    //    Kapacitet = obrok.Lokacija.Kapacitet,
        //                    //    DostupnaOprema = obrok.Lokacija.DostupnaOprema
        //                    //} : null,
        //                    //Aktivnost = obrok.Aktivnost != null ? new AktivnostBasic
        //                    //{
        //                    //    Id = obrok.Aktivnost.IdAktivnosti,
        //                    //    Tip = obrok.Aktivnost.Tip,
        //                    //    Naziv = obrok.Aktivnost.Naziv,
        //                    //    Datum = obrok.Aktivnost.Datum
        //                    //} : null
        //                };

        //                return ob;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja obroka: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task AddObrokAsync(ObrokBasic obrok)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                Obrok noviObrok = new Obrok
        //                {
        //                    Tip = obrok.Tip,
        //                    Uzrast = obrok.Uzrast,
        //                    Jelovnik = obrok.Jelovnik,
        //                    PosebneOpcije = obrok.PosebneOpcije,
        //                    Lokacija = obrok.Lokacija != null
        //                        ? await session.GetAsync<Lokacija>(obrok.Lokacija.Naziv)
        //                        : null,
        //                    Aktivnost = obrok.Aktivnost != null
        //                        ? await session.GetAsync<Aktivnost>(obrok.Aktivnost.Id)
        //                        : null
        //                };

        //                await session.SaveAsync(noviObrok);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom dodavanja obroka: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task UpdateObrokAsync(ObrokBasic obrok)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var postojeci = await session.GetAsync<Obrok>(obrok.Id);
        //                if (postojeci == null)
        //                    throw new Exception("Obrok ne postoji u bazi.");

        //                postojeci.Tip = obrok.Tip;
        //                postojeci.Uzrast = obrok.Uzrast;
        //                postojeci.Jelovnik = obrok.Jelovnik;
        //                postojeci.PosebneOpcije = obrok.PosebneOpcije;

        //                if (obrok.Lokacija != null)
        //                    postojeci.Lokacija = await session.GetAsync<Lokacija>(obrok.Lokacija.Naziv);

        //                if (obrok.Aktivnost != null)
        //                    postojeci.Aktivnost = await session.GetAsync<Aktivnost>(obrok.Aktivnost.Id);

        //                await session.UpdateAsync(postojeci);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom ažuriranja obroka: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task DeleteObrokAsync(int id)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var obrok = await session.GetAsync<Obrok>(id);
        //                if (obrok == null)
        //                    throw new Exception("Obrok sa zadatim ID-jem ne postoji.");

        //                await session.DeleteAsync(obrok);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom brisanja obroka: " + ex.Message, ex);
        //        }
        //    }
        //    public static async Task DodeliObrokDetetuAsync(int deteId, int obrokId)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var dete = await session.GetAsync<Dete>(deteId);
        //                if (dete == null)
        //                    throw new Exception("Dete nije pronađeno.");

        //                var obrok = await session.GetAsync<Obrok>(obrokId);
        //                if (obrok == null)
        //                    throw new Exception("Obrok nije pronađen.");

        //                // Provera uzrasta
        //                var trenutniDatum = DateTime.Now;
        //                var starost = trenutniDatum.Year - dete.DatumRodjenja.Year;
        //                if (trenutniDatum.Month < dete.DatumRodjenja.Month ||
        //                    (trenutniDatum.Month == dete.DatumRodjenja.Month && trenutniDatum.Day < dete.DatumRodjenja.Day))
        //                {
        //                    starost--;
        //                }

        //                if (obrok.Uzrast != "Svi")
        //                {
        //                    var uzrastDelovi = obrok.Uzrast.Split('-');
        //                    if (uzrastDelovi.Length == 2)
        //                    {
        //                        var minUzrast = int.Parse(uzrastDelovi[0]);
        //                        var maxUzrast = int.Parse(uzrastDelovi[1]);
        //                        if (starost < minUzrast || starost > maxUzrast)
        //                            throw new Exception($"Obrok nije predviđen za uzrast deteta. Detetu je {starost} godina.");
        //                    }
        //                    else
        //                        throw new Exception("Format uzrasta nije ispravan.");
        //                }

        //                if (!string.IsNullOrEmpty(dete.PosebnePotrebe))
        //                {
        //                    var posebnePotrebe = dete.PosebnePotrebe.ToLower();
        //                    var opcije = obrok.PosebneOpcije?.ToLower() ?? "";

        //                    if (posebnePotrebe.Contains("laktoza") && !opcije.Contains("bez mlečnih proizvoda"))
        //                        throw new Exception("Dete ima alergiju na laktozu, pa ne može dobiti obrok sa mlečnim proizvodima.");
        //                    if (posebnePotrebe.Contains("gluten") && !opcije.Contains("bez glutena"))
        //                        throw new Exception("Dete ima alergiju na gluten, pa ne može dobiti obrok sa glutenom.");
        //                    if (posebnePotrebe.Contains("vegetarijanski") && !opcije.Contains("vegetarijanski"))
        //                        throw new Exception("Dete ima vegetarijansku ishranu, pa ne može dobiti obrok sa mesom.");
        //                }

        //                // Proveri da li je već dodeljeno
        //                if (obrok.Deca.Any(d => d.ID == deteId))
        //                    throw new Exception("Ovaj obrok je već dodeljen ovom detetu.");

        //                if (!obrok.Deca.Contains(dete))
        //                {
        //                    obrok.Deca.Add(dete);
        //                }

        //                await session.FlushAsync();

        //                Console.WriteLine($"Obrok sa ID: {obrokId} je uspešno dodeljen detetu {dete.Ime} {dete.Prezime}.");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške dodele obroka: " + ex.Message, ex);
        //        }
        //    }

        //    #endregion

        //    #region AngazovanoLice

        //    public static async Task<List<AngazovanoLicePregled>> GetAllAngazovanaLicaAsync()
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var lica = await session.Query<AngazovanoLice>()
        //                    .Select(al => new AngazovanoLicePregled(
        //                        al.JMBG,
        //                        al.Ime,
        //                        al.Prezime))
        //                    .ToListAsync();

        //                return lica;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja angažovanih lica: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task<AngazovanoLiceBasic> GetAngazovanoLiceAsync(string jmbg)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var lice = await session.GetAsync<AngazovanoLice>(jmbg);

        //                if (lice == null)
        //                    return null;

        //                AngazovanoLiceBasic alb = new AngazovanoLiceBasic
        //                {
        //                    JMBG = lice.JMBG,
        //                    Ime = lice.Ime,
        //                    Prezime = lice.Prezime,
        //                    Pol = lice.Pol,
        //                    Adresa = lice.Adresa,
        //                    BrojTelefona = lice.BrojTelefona,
        //                    Email = lice.Email,
        //                    StrucnaSprema = lice.StrucnaSprema,
        //                    Volonter = lice.Volonter,
        //                    Trener = lice.Trener,
        //                    Animator = lice.Animator,
        //                    ZdravstveniRadnik = lice.ZdravstveniRadnik,
        //                    Evaluacija = lice.Evaluacija != null ? new EvaluacijaBasic
        //                    {
        //                        Id = lice.Evaluacija.ID,
        //                        Ocena = lice.Evaluacija.Ocena,
        //                        Datum = lice.Evaluacija.Datum,
        //                        Opis = lice.Evaluacija.Opis
        //                    } : null
        //                };

        //                return alb;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja angažovanog lica: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task AddAngazovanoLiceAsync(AngazovanoLiceBasic lice)
        //    {
        //        try
        //        {
        //            if (string.IsNullOrWhiteSpace(lice.JMBG) || string.IsNullOrWhiteSpace(lice.Ime) || string.IsNullOrWhiteSpace(lice.Prezime))
        //                throw new Exception("JMBG, Ime i Prezime su obavezni.");

        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var novoLice = new AngazovanoLice
        //                {
        //                    JMBG = lice.JMBG,
        //                    Ime = lice.Ime,
        //                    Prezime = lice.Prezime,
        //                    Pol = lice.Pol,
        //                    Adresa = lice.Adresa,
        //                    BrojTelefona = lice.BrojTelefona,
        //                    Email = lice.Email,
        //                    StrucnaSprema = lice.StrucnaSprema,
        //                    Volonter = lice.Volonter,
        //                    Trener = lice.Trener,
        //                    Animator = lice.Animator,
        //                    ZdravstveniRadnik = lice.ZdravstveniRadnik
        //                };

        //                await session.SaveAsync(novoLice);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom dodavanja angažovanog lica: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task UpdateAngazovanoLiceAsync(AngazovanoLiceBasic lice)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var postojeci = await session.GetAsync<AngazovanoLice>(lice.JMBG);
        //                if (postojeci == null)
        //                    throw new Exception("Angažovano lice ne postoji u bazi.");

        //                postojeci.Ime = lice.Ime;
        //                postojeci.Prezime = lice.Prezime;
        //                postojeci.Pol = lice.Pol;
        //                postojeci.Adresa = lice.Adresa;
        //                postojeci.BrojTelefona = lice.BrojTelefona;
        //                postojeci.Email = lice.Email;
        //                postojeci.StrucnaSprema = lice.StrucnaSprema;
        //                postojeci.Volonter = lice.Volonter;
        //                postojeci.Trener = lice.Trener;
        //                postojeci.Animator = lice.Animator;
        //                postojeci.ZdravstveniRadnik = lice.ZdravstveniRadnik;

        //                await session.UpdateAsync(postojeci);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom ažuriranja angažovanog lica: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task DeleteAngazovanoLiceAsync(string jmbg)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var lice = await session.GetAsync<AngazovanoLice>(jmbg);

        //                if (lice == null)
        //                    throw new Exception("Angažovano lice sa zadatim JMBG-om ne postoji.");

        //                await session.DeleteAsync(lice);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom brisanja angažovanog lica: " + ex.Message, ex);
        //        }
        //    }


        //    #endregion

        //    #region Lokacija
        //    public static async Task<List<AktivnostPregled>> GetAktivnostiNaLokacijiAsync(string nazivLokacije)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var aktivnosti = await session.Query<Aktivnost>()
        //                    .Where(a => a.Lokacija.Naziv == nazivLokacije)
        //                    .Select(a => new AktivnostPregled
        //                    {
        //                        Id = a.IdAktivnosti,
        //                        Tip = a.Tip,
        //                        Naziv = a.Naziv,
        //                        Datum = a.Datum
        //                    })
        //                    .ToListAsync();

        //                return aktivnosti;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja aktivnosti na lokaciji: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task<List<LokacijaPregled>> GetAllLokacijeAsync()
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var lokacije = await session.Query<Lokacija>()
        //                    .Select(l => new LokacijaPregled(
        //                        l.Naziv,
        //                        l.Tip))
        //                    .ToListAsync();

        //                return lokacije;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja lokacija: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task<LokacijaBasic> GetLokacijaAsync(string naziv)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var lokacija = await session.GetAsync<Lokacija>(naziv);

        //                if (lokacija == null)
        //                    return null;

        //                LokacijaBasic lb = new LokacijaBasic
        //                {
        //                    Naziv = lokacija.Naziv,
        //                    Tip = lokacija.Tip,
        //                    Adresa = lokacija.Adresa,
        //                    Kapacitet = lokacija.Kapacitet,
        //                    DostupnaOprema = lokacija.DostupnaOprema
        //                };

        //                return lb;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja lokacije: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task AddLokacijaAsync(LokacijaBasic lokacija)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                Lokacija novaLokacija = new Lokacija
        //                {
        //                    Naziv = lokacija.Naziv,
        //                    Tip = lokacija.Tip,
        //                    Adresa = lokacija.Adresa,
        //                    Kapacitet = lokacija.Kapacitet,
        //                    DostupnaOprema = lokacija.DostupnaOprema
        //                };

        //                await session.SaveAsync(novaLokacija);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom dodavanja lokacije: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task UpdateLokacijaAsync(LokacijaBasic lokacija)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var postojeca = await session.GetAsync<Lokacija>(lokacija.Naziv);

        //                if (postojeca == null)
        //                    throw new Exception("Lokacija ne postoji u bazi.");

        //                postojeca.Tip = lokacija.Tip;
        //                postojeca.Adresa = lokacija.Adresa;
        //                postojeca.Kapacitet = lokacija.Kapacitet;
        //                postojeca.DostupnaOprema = lokacija.DostupnaOprema;

        //                await session.UpdateAsync(postojeca);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom ažuriranja lokacije: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task DeleteLokacijaAsync(string naziv)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var lokacija = await session.GetAsync<Lokacija>(naziv);

        //                if (lokacija == null)
        //                    throw new Exception("Lokacija sa zadatim nazivom ne postoji.");

        //                await session.DeleteAsync(lokacija);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom brisanja lokacije: " + ex.Message, ex);
        //        }
        //    }

        //    #endregion

        //    #region TelefonRoditelja

        //    public static async Task<List<TelefonRoditeljaPregled>> GetAllTelefoniRoditeljaAsync()
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var telefoni = await session.Query<TelefonRoditelja>()
        //                    .Select(t => new TelefonRoditeljaPregled(
        //                        t.ID,
        //                        t.Telefon))
        //                    .ToListAsync();

        //                return telefoni;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja telefona roditelja: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task<TelefonRoditeljaBasic> GetTelefonRoditeljaAsync(int id)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var telefon = await session.GetAsync<TelefonRoditelja>(id);
        //                if (telefon == null)
        //                    return null;

        //                var dto = new TelefonRoditeljaBasic
        //                {
        //                    Id = telefon.ID,
        //                    Telefon = telefon.Telefon
        //                };

        //                return dto;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja telefona roditelja: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task AddTelefonRoditeljaAsync(TelefonRoditeljaBasic telefonDto, int deteId)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var dete = await session.GetAsync<Dete>(deteId);
        //                if (dete == null)
        //                    throw new Exception("Dete ne postoji.");

        //                TelefonRoditelja noviTelefon = new TelefonRoditelja
        //                {
        //                    Telefon = telefonDto.Telefon,
        //                    Dete = dete
        //                };

        //                await session.SaveAsync(noviTelefon);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom dodavanja telefona roditelja: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task UpdateTelefonRoditeljaAsync(TelefonRoditeljaBasic telefonDto)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var telefon = await session.GetAsync<TelefonRoditelja>(telefonDto.Id);
        //                if (telefon == null)
        //                    throw new Exception("Telefon roditelja ne postoji.");

        //                telefon.Telefon = telefonDto.Telefon;

        //                await session.UpdateAsync(telefon);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom ažuriranja telefona roditelja: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task DeleteTelefonRoditeljaAsync(int id)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var telefon = await session.GetAsync<TelefonRoditelja>(id);
        //                if (telefon == null)
        //                    throw new Exception("Telefon roditelja sa zadatim ID-jem ne postoji.");

        //                await session.DeleteAsync(telefon);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom brisanja telefona roditelja: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task<List<TelefonRoditeljaPregled>> GetTelefoniRoditeljaZaDeteAsync(int deteId)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var telefoni = await session.Query<TelefonRoditelja>()
        //                    .Where(t => t.Dete.ID == deteId)
        //                    .Select(t => new TelefonRoditeljaPregled(
        //                        t.ID,
        //                        t.Telefon))
        //                    .ToListAsync();

        //                return telefoni;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja telefona roditelja za dete: " + ex.Message, ex);
        //        }
        //    }

        //    #endregion

        //    #region EmailRoditelja

        //    public static async Task<List<EmailRoditeljaPregled>> GetAllEmailoviRoditeljaAsync()
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var emailovi = await session.Query<EmailRoditelja>()
        //                    .Select(e => new EmailRoditeljaPregled(
        //                        e.ID,
        //                        e.Email))
        //                    .ToListAsync();

        //                return emailovi;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja emailova roditelja: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task<EmailRoditeljaBasic> GetEmailRoditeljaAsync(int id)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var email = await session.GetAsync<EmailRoditelja>(id);
        //                if (email == null)
        //                    return null;

        //                var dto = new EmailRoditeljaBasic
        //                {
        //                    Id = email.ID,
        //                    Email = email.Email
        //                };

        //                return dto;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja emaila roditelja: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task AddEmailRoditeljaAsync(EmailRoditeljaBasic emailDto, int deteId)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var dete = await session.GetAsync<Dete>(deteId);
        //                if (dete == null)
        //                    throw new Exception("Dete ne postoji.");

        //                EmailRoditelja noviEmail = new EmailRoditelja
        //                {
        //                    Email = emailDto.Email,
        //                    Dete = dete
        //                };

        //                await session.SaveAsync(noviEmail);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom dodavanja emaila roditelja: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task UpdateEmailRoditeljaAsync(EmailRoditeljaBasic emailDto)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var email = await session.GetAsync<EmailRoditelja>(emailDto.Id);
        //                if (email == null)
        //                    throw new Exception("Email roditelja ne postoji.");

        //                email.Email = emailDto.Email;

        //                await session.UpdateAsync(email);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom ažuriranja emaila roditelja: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task DeleteEmailRoditeljaAsync(int id)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var email = await session.GetAsync<EmailRoditelja>(id);
        //                if (email == null)
        //                    throw new Exception("Email roditelja sa zadatim ID-jem ne postoji.");

        //                await session.DeleteAsync(email);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom brisanja emaila roditelja: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task<List<EmailRoditeljaPregled>> GetEmailoviRoditeljaZaDeteAsync(int deteId)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var emailovi = await session.Query<EmailRoditelja>()
        //                    .Where(e => e.Dete.ID == deteId)
        //                    .Select(e => new EmailRoditeljaPregled(
        //                        e.ID,
        //                        e.Email))
        //                    .ToListAsync();

        //                return emailovi;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja emailova roditelja za dete: " + ex.Message, ex);
        //        }
        //    }

        //    #endregion

        //    #region Ucestvuje

        //    public static async Task<List<UcestvujePregled>> GetAllUcescaAsync()
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var ucesca = await session.Query<Ucestvuje>()
        //                    .Select(u => new UcestvujePregled(
        //                        u.ID,
        //                        u.OcenaAktivnosti.ToString(),
        //                        u.Komentari))
        //                    .ToListAsync();

        //                return ucesca;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja učešća: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task<UcestvujeBasic> GetUcesceAsync(int id)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var ucesce = await session.GetAsync<Ucestvuje>(id);

        //                if (ucesce == null)
        //                    return null;

        //                var dto = new UcestvujeBasic
        //                {
        //                    ID = ucesce.ID,
        //                    Prisustvo = ucesce.Prisustvo,
        //                    OcenaAktivnosti = ucesce.OcenaAktivnosti,
        //                    Komentari = ucesce.Komentari,
        //                    Pratilac = ucesce.Pratilac,
        //                    Dete = new DeteBasic
        //                    {
        //                        Id = ucesce.Dete.ID,
        //                        Ime = ucesce.Dete.Ime,
        //                        Prezime = ucesce.Dete.Prezime
        //                    },
        //                    Roditelj = new RoditeljBasic
        //                    {
        //                        Id = ucesce.Roditelj.ID,
        //                        Ime = ucesce.Roditelj.Ime,
        //                        Prezime = ucesce.Roditelj.Prezime
        //                    },
        //                    Aktivnost = new AktivnostBasic
        //                    {
        //                        Id = ucesce.Aktivnost.IdAktivnosti,
        //                        Naziv = ucesce.Aktivnost.Naziv,
        //                        Tip = ucesce.Aktivnost.Tip
        //                    }
        //                };

        //                return dto;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom učitavanja učešća: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task AddUcesceAsync(UcestvujeBasic ucesce)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var dete = await session.GetAsync<Dete>(ucesce.Dete.Id);
        //                var roditelj = await session.GetAsync<Roditelj>(ucesce.Roditelj.Id);
        //                var aktivnost = await session.GetAsync<Aktivnost>(ucesce.Aktivnost.Id);

        //                if (dete == null || roditelj == null || aktivnost == null)
        //                    throw new Exception("Dete, roditelj ili aktivnost nisu pronađeni.");

        //                var novoUcesce = new Ucestvuje
        //                {
        //                    Prisustvo = ucesce.Prisustvo,
        //                    OcenaAktivnosti = ucesce.OcenaAktivnosti,
        //                    Komentari = ucesce.Komentari,
        //                    Pratilac = ucesce.Pratilac,
        //                    Dete = dete,
        //                    Roditelj = roditelj,
        //                    Aktivnost = aktivnost
        //                };

        //                await session.SaveAsync(novoUcesce);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom dodavanja učešća: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task UpdateUcesceAsync(UcestvujeBasic ucesce)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var postojeci = await session.GetAsync<Ucestvuje>(ucesce.ID);

        //                if (postojeci == null)
        //                    throw new Exception("Učešće nije pronađeno.");

        //                postojeci.Prisustvo = ucesce.Prisustvo;
        //                postojeci.OcenaAktivnosti = ucesce.OcenaAktivnosti;
        //                postojeci.Komentari = ucesce.Komentari;
        //                postojeci.Pratilac = ucesce.Pratilac;

        //                // Po želji ažurirati i reference
        //                if (ucesce.Dete != null)
        //                    postojeci.Dete = await session.GetAsync<Dete>(ucesce.Dete.Id);
        //                if (ucesce.Roditelj != null)
        //                    postojeci.Roditelj = await session.GetAsync<Roditelj>(ucesce.Roditelj.Id);
        //                if (ucesce.Aktivnost != null)
        //                    postojeci.Aktivnost = await session.GetAsync<Aktivnost>(ucesce.Aktivnost.Id);

        //                await session.UpdateAsync(postojeci);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom ažuriranja učešća: " + ex.Message, ex);
        //        }
        //    }

        //    public static async Task DeleteUcesceAsync(int id)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var ucesce = await session.GetAsync<Ucestvuje>(id);

        //                if (ucesce == null)
        //                    throw new Exception("Učešće sa zadatim ID-jem ne postoji.");

        //                await session.DeleteAsync(ucesce);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Došlo je do greške prilikom brisanja učešća: " + ex.Message, ex);
        //        }
        //    }

        //    #endregion

        //    #region Ucesce

        //    #region AngazovanoLice - Aktivnost Veze

        //    // Dodaje angažovano lice na aktivnost
        //    public static async Task AddAngazovanoLiceToAktivnostAsync(string jmbg, int aktivnostId)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var lice = await session.GetAsync<AngazovanoLice>(jmbg);
        //                var aktivnost = await session.GetAsync<Aktivnost>(aktivnostId);

        //                if (lice == null)
        //                    throw new Exception("Angažovano lice ne postoji.");
        //                if (aktivnost == null)
        //                    throw new Exception("Aktivnost ne postoji.");

        //                if (!aktivnost.AngazovanaLica.Contains(lice))
        //                {
        //                    aktivnost.AngazovanaLica.Add(lice);
        //                    lice.Aktivnosti.Add(aktivnost);
        //                }

        //                await session.UpdateAsync(aktivnost);
        //                await session.UpdateAsync(lice);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Greška prilikom dodavanja angažovanog lica na aktivnost: " + ex.Message, ex);
        //        }
        //    }

        //    // Prikazuje sva angažovana lica na određenoj aktivnosti
        //    public static async Task<List<AngazovanoLicePregled>> GetAngazovanaLicaNaAktivnostiAsync(int aktivnostId)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var aktivnost = await session.GetAsync<Aktivnost>(aktivnostId);
        //                if (aktivnost == null)
        //                    throw new Exception("Aktivnost ne postoji.");

        //                var lista = aktivnost.AngazovanaLica
        //                            .Select(al => new AngazovanoLicePregled(al.JMBG, al.Ime, al.Prezime))
        //                            .ToList();

        //                return lista;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Greška prilikom učitavanja angažovanih lica za aktivnost: " + ex.Message, ex);
        //        }
        //    }

        //    // Prikazuje angažovana lica koja **nisu** na toj aktivnosti
        //    public static async Task<List<AngazovanoLicePregled>> GetAngazovanaLicaNeNaAktivnostiAsync(int aktivnostId)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var sviLica = await session.Query<AngazovanoLice>().ToListAsync();
        //                var aktivnost = await session.GetAsync<Aktivnost>(aktivnostId);

        //                if (aktivnost == null)
        //                    throw new Exception("Aktivnost ne postoji.");

        //                var neAngazovana = sviLica
        //                    .Where(l => !aktivnost.AngazovanaLica.Contains(l))
        //                    .Select(al => new AngazovanoLicePregled(al.JMBG, al.Ime, al.Prezime))
        //                    .ToList();

        //                return neAngazovana;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Greška prilikom učitavanja angažovanih lica koja nisu na aktivnosti: " + ex.Message, ex);
        //        }
        //    }

        //    // Uklanja angažovano lice sa aktivnosti
        //    public static async Task RemoveAngazovanoLiceFromAktivnostAsync(string jmbg, int aktivnostId)
        //    {
        //        try
        //        {
        //            using (ISession session = DataLayer.GetSession())
        //            {
        //                var lice = await session.GetAsync<AngazovanoLice>(jmbg);
        //                var aktivnost = await session.GetAsync<Aktivnost>(aktivnostId);

        //                if (lice == null)
        //                    throw new Exception("Angažovano lice ne postoji.");
        //                if (aktivnost == null)
        //                    throw new Exception("Aktivnost ne postoji.");

        //                if (aktivnost.AngazovanaLica.Contains(lice))
        //                {
        //                    aktivnost.AngazovanaLica.Remove(lice);
        //                    lice.Aktivnosti.Remove(aktivnost);
        //                }

        //                await session.UpdateAsync(aktivnost);
        //                await session.UpdateAsync(lice);
        //                await session.FlushAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Greška prilikom uklanjanja angažovanog lica sa aktivnosti: " + ex.Message, ex);
        //        }
        //    }

        //    #endregion


        //#endregion

    }
}
