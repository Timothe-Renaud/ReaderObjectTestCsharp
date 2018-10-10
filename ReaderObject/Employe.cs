using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderObject
{
    class Employe
    {
        private Int16 numemp;
        private String nomemp;
        private String prenomemp;
        private String poste;
        private Single salaire;
        private Single? prime;
        private String codeProjet;
        private Int16? superieur;

        public short Numemp { get => numemp; set => numemp = value; }
        public string Nomemp { get => nomemp; set => nomemp = value; }
        public string Prenomemp { get => prenomemp; set => prenomemp = value; }
        public string Poste { get => poste; set => poste = value; }
        public float Salaire { get => salaire; set => salaire = value; }
        public float? Prime { get => prime; set => prime = value; }
        public string CodeProjet { get => codeProjet; set => codeProjet = value; }
        public short? Superieur { get => superieur; set => superieur = value; }

        public override string ToString()
        {
            return Numemp + " - " + Nomemp + " - " + Prenomemp + " - " + Poste + " - " + Salaire + " - " + Prime + " - " + CodeProjet + " - " + Superieur;
        }
    }
}
