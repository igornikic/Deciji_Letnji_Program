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

            Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Ime, "Ime");
            Map(x => x.Prezime, "Prezime");
            Map(x => x.DatumRodjenja , "Datum_rodjenja");
            Map(x => x.Pol , "Pol");
            Map(x => x.Adresa , "Adresa");
            Map(x => x.TelefonDeteta, "Telefon_deteta");
            Map(x => x.EmailDeteta, "Email_deteta");
            Map(x => x.PosebnePotrebe, "Posebne_potrebe");



            HasManyToMany(x => x.Roditelji)
                .Table("STARATELJSTVO")
                .ParentKeyColumn("ID_dete")//!!!!
                .ChildKeyColumn("ID_roditelj")
                .Cascade.All()
                .Inverse();//Dete nije vlasnik veze
                

            HasMany(x => x.Povrede)
                .KeyColumn("Id_dete")
                .Inverse()
                .Cascade.All();

            
            HasMany(x => x.Pratioci)
                .KeyColumn("ID_Dete")
                .Inverse()
                .Cascade.All();
                

            HasMany(x => x.Prijave)
                .KeyColumn("ID_deteta")
                .Inverse()
                .Cascade.All();

            HasManyToMany(x => x.EvidencijePrisustva)
                .Table("EVIDENCIJA_PRISUSTVA")
                .ParentKeyColumn("ID_dete")
                .ChildKeyColumn("ID_aktivnosti")
                .Cascade.All()
                .Inverse();//Dete nije vlasnik veze

            HasMany(x => x.Telefoni)
                .Table("TELEFON_RODITELJA")
                .KeyColumn("ID_dete")
                .Element("Telefon")
                .Cascade.All()
                .LazyLoad();//Visvrednosni atribut

            HasMany(x => x.EmailAdrese)
                .Table("EMAIL_RODITELJA")
                .KeyColumn("ID_dete")
                .Element("Email")
                .Cascade.All()
                .LazyLoad();//Visevrednosni element

            HasManyToMany(x => x.Obroci)
                .Table("JE_DAT")
                .ParentKeyColumn("ID_dete")//!!!!
                .ChildKeyColumn("ID_obrok")
                .Cascade.All()
                .Inverse();//Dete nije vlasnik veze

            HasMany(x => x.Komentari)
                .Table("KOMENTARI")
                .KeyColumn("ID_deteta")
                .Element("Komentar")
                .Cascade.All()
                .LazyLoad();

        }
    }
}