using FluentNHibernate.Mapping;
using Deciji_Letnji_Program.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Mapiranja
{
    public class PovredaMap : ClassMap<Povreda>
    {
        public PovredaMap()
        {
            Table("POVREDA");
            Id(x => x.ID, "ID").GeneratedBy.TriggerIdentity();
            Map(x => x.Datum, "Datum");
            Map(x => x.Opis, "Opis");
            Map(x => x.PreduzeteMere, "Preduzete_mere");
            References(x => x.Dete)
                .Column("ID_dete")
                .Not.Nullable();
            References(x => x.Aktivnost)
                .Column("ID_aktivnosti")
                .Not.Nullable();
            References(x => x.OdgovornoOsoblje)
                .Column("Odgovorno_osoblje_JMBG");
        }
    }

}
