using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Starateljstvo
    {
        public virtual int ID { get; set; }

        public virtual Roditelj Roditelj { get; set; }
        public virtual Dete Dete { get; set; }
    }
}
