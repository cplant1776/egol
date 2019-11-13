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

namespace Egol.Views
{
    /// <summary>
    /// Interaction logic for NewContactView.xaml
    /// </summary>
    public partial class NewContactView : UserControl
    {
        private NewContactViewModel _viewModel = new NewContactViewModel();
        public NewContactView()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }
    }
}
