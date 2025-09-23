using FluentNHibernate.Mapping;
using Deciji_Letnji_Program.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Mapiranja
{
    internal class ObrokMap : ClassMap<Obrok>
    {
        public ObrokMap() 
        {
            Table("OBROK");

            Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Tip, "Tip");
            Map(x => x.Uzrast, "Uzrast");
            Map(x => x.Jelovnik, "Jelovnik");
            Map(x => x.PosebneOpcije, "Posebne_opcije");

            References(x => x.Lokacija).Column("Naziv_lokacije").LazyLoad();
            References(x => x.Aktivnost).Column("ID_aktivnosti").LazyLoad();

            HasManyToMany(x => x.Deca)
                .Table("JE_DAT")
                .ParentKeyColumn("ID_obrok")
                .ChildKeyColumn("ID_dete")
                .Cascade.All();//Vlasnik veze
        }
    }
}

