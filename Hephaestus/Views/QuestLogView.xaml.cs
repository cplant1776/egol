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
using Engine.ViewModels;

namespace Hephaestus.Views
{
    /// <summary>
    /// Interaction logic for QuestLogView.xaml
    /// </summary>
    public partial class QuestLogView : UserControl
    {
        private readonly QuestLogViewModel _viewModel = new QuestLogViewModel();
        public QuestLogView()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }

        /*
         * SelectedItem is readonly in a tree view, so cannot bind directly to its value in the view model
         * 
         */
        private void UpdateSelectedQuest(object sender, MouseButtonEventArgs args)
        {
            try
            {
                // Find clicked quest's name
                QuestModel targetQuest = (QuestModel)QuestTree.SelectedItem;
                int targetQuestId = targetQuest.Id;

                // Set selected quest & contact
                _viewModel.SelectedQuest = _viewModel.UserCharacter.GetQuest(targetId: targetQuestId);
                _viewModel.SelectedContact = _viewModel.UserCharacter.GetContact(targetId: _viewModel.SelectedQuest.ContactId);
                _viewModel.UpdateQuestState();
            }
            catch (InvalidCastException) // Quest category clicked
            {
                // Expand quest category
                (sender as TreeViewItem).IsExpanded = true;
            }



        }
    }
}
