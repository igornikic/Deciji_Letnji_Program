using FluentNHibernate.Mapping;
using Deciji_Letnji_Program.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Mapiranja
{
    internal class KomentarMap : ClassMap<Komentar>
    {
        public KomentarMap()
        {
            Table("KOMENTAR");

            Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Tekst, "Komentar");

            References(x => x.EvidencijaPrisustva).Column("EVIDENCIJA_ID").LazyLoad();
            References(x => x.Roditelj).Column("ID_roditelja").LazyLoad();
            References(x => x.Dete).Column("ID_deteta").LazyLoad();
        }
    }
}
