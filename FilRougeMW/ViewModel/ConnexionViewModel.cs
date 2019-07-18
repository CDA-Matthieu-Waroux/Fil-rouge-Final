using FilRougeMW.Model.Class;
using FilRougeMW.Model.Service;
using FilRougeMW.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace FilRougeMW.ViewModel
{
    class ConnexionViewModel  : ObservableObject,INotifyPropertyChanged

    {
        /// <summary>
        /// Connection au logiciel
        /// </summary>

        // Création des propriétés pour les commandes à binder ---> lier dans le fichier xaml exemple : Command="{Binding CommandQuitter}"
        // Ces propriétés contiennent des getter & setter et interagissent avec la classe RelayCommand du dossier helpers
        // Déclaration des attributs
        // Encapsulation des attrubuts pour créer des propriétés
        // Création des obdervable collection pour stocker les données 
        // on encapsule les listes en utilisant la propriété
        private string motPasse;
        public string MotPasse { get => motPasse; set => motPasse = value; }
        private string identifiant;
        public string Identifiant { get => identifiant; set => identifiant = value; }
        private string motPasseSaisiEncrypte;
        public string MotPasseSaisiEncrypte { get => motPasseSaisiEncrypte; set => motPasseSaisiEncrypte = value; }
        private static ObservableCollection<Utilisateur> maListeUtilisateur = new ObservableCollection<Utilisateur>();
        public static ObservableCollection<Utilisateur> MaListeUtilisateur { get => maListeUtilisateur; set => maListeUtilisateur = value; } 

        public ICommand CommandOuvrirInfos { get; private set; }
        public ICommand CommandOuvrirPrincipal { get; private set; }
        public ICommand CommandOuvrirAjouterUtilisateur { get; private set; }
        public ICommand CommandQuitter { get; private set; }
        public PasswordBox Password { get; set; } 

        public ConnexionViewModel()
        {
            // Création des propriétés pour les commandes à binder ---> lier dans le fichier xaml exemple : Command="{Binding CommandQuitter}"
            // Ces propriétés contiennent des getter & setter et interagissent avec la classe RelayCommand du dossier helpers
            CommandOuvrirPrincipal = new RelayCommandSP(Connexion);
            CommandOuvrirAjouterUtilisateur = new RelayCommandSP(OuvrirAjouterUtilisateur);
            CommandOuvrirInfos = new RelayCommandSP(OuvrirInfos);
            CommandQuitter = new RelayCommandSP(Quitter); 
        }
        // Ci-dessous les propriétés qui seront liés à la vue par les tags exemple: Text="{Binding SelectedItem.Marque, ElementName= listeVoitureDataGrid}" 
        // Le OnPropertyChanged implémente la notification des modifications de propriétés, elle utilise les méthodes contenu dans le dossier Helpers --> ObservableObjects
        // Le : ElementName=listeVoitureDataGrid fait référence au datagrid dans la vue xaml
        private string identifiantSaisi = null;
        public string IdentifiantSaisi
        {
            get { return identifiantSaisi; }
            set { identifiantSaisi = value; RaisePropertyChanged("Identifiant"); }
        }

        private string message = null;
        public string Message
        {
            get { return message; }
            set { message = value; RaisePropertyChanged("Message"); }
        }
        // Méthode pour se connecter
        private void Connexion()
        {
            // si l'identifiant n'est pas null, on charge la liste des utilisateurs (login et mot de passe)
            if (IdentifiantSaisi != null)
            {
                MaListeUtilisateur = ServiceUtilisateur.ObtenirMotPasse(IdentifiantSaisi);
                for (int i = 0; i < MaListeUtilisateur.Count; i++)
                {
                    MotPasse = MaListeUtilisateur.ElementAt(i).MotDePasseUtilisateur;
                    Identifiant = MaListeUtilisateur.ElementAt(i).NomUtilisateur.ToString();
                }
            }
            // si le mot de passe n'est pas null, on charge le mot de passe hashé en sha512
            if (Password.Password != null)
            {
                var secureString = Password.Password;
                var hash = ServiceUtilisateur.EncryypterSha512(secureString);
                MotPasseSaisiEncrypte = hash.ToString();
            }

            //Identifiant = MotDePasse + Active; 
            if (IdentifiantSaisi == Identifiant && MotPasseSaisiEncrypte == MotPasse)
            {
                Principal principal = new Principal();
                principal.Show();
                App.Current.Windows[0].Close();
            }
            else if (Identifiant == Password.Password)
            {
                Message = "Désolé votre compte est bloqué !\r\nContactez l'admin...";
            }
            else
            {
                Message = "Vos identifiants sont incorrectes !";
            }
        }
        // on crée une méthode pour sécuriser le mot de passe 
        // on convertit le string en un string securisé avec le System.Security.SecureString
        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;// retourne la chaîne vide 
            }
            // on représente un pointeur pour unr chaîne non gérée que l'on remet à zéro
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                // on utilise la methode Marshal.SecureStringToGlobalAllocUnicode pour copier le contenu d'un objet Secure String objet en mémoire non managée
                unmanagedString = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                //on utilise la méthode Marshal.PtrToStringUni pour allouer un objet string et copier dans cellui-ci tous les caractères jusqu'au 1er caractère non null d'une chaîne
                return System.Runtime.InteropServices.Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                // on utilise la méthode Marshal.ZeroFreeGlobalAllocUnicode pour liberer un pointeur de chaîne non managée qui a été alloué avec la 
                // méthode SecureStringToGlobalAllocUnicode
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
        // Méthode pour ouvrir la fenêtre pour ajouter un nouveau utilisateur
        private void OuvrirAjouterUtilisateur()
        { 
            // on déclare une fenêtre et on l'instancie
            AjouterUtilisateur fenetre = new AjouterUtilisateur();
            fenetre.Show();
        }
        // Méthode pour ouvrir la fenêtre info
        private void OuvrirInfos()
        {
            InfosViewModel.VerificationPageConnexion = true;
          
            Infos fenetre = new Infos();
            fenetre.Show();
            PrincipalViewModel.CompteurPage = 1;
        }
        // Méthode pour quitter
        private void Quitter()
        {
            App.Current.Windows[0].Close();
        }
    }
}

