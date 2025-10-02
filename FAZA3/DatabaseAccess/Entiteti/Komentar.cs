using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Entiteti
{
    public class Komentar
    {
        public int Id { get; set; }
        public string Tekst { get; set; }

        // Komentar je vezan za evidenciju prisustva

        public EvidencijaPrisustva EvidencijaPrisustva { get; set; } // 1 prema 1

        // Autor komentara može biti dete ili roditelj (jedno od ta dva)
       
        public Roditelj Roditelj { get; set; } // 1 prema 1

        public Dete Dete { get; set; } //1 prema 1

        public Komentar() { 
        
        }
    }

}
