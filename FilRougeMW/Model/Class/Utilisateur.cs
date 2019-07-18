using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRougeMW.Model.Class
{
    class Utilisateur
    {
        // Déclaration des attributs définissant l'utilisateur
        private int idUtilisateur;
        private string nomUtilisateur;
        private string motDePasseUtilisateur;
        private string nomGrade;
        private int idGrade;
        private string cleemploye;

        // Encapsulation des attrubuts pour créer des propriétés
        public int IdUtilisateur { get => idUtilisateur; set => idUtilisateur = value; }
        public string NomUtilisateur { get => nomUtilisateur; set => nomUtilisateur = value; }
        public string MotDePasseUtilisateur { get => motDePasseUtilisateur; set => motDePasseUtilisateur = value; }
        public int IdGrade { get => idGrade; set => idGrade = value; }
        public string NomGrade { get => nomGrade; set => nomGrade = value; }
        public string Cleemploye { get => cleemploye; set => cleemploye = value; }

        //Initialisation du constructeur par défaut
        public Utilisateur()
        {
        }
    }
}
