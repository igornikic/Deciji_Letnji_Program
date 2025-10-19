using FluentNHibernate.Conventions.Helpers;
using Deciji_Letnji_Program.Entiteti;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Mapiranja
{
    internal class TelefonRoditeljaMap : ClassMap<TelefonRoditelja>
    {
        public TelefonRoditeljaMap()
        {
            Table("TELEFON_RODITELJA");

            Id(x => x.ID, "ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Telefon, "Telefon");

            References(x => x.Dete, "ID_deteta").LazyLoad();

        }
    }
}
