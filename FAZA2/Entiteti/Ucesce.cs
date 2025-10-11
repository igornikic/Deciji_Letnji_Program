using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Ucesce
    {
        public virtual int ID { get; set; }
        
        public virtual Aktivnost Aktivnost { get; set; }
        public virtual AngazovanoLice AngazovanoLice { get; set; }
    }
}
