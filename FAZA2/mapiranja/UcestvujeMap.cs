using System;
using System.Collections.Generic;
using Deciji_Letnji_Program.Entiteti;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace Deciji_Letnji_Program.Mapiranja
{
    internal class UcestvujeMap : ClassMap<Ucestvuje>
    {
        public UcestvujeMap()
        {
            Table("UCESTVUJE");

            Id(x => x.ID, "ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Prisustvo, "Prisustvo");
            Map(x => x.OcenaAktivnosti, "Ocena_aktivnosti");
            Map(x => x.Komentari, "Komentari");
            Map(x => x.Pratilac, "Pratilac");

            References(x => x.Dete, "ID_dete").LazyLoad();
            References(x => x.Aktivnost, "ID_aktivnosti").LazyLoad();
            References(x => x.Roditelj, "ID_roditelj").LazyLoad();

        }
    }
}
