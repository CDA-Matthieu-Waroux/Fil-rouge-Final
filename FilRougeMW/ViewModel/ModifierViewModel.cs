using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using FilRougeMW.ViewModel;
using System.Reflection;
using FilRougeMW.View;
using FilRougeMW.Model.Class;
using FilRougeMW.Model.Service;
using System.Windows;

namespace FilRougeMW.ViewModel
{
    class ModifierViewModel : ObservableObject, INotifyPropertyChanged
    {
        /// <summary>
        /// Modifier d'une nouvelle voiture et de nouvelles options
        /// </summary>

        // Création des propriétés pour les commandes à binder ---> lier dans le fichier xaml exemple : Command="{Binding CommandQuitter}"
        // Ces propriétés contiennent des getter & setter et interagissent avec la classe RelayCommand du dossier helpers
        // Déclaration des attributs
        // Encapsulation des attrubuts pour créer des propriétés
        // Création des obdervable collection pour stocker les données 
        // on encapsule les listes en utilisant la propriété
        private Voiture voiture;
        private static ObservableCollection<Voiture> listevoiture;
        private static ObservableCollection<Marque> listemarque;
        private static ObservableCollection<Modele> listemodele;
        private static List<string> listecarburant;
        private static List<string> listecouleur;
        private static List<string> listecategorie;
        private static List<int> listeannee;
        private static List<string> listeoptions;
        private string photo;

        public ICommand CommandQuitter { get; private set; }
        public ICommand CommandOuvrirOptionModifier { get; private set; }
        public ICommand CommandModifierVoiture { get; private set; }
        public ICommand CommandOuvrirInfos { get; private set; }

        public Voiture Voiture { get => voiture; set => voiture = value; }
        public static ObservableCollection<Voiture> Listevoiture { get => listevoiture; set => listevoiture = value; }
        public static ObservableCollection<Marque> Listemarque { get => listemarque; set => listemarque = value; }
        public static List<string> Listecarburant { get => listecarburant; set => listecarburant = value; }
        public static List<string> Listecouleur { get => listecouleur; set => listecouleur = value; }
        public static List<string> Listecategorie { get => listecategorie; set => listecategorie = value; }
        public static List<int> Listeannee { get => listeannee; set => listeannee = value; }
        public static List<string> Listeoptions { get => listeoptions; set => listeoptions = value; }
        public string Photo { get => photo; set => photo = value; }
        public static ObservableCollection<Modele> Listemodele { get => listemodele; set => listemodele = value; }

        public ModifierViewModel()
        {
            // Création des propriétés pour les commandes à binder ---> lier dans le fichier xaml exemple : Command="{Binding CommandQuitter}"
            // Ces propriétés contiennent des getter & setter et interagissent avec la classe RelayCommand du dossier helpers
            // Chargement des données
            // Chargement de la procédure stockée
            CommandOuvrirOptionModifier = new RelayCommandSP(OuvrirOptionModifier);
            CommandQuitter = new RelayCommandSP(Quitter);
            CommandModifierVoiture = new RelayCommandSP(ModifierVoiture);
            CommandOuvrirInfos = new RelayCommandSP(OuvrirInfos);
            listevoiture = ServiceVoiture.ChargeeDonneeVoitureByID(PrincipalViewModel.Id);         
            listecarburant = ServiceCarburant.ChargeeDonneeCarburant();
            listecouleur = ServiceCouleur.ChargeeDonneeCouleur();
            listecategorie = ServiceCategorie.ChargeeDonneeCategorie();
            listeannee = ServiceAnneeFabrication.ChargeeDonneeAnneeFabrication();            
            ChargerMarqueByModele();
        }
        // Ci-dessous les propriétés qui seront liés à la vue par les tags exemple: Text="{Binding SelectedItem.Marque, ElementName= listeVoitureDataGrid}" 
        // Le OnPropertyChanged implémente la notification des modifications de propriétés, elle utilise les méthodes contenu dans le dossier Helpers --> ObservableObjects
        // Le : ElementName=listeVoitureDataGrid fait référence au datagrid dans la vue xaml
        public static ObservableCollection<Voiture> ListeVoiture
        {
            get { return Listevoiture; }
        }

        public static List<int> ListeAnnee
        {
            get { return Listeannee; }
        }

        public ObservableCollection<Marque> ListeMarque
        {
            get { return Listemarque; }
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
            set { _lienPhoto = value; RaisePropertyChanged("LienPhoto"); }
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
            set
            {
                _anneeFabrication = value; RaisePropertyChanged("AnneeFabrication");
            }
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

        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged("Id"); }
        }
        // Méthode pour ouvrir la fenêtre option modifier
        public void OuvrirOptionModifier()
        {
            // on supprime toute les options de la voiture 
            // on declare et instancie une fenêtre
            // ouverture de la fenêtre
            ServiceVoiture.SupprimerOptionsByVoiture(PrincipalViewModel.Id);
            OptionsModifier fenetre = new OptionsModifier();
            fenetre.Show();
        }
        // méthode pour quitter la fenêtre modifier
        public void Quitter()
        {
            listevoiture.Clear();
            ModifierViewModel.Listecarburant.Clear();
            ModifierViewModel.Listecategorie.Clear();
            ModifierViewModel.Listecouleur.Clear();
            ModifierViewModel.Listemarque.Clear();
            ModifierViewModel.Listeannee.Clear();
            ModifierViewModel.Listemodele.Clear();
            App.Current.Windows[1].Close();
        }

        // Méthode pour charger les combobox en cascading
        private void ChargerMarqueByModele()
        {
            // Chargement des listes modèle et marque
            listemarque = ServiceMarque.ChargeeDonneeMarque();
            listemodele = ServiceModele.ChargeeDonneeModele();
            //On instancie une liste marque
            Marques = new List<Marque>();

            // On charge les marques dans MarquesIList
            // Chaque marque dispose d'une liste de modèle qui sera rempli dans la boucle for suivante
            for (int i = 0; i < listemarque.Count; i++)
            {
                Marques.Add(new Marque(  listemarque[i].NomMarque, new List<Modele>() , listemarque[i].IdMarque)   );
            }

            for (int i = 0; i < listemarque.Count; i++)
            // Comparaison entre le numéro des clés primaires des marques "IdMarque" et les clés étrangères des modèles "IdMarqueFK"
            // Si les deux numéros comparés ont la même valeur, alors le modèle fait bien parti de cette marque
            {
                for (int j = 0; j < listemodele.Count; j++)
                {
                    if (Marques[i].IdMarque == listemodele[j].IdMarque)
                    {
                        Marques[i].Modeles.Add(listemodele[j]);
                    }
                }
            }
            // Met par défaut la marque selectionné
            this.SelectedMarque = this.Marques[0];
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
        public  Marque SelectedMarque
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

        // Mèthode pour modifier la voiture
        public void ModifierVoiture()
        {
            var resultat = MessageBox.Show("Êtes-vous sûr de vouloir Modifier Cette Voiture ? ", "Confimation Modification", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (resultat == MessageBoxResult.Yes)
            {
                Voiture voiture = new Voiture(); 
                voiture.IdVoiture = PrincipalViewModel.Id;
                voiture.Puissance = ListeVoiture.ElementAt(Convert.ToInt32(IndexSelected)).Puissance;
                voiture.PrixAchat = ListeVoiture.ElementAt(Convert.ToInt32(IndexSelected)).PrixAchat;
                voiture.CoteArgus = ListeVoiture.ElementAt(Convert.ToInt32(IndexSelected)).CoteArgus;
                voiture.NombrePlace = ListeVoiture.ElementAt(Convert.ToInt32(IndexSelected)).NombrePlace;
                voiture.Carburant = ListeVoiture.ElementAt(Convert.ToInt32(IndexSelected)).Carburant;
                voiture.Modele = ListeVoiture.ElementAt(Convert.ToInt32(IndexSelected)).Modele;
                voiture.Couleur = ListeVoiture.ElementAt(Convert.ToInt32(IndexSelected)).Couleur;
                voiture.AnneeFabrication = ListeVoiture.ElementAt(Convert.ToInt32(IndexSelected)).AnneeFabrication;
                voiture.Categorie = ListeVoiture.ElementAt(Convert.ToInt32(IndexSelected)).Categorie;
                voiture.Photo = ListeVoiture.ElementAt(Convert.ToInt32(IndexSelected)).Marque + ".png";

                ServiceVoiture.ModifierVoiture(voiture);          
                Quitter();
               ChargerDonnee();
            }
        }
        // Mèthode ouvrir la fenêtre info
        private void OuvrirInfos()
        {
            InfosViewModel.VerificationPageConnexion = false;
            Infos fenetre = new Infos();
            fenetre.Show();
            PrincipalViewModel.CompteurPage = 2;
        }
        // Mèthode pour charger les données
        private void ChargerDonnee()
        {
            MessageBox.Show("List rafraichit avec succés !", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
            // On vide la liste pour eviter les doublons
            PrincipalViewModel.ListeVoiture1.Clear();
            // On charge une nouvelle fois la liste
            PrincipalViewModel.ListeVoiture1 = ServiceVoiture.ChargeeDonneeVoiture();
            // Pour mettre en place le lien de l'image 
            ChargerImage();
        }
        // Mèthode pour charger les images
        private void ChargerImage()
        {
            foreach (Voiture voiture in PrincipalViewModel.ListeVoiture1)
            {
                voiture.Photo = "http://147.135.231.175/Upload/" + voiture.Photo;
            }
        }
    }
}
