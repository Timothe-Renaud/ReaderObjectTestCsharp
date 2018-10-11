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

        public DbConnection maConnexion { get; private set; }

        public EmployeServices()
        {
            Bdd bdd = new Bdd();
        }

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
                using (DbCommand cmdTousLesEmployes = Bdd.GetDbCommande)
                {
                    cmdTousLesEmployes.CommandText = "select * from EMPLOYE";
                    cmdTousLesEmployes.Connection = maConnexion;
                    maConnexion.Open();
                    DbDataReader rdrEmploye = cmdTousLesEmployes.ExecuteReader();

                    while (rdrEmploye.Read())
                    {
                        Employe employe = HydrateEmploye(rdrEmploye);
                        employes.Add(employe);
                    }
                    rdrEmploye.Close();
                }
                maConnexion.Close();
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
        public Employe FindEmployeByID(Int16 id)
        {
            var MaConnexion = Bdd.GetConnection;
            Employe employe = null;

            try
            {

                DbCommand cmdTousLesEmployes = Bdd.GetDbCommande;
                {
                    DbParameter pID = cmdTousLesEmployes.CreateParameter();
                    pID.ParameterName = "numemp";
                    pID.Direction = System.Data.ParameterDirection.Input;
                    pID.DbType = System.Data.DbType.Int16;
                    pID.Value = id;
                    cmdTousLesEmployes.Parameters.Add(pID);
                    cmdTousLesEmployes.CommandText = "select * from EMPLOYE where numemp = :numemp";
                    cmdTousLesEmployes.Connection = MaConnexion;
                    MaConnexion.Open();
                    DbDataReader readerEmploye = cmdTousLesEmployes.ExecuteReader();
                    if (readerEmploye.Read())
                    {
                        employe = HydrateEmploye(readerEmploye);
                    }
                    readerEmploye.Close();
                }
                MaConnexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return employe;

        }
    }
}
