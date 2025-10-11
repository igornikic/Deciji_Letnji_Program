using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Entiteti
{
    public class TelefonRoditelja
    {
        public virtual int ID { get; set; }
        public virtual string Telefon { get; set; }

        public virtual Dete Dete { get; set; }
    }
}
