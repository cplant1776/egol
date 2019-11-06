using Engine.Utils;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Engine.ViewModels
{
    public class NewQuestViewModel : BaseViewModel, IPageViewModel
    {

        #region Fields
        private ContactModel _selectedContact;
        private QuestModel _selectedQuest;
        private ContactModel _newContact;
        private ObservableCollection<ContactModel> _contactList;
        private ObservableCollection<QuestModel> _questList;
        private ICommand _done;
        private ICommand _newContactDialog;

        #endregion

        #region Constructors
        public NewQuestViewModel()
        {
            this.ContactList = new ObservableCollection<ContactModel>(this.UserCharacter.CharacterContacts);
            this.QuestList = new ObservableCollection<QuestModel>(this.UserCharacter.Quests);
            this.SelectableXPValues = AppSettings.XPSelectableValues;
            this.SelectableRepuationValues = Enumerable.Range(0, 100).ToList();
            this.SelectedContact = new ContactModel();
            this.SelectedQuest = new QuestModel();
            this.NewContact = new ContactModel();
            _selectedContact = new ContactModel();

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
                _selectedQuest = value;
               var matches = QuestList.Where(q => q.Id == _selectedQuest.Id).ToList(); // is selected quest ID in list of quests?
                if (matches.Any())
                {
                    SelectedContact = this.UserCharacter.GetCharacterContact(_selectedQuest.ContactId);
                    OnPropertyChanged("SelectedContact");
                    OnPropertyChanged("SelectedQuest");
                }
            }
        }

        public ContactModel NewContact
        {
            get
            {
                return _newContact;
            }
            set
            {
                _newContact = value;
                OnPropertyChanged("NewContact");
            }
        }

        public ObservableCollection<ContactModel> ContactList { get { return _contactList; } set { _contactList = value; } }
        public ObservableCollection<QuestModel> QuestList { get { return _questList; } set { _questList = value; } }


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


        #endregion

        #region Methods
        private void Done()
        {
            QuestModel newQuest = new QuestModel(
                title: SelectedQuest.Title,
                description: SelectedQuest.Description,
                xpValue: SelectedQuest.XPValue,
                contactId: SelectedContact.Id,
                reputationValue: SelectedQuest.ReputationValue,
                deadline: SelectedQuest.Deadline);

            if (QuestIsActive)
            {
                newQuest.Status = (int)QuestModel.QuestStatus.ACTIVE;
            }

            // Add quest to character
            this.UserCharacter.Quests.Add(newQuest);

            NavigateTo("Dashboard");
        }

        private void NewContactDialog()
        {
            var result = DialogHost.Show(new NewContactViewModel(), NewContactDialogClosingHandler);
        }

        private void NewContactDialogClosingHandler(object sender, DialogClosingEventArgs e)
        {
            // Add most recently added contact (one just created by user)
            this.SelectedContact = this.UserCharacter.CharacterContacts[this.UserCharacter.CharacterContacts.Count -1];
        }
        #endregion
    }
}
