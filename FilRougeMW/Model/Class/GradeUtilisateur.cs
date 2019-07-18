using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRougeMW.Model.Class
{
    class GradeUtilisateur
    {
      
        // Déclaration des paramètres définissant le grade utilisateur
        private int idGrade;
        private string nomGrade;

        // Encapsulation des attrubuts pour créer des propriétés
        public int IdGrade { get => idGrade; set => idGrade = value; }
        public string NomGrade { get => nomGrade; set => nomGrade = value; }

        //Initialisation du constructeur par défaut
        public GradeUtilisateur()
        {
        }
    }
}
