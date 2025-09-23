using System;
using System.Collections.Generic;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Pratilac
    {
        public virtual int Id { get; protected set; }
        public virtual string Ime { get; set; }
        public virtual string Prezime { get; set; }
        public virtual char Pol { get; set; }
        public virtual string BrojTelefona { get; set; }

        public virtual IList<PratilacDeteAktivnost> PratilacDeteAktivnosti { get; set; } // N:M

        public Pratilac()
        {
            PratilacDeteAktivnosti = new List<PratilacDeteAktivnost>();
        }
    }
}
