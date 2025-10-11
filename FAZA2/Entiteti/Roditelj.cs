using System;
using System.Collections.Generic;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Roditelj
    {
        public virtual int ID { get; protected set; }
        public virtual string Ime { get; set; }
        public virtual string Prezime { get; set; }

        public virtual IList<Dete> Deca { get; set; } // N:M
        
        public virtual IList<Prijava> Prijave { get; set; } // 1:N
        public virtual IList<Ucestvuje> Ucestvuje { get; set; }
        public Roditelj()
        {
            Deca = new List<Dete>();
            Prijave = new List<Prijava>();
            Ucestvuje = new List<Ucestvuje>();
        }
    }
}
