using CharSheet.classes.data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
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

namespace CharSheet.Pages
{
    /// <summary>
    /// Interaction logic for QuestLogPage.xaml
    /// </summary>
    public partial class QuestLogPage : Page, INotifyPropertyChanged
    {

        private Quest _selectedQuest;
        private Contact _selectedContact;

        public MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        public Quest SelectedQuest
        {
            get { return _selectedQuest; }
            set
            {
                _selectedQuest = value;
                OnPropertyChanged(() => SelectedQuest);
                OnPropertyChanged(() => SelectedContact);
            }
        }

        public Contact SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                OnPropertyChanged(() => SelectedContact);
                OnPropertyChanged(() => SelectedQuest);
            }
        }

        public QuestLogPage()
        {
            InitializeComponent();

            GenerateQuestsLists();
        }

        private void GenerateQuestsLists()
        {
            TextBlock questDisplay = new TextBlock();
            foreach(Quest q in mainWindow.CurrentCharacter.Quests)
            {
                questDisplay = new TextBlock()
                {
                    Text = q.Title
                };

                if (q.Status == (int)Quest.QuestStatus.CURRENT)
                {
                    CurrentQuestsList.Items.Add(questDisplay);
                }
                else if (q.Status == (int)Quest.QuestStatus.ACCEPTED)
                {
                    AcceptedQuestsList.Items.Add(questDisplay);
                }
                else if (q.Status == (int)Quest.QuestStatus.COMPLETED)
                {
                    CompleteQuestsList.Items.Add(questDisplay);
                }
            }
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            // Find quest and mark as completed
            Quest targetQuest = new Quest();
            foreach (Quest q in mainWindow.CurrentCharacter.Quests)
            {
                    if (q.Title == this.SelectedQuest.Title)
                    {
                        q.Status = (int)Quest.QuestStatus.COMPLETED;
                        targetQuest = q;
                    }
            }
            // Update contact reputation
            foreach (Contact c in mainWindow.CurrentCharacter.CharacterContacts)
            {
                if (c.Id == targetQuest.ContactId)
                {
                    c.Reputation += targetQuest.ReputationValue;
                }
            }

            // Update XP
            mainWindow.UpdateXP(targetQuest.XPValue);

            //Refresh page
            this.NavigationService.Refresh();
        }

        private void UpdateSelectedQuest(object sender, SelectionChangedEventArgs args)
        {
            if ((sender as ListView).SelectedItems.Count > 0)
            {
                TextBlock questTitle = (TextBlock)(sender as ListView).SelectedItems[0];

                // Locate selected quest object
                foreach (Quest q in mainWindow.CurrentCharacter.Quests)
                {
                    if (q.Title == questTitle.Text)
                    {
                        this.SelectedQuest = q;
                    }
                }
                // Set contact
                foreach (Contact c in mainWindow.CurrentCharacter.CharacterContacts)
                {
                    if (c.Id == this.SelectedQuest.ContactId)
                    {
                        this.SelectedContact = c;
                    }
                }
            }

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.NavigateTo(AppSettings.pagePaths["Dashboard"], this.NavigationService);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        // Just so we can call it via lambda which is nicer
        public void OnPropertyChanged<T>(Expression<Func<T>> propertyNameExpression)
        {
            OnPropertyChanged(((MemberExpression)propertyNameExpression.Body).Member.Name);
        }

        private void ListView_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as ListView).UnselectAll();
        }
    }
}
