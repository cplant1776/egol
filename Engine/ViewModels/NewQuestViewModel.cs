using Engine.Utils;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Engine.ViewModels
{
    public class NewQuestViewModel : BaseViewModel, IPageViewModel
    {

        #region Fields
        private ContactModel _selectedContact;
        private QuestModel _selectedQuest;
        private string _selectedQuestTitle;
        private DateTime _selectedQuestDeadline;
        private int _numOfContacts;
        private bool _deadlineSet;

        private ObservableCollection<ContactModel> _contactList;
        private ObservableCollection<QuestModel> _questList;

        private ICommand _done;
        private ICommand _newContactDialog;
        private ICommand _questTitleUpdated;

        #endregion

        #region Constructors
        public NewQuestViewModel()
        {
            this.QuestList = new ObservableCollection<QuestModel>(this.UserCharacter.Quests.Where(x => x.Status == (int)QuestModel.QuestStatus.COMPLETED).ToList());
            CollectionView questView = (CollectionView)CollectionViewSource.GetDefaultView(this.QuestList);
            questView.Filter = QuestFilter;

            this.SelectedContact = new ContactModel();
            this.SelectedQuest = new QuestModel();

            this.ContactList = new ObservableCollection<ContactModel>(this.UserCharacter.CharacterContacts);
            this.SelectableXPValues = AppSettings.XPSelectableValues;
            this.SelectableRepuationValues = Enumerable.Range(0, 100).ToList();

            this.SelectedQuest.XPValue = 1;
            this.SelectedQuest.ReputationValue = 0;

            SelectedQuestDeadline = DateTime.UtcNow.Date;
            _deadlineSet = false;

        }
        #endregion

        #region Filters
        private bool QuestFilter(object item)
        {
            if (String.IsNullOrEmpty(SelectedQuestTitle) || item == null)
                return true;

            if ((item as QuestModel).Title.ToLower().Contains(SelectedQuestTitle.ToLower()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Public Properties/Commands
        public string Name { get { return "NewQuest"; } }
       
        public bool QuestIsActive { get; set; }

        public List<int> SelectableXPValues { get; set; }
        public List<int> SelectableRepuationValues { get; set; }

        public ContactModel SelectedContact
        {
            get
            {
                return _selectedContact; }
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
                try
                {
                    _selectedQuest = value;
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("ERROR: No known quest selected.");
                }
               var matches = QuestList.Where(q => q.Id == _selectedQuest.Id).ToList(); // is selected quest ID in list of quests?
                if (matches.Any())
                {
                    SelectedContact = this.UserCharacter.GetCharacterContact(_selectedQuest.ContactId);
                    OnPropertyChanged("SelectedContact");
                    OnPropertyChanged("SelectedQuest");
                }
            }
        }

        public ObservableCollection<ContactModel> ContactList { get { return _contactList; } set { _contactList = value; } }
        public ObservableCollection<QuestModel> QuestList { get { return _questList; } set { _questList = value; } }
        public string SelectedQuestTitle { get { return _selectedQuestTitle; } set { _selectedQuestTitle = value; OnPropertyChanged("SelectedQuestTitle"); } }
        public Brush DeadlineTextColor
        {
            get
            {
                if (_deadlineSet)
                    return Brushes.White;
                else
                    return Brushes.Transparent;
            }
        }
        public DateTime SelectedQuestDeadline {
            get
            {
                return _selectedQuestDeadline;
            }
            set
            {
                _selectedQuestDeadline = value;
               if(value != new DateTime() && value != DateTime.UtcNow.Date)
                    _deadlineSet = true;
                OnPropertyChanged("SelectedQuestDeadline");
                OnPropertyChanged("DeadlineTextColor");
            }
        }


        public ICommand DoneCommand
        {
            get
            {
                if (_done == null)
                {
                    _done = new RelayCommand(
                        param => Done()
                    );
                }
                return _done;
            }
        }

        public ICommand NewContactCommand
        {
            get
            {
                if (_newContactDialog == null)
                {
                    _newContactDialog = new RelayCommand(
                        param => NewContactDialog()
                    );
                }
                return _newContactDialog;
            }
        }

        public ICommand QuestTitleUpdatedCommand
        {
            get
            {
                if (_questTitleUpdated == null)
                {
                    _questTitleUpdated = new RelayCommand(
                        param => QuestTitleUpdated()
                    );
                }
                return _questTitleUpdated;
            }
        }
        #endregion

        #region Methods
        private void Done()
        {
            bool isMissingFields = CheckForMissingFields();
            if (isMissingFields)
            {

            }
            else
            {
                DateTime deadline = new DateTime();
                if (_deadlineSet)
                {
                    deadline = SelectedQuestDeadline;
                }

                QuestModel newQuest = new QuestModel(
                    title: SelectedQuestTitle,
                    description: SelectedQuest.Description,
                    xpValue: SelectedQuest.XPValue,
                    contactId: SelectedContact.Id,
                    reputationValue: SelectedQuest.ReputationValue,
                    deadline: deadline);

                if (QuestIsActive)
                {
                    newQuest.Status = (int)QuestModel.QuestStatus.ACTIVE;
                }

                // Add quest to character
                this.UserCharacter.Quests.Add(newQuest);

                NavigateTo("Dashboard");
            }

        }

        private bool CheckForMissingFields()
        {
            List<string> missingFields = new List<string> { };
            string errorTitle = "Missing Fields";

            if (String.IsNullOrEmpty(SelectedQuestTitle))
                missingFields.Add("Quest Title");
            if (String.IsNullOrEmpty(SelectedQuest.Description) || SelectedQuest.Description == "\n\n")
                missingFields.Add("Quest Description");
            if (SelectedQuest.XPValue < 0)
                missingFields.Add("XP Value");
            if (SelectedQuest.ReputationValue < 0)
                missingFields.Add("Quest Description");
            if (SelectedContact.Id == -1)
                missingFields.Add("Quest Contact");

            if (missingFields.Any())
            {
                string errorMsg = "Please make sure the following fields are filled: \n\n";
                foreach (string field in missingFields)
                    errorMsg += "* " + field + "\n";

                DialogHost.Show(new ErrorDialogViewModel(msg: errorMsg, title: errorTitle));
                return true;
            }
            else
            {
                return false;
            }
        }
        private void QuestTitleUpdated()
        {
            CollectionViewSource.GetDefaultView(this.QuestList).Refresh();
        }

        private void NewContactDialog()
        {
            _numOfContacts = this.UserCharacter.CharacterContacts.Count();
            var result = DialogHost.Show(new NewContactViewModel(), NewContactDialogClosingHandler);
        }

        private void NewContactDialogClosingHandler(object sender, DialogClosingEventArgs e)
        {
            if(this.UserCharacter.CharacterContacts.Count() > _numOfContacts) // New contact was added
            {
                // Add most recently added contact (one just created by user)
                this.SelectedContact = this.UserCharacter.CharacterContacts[this.UserCharacter.CharacterContacts.Count - 1];
                _numOfContacts = this.UserCharacter.CharacterContacts.Count();
            }

        }
        #endregion
    }
}
