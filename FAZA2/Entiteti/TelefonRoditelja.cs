using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Entiteti
{
    public class TelefonRoditelja
    {
        public string Telefon { get; set; }


        public int RoditeljId { get; set; }
        public Roditelj Roditelj { get; set; }
    }
}
