using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace ReaderObject
{
    class Bdd
    {
        public static System.Data.Common.DbConnection GetConnection
        {
            get
            {

                DbProviderFactory factory = DbProviderFactories.GetFactory("Oracle.DataAccess.Client");
                DbConnection cnx = factory.CreateConnection();
                string ch = String.Format(ConfigurationManager.ConnectionStrings["Oracle"].ToString(),
                ConfigurationManager.AppSettings["SERVER"],
                ConfigurationManager.AppSettings["PORTIN"],
                ConfigurationManager.AppSettings["SID"],
                ConfigurationManager.AppSettings["LOGIN"],
                ConfigurationManager.AppSettings["PASSWORD"]);


                cnx.ConnectionString = ch;
                return cnx;
            }
        }

        public static DbCommand GetDbCommand
        {
            get
            {
                string fournisseurDeDonnees = ConfigurationManager.AppSettings["Provider"];
                DbProviderFactory fctry = DbProviderFactories.GetFactory(fournisseurDeDonnees);
                DbCommand cmmd = fctry.CreateCommand();
                return cmmd;
            }
        }

    }
}
