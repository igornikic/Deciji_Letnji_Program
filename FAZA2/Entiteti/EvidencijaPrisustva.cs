using System;
using System.Collections.Generic;

namespace Deciji_Letnji_Program.Entiteti
{
    public class EvidencijaPrisustva
    {
        public virtual int Id { get; protected set; }
        public virtual char Prisustvo { get; set; }
        public virtual int OcenaAktivnosti { get; set; }
        public virtual string Sugestije { get; set; }

        public virtual Dete Dete { get; set; } // FK ka Dete
        public virtual Aktivnost Aktivnost { get; set; } // FK ka Aktivnost
        public virtual IList<Komentar> Komentari { get; set; } // 1:N

        public EvidencijaPrisustva()
        {
            Komentari = new List<Komentar>();
        }
    }
}