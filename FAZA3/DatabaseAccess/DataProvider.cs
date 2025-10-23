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
                return GetError("Doslo je do greske prilikom dodavanja deteta: " + ex.Message, 500);
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

        #region Povreda
        //=========================================================================================
        //TREBA DA SE MENJA VEROVATNO, BAS LOSE DELUJE ------------- Zapravo Radi ali ne svidja mi se kako izgleda
        //=========================================================================================
        public static async Task<Result<List<PovredaPregled>, ErrorMessage>> GetAllPovredeAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var povrede = await session.Query<Povreda>()
                        .Select(p => new PovredaPregled
                        {
                            Id = p.ID,
                            Datum = p.Datum,
                            Opis = p.Opis,
                            PreduzeteMere = p.PreduzeteMere,

                            Dete = p.Dete != null ? new DetePregled
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
                            } : null,

                            Aktivnost = p.Aktivnost != null ? new AktivnostPregled
                            {
                                Id = p.Aktivnost.IdAktivnosti,
                                Tip = p.Aktivnost.Tip,
                                Naziv = p.Aktivnost.Naziv,
                                Datum = p.Aktivnost.Datum,
                                StarosnaGrupa = p.Aktivnost.StarosnaGrupa,
                                MaxUcesnika = p.Aktivnost.MaxUcesnika,
                                Ogranicenja = p.Aktivnost.Ogranicenja,
                                PrevoznoSredstvo = p.Aktivnost.PrevoznoSredstvo,
                                PlanPuta = p.Aktivnost.PlanPuta,
                                PotrebnaOprema = p.Aktivnost.PotrebnaOprema,
                                Vodic = p.Aktivnost.Vodic,
                                Sport = p.Aktivnost.Sport,
                                PosebnaOprema = p.Aktivnost.PosebnaOprema
                            } : null,

                            OdgovornoOsoblje = p.OdgovornoOsoblje != null ? new AngazovanoLicePregled
                            {
                                JMBG = p.OdgovornoOsoblje.JMBG,
                                Ime = p.OdgovornoOsoblje.Ime,
                                Prezime = p.OdgovornoOsoblje.Prezime,
                                Pol = p.OdgovornoOsoblje.Pol,
                                Adresa = p.OdgovornoOsoblje.Adresa,
                                BrojTelefona = p.OdgovornoOsoblje.BrojTelefona,
                                Email = p.OdgovornoOsoblje.Email,
                                StrucnaSprema = p.OdgovornoOsoblje.StrucnaSprema,
                                Volonter = p.OdgovornoOsoblje.Volonter,
                                Trener = p.OdgovornoOsoblje.Trener,
                                Animator = p.OdgovornoOsoblje.Animator,
                                ZdravstveniRadnik = p.OdgovornoOsoblje.ZdravstveniRadnik
                            } : null
                        })
                        .ToListAsync();

                    return povrede;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja povreda: " + ex.Message, 500);
            }
        }
        //===================================================================================
        //ISTO OVDE
        //===================================================================================
        public static async Task<Result<PovredaPregled, ErrorMessage>> GetPovredaAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var povreda = await session.GetAsync<Povreda>(id);
                    if (povreda == null)
                        return GetError($"Povreda sa ID-em {id} ne postoji.", 404);

                    var pp = new PovredaPregled
                    {
                        Id = povreda.ID,
                        Datum = povreda.Datum,
                        Opis = povreda.Opis,
                        PreduzeteMere = povreda.PreduzeteMere,
                        Dete = povreda.Dete != null ? new DetePregled
                        {
                            ID = povreda.Dete.ID,
                            Ime = povreda.Dete.Ime,
                            Prezime = povreda.Dete.Prezime,
                            DatumRodjenja = povreda.Dete.DatumRodjenja,
                            Pol = povreda.Dete.Pol,
                            Adresa = povreda.Dete.Adresa,
                            TelefonDeteta = povreda.Dete.TelefonDeteta,
                            EmailDeteta = povreda.Dete.EmailDeteta,
                            PosebnePotrebe = povreda.Dete.PosebnePotrebe
                        } : null,
                        Aktivnost = povreda.Aktivnost != null ? new AktivnostPregled
                        {
                            Id = povreda.Aktivnost.IdAktivnosti,
                            Tip = povreda.Aktivnost.Tip,
                            Naziv = povreda.Aktivnost.Naziv,
                            Datum = povreda.Aktivnost.Datum,
                            StarosnaGrupa = povreda.Aktivnost.StarosnaGrupa,
                            MaxUcesnika = povreda.Aktivnost.MaxUcesnika,
                            Ogranicenja = povreda.Aktivnost.Ogranicenja,
                            PrevoznoSredstvo = povreda.Aktivnost.PrevoznoSredstvo,
                            PlanPuta = povreda.Aktivnost.PlanPuta,
                            PotrebnaOprema = povreda.Aktivnost.PotrebnaOprema,
                            Vodic = povreda.Aktivnost.Vodic,
                            Sport = povreda.Aktivnost.Sport,
                            PosebnaOprema = povreda.Aktivnost.PosebnaOprema
                        } : null,
                        OdgovornoOsoblje = povreda.OdgovornoOsoblje != null ? new AngazovanoLicePregled
                        {
                            JMBG = povreda.OdgovornoOsoblje.JMBG,
                            Ime = povreda.OdgovornoOsoblje.Ime,
                            Prezime = povreda.OdgovornoOsoblje.Prezime,
                            Pol = povreda.OdgovornoOsoblje.Pol,
                            Adresa = povreda.OdgovornoOsoblje.Adresa,
                            BrojTelefona = povreda.OdgovornoOsoblje.BrojTelefona,
                            Email = povreda.OdgovornoOsoblje.Email,
                            StrucnaSprema = povreda.OdgovornoOsoblje.StrucnaSprema,
                            Volonter = povreda.OdgovornoOsoblje.Volonter,
                            Trener = povreda.OdgovornoOsoblje.Trener,
                            Animator = povreda.OdgovornoOsoblje.Animator,
                            ZdravstveniRadnik = povreda.OdgovornoOsoblje.ZdravstveniRadnik
                        } : null
                    };

                    return pp;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja povrede: " + ex.Message, 500);
            }
        }
        //Mozda i ova =======================================================================
        public static async Task<Result<bool, ErrorMessage>> AddPovredaAsync(PovredaPregled povreda)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                using (ITransaction tx = session.BeginTransaction())
                {
                    // Provera i učitavanje povezanih entiteta
                    var dete = povreda.Dete != null ? await session.GetAsync<Dete>(povreda.Dete.ID) : null;
                    if (dete == null)
                        return GetError("Dete sa zadatim ID-em ne postoji.", 404);

                    var aktivnost = povreda.Aktivnost != null ? await session.GetAsync<Aktivnost>(povreda.Aktivnost.Id) : null;
                    if (aktivnost == null)
                        return GetError("Aktivnost sa zadatim ID-em ne postoji.", 404);

                    var lice = povreda.OdgovornoOsoblje != null ? await session.GetAsync<AngazovanoLice>(povreda.OdgovornoOsoblje.JMBG) : null;
                    if (lice == null)
                        return GetError("Odgovorno lice sa zadatim JMBG ne postoji.", 404);

                    // Kreiranje nove povrede
                    Povreda novaPovreda = new Povreda
                    {
                        Datum = povreda.Datum,
                        Opis = povreda.Opis,
                        PreduzeteMere = povreda.PreduzeteMere,
                        Dete = dete,
                        Aktivnost = aktivnost,
                        OdgovornoOsoblje = lice
                    };

                    await session.SaveAsync(novaPovreda);
                    await tx.CommitAsync();
                }

                return true; // uspešno dodato
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom dodavanja povrede: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> UpdatePovredaAsync(PovredaPregled povreda)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                using (ITransaction tx = session.BeginTransaction())
                {
                    var postojeca = await session.GetAsync<Povreda>(povreda.Id);
                    if (postojeca == null)
                        return GetError("Povreda ne postoji u bazi.", 404);

                    // Ažuriranje osnovnih polja
                    postojeca.Datum = povreda.Datum;
                    postojeca.Opis = povreda.Opis;
                    postojeca.PreduzeteMere = povreda.PreduzeteMere;

                    // Ažuriranje povezanih entiteta
                    if (povreda.Dete != null)
                    {
                        var dete = await session.GetAsync<Dete>(povreda.Dete.ID);
                        if (dete == null)
                            return GetError("Dete sa zadatim ID-em ne postoji.", 404);
                        postojeca.Dete = dete;
                    }

                    if (povreda.Aktivnost != null)
                    {
                        var aktivnost = await session.GetAsync<Aktivnost>(povreda.Aktivnost.Id);
                        if (aktivnost == null)
                            return GetError("Aktivnost sa zadatim ID-em ne postoji.", 404);
                        postojeca.Aktivnost = aktivnost;
                    }

                    if (povreda.OdgovornoOsoblje != null)
                    {
                        var lice = await session.GetAsync<AngazovanoLice>(povreda.OdgovornoOsoblje.JMBG);
                        if (lice == null)
                            return GetError("Odgovorno lice sa zadatim JMBG ne postoji.", 404);
                        postojeca.OdgovornoOsoblje = lice;
                    }

                    await session.UpdateAsync(postojeca);
                    await tx.CommitAsync();
                }

                return true; // uspešno ažurirano
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom ažuriranja povrede: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> DeletePovredaAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                using (ITransaction tx = session.BeginTransaction())
                {
                    var povreda = await session.GetAsync<Povreda>(id);
                    if (povreda == null)
                        return GetError($"Povreda sa ID-em {id} ne postoji.", 404);

                    await session.DeleteAsync(povreda);
                    await tx.CommitAsync();

                    return true; // uspešno obrisano
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom brisanja povrede: " + ex.Message, 500);
            }
        }

        #endregion

        #region Aktivnost

        public static async Task<Result<List<AktivnostPregled>, ErrorMessage>> GetAllAktivnostiAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var aktivnosti = await session.Query<Aktivnost>()
                        .Select(a => new AktivnostPregled
                        {
                            Id = a.IdAktivnosti,
                            Tip = a.Tip,
                            Naziv = a.Naziv,
                            Datum = a.Datum,
                            StarosnaGrupa = a.StarosnaGrupa,
                            MaxUcesnika = a.MaxUcesnika,
                            Ogranicenja = a.Ogranicenja,
                            PrevoznoSredstvo = a.PrevoznoSredstvo,
                            PlanPuta = a.PlanPuta,
                            PotrebnaOprema = a.PotrebnaOprema,
                            Vodic = a.Vodic,
                            Sport = a.Sport,
                            PosebnaOprema = a.PosebnaOprema
                            // Lokacija i Evaluacija se mogu dodati ako je potrebno
                        })
                        .ToListAsync();

                    return aktivnosti;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja aktivnosti: " + ex.Message, 500);
            }
        }

        public static async Task<Result<AktivnostPregled, ErrorMessage>> GetAktivnostAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var aktivnost = await session.GetAsync<Aktivnost>(id);

                    if (aktivnost == null)
                        return GetError($"Aktivnost sa ID-em {id} ne postoji.", 404);

                    var ap = new AktivnostPregled
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
                        PosebnaOprema = aktivnost.PosebnaOprema
                        // Lokacija i Evaluacija se mogu dodati ako je potrebno
                    };

                    return ap;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja aktivnosti: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> AddAktivnostAsync(AktivnostPregled aktivnost)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                using (ITransaction tx = session.BeginTransaction())
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
                        var lokacija = await session.GetAsync<Lokacija>(aktivnost.Lokacija.Naziv);
                        nova.Lokacija = lokacija;
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
                    await tx.CommitAsync();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom dodavanja aktivnosti: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> UpdateAktivnostAsync(AktivnostPregled aktivnost)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                using (ITransaction tx = session.BeginTransaction())
                {
                    var postojeca = await session.GetAsync<Aktivnost>(aktivnost.Id);
                    if (postojeca == null)
                        return GetError("Aktivnost sa zadatim ID-em ne postoji.", 404);

                    // Ažuriranje osnovnih polja
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

                    // Ažuriranje povezane lokacije
                    if (aktivnost.Lokacija != null)
                    {
                        var lokacija = await session.GetAsync<Lokacija>(aktivnost.Lokacija.Naziv);
                        if (lokacija == null)
                            return GetError("Lokacija sa zadatim nazivom ne postoji.", 404);
                        postojeca.Lokacija = lokacija;
                    }

                    await session.UpdateAsync(postojeca);
                    await tx.CommitAsync();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom ažuriranja aktivnosti: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> DeleteAktivnostAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                using (ITransaction tx = session.BeginTransaction())
                {
                    var aktivnost = await session.GetAsync<Aktivnost>(id);

                    if (aktivnost == null)
                        return GetError("Aktivnost sa zadatim ID-jem ne postoji.", 404);

                    await session.DeleteAsync(aktivnost);
                    await tx.CommitAsync();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom brisanja aktivnosti: " + ex.Message, 500);
            }
        }

        #endregion

        #region Evaluacija
        //===================================================================================
        //SUMNJIVO
        //===================================================================================
        public static async Task<Result<List<EvaluacijaPregled>, ErrorMessage>> GetAllEvaluacijeAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var evaluacije = await session.Query<Evaluacija>()
                        .Select(e => new EvaluacijaPregled
                        {
                            Id = e.ID,
                            Ocena = e.Ocena,
                            Datum = e.Datum,
                            Opis = e.Opis,
                            Aktivnost = e.Aktivnost != null ? new AktivnostPregled
                            {
                                Id = e.Aktivnost.IdAktivnosti,
                                Naziv = e.Aktivnost.Naziv,
                                Tip = e.Aktivnost.Tip,
                                Datum = e.Aktivnost.Datum,
                                StarosnaGrupa = e.Aktivnost.StarosnaGrupa,
                                MaxUcesnika = e.Aktivnost.MaxUcesnika,
                                Ogranicenja = e.Aktivnost.Ogranicenja
                            } : null,
                            AngazovanoLice = e.AngazovanoLice != null ? new AngazovanoLicePregled
                            {
                                JMBG = e.AngazovanoLice.JMBG,
                                Ime = e.AngazovanoLice.Ime,
                                Prezime = e.AngazovanoLice.Prezime
                            } : null
                        })
                        .ToListAsync();

                    return evaluacije;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja evaluacija: " + ex.Message, 500);
            }
        }

        public static async Task<Result<EvaluacijaPregled, ErrorMessage>> GetEvaluacijaAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var evaluacija = await session.GetAsync<Evaluacija>(id);
                    if (evaluacija == null)
                        return GetError($"Evaluacija sa ID-em {id} ne postoji.", 404);

                    var ep = new EvaluacijaPregled
                    {
                        Id = evaluacija.ID,
                        Ocena = evaluacija.Ocena,
                        Datum = evaluacija.Datum,
                        Opis = evaluacija.Opis,
                        Aktivnost = evaluacija.Aktivnost != null ? new AktivnostPregled
                        {
                            Id = evaluacija.Aktivnost.IdAktivnosti,
                            Naziv = evaluacija.Aktivnost.Naziv,
                            Tip = evaluacija.Aktivnost.Tip,
                            Datum = evaluacija.Aktivnost.Datum,
                            StarosnaGrupa = evaluacija.Aktivnost.StarosnaGrupa,
                            MaxUcesnika = evaluacija.Aktivnost.MaxUcesnika,
                            Ogranicenja = evaluacija.Aktivnost.Ogranicenja
                        } : null,
                        AngazovanoLice = evaluacija.AngazovanoLice != null ? new AngazovanoLicePregled
                        {
                            JMBG = evaluacija.AngazovanoLice.JMBG,
                            Ime = evaluacija.AngazovanoLice.Ime,
                            Prezime = evaluacija.AngazovanoLice.Prezime
                        } : null
                    };

                    return ep;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja evaluacije: " + ex.Message, 500);
            }
        }
        //====================================================================================
        //NISAM SIGURAN DA RADI
        //====================================================================================
        public static async Task<Result<bool, ErrorMessage>> AddEvaluacijaAsync(EvaluacijaPregled evaluacija)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                using (ITransaction tx = session.BeginTransaction())
                {
                    var novaEvaluacija = new Evaluacija
                    {
                        Ocena = evaluacija.Ocena,
                        Datum = evaluacija.Datum,
                        Opis = evaluacija.Opis
                    };

                    // Povezivanje sa entitetima preko ID-a/JMBG
                    if (evaluacija.Aktivnost != null)
                    {
                        var aktivnost = await session.GetAsync<Aktivnost>(evaluacija.Aktivnost.Id);
                        if (aktivnost == null)
                            return GetError("Aktivnost sa zadatim ID-em ne postoji.", 404);
                        novaEvaluacija.Aktivnost = aktivnost;
                    }

                    if (evaluacija.AngazovanoLice != null)
                    {
                        var lice = await session.GetAsync<AngazovanoLice>(evaluacija.AngazovanoLice.JMBG);
                        if (lice == null)
                            return GetError("Angažovano lice sa zadatim JMBG-om ne postoji.", 404);
                        novaEvaluacija.AngazovanoLice = lice;
                    }

                    await session.SaveAsync(novaEvaluacija);
                    await tx.CommitAsync();
                }

                return true; // uspešno dodato
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom dodavanja evaluacije: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> UpdateEvaluacijaAsync(EvaluacijaPregled evaluacija)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                using (ITransaction tx = session.BeginTransaction())
                {
                    var postojeca = await session.GetAsync<Evaluacija>(evaluacija.Id);
                    if (postojeca == null)
                        return GetError("Evaluacija ne postoji u bazi.", 404);

                    // Ažuriranje polja
                    postojeca.Ocena = evaluacija.Ocena;
                    postojeca.Datum = evaluacija.Datum;
                    postojeca.Opis = evaluacija.Opis;

                    // Povezivanje sa entitetima preko ID-a/JMBG
                    if (evaluacija.Aktivnost != null)
                    {
                        var aktivnost = await session.GetAsync<Aktivnost>(evaluacija.Aktivnost.Id);
                        if (aktivnost == null)
                            return GetError("Aktivnost sa zadatim ID-em ne postoji.", 404);
                        postojeca.Aktivnost = aktivnost;
                    }

                    if (evaluacija.AngazovanoLice != null)
                    {
                        var lice = await session.GetAsync<AngazovanoLice>(evaluacija.AngazovanoLice.JMBG);
                        if (lice == null)
                            return GetError("Angažovano lice sa zadatim JMBG-om ne postoji.", 404);
                        postojeca.AngazovanoLice = lice;
                    }

                    await session.UpdateAsync(postojeca);
                    await tx.CommitAsync();
                }

                return true; // uspešno ažurirano
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom ažuriranja evaluacije: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> DeleteEvaluacijaAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                using (ITransaction tx = session.BeginTransaction())
                {
                    var evaluacija = await session.GetAsync<Evaluacija>(id);
                    if (evaluacija == null)
                        return GetError("Evaluacija sa zadatim ID-jem ne postoji.", 404);

                    await session.DeleteAsync(evaluacija);
                    await tx.CommitAsync();
                }

                return true; // uspešno obrisano
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom brisanja evaluacije: " + ex.Message, 500);
            }
        }

        #endregion
        //=========================================================
        //VALJDA RADII
        //=========================================================
        #region Obrok
        public static async Task<Result<List<ObrokPregled>, ErrorMessage>> GetObrociZaDeteAsync(int deteId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var dete = await session.GetAsync<Dete>(deteId);
                    if (dete == null)
                        return GetError("Dete sa datim ID-jem nije pronađeno.", 404);

                    // Eksplicitno učitavanje obroka
                    await NHibernateUtil.InitializeAsync(dete.Obroci);

                    var obroci = dete.Obroci.Select(o => new ObrokPregled
                    {
                        Id = o.ID,
                        Tip = o.Tip,
                        Jelovnik = o.Jelovnik,
                        Uzrast = o.Uzrast,
                        PosebneOpcije = o.PosebneOpcije,
                        Lokacija = o.Lokacija != null ? new LokacijaPregled
                        {
                            Naziv = o.Lokacija.Naziv,
                            Tip = o.Lokacija.Tip,
                            Adresa = o.Lokacija.Adresa,
                            Kapacitet = o.Lokacija.Kapacitet,
                            DostupnaOprema = o.Lokacija.DostupnaOprema
                        } : null,
                        Aktivnost = o.Aktivnost != null ? new AktivnostPregled
                        {
                            Id = o.Aktivnost.IdAktivnosti,
                            Tip = o.Aktivnost.Tip,
                            Naziv = o.Aktivnost.Naziv,
                            Datum = o.Aktivnost.Datum,
                            StarosnaGrupa = o.Aktivnost.StarosnaGrupa,
                            MaxUcesnika = o.Aktivnost.MaxUcesnika,
                            Ogranicenja = o.Aktivnost.Ogranicenja,
                            PrevoznoSredstvo = o.Aktivnost.PrevoznoSredstvo,
                            PlanPuta = o.Aktivnost.PlanPuta,
                            PotrebnaOprema = o.Aktivnost.PotrebnaOprema,
                            Vodic = o.Aktivnost.Vodic,
                            Sport = o.Aktivnost.Sport,
                            PosebnaOprema = o.Aktivnost.PosebnaOprema
                        } : null
                    }).ToList();

                    return obroci;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja obroka deteta: " + ex.Message, 500);
            }
        }

        public static async Task<Result<List<ObrokPregled>, ErrorMessage>> GetAllObrociAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var obroci = await session.Query<Obrok>()
                        .Select(o => new ObrokPregled
                        {
                            Id = o.ID,
                            Tip = o.Tip,
                            Jelovnik = o.Jelovnik,
                            Uzrast = o.Uzrast,
                            PosebneOpcije = o.PosebneOpcije,
                            Lokacija = o.Lokacija != null ? new LokacijaPregled
                            {
                                Naziv = o.Lokacija.Naziv,
                                Tip = o.Lokacija.Tip,
                                Adresa = o.Lokacija.Adresa,
                                Kapacitet = o.Lokacija.Kapacitet,
                                DostupnaOprema = o.Lokacija.DostupnaOprema
                            } : null,
                            Aktivnost = o.Aktivnost != null ? new AktivnostPregled
                            {
                                Id = o.Aktivnost.IdAktivnosti,
                                Tip = o.Aktivnost.Tip,
                                Naziv = o.Aktivnost.Naziv,
                                Datum = o.Aktivnost.Datum,
                                StarosnaGrupa = o.Aktivnost.StarosnaGrupa,
                                MaxUcesnika = o.Aktivnost.MaxUcesnika,
                                Ogranicenja = o.Aktivnost.Ogranicenja,
                                PrevoznoSredstvo = o.Aktivnost.PrevoznoSredstvo,
                                PlanPuta = o.Aktivnost.PlanPuta,
                                PotrebnaOprema = o.Aktivnost.PotrebnaOprema,
                                Vodic = o.Aktivnost.Vodic,
                                Sport = o.Aktivnost.Sport,
                                PosebnaOprema = o.Aktivnost.PosebnaOprema
                            } : null
                        })
                        .ToListAsync();

                    return obroci; // uspešno učitano
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja obroka: " + ex.Message, 500);
            }
        }

        // DataProvider
        public static async Task<Result<ObrokPregled, ErrorMessage>> GetObrokAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var obrok = await session.GetAsync<Obrok>(id);
                    if (obrok == null)
                        return GetError($"Obrok sa ID-em {id} ne postoji.", 404);

                    // Mapiranje u ObrokPregled
                    var op = new ObrokPregled
                    {
                        Id = obrok.ID,
                        Tip = obrok.Tip,
                        Jelovnik = obrok.Jelovnik,
                        Uzrast = obrok.Uzrast,
                        PosebneOpcije = obrok.PosebneOpcije,
                        Lokacija = obrok.Lokacija != null ? new LokacijaPregled
                        {
                            Naziv = obrok.Lokacija.Naziv,
                            Tip = obrok.Lokacija.Tip,
                            Adresa = obrok.Lokacija.Adresa,
                            Kapacitet = obrok.Lokacija.Kapacitet,
                            DostupnaOprema = obrok.Lokacija.DostupnaOprema
                        } : null,
                        Aktivnost = obrok.Aktivnost != null ? new AktivnostPregled
                        {
                            Id = obrok.Aktivnost.IdAktivnosti,
                            Naziv = obrok.Aktivnost.Naziv,
                            Tip = obrok.Aktivnost.Tip,
                            Datum = obrok.Aktivnost.Datum,
                            StarosnaGrupa = obrok.Aktivnost.StarosnaGrupa,
                            MaxUcesnika = obrok.Aktivnost.MaxUcesnika,
                            Ogranicenja = obrok.Aktivnost.Ogranicenja
                        } : null
                    };

                    return op;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja obroka: " + ex.Message, 500);
            }
        }
        //==============================================================
        //Ne znam dal radi lepo zbog ovih veza
        //==============================================================
        public static async Task<Result<bool, ErrorMessage>> AddObrokAsync(ObrokPregled obrok)
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

                    return true; // uspešno dodato
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom dodavanja obroka: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> UpdateObrokAsync(ObrokPregled obrok)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                using (ITransaction tx = session.BeginTransaction())
                {
                    var postojeci = await session.GetAsync<Obrok>(obrok.Id);
                    if (postojeci == null)
                        return GetError("Obrok sa zadatim ID-jem ne postoji.", 404);

                    postojeci.Tip = obrok.Tip;
                    postojeci.Uzrast = obrok.Uzrast;
                    postojeci.Jelovnik = obrok.Jelovnik;
                    postojeci.PosebneOpcije = obrok.PosebneOpcije;

                    if (obrok.Lokacija != null)
                    {
                        var lokacija = await session.GetAsync<Lokacija>(obrok.Lokacija.Naziv);
                        if (lokacija == null)
                            return GetError("Lokacija sa zadatim nazivom ne postoji.", 404);
                        postojeci.Lokacija = lokacija;
                    }

                    if (obrok.Aktivnost != null)
                    {
                        var aktivnost = await session.GetAsync<Aktivnost>(obrok.Aktivnost.Id);
                        if (aktivnost == null)
                            return GetError("Aktivnost sa zadatim ID-jem ne postoji.", 404);
                        postojeci.Aktivnost = aktivnost;
                    }

                    await session.UpdateAsync(postojeci);
                    await tx.CommitAsync();
                }

                return true; // uspešno ažurirano
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom ažuriranja obroka: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> DeleteObrokAsync(int id)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                using (ITransaction tx = session.BeginTransaction())
                {
                    var obrok = await session.GetAsync<Obrok>(id);
                    if (obrok == null)
                        return GetError("Obrok sa zadatim ID-jem ne postoji.", 404);

                    await session.DeleteAsync(obrok);
                    await tx.CommitAsync();
                }

                return true; // uspešno obrisano
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom brisanja obroka: " + ex.Message, 500);
            }
        }
        //===================================================================================================
        //ODLICNO RADI proverava i uzrast, i da li dete postoji i sve
        //===================================================================================================
        public static async Task<Result<bool, ErrorMessage>> DodeliObrokDetetuAsync(int deteId, int obrokId)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                using (ITransaction tx = session.BeginTransaction())
                {
                    var dete = await session.GetAsync<Dete>(deteId);
                    if (dete == null)
                        return GetError("Dete nije pronađeno.", 404);

                    var obrok = await session.GetAsync<Obrok>(obrokId);
                    if (obrok == null)
                        return GetError("Obrok nije pronađen.", 404);

                    // Provera uzrasta
                    var starost = DateTime.Now.Year - dete.DatumRodjenja.Year;
                    if (DateTime.Now.Month < dete.DatumRodjenja.Month ||
                        (DateTime.Now.Month == dete.DatumRodjenja.Month && DateTime.Now.Day < dete.DatumRodjenja.Day))
                        starost--;

                    if (obrok.Uzrast != "Svi")
                    {
                        var uzrastDelovi = obrok.Uzrast.Split('-');
                        if (uzrastDelovi.Length == 2 &&
                            int.TryParse(uzrastDelovi[0], out int minUzrast) &&
                            int.TryParse(uzrastDelovi[1], out int maxUzrast))
                        {
                            if (starost < minUzrast || starost > maxUzrast)
                                return GetError($"Obrok nije predviđen za uzrast deteta. Detetu je {starost} godina.", 400);
                        }
                        else
                            return GetError("Format uzrasta nije ispravan.", 400);
                    }

                    // Provera posebnih potreba
                    if (!string.IsNullOrEmpty(dete.PosebnePotrebe))
                    {
                        var posebnePotrebe = dete.PosebnePotrebe.ToLower();
                        var opcije = obrok.PosebneOpcije?.ToLower() ?? "";

                        if (posebnePotrebe.Contains("laktoza") && !opcije.Contains("bez mlečnih proizvoda"))
                            return GetError("Dete ima alergiju na laktozu, pa ne može dobiti obrok sa mlečnim proizvodima.", 400);
                        if (posebnePotrebe.Contains("gluten") && !opcije.Contains("bez glutena"))
                            return GetError("Dete ima alergiju na gluten, pa ne može dobiti obrok sa glutenom.", 400);
                        if (posebnePotrebe.Contains("vegetarijanski") && !opcije.Contains("vegetarijanski"))
                            return GetError("Dete ima vegetarijansku ishranu, pa ne može dobiti obrok sa mesom.", 400);
                    }

                    // Provera da li je već dodeljeno
                    if (!obrok.Deca.Contains(dete))
                    {
                        obrok.Deca.Add(dete);
                    }
                    else
                    {
                        return GetError("Ovaj obrok je već dodeljen ovom detetu.", 400);
                    }

                    await tx.CommitAsync();
                }

                return true; // uspešno dodeljeno
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom dodele obroka: " + ex.Message, 500);
            }
        }

        #endregion

        #region AngazovanoLice

        public static async Task<Result<List<AngazovanoLicePregled>, ErrorMessage>> GetAllAngazovanaLicaAsync()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var lica = await session.Query<AngazovanoLice>()
                        .Select(al => new AngazovanoLicePregled
                        {
                            JMBG = al.JMBG,
                            Ime = al.Ime,
                            Prezime = al.Prezime,
                            Pol = al.Pol,
                            Adresa = al.Adresa,
                            BrojTelefona = al.BrojTelefona,
                            Email = al.Email,
                            StrucnaSprema = al.StrucnaSprema,
                            Volonter = al.Volonter,
                            Trener = al.Trener,
                            Animator = al.Animator,
                            ZdravstveniRadnik = al.ZdravstveniRadnik,
                            // Ne učitavamo veze: Aktivnosti, Evaluacije, Povrede
                            Aktivnosti = null,
                            Evaluacija = null,
                            Povrede = null
                        })
                        .ToListAsync();

                    return lica;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja angažovanih lica: " + ex.Message, 500);
            }
        }
        //=================================================================================
        //MOZE I NA OVAJ NACIN DA VRACA I SVE VEZE ALI NE ZNAM DA LI TO ZAPRAVO ZELIMO 
        //=================================================================================

        //public static async Task<Result<List<AngazovanoLicePregled>, ErrorMessage>> GetAllAngazovanaLicaAsync()
        //{
        //    try
        //    {
        //        using (ISession session = DataLayer.GetSession())
        //        {
        //            var lica = await session.Query<AngazovanoLice>()
        //                .Select(al => new AngazovanoLicePregled
        //                {
        //                    JMBG = al.JMBG,
        //                    Ime = al.Ime,
        //                    Prezime = al.Prezime,
        //                    Pol = al.Pol,
        //                    Adresa = al.Adresa,
        //                    BrojTelefona = al.BrojTelefona,
        //                    Email = al.Email,
        //                    StrucnaSprema = al.StrucnaSprema,
        //                    Volonter = al.Volonter,
        //                    Trener = al.Trener,
        //                    Animator = al.Animator,
        //                    ZdravstveniRadnik = al.ZdravstveniRadnik,
        //                    Aktivnosti = al.Aktivnosti.Select(a => new AktivnostPregled
        //                    {
        //                        Id = a.IdAktivnosti,
        //                        Tip = a.Tip,
        //                        Naziv = a.Naziv,
        //                        Datum = a.Datum,
        //                        StarosnaGrupa = a.StarosnaGrupa,
        //                        MaxUcesnika = a.MaxUcesnika,
        //                        Ogranicenja = a.Ogranicenja,
        //                        PrevoznoSredstvo = a.PrevoznoSredstvo,
        //                        PlanPuta = a.PlanPuta,
        //                        PotrebnaOprema = a.PotrebnaOprema,
        //                        Vodic = a.Vodic,
        //                        Sport = a.Sport,
        //                        PosebnaOprema = a.PosebnaOprema,
        //                        Lokacija = a.Lokacija != null ? new LokacijaPregled
        //                        {
        //                            Naziv = a.Lokacija.Naziv,
        //                            Tip = a.Lokacija.Tip,
        //                            Adresa = a.Lokacija.Adresa,
        //                            Kapacitet = a.Lokacija.Kapacitet,
        //                            DostupnaOprema = a.Lokacija.DostupnaOprema
        //                        } : null,
        //                        Evaluacija = a.Evaluacija != null ? new EvaluacijaPregled
        //                        {
        //                            Id = a.Evaluacija.ID,
        //                            Ocena = a.Evaluacija.Ocena,
        //                            Datum = a.Evaluacija.Datum,
        //                            Opis = a.Evaluacija.Opis
        //                        } : null
        //                    }).ToList(),
        //                    Evaluacija = al.Evaluacija != null ? new EvaluacijaPregled
        //                    {
        //                        Id = al.Evaluacija.ID,
        //                        Ocena = al.Evaluacija.Ocena,
        //                        Datum = al.Evaluacija.Datum,
        //                        Opis = al.Evaluacija.Opis
        //                    } : null,
        //                    Povrede = al.Povrede.Select(p => new PovredaPregled
        //                    {
        //                        Id = p.ID,
        //                        Datum = p.Datum,
        //                        Opis = p.Opis,
        //                        PreduzeteMere = p.PreduzeteMere,
        //                        Dete = p.Dete != null ? new DetePregled
        //                        {
        //                            Id = p.Dete.IdDeteta,
        //                            Ime = p.Dete.Ime,
        //                            Prezime = p.Dete.Prezime
        //                        } : null,
        //                        Aktivnost = p.Aktivnost != null ? new AktivnostPregled
        //                        {
        //                            Id = p.Aktivnost.IdAktivnosti,
        //                            Naziv = p.Aktivnost.Naziv
        //                        } : null,
        //                        OdgovornoOsoblje = p.OdgovornoOsoblje != null ? new AngazovanoLicePregled
        //                        {
        //                            JMBG = p.OdgovornoOsoblje.JMBG,
        //                            Ime = p.OdgovornoOsoblje.Ime,
        //                            Prezime = p.OdgovornoOsoblje.Prezime
        //                        } : null
        //                    }).ToList()
        //                }).ToListAsync();

        //            return lica;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetError("Došlo je do greške prilikom učitavanja angažovanih lica: " + ex.Message, 500);
        //    }
        //}

        public static async Task<Result<AngazovanoLicePregled, ErrorMessage>> GetAngazovanoLiceAsync(string jmbg)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var lice = await session.GetAsync<AngazovanoLice>(jmbg);

                    if (lice == null)
                        return GetError($"Angažovano lice sa JMBG-om {jmbg} ne postoji.", 404);

                    var alb = new AngazovanoLicePregled
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
                        // Bez Evaluacija, Aktivnosti i Povrede
                    };

                    return alb;
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom učitavanja angažovanog lica: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> AddAngazovanoLiceAsync(AngazovanoLicePregled lice)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    // Provera obaveznih polja
                    if (string.IsNullOrWhiteSpace(lice.JMBG) || string.IsNullOrWhiteSpace(lice.Ime) || string.IsNullOrWhiteSpace(lice.Prezime))
                        return GetError("JMBG, Ime i Prezime su obavezni.", 400);

                    // Provera da li lice već postoji
                    var postoji = await session.GetAsync<AngazovanoLice>(lice.JMBG);
                    if (postoji != null)
                        return GetError($"Angažovano lice sa JMBG-om {lice.JMBG} već postoji.", 403);

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

                    return true; // uspešno dodato
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom dodavanja angažovanog lica: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> UpdateAngazovanoLiceAsync(AngazovanoLicePregled lice)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var postojeci = await session.GetAsync<AngazovanoLice>(lice.JMBG);
                    if (postojeci == null)
                        return GetError($"Angažovano lice sa JMBG-om {lice.JMBG} ne postoji.", 404);

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

                    return true; // uspešno ažurirano
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom ažuriranja angažovanog lica: " + ex.Message, 500);
            }
        }

        public static async Task<Result<bool, ErrorMessage>> DeleteAngazovanoLiceAsync(string jmbg)
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    var lice = await session.GetAsync<AngazovanoLice>(jmbg);
                    if (lice == null)
                        return GetError($"Angažovano lice sa JMBG-om {jmbg} ne postoji.", 404);

                    await session.DeleteAsync(lice);
                    await session.FlushAsync();

                    return true; // uspešno obrisano
                }
            }
            catch (Exception ex)
            {
                return GetError("Došlo je do greške prilikom brisanja angažovanog lica: " + ex.Message, 500);
            }
        }


        #endregion

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
