using Deciji_Letnji_Program.Entiteti;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Mapiranja
{
    internal class EmailRoditeljaMap : ClassMap<EmailRoditelja>
    {
        public EmailRoditeljaMap()
        {
            Table("EMAIL_RODITELJA");

            Id(x => x.ID, "ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Email, "Email");

            References(x => x.Dete, "ID_deteta").LazyLoad();
        }
    }
}
