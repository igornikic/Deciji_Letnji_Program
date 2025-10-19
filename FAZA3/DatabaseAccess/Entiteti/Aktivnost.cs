using System;
using System.Collections.Generic;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Aktivnost
    {
        public virtual int IdAktivnosti { get; protected set; }
        public virtual string Tip { get; set; }
        public virtual string Naziv { get; set; }
        public virtual DateTime? Datum { get; set; }
        public virtual string StarosnaGrupa { get; set; }
        public virtual int MaxUcesnika { get; set; }
        public virtual string Ogranicenja { get; set; }
        public virtual string PrevoznoSredstvo { get; set; }
        public virtual string PlanPuta { get; set; }
        public virtual string PotrebnaOprema { get; set; }
        public virtual string Vodic { get; set; }
        public virtual string Sport { get; set; }
        public virtual string PosebnaOprema { get; set; }
        public virtual Lokacija Lokacija { get; set; } // FK za Lokaciju

        public virtual Evaluacija Evaluacija { get; set; }  // 1:1
        public virtual IList<Prijava> Prijave { get; set; } // 1:N
        public virtual IList<Povreda> Povrede { get; set; } // 1:N
        public virtual IList<Obrok> Obrok { get; set; } // 1:N

        public virtual IList<AngazovanoLice> AngazovanaLica { get; set; } // N:M
        public virtual IList<Ucestvuje> Ucestvuju { get; set; } // N:M


        public Aktivnost()
        {
            Prijave = new List<Prijava>();
            Povrede = new List<Povreda>();
            AngazovanaLica = new List<AngazovanoLice>();
            Ucestvuju = new List<Ucestvuje>();
            Obrok = new List<Obrok>();
        }
    }
}
