using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Entiteti
{
    public class JeDat
    {
        public virtual int ID { get; set; }

        public virtual Dete Dete { get; set; }
        public virtual Obrok Obrok { get; set; }
    }
}
