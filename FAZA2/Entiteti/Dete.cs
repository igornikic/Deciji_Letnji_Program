using System;
using System.Collections.Generic;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Dete
    {
        public virtual int Id { get; protected set; }
        public virtual string Ime { get; set; }
        public virtual string Prezime { get; set; }
        public virtual DateTime DatumRodjenja { get; set; }
        public virtual string Adresa { get; set; }
        public virtual string TelefonDeteta { get; set; }
        public virtual string EmailDeteta { get; set; }
        public virtual string PosebnePotrebe { get; set; }

        // M:N veza sa roditeljima preko STARATELJSTVO
        public virtual IList<Roditelj> Roditelji { get; set; }

        // 1:N veza sa povredama
        public virtual IList<Povreda> Povrede { get; set; }

        // M:N veza sa obrocima preko JE_DAT
        public virtual IList<Obrok> Obroci { get; set; }

        // N:M veza sa aktivnostima preko UCESTVUJE
        public virtual IList<Ucesce> Ucesca { get; set; }

        // Telefoni roditelja - relacija iz TELEFON
        public virtual IList<TelefonRoditelja> TelefoniRoditelja { get; set; }

        // Emailovi roditelja - relacija iz EMAIL
        public virtual IList<EmailRoditelja> EmailoviRoditelja { get; set; }

        public Dete()
        {
            Staratelji = new List<Roditelj>();
            Povrede = new List<Povreda>();
            Obroci = new List<Obrok>();
            Ucesca = new List<Ucesce>();
            TelefoniRoditelja = new List<Telefon>();
            EmailoviRoditelja = new List<Email>();
        }
    }
}
