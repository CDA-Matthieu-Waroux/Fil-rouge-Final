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

namespace FilRougeMW
{
    /// <summary>
    /// Logique d'interaction pour Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        public Principal()
        {
            InitializeComponent();
            // Le DataContext pour afficher les éléments dans le logiciel
            // Le DataContext, nous permet d'utiliser le "Binding"
            this.DataContext = new PrincipalViewModel();
        }
    }
}
