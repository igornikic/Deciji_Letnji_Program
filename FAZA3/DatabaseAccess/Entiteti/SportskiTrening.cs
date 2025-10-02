using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Entiteti
{
    public class SportskiTrening: Aktivnost
    {
        public virtual string Sport { get; set; }
        public virtual string PosebnaOprema { get; set; }

        public SportskiTrening()
        {

        }
    }
}
