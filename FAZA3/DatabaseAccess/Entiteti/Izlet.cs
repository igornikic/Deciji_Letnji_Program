using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Izlet: Aktivnost
    {
        public virtual string PrevoznoSredstvo { get; set; }
        public virtual string PlanPuta { get; set; }
        public virtual string PotrebnaOprema { get; set; }
        public virtual string Vodic { get; set; }
        public virtual string Obroci { get; set; }

        public Izlet()
        {
        }
    }

}
