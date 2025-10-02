using System;
using System.Collections.Generic;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Pratilac
    {
        public virtual int Id { get; protected set; }
        public virtual string Ime { get; set; }
        public virtual string Prezime { get; set; }
        public virtual char Pol { get; set; }
        public virtual string BrojTelefona { get; set; }

       
        public virtual Dete Dete { get; set; } //fk za dete
        public virtual Aktivnost Aktivnost { get; set; } // fk za aktivnost
        public Pratilac()
        {
            
        }
    }
}
