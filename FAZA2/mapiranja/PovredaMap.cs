using FluentNHibernate.Mapping;
using Deciji_Letnji_Program.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Mapiranja
{
    internal class PovredaMap : ClassMap<Povreda>
    {
        public PovredaMap()
        {
            Table("POVREDA");

            Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Datum, "Datum");
            Map(x => x.PreduzeteMere, "Preduzete_mere");
            Map(x => x.Opis, "Opis");

            References(x => x.Dete).Column("ID_dete").LazyLoad();
            References(x => x.Aktivnost).Column("ID_aktivnosti").LazyLoad();
            References(x => x.OdgovornoOsoblje).Column("Odgovorno_osoblje").LazyLoad();

        }
    }
}
