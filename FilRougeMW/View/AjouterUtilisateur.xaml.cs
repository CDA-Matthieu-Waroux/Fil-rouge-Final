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
    /// Logique d'interaction pour AjouterUtilisateur.xaml
    /// </summary>
    public partial class AjouterUtilisateur : Window
    {
        public AjouterUtilisateur()
        {
            InitializeComponent();
            this.DataContext = new AjouterUtilisateurViewModel();
        }
    }
}
