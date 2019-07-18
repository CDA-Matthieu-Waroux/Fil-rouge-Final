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
using System.Windows.Data;
using System.Windows.Input;

namespace FilRougeMW.ViewModel
{
    class PrincipalViewModel : ObservableObject, INotifyPropertyChanged
    {
        // Création des propriétés pour les commandes à binder ---> lier dans le fichier xaml exemple : Command="{Binding CommandQuitter}"
        // Ces propriétés contiennent des getter & setter et interagissent avec la classe RelayCommand du dossier helpers
        public ICommand CommandDeconnexion { get; private set; }
        public ICommand CommandOuvrirModifier { get; private set; }
        public ICommand CommandOuvrirOptionPrincipal { get; private set; }
        public ICommand CommandSupprimer { get; private set; }
        public ICommand NettoyerListe { get; private set; }
        public ICommand CommandRafraichir { get; private set; }
        public ICommand CommandChargerOptions { get; private set; }
        public ICommand CommandOuvrirAjouter { get; private set; }
        public ICommand CommandOuvrirInfos { get; private set; }
        public ICommand CommandQuitter { get; private set; }

        public static int CompteurInfos { get => compteurInfos; set => compteurInfos = value; }
        public static int CompteurPage { get => compteurPage; set => compteurPage = value; }
        public static ObservableCollection<Voiture> ListeVoiture1 { get => listeVoiture; set => listeVoiture = value; }
        public static int Id { get => id; set => id = value; }

        //Création d'une observable collection pour stocker les données des voitures
        private static ObservableCollection<Voiture> listeVoiture;
        //Création d'une observable collection pour récupérer les voitures filtrés
        private static ObservableCollection<Voiture> listeVoiture2;
        private static List<string> listeOptions;

        // déclaration des attributs
        private static int compteurInfos;
        private static int compteurPage ;
        private static int id;

        public PrincipalViewModel() 
        {
            // Création des propriétés pour les commandes à binder ---> lier dans le fichier xaml exemple : Command="{Binding CommandQuitter}"
            // Ces propriétés contiennent des getter & setter et interagissent avec la classe RelayCommand du dossier helpers
            NettoyerListe = new RelayCommandSP(Nettoyer);
            CommandDeconnexion = new RelayCommandSP(Deconnexion);
            CommandOuvrirModifier = new RelayCommandSP(OuvrirModifier);
            CommandOuvrirOptionPrincipal = new RelayCommandSP(OuvrirOptionPrincipal);
            CommandSupprimer = new RelayCommandSP(SupprimerVoiture);
            CommandRafraichir = new RelayCommandSP(ChargerDonnee);
            CommandChargerOptions = new RelayCommandSP(ChargerDonneeOptions);
            CommandOuvrirAjouter = new RelayCommandSP(OuvrirAjouter);
            CommandOuvrirInfos = new RelayCommandSP(OuvrirInfos);
            CommandQuitter = new RelayCommandSP(Quitter);

            // On charge les données des voitures grâce à la méthode stockés dans ServiceVoiture
            ListeVoiture1 = ServiceVoiture.ChargeeDonneeVoiture();
            // On ajoute dans la listeCollectionView des voitures suivant le résultat du filtre
            ListeVoitureFiltre.Filter = new Predicate<object>(o => Filtrer(o as Voiture));
            Id = ListeVoiture.ElementAt(Convert.ToInt32(IndexSelected)).IdVoiture;
            ChargerImage();
        }

        // Getter sur la liste de Voiture observable collection
        public ObservableCollection<Voiture> ListeVoiture
        {
            get { return ListeVoiture1; }
        }
        
        public List<string> ListeOptions
        {
            get { return listeOptions; }
        }
        // Getter sur la liste de voitures filtrées icollection view 
        //-> cette liste à pour particularité de permettre de filtrer instantanément sur la liste grâce à la méthode refresh() 
        public ICollectionView ListeVoitureFiltre
        {
            get { return CollectionViewSource.GetDefaultView(ListeVoiture1); }
        }

        // Ci-dessous les propriétés qui seront liés à la vue par les tags exemple: Text="{Binding SelectedItem.Marque, ElementName= listeVoitureDataGrid}" 
        // Le RaisePropertyChanged implémente la notification des modifications de propriétés, elle utilise les méthodes contenu dans le dossier Helpers --> ObservableObjects
        // Le : ElementName=listeVoitureDataGrid fait référence au datagrid dans la vue xaml
        private int? _idVoiture = null;
        public int? IdVoiture
        {
            get { return _idVoiture; }
            set { _idVoiture = value; RaisePropertyChanged("IdVoiture"); }
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
        private string _lienPhoto = null;
        public string LienPhoto
        {
            get { return _lienPhoto; }
            set { _lienPhoto = value; RaisePropertyChanged("LienPhoto");  }
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
            set { _carburant = value; RaisePropertyChanged("Carburant"); }
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
        private int? indexSelected;
        public int? IndexSelected
        {
            get { return indexSelected; }
            set { indexSelected = value; RaisePropertyChanged("IndexSelected"); }
        }
        private int itemSelected;
        public int ItemSelected
        {
            get { return itemSelected; }
            set { itemSelected = value; RaisePropertyChanged("ItemSelected"); }
        }
        private string _photo;
        public string Photo
        {
            get { return _photo; }
            set { _photo = value; RaisePropertyChanged("Photo"); }
        }

        private string recherche;
        public string Recherche
        {
            get { return recherche; }
            set { recherche = value; RaisePropertyChanged("Recherche"); ListeVoitureFiltre.Refresh(); }
        }

        // Méthode pour se déconnecter
        private void Deconnexion()
        {
            // Cette méthode permet de fermer l'application
            Connexion fenetre = new Connexion();
            fenetre.Show();
            // On efface la liste pour éviter les doublons
            ConnexionViewModel.MaListeUtilisateur.Clear();
            App.Current.Windows[0].Close();
        }
        // méthode pour ouvvrir info
        private void OuvrirInfos()
        {
            InfosViewModel.VerificationPageConnexion = false;
            Infos fenetre = new Infos();
            fenetre.Show();
            CompteurPage = 1;
        }
        // méthode pour quitter la fenêtre
        private void Quitter()
        {
            Environment.Exit(0);
        }
        // Mèthode pour charger les images
        private void ChargerImage()
        {
            foreach (Voiture voiture in ListeVoiture)
            {
                voiture.Photo = "http://147.135.231.175/Upload/" + voiture.Photo;
            }
        }
        // Méthode pour ouvrir la page Modifier
        private  void OuvrirModifier()
        {
            // si l'utilisateur est l'admin, la fen s'ouvre en récuperant l'Id de la voiture selectionné dans le datagrid
           if (ConnexionViewModel.MaListeUtilisateur[0].IdGrade == 1)
            {
                 Id = ListeVoiture.ElementAt(Convert.ToInt32(IndexSelected)).IdVoiture;
            // cette méthode permet d'ouvrir la fenetre modifier en y remontant L'id de la voiture selectionné dans le datagrid
            Modifier fenetre = new Modifier(Id);
            fenetre.Show();
            }
            else
            {
                MessageBox.Show("Secret défence , vous ne possedez pas les droits !\n \n"+"╭∩╮（︶︿︶） ╭∩╮" + "        "+ "╭∩╮（︶︿︶） ╭∩╮");
            }
        }
        // Métode Pour ouvrir la fenêtre des options de la fenêtre principal
        private void OuvrirOptionPrincipal()
        {
            // si l'utilisateur est l'admin, la fenêtre des options s'ouvre en récuperant l'Id de la voiture selectionné dans le datagrid
            if (ConnexionViewModel.MaListeUtilisateur[0].IdGrade == 1)
            { 
                 // cette méthode permet d'ouvrir la fenetre des options de la voiture
                 Id = ListeVoiture.ElementAt(Convert.ToInt32(IndexSelected)).IdVoiture;
                 OptionsPrincipal fenetre = new OptionsPrincipal(Id);
                 fenetre.Show();
            }
             else
            {
                MessageBox.Show("Secret défence , vous ne possedez pas les droits !\n \n" + "╭∩╮（︶︿︶） ╭∩╮" + "        " + "╭∩╮（︶︿︶） ╭∩╮");
            }
        }
        // Mèthode pour ouvrir la méthode Ajouter
        private void OuvrirAjouter()
        {
            // si l'utilisateur est l'admin, la fenêtre Ajouter s'ouvre 
            if (ConnexionViewModel.MaListeUtilisateur[0].IdGrade == 1)
            { 
                Ajouter fenetre = new Ajouter();
                fenetre.Show();
            }
            else
            {
                MessageBox.Show("Secret défence , vous ne possedez pas les droits !\n \n" + "╭∩╮（︶︿︶） ╭∩╮" + "        " + "╭∩╮（︶︿︶） ╭∩╮");
            }
        }
        // Méthode pour Charger les données de la voiture dans le dataGrid
        private void ChargerDonnee()
        {
            MessageBox.Show("List rafraichit avec succés !", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
            // On vide la liste pour eviter les doublons
            ListeVoiture1.Clear();
            // On charge une nouvelle fois la liste
            ListeVoiture1 = ServiceVoiture.ChargeeDonneeVoiture();
            // Pour chaque résultat on associe les valeurs contenus dans la liste au propriétés à binder.
            for (int i = 0; i < ListeVoiture1.Count; i++)
            {
                IdVoiture = ListeVoiture1.ElementAt(i).IdVoiture;
                Marque = ListeVoiture1.ElementAt(i).Marque;
                Modele = ListeVoiture1.ElementAt(i).Modele;                
                Carburant = ListeVoiture1.ElementAt(i).Carburant;
                PrixAchat = ListeVoiture1.ElementAt(i).PrixAchat;
                Puissance = ListeVoiture1.ElementAt(i).Puissance;
                Categorie = ListeVoiture1.ElementAt(i).Categorie;
                CoteArgus = ListeVoiture1.ElementAt(i).CoteArgus;
                Couleur = ListeVoiture1.ElementAt(i).Couleur;
                AnneeFabrication = ListeVoiture1.ElementAt(i).AnneeFabrication;
                NombrePlace = ListeVoiture1.ElementAt(i).NombrePlace;
            }
            ChargerImage();
        }
        // Mèthode pour supprimer une voiture
        private void SupprimerVoiture()
        {
            // si l'utilisateur est l'admin, la voiture peut être supprimer
            if (ConnexionViewModel.MaListeUtilisateur[0].IdGrade == 1)
            { 
                //Creation d'une message box pour Confirmer la suppression
                var resultat = MessageBox.Show("Êtes-vous sûr de vouloir supprimer", "Confimation Suppression", MessageBoxButton.YesNo , MessageBoxImage.Warning);

                         if (resultat == MessageBoxResult.Yes)
                         {
                                  // Création d'un liste intermédiaire avec les  filtrés
                                  IList<Voiture> maList;
                                  maList = ListeVoitureFiltre.Cast<Voiture>().ToList();
                                 // Convertion de cette liste en Observable pur récupérer l'utilisation des methodes SelectedItem et SelectedIndex
                                 listeVoiture2 = new ObservableCollection<Voiture>(maList);
                                // On s'assure que l'utilisateur a bien cliqué sur la datagrid pour executer la méthode et prévenir les crash de l'application.

                                 if (ListeVoitureFiltre.CurrentItem != null)
                                 {
                                     // On travail avec l'id de la voiture pour être certain de ne pas supprimer la mauvaise voiture
                                     // La méthode SupprimerVoiture attend un paramètre : idVoiture
                                     // On demande l'id de la voiture dans la ligne de la liste Filtrée, obtenu par l'attribut IndexSelected 
                                     // Exemple listeVoiture.ElementAt(2).IdVoiture --> retournera l'id de la voiture contenu dans la liste à la postion numéro 2
                                     // IndexSelected est renvoyé par le Binding --> voir dans le Datatgrid de la vue xaml Principal            
                                     ServiceVoiture.SupprimerVoiture(listeVoiture2.ElementAt(Convert.ToInt32(IndexSelected)).IdVoiture);
                                     MessageBox.Show("Id supprime : " + (listeVoiture2.ElementAt(Convert.ToInt32(IndexSelected)).IdVoiture) + " - - " + listeVoiture2.ElementAt(Convert.ToInt32(IndexSelected)).Marque + " " + listeVoiture2.ElementAt(Convert.ToInt32(IndexSelected)).Modele + " " , "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                                 }
                                 // Pour rafraichir la liste après traitement
                                 ChargerDonnee();
                         }
            }
            else
            {
                MessageBox.Show("Secret défence , vous ne possedez pas les droits !\n \n" + "╭∩╮（︶︿︶） ╭∩╮" + "        " + "╭∩╮（︶︿︶） ╭∩╮");
            }
        }
        // Méthode pour filter les voiture dans notre icollection view et on cast la marque, le modèle, la version , l'année de fabrication , 
        //la puissance , le carburant et la couleur
        private bool Filtrer(Voiture voiture)
        {
            return Recherche == null
                || voiture.Marque.IndexOf(Recherche, StringComparison.OrdinalIgnoreCase) != -1
                || voiture.Modele.IndexOf(Recherche, StringComparison.OrdinalIgnoreCase) != -1                             
                || voiture.AnneeFabrication.ToString().IndexOf(Recherche, StringComparison.OrdinalIgnoreCase) != -1
                || voiture.Puissance.ToString().IndexOf(Recherche, StringComparison.OrdinalIgnoreCase) != -1
                || voiture.Categorie.IndexOf(Recherche, StringComparison.OrdinalIgnoreCase) != -1
                || voiture.Carburant.IndexOf(Recherche, StringComparison.OrdinalIgnoreCase) != -1
                || voiture.Couleur.IndexOf(Recherche, StringComparison.OrdinalIgnoreCase) != -1;
        }
        // Mèthode pour charger les option de la voiture
         private void ChargerDonneeOptions()
        {
            Id = ListeVoiture.ElementAt(Convert.ToInt32(IndexSelected)).IdVoiture;
            listeOptions = ServiceOptions.ChargeeDonneeOptionsByID(Id);
        }
        // Méthode pour nettoyer la barre de recherche
        private void Nettoyer()
        {
            Recherche = "";
        }
    }
}



