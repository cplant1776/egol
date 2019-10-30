using Engine;
using Engine.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hephaestus.Views
{
    /// <summary>
    /// Interaction logic for StartViewModel.xaml
    /// </summary>
    public partial class StartView : Page
    {
        public Class1 class1;
        private readonly StartViewModel _viewModel = new StartViewModel();
        public StartView()
        {
            DataContext = _viewModel;
        }


        public void NewCharacter_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // NAVIGATE TO NEW CHARACTER PAGE
        }

        public void LoadCharacter_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.LoadCharacter_Click();
        }

        public void GeneratedCharacter_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.GeneratedCharacter_Click();
        }
    }
}
