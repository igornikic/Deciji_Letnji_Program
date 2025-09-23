using System;

namespace Deciji_Letnji_Program.Entiteti
{
    public class JeDat
    {
        public virtual int Id { get; protected set; }

        public virtual Dete Dete { get; set; } // FK za Dete
        public virtual Obrok Obrok { get; set; } // FK za Obrok

        public JeDat() 
        {

        }
    }
}
