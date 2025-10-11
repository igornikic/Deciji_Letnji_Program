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


            Id(x => x.ID, "ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Ime, "Ime");
            Map(x => x.Prezime, "Prezime");

            HasManyToMany(x => x.Deca)
                .Table("STARATELJSTVO")
                .ParentKeyColumn("ID_roditelj")
                .ChildKeyColumn("ID_dete")
                .Cascade.All(); //Roditelj je vlasnik veze

            HasMany(x => x.Prijave)
                .KeyColumn("ID_roditelja")
                .Inverse()
                .Cascade.All();

            HasMany(x => x.Ucestvuje)
                .KeyColumn("ID_roditelj")
                .Inverse()
                .Cascade.All();
        }
    }
}
