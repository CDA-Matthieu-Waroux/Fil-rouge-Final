﻿using FilRougeMW.ViewModel;
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
    /// Logique d'interaction pour OptionsAjouter.xaml
    /// </summary>
    public partial class OptionsAjouter : Window
    {
        public OptionsAjouter()
        {
            InitializeComponent();
            this.DataContext = new OptionsAjouterViewModel();
        }
    }
}
