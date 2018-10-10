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
    class EmployeServices
    {

        private static EmployeServices instance;

        public static EmployeServices Instance
        {
            get
            {
                return instance ?? (instance = new EmployeServices());
            }
        }

        public DbConnection MaConnexion { get; private set; }

        /*public EmployeServices()
        {
            Bdd bdd = new Bdd();
            cnOracle = bdd.cnOracle;
        }*/

        /// <summary>
        /// HydrateEmploye ->
        /// </summary>
        /// <param name="readerEmploye"></param>
        /// <returns></returns>
        public static Employe HydrateEmploye(DbDataReader readerEmploye)
        {
            Employe employe = new Employe();
            employe.Numemp = Convert.ToInt16(readerEmploye["NUMEMP"]);
            employe.Nomemp = readerEmploye["NOMEMP"] as String;
            employe.Prenomemp = readerEmploye["PRENOMEMP"] as String;
            employe.Poste = readerEmploye["POSTE"] as String;
            employe.Salaire = Convert.ToSingle(readerEmploye["SALAIRE"]);
            if (readerEmploye["PRIME"] == DBNull.Value)
            {
                employe.Prime = null;
            }
            else
            {
                employe.Prime = Convert.ToSingle(readerEmploye["PRIME"]);
            }

            employe.CodeProjet = readerEmploye["CODEPROJET"] as String;
            if (readerEmploye["SUPERIEUR"] == DBNull.Value)
            {
                employe.Superieur = null;
            }
            else
            {
                employe.Superieur = Convert.ToInt16(readerEmploye["SUPERIEUR"]);
            }
            return employe;
        }

        /// <summary>
        /// FinAllEmployes ->
        /// </summary>
        /// <returns></returns>
        public List<Employe> FindAllEmployes()
        {
            List<Employe> employes = new List<Employe>();
            try
            {
                using (DbCommand cmdTousLesEmployes = Bdd.GetDbCommand)
                {
                    cmdTousLesEmployes.CommandText = "select * from EMPLOYE";
                    cmdTousLesEmployes.Connection = MaConnexion;
                    MaConnexion.Open();
                    DbDataReader rdrEmploye = cmdTousLesEmployes.ExecuteReader();
      
                    while (rdrEmploye.Read())
                    {
                        Employe employe = HydrateEmploye(rdrEmploye);
                        employes.Add(employe);
                    }
                    rdrEmploye.Close();
                }
                MaConnexion.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return employes;
        }

        /// <summary>
        /// FindEmployeById ->
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employe FindEmployeById(Int16 id)
        {
            Employe employe = null;
            using (cnOracle)
            {
                try
                {
                    using (OracleCommand cmdTousLesEmployes = new OracleCommand(@"select * from employe where numemp= :nnumemp", cnOracle))
                    {
                        OracleParameter pId = new OracleParameter("numemp", OracleDbType.Int16, System.Data.ParameterDirection.Input);
                        pId.Value = id;
                        cmdTousLesEmployes.Parameters.Add(pId);
                        cnOracle.Open();
                        OracleDataReader readerEmploye = cmdTousLesEmployes.ExecuteReader();
                        if (readerEmploye.Read())
                        {
                            employe = HydrateEmploye(readerEmploye);
                        }
                        readerEmploye.Close();
                    }
                    cnOracle.Close();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                return employe;
            }
        }



    }
}
