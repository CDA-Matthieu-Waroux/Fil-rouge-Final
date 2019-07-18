using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRougeMW.Model.Class
{
    class Employe
    {
        //Déclaration des attributs définissant un employé
        private string nom;
        private string prénom;
        private string cleEmploye;

        // Encapsulation des attrubuts pour créer des propriétés
        public string Nom { get => nom; set => nom = value; }
        public string Prénom { get => prénom; set => prénom = value; }
        public string CleEmploye { get => cleEmploye; set => cleEmploye = value; }

        // Création du constructeur par défaut
        public Employe()
        {
        }
    }
}
