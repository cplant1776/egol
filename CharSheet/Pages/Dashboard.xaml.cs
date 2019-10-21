using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using CharSheet.classes;
using CharSheet.classes.data;

namespace CharSheet.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page, INotifyPropertyChanged
    {
        private MainWindow _mainWindow;

        public MainWindow MainWindow
        {
            get { return _mainWindow; }
            set
            {
                _mainWindow = value;
                OnPropertyChanged("MainWindow");
            }
        }

        public Dashboard()
        {
            InitializeComponent();

            this.MainWindow = (MainWindow)Application.Current.MainWindow;

            AttributeControl.ItemsSource = this.MainWindow.CurrentCharacter.AttributeValue;
            SkillControl.ItemsSource = this.MainWindow.CurrentCharacter.SkillValue;
            // Show n most recent entries entries
            HistoryControl.ItemsSource = this.MainWindow.CurrentCharacter.EventHistory.Skip(
                this.MainWindow.CurrentCharacter.EventHistory.Count - AppSettings.NumOfRecentEvents);

            GenerateCurrentQuests();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void GenerateCurrentQuests()
        {
            foreach(Quest quest in MainWindow.CurrentCharacter.Quests)
            {
                if (quest.Status == (int)Quest.QuestStatus.CURRENT)
                {
                    TextBlock newQuest = new TextBlock()
                    {
                        Text = quest.Title
                    };

                    QuestList.Items.Add(newQuest);
                }
                else
                {
                    ;
                }

            }
        }

        private void AddMilestone_Click(object sender, RoutedEventArgs e)
        {
            DialogWindow popup = new DialogWindow();
            if(popup.ShowDialog() == true)
            {
                Milestone newMilestone = popup.result;
                MainWindow.CurrentCharacter.EventHistory.Add(newMilestone);
                MainWindow.CurrentCharacter.AttributeValue[newMilestone.AttributeId] += newMilestone.Value;
            }
            RefreshPage();
        }

        private void CompleteHistory_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateTo(AppSettings.pagePaths["FullHistory"], this.NavigationService);
        }

        private void AddQuest_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateTo(AppSettings.pagePaths["NewQuest"], this.NavigationService);
        }

        private void CompleteSelected_Click(object sender, RoutedEventArgs e)
        {
            // Find quest and mark as completed
            Quest targetQuest = new Quest();
            foreach(Quest q in MainWindow.CurrentCharacter.Quests)
            {
                if (q.Status == (int)Quest.QuestStatus.CURRENT)
                {
                    TextBlock selectedQuest = (TextBlock)QuestList.SelectedItems[0];
                    if (q.Title == selectedQuest.Text)
                    {
                        q.Status = (int)Quest.QuestStatus.COMPLETED;
                        targetQuest = q;
                    }
                }
            }
            // Update contact reputation
            foreach(Contact c in MainWindow.CurrentCharacter.CharacterContacts)
            {
                if (c.Id == targetQuest.ContactId)
                {
                    c.Reputation += targetQuest.ReputationValue;
                }
            }

            // Update XP
            MainWindow.UpdateXP(targetQuest.XPValue);
            RefreshPage();
        }

        private void QuestLog_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateTo(AppSettings.pagePaths["QuestLog"], this.NavigationService);
        }

        private void RefreshPage()
        {
            this.NavigationService.Refresh();
        }
    }
}
