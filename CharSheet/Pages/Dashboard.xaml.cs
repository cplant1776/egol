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

            GenerateAttributeRows();
            GenerateSkillRows();
            GenerateHistory();
            GenerateCurrentQuests();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void GenerateHistory()
        {
            foreach(HistoryEntry e in MainWindow.CurrentCharacter.EventHistory)
            {
                HistoryStack.Children.Add(e.GenerateHistoryEntryTextBlock());
            }
        }

        private void GenerateAttributeRows()
        {
            foreach (KeyValuePair<int, int> entry in MainWindow.CurrentCharacter.AttributeValue)
            {
                // Create row from current character's values
                var newRow = new AttributeRow(DataHandler.getAttributeDesc(entry.Key), entry.Value);
                AttributeStack.Children.Add(newRow.GenerateAttributeDisplayRow());
            };
        }

        private void GenerateSkillRows()
        {
            // Iterate through skill dictionary
            foreach (KeyValuePair<int, int> entry in MainWindow.CurrentCharacter.SkillValue)
            {
                // Create row from current character's values
                var newRow = new SkillRow(DataHandler.getSkillDesc(entry.Key), entry.Value);
                SkillStack.Children.Add(newRow.GenerateSkillDisplayRow());
            };
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
                HistoryEntry newEntry = popup.result;
                MainWindow.CurrentCharacter.Add(newEntry);
                MainWindow.CurrentCharacter.AttributeValue[newEntry.primarySkill] += newEntry.value;
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
