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
    /// Interaction logic for NewQuestView.xaml
    /// </summary>
    public partial class NewQuestView : UserControl
    {
        private readonly NewQuestViewModel _viewModel = new NewQuestViewModel();
        public NewQuestView()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }

        private void QuestTitleUpdatedCommand(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return) // Return moves off of quest title rather than submits it
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                MoveFocus(request);
            }
            else
            {
                if (_viewModel.QuestTitleUpdatedCommand.CanExecute(sender))
                    _viewModel.QuestTitleUpdatedCommand.Execute(sender);
            }

        }
    }
}
