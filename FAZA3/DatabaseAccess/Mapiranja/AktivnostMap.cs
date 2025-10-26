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

            Id(x => x.IdAktivnosti, "ID_aktivnosti").GeneratedBy.TriggerIdentity();

            Map(x => x.Tip, "Tip");
            Map(x => x.Naziv, "Naziv");
            Map(x => x.Datum, "Datum");
            Map(x => x.StarosnaGrupa, "Starosna_grupa");
            Map(x => x.MaxUcesnika, "Max_ucesnika");
            Map(x => x.Ogranicenja, "Ogranicenja");
            Map(x => x.PrevoznoSredstvo, "Prevozno_sredstvo");
            Map(x => x.PlanPuta, "Plan_puta");
            Map(x => x.PotrebnaOprema, "Potrebna_oprema");
            Map(x => x.Vodic, "Vodic");
            Map(x => x.Sport, "Sport");
            Map(x => x.PosebnaOprema, "Posebna_oprema");


            //Veza prema lokacijama 
            References(x => x.Lokacija).Column("Naziv_lokacije").LazyLoad();

            HasMany(x => x.Prijave)
                .KeyColumn("ID_aktivnosti")
                .Inverse()
                .Cascade.All();

            HasMany(x => x.Povrede)
                .KeyColumn("ID_aktivnosti")
                .Inverse()
                .Cascade.All();

            HasMany(x => x.Obrok)
                .KeyColumn("ID_aktivnosti")
                .Inverse()
            .Cascade.All();

            HasManyToMany(x => x.AngazovanaLica)
                .Table("UCESCE")
                .ParentKeyColumn("ID_aktivnosti")
                .ChildKeyColumn("JMBG")
                .Cascade.SaveUpdate();


            HasMany(x => x.Ucestvuju)
                .KeyColumn("ID_aktivnosti")
                .Inverse()
                .Cascade.All();

            //HasOne(x => x.Evaluacija)
            //                .PropertyRef("Aktivnost")
            //                .Cascade.All();






        }
    }
}