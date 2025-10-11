using System;
using System.Collections.Generic;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Prijava
    {
        public virtual int Id_prijave { get; protected set; }
        public virtual DateTime Datum_prijave { get; set; }
        public virtual string Status { get; set; }

        public virtual Aktivnost Aktivnost { get; set; }        // FK Aktivnost
        public virtual Roditelj Roditelj { get; set; }          // FK Roditelj
        public virtual Dete Dete { get; set; }                  // FK Dete

        public Prijava()
        {
           
        }
    }
}
