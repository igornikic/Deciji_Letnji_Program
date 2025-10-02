using FluentNHibernate.Mapping;
using Deciji_Letnji_Program.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Mapiranja
{
    internal class PratilacMap : ClassMap<Pratilac>
    {
        public PratilacMap() 
        {
            Table("PRATILAC");

            Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Ime, "Ime");
            Map(x => x.Prezime, "Prezime");
            Map(x => x.Pol, "Pol");
            Map(x => x.BrojTelefona, "Broj_telefona");

            References(x => x.Dete).Column("ID_dete").LazyLoad();
            References(x => x.Aktivnost).Column("ID_aktivnosti").LazyLoad();

        }

    }
}
