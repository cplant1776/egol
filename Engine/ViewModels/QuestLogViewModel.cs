using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private ICommand _updateQuestState;
        private ICommand _goBack;
        private ICommand _setQuestStateActive;
        private ICommand _setQuestStateAccepted;
        private ICommand _setQuestStateCompleted;
        private ICommand _questStatusChangeCancel;
        #endregion

        #region Constructors
        public QuestLogViewModel()
        {
            if(this.UserCharacter.Quests.Any())
            {
                this.SelectedQuest = this.UserCharacter.Quests[0];
                this.SelectedContact = this.UserCharacter.CharacterContacts[0];
                this._quests = this.UserCharacter.Quests;
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
                OnPropertyChanged("ContactName");
                OnPropertyChanged("ContactImage");
                OnPropertyChanged("ContactReputation");
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
                OnPropertyChanged("QuestTitle");
                OnPropertyChanged("QuestXP");
                OnPropertyChanged("QuestReputation");
                OnPropertyChanged("QuestDeadline");
                OnPropertyChanged("QuestDescription");
                OnPropertyChanged("ActiveQuestsItems");
                OnPropertyChanged("AcceptedQuestsItems");
                OnPropertyChanged("CompletedQuestsItems");
            }
        }

        public ObservableCollection<QuestModel> ActiveQuestsItems
        {
            get
            {
                ObservableCollection<QuestModel> activeQuests = new ObservableCollection<QuestModel>();
                foreach(QuestModel q in _quests)
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

        /* Contact */
        public string ContactName { get { return _selectedContact.Name; } }
        public string ContactImage { get { return _selectedContact.ImgName; } }
        public int ContactReputation { get { return _selectedContact.Reputation; } }

        /* Quest */
        public string QuestTitle { get { return _selectedQuest.Title; } }
        public int QuestXP { get { return _selectedQuest.XPValue; } }
        public int QuestReputation { get { return _selectedQuest.ReputationValue; } }
        public DateTime QuestDeadline { get { return _selectedQuest.Deadline; } }
        public string QuestDescription { get { return _selectedQuest.Description; } }

        public bool SelectedQuestStateActive { get { return _selectedQuestStateActive; } set { _selectedQuestStateActive = value; OnPropertyChanged("SelectedQuestStateActive"); } }
        public bool SelectedQuestStateAccepted { get { return _selectedQuestStateAccepted; } set { _selectedQuestStateAccepted = value; OnPropertyChanged("SelectedQuestStateAccepted"); } }
        public bool SelectedQuestStateComplete { get { return _selectedQuestStateComplete; } set { _selectedQuestStateComplete = value; OnPropertyChanged("SelectedQuestStateComplete"); } }

        public ICommand SelectQuestCommand
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

        public ICommand GoBackCommand
        {
            get
            {
                if (_goBack == null)
                {
                    _goBack = new RelayCommand(
                        param => GoBack()
                    );
                }
                return _goBack;
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
            // Close dialog host
            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);

        }

        private void SetQuestStateAccepted()
        {
            this.SelectedQuest.Status = (int)QuestModel.QuestStatus.ACCEPTED;
            // Close dialog host
            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);

        }

        private void SetQuestStateCompleted()
        {
            this.SelectedQuest.Status = (int)QuestModel.QuestStatus.COMPLETED;
            // Close dialog host
            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);

            CompleteQuest();

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
                // LEVEL UP SEQUENCE
            }
        }

        private void ChangeStateChangeCancel()
        {
            UpdateQuestState();
            // Close dialog host
            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void GoBack()
        {
            NavigateTo("Dashboard");
        }

        #endregion
    }
}
