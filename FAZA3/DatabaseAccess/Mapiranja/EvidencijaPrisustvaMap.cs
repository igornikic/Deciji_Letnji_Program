using FluentNHibernate.Mapping;
using Deciji_Letnji_Program.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Mapiranja
{
    internal class EvidencijaPrisustvaMap : ClassMap<EvidencijaPrisustva>
    {
        public EvidencijaPrisustvaMap()
        {
            Table("EVIDENCIJA_PRISUSTVA");

            Id(x => x.Id, "EVIDENCIJA_ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Prisustvo, "Prisustvo");
            Map(x => x.OcenaAktivnosti, "Ocena_aktivnosti");
            Map(x => x.Sugestije, "Sugestije");

            References(x => x.Dete).Columns("Id_dete").LazyLoad();
            References(x => x.Aktivnost).Column("ID_aktivnosti").LazyLoad();

            HasMany(x => x.Komentari)
                .Table("KOMENTARI")
                .KeyColumn("EVIDENCIJA_ID")
                .Element("Komentar")
                .Cascade.All()
                .LazyLoad(); 
        }
    }
}
