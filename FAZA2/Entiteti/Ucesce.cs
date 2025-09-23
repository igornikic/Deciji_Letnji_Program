using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Ucesce
    {
        // Composite key: AktivnostId + JMBG
        public int AktivnostId { get; set; }
        public Aktivnost Aktivnost { get; set; }

        public string JMBG { get; set; }
        public AngazovanoLice AngazovanoLice { get; set; }
    }
}
