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
    /// Interaction logic for CharacterCreationView.xaml
    /// </summary>
    public partial class CharacterCreationView : Page
    {
        private readonly CharacterCreationViewModel _viewModel = new CharacterCreationViewModel();

        public CharacterCreationView()
        {
            InitializeComponent();

            DataContext = _viewModel;
        }

        public void Done_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.Done_Click(name: CharacterName.Text, description: CharacterDescription.Text);
        }

        public void Cancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.Cancel_Click();
        }

        public void AddImage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.AddImage_Click();
        }

        public void PlusAttribute_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.PlusAttribute_Click(sender);
        }
        public void MinusAttribute_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.MinusAttribute_Click(sender);

        }

        public void PlusSkill_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.PlusSkill_Click(sender);
        }
        public void MinusSkill_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.MinusSkill_Click(sender);
        }
    }
}
