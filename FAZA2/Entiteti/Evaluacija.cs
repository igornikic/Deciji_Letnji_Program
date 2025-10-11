using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Evaluacija
    {
        public virtual int ID { get; set; }
        public virtual int Ocena { get; set; }
        public virtual DateTime Datum { get; set; }
        public virtual string Opis { get; set; }


        public Aktivnost Aktivnost { get; set; }  // FK za Aktivnost
        public AngazovanoLice AngazovanoLice { get; set; } // FK za AngazovanoLice

        public Evaluacija() 
        {
        
        }
    }

}
