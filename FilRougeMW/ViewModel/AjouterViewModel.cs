using FilRougeMW.Model.Class;
using FilRougeMW.Model.Service;
using FilRougeMW.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FilRougeMW.ViewModel
{
    class AjouterViewModel :  ObservableObject , INotifyPropertyChanged
    {
        /// <summary>
        /// Ajout d'une nouvelle voiture et de nouvelles options
        /// </summary>

        // Création des propriétés pour les commandes à binder ---> lier dans le fichier xaml exemple : Command="{Binding CommandQuitter}"
        // Ces propriétés contiennent des getter & setter et interagissent avec la classe RelayCommand du dossier helpers
        //Création des obdervable collection pour stocker les données 
        // on encapsule les listes en utilisant la propriété
        private static ObservableCollection<Marque> listemarque;
        private static List<string> listecarburant;
        private static List<string> listecouleur;
        private static List<string> listecategorie;
        private static List<int> listeannee;
        private static ObservableCollection<Modele> listemodele;
        private static bool boule = true;
        
        public static ObservableCollection<Marque> Listemarque { get => listemarque; set => listemarque = value; }
        public static List<string> Listecarburant { get => listecarburant; set => listecarburant = value; }
        public static List<string> Listecouleur { get => listecouleur; set => listecouleur = value; }
        public static List<string> Listecategorie { get => listecategorie; set => listecategorie = value; }
        public static List<int> Listeannee { get => listeannee; set => listeannee = value; }
        public static ObservableCollection<Modele> Listemodele { get => listemodele; set => listemodele = value; } 

        public ICommand CommandQuitter { get; private set; }       
        public ICommand CommandAjouter { get; private set; }
        public ICommand CommandOuvrirInfos { get; private set; } 

        public AjouterViewModel()
        {
            // Création des propriétés pour les commandes à binder ---> lier dans le fichier xaml exemple : Command="{Binding CommandQuitter}"
            // Ces propriétés contiennent des getter & setter et interagissent avec la classe RelayCommand du dossier helpers
            // Chargement des données
            // Chargement de la procédure stockée
            CommandAjouter = new RelayCommandSP(AjouterVoiture);         
            CommandQuitter = new RelayCommandSP(Quitter);
            CommandOuvrirInfos = new RelayCommandSP(OuvrirInfos);
            listeannee = ServiceAnneeFabrication.ChargeeDonneeAnneeFabrication();
            listecarburant = ServiceCarburant.ChargeeDonneeCarburant();
            listecouleur = ServiceCouleur.ChargeeDonneeCouleur();
            listecategorie = ServiceCategorie.ChargeeDonneeCategorie();
            ChargerMarqueByModele();
        }
        // Ci-dessous les propriétés qui seront liés à la vue par les tags exemple: Text="{Binding SelectedItem.Marque, ElementName= listeVoitureDataGrid}" 
        // Le OnPropertyChanged implémente la notification des modifications de propriétés, elle utilise les méthodes contenu dans le dossier Helpers --> ObservableObjects
        // Le : ElementName=listeVoitureDataGrid fait référence au datagrid dans la vue xaml
        public List<int> ListeAnnee
        {
            get { return Listeannee; }
        }
       
        public List<string> ListeCarburant
        {
            get { return Listecarburant; }
        }

        public List<string> ListeCouleur
        {
            get { return Listecouleur; }
        }

        public List<string> ListeCategorie
        {
            get { return Listecategorie; }
        } 

        private int? _puissance = null;
        public int? Puissance
        {
            get { return _puissance; }
            set { _puissance = value; RaisePropertyChanged("Puissance"); }
        }

        private int? _prixAchat = null;
        public int? PrixAchat
        {
            get { return _prixAchat; }
            set { _prixAchat = value; RaisePropertyChanged("PrixAchat"); }
        }

        private int? _coteArgus = null;
        public int? CoteArgus
        {
            get { return _coteArgus; }
            set { _coteArgus = value; RaisePropertyChanged("CoteArgus"); }
        }


        private int? _nombrePlace = null;
        public int? NombrePlace
        {
            get { return _nombrePlace; }
            set { _nombrePlace = value; RaisePropertyChanged("NombrePlace"); }
        }

        private string _carburant = null;
        public string Carburant
        {
            get { return _carburant; }
            set {  _carburant = value; RaisePropertyChanged("Carburant"); }
        }

        private string _modele = null;
        public string Modele
        {
            get { return _modele; }
            set { _modele = value; RaisePropertyChanged("Modele"); }
        }

        private int? _anneeFabrication = null;
        public int? AnneeFabrication
        {
            get { return _anneeFabrication; }
            set { _anneeFabrication = value; RaisePropertyChanged("AnneeFabrication"); }
        }

        private string _couleur = null;
        public string Couleur
        {
            get { return _couleur; }
            set { _couleur = value; RaisePropertyChanged("Couleur"); }
        }

        private string _marque = null;
        public string Marque
        {
            get { return _marque; }
            set { _marque = value; RaisePropertyChanged("Marque"); }
        }

        private string _categorie = null;
        public string Categorie
        {
            get { return _categorie; }
            set { _categorie = value; RaisePropertyChanged("Modele"); }
        }
   
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged("Id"); }
        }
        //on crée une interface Ilist marque pour definir la méthode pour manipuler des collection non générique
        public IList<Marque> Marques
        {
            get; private set;
        }
        // on crée une inetrface Icollection Modele pour definir la méthode pour manipuler des collection générique 
        public ICollection<Modele> Modeles
        {
            get; private set;
        }

        //on crée des propriétés pour les lier à la vue par les tags du XAML avec les Binding
        // Le OnPropertyChanged implémente la notification des modifications de propriétés, elle utilise les méthodes contenu dans le dossier Helpers --> ObservableObjects
        // le DisplayMemberPath="" définit le chemind'une valeur d'un objet source pour sa représentation visuelle (hérité de ItemsControl)
        private Marque _selectedMarque;
        public Marque SelectedMarque
        {
            get { return _selectedMarque; }
            set
            {
                _selectedMarque = value;
                RaisePropertyChanged("SelectedMarque");
                this.Modeles = _selectedMarque.Modeles;
                RaisePropertyChanged("Modeles");
            }
        }

        public Modele SelectedModele
        {
            get; set;
        }
        // Déclaration d'un bool pour demander si on doit ajouter des options
        public static bool Boule { get => boule; set => boule = value; }
        
        // Méthode pour charger les combobox en cascading
        private void ChargerMarqueByModele()
        {
            // Chargement des listes modèle et marque
            listemarque = ServiceMarque.ChargeeDonneeMarque();
            Listemodele = ServiceModele.ChargeeDonneeModele(); 
            //On instancie une liste marque
            Marques = new List<Marque>();

            // On charge les marques dans MarquesIList
            // Chaque marque dispose d'une liste de modèle qui sera rempli dans la boucle for suivante
            for (int i = 0; i < listemarque.Count; i++)
            {
                Marques.Add(new Marque(listemarque[i].NomMarque, new List<Modele>(), listemarque[i].IdMarque)); 
            }

            for (int i = 0; i < listemarque.Count; i++)
            {
                // Comparaison entre le numéro des clés primaires des marques "IdMarque" et les clés étrangères des modèles "IdMarqueFK"
                // Si les deux numéros comparés ont la même valeur, alors le modèle fait bien parti de cette marque
                for (int j = 0; j < Listemodele.Count; j++)
                {
                    if (Marques[i].IdMarque == Listemodele[j].IdMarque)
                    {
                        Marques[i].Modeles.Add(Listemodele[j]);
                    }
                }
            }
            // Met par défaut la marque selectionné
            this.SelectedMarque = this.Marques[0];
        }
        // Méthode pour ajouter les caractèristique de la nouvelle voiture
        public void AjouterVoiture()
        {
            Voiture voiture = new Voiture();
            voiture.AnneeFabrication = AnneeFabrication;
            voiture.Carburant = Carburant;
            voiture.Categorie = Categorie;
            voiture.CoteArgus = CoteArgus;
            voiture.Couleur = Couleur;
            voiture.Modele = Modele;
            voiture.NombrePlace = NombrePlace;
            voiture.PrixAchat = PrixAchat;
            voiture.Puissance = Puissance;
            voiture.Photo = Marque + ".png"; 

            // Demande si on veut ajouter des options à la voiture
            ServiceVoiture.AjouterVoiture(voiture);
            if(boule == true)
            {
                MessageBox.Show("Voiture ajoutée");
                OuvrirOptionAjouter();
            }
            else
            {
                boule = true;
            }
        } 

        // Méthode pour ajouter les options de la voiture
        public void OuvrirOptionAjouter()
        {
            var resultat = MessageBox.Show("Voulez-vous rajouter des options à cette voiture ?", "Confimation Ajout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultat == MessageBoxResult.Yes)
            {
                App.Current.Windows[1].Close();
                
                OptionsAjouter fenetre = new OptionsAjouter();
                fenetre.Show();
            }
            else
            {
                App.Current.Windows[1].Close();
                ChargerDonnee();
            }
        }
        // Méthode pour Quitter la fenêtre
        public void Quitter()
        { // on nettoie la page
            AjouterViewModel.Listecarburant.Clear();
            AjouterViewModel.Listecategorie.Clear();
            AjouterViewModel.Listecouleur.Clear();
            AjouterViewModel.Listemarque.Clear();
            AjouterViewModel.Listeannee.Clear();
            AjouterViewModel.Listemodele.Clear();
            // retour à la page principal
            App.Current.Windows[1].Close();
        }
        // Méthode pour ouvrir la page info
        private void OuvrirInfos()
        {
            InfosViewModel.VerificationPageConnexion = false;
            Infos fenetre = new Infos();
            fenetre.Show();
            PrincipalViewModel.CompteurPage = 2;
        }
        // Méthode pour charger la liste Voiture
        private void ChargerDonnee()
        {
            MessageBox.Show("List rafraichit avec succés !", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
            // On vide la liste pour eviter les doublons
            PrincipalViewModel.ListeVoiture1.Clear();
            // On charge une nouvelle fois la liste
            PrincipalViewModel.ListeVoiture1 = ServiceVoiture.ChargeeDonneeVoiture();
            // Pour chaque résultat on associe les valeurs contenus dans la liste au propriétés à binder.
            ChargerImage();
        }
        // méthode pour charger image
        private void ChargerImage()
        {
            foreach (Voiture voiture in PrincipalViewModel.ListeVoiture1)
            {
                voiture.Photo = "http://147.135.231.175/Upload/" + voiture.Photo;
            }
        }
    }
}
