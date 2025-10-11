using System;
using System.Collections.Generic;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Ucestvuje
    {
        public virtual int ID { get; set; }

        public virtual Roditelj Roditelj { get; set; }
        public virtual Dete Dete { get; set; }
        public virtual Aktivnost Aktivnost { get; set; }

        public virtual string Prisustvo { get; set; }
        public virtual int OcenaAktivnosti { get; set; }
        public virtual string Komentari { get; set; }
        public virtual string Pratilac { get; set; }
    }
}
