using FluentNHibernate.Mapping;
using Deciji_Letnji_Program.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Util;

namespace Deciji_Letnji_Program.Mapiranja
{
    public class AngazovanoLiceMap : ClassMap<AngazovanoLice>
    {
        public AngazovanoLiceMap()
        {
            Table("ANGAZOVANO_LICE");

            Id(x => x.JMBG, "JMBG").GeneratedBy.Assigned();

            Map(x => x.Ime, "Ime");
            Map(x => x.Prezime, "Prezime");
            Map(x => x.Pol, "Pol");
            Map(x => x.Adresa, "Adresa");
            Map(x => x.BrojTelefona, "Broj_telefona");
            Map(x => x.Email, "Email");
            Map(x => x.StrucnaSprema, "Strucna_sprema");
            Map(x => x.Volonter, "Volonter");
            Map(x => x.Trener, "Trener");
            Map(x => x.Animator, "Animator");
            Map(x => x.ZdravstveniRadnik, "Zdravstveni_radnik");


            HasManyToMany(x => x.Aktivnosti)
                .Table("UCESCE")
                .ParentKeyColumn("JMBG")
                .ChildKeyColumn("ID_aktivnosti")
                .Cascade.None()
                .Inverse();


            HasOne(x => x.Evaluacija)
                .PropertyRef(x => x.AngazovanoLice)
                .Cascade.All();

            HasMany(x => x.Povrede)
                .KeyColumn("Odgovorno_osoblje_JMBG")
                .Inverse()
                .Cascade.All();

        }
    }
}
