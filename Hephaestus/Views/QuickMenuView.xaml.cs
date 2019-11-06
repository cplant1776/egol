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
    /// Interaction logic for QuickMenuView.xaml
    /// </summary>
    public partial class QuickMenuView : UserControl
    {
        private QuickMenuViewModel _viewModel = new QuickMenuViewModel();
        public QuickMenuView()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }
    }
}
