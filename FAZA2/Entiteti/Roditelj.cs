using System;
using System.Collections.Generic;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Roditelj
    {
        public virtual int Id { get; protected set; }
        public virtual string Ime { get; set; }
        public virtual string Prezime { get; set; }

        public virtual IList<Starateljstvo> Starateljstvo { get; set; } // N:M
        public virtual IList<TelefonRoditelja> Telefoni { get; set; } // 1:N
        public virtual IList<EmailRoditelja> EmailAdrese { get; set; } // 1:N
        public virtual IList<Komentar> Komentari { get; set; } = new List<Komentar>();

        public Roditelj()
        {
            Starateljstvo = new List<Starateljstvo>();
            Telefoni = new List<TelefonRoditelja>();
            EmailAdrese = new List<EmailRoditelja>();
            Komentari = new List<Komentar>();
        }
    }
}
