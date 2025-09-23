using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Evaluacija
    {
        public int Id { get; set; }
        public int Ocena { get; set; }
        public DateTime Datum { get; set; }
        public string Opis { get; set; }


        public Aktivnost Aktivnost { get; set; }  // FK za Aktivnost

    
        public AngazovanoLice AngazovanoLice { get; set; } // FK za AngazovanoLice

        public Evaluacija() 
        {
        
        }
    }

}
