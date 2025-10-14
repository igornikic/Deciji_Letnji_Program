using FluentNHibernate.Mapping;
using Deciji_Letnji_Program.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Mapiranja
{
    public class PrijavaMap : ClassMap<Prijava>
    {
        public PrijavaMap()
        {
            Table("PRIJAVA");
            Id(x => x.IdPrijave, "ID_prijave").GeneratedBy.TriggerIdentity();
            Map(x => x.DatumPrijave, "Datum_prijave");
            Map(x => x.Status, "Status");
            References(x => x.Dete)
                .Column("ID_deteta")
                .Not.Nullable();
            References(x => x.Roditelj)
                .Column("ID_roditelja")
                .Not.Nullable();
            References(x => x.Aktivnost)
                .Column("ID_aktivnosti")
                .Not.Nullable();
        }
    }

}
