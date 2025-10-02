using System;
using System.Collections.Generic;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Lokacija
    {
        public virtual string Naziv { get; set; }
        public virtual string Tip { get; set; }
        public virtual string Adresa { get; set; }
        public virtual int Kapacitet { get; set; }
        public virtual string DostupnaOprema { get; set; }


        public virtual IList<Obrok> Obroci { get; set; } // 1:N
        public virtual IList<Aktivnost> Aktivnosti { get; set; } // 1:N

        public Lokacija()
        {
            Obroci = new List<Obrok>();
            Aktivnosti = new List<Aktivnost>();
        }
    }
}
