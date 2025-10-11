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
        public virtual string Broj_telefona { get; set; }
        public virtual string Email { get; set; }
        public virtual string Strucna_sprema { get; set; }
        public virtual char Volonter { get; set; }
        public virtual char Trener { get; set; }
        public virtual char Animator { get; set; }
        public virtual char Zdravstveni_radnik { get; set; }

        public virtual IList<Aktivnost> Aktivnosti { get; set; } // M:N
        public virtual Evaluacija Evaluacija { get; set; }  // 1:1
        public virtual IList<Povreda> Povrede { get; set; } // 1:N

        public AngazovanoLice()
        {
            Aktivnosti = new List<Aktivnost>();
            Povrede = new List<Povreda>();
        }
    }
}
