using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Povreda
    {
        public virtual int ID { get; protected set; }
        public virtual DateTime Datum { get; set; }
        public virtual string Preduzete_mere { get; set; }
        public virtual string Opis { get; set; }
        public virtual Dete Dete { get; set; }                   // FK na Dete
        public virtual Aktivnost Aktivnost { get; set; }         // FK na Aktivnost
        public virtual AngazovanoLice OdgovornoOsoblje { get; set; } // FK na AngazovanoLice

        public Povreda()
        {
           
        }
    }
}
