using FilRougeMW.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FilRougeMW.View
{
    /// <summary>
    /// Logique d'interaction pour Ajouter.xaml
    /// </summary>
    public partial class Ajouter : Window
    {
        public Ajouter()
        {
            InitializeComponent();
            this.DataContext = new AjouterViewModel();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

            AjouterViewModel.Listecarburant.Clear();
            AjouterViewModel.Listecategorie.Clear();
            AjouterViewModel.Listecouleur.Clear();
            AjouterViewModel.Listemarque.Clear();
            AjouterViewModel.Listeannee.Clear();
            AjouterViewModel.Listemodele.Clear();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            AjouterViewModel.Listecarburant.Clear();
            AjouterViewModel.Listecategorie.Clear();
            AjouterViewModel.Listecouleur.Clear();
            AjouterViewModel.Listemarque.Clear();
            AjouterViewModel.Listeannee.Clear();
            AjouterViewModel.Listemodele.Clear();
        }
    }
}
