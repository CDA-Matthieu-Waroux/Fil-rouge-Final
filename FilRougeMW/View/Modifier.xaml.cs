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
using FilRougeMW.ViewModel;

namespace FilRougeMW.View
{
    /// <summary>
    /// Logique d'interaction pour Modifier.xaml
    /// </summary>
    public partial class Modifier : Window
    {
       

        public Modifier()
        {
            InitializeComponent();
            this.DataContext = new ModifierViewModel();
            DataGridCacheParImage.SelectedIndex = 0;
        }
        public Modifier (int id)
        {
            InitializeComponent();
            this.DataContext = new ModifierViewModel();
            DataGridCacheParImage.SelectedIndex = 0;
            
        }

        

        private void Window_Closed(object sender, EventArgs e)
        {
            ModifierViewModel.Listevoiture.Clear();
            ModifierViewModel.Listecarburant.Clear();
            ModifierViewModel.Listecategorie.Clear();
            ModifierViewModel.Listecouleur.Clear();
            ModifierViewModel.Listemarque.Clear();
            ModifierViewModel.Listeannee.Clear();
            ModifierViewModel.Listemodele.Clear();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ModifierViewModel.Listevoiture.Clear();
            ModifierViewModel.Listecarburant.Clear();
            ModifierViewModel.Listecategorie.Clear();
            ModifierViewModel.Listecouleur.Clear();
            ModifierViewModel.Listemarque.Clear();
            ModifierViewModel.Listeannee.Clear();
            ModifierViewModel.Listemodele.Clear();
        }
    }
}
