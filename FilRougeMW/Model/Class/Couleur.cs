using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRougeMW.Model.Class
{
    class Couleur
    {
       
        // Déclaration des attributs définissant la couleur
        private string nomCouleur;
        private int idCouleur;

        // Encapsulation des attrubuts pour créer des propriétés
        public string NomCouleur { get => nomCouleur; set => nomCouleur = value; }
        public int IdCouleur { get => idCouleur; set => idCouleur = value; }

        //Initialisation du constructeur par défaut
        public Couleur()
        {
        }
    }
}
