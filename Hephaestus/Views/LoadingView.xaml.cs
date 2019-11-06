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
    /// Interaction logic for LoadingView.xaml
    /// </summary>
    public partial class LoadingView : UserControl
    {
        private readonly LoadingViewModel _viewModel = new LoadingViewModel();

        public LoadingView()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }
    }
}
