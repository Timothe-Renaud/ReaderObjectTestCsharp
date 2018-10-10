using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace ReaderObject
{
    class EmployeServices
    {

        private OracleConnection cnOracle;

        public EmployeServices()
        {
            Bdd bdd = new Bdd();
            cnOracle = bdd.CnOracle;
        }





        /// <summary>
        /// HydrateEmploye ->
        /// </summary>
        /// <param name="readerEmploye"></param>
        /// <returns></returns>
        public static Employe HydrateEmploye(OracleDataReader readerEmploye)
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
        public List<Employe> FindAllEmplolyes()
        {
            List<Employe> employes = new List<Employe>();
            string ch = String.Format(ConfigurationManager.ConnectionStrings["oracle"].ToString(), ConfigurationManager.AppSettings["SERVEUR"],
                                                                                ConfigurationManager.AppSettings["PORT"],
                                                                                ConfigurationManager.AppSettings["SID"],
                                                                                ConfigurationManager.AppSettings["USERID"],
                                                                                ConfigurationManager.AppSettings["PWD"]
                                                                                );

            try
            {
                using (OracleCommand cmdTousLesEmploes = new OracleCommand(@"select * from employe", CnOracle))
                {
                    cnOracle.Open();
                    OracleDataReader rdr = cmdTousLesEmploes.ExecuteReader();
                    while (rdr.Read())
                    {
                        Employe employe = HydrateEmploye(rdr);
                        employes.Add(employe);
                    }
                    rdr.Close();
                }
                cnOracle.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return employes;
        }

        /// <summary>
        /// HydrateEmploye ->
        /// </summary>
        /// <param name="readerEmploye"></param>
        /// <returns></returns>
        public static Employe HydrateEmploye(OracleDataReader readerEmploye)
        {
            Employe employe = new Employe();
            employe.Numemp = Convert.ToInt16(readerEmploye["NUMEMP"]);
            employe.Nomemp = readerEmploye["NOMEMP"] as string;
            employe.Prenomemp = readerEmploye["PRENOMEMP"] as string;
            employe.Poste = readerEmploye["POSTE"] as string;
            employe.Salaire = Convert.ToSingle(readerEmploye["SALAIRE"]);
            if (readerEmploye["PRIME"] == DBNull.Value)
            {
                employe.Prime = null;
            }
            else
            {
                employe.Superieur = null;
            }

            employe.CodeProjet = readerEmploye["CODEPROJET"] as string;
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
        /// FindEmployeById ->
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employe FindEmployeById(Int16 id)
        {
            Employe employe = null;
            string ch = "Data Source=(DESCRI£PTION =(ADRESS = (PROTOCOL = TCP)(HOST = localhost))(CONNECT_DATA = (SERVER = DEDICATE )(SERVICE_NAME = {2}))); User Id={3};Password={4};";
            using (OracleConnection CnOracle = new OracleConnection(ch))
            {
                try
                {
                    using (OracleCommand cmdTousLesEmployes = new OracleCommand(@"select * from employe where numemp= :nnumemp", CnOracle))
                    {
                        OracleParameter pId = new OracleParameter("numemp", OracleDbType.Int16, System.Data.ParameterDirection.Input);
                        pId.Value = id;
                        cmdTousLesEmployes.Parameters.Add(pId);
                        CnOracle.Open();
                        OracleDataReader readerEmploye = cmdTousLesEmployes.ExecuteReader();
                        if (readerEmploye.Read())
                        {
                            employe = HydrateEmploye(readerEmploye);
                        }
                        readerEmploye.Close();
                    }
                    CnOracle.Close();
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
