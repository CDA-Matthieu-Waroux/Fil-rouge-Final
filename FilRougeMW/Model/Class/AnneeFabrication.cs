using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRougeMW.Model.Class
{
    class AnneeFabrication
    {
        //Déclaration des attributs pour définir l'année de fabrication
        private int idAnneeFabrication;
        private int anneFabrication;

        // Encapsulation des attrubuts pour créer des propriétés
        public int AnneFabrication { get => anneFabrication; set => anneFabrication = value; }
        public int IdAnneeFabrication { get => idAnneeFabrication; set => idAnneeFabrication = value; }

       
        // Initialisation du constructeur par défault
        public AnneeFabrication()
        {
        }
       
    }
}
