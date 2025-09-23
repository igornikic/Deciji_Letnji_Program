using System;
using System.Collections.Generic;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Aktivnost
    {
        public virtual int Id { get; protected set; }
        public virtual string Tip { get; set; }
        public virtual string Naziv { get; set; }
        public virtual DateTime? Datum { get; set; }
        public virtual string StarosnaGrupa { get; set; }
        public virtual int MaxUcesnika { get; set; }
        public virtual string Ogranicenja { get; set; }
        public virtual Lokacija Lokacija { get; set; } // FK za Lokaciju

        public virtual IList<Prijava> Prijave { get; set; } // 1:N
        public virtual IList<EvidencijaPrisustva> EvidencijaPrisustva { get; set; } // 1:N
        public virtual IList<PratilacDeteAktivnost> PratilacDeteAktivnosti { get; set; } // N:M
        public virtual Evaluacija Evaluacija { get; set; }  // 1:1
        public virtual IList<Povreda> Povrede { get; set; } // 1:N
        public virtual IList<Ucesce> Ucesca { get; set; } // N:M

        public virtual IList<Obrok> Obrok { get; set; } // 1:N

        public Aktivnost()
        {
            Prijave = new List<Prijava>();
            EvidencijaPrisustva = new List<EvidencijaPrisustva>();
            PratilacDeteAktivnosti = new List<PratilacDeteAktivnost>();
            Povrede = new List<Povreda>();
            Ucesca = new List<Ucesce>();
            Obrok = new List<Obrok>();
        }
    }
}
