using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRougeMW.Model.Class
{
    class Categorie
    {
        
        // Déclaration des attributs définissant la catégorie
        private string typeCategorie;
        private int idCategorie;

        // Encapsulation des attrubuts pour créer des propriétés
        public int IdCategorie { get => idCategorie; set => idCategorie = value; }
        public string TypeCategorie { get => typeCategorie; set => typeCategorie = value; }

        // Initialisation du constructeur par défaut
        public Categorie()
        {
        }
    }
}
