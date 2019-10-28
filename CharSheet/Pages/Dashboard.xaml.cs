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
using CharSheet.classes.display;
using MaterialDesignThemes.Wpf;

namespace CharSheet.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : MyBasePage, INotifyPropertyChanged
    {
        private MainWindow _mainWindow;
        private List<AttributeRow> _attributeRows = new List<AttributeRow> { };
        private List<SkillRow> _skillRows = new List<SkillRow> { };
        private List<Quest> _quests = new List<Quest> { };
        private List<EventRecord> _eventRecords = new List<EventRecord> { };

        public MainWindow MainWindow
        {
            get { return _mainWindow; }
            set
            {
                _mainWindow = value;
                OnPropertyChanged("MainWindow");
            }
        }

        public List<AttributeRow> AttributeRows
        {
            get { return _attributeRows; }
            set
            {
                _attributeRows = value;
                OnPropertyChanged("AttributeRows");
            }
        }

        public List<SkillRow> SkillRows
        {
            get { return _skillRows; }
            set
            {
                _skillRows = value;
                OnPropertyChanged("SkillRows");
            }
        }

        public List<Quest> Quests
        {
            get { return _quests; }
            set
            {
                _quests = value;
                OnPropertyChanged("Quests");
            }
        }

        public List<EventRecord> EventRecords
        {
            get { return _eventRecords; }
            set
            {
                _eventRecords = value;
                OnPropertyChanged("EventRecords");
            }
        }

        public Dashboard()
        {
            InitializeComponent();

            this.MainWindow = (MainWindow)Application.Current.MainWindow;
            GenerateAttributeRows();
            GenerateSkillRows();
            GenerateMilestoneDialogAttributeList();

            AttributeList.ItemsSource = this.AttributeRows;
            SkillList.ItemsSource = this.SkillRows;

            // Show n most recent entries 
            int startingIndex = Math.Max(0, this.MainWindow.CurrentCharacter.EventHistory.Count() - AppSettings.NumOfRecentEvents);
            int endingIndex = this.MainWindow.CurrentCharacter.EventHistory.Count();
            for (int i = startingIndex; i < endingIndex; i++)
                this.EventRecords.Add(this.MainWindow.CurrentCharacter.EventHistory[i]);
            HistoryControl.ItemsSource = this.EventRecords;
            // Sort recent events by date
            CollectionView eventsView = (CollectionView)CollectionViewSource.GetDefaultView(HistoryControl.ItemsSource);
            eventsView.SortDescriptions.Add(new SortDescription("Timestamp", ListSortDirection.Ascending));

            // Filter quests such that only Active ones appear
            QuestList.ItemsSource = this.MainWindow.CurrentCharacter.Quests;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(QuestList.ItemsSource);
            view.Filter = QuestFilter;

        }

        // Ensure only active quests are displayed
        private bool QuestFilter(object item)
        {
            if((item as Quest).Status == (int)Quest.QuestStatus.ACTIVE)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GenerateAttributeRows()
        {
            foreach (KeyValuePair<int, int> entry in this.MainWindow.CurrentCharacter.AttributeValue)
            {
                AttributeRows.Add(new AttributeRow(name: DataHandler.getAttributeDesc(entry.Key), value: entry.Value));
            }
        }

        private void GenerateSkillRows()
        {
            foreach (KeyValuePair<int, int> entry in this.MainWindow.CurrentCharacter.SkillValue)
            {
                SkillRows.Add(new SkillRow(name: DataHandler.getSkillDesc(entry.Key), value: entry.Value));
            }
        }

        private void GenerateMilestoneDialogAttributeList()
        {
            List<string> attributeNames = new List<string> { };
            foreach (string val in AppSettings.Attributes.Values)
            {
                attributeNames.Add(val);
            }
            AttributeInput.ItemsSource = attributeNames;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
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
            Quest targetQuest;

            try
            {
                targetQuest = (Quest)QuestList.SelectedItems[0];
            }
            catch (ArgumentOutOfRangeException) //No quest was selected
            {
                return;
            }

            foreach (Quest q in MainWindow.CurrentCharacter.Quests)
            {
                if (q.Status == (int)Quest.QuestStatus.ACTIVE)
                {
                    if (q.Id == targetQuest.Id)
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
            

            // Add new event record
            XPEvent newRecord = new XPEvent(
                description : "Completed " + targetQuest.Title + ".",
                eventId: targetQuest.Id,
                primarySkill: -1,
                value: targetQuest.XPValue
                );

            AddNewRecord(newRecord);
            ScrollToBottomOfEvents();

            // Refresh quest filter
            CollectionViewSource.GetDefaultView(QuestList.ItemsSource).Refresh();
        }

        private void AddNewRecord(XPEvent e)
        {
            // Update character's event history
            this.MainWindow.CurrentCharacter.EventHistory.Add(e);
            // Update event history display
            if(this.EventRecords.Count >= AppSettings.NumOfRecentEvents)
            {
                this.EventRecords.RemoveAt(0);
            }
            this.EventRecords.Add(e);
            HistoryControl.Items.Refresh();
        }

        private void AddNewRecord(Milestone e)
        {
            // Update character
            this.MainWindow.CurrentCharacter.EventHistory.Add(e);
            this.MainWindow.CurrentCharacter.AttributeValue[e.AttributeId] += e.Value;
            // Update event history display
            this.EventRecords.RemoveAt(0);
            this.EventRecords.Add(e);
            HistoryControl.Items.Refresh();
        }

        private void ScrollToBottomOfEvents()
        {
            HistoryControl.SelectedIndex = HistoryControl.Items.Count - 1;
            HistoryControl.ScrollIntoView(HistoryControl.SelectedItem);
        }

        private void QuestLog_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateTo(AppSettings.pagePaths["QuestLog"], this.NavigationService);
        }

        private void QuestList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Quest selectedQuest = new Quest();
            // Find selected quest
            try
            {
                selectedQuest = (Quest)QuestList.SelectedItems[0];

            }
            catch (ArgumentOutOfRangeException) // Clicks were on icon (whose event is not implemented yet)
            {
                return;
            }

            // Update default quest displayed when opening quest log
            AppSettings.UpdateDefaultSelectedQuest(selectedQuest.Id);
            NavigateToPage("QuestLog");
        }

        private void HistoryControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Find quest associated with entry
            EventRecord selectedEvent = (EventRecord)HistoryControl.SelectedItems[0];
            Quest targetQuest = this.MainWindow.CurrentCharacter.GetQuest(targetId: selectedEvent.AssociatedEventId);
            // Update default quest displayed when opening quest log
            AppSettings.UpdateDefaultSelectedQuest(targetQuest.Id);
            NavigateToPage("QuestLog");
        }

        private void MilestoneDialogDone_Click(object sender, RoutedEventArgs e)
        {
            // Generate Id
            Random rnd = new Random();
            int newId = rnd.Next(1, 600000); // generate random id
            Milestone newMilestone = new Milestone(
                                                description: this.DescriptionInput.Text,
                                                eventId: newId,
                                                attributeId: DataHandler.getAttributeId(AttributeInput.Text),
                                                value: Convert.ToInt32(ValueInput.Text)
                                             );
            this.AddNewRecord(newMilestone);
            RefreshPage();
            ScrollToBottomOfEvents();
        }
    }
}
