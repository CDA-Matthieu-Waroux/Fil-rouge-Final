using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRougeMW.Model.Class

{
    class Voiture
    {
        // Déclaration des attributs définissant la voiture
        private int idVoiture;
        private int? puissance;
        private int? prixAchat;
        private int? coteArgus;     
        private int? nombrePlace;
        private string carburant;
        private string modele;
        private string couleur;
        private int? anneeFabrication;
        private string marque;
        private string categorie;
        private string photo;

        // Encapsulation des attrubuts pour créer des propriétés 
        public int IdVoiture { get => idVoiture; set => idVoiture = value; }
        public int? Puissance { get => puissance; set => puissance = value; }
        public int? PrixAchat { get => prixAchat; set => prixAchat = value; }
        public int? CoteArgus { get => coteArgus; set => coteArgus = value; }
        public int? NombrePlace { get => nombrePlace; set => nombrePlace = value; }
        public string Carburant { get => carburant; set => carburant = value; }
        public string Modele { get => modele; set => modele = value; }
        public string Couleur { get => couleur; set => couleur = value; }
        public int? AnneeFabrication { get => anneeFabrication; set => anneeFabrication = value; }
        public string Marque { get => marque; set => marque = value; }
        public string Categorie { get => categorie; set => categorie = value; }
        public string Photo { get => photo; set => photo = value; }

        //Initialisation du constructeur par défaut
        public Voiture()
        {
        }

        //Initialisation des différents constructeurs utiles avec leurs attributs
        public Voiture(int idVoiture, int? puissance, int? prixAchat, int? coteArgus,  int? nombrePlace, string carburant, string modele, string couleur, int? anneeFabrication, string categorie , string photo)
        {
            IdVoiture = idVoiture;
            Puissance = puissance;
            PrixAchat = prixAchat;
            CoteArgus = coteArgus;           
            NombrePlace = nombrePlace;
            Carburant = carburant;
            Modele = modele;
            Couleur = couleur;
            AnneeFabrication = anneeFabrication;           
            Categorie = categorie;
            Photo = photo;
           
        }
        public Voiture(int? puissance, int? prixAchat, int? coteArgus, int? nombrePlace, string carburant, string modele, string couleur, int? anneeFabrication, string categorie , string photo)
        {
            Puissance = puissance;
            PrixAchat = prixAchat;
            CoteArgus = coteArgus;
            NombrePlace = nombrePlace;
            Carburant = carburant;
            Modele = modele;
            Couleur = couleur;
            AnneeFabrication = anneeFabrication;
            Categorie = categorie;
            Photo = photo;
        }
    }
}
