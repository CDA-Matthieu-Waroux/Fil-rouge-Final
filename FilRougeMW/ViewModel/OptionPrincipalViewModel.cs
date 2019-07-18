using FilRougeMW.Helpers;
using FilRougeMW.Model.Service;
using FilRougeMW.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FilRougeMW.ViewModel
{
    class OptionPrincipalViewModel : ObservableObject, INotifyPropertyChanged
    {
        /// <summary>
        /// Fenêtre options
        /// </summary>

        // Création des propriétés pour les commandes à binder ---> lier dans le fichier xaml exemple : Command="{Binding CommandQuitter}"
        // Ces propriétés contiennent des getter & setter et interagissent avec la classe RelayCommand du dossier helpers
        //Création d'une liste pour stocker les données 
        // on encapsule les listes en utilisant la propriété
        public ICommand CommandQuitter { get; private set; }
        public ICommand CommandOuvrirInfos { get; private set; }

        private static List<string> listeoptions;
        public static List<string> Listeoptions { get => listeoptions; set => listeoptions = value; }
        // getter la liste
        public static List<string> ListeOptions
        {
            get { return Listeoptions; }
        } 
        
        public OptionPrincipalViewModel ()
        {
            // Création des propriétés pour les commandes à binder ---> lier dans le fichier xaml exemple : Command="{Binding CommandQuitter}"
            // Ces propriétés contiennent des getter & setter et interagissent avec la classe RelayCommand du dossier helpers
            // Chargement des données
            CommandQuitter = new RelayCommandSP(Quitter);
            CommandOuvrirInfos = new RelayCommandSP(OuvrirInfos);
            
            listeoptions = ServiceOptions.ChargeeDonneeOptionsByID(PrincipalViewModel.Id); 
        } 

        // Méthode Pour Quitter 
        private void Quitter()
        {
            Listeoptions.Clear();
            App.Current.Windows[1].Close();
        } 
        // Méthode pour ouvrir la fenêtre info
        private void OuvrirInfos()
        {
            InfosViewModel.VerificationPageConnexion = false;
            Infos fenetre = new Infos();
            fenetre.Show();
            PrincipalViewModel.CompteurPage = 2;
        }
    }
}
