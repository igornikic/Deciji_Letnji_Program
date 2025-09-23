using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Entiteti
{
    public class AngazovanoLice
    {
        public virtual string JMBG { get; protected set; }
        public virtual string Ime { get; set; }
        public virtual string Prezime { get; set; }
        public virtual char Pol { get; set; }
        public virtual string Adresa { get; set; }
        public virtual string BrojTelefona { get; set; }
        public virtual string Email { get; set; }
        public virtual string StrucnaSprema { get; set; }
        public virtual string OblastRada { get; set; }

        public virtual IList<Ucesce> Ucesca { get; set; } // 1:N
        public virtual Evaluacija Evaluacija { get; set; }  // 1:1
        public virtual IList<Povreda> Povrede { get; set; } // N:M

        public AngazovanoLice()
        {
            Ucesca = new List<Ucesce>();
            Povrede = new List<Povreda>();
        }
    }
}
