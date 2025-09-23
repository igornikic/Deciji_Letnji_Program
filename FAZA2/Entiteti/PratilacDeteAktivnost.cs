using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Entiteti
{
    public class PratilacDeteAktivnost
    {
        // Composite Key: PratilacId + DeteId + AktivnostId
        public int PratilacId { get; set; }
        public Pratilac Pratilac { get; set; }

        public int DeteId { get; set; }
        public Dete Dete { get; set; }

        public int AktivnostId { get; set; }
        public Aktivnost Aktivnost { get; set; }

        public PratilacDeteAktivnost()
        {

        }
    }

}
