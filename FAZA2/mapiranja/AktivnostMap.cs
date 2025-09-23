using FluentNHibernate.Mapping;
using Deciji_Letnji_Program.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Mapiranja
{
    internal class AktivnostMap : ClassMap<Aktivnost>
    {
        public AktivnostMap() 
        {
            Table("AKTIVNOST");

            Id(x => x.Id, "ID_aktivnosti").GeneratedBy.TriggerIdentity();

            Map(x => x.Tip, "Tip");
            Map(x => x.Naziv, "Naziv");
            Map(x => x.Datum, "Datum");
            Map(x => x.StarosnaGrupa, "Starosna_grupa");
            Map(x => x.MaxUcesnika, "Max_ucesnika");
            Map(x => x.Ogranicenja, "Ogranicenja");

            //Veza prema lokacijama 
            References(x => x.Lokacija).Column("Naziv_lokacije").LazyLoad();

            HasMany(x => x.Prijave)
                .KeyColumn("ID_aktivnosti")
                .Inverse()
                .Cascade.All();

            HasMany(x => x.EvidencijaPrisustva)
                .KeyColumn("ID_aktivnosti")
                .Inverse()
                .Cascade.All();

            HasMany(x => x.Pratioci)
                .KeyColumn("ID_aktivnosti")
                .Inverse()
                .Cascade.All();

            References(x => x.Evaluacija).Column("ID_evaluacije").LazyLoad();

            HasMany(x => x.Povrede)
                .KeyColumn("ID_aktivnosti")
                .Inverse()
                .Cascade.All();

            HasManyToMany(x => x.AngazovanaLica)
                .Table("Ucesce")
                .ParentKeyColumn("Id_aktivnosti")
                .ChildKeyColumn("JMBG")
                .Cascade.All();//Vlasnik veze

            HasMany(x => x.Obrok)
                .KeyColumn("ID_aktivnosti")
                .Inverse()
                .Cascade.All();
                
        }
    }
}
