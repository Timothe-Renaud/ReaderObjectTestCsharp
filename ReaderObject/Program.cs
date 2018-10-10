using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReaderObject;
using Oracle.ManagedDataAccess.Client;

namespace ReaderObject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employe> emp = new List<Employe>();
            //info pour la connexion la la BD
            string Chn = @"Server = freesio.lyc-bonaparte.fr; Database = LangloisSQL; Integrated Secuity = true;";
            //OracleConnection connection = new OracleConnection(Chn);


            //parcours des employes
            List<Employe> employes;
            EmployeServices servicesEmploye = new EmployeServices();
            employes = servicesEmploye.FindAllEmplolyes();
            foreach (Employe employe in employes)
            {
                Console.WriteLine(employe);
            }
            Console.WriteLine("-------------------------------------");
            Employe unEmploye = servicesEmploye.FindEmployeById(4);
            Console.WriteLine(unEmploye);
            
        }
    }
}
