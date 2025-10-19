using System;
using System.Collections.Generic;

namespace Deciji_Letnji_Program
{
    internal class DTOs
    {
        #region Dete

        public class DetePregled
        {
            public int ID { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public DateTime DatumRodjenja { get; set; }
            public char Pol { get; set; }
            public string PunoIme => $"{Ime} {Prezime}";

            public DetePregled() { }

            public DetePregled(int id, string ime, string prezime, DateTime datumRodjenja, char pol)
            {
                ID = id;
                Ime = ime;
                Prezime = prezime;
                DatumRodjenja = datumRodjenja;
                Pol = pol;
            }
        }

        public class DeteBasic
        {
            public int Id { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public DateTime DatumRodjenja { get; set; }
            public char Pol { get; set; }
            public string Adresa { get; set; }
            public string TelefonDeteta { get; set; }
            public string EmailDeteta { get; set; }
            public string PosebnePotrebe { get; set; }

            public IList<string> TelefoniRoditelja { get; set; } = new List<string>();
            public IList<string> EmailoviRoditeja { get; set; } = new List<string>();

            public IList<RoditeljBasic> Roditelji { get; set; } = new List<RoditeljBasic>();
            public IList<PovredaBasic> Povrede { get; set; } = new List<PovredaBasic>();
            public IList<PrijavaBasic> Prijava { get; set; } = new List<PrijavaBasic>();
            public IList<ObrokBasic> Obroci { get; set; } = new List<ObrokBasic>();
            public IList<UcestvujeBasic> Ucestvuje { get; set; } = new List<UcestvujeBasic>();

            public DeteBasic()
            {
                TelefoniRoditelja = new List<string>();
                EmailoviRoditeja = new List<string>();

                Roditelji = new List<RoditeljBasic>();
                Povrede = new List<PovredaBasic>();
                Prijava = new List<PrijavaBasic>();
                Obroci = new List<ObrokBasic>();
                Ucestvuje = new List<UcestvujeBasic>();
            }

            public DeteBasic(int id, string ime, string prezime, DateTime datumRodjenja, char pol,
                string adresa, string telefonDeteta, string emailDeteta, string posebnePotrebe)
            {
                Id = id;
                Ime = ime;
                Prezime = prezime;
                DatumRodjenja = datumRodjenja;
                Pol = pol;
                Adresa = adresa;
                TelefonDeteta = telefonDeteta;
                EmailDeteta = emailDeteta;
                PosebnePotrebe = posebnePotrebe;
            }
        }

        #endregion

        #region Roditelj

        public class RoditeljPregled
        {
            public int Id { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }

            public string ImePrezime => $"{Ime} {Prezime}";

            public RoditeljPregled() { }

            public RoditeljPregled(int id, string ime, string prezime)
            {
                Id = id;
                Ime = ime;
                Prezime = prezime;
            }
        }

        public class RoditeljBasic
        {
            public int Id { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }

            public IList<DeteBasic> Deca { get; set; }
            public IList<PrijavaBasic> Prijava { get; set; }
            public IList<UcestvujeBasic> Ucestvuje { get; set; }

            public RoditeljBasic()
            {
                Deca = new List<DeteBasic>();
                Prijava = new List<PrijavaBasic>();
                Ucestvuje = new List<UcestvujeBasic>();
            }

            public RoditeljBasic(int id, string ime, string prezime)
            {
                Id = id;
                Ime = ime;
                Prezime = prezime;
            }
        }

        #endregion

        #region Povreda

        public class PovredaPregled
        {
            public int Id { get; set; }
            public DateTime Datum { get; set; }
            public string Opis { get; set; }

            public PovredaPregled() { }

            public PovredaPregled(int id, DateTime datum, string opis)
            {
                Id = id;
                Datum = datum;
                Opis = opis;
            }
        }

        public class PovredaBasic
        {
            public int Id { get; set; }
            public DateTime Datum { get; set; }
            public string PreduzeteMere { get; set; }
            public string Opis { get; set; }

            public DeteBasic Dete { get; set; }
            public AktivnostBasic Aktivnost { get; set; }
            public AngazovanoLiceBasic OdgovornoOsoblje { get; set; }

            public PovredaBasic() 
            {
            }

            public PovredaBasic(int id, DateTime datum, string preduzeteMere, string opis,
                DeteBasic dete,AktivnostBasic aktivnost, AngazovanoLiceBasic odgovornoOsoblje)
            {
                Id = id;
                Datum = datum;
                PreduzeteMere = preduzeteMere;
                Opis = opis;
                Dete = dete;
                Aktivnost = aktivnost;
                OdgovornoOsoblje = odgovornoOsoblje;
            }
        }

        #endregion

        #region Prijava

        public class PrijavaPregled
        {
            public int IdPrijave { get; set; }
            public DateTime DatumPrijave { get; set; }
            public string Status { get; set; }

            public PrijavaPregled() { }

            public PrijavaPregled(int idPrijave, DateTime datum, string status)
            {
                IdPrijave = idPrijave;
                DatumPrijave = datum;
                Status = status;
            }
        }

        public class PrijavaBasic
        {
            public int IdPrijave { get; set; }
            public DateTime DatumPrijave { get; set; }
            public string Status { get; set; }

            public AktivnostBasic Aktivnost { get; set; }
            public RoditeljBasic Roditelj { get; set; }
            public DeteBasic Dete { get; set; }

            public PrijavaBasic() { }

            public PrijavaBasic(int idPrijave, DateTime datum, string status,
                AktivnostBasic aktivnost, RoditeljBasic roditelj, DeteBasic dete)
            {
                IdPrijave = idPrijave;
                DatumPrijave = datum;
                Status = status;
                Aktivnost = aktivnost;
                Roditelj = roditelj;
                Dete = dete;
            }
        }

        #endregion

        #region Obrok

        public class ObrokPregled
        {
            public int Id { get; set; }
            public string Tip { get; set; }
            public string Jelovnik { get; set; }
            public string Uzrast { get; set; }

            public ObrokPregled() { }

            public ObrokPregled(int id, string tip, string jelovnik,  string uzrast)
            {
                Id = id;
                Tip = tip;
                Jelovnik = jelovnik;
                Uzrast = uzrast;
            }
        }

        public class ObrokBasic
        {
            public int Id { get; set; }
            public string Tip { get; set; }
            public string Uzrast { get; set; }
            public string Jelovnik { get; set; }
            public string PosebneOpcije { get; set; }

            public LokacijaBasic Lokacija { get; set; }
            public AktivnostBasic Aktivnost { get; set; }

            public IList<DeteBasic> Deca { get; set; }

            public ObrokBasic()
            {
                Deca = new List<DeteBasic>();
            }

            public ObrokBasic(int id, string tip, string uzrast, string jelovnik, string posebneOpcije,
                LokacijaBasic lokacija, AktivnostBasic aktivnost)
            {
                Id = id;
                Tip = tip;
                Uzrast = uzrast;
                Jelovnik = jelovnik;
                PosebneOpcije = posebneOpcije;
                Lokacija = lokacija;
                Aktivnost = aktivnost;
            }
        }

        #endregion

        #region Aktivnost

        public class AktivnostPregled
        {
            public int Id { get; set; }
            public string Tip { get; set; }
            public string Naziv { get; set; }
            public DateTime? Datum { get; set; }

            public AktivnostPregled() { }

            public AktivnostPregled(int id, string tip, string naziv, DateTime? datum)
            {
                Id = id;
                Tip = tip;
                Naziv = naziv;
                Datum = datum;
            }
        }

        public class AktivnostBasic
        {
            public int Id { get; set; }
            public string Tip { get; set; }
            public string Naziv { get; set; }
            public DateTime? Datum { get; set; }
            public string StarosnaGrupa { get; set; }
            public int MaxUcesnika { get; set; }
            public string Ogranicenja { get; set; }
            public string PrevoznoSredstvo { get; set; }
            public string PlanPuta { get; set; }
            public string PotrebnaOprema { get; set; }
            public string Vodic { get; set; }
            public string Sport { get; set; }
            public string PosebnaOprema { get; set; }

            public LokacijaBasic Lokacija { get; set; }
            public EvaluacijaBasic Evaluacija { get; set; }

            public IList<PrijavaBasic> Prijave { get; set; }
            public IList<PovredaBasic> Povrede { get; set; }
            public IList<UcestvujeBasic> Ucestvuje { get; set; }
            public IList<AngazovanoLiceBasic> AngazovanaLica { get; set; }
            public IList<ObrokBasic> Obroci { get; set; }

            public AktivnostBasic()
            {
                Prijave = new List<PrijavaBasic>();
                Ucestvuje = new List<UcestvujeBasic>();
                Povrede = new List<PovredaBasic>();
                AngazovanaLica = new List<AngazovanoLiceBasic>();
                Obroci = new List<ObrokBasic>();
            }

            public AktivnostBasic(int id, string tip, string naziv, DateTime? datum, string starosnaGrupa, int maxUcesnika, string ogranicenja,
                LokacijaBasic lok, EvaluacijaBasic ev)
            {
                Id = id;
                Tip = tip;
                Naziv = naziv;
                Datum = datum;
                StarosnaGrupa = starosnaGrupa;
                MaxUcesnika = maxUcesnika;
                Ogranicenja = ogranicenja;
                Lokacija = lok;
                Evaluacija = ev;
            }
        }

        #endregion

        #region AngazovanoLice

        public class AngazovanoLicePregled
        {
            public string JMBG { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }

            public AngazovanoLicePregled() { }

            public AngazovanoLicePregled(string jmbg, string ime, string prezime)
            {
                JMBG = jmbg;
                Ime = ime;
                Prezime = prezime;
            }
        }

        public class AngazovanoLiceBasic
        {
            public string JMBG { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public char Pol { get; set; }
            public string Adresa { get; set; }
            public string BrojTelefona { get; set; }
            public string Email { get; set; }
            public string StrucnaSprema { get; set; }
            public char Volonter { get; set; }
            public char Trener { get; set; }
            public char Animator { get; set; }
            public char ZdravstveniRadnik { get; set; }

            public IList<AktivnostBasic> Aktivnosti { get; set; }
            public EvaluacijaBasic Evaluacija { get; set; }
            public IList<PovredaBasic> Povrede { get; set; }

            public AngazovanoLiceBasic()
            {
                Aktivnosti = new List<AktivnostBasic>();
                Povrede = new List<PovredaBasic>();
            }

            public AngazovanoLiceBasic(string jmbg, string ime, string prezime, char pol, string adresa,
                                       string brojTelefona, string email, string strucnaSprema, char volonter,
                                       char trener, char animator, char zdravstveniRadnik, EvaluacijaBasic evaluacija)
            {
                JMBG = jmbg;
                Ime = ime;
                Prezime = prezime;
                Pol = pol;
                Adresa = adresa;
                BrojTelefona = brojTelefona;
                Email = email;
                StrucnaSprema = strucnaSprema;
                Volonter = volonter;
                Trener = trener;
                Animator = animator;
                ZdravstveniRadnik = zdravstveniRadnik;
                Evaluacija = evaluacija;
            }
        }

        #endregion

        #region Lokacija

        public class LokacijaPregled
        {
            public string Naziv { get; set; }
            public string Tip { get; set; }

            public LokacijaPregled() { }

            public LokacijaPregled(string naziv, string tip)
            {
                Naziv = naziv;
                Tip = tip;
            }
        }

        public class LokacijaBasic
        {
            public string Naziv { get; set; }
            public string Tip { get; set; }
            public string Adresa { get; set; }
            public int Kapacitet { get; set; }
            public string DostupnaOprema { get; set; }

            public IList<ObrokBasic> Obroci { get; set; }
            public IList<AktivnostBasic> Aktivnosti { get; set; }

            public LokacijaBasic()
            {
                Obroci = new List<ObrokBasic>();
                Aktivnosti = new List<AktivnostBasic>();
            }

            public LokacijaBasic(string naziv, string tip, string adresa, int kapacitet, string dostupnaOprema)
            {
                Naziv = naziv;
                Tip = tip;
                Adresa = adresa;
                Kapacitet = kapacitet;
                DostupnaOprema = dostupnaOprema;
            }
        }

        #endregion

        #region Evaluacija

        public class EvaluacijaPregled
        {
            public int Id { get; set; }
            public int Ocena { get; set; }
            public DateTime Datum { get; set; }
            public string Opis { get; set; }

            public EvaluacijaPregled() { }

            public EvaluacijaPregled(int id, int ocena, DateTime datum, string opis)
            {
                Id = id;
                Ocena = ocena;
                Datum = datum;
                Opis = opis;
            }
        }

        public class EvaluacijaBasic
        {
            public int Id { get; set; }
            public int Ocena { get; set; }
            public DateTime Datum { get; set; }
            public string Opis { get; set; }

            public AktivnostBasic Aktivnost { get; set; }
            public AngazovanoLiceBasic AngazovanoLice { get; set; }

            public EvaluacijaBasic()
            {
            }

            public EvaluacijaBasic(int id, int ocena, DateTime datum, string opis, AktivnostBasic ak, AngazovanoLiceBasic al)
            {
                Id = id;
                Ocena = ocena;
                Datum = datum;
                Opis = opis;
                Aktivnost = ak;
                AngazovanoLice = al;
            }
        }

        #endregion

        #region TelefonRoditelja

        public class TelefonRoditeljaPregled
        {
            public int Id { get; set; }
            public string Telefon { get; set; }

            public TelefonRoditeljaPregled() { }

            public TelefonRoditeljaPregled(int id, string telefon)
            {
                Id = id;
                Telefon = telefon;
            }
        }

        public class TelefonRoditeljaBasic
        {
            public int Id { get; set; }
            public string Telefon { get; set; }

            public DeteBasic Dete { get; set; }

            public TelefonRoditeljaBasic()
            {
            }

            public TelefonRoditeljaBasic(int id, string telefon, DeteBasic dete)
            {
                Id = id;
                Telefon = telefon;
                Dete = dete;
            }
        }

        #endregion

        #region EmailRoditelja

        public class EmailRoditeljaPregled
        {
            public int Id { get; set; }
            public string Email { get; set; }

            public EmailRoditeljaPregled() { }

            public EmailRoditeljaPregled(int id, string email)
            {
                Id = id;
                Email = email;
            }
        }

        public class EmailRoditeljaBasic
        {
            public int Id { get; set; }
            public string Email { get; set; }

            public DeteBasic Dete { get; set; }

            public EmailRoditeljaBasic()
            {
            }

            public EmailRoditeljaBasic(int id, string telefon, DeteBasic dete)
            {
                Id = id;
                Email = telefon;
                Dete = dete;
            }
        }

        #endregion

        #region Ucestvuje

        public class UcestvujePregled
        {
            public int ID { get; set; }
            public string Prisustvo { get; set; }
            public int? OcenaAktivnosti { get; set; }
            public string Komentari { get; set; }
            public string Pratilac { get; set; }

            public UcestvujePregled() { }
            public UcestvujePregled(int id, string prisustvo, int? ocenaAktivnosti, string komentari, string pratilac)
            {
                ID = id;
                Prisustvo = prisustvo;
                OcenaAktivnosti = ocenaAktivnosti;
                Komentari = komentari;
                Pratilac = pratilac;
            }
        }

        public class UcestvujeBasic
        {
            public int ID { get; set; }
            public string Prisustvo { get; set; }
            public int? OcenaAktivnosti { get; set; }
            public string Komentari { get; set; }
            public string Pratilac { get; set; }

            public DeteBasic Dete { get; set; }
            public AktivnostBasic Aktivnost { get; set; }
            public RoditeljBasic Roditelj { get; set; }

            public UcestvujeBasic() { }
            public UcestvujeBasic(int id, string prisustvo, int? ocenaAktivnosti,string komentari,
                string pratilac, DeteBasic dete, AktivnostBasic aktivnost, RoditeljBasic roditelj)
            {
                ID = id;
                Prisustvo = prisustvo;
                OcenaAktivnosti = ocenaAktivnosti;
                Komentari = komentari;
                Pratilac = pratilac;
                Dete = dete;
                Aktivnost = aktivnost;
                Roditelj = roditelj;
            }
        }

        #endregion

    }
}
