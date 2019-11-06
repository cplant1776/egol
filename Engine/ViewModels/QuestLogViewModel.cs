using Engine.Utils;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Engine.ViewModels
{
    public class QuestLogViewModel : BaseViewModel, IPageViewModel, INotifyPropertyChanged
    {
        #region Fields
        private List<QuestModel> _quests = new List<QuestModel> { };
        private QuestModel _selectedQuest;
        private ContactModel _selectedContact;
        private bool _selectedQuestStateActive;
        private bool _selectedQuestStateAccepted;
        private bool _selectedQuestStateComplete;

        private ICommand _updateSelectedQuest;
        private ICommand _updateQuestState;
        private ICommand _setQuestStateActive;
        private ICommand _setQuestStateAccepted;
        private ICommand _setQuestStateCompleted;
        private ICommand _questStatusChangeCancel;
        #endregion

        #region Constructors
        public QuestLogViewModel()
        {
            this._quests = this.UserCharacter.Quests;
            if (this.UserCharacter.Quests.Any()) // If there are any quests in the log
            {
                FindDefaultSelectedQuest();
            }
        }
        #endregion

        #region Public properties/commands

        public ContactModel SelectedContact
        {
            get
            {
                return _selectedContact;
            }
            set
            {
                _selectedContact = value;
                OnPropertyChanged("SelectedContact");
            }
        }

        public QuestModel SelectedQuest
        {
            get
            {
                return _selectedQuest;
            }
            set
            {
                _selectedQuest = value;
                OnPropertyChanged("SelectedQuest");
                OnPropertyChanged("SelectedContact");
            }
        }

        public ObservableCollection<QuestModel> ActiveQuestsItems
        {
            get
            {
                ObservableCollection<QuestModel> activeQuests = new ObservableCollection<QuestModel>();
                foreach (QuestModel q in _quests)
                {
                    if (q.Status == (int)QuestModel.QuestStatus.ACTIVE)
                    {
                        activeQuests.Add(q);
                    }
                }
                return activeQuests;
            }
        }

        public ObservableCollection<QuestModel> AcceptedQuestsItems
        {
            get
            {
                ObservableCollection<QuestModel> acceptedQuests = new ObservableCollection<QuestModel>();
                foreach (QuestModel q in _quests)
                {
                    if (q.Status == (int)QuestModel.QuestStatus.ACCEPTED)
                    {
                        acceptedQuests.Add(q);
                    }
                }
                return acceptedQuests;
            }
        }

        public ObservableCollection<QuestModel> CompletedQuestsItems
        {
            get
            {
                ObservableCollection<QuestModel> completedQuests = new ObservableCollection<QuestModel>();
                foreach (QuestModel q in _quests)
                {
                    if (q.Status == (int)QuestModel.QuestStatus.COMPLETED)
                    {
                        completedQuests.Add(q);
                    }
                }
                return completedQuests;
            }
        }

        public System.Windows.Visibility DueDateShowing
        {
            get
            {
                if (_selectedQuest.Deadline != DateTime.MinValue)
                    return System.Windows.Visibility.Visible;
                else
                    return System.Windows.Visibility.Hidden;
            }
        }

        public bool SelectedQuestStateActive { get { return _selectedQuestStateActive; } set { _selectedQuestStateActive = value; OnPropertyChanged("SelectedQuestStateActive"); } }
        public bool SelectedQuestStateAccepted { get { return _selectedQuestStateAccepted; } set { _selectedQuestStateAccepted = value; OnPropertyChanged("SelectedQuestStateAccepted"); } }
        public bool SelectedQuestStateComplete { get { return _selectedQuestStateComplete; } set { _selectedQuestStateComplete = value; OnPropertyChanged("SelectedQuestStateComplete"); } }

        public ICommand UpdateSelectedQuestCommand
        {
            get
            {
                if (_updateSelectedQuest == null)
                {
                    _updateSelectedQuest = new RelayCommand(
                        param => UpdateSelectedQuest(param)
                    );
                }
                return _updateSelectedQuest;
            }
        }

        public ICommand UpdateQuestStateCommand
        {
            get
            {
                if (_updateQuestState == null)
                {
                    _updateQuestState = new RelayCommand(
                        param => UpdateQuestState()
                    );
                }
                return _updateQuestState;
            }
        }

        public ICommand SetQuestStateActiveCommand
        {
            get
            {
                if (_setQuestStateActive == null)
                {
                    _setQuestStateActive = new RelayCommand(
                        param => SetQuestStateActive()
                    );
                }
                return _setQuestStateActive;
            }
        }

        public ICommand SetQuestStateAcceptedCommand
        {
            get
            {
                if (_setQuestStateAccepted == null)
                {
                    _setQuestStateAccepted = new RelayCommand(
                        param => SetQuestStateAccepted()
                    );
                }
                return _setQuestStateAccepted;
            }
        }

        public ICommand SetQuestStateCompletedCommand
        {
            get
            {
                if (_setQuestStateCompleted == null)
                {
                    _setQuestStateCompleted = new RelayCommand(
                        param => SetQuestStateCompleted()
                    );
                }
                return _setQuestStateCompleted;
            }
        }

        public ICommand ChangeStateChangeCancelCommand
        {
            get
            {
                if (_questStatusChangeCancel == null)
                {
                    _questStatusChangeCancel = new RelayCommand(
                        param => ChangeStateChangeCancel()
                    );
                }
                return _questStatusChangeCancel;
            }
        }

        public string Name
        {
            get { return "QuestLog"; }
        }
        #endregion

        #region Methods
        public void FindDefaultSelectedQuest()
        {
            bool questWasFound = false;
            this.SelectedQuest = new QuestModel();
            this.SelectedContact = new ContactModel();
            // Set default selected quest & contact
            if (AppSettings.DefaultSelectedQuestId == null)
            {
                this.SelectedQuest = this.UserCharacter.Quests[0];
                this.SelectedContact = this.UserCharacter.GetContact(this.SelectedQuest.ContactId);
            }
            else
            {
                foreach (QuestModel q in this.UserCharacter.Quests)
                {
                    if (q.Id == AppSettings.DefaultSelectedQuestId)
                    {
                        this.SelectedQuest = q;
                        this.SelectedContact = this.UserCharacter.GetContact(q.ContactId);
                        questWasFound = true;
                        break;
                    }
                    if(!questWasFound)
                    {
                        this.SelectedQuest = this.UserCharacter.Quests[0];
                        this.SelectedContact = this.UserCharacter.GetContact(this.SelectedQuest.ContactId);
                    }
                }
            }

            UpdateQuestState();
        }

        public void UpdateSelectedQuest(object sender)
        {
            try
            {
                TreeViewItem sendingItem = (TreeViewItem)sender;
                TreeView sendingTree = (TreeView)sendingItem.Parent;
                // Find clicked quest's name
                QuestModel targetQuest = (QuestModel)sendingTree.SelectedItem;
                int targetQuestId = targetQuest.Id;

                // Set selected quest & contact
                this.SelectedQuest = this.UserCharacter.GetQuest(targetId: targetQuestId);
                this.SelectedContact = this.UserCharacter.GetContact(targetId: this.SelectedQuest.ContactId);
                this.UpdateQuestState();
            }
            catch (InvalidCastException) // Quest category clicked
            {
                // Expand quest category
                (sender as TreeViewItem).IsExpanded = true;
            }
        }


        public void UpdateQuestState()
        {
            if (this.SelectedQuest.Status == (int)QuestModel.QuestStatus.ACTIVE)
            {
                SelectedQuestStateActive = true;
                SelectedQuestStateAccepted = false;
                SelectedQuestStateComplete = false;
            }
            else if (this.SelectedQuest.Status == (int)QuestModel.QuestStatus.ACCEPTED)
            {
                SelectedQuestStateActive = false;
                SelectedQuestStateAccepted = true;
                SelectedQuestStateComplete = false;
            }
            else if (this.SelectedQuest.Status == (int)QuestModel.QuestStatus.COMPLETED)
            {
                SelectedQuestStateActive = false;
                SelectedQuestStateAccepted = false;
                SelectedQuestStateComplete = true;
            }
        }

        private void SetQuestStateActive()
        {
            this.SelectedQuest.Status = (int)QuestModel.QuestStatus.ACTIVE;
            UpdateTree();
            CloseDialog();

        }

        private void SetQuestStateAccepted()
        {
            this.SelectedQuest.Status = (int)QuestModel.QuestStatus.ACCEPTED;
            UpdateTree();
            CloseDialog();

        }

        private void SetQuestStateCompleted()
        {
            this.SelectedQuest.Status = (int)QuestModel.QuestStatus.COMPLETED;
            UpdateTree();
            CloseDialog();
            CompleteQuest();

        }

        private void UpdateTree()
        {
            // Update display properties
            OnPropertyChanged("AcceptedQuestsItems");
            OnPropertyChanged("ActiveQuestsItems");
            OnPropertyChanged("CompletedQuestsItems");
        }

        private void CloseDialog()
        {
            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CompleteQuest()
        {
            // Update contact reputation
            foreach (ContactModel c in this.UserCharacter.CharacterContacts)
            {
                if (c.Id == this.SelectedQuest.ContactId)
                {
                    c.Reputation += this.SelectedQuest.ReputationValue;
                }
            }

            // Update XP
            int levelChange = this.UserCharacter.AddXPAndGetLevelDifference(this.SelectedQuest.XPValue);


            // Add new event record
            XPEventModel newRecord = new XPEventModel(
                description: "Completed " + this.SelectedQuest.Title + ".",
                eventId: this.SelectedQuest.Id,
                primarySkill: -1,
                value: this.SelectedQuest.XPValue
                );

            this.UserCharacter.EventHistory.Add(newRecord);

            // Check for level up
            if(levelChange > 0)
            {
                AppSettings.NumOfLevelsOnLevelUp = levelChange;
                var result = DialogHost.Show(new LevelUpViewModel());

            }
        }

        private void ChangeStateChangeCancel()
        {
            UpdateQuestState();
            // Close dialog host
            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void RefreshQuestStatus()
        {

        }

        #endregion
    }
}
