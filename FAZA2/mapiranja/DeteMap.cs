using FluentNHibernate.Mapping;
using Deciji_Letnji_Program.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deciji_Letnji_Program.Mapiranje
{
    public class DeteMap : ClassMap<Dete>
    {
        public DeteMap()
        {
            Table("DETE");

            Id(x => x.ID, "ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Ime, "Ime");
            Map(x => x.Prezime, "Prezime");
            Map(x => x.DatumRodjenja, "Datum_rodjenja");
            Map(x => x.Pol, "Pol");
            Map(x => x.Adresa, "Adresa");
            Map(x => x.TelefonDeteta, "Telefon_deteta");
            Map(x => x.EmailDeteta, "Email_deteta");
            Map(x => x.PosebnePotrebe, "Posebne_potrebe");



            HasManyToMany(x => x.Roditelji)
                .Table("STARATELJSTVO")
                .ParentKeyColumn("ID_dete")//!!!!
                .ChildKeyColumn("ID_roditelj")
                .Cascade.All()
                .Inverse();//Dete nije vlasnik veze


            //HasMany(x => x.Povrede)
            //    .KeyColumn("Id_dete")
            //    .Inverse()
            //    .Cascade.All();

            HasManyToMany(x => x.Obroci)
                .Table("JE_DAT")
                .ParentKeyColumn("ID_dete")//!!!!
                .ChildKeyColumn("ID_obrok")
                .Cascade.All()
                .Inverse();//Dete nije vlasnik veze 


            HasMany(x => x.Ucestvuje)
                .KeyColumn("ID_dete")
                .Inverse()
                .Cascade.All();

            HasMany(x => x.TelefoniRoditelja)
                .KeyColumn("ID_deteta")
                .Inverse()
                .Cascade.All();

            HasMany(x => x.EmailoviRoditelja)
                .KeyColumn("ID_deteta")
                .Inverse()
                .Cascade.All();
        }
    }
}