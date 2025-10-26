using System;
using System.Collections.Generic;

namespace Deciji_Letnji_Program
{
    public class DTOs
    {
        #region Dete

        public class DetePregled
        {
            public int ID { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public DateTime DatumRodjenja { get; set; }
            public char Pol { get; set; }
            public string Adresa { get; set; }
            public string TelefonDeteta { get; set; }
            public string EmailDeteta { get; set; }
            public string PosebnePotrebe { get; set; }
            public string PunoIme => $"{Ime} {Prezime}";

            public DetePregled() { }

            public DetePregled(DetePregled? d)
            {
                if( d != null)
                {
                    this.ID = d.ID;
                    this.Ime = d.Ime;
                    this.Prezime = d.Prezime;
                    this.DatumRodjenja = d.DatumRodjenja;
                    this.Pol = d.Pol;
                    this.Adresa = d.Adresa; 
                    this.TelefonDeteta = d.TelefonDeteta;
                    this.EmailDeteta = d.EmailDeteta;
                    this.PosebnePotrebe = d.PosebnePotrebe; 
                 
                }    
            }
        }

     

        #endregion

        #region Roditelj

        public class RoditeljPregled
        {
            public int Id { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public IList<DetePregled>? Deca { get; set; }
            public IList<PrijavaPregled>? Prijave { get; set; }
            public IList<UcestvujePregled>? Ucestvuje { get; set; }

            public string ImePrezime => $"{Ime} {Prezime}";

            public RoditeljPregled() 
            {
                Deca = new List<DetePregled>();
                Prijave = new List<PrijavaPregled>();
                Ucestvuje = new List<UcestvujePregled>();
            }

            public RoditeljPregled(RoditeljPregled? r)
            {
                this.Id = r.Id;
                this.Ime = r.Ime;
                this.Prezime = r.Prezime; 
            }
        }

     

        #endregion

        #region Povreda

        public class PovredaPregled
        {
            public int Id { get; set; }
            public DateTime Datum { get; set; }
            public string  PreduzeteMere { get; set; }
            public string Opis { get; set; }
            public DetePregled? Dete {  get; set; }
            public AktivnostPregled? Aktivnost { get; set; }
            public AngazovanoLicePregled? OdgovornoOsoblje { get; set; }
            public PovredaPregled() { }

            public PovredaPregled(PovredaPregled? p)
            {
                this.Id = p.Id;
                this.Datum = p.Datum;
                this.Opis = p.Opis;
                this.PreduzeteMere = p.PreduzeteMere;
            }
        }

        

        #endregion

        #region Prijava

        public class PrijavaPregled
        {
            public int IdPrijave { get; set; }
            public DateTime DatumPrijave { get; set; }
            public string Status { get; set; }
            public AktivnostPregled Aktivnost { get; set; }
            public RoditeljPregled Roditelj { get; set; }
            public DetePregled Dete { get; set; }
            public PrijavaPregled() { }
            public PrijavaPregled(PrijavaPregled? p)
            {
                this.IdPrijave = p.IdPrijave;
                this.DatumPrijave = p.DatumPrijave;
                this.Status = p.Status;
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
            public string  PosebneOpcije { get; set; }
            public LokacijaPregled? Lokacija { get; set; }
            public AktivnostPregled? Aktivnost { get; set; }
            public ObrokPregled() { }
            public ObrokPregled(ObrokPregled? o)
            {
                this.Id = o.Id;
                this.Tip = o.Tip;
                this.Jelovnik = o.Jelovnik;
                this.Uzrast = o.Uzrast;
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
            public string StarosnaGrupa { get; set; }
            public int MaxUcesnika { get; set; }
            public string Ogranicenja { get; set; }
            public string PrevoznoSredstvo { get; set; }
            public string PlanPuta { get; set; }
            public string PotrebnaOprema { get; set; }
            public string Vodic { get; set; }
            public string Sport { get; set; }
            public string PosebnaOprema { get; set; }
            public LokacijaPregled? Lokacija { get; set; }
            public EvaluacijaPregled? Evaluacija { get; set; }
            public AktivnostPregled() { }
            public AktivnostPregled(AktivnostPregled? a)
            {
                this.Id = a.Id;
                this.Tip = a.Tip;
                this.Naziv = a.Naziv;
                this.Datum = a.Datum;
                this.StarosnaGrupa = a.StarosnaGrupa;
                this.MaxUcesnika = a.MaxUcesnika;
                this.Ogranicenja = a.Ogranicenja;
                this.PrevoznoSredstvo = a.PrevoznoSredstvo;
                this.PlanPuta = a.PlanPuta;
                this.PotrebnaOprema = a.PotrebnaOprema;
                this.Vodic = a.Vodic;
                this.Sport = a.Sport;
                this.PosebnaOprema = a.PosebnaOprema;
            }
        }

      

        #endregion

        #region AngazovanoLice

        public class AngazovanoLicePregled
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
            public IList<AktivnostPregled>? Aktivnosti { get; set; }
            public EvaluacijaPregled? Evaluacija { get; set; }
            public IList<PovredaPregled>? Povrede { get; set; }
            public AngazovanoLicePregled() { }
            public AngazovanoLicePregled(AngazovanoLicePregled? a)
            {
                this.JMBG = a.JMBG;
                this.Ime = a.Ime;
                this.Prezime = a.Prezime;
                this.Pol = a.Pol;
                this.Adresa = a.Adresa; 
                this.BrojTelefona = a.BrojTelefona;
                this.Email = a.Email;
                this.StrucnaSprema = a.StrucnaSprema;
                this.Volonter = a.Volonter;
                this.Trener = a.Trener;
                this.Animator = a.Animator;
                this.ZdravstveniRadnik = a.ZdravstveniRadnik;
            }
        }

      

        #endregion

        #region Lokacija

        public class LokacijaPregled
        {
            public string Naziv { get; set; }
            public string Tip { get; set; }
            public string Adresa { get; set; }
            public int Kapacitet { get; set; }
            public string DostupnaOprema { get; set; }
            public IList<ObrokPregled>? Obroci { get; set; }
            public IList<AktivnostPregled>? Aktivnosti { get; set; }
            public LokacijaPregled() { }
            public LokacijaPregled(LokacijaPregled? l)
            {
                this.Naziv = l.Naziv;
                this.Tip = l.Tip;
                this.Adresa = l.Adresa;
                this.Kapacitet = l.Kapacitet;
                this.DostupnaOprema = l.DostupnaOprema;
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
            public AktivnostPregled? Aktivnost { get; set; }
            public AngazovanoLicePregled? AngazovanoLice { get; set; }
            public EvaluacijaPregled() { }
            public EvaluacijaPregled(EvaluacijaPregled? e)
            {
                this.Id = e.Id;
                this.Ocena = e.Ocena;
                this.Datum = e.Datum;
                this.Opis = e.Opis;
            }
        }
        #endregion

        #region TelefonRoditelja

        public class TelefonRoditeljaPregled
        {
            public int Id { get; set; }
            public string Telefon { get; set; }
            public DetePregled? Dete { get; set; }

            public TelefonRoditeljaPregled() { }
            public TelefonRoditeljaPregled(TelefonRoditeljaPregled? t)
            {
                this.Id = t.Id;
                this.Telefon = t.Telefon;
            }
        }

        #endregion

        #region EmailRoditelja

        public class EmailRoditeljaPregled
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public DetePregled? Dete { get; set; }
            public EmailRoditeljaPregled() { }
            public EmailRoditeljaPregled(EmailRoditeljaPregled? e)
            {
                this.Id = e.Id;
                this.Email = e.Email;
            }
        }

        #endregion

        #region Ucestvuje

        public class UcestvujePregled
        {
            public int ID { get; set; }
            public string Prisustvo { get; set; }
            public int OcenaAktivnosti { get; set; }
            public string Komentari { get; set; }
            public string Pratilac { get; set; }
            public DetePregled? Dete { get; set; }
            public RoditeljPregled? Roditelj { get; set; }
            public AktivnostPregled? Aktivnost { get; set; }
            public UcestvujePregled() { }
            public UcestvujePregled(UcestvujePregled? u)
            {
                this.ID = u.ID;
                this.Prisustvo = u.Prisustvo;
                this.OcenaAktivnosti = u.OcenaAktivnosti;
                this.Komentari = u.Komentari;
                this.Pratilac = u.Pratilac;
            }
              
        }

       

        #endregion

    }
}
