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
using System.Collections.ObjectModel;

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

            //TODO: Implement this using HierarchicalDataTemplate
            // https://docs.microsoft.com/en-us/dotnet/api/system.windows.hierarchicaldatatemplate?view=netframework-4.8
            GenerateQuestsLists();
        }

        private void GenerateQuestsLists()
        {
            List<QuestMenuCategory> questListRoot = new List<QuestMenuCategory>
            {
                new QuestMenuCategory() {Title = "Active Quests"},
                new QuestMenuCategory() {Title = "Accepted Quests"},
                new QuestMenuCategory() {Title = "Completed Quests"},
            };

            foreach (Quest q in mainWindow.CurrentCharacter.Quests)
            {
                TreeViewItem newItem = new TreeViewItem
                {
                    Header = q.Title,
                    Foreground = (Brush)FindResource("PrimaryHueMidForegroundBrush"),
                };

                newItem.MouseLeftButtonUp += new MouseButtonEventHandler(UpdateSelectedQuest);

                if (q.Status == (int)Quest.QuestStatus.CURRENT)
                {
                    //questListRoot[0].AddQuest(new QuestMenuItem() { Title = q.Title });
                    ActiveQuestsItem.Items.Add(newItem);
                }
                else if (q.Status == (int)Quest.QuestStatus.ACCEPTED)
                {
                    //questListRoot[1].AddQuest(new QuestMenuItem() { Title = q.Title });
                    AcceptedQuestsItem.Items.Add(newItem);
                }
                else if (q.Status == (int)Quest.QuestStatus.COMPLETED)
                {
                    //questListRoot[2].AddQuest(new QuestMenuItem() { Title = q.Title });
                    CompletedQuestsItem.Items.Add(newItem);
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

        private void UpdateSelectedQuest(object sender, MouseButtonEventArgs args)
        {
            // Find clicked quest's name
            var targetSender = (TreeViewItem)sender;
            string targetTitle = targetSender.Header.ToString();
            
            
                // Locate selected quest object
                foreach (Quest q in mainWindow.CurrentCharacter.Quests)
                {
                    if (q.Title == targetTitle)
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

            // Make selected quest content visible if not currently shown
            if(SelectedQuestInfo.Visibility.Equals(Visibility.Hidden))
            {
                SelectedQuestInfo.Visibility = Visibility.Visible;
            }

            // Hide/show due date
            if(SelectedQuest.Deadline == DateTime.MinValue) //no deadline
            {
                DeadlineGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                DeadlineGrid.Visibility = Visibility.Visible;
            }
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.NavigateTo(AppSettings.pagePaths["Dashboard"], this.NavigationService);
        }

        private void QuestCategory_Click(object sender, RoutedEventArgs e)
        {

            (sender as TreeViewItem).IsExpanded = true;
            /*
            //Toggle menu expansion on click
            if ((sender as TreeViewItem).IsExpanded)
                (sender as TreeViewItem).IsExpanded = false;
            else
                (sender as TreeViewItem).IsExpanded = true;
                */
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

    public class QuestMenuItem
    {
        public string Title { get; set; }

        public QuestMenuItem()
        {
            
        }
    }

    public class QuestMenuCategory
    {
        public string Title { get; set; }
        public List<QuestMenuItem> Quests = new List<QuestMenuItem> { };

        public QuestMenuCategory()
        {

        }

        public void AddQuest(QuestMenuItem q)
        {
            this.Quests.Add(q);
        }
    }
}
