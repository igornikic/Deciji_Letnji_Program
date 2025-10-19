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

            public string ImePrezime => $"{Ime} {Prezime}";

            public RoditeljPregled() { }

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
            public string Opis { get; set; }

            public PovredaPregled() { }

            public PovredaPregled(PovredaPregled? p)
            {
                this.Id = p.Id;
                this.Datum = p.Datum;
                this.Opis = p.Opis;

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

            public AktivnostPregled() { }
            public AktivnostPregled(AktivnostPregled? a)
            {
                this.Id = a.Id;
                this.Tip = a.Tip;
                this.Naziv = a.Naziv;
                this.Datum = a.Datum;
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
            public AngazovanoLicePregled(AngazovanoLicePregled? a)
            {
                this.JMBG = a.JMBG;
                this.Ime = a.Ime;
                this.Prezime = a.Prezime;
            }
        }

      

        #endregion

        #region Lokacija

        public class LokacijaPregled
        {
            public string Naziv { get; set; }
            public string Tip { get; set; }

            public LokacijaPregled() { }
            public LokacijaPregled(LokacijaPregled? l)
            {
                this.Naziv = l.Naziv;
                this.Tip = l.Tip;
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
            public string OcenaAktivnosti { get; set; }
            public string Komentari { get; set; }

            public UcestvujePregled() { }
            public UcestvujePregled(UcestvujePregled? u)
            {
                this.ID = u.ID;
                this.OcenaAktivnosti = u.OcenaAktivnosti;
                this.Komentari = u.Komentari;
            }
              
        }

       

        #endregion

    }
}
