using Engine.Models;
using Engine.Utils;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Engine.ViewModels
{
    public class DashboardViewModel : BaseViewModel, IPageViewModel, INotifyPropertyChanged
    {
        #region Fields
        private ObservableCollection<StatRow> _attributeRows = new ObservableCollection<StatRow> { };
        private ObservableCollection<StatRow> _skillRows = new ObservableCollection<StatRow> { };
        private ObservableCollection<EventRecordModel> _eventRecords = new ObservableCollection<EventRecordModel> { };
        private int _characterXP;

        private ICommand _fullHistory;
        private ICommand _completeSelected;
        private ICommand _addQuest;
        private ICommand _questLog;

        private ICommand _saveCharacterAs;
        private ICommand _saveCharacter;
        private ICommand _loadCharacter;
        private ICommand _exitProgram;
        private ICommand _goToHistoryEvent;
        private ICommand _goToQuest;
        private ICommand _openMilestoneDialogCommand;

        #endregion

        #region Constructors
        public DashboardViewModel()
        {
            GenerateStatRows();
            this.CharacterXP = this.UserCharacter.CurrentXP;

            // Set quest filter
            this.ActiveQuests = new ObservableCollection<QuestModel>(this.UserCharacter.Quests);
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ActiveQuests);
            view.Filter = QuestFilter;

            // Add listener for when event added
            this._eventRecords = new ObservableCollection<EventRecordModel>(this.UserCharacter.EventHistory);
            this._eventRecords.CollectionChanged += EventRecords_CollectionChanged;

        }
        #endregion

        #region Public Properties/Commands

        public ObservableCollection<StatRow> AttributeRows { get {  return _attributeRows; } set { _attributeRows = value; } }

        public ObservableCollection<StatRow> SkillRows { get { return _skillRows; } set { _skillRows = value; } }

        public int CharacterXP { get { return _characterXP; } set { _characterXP = value; OnPropertyChanged("CharacterXP"); OnPropertyChanged("CharacterLevel"); OnPropertyChanged("LevelProgress"); } }
        public int CharacterLevel { get { return _characterXP / 100; } set { OnPropertyChanged("CharacterXP"); } }
        public int LevelProgress { get { return _characterXP % 100; }  set { OnPropertyChanged("CharacterXP"); }}
        public ObservableCollection<QuestModel> ActiveQuests { get; set; }

        public QuestModel SelectedQuest { get; set; }

        public ObservableCollection<EventRecordModel> EventRecords
        {
            get
            {
                // Display n most recent entries
                int startingIndex = Math.Max(0, _eventRecords.Count() - AppSettings.NumOfRecentEvents);
                int endingIndex = _eventRecords.Count();
                ObservableCollection<EventRecordModel> recentRecords = new ObservableCollection<EventRecordModel> { };
                for(int i = startingIndex; i < endingIndex; i++)
                    recentRecords.Add(_eventRecords[i]);
                return recentRecords;
            }
        }

        private void EventRecords_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("EventRecords");
        }

        public string Name
        {
            get { return "CharacterCreation"; }
        }

        public ICommand FullHistoryCommand
        {
            get
            {
                if (_fullHistory == null)
                {
                    _fullHistory = new RelayCommand(
                        param => FullHistory()
                    );
                }
                return _fullHistory;
            }
        }

        public ICommand CompleteSelectedCommand
        {
            get
            {
                if (_completeSelected == null)
                {
                    _completeSelected = new RelayCommand(
                        param => CompleteSelected()
                    );
                }
                return _completeSelected;
            }
        }

        public ICommand QuestLogCommand
        {
            get
            {
                if (_questLog == null)
                {
                    _questLog = new RelayCommand(
                        param => QuestLog()
                    );
                }
                return _questLog;
            }
        }

        public ICommand AddQuestCommand
        {
            get
            {
                if (_addQuest == null)
                {
                    _addQuest = new RelayCommand(
                        param => AddQuest()
                    );
                }
                return _addQuest;
            }
        }

        public ICommand SaveCharacterCommand
        {
            get
            {
                if (_saveCharacter == null)
                {
                    _saveCharacter = new RelayCommand(
                        param => SaveCharacter()
                    );
                }
                return _saveCharacter;
            }
        }

        public ICommand SaveCharacterAsCommand
        {
            get
            {
                if (_saveCharacterAs == null)
                {
                    _saveCharacterAs = new RelayCommand(
                        param => SaveCharacterAs()
                    );
                }
                return _saveCharacterAs;
            }
        }

        public ICommand LoadCharacterCommand
        {
            get
            {
                if (_loadCharacter == null)
                {
                    _loadCharacter = new RelayCommand(
                        param => LoadCharacter()
                    );
                }
                return _loadCharacter;
            }
        }

        public ICommand ExitProgramCommand
        {
            get
            {
                if (_exitProgram == null)
                {
                    _exitProgram = new RelayCommand(
                        param => ExitProgram()
                    );
                }
                return _exitProgram;
            }
        }

        public ICommand GoToHistoryEventCommand
        {
            get
            {
                if (_goToHistoryEvent == null)
                {
                    _saveCharacterAs = new RelayCommand(
                        param => GoToHistory()
                    );
                }
                return _goToHistoryEvent;
            }
        }

        public ICommand GoToQuestCommand
        {
            get
            {
                if (_goToQuest == null)
                {
                    _goToQuest = new RelayCommand(
                        param => GoToQuest(param)
                    );
                }
                return _goToQuest;
            }
        }

        public ICommand OpenMilestoneDialogCommand
        {
            get
            {
                if (_openMilestoneDialogCommand == null)
                {
                    _openMilestoneDialogCommand = new RelayCommand(
                        param => OpenMilestoneDialog()
                    );
                }
                return _openMilestoneDialogCommand;
            }
        }
        #endregion

        #region Filters
        // Ensure only active quests are displayed
        private bool QuestFilter(object item)
        {
            if ((item as QuestModel).Status == (int)QuestModel.QuestStatus.ACTIVE)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
        #endregion

        #region Methods

        private void GenerateStatRows()
        {
            // Clear out old values
            this.AttributeRows.Clear();
            this.SkillRows.Clear();
            // Generate attribute rows
            foreach (KeyValuePair<int, int> entry in this.UserCharacter.AttributeValue)
            {
                this._attributeRows.Add(new StatRow(name: DataHandler.getAttributeDesc(entry.Key), value: entry.Value, isSkill: false));
            }

            // Generate skill rows
            foreach (KeyValuePair<int, int> entry in this.UserCharacter.SkillValue)
            {
                this.SkillRows.Add(new StatRow(name: DataHandler.getSkillDesc(entry.Key), value: entry.Value, isSkill: true));
            }
        }

        private void FullHistory()
        {
            NavigateTo("Full History");
        }

        private void QuestLog()
        {
            NavigateTo("Quest Log");
        }

        private void AddQuest()
        {
            NavigateTo("New Quest");
        }

        private void CompleteSelected()
        {
            QuestModel targetQuest;

            try
            {
                targetQuest = (QuestModel)SelectedQuest;
            }
            catch (ArgumentOutOfRangeException) //No quest was selected
            {
                return;
            }

            foreach (QuestModel q in this.UserCharacter.Quests)
            {
                if (q.Status == (int)QuestModel.QuestStatus.ACTIVE)
                {
                    if (q.Id == targetQuest.Id)
                    {
                        q.Status = (int)QuestModel.QuestStatus.COMPLETED;
                        targetQuest = q;
                    }
                }
            }
            // Update contact reputation
            foreach (ContactModel c in this.UserCharacter.CharacterContacts)
            {
                if (c.Id == targetQuest.ContactId)
                {
                    c.Reputation += targetQuest.ReputationValue;
                }
            }

            // Update XP
            int levelChange = this.UserCharacter.AddXPAndGetLevelDifference(targetQuest.XPValue);
            this.CharacterXP += targetQuest.XPValue;


            // Add new event record
            XPEventModel newRecord = new XPEventModel(
                description: "Completed " + targetQuest.Title + ".",
                eventId: targetQuest.Id,
                primarySkill: -1,
                value: targetQuest.XPValue
                );

            this.UserCharacter.EventHistory.Add(newRecord);
            _eventRecords.Add(newRecord);

            // Level up sequence if needed
            if (levelChange > 0)
            {
                AppSettings.NumOfLevelsOnLevelUp = levelChange;
                var result = DialogHost.Show(new LevelUpViewModel(), LevelUpDialogClosingHandler);

            }

            // Refresh quest filter
            CollectionViewSource.GetDefaultView(ActiveQuests).Refresh();
        }

        private void LevelUpDialogClosingHandler(object sender, DialogClosingEventArgs e)
        {
            GenerateStatRows();
        }

        private void OpenMilestoneDialog()
        {
            var result = DialogHost.Show(new MilestoneViewModel(), MilestoneDialogClosingHandler);
        }

        private void MilestoneDialogClosingHandler(object sender, DialogClosingEventArgs e)
        {
            GenerateStatRows();
            _eventRecords.Add(this.UserCharacter.EventHistory[this.UserCharacter.EventHistory.Count - 1]);
        }

        private void SaveCharacterAs()
        {
            // Create OpenFileDialog 
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
            {
                // Set filter for file extension and default file extension 
                DefaultExt = ".xml",
                Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*"

            };

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected/new file name and write file to it
            if (result == true)
            {
                string filename = dlg.FileName;
                DataHandler.SaveToXml(this.UserCharacter, filename);
                AppSettings.UpdateSaveLocation(filename);
            }
        }

        private void SaveCharacter()
        {
            if (AppSettings.SaveDestination == null)
            {
                SaveCharacterAs();
            }
            else
            {
                DataHandler.SaveToXml(this.UserCharacter, AppSettings.SaveDestination);
            }
        }

        private void LoadCharacter()
        {
            CharacterModel loadedChar = new CharacterModel();
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".xml",
                Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*"
            };

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and load a character with it
            if (result == true)
            {
                string filename = dlg.FileName;
                loadedChar = DataHandler.LoadCharacterFromXml(filename);
                AppSettings.SaveLocation = filename;
                this.UserCharacter = loadedChar;
                RefreshView();
            }

        }

        private void ExitProgram()
        {
            Application.Current.Shutdown();
        }

        private void GoToQuest(object sender)
        {
            QuestModel selectedQuest = new QuestModel();
            ListView senderList = (ListView)sender;
            int targetId = -1;
            // Find selected quest
            try // Double clicked "ACTIVE QUESTS" item
            {
                selectedQuest = (QuestModel)senderList.SelectedItems[0];
                targetId = selectedQuest.Id;

            }
            catch (InvalidCastException) // Double Clicked "RECENT EVENTS" item
            {
                EventRecordModel selectedEvent = (EventRecordModel)senderList.SelectedItems[0];
                 targetId = selectedEvent.AssociatedEventId;
            }
            catch (ArgumentOutOfRangeException) // Double clicked on contact's chip; not implemented yet
            {
                return;
            }


            // Update default quest displayed when opening quest log
            AppSettings.UpdateDefaultSelectedQuestId(targetId);
            NavigateTo("Quest Log");
        }

        private void GoToHistory()
        {

        }

        private void RefreshView()
        {
            GenerateStatRows();
            this.CharacterXP = this.UserCharacter.CurrentXP;

            // Refresh Quests
            this.ActiveQuests.Clear();
            this.ActiveQuests = new ObservableCollection<QuestModel>(this.UserCharacter.Quests);
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ActiveQuests);
            view.Filter = QuestFilter;

            // Refresh Event History
            this._eventRecords.Clear();
            this._eventRecords = new ObservableCollection<EventRecordModel>(this.UserCharacter.EventHistory);
            this._eventRecords.CollectionChanged += EventRecords_CollectionChanged;

            // Refresh Quest Filter
            CollectionViewSource.GetDefaultView(ActiveQuests).Refresh();
            OnPropertyChanged("ActiveQuests");
            OnPropertyChanged("EventRecords");
        }

        #endregion
    }
}
