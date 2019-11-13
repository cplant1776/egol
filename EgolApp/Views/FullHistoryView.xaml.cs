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
    /// Interaction logic for FullHistoryView.xaml
    /// </summary>
    public partial class FullHistoryView : UserControl
    {
        private readonly FullHistoryViewModel _viewModel = new FullHistoryViewModel();
        public FullHistoryView()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }

        /*
         * MouseGesture DoubleLeftClick and LeftClick unresponsive; handling in View for time being
         * 
         */
        private void EventList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_viewModel.GoToQuestCommand.CanExecute(sender))
            {
                _viewModel.GoToQuestCommand.Execute(sender);
            }
        }
    }
}
