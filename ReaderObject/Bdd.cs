using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace ReaderObject
{
    class Bdd
    {
        public OracleConnection CnOracle { get; set; }

        /// <summary>
        /// Constructeur de Bdd.
        /// </summary>
        public Bdd()
        {
            string ch = String.Format(ConfigurationManager.ConnectionStrings["oracle"].ToString(),
                ConfigurationManager.AppSettings["SERVER"],
                ConfigurationManager.AppSettings["PORTIN"],
                ConfigurationManager.AppSettings["SID"],
                ConfigurationManager.AppSettings["LOGIN"],
                ConfigurationManager.AppSettings["PASSWORD"]);
            CnOracle = new OracleConnection(ch);
        }
    }
}
