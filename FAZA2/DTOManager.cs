using Deciji_Letnji_Program.Entiteti;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using static Deciji_Letnji_Program.DTOs;
using System.Windows.Forms;

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
                        PosebnePotrebe = dete.PosebnePotrebe
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
                        Deca = roditelj.Deca.Select(d => new DeteBasic
                        {
                            Id = d.ID,
                            Ime = d.Ime,
                            Prezime = d.Prezime,
                            DatumRodjenja = d.DatumRodjenja,
                            Pol = d.Pol,
                            Adresa = d.Adresa,
                            TelefonDeteta = d.TelefonDeteta,
                            EmailDeteta = d.EmailDeteta,
                            PosebnePotrebe = d.PosebnePotrebe
                        }).ToList(),
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


        //        #region Pratilac

        //        public static void DodajPratioca(PratilacBasic pratilac)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                Pratilac p = new Pratilac();

        //                p.Ime = pratilac.Ime;
        //                p.Prezime = pratilac.Prezime;
        //                p.Pol = pratilac.Pol;
        //                p.BrojTelefona = pratilac.BrojTelefona;

        //                if (pratilac.Dete != null)
        //                {
        //                    p.Dete = s.Load<Dete>(pratilac.Dete.Id);
        //                }

        //                if (pratilac.Aktivnost != null)
        //                {
        //                    p.Aktivnost = s.Load<Aktivnost>(pratilac.Aktivnost.Id);
        //                }

        //                s.Save(p);
        //                s.Flush();
        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //        }

        //        public static PratilacBasic VratiPratioca(int id)
        //        {
        //            PratilacBasic pratilac = new PratilacBasic();
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                Pratilac p = s.Load<Pratilac>(id);

        //                pratilac = new PratilacBasic(
        //                    p.Id, p.Ime, p.Prezime, p.Pol, p.BrojTelefona
        //                );

        //                if (p.Dete != null)
        //                {
        //                    pratilac.Dete = new DetePregled(p.Dete.Id, p.Dete.Ime, p.Dete.Prezime, p.Dete.DatumRodjenja, p.Dete.Pol);
        //                }

        //                if (p.Aktivnost != null)
        //                {
        //                    pratilac.Aktivnost = new AktivnostPregled(p.Aktivnost.Id,p.Aktivnost.Tip, p.Aktivnost.Naziv, p.Aktivnost.Datum);
        //                }

        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //            return pratilac;
        //        }

        //        public static List<PratilacPregled> VratiSvePratice()
        //        {
        //            List<PratilacPregled> pratioci = new List<PratilacPregled>();
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                IEnumerable<Pratilac> sviPratioci = from p in s.Query<Pratilac>()
        //                                                    select p;

        //                foreach (Pratilac p in sviPratioci)
        //                {
        //                    pratioci.Add(new PratilacPregled(p.Id, p.Ime, p.Prezime, p.Pol));
        //                }

        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //            return pratioci;
        //        }

        //        public static Pratilac AzurirajPratioca(int id, string ime, string prezime, char pol, string brojTelefona, int? idDeteta, int? idAktivnosti)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                Pratilac p = s.Load<Pratilac>(id);

        //                p.Ime = string.IsNullOrEmpty(ime) ? p.Ime : ime;
        //                p.Prezime = string.IsNullOrEmpty(prezime) ? p.Prezime : prezime;
        //                p.Pol = pol;
        //                p.BrojTelefona = string.IsNullOrEmpty(brojTelefona) ? p.BrojTelefona : brojTelefona;

        //                if (idDeteta.HasValue)
        //                {
        //                    p.Dete = s.Load<Dete>(idDeteta.Value);
        //                }

        //                if (idAktivnosti.HasValue)
        //                {
        //                    p.Aktivnost = s.Load<Aktivnost>(idAktivnosti.Value);
        //                }

        //                s.Update(p);
        //                s.Flush();
        //                s.Close();

        //                return p;
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //                return null;
        //            }
        //        }

        //        public static void ObrisiPratioca(int id)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                Pratilac p = s.Load<Pratilac>(id);
        //                s.Delete(p);
        //                s.Flush();
        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //        }

        //        #endregion

        //        #region Povreda

        //        public static void DodajPovredu(PovredaBasic povreda)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                Povreda p = new Povreda
        //                {
        //                    Datum = povreda.Datum,
        //                    PreduzeteMere = povreda.PreduzeteMere,
        //                    Opis = povreda.Opis,
        //                    Dete = s.Load<Dete>(povreda.Dete.Id),
        //                    Aktivnost = s.Load<Aktivnost>(povreda.Aktivnost.Id),
        //                    OdgovornoOsoblje = s.Load<AngazovanoLice>(povreda.OdgovornoOsoblje.JMBG)
        //                };

        //                s.Save(p);
        //                s.Flush();
        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //        }

        //        public static PovredaBasic VratiPovredu(int id)
        //        {
        //            PovredaBasic povreda = new PovredaBasic();
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                Povreda p = s.Load<Povreda>(id);
        //                povreda = new PovredaBasic(p.Id, p.Datum, p.PreduzeteMere, p.Opis)
        //                {
        //                    Dete = new DetePregled(p.Dete.Id, p.Dete.Ime, p.Dete.Prezime, p.Dete.DatumRodjenja, p.Dete.Pol),
        //                    Aktivnost = new AktivnostPregled(p.Aktivnost.Id,p.Aktivnost.Tip, p.Aktivnost.Naziv, p.Aktivnost.Datum),
        //                    OdgovornoOsoblje = new AngazovanoLicePregled(p.OdgovornoOsoblje.JMBG, p.OdgovornoOsoblje.Ime, p.OdgovornoOsoblje.Prezime)
        //                };

        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //            return povreda;
        //        }

        //        public static List<PovredaPregled> VratiPovrede()
        //        {
        //            List<PovredaPregled> povrede = new List<PovredaPregled>();
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                IEnumerable<Povreda> sviPovrede = from o in s.Query<Povreda>()
        //                                                  select o;

        //                foreach (Povreda p in sviPovrede)
        //                {
        //                    povrede.Add(new PovredaPregled(p.Id, p.Datum, p.Opis));
        //                }

        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //            return povrede;
        //        }

        //        public static Povreda AzurirajPovredu(int id, DateTime datum, string preduzeteMere, string opis)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();
        //                Povreda p = s.Load<Povreda>(id);

        //                p.Datum = datum != default(DateTime) ? datum : p.Datum;
        //                p.PreduzeteMere = !string.IsNullOrEmpty(preduzeteMere) ? preduzeteMere : p.PreduzeteMere;
        //                p.Opis = !string.IsNullOrEmpty(opis) ? opis : p.Opis;

        //                s.Update(p);
        //                s.Flush();
        //                s.Close();
        //                return p;
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //                return null;
        //            }
        //        }

        //        public static void ObrisiPovredu(int id)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                Povreda povreda = s.Load<Povreda>(id);
        //                s.Delete(povreda);
        //                s.Flush();
        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //        }

        //        #endregion

        //        #region Prijava

        //        public static void DodajPrijavu(PrijavaBasic prijava)
        //        {
        //            try
        //            {
        //                using (ISession s = DataLayer.GetSession())
        //                {
        //                    Prijava p = new Prijava
        //                    {
        //                        DatumPrijave = prijava.DatumPrijave,
        //                        Status = prijava.Status
        //                    };

        //                    if (prijava.Aktivnost != null)
        //                        p.Aktivnost = s.Load<Aktivnost>(prijava.Aktivnost.Id);

        //                    if (prijava.Roditelj != null)
        //                        p.Roditelj = s.Load<Roditelj>(prijava.Roditelj.Id);

        //                    if (prijava.Dete != null)
        //                        p.Dete = s.Load<Dete>(prijava.Dete.Id);

        //                    s.Save(p);
        //                    s.Flush();
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                MessageBox.Show(e.Message);
        //            }
        //        }

        //        public static PrijavaBasic VratiPrijavu(int id)
        //        {
        //            PrijavaBasic prijava = new PrijavaBasic();
        //            try
        //            {
        //                using (ISession s = DataLayer.GetSession())
        //                {
        //                    Prijava p = s.Load<Prijava>(id);
        //                    prijava = new PrijavaBasic(p.IdPrijave, p.DatumPrijave, p.Status)
        //                    {
        //                        Aktivnost = p.Aktivnost != null ? new AktivnostPregled(p.Aktivnost.Id,p.Aktivnost.Tip, p.Aktivnost.Naziv,p.Aktivnost.Datum) : null,
        //                        Roditelj = p.Roditelj != null ? new RoditeljPregled(p.Roditelj.Id, p.Roditelj.Ime, p.Roditelj.Prezime) : null,
        //                        Dete = p.Dete != null ? new DetePregled(p.Dete.Id,p.Dete.Ime, p.Dete.Prezime, p.Dete.DatumRodjenja, p.Dete.Pol) : null
        //                    };
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                MessageBox.Show(e.Message);
        //            }
        //            return prijava;
        //        }

        //        public static List<PrijavaPregled> VratiPrijave()
        //        {
        //            List<PrijavaPregled> prijave = new List<PrijavaPregled>();
        //            try
        //            {
        //                using (ISession s = DataLayer.GetSession())
        //                {
        //                    IEnumerable<Prijava> sviPrijave = from o in s.Query<Prijava>()
        //                                                      select o;

        //                    foreach (Prijava p in sviPrijave)
        //                    {
        //                        prijave.Add(new PrijavaPregled(p.IdPrijave, p.DatumPrijave, p.Status));
        //                    }
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                MessageBox.Show(e.Message);
        //            }
        //            return prijave;
        //        }

        //        public static Prijava AzurirajPrijavu(int idPrijave, DateTime datumPrijave, string status)
        //        {
        //            Prijava prijava = null;
        //            try
        //            {
        //                using (ISession s = DataLayer.GetSession())
        //                {
        //                    prijava = s.Load<Prijava>(idPrijave);
        //                    prijava.DatumPrijave = datumPrijave == DateTime.MinValue ? prijava.DatumPrijave : datumPrijave;
        //                    prijava.Status = string.IsNullOrEmpty(status) ? prijava.Status : status;

        //                    s.Update(prijava);
        //                    s.Flush();
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                MessageBox.Show(e.Message);
        //            }
        //            return prijava;
        //        }

        //        public static void ObrisiPrijavu(int id)
        //        {
        //            try
        //            {
        //                using (ISession s = DataLayer.GetSession())
        //                {
        //                    Prijava prijava = s.Load<Prijava>(id);
        //                    s.Delete(prijava);
        //                    s.Flush();
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                MessageBox.Show(e.Message);
        //            }
        //        }

        //        #endregion

        //        #region EvidencijaPrisustva

        //        public static void DodajEvidencijuPrisustva(EvidencijaPrisustvaBasic evidencija)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                EvidencijaPrisustva e = new EvidencijaPrisustva
        //                {
        //                    Prisustvo = evidencija.Prisustvo,
        //                    OcenaAktivnosti = evidencija.OcenaAktivnosti,
        //                    Sugestije = evidencija.Sugestije
        //                };

        //                if (evidencija.Dete != null)
        //                    e.Dete = s.Load<Dete>(evidencija.Dete.Id);

        //                if (evidencija.Aktivnost != null)
        //                    e.Aktivnost = s.Load<Aktivnost>(evidencija.Aktivnost.Id);

        //                s.Save(e);
        //                s.Flush();
        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //        }

        //        public static EvidencijaPrisustvaBasic VratiEvidencijuPrisustva(int id)
        //        {
        //            EvidencijaPrisustvaBasic evidencija = new EvidencijaPrisustvaBasic();
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();
        //                EvidencijaPrisustva e = s.Load<EvidencijaPrisustva>(id);

        //                DetePregled dete = e.Dete != null ? new DetePregled(e.Dete.Id, e.Dete.Ime, e.Dete.Prezime, e.Dete.DatumRodjenja, e.Dete.Pol) : null;
        //                AktivnostPregled aktivnost = e.Aktivnost != null ? new AktivnostPregled(e.Aktivnost.Id,e.Aktivnost.Tip, e.Aktivnost.Naziv, e.Aktivnost.Datum) : null;

        //                evidencija = new EvidencijaPrisustvaBasic(e.Id, e.Prisustvo, e.OcenaAktivnosti, e.Sugestije)
        //                {
        //                    Dete = dete,
        //                    Aktivnost = aktivnost
        //                };

        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //            return evidencija;
        //        }

        //        public static List<EvidencijaPrisustvaPregled> VratiEvidencijePrisustva()
        //        {
        //            List<EvidencijaPrisustvaPregled> evidencije = new List<EvidencijaPrisustvaPregled>();
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();
        //                IEnumerable<EvidencijaPrisustva> sviEvidencije = from e in s.Query<EvidencijaPrisustva>()
        //                                                                 select e;

        //                foreach (EvidencijaPrisustva e in sviEvidencije)
        //                {
        //                    evidencije.Add(new EvidencijaPrisustvaPregled(e.Id, e.Prisustvo, e.OcenaAktivnosti));
        //                }

        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //            return evidencije;
        //        }

        //        public static void AzurirajEvidencijuPrisustva(int id, char prisustvo, int ocenaAktivnosti, string sugestije)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();
        //                EvidencijaPrisustva e = s.Load<EvidencijaPrisustva>(id);

        //                e.Prisustvo = prisustvo;
        //                e.OcenaAktivnosti = ocenaAktivnosti;
        //                e.Sugestije = sugestije;

        //                s.Update(e);
        //                s.Flush();
        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //        }

        //        public static void ObrisiEvidencijuPrisustva(int id)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();
        //                EvidencijaPrisustva e = s.Load<EvidencijaPrisustva>(id);

        //                s.Delete(e);
        //                s.Flush();
        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //        }

        //        #endregion

        //        #region Obrok

        //        public static void DodajObrok(ObrokBasic obrok)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();
        //                Obrok o = new Obrok();

        //                o.Tip = obrok.Tip;
        //                o.Uzrast = obrok.Uzrast;
        //                o.Jelovnik = obrok.Jelovnik;
        //                o.PosebneOpcije = obrok.PosebneOpcije;

        //                if (obrok.Lokacija != null)
        //                {
        //                    o.Lokacija = s.Load<Lokacija>(obrok.Lokacija.Naziv);
        //                }

        //                if (obrok.Aktivnost != null)
        //                {
        //                    o.Aktivnost = s.Load<Aktivnost>(obrok.Aktivnost.Id);
        //                }

        //                s.Save(o);
        //                s.Flush();
        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //        }

        //        public static ObrokBasic VratiObrok(int id)
        //        {
        //            ObrokBasic obrok = new ObrokBasic();
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();
        //                Obrok o = s.Load<Obrok>(id);

        //                LokacijaPregled lokacija = null;
        //                AktivnostPregled aktivnost = null;

        //                if (o.Lokacija != null)
        //                {
        //                    lokacija = new LokacijaPregled(o.Lokacija.Naziv, o.Lokacija.Naziv);
        //                }

        //                if (o.Aktivnost != null)
        //                {
        //                    aktivnost = new AktivnostPregled(o.Aktivnost.Id,o.Aktivnost.Tip, o.Aktivnost.Naziv, o.Aktivnost.Datum);
        //                }

        //                obrok = new ObrokBasic(o.Id, o.Tip, o.Uzrast, o.Jelovnik, o.PosebneOpcije)
        //                {
        //                    Lokacija = lokacija,
        //                    Aktivnost = aktivnost
        //                };

        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }

        //            return obrok;
        //        }

        //        public static List<ObrokPregled> VratiSveObroke()
        //        {
        //            List<ObrokPregled> obroci = new List<ObrokPregled>();
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                IEnumerable<Obrok> sviObroci = from o in s.Query<Obrok>()
        //                                               select o;

        //                foreach (Obrok o in sviObroci)
        //                {
        //                    obroci.Add(new ObrokPregled(o.Id, o.Tip, o.Uzrast));
        //                }

        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }

        //            return obroci;
        //        }

        //        public static Obrok AzurirajObrok(int idObroka, string tip, string uzrast, string jelovnik, string posebneOpcije)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();
        //                Obrok o = s.Load<Obrok>(idObroka);

        //                o.Tip = (tip == "" ? o.Tip : tip);
        //                o.Uzrast = (uzrast == "" ? o.Uzrast : uzrast);
        //                o.Jelovnik = (jelovnik == "" ? o.Jelovnik : jelovnik);
        //                o.PosebneOpcije = (posebneOpcije == "" ? o.PosebneOpcije : posebneOpcije);

        //                s.Update(o);
        //                s.Flush();
        //                s.Close();
        //                return o;
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //                return null;
        //            }
        //        }

        //        public static void ObrisiObrok(int id)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();
        //                Obrok obrok = s.Load<Obrok>(id);

        //                s.Delete(obrok);
        //                s.Flush();
        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //        }

        //        #endregion

        //        #region Aktivnost

        //            public static void DodajAktivnost(AktivnostBasic aktivnost)
        //            {
        //                try
        //                {
        //                    ISession s = DataLayer.GetSession();
        //                    Aktivnost a = new Aktivnost
        //                    {
        //                        Tip = aktivnost.Tip,
        //                        Naziv = aktivnost.Naziv,
        //                        Datum = aktivnost.Datum,
        //                        StarosnaGrupa = aktivnost.StarosnaGrupa,
        //                        MaxUcesnika = aktivnost.MaxUcesnika,
        //                        Ogranicenja = aktivnost.Ogranicenja,
        //                        Lokacija = s.Load<Lokacija>(aktivnost.Lokacija.Naziv)
        //                    };
        //                    s.Save(a);
        //                    s.Flush();
        //                    s.Close();
        //                }
        //                catch (Exception ec)
        //                {
        //                    MessageBox.Show(ec.Message);
        //                }
        //            }

        //            public static AktivnostBasic VratiAktivnost(int id)
        //            {
        //                AktivnostBasic aktivnost = new AktivnostBasic();
        //                try
        //                {
        //                    ISession s = DataLayer.GetSession();
        //                    Aktivnost a = s.Load<Aktivnost>(id);

        //                    aktivnost = new AktivnostBasic(
        //                        a.Id,
        //                        a.Tip,
        //                        a.Naziv,
        //                        a.Datum,
        //                        a.StarosnaGrupa,
        //                        a.MaxUcesnika,
        //                        a.Ogranicenja
        //                    );

        //                    aktivnost.Lokacija = new LokacijaPregled
        //                    {
        //                        Naziv = a.Lokacija.Naziv
        //                    };

        //                    s.Close();
        //                }
        //                catch (Exception ec)
        //                {
        //                    MessageBox.Show(ec.Message);
        //                }
        //                return aktivnost;
        //            }

        //            public static List<AktivnostPregled> VratiSveAktivnosti()
        //            {
        //                List<AktivnostPregled> aktivnosti = new List<AktivnostPregled>();
        //                try
        //                {
        //                    ISession s = DataLayer.GetSession();
        //                    IEnumerable<Aktivnost> sviAktivnosti = from a in s.Query<Aktivnost>()
        //                                                           select a;

        //                    foreach (Aktivnost a in sviAktivnosti)
        //                    {
        //                        aktivnosti.Add(new AktivnostPregled(
        //                            a.Id,
        //                            a.Tip,
        //                            a.Naziv,
        //                            a.Datum
        //                        ));
        //                    }

        //                    s.Close();
        //                }
        //                catch (Exception ec)
        //                {
        //                    MessageBox.Show(ec.Message);
        //                }

        //                return aktivnosti;
        //            }

        //            public static Aktivnost AzurirajAktivnost(int id, string tip, string naziv, DateTime? datum, string starosnaGrupa, int maxUcesnika, string ogranicenja)
        //            {
        //                try
        //                {
        //                    ISession s = DataLayer.GetSession();
        //                    Aktivnost a = s.Load<Aktivnost>(id);

        //                    a.Tip = (tip == "" ? a.Tip : tip);
        //                    a.Naziv = (naziv == "" ? a.Naziv : naziv);
        //                    a.Datum = datum.HasValue ? datum : a.Datum;
        //                    a.StarosnaGrupa = (starosnaGrupa == "" ? a.StarosnaGrupa : starosnaGrupa);
        //                    a.MaxUcesnika = (maxUcesnika == 0 ? a.MaxUcesnika : maxUcesnika);
        //                    a.Ogranicenja = (ogranicenja == "" ? a.Ogranicenja : ogranicenja);

        //                    s.Update(a);
        //                    s.Flush();
        //                    s.Close();

        //                    return a;
        //                }
        //                catch (Exception ec)
        //                {
        //                    MessageBox.Show(ec.Message);
        //                    return null;
        //                }
        //            }

        //            public static void ObrisiAktivnost(int id)
        //            {
        //                try
        //                {
        //                    ISession s = DataLayer.GetSession();
        //                    Aktivnost a = s.Load<Aktivnost>(id);
        //                    s.Delete(a);
        //                    s.Flush();
        //                    s.Close();
        //                }
        //                catch (Exception ec)
        //                {
        //                    MessageBox.Show(ec.Message);
        //                }
        //            }

        //        #endregion

        //        #region AngazovanoLice

        //        public static void DodajAngazovanoLice(AngazovanoLiceBasic angazovanoLice)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                AngazovanoLice al = new AngazovanoLice();

        //                al.Ime = angazovanoLice.Ime;
        //                al.Prezime = angazovanoLice.Prezime;
        //                al.Pol = angazovanoLice.Pol;
        //                al.Adresa = angazovanoLice.Adresa;
        //                al.BrojTelefona = angazovanoLice.BrojTelefona;
        //                al.Email = angazovanoLice.Email;
        //                al.StrucnaSprema = angazovanoLice.StrucnaSprema;
        //                al.OblastRada = angazovanoLice.OblastRada;

        //                s.Save(al);
        //                s.Flush();
        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //        }

        //        public static AngazovanoLiceBasic VratiAngazovanoLice(string jmbg)
        //        {
        //            AngazovanoLiceBasic angazovanoLice = new AngazovanoLiceBasic();
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();
        //                AngazovanoLice al = s.Load<AngazovanoLice>(jmbg);

        //                angazovanoLice = new AngazovanoLiceBasic(
        //                    al.JMBG, al.Ime, al.Prezime, al.Pol, al.Adresa, al.BrojTelefona, al.Email, al.StrucnaSprema, al.OblastRada
        //                );

        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }

        //            return angazovanoLice;
        //        }

        //        public static List<AngazovanoLicePregled> VratiSvaAngazovanaLica()
        //        {
        //            List<AngazovanoLicePregled> angazovanaLica = new List<AngazovanoLicePregled>();
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                IEnumerable<AngazovanoLice> sviAngazovani = from o in s.Query<AngazovanoLice>()
        //                                                            select o;

        //                foreach (AngazovanoLice al in sviAngazovani)
        //                {
        //                    angazovanaLica.Add(new AngazovanoLicePregled(al.JMBG, al.Ime, al.Prezime));
        //                }

        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }

        //            return angazovanaLica;
        //        }

        //        public static AngazovanoLice AzurirajAngazovanoLice(string jmbg, string ime, string prezime, char pol, string adresa,
        //            string brojTelefona, string email, string strucnaSprema, string oblastRada)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();
        //                AngazovanoLice al = s.Load<AngazovanoLice>(jmbg);

        //                al.Ime = (ime == "" ? al.Ime : ime);
        //                al.Prezime = (prezime == "" ? al.Prezime : prezime);
        //                al.Pol = pol;
        //                al.Adresa = (adresa == "" ? al.Adresa : adresa);
        //                al.BrojTelefona = (brojTelefona == "" ? al.BrojTelefona : brojTelefona);
        //                al.Email = (email == "" ? al.Email : email);
        //                al.StrucnaSprema = (strucnaSprema == "" ? al.StrucnaSprema : strucnaSprema);
        //                al.OblastRada = (oblastRada == "" ? al.OblastRada : oblastRada);

        //                s.Update(al);
        //                s.Flush();
        //                s.Close();
        //                return al;
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //                return null;
        //            }
        //        }

        //        public static void ObrisiAngazovanoLice(string jmbg)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                AngazovanoLice al = s.Load<AngazovanoLice>(jmbg);

        //                s.Delete(al);
        //                s.Flush();
        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //        }

        //        #endregion

        //        #region Lokacija
        //        public static void DodajLokaciju(LokacijaBasic lokacija)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                Lokacija l = new Lokacija()
        //                {
        //                    Naziv = lokacija.Naziv,
        //                    Tip = lokacija.Tip,
        //                    Adresa = lokacija.Adresa,
        //                    Kapacitet = lokacija.Kapacitet,
        //                    DostupnaOprema = lokacija.DostupnaOprema
        //                };

        //                s.Save(l);
        //                s.Flush();
        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //        }

        //        public static LokacijaBasic VratiLokaciju(string naziv)
        //        {
        //            LokacijaBasic lokacija = new LokacijaBasic();
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();
        //                Lokacija l = s.Load<Lokacija>(naziv);

        //                lokacija = new LokacijaBasic(l.Naziv, l.Tip, l.Adresa, l.Kapacitet, l.DostupnaOprema);

        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //            return lokacija;
        //        }

        //        public static List<LokacijaPregled> VratiSveLokacije()
        //        {
        //            List<LokacijaPregled> lokacije = new List<LokacijaPregled>();
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                IEnumerable<Lokacija> sveLokacije = from o in s.Query<Lokacija>()
        //                                                    select o;

        //                foreach (Lokacija l in sveLokacije)
        //                {
        //                    lokacije.Add(new LokacijaPregled(l.Naziv, l.Tip));
        //                }

        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }

        //            return lokacije;
        //        }

        //        public static Lokacija AzurirajLokaciju(string naziv, string tip, string adresa, int kapacitet, string dostupnaOprema)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                Lokacija l = s.Load<Lokacija>(naziv);

        //                l.Tip = (tip == "" ? l.Tip : tip);
        //                l.Adresa = (adresa == "" ? l.Adresa : adresa);
        //                l.Kapacitet = (kapacitet == 0 ? l.Kapacitet : kapacitet);
        //                l.DostupnaOprema = (dostupnaOprema == "" ? l.DostupnaOprema : dostupnaOprema);

        //                s.Update(l);
        //                s.Flush();
        //                s.Close();
        //                return l;
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //                return null;
        //            }
        //        }

        //        public static void ObrisiLokaciju(string naziv)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                Lokacija l = s.Load<Lokacija>(naziv);

        //                s.Delete(l);
        //                s.Flush();
        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //        }

        //        #endregion

        //        #region Evaluacija
        //        public static void DodajEvaluaciju(EvaluacijaBasic evaluacija)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();

        //                Evaluacija e = new Evaluacija
        //                {
        //                    Ocena = evaluacija.Ocena,
        //                    Datum = evaluacija.Datum,
        //                    Opis = evaluacija.Opis
        //                };

        //                if (evaluacija.Aktivnost != null)
        //                {
        //                    Aktivnost a = s.Load<Aktivnost>(evaluacija.Aktivnost.Id);
        //                    e.Aktivnost = a;
        //                }

        //                if (evaluacija.AngazovanoLice != null)
        //                {
        //                    AngazovanoLice al = s.Load<AngazovanoLice>(evaluacija.AngazovanoLice.JMBG);
        //                    e.AngazovanoLice = al;
        //                }

        //                s.Save(e);
        //                s.Flush();
        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //        }

        //        public static EvaluacijaBasic VratiEvaluaciju(int id)
        //        {
        //            EvaluacijaBasic evaluacija = new EvaluacijaBasic();
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();
        //                Evaluacija e = s.Load<Evaluacija>(id);

        //                EvaluacijaBasic eb = new EvaluacijaBasic
        //                {
        //                    Id = e.Id,
        //                    Ocena = e.Ocena,
        //                    Datum = e.Datum,
        //                    Opis = e.Opis,
        //                    Aktivnost = e.Aktivnost != null ? new AktivnostPregled(e.Aktivnost.Id,e.Aktivnost.Tip, e.Aktivnost.Naziv, e.Aktivnost.Datum) : null,
        //                    AngazovanoLice = e.AngazovanoLice != null ? new AngazovanoLicePregled(e.AngazovanoLice.JMBG, e.AngazovanoLice.Ime, e.AngazovanoLice.Prezime) : null
        //                };

        //                evaluacija = eb;

        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //            return evaluacija;
        //        }

        //        public static List<EvaluacijaPregled> VratiSveEvaluacije()
        //        {
        //            List<EvaluacijaPregled> evaluacije = new List<EvaluacijaPregled>();
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();
        //                IEnumerable<Evaluacija> sveEvaluacije = from e in s.Query<Evaluacija>()
        //                                                        select e;

        //                foreach (Evaluacija e in sveEvaluacije)
        //                {
        //                    evaluacije.Add(new EvaluacijaPregled
        //                    {
        //                        Id = e.Id,
        //                        Ocena = e.Ocena,
        //                        Datum = e.Datum,
        //                        Opis = e.Opis
        //                    });
        //                }

        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }

        //            return evaluacije;
        //        }

        //        public static Evaluacija AzurirajEvaluaciju(int id, int ocena, DateTime datum, string opis)
        //        {
        //            Evaluacija evaluacija = null;
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();
        //                evaluacija = s.Load<Evaluacija>(id);

        //                evaluacija.Ocena = ocena != 0 ? ocena : evaluacija.Ocena;
        //                evaluacija.Datum = datum != DateTime.MinValue ? datum : evaluacija.Datum;
        //                evaluacija.Opis = !string.IsNullOrEmpty(opis) ? opis : evaluacija.Opis;

        //                s.Update(evaluacija);
        //                s.Flush();

        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //            return evaluacija;
        //        }

        //        public static void ObrisiEvaluaciju(int id)
        //        {
        //            try
        //            {
        //                ISession s = DataLayer.GetSession();
        //                Evaluacija evaluacija = s.Load<Evaluacija>(id);

        //                s.Delete(evaluacija);
        //                s.Flush();

        //                s.Close();
        //            }
        //            catch (Exception ec)
        //            {
        //                MessageBox.Show(ec.Message);
        //            }
        //        }

        //#endregion

    }
}
