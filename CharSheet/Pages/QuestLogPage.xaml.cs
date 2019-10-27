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
            this.Loaded += new RoutedEventHandler(QuestLogPage_Loaded);

            //TODO: Implement this using HierarchicalDataTemplate
            // https://docs.microsoft.com/en-us/dotnet/api/system.windows.hierarchicaldatatemplate?view=netframework-4.8
            GenerateQuestsLists();
        }

        void QuestLogPage_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateSelectedQuest();
        }


        private void GenerateQuestsLists()
        {
            // Clear out previous list
            ActiveQuestsItem.Items.Clear();
            AcceptedQuestsItem.Items.Clear();
            CompletedQuestsItem.Items.Clear();

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
                    ActiveQuestsItem.Items.Add(newItem);
                }
                else if (q.Status == (int)Quest.QuestStatus.ACCEPTED)
                {
                    AcceptedQuestsItem.Items.Add(newItem);
                }
                else if (q.Status == (int)Quest.QuestStatus.COMPLETED)
                {
                    CompletedQuestsItem.Items.Add(newItem);
                }
            }
        }

        private void UpdateSelectedQuest()
        {
            Quest selectedQuest = new Quest();
            if(AppSettings.DefaultSelectedQuest == null) // Default quest IS NOT set
            {
                // Default to first Active quest
                if (!ActiveQuestsItem.Items.IsEmpty) 
                {
                    TreeViewItem item = (TreeViewItem)ActiveQuestsItem.Items[0];
                    selectedQuest = this.mainWindow.CurrentCharacter.GetQuest(targetTitle: item.Header.ToString());
                }
            }
            else // Default quest IS set
            {
                bool questLocated = false;
                // Search active quests
                foreach(TreeViewItem i in ActiveQuestsItem.Items)
                {
                    // Find corresponding quest in treeview
                    if (i.Header.ToString() == AppSettings.DefaultSelectedQuest) 
                    {
                        selectedQuest = this.mainWindow.CurrentCharacter.GetQuest(targetTitle: i.Header.ToString());
                        questLocated = true;
                        break;
                    }
                }
                // If not found in active quests, search completed quests
                if (!questLocated)
                {
                    foreach (TreeViewItem i in CompletedQuestsItem.Items)
                    {
                        if (i.Header.ToString() == AppSettings.DefaultSelectedQuest) // Find corresponding quest in treeview
                        {
                            selectedQuest = this.mainWindow.CurrentCharacter.GetQuest(targetTitle: i.Header.ToString());
                            break;
                        }
                    }
                }
            }

            // Update displayed quest and contact
            this.SelectedQuest = selectedQuest;
            this.SelectedContact = this.mainWindow.CurrentCharacter.GetContact(targetId: selectedQuest.ContactId);
            AppSettings.ResetDefaultSelectedQuest();

            SetQuestStatus();
            UpdateVisibility();
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

            // Set selected quest & contact
            this.SelectedQuest = this.mainWindow.CurrentCharacter.GetQuest(targetTitle: targetTitle);
            this.SelectedContact = this.mainWindow.CurrentCharacter.GetContact(targetId: this.SelectedQuest.ContactId);

            SetQuestStatus();
            UpdateVisibility();
            
        }

        private void SetQuestStatus()
        {
            if(this.SelectedQuest.Status == (int)Quest.QuestStatus.CURRENT)
            {
                ActiveButton.IsChecked = true;
                AcceptedButton.IsChecked = false;
                CompletedButton.IsChecked = false;
            }
            else if (this.SelectedQuest.Status == (int)Quest.QuestStatus.ACCEPTED)
            {
                ActiveButton.IsChecked = false;
                AcceptedButton.IsChecked = true;
                CompletedButton.IsChecked = false;
            }
            else if (this.SelectedQuest.Status == (int)Quest.QuestStatus.COMPLETED)
            {
                ActiveButton.IsChecked = false;
                AcceptedButton.IsChecked = false;
                CompletedButton.IsChecked = true;
            }
        }

        private void UpdateVisibility()
        {
            // Make selected quest content visible if not currently shown
            if (SelectedQuestInfo.Visibility.Equals(Visibility.Hidden))
            {
                SelectedQuestInfo.Visibility = Visibility.Visible;
            }

            // Hide/show due date
            if (SelectedQuest.Deadline == DateTime.MinValue) //no deadline
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
            // Expand quest category
            (sender as TreeViewItem).IsExpanded = true;
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
            // Unhighlight selected quest in tree view
            (sender as ListView).UnselectAll();
        }

        private void QuestStatusRadio_Checked(object sender, RoutedEventArgs e)
        {
            string typeOfButton = (sender as RadioButton).Tag.ToString();
            switch(typeOfButton)
            {
                case "Active":
                    break;
                case "Accepted":
                    break;
                case "Completed":
                    break;
                default:
                    break;
            }
        }

        private void SetActiveAccept_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedQuest.Status = (int)Quest.QuestStatus.CURRENT;
            GenerateQuestsLists();
        }

        private void SetAcceptedAccept_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedQuest.Status = (int)Quest.QuestStatus.ACCEPTED;
            GenerateQuestsLists();
        }

        private void SetCompletedAccept_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedQuest.Status = (int)Quest.QuestStatus.COMPLETED;

            // Update contact reputation
            foreach (Contact c in this.mainWindow.CurrentCharacter.CharacterContacts)
            {
                if (c.Id == this.SelectedQuest.ContactId)
                {
                    c.Reputation += this.SelectedQuest.ReputationValue;
                }
            }

            // Update XP
            this.mainWindow.UpdateXP(this.SelectedQuest.XPValue);


            // Add new event record
            XPEvent newRecord = new XPEvent(
                description: "Completed " + this.SelectedQuest.Title + ".",
                primarySkill: -1,
                value: this.SelectedQuest.XPValue
                );

            this.mainWindow.CurrentCharacter.EventHistory.Add(newRecord);
            GenerateQuestsLists();
        }

        private void StatusChangeCancel_Button(object sender, RoutedEventArgs e)
        {
            // Reset radio button
            SetQuestStatus();
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
