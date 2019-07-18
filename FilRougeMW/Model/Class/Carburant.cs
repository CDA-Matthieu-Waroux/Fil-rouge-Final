using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRougeMW.Model.Class
{
    class Carburant
    {
        // Déclaration des attributs définissant le carburant
          private string nomCarburant;
          private int idCarburant;

        // Encapsulation des attrubuts pour créer des propriétés
         public int IdCarburant { get => idCarburant; set => idCarburant = value; }
         public string NomCarburant { get => nomCarburant; set => nomCarburant = value; }
      
        //Initialisation du constructeur par défaut
         public Carburant()
         {

         }
        
    }
}
