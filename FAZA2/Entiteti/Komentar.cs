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
        public int EvidencijaId { get; set; }
        public EvidencijaPrisustva EvidencijaPrisustva { get; set; }

        // Autor komentara može biti dete ili roditelj (jedno od ta dva)
        public int? RoditeljId { get; set; }
        public Roditelj Roditelj { get; set; }

        public int? DeteId { get; set; }
        public Dete Dete { get; set; }

        public Komentar() { 
        
        }
    }

}
