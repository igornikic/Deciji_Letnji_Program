using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Deciji_Letnji_Program.Mapiranje;

namespace Deciji_Letnji_Program
{
    internal class DataLayer
    {
        private static ISessionFactory _factory = null;
        private static object objLock = new object();


        //funkcija na zahtev otvara sesiju
        public static ISession GetSession()
        {
            //ukoliko session factory nije kreiran
            if (_factory == null)
            {
                lock (objLock)
                {
                    if (_factory == null)
                        _factory = CreateSessionFactory();
                }
            }

            return _factory.OpenSession();
        }
        public static void ProveriKonekciju()
        {
            try
            {
                using (ISession session = DataLayer.GetSession())
                {
                    // Pokusavamo da otvorimo transakciju i odmah zatvorimo
                    using (var tx = session.BeginTransaction())
                    {
                        tx.Commit();
                    }
                }

                MessageBox.Show("Uspesno povezano sa bazom!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom povezivanja sa bazom: " + ex.Message, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //konfiguracija i kreiranje session factory
        private static ISessionFactory CreateSessionFactory()
        {
            try
            {
                var cfg = OracleManagedDataClientConfiguration.Oracle10
                .ConnectionString(c =>
                    c.Is("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=S19297;Password=S19297;"));
                    //c.Is("Data Source=localhost/Free;User Id=c##testuser;Password=12345678;"));

                Console.WriteLine(cfg);

                return Fluently.Configure()
                    .Database(cfg.ShowSql())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DeteMap>())
                    .BuildSessionFactory();

            }
            catch (Exception ec)
            {
                System.Windows.Forms.MessageBox.Show(ec.Message);
                return null;


            }

        }
    }
}