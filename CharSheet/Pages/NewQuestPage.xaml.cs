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

        public NewQuestPage()
        {
            InitializeComponent();
            List<int> reputationRange = Enumerable.Range(-100, 100).ToList();
            QuestReputationValue.ItemsSource = reputationRange;
        }

        private void NewContact_Click(object sender, RoutedEventArgs e)
        {
            ContactWindow popup = new ContactWindow();
            if (popup.ShowDialog() == true)
            {
                // Set new contact for this quest
                this.CurrentContact = popup.result;
            }
        }

        private void ExistingContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            // Create new quest
            Quest newQuest = new Quest(
                title: QuestTitle.Text,
                description: QuestDescription.Text,
                xpValue: Convert.ToInt32(QuestXPValue.Text),
                contactId: this.CurrentContact.Id,
                reputationValue: Convert.ToInt32(QuestReputationValue.Text),
                deadline: DateTime.UtcNow
                );

            // Add quest to character's quest list
            mainWindow.CurrentCharacter.Quests.Add(newQuest);
            // Add quest contact to character's contact list
            Random rnd = new Random();
            this.CurrentContact.Id = rnd.Next(1, 60000); // generate random id
            mainWindow.CurrentCharacter.CharacterContacts.Add(this.CurrentContact);

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
    }
}
