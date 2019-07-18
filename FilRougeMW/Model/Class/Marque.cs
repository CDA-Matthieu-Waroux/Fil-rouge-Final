using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FilRougeMW.Model.Class
{
    public class Marque
    {

        // Déclaration des attributs définissant la marque
        private int idMarque;
        private string nomMarque;
        private ICollection<Modele> modeles;

        // Encapsulation des attrubuts pour créer des propriétés
        public string NomMarque { get => nomMarque; set => nomMarque = value; }
        public int IdMarque { get => idMarque; set => idMarque = value; }
        public ICollection<Modele> Modeles { get => modeles; set => modeles = value; }

        //Initialisation du constructeur par défaut
        public Marque()
        {
        }

        //Initialisation des différents constructeurs utiles avec leur paramètres
        public Marque(string nomMarque, ICollection<Modele> modeles , int idmarque)
        {
            NomMarque = nomMarque;
            Modeles = modeles;
            IdMarque = idmarque;
        }
    }
}
