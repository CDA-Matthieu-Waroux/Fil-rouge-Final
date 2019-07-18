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
    /// Logique d'interaction pour OptionsPrincipal.xaml
    /// </summary>
    public partial class OptionsPrincipal : Window
    {
        public OptionsPrincipal()
        {
            InitializeComponent();
            this.DataContext = new OptionPrincipalViewModel();
        }
        public OptionsPrincipal(int b)
        {
            InitializeComponent();
            this.DataContext = new OptionPrincipalViewModel();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            OptionPrincipalViewModel.Listeoptions.Clear();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OptionPrincipalViewModel.Listeoptions.Clear();
        }
    }
}
