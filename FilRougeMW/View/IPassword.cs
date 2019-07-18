using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRougeMW.View
{
    public interface IPassword
    {
        // le System.Security nous permet de faire le hashage du code avec une génération aléatoire
        //la classe SecureString permet au texte de rester confidentiel
        // on crée une interface IPassword pour implémenter la propriété afin de hasher le mot de passe
        System.Security.SecureString Password { get; }
    }
}
