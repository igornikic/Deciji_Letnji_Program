using System;
using System.Collections.Generic;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Obrok
    {
        public virtual int Id { get; protected set; }
        public virtual string Tip { get; set; }
        public virtual string Uzrast { get; set; }
        public virtual string Jelovnik { get; set; }
        public virtual string PosebneOpcije { get; set; }
        public virtual Lokacija Lokacija { get; set; } // FK ka Lokaciji
        public virtual Aktivnost Aktivnost { get; set; } // FK ka Aktivnosti

        public virtual IList<Dete> Deca { get; set; } // N:M

        public Obrok()
        {
            Deca = new List<Dete>();
        }
    }
}
