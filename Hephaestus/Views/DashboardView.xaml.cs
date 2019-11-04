using System;
using Engine.ViewModels;
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
using Engine.Utils;

namespace Hephaestus.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        private readonly DashboardViewModel _viewModel = new DashboardViewModel();

        public DashboardView()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }


        /*
         * MouseGesture DoubleLeftClick and LeftClick unresponsive; handling in View for time being
         * 
         */
        private void QuestList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(_viewModel.GoToQuestCommand.CanExecute(sender))
            {
                _viewModel.GoToQuestCommand.Execute(sender);
            }
        }
    }
}
