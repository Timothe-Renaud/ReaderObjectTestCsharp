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
            List<Employe> employes;
            EmployeServices servicesEmploye = new EmployeServices();
            employes = servicesEmploye.FindAllEmployes();
            foreach (Employe employe in EmployeServices.Instance.FindAllEmployes())
            {
                Console.WriteLine(employe);
            }
            Console.WriteLine("-------------------------------------");
            Console.WriteLine(EmployeServices.Instance.FindEmployeByID(4));

            Console.ReadKey();

        }
    }
}
