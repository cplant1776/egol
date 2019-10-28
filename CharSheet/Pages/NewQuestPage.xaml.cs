using CharSheet.classes;
using CharSheet.classes.data;
using CharSheet.window;
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

namespace CharSheet.Pages
{
    /// <summary>
    /// Interaction logic for NewQuestPage.xaml
    /// </summary>
    public partial class NewQuestPage : Page, INotifyPropertyChanged
    {

        private Contact _currentContact = new Contact();

        private List<string> _contactList = new List<string> { };

        private List<Quest> _questList = new List<Quest> { };

        public MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;


        public Contact CurrentContact
        {
            get { return _currentContact; }
            set
            {
                _currentContact = value;
                OnPropertyChanged(() => CurrentContact);
            }
        }

        public List<string> ContactList
        {
            get { return _contactList; }
            set
            {
                _contactList = value;
                OnPropertyChanged(() => ContactList);
            }
        }

        public List<Quest> QuestList
        {
            get { return _questList; }
            set
            {
                _questList = value;
                OnPropertyChanged(() => QuestList);
            }
        }

        public NewQuestPage()
        {
            InitializeComponent();
            // Populate reputation combobox
            List<int> reputationRange = Enumerable.Range(0, 100).ToList();
            ReputationValue.ItemsSource = reputationRange;
            // Populate XP combobox
            XPValue.ItemsSource = AppSettings.XPSelectableValues;

            // Populate character contacts
            this.ContactList = mainWindow.CurrentCharacter.CharacterContacts.Select(o => o.Name).ToList();
            ContactInput.ItemsSource = this.ContactList;
            // Populate past quest titles
            foreach(Quest q in this.mainWindow.CurrentCharacter.Quests)
            {
                this.QuestList.Add(q);
            }
            TitleInput.ItemsSource = this.QuestList;

            // Initialize Contact so default image shows up
            this.CurrentContact = new Contact();
        }

        private void NewContact_Click(object sender, RoutedEventArgs e)
        {
            ContactWindow popup = new ContactWindow();
            if (popup.ShowDialog() == true)
            {
                // Set new contact for this quest
                this.CurrentContact = popup.result;
                this.mainWindow.CurrentCharacter.CharacterContacts.Add(popup.result);
                ContactInput.Text = popup.result.Name;
            }
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            // Check if contact already exists in contact list
            List<Contact> idOverlap = this.mainWindow.CurrentCharacter.CharacterContacts.Where(i => i.Id == this.CurrentContact.Id).ToList();
            if (!idOverlap.Any())
            {
                // Add quest contact to character's contact list
                Random rnd = new Random();
                this.CurrentContact.Id = rnd.Next(1, 60000); // generate random id
                mainWindow.CurrentCharacter.CharacterContacts.Add(this.CurrentContact);
            }

            // Create new quest
            Quest newQuest = new Quest(
                title: TitleInput.Text,
                description: DescriptionInput.Text,
                xpValue: Convert.ToInt32(XPValue.Text),
                contactId: this.CurrentContact.Id,
                reputationValue: Convert.ToInt32(ReputationValue.Text),
                deadline: (DateTime)DeadlineInput.SelectedDate  //TODO: Set from input
                );

            // Set status to active if checked
            if (IsActiveToggle.IsChecked ?? true)
            {
                newQuest.Status = (int)Quest.QuestStatus.ACTIVE;
            }

            // Add quest to character's quest list
            mainWindow.CurrentCharacter.Quests.Add(newQuest);

            // Return to dashboard
            mainWindow.NavigateTo(AppSettings.pagePaths["Dashboard"], this.NavigationService);
            
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
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

        private void TitleInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Quest targetQuest = (sender as ComboBox).SelectedItem as Quest;
            if(targetQuest != null)
            {
                // Locate quest object associated with title
                foreach (Quest q in mainWindow.CurrentCharacter.Quests)
                {
                    if (targetQuest.Id == q.Id)
                    {
                        // Set values to target quest values
                        this.CurrentContact = mainWindow.CurrentCharacter.GetCharacterContact(q.ContactId);
                        XPValue.Text = q.XPValue.ToString();
                        ReputationValue.Text = q.ReputationValue.ToString();
                        ContactInput.Text = this.CurrentContact.Name;
                    }
                }
            }

        }

        private void ContactInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string targetName = (sender as ComboBox).SelectedItem as string;
            Contact targetContact = this.mainWindow.CurrentCharacter.CharacterContacts.Find(i => i.Name == targetName);
            this.CurrentContact = targetContact;
        }
    }
}
