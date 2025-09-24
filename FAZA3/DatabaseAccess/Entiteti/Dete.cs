using System;
using System.Collections.Generic;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Dete
    {
        public virtual int Id { get; protected set; }
        public virtual string Ime { get; set; }
        public virtual string Prezime { get; set; }
        public virtual DateTime DatumRodjenja { get; set; }
        public virtual char Pol { get; set; }
        public virtual string Adresa { get; set; }
        public virtual string TelefonDeteta { get; set; }
        public virtual string EmailDeteta { get; set; }
        public virtual string PosebnePotrebe { get; set; }

        public virtual IList<Pratilac> Pratioci { get; set; } // 1:N
        public virtual IList<string> Telefoni { get; set; } // 1:N
        public virtual IList<string> EmailAdrese { get; set; } // 1:N
        public virtual IList<Roditelj> Roditelji { get; set; } // N:M
        public virtual IList<Povreda> Povrede { get; set; } // 1:N
        public virtual IList<Prijava> Prijave { get; set; } // 1:N
        public virtual IList<EvidencijaPrisustva> EvidencijePrisustva { get; set; } // N:M
        public virtual IList<Obrok> Obroci { get; set; } // N:M

        public virtual IList<string> Komentari { get; set; }

        public Dete()
        {
            Roditelji = new List<Roditelj>();
            Povrede = new List<Povreda>();
            Pratioci = new List<Pratilac>();
            Prijave = new List<Prijava>();
            EvidencijePrisustva = new List<EvidencijaPrisustva>();
            Obroci = new List<Obrok>();
            Komentari = new List<string>();
        }
    }
}