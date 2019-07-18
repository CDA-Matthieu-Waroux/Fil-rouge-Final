using FilRougeMW.Helpers;
using FilRougeMW.Model.Service;
using FilRougeMW.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FilRougeMW.ViewModel
{
    class OptionsModifierViewModel : ObservableObject, INotifyPropertyChanged
    {
        /// <summary>
        /// Modifier les options de la voiture
        /// </summary>

        // Création des propriétés pour les commandes à binder ---> lier dans le fichier xaml exemple : Command="{Binding CommandQuitter}"
        // Ces propriétés contiennent des getter & setter et interagissent avec la classe RelayCommand du dossier helpers
        //Création d'une liste pour stocker les données 
        // on encapsule les listes en utilisant la propriété
        public ICommand CommandAjouterOptionsByVoiture { get; private set; }
        public ICommand CommandCheckBox { get; private set; }
        public ICommand CommandOuvrirInfos { get; private set; }
        public ICommand CommandQuitter { get; private set; }

        public static ObservableCollection<string> Listeoptions { get => listeoptions; set => listeoptions = value; }
        private static ObservableCollection<string> listeoptions = new ObservableCollection<string>();

        public OptionsModifierViewModel()
        {
            // Création des propriétés pour les commandes à binder ---> lier dans le fichier xaml exemple : Command="{Binding CommandQuitter}"
            // Ces propriétés contiennent des getter & setter et interagissent avec la classe RelayCommand du dossier helpers
            // on instancie la liste Option
            CommandAjouterOptionsByVoiture = new RelayCommandSP(AjouterOptions);
            CommandCheckBox = new RelayCommandCB(executemethod, canexecutemethod);
            CommandOuvrirInfos = new RelayCommandSP(OuvrirInfos);
            CommandQuitter = new RelayCommandSP(Quitter);
            ListeOptions = new ObservableCollection<string>();
        }
        // Ci-dessous les propriétés qui seront liés à la vue par les tags exemple: Text="{Binding SelectedItem.Marque, ElementName= listeVoitureDataGrid}" 
        // Le OnPropertyChanged implémente la notification des modifications de propriétés, elle utilise les méthodes contenu dans le dossier Helpers --> ObservableObjects
        // Le : ElementName=listeVoitureDataGrid fait référence au datagrid dans la vue xaml
        private ObservableCollection<string> _listeOptions;
        public ObservableCollection<string> ListeOptions
        { 
            get { return _listeOptions; }
            set { _listeOptions = value;  RaisePropertyChanged("ListeOptions"); }
        }
        // méthode de Bool
        private bool canexecutemethod(object parameter)
        {
            return true;
        }
        // méthode pour selectionner les chekbox
        private void executemethod(object parameter)
        {
            var values = (object[])parameter;
            string options = (string)values[0];
            bool check = (bool)values[1];
            if (check)
            {
                ListeOptions.Add(options);
            }
            else
            {
                ListeOptions.Remove(options);
            }
        }
        // Mèthode pour ajouter option
        public void AjouterOptions()
        {
            for (int i = 0; i < ListeOptions.Count; i++)
            {
                ServiceOptions.AjouterOptionsByVoiture(PrincipalViewModel.Id, ListeOptions[i]);
            }
            App.Current.Windows[2].Close();
        }
        // Méthode pour ouvrir la fenêtre info
        private void OuvrirInfos()
        {
            InfosViewModel.VerificationPageConnexion = false;
            Infos fenetre = new Infos();
            fenetre.Show();
            PrincipalViewModel.CompteurPage = 3;
        }
        // Méthode Pour Quitter
        private void Quitter()
        {
            App.Current.Windows[2].Close();
        }
    }
}
