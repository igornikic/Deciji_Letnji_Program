using FluentNHibernate.Mapping;
using Deciji_Letnji_Program.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Proxy;

namespace Deciji_Letnji_Program.Mapiranja
{
    internal class EvaluacijaMap : ClassMap<Evaluacija>
    {
        public EvaluacijaMap()
        {
            Table("EVALUACIJA");

            Id(x => x.ID, "ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Ocena, "Ocena");
            Map(x => x.Datum, "Datum");
            Map(x => x.Opis, "Opis");

            //References(x => x.AngazovanoLice)
            //    .Column("JMBG_lice")
            //    .Unique();


            //References(x => x.Aktivnost)
            //    .Column("ID_aktivnosti")
            //    .Unique() // 1:1 — samo jedna evaluacija po aktivnosti
            //    .Cascade.None();
            //HasOne(x => x.Aktivnost).Constrained();
            //HasOne(x => x.AngazovanoLice).Cascade.All();
            References(x => x.Aktivnost, "ID_aktivnosti").Unique();
            References(x => x.AngazovanoLice, "JMBG_lice").Unique();
        }
    }
}