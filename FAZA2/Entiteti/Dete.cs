using System;
using System.Collections.Generic;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Dete
    {
        public virtual int ID { get; protected set; }
        public virtual string Ime { get; set; }
        public virtual string Prezime { get; set; }
        public virtual char Pol { get; set; }
        public virtual DateTime DatumRodjenja { get; set; }
        public virtual string Adresa { get; set; }
        public virtual string TelefonDeteta { get; set; }
        public virtual string EmailDeteta { get; set; }
        public virtual string PosebnePotrebe { get; set; }


        // 1:N veza sa prijavom preko PRIJAVLJUJE_SE
        public virtual IList<Prijava> Prijava { get; set; }
        // M:N veza sa roditeljima preko STARATELJSTVO
        public virtual IList<Roditelj> Roditelji { get; set; }

        // 1:N veza sa povredama
        public virtual IList<Povreda> Povrede { get; set; }

        // M:N veza sa obrocima preko JE_DAT
        public virtual IList<Obrok> Obroci { get; set; }

        // N:M veza sa aktivnostima preko UCESTVUJE
        public virtual IList<Ucestvuje> Ucestvuje { get; set; }

        // Telefoni roditelja - relacija iz TELEFON
        public virtual IList<TelefonRoditelja> TelefoniRoditelja { get; set; }

        // Emailovi roditelja - relacija iz EMAIL
        public virtual IList<EmailRoditelja> EmailoviRoditelja { get; set; }

        public Dete()
        {
            Roditelji = new List<Roditelj>();
            Povrede = new List<Povreda>();
            Obroci = new List<Obrok>();
            Ucestvuje = new List<Ucestvuje>();
            TelefoniRoditelja = new List<TelefonRoditelja>();
            EmailoviRoditelja = new List<EmailRoditelja>();
        }
    }
}
