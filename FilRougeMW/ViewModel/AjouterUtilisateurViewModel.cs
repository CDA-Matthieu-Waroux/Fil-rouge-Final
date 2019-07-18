using FilRougeMW.Helpers;
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
    class AjouterUtilisateurViewModel : ObservableObject , INotifyPropertyChanged
    {
        /// <summary>
        /// Ajout de nouveau utilisateur du logiciel
        /// </summary>
        
        // Création des propriétés pour les commandes à binder ---> lier dans le fichier xaml exemple : Command="{Binding CommandQuitter}"
        // Ces propriétés contiennent des getter & setter et interagissent avec la classe RelayCommand du dossier helpers
        public ICommand CommandQuitter { get; private set; }        
        public ICommand CommandAjouter { get; private set; } 
        public ICommand CommandOuvrirInfos { get; private set; }

        //Création des obdervable collection pour stocker les données 
        private ObservableCollection<string> listenomutilisateur;
        private ObservableCollection<string> listecleemploye;
        private ObservableCollection<Utilisateur> listeUtilisateur;
        private ObservableCollection<string> listeCleEmployeUtilise = new ObservableCollection<string>();
        private string temporaire;

        // on encapsule les listes en utilisant la propriété
        public ObservableCollection<string> ListeNomutilisateur { get => listenomutilisateur; set => listenomutilisateur = value; }
        public ObservableCollection<string> Listecleemploye { get => listecleemploye; set => listecleemploye = value; }
        public string Temporaire { get => temporaire; set => temporaire = value; }
        public ObservableCollection<Utilisateur> ListeUtilisateur { get => listeUtilisateur; set => listeUtilisateur = value; }
        public ObservableCollection<string> ListeCleEmployeUtilise { get => listeCleEmployeUtilise; set => listeCleEmployeUtilise = value; } 

        public AjouterUtilisateurViewModel()
        {
            // Création des propriétés pour les commandes à binder ---> lier dans le fichier xaml exemple : Command="{Binding CommandQuitter}"
            // Ces propriétés contiennent des getter & setter et interagissent avec la classe RelayCommand du dossier helpers
            CommandQuitter = new RelayCommandSP(Quitter);            
            CommandAjouter = new RelayCommandSP(Ajouter);
            CommandOuvrirInfos = new RelayCommandSP(OuvrirInfos);

            // Chargement des données 
            listenomutilisateur = ServiceUtilisateur.ChargeeDonnee();
            listecleemploye = ServiceEmploye.ObtenirClefEmploye();
            ListeUtilisateur = ServiceUtilisateur.ObtenirUtilisateur(); 
        }
        // Getter sur la liste string
        public ObservableCollection<string> ListeCleEmploye
        {
            get { return Listecleemploye; }
        }
        // Ci-dessous les propriétés qui seront liés à la vue par les tags exemple: Text="{Binding SelectedItem.Marque, ElementName= listeVoitureDataGrid}" 
        // Le OnPropertyChanged implémente la notification des modifications de propriétés, elle utilise les méthodes contenu dans le dossier Helpers --> ObservableObjects
        // Le : ElementName=listeVoitureDataGrid fait référence au datagrid dans la vue xaml
        private string _nomutilisateur ;
        public string NomUtilisateur
        {
            get { return _nomutilisateur; }
            set { _nomutilisateur = value; RaisePropertyChanged("Utilisateur"); }
        }

        private string _motDePasse ;
        public string MotDePasse
        {
            get { return _motDePasse; }
            set { _motDePasse = value; RaisePropertyChanged("MotDePasse"); }
        }

        private string _cleEmploye ;
        public string CleEmploye
        {
            get { return _cleEmploye; }
            set { _cleEmploye = value; RaisePropertyChanged("CleEmploye"); }
        }

        private string _login; 
        public string Login
        {
            get { return _login; }
            set { _login = value; RaisePropertyChanged("Login"); }
        }
        // Méthode pour fermer la fenêtre à l'aide du boutton quitter
        public void Quitter()
        {
            App.Current.Windows[1].Close(); 
        }
        // méthode pour ajouter des nouveau utilisateur
        public void Ajouter()
        {
            //On déclare une classe utilisateur et que l'on instancie
            //On charge cette classe avec les différent élément ci-dessous
            Utilisateur utilisateur = new Utilisateur();
            utilisateur.NomUtilisateur = Login;
            utilisateur.NomGrade = "Employe";
            utilisateur.MotDePasseUtilisateur = ServiceUtilisateur.EncryypterSha512(MotDePasse);
            utilisateur.Cleemploye = CleEmploye;
            temporaire = "Connexion";
            // Pour chaque élément dans la liste utilisateur, on ajoute une clé employé
            foreach( Utilisateur element in listeUtilisateur)
            {
                listeCleEmployeUtilise.Add(element.Cleemploye);
            }
            // Si le nom utilisateur est contenu dans la liste
            if (listenomutilisateur.Contains(Login))
            {
                temporaire = "Utilisateur"; 
            }
            // si la clé employé n'est pas contenu dans la liste
            if (listecleemploye.Contains(utilisateur.Cleemploye) != true)
            {
                temporaire = "CleEmployeNonValide";
            }
            // Si la clé employé est contenu dans la liste
            if (listeCleEmployeUtilise.Contains(utilisateur.Cleemploye))
            {
                temporaire = "CleEmployeUtilisé";
            }

            switch (temporaire)
            {
                case "Utilisateur":
                    MessageBox.Show(" Nom d'utilisateur déjà présent ! \n Veuillez recommencez !");
                    break;
                case "Connexion":
                    ServiceUtilisateur.Ajouterutilisateur(utilisateur);
                    MessageBox.Show("Utilisateur Créer !");
                    App.Current.Windows[1].Close();
                    break;
                case "CleEmployeUtilisé":
                    MessageBox.Show("Clé Employé déjà utilisé ! \n Veuillez recommencer !");
                    break;
                case "CleEmployeNonValide":
                    MessageBox.Show("Clé Employé non valide ! \n Veuillez recommencer !");
                    break;
                default:                    
                    MessageBox.Show("Erreur je suis beau");
                    break;
            } 
        }
        //Méthode pour ouvrir la fenêtre info 
        private void OuvrirInfos()
        {
            // 
            InfosViewModel.VerificationPageConnexion = false;
            // Déclaration d'une fenêtre que l'on instancie
            Infos fenetre = new Infos();
            // Ouverture de la fenêtre
            fenetre.Show();
            // compteur de page limiter à 3 ouvertures
            PrincipalViewModel.CompteurPage = 2;
        }
    }
}
