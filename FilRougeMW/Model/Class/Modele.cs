using FilRougeMW.Model.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRougeMW.Model.Class
{
    public class Modele
    {
        // Déclaration des attributs définissant le modèle
        private int idModele;
        private string nomModele;
        private int idMarque;

        // Encapsulation des attrubuts pour créer des propriétés
        private Marque Marque { get; set; }
        public int IdModele { get => idModele; set => idModele = value; }
        public string NomModele { get => nomModele; set => nomModele = value; }
        public int IdMarque { get => idMarque; set => idMarque = value; }

        //Initialisation du constructeur par défaut
        public Modele()
        {
            
        }
    }
}
