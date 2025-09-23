using FluentNHibernate.Mapping;
using Deciji_Letnji_Program.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Mapiranja
{
    internal class LokacijaMap : ClassMap<Lokacija>
    {
        public LokacijaMap()
        {
            Table("LOKACIJA");

            Id(x => x.Naziv, "Naziv").GeneratedBy.Assigned();

            Map(x => x.Tip, "Tip");
            Map(x => x.Adresa, "Adresa");
            Map(x => x.Kapacitet, "Kapacitet");
            Map(x => x.DostupnaOprema, "Dostupna_oprema");

            HasMany(x => x.Obroci)
                .KeyColumn("Naziv_lokacije")
                .Inverse()
                .Cascade.All();

            HasMany(x => x.Aktivnosti)
                .KeyColumn("Naziv_lokacije")
                .Inverse()
                .Cascade.All();

        }

    }
}
