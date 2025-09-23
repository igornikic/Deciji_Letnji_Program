using FluentNHibernate.Mapping;
using Deciji_Letnji_Program.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Mapiranja
{
    internal class RoditeljMap : ClassMap<Roditelj>
    {
        public RoditeljMap()
        {
            Table("RODITELJ");

            
            Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Ime, "Ime");
            Map(x => x.Prezime, "Prezime");

            HasManyToMany(x => x.Deca)
                .Table("STARTELJSTVO")
                .ParentKeyColumn("ID_roditelj")
                .ChildKeyColumn("ID_dete")
                .Cascade.All(); //Roditelj je vlasnik veze

            HasMany(x => x.Komentari)
             .Table("KOMENTARI")
             .KeyColumn("ID_roditelja")
             .Element("Komentar")
             .Cascade.All()
             .LazyLoad();
        }

    }
}
