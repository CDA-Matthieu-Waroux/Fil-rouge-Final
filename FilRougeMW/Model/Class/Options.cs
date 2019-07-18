using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRougeMW.Model.Class
{
    class Options
    {
        // Déclaration des attributs définissant l'option
        private string typeOption;
        private int idOption;

        // Encapsulation des attrubuts pour créer des propriétés
        public int IdOption { get => idOption; set => idOption = value; }
        public string TypeOption { get => typeOption; set => typeOption = value; }

        //Initialisation du constructeur par défaut
        public Options()
        {
        }
    }
}
