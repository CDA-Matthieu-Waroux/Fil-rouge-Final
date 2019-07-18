using FilRougeMW.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FilRougeMW.ViewModel
{
    class InfosViewModel : ObservableObject, INotifyPropertyChanged
    {
        /// <summary>
        /// Fenêtre info
        /// </summary>

        // Création des propriétés pour les commandes à binder ---> lier dans le fichier xaml exemple : Command="{Binding CommandQuitter}"
        // Ces propriétés contiennent des getter & setter et interagissent avec la classe RelayCommand du dossier helpers
        // déclaration des attributs 
        // Encapsulation des attrubuts pour créer des propriétés
        private static int compteurInfos;
        private int compteurDePage ;
        private static bool verificationPageConnexion;
        // Encapsulation des attrubuts pour créer des propriétés
        public static int CompteurInfos { get => compteurInfos; set => compteurInfos = value; }
        public static bool VerificationPageConnexion { get => verificationPageConnexion; set => verificationPageConnexion = value; }
        public int CompteurDePage { get => compteurDePage; set => compteurDePage = value; }

        public ICommand CommandQuitter { get; private set; }
        public ICommand CommandOuvrirInfos { get; private set; }
        
       
        public InfosViewModel()
        {  
            // Compteur pour le nombre de fenêtre ouverte
            PrincipalViewModel.CompteurInfos++;
            // Création des propriétés pour les commandes à binder ---> lier dans le fichier xaml exemple : Command="{Binding CommandQuitter}"
            // Ces propriétés contiennent des getter & setter et interagissent avec la classe RelayCommand du dossier helpers
            CommandQuitter = new RelayCommandSP(Quitter);
            CommandOuvrirInfos = new RelayCommandSP(OuvrirInfos);
            // Chargement du texte info
            ChargerTexteInfos(); 
        } 
        // Méthode pour charger la méthode info
        private void ChargerTexteInfos()
        {
            // si la connection est verifié, alors le texte s'ouvrir; sinon le compteur se met en route
            // si on ouvre le nombre d'ouverture est inférieur ou égal à 3, on a le texte modifier qui s'affiche
            // si c'est inférieur ou égal à 5, le texte final s'affiche
            if (verificationPageConnexion == true)
            {
                TexteConnexion();
            }
            else
            {
                compteurInfos = PrincipalViewModel.CompteurInfos;

                if (compteurInfos == 3)
                {
                    TexteModifier(); 
                }
                else if ( compteurInfos == 5)
                {
                    TexteFinal();
                } 
                else
                {
                    TexteNormal();
                } 
            } 
        }
        // méthode pour afficher le texte normal
        private void TexteNormal()
        { 
            Texteinfos = "Cette Application a été faite par Mohammed Et Matthieu dans le cadre d'une formation";
            Texteinfos += "\r \n";
            Texteinfos += "Celle-ci est codé en c# ";
            Texteinfos += " \r \n"; 
            Texteinfos += "C'est le fil rouge qui nous accompagnera tout au long de la formation Développeur Logiciel à L'AFPA de Roubaix  section 2018/2019"; 
        }
        // méthode pour afficher le texte de connection
        private void TexteConnexion()
        {
            Texteinfos = "SECRET DEFENSE SORRY :')";
            MessageBox.Show( "Désolé mais comme tu n'es pas connecté , je peux pas te laisser d'infos :'( "); 
        }
        // méthode pour afficher le texte modifier
        private void TexteModifier()
        {
            TexteNormal();
            MessageBox.Show("Tu as pas marre de lire les infos ??? ");
        }
        // méthode pour afficher le texte final
        private void TexteFinal()
        {
            MessageBox.Show("Désolé mais je n'ai plus vraiment de salive je suis désolé :c \n à la prochaine :D");
            Environment.Exit(0);
        } 
        // méthode pour quitter la fenêtre
        private void Quitter()
        { 
            App.Current.Windows[PrincipalViewModel.CompteurPage].Close();
            PrincipalViewModel.CompteurPage--;
        }
        // méthode pour ouvvrir info
        private void OuvrirInfos()
        {
            PrincipalViewModel.CompteurPage ++;
            Infos fenetre = new Infos();
            fenetre.Show();
        } 

        private string _texteinfos;
        public string Texteinfos
        {
            get { return _texteinfos; }
            set { _texteinfos = value; RaisePropertyChanged("Texteinfos"); }
        } 
    }
}
