using System;
using System.Collections.Generic;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Aktivnost
    {
        public virtual int ID_aktivnosti { get; protected set; }
        public virtual string Tip { get; set; }
        public virtual string Naziv { get; set; }
        public virtual DateTime? Datum { get; set; }
        public virtual string Starosna_grupa { get; set; }
        public virtual int Max_ucesnika { get; set; }
        public virtual string Ogranicenja { get; set; }
        public virtual string Prevozno_sredstvo { get; set; }
        public virtual string Plan_puta { get; set; }
        public virtual string Potrebna_oprema { get; set; }
        public virtual string Vodic { get; set; }
        public virtual string Obroci { get; set; }
        public virtual string Sport { get; set; }
        public virtual string Posebna_oprema { get; set; }
        public virtual Lokacija Lokacija { get; set; } // FK za Lokaciju

        public virtual IList<Prijava> Prijave { get; set; } // 1:N
        public virtual IList<EvidencijaPrisustva> EvidencijaPrisustva { get; set; } // 1:N
        public virtual IList<Pratilac> Pratioci{ get; set; } // 1:N
        public virtual Evaluacija Evaluacija { get; set; }  // 1:1
        public virtual IList<Povreda> Povrede { get; set; } // 1:N

        public virtual IList<AngazovanoLice> AngazovanaLica { get; set; } // N:M

        public virtual IList<Obrok> Obrok { get; set; } // 1:N

        public Aktivnost()
        {
            Prijave = new List<Prijava>();
            EvidencijaPrisustva = new List<EvidencijaPrisustva>();
            Pratioci = new List<Pratilac>();
            Povrede = new List<Povreda>();
            Obrok = new List<Obrok>();
        }
    }
}
