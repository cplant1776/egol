using Engine.Models;
using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Engine.ViewModels
{
    public class DashboardViewModel : BaseViewModel, IPageViewModel, INotifyPropertyChanged
    {
        #region Fields
        private ObservableCollection<StatRow> _attributeRows = new ObservableCollection<StatRow> { };
        private List<StatRow> _skillRows = new List<StatRow> { };
        private List<QuestModel> _quests = new List<QuestModel> { };
        private ObservableCollection<EventRecordModel> _eventRecords = new ObservableCollection<EventRecordModel> { };
        private int _characterXP;
        private string _imgName;
        private string _characterName;

        private string _milestoneDescription;
        private string _milestoneStat;
        private string _milestoneValue;
        private List<string> _milestoneStatList = new List<string> { };

        private ICommand _fullHistory;
        private ICommand _completeSelected;
        private ICommand _addQuest;
        private ICommand _questLog;

        private ICommand _saveCharacterAs;
        private ICommand _loadCharacter;
        private ICommand _exitProgram;
        private ICommand _goToHistoryEvent;
        private ICommand _goToQuest;
        private ICommand _milestoneDialogDone;

        #endregion

        #region Constructors
        public DashboardViewModel()
        {
            GenerateStatRows();
            GenerateMilestoneStats();
            this.ImgName = this.UserCharacter.ImgName;
            this.CharacterName = this.UserCharacter.Name;
            this.CharacterXP = this.UserCharacter.CurrentXP;
            this.MilestoneValue = "1";
            this.MilestoneStat = "Attribute 4";

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

        public ObservableCollection<StatRow> AttributeRows { get {  return _attributeRows; } }

        public List<StatRow> SkillRows { get { return _skillRows; } set { _skillRows = value; } }

        public string CharacterName { get { return _characterName; } set { _characterName = value; } }
        public string ImgName { get { return _imgName; } set { _imgName = value; OnPropertyChanged("ImgName"); } }
        public int CharacterXP { get { return _characterXP; } set { _characterXP = value; OnPropertyChanged("CharacterXP"); OnPropertyChanged("CharacterLevel"); OnPropertyChanged("LevelProgress"); } }
        public int CharacterLevel { get { return _characterXP / 100; } set { OnPropertyChanged("CharacterXP"); } }
        public int LevelProgress { get { return _characterXP % 100; }  set { OnPropertyChanged("CharacterXP"); }}
        public ObservableCollection<QuestModel> ActiveQuests { get; set; }

        public string MilestoneDescription { get { return _milestoneDescription; } set { _milestoneDescription = value; OnPropertyChanged("MilestoneDescription"); } }
        public string MilestoneStat { get { return _milestoneStat; } set { _milestoneStat = value; OnPropertyChanged("MilestoneStat"); } }
        public string MilestoneValue { get { return _milestoneValue; } set { _milestoneValue = value; OnPropertyChanged("MilestoneValue"); } }
        public List<string> MilestoneStatList { get { return _milestoneStatList; } set { _milestoneStatList = value; OnPropertyChanged("MilestoneStatList"); } }

        public QuestModel SelectedQuest { get; set; }

        public List<QuestModel> Quests
        {
            get { return _quests; }
            set
            {
                _quests = value;
                OnPropertyChanged("Quests");
            }
        }

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
                        param => GoToQuest()
                    );
                }
                return _goToQuest;
            }
        }

        public ICommand MilestoneDialogDoneCommand
        {
            get
            {
                if (_milestoneDialogDone == null)
                {
                    _milestoneDialogDone = new RelayCommand(
                        param => MilestoneDialogDone()
                    );
                }
                return _milestoneDialogDone;
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

        private void GenerateMilestoneStats()
        {
            foreach (string val in AppSettings.Attributes.Values)
                this.MilestoneStatList.Add(val);
        }

        private void FullHistory()
        {
            NavigateTo("FullHistory");
        }

        private void QuestLog()
        {
            NavigateTo("QuestLog");
        }

        private void AddQuest()
        {
            NavigateTo("NewQuest");
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
            this.UserCharacter.CurrentXP += targetQuest.XPValue;
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

            // Refresh quest filter
            CollectionViewSource.GetDefaultView(ActiveQuests).Refresh();
        }

        private void SaveCharacterAs()
        {

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
            }

            this.UserCharacter = loadedChar;
        }

        private void ExitProgram()
        {
            Application.Current.Shutdown();
        }

        private void GoToHistory()
        {
            //TODO
        }

        private void GoToQuest()
        {
            //TODO
        }

        private void MilestoneDialogDone()
        {
            // generate random id
            Random rnd = new Random();
            int newId = rnd.Next(1, 600000); 

            // Get attribute id and added value
            int attributeId = DataHandler.getAttributeId(MilestoneStat);
            int attributeValue = Convert.ToInt32(MilestoneValue);

            MilestoneModel newMilestone = new MilestoneModel(
                                                description: MilestoneDescription,
                                                eventId: newId,
                                                attributeId: attributeId,
                                                value: attributeValue
                                             );

            // Add record of milestone
            this.UserCharacter.EventHistory.Add(newMilestone);
            _eventRecords.Add(newMilestone);

            // Update attribute
            this.UserCharacter.AttributeValue[attributeId] += attributeValue;
            this.UpdateAttributeRowValue(attributeId, attributeValue);

            // Close dialog host
            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void UpdateAttributeRowValue(int attributeId, int attributeValue)
        {
            foreach(StatRow r in this._attributeRows)
            {
                if(DataHandler.getAttributeId(r.StatName) == attributeId)
                {
                    r.StatValue += attributeValue;
                }
            }
        }

        #endregion
    }
}
