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

namespace CharSheet.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page, INotifyPropertyChanged
    {
        public MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        private int _currentXP;
        private int _levelProgress;
        private int _currentLevel;

        public int CurrentXP
        {
            get { return _currentXP; }
            set
            {
                _currentXP = value;
                LevelProgress = _currentXP % 100;
                CurrentLevel = _currentXP / 100;
                OnPropertyChanged("LevelProgress");
                OnPropertyChanged("CurrentLevel");
                OnPropertyChanged("CurrentXP");
            }
        }
        public int LevelProgress {
            get { return _levelProgress; }
            set
            {
                _levelProgress = value;
                OnPropertyChanged("LevelProgress");
                OnPropertyChanged("CurrentLevel");
                OnPropertyChanged("CurrentXP");
            }
        }
        public int CurrentLevel {
            get { return _currentLevel; }
            set{ _currentLevel = value;
                OnPropertyChanged("LevelProgress");
                OnPropertyChanged("CurrentLevel");
                OnPropertyChanged("CurrentXP");
            }
        }

        public Dashboard()
        {
            InitializeComponent();

            this.CurrentXP = mainWindow.currentCharacter.currentXP;
            Console.WriteLine(this.CurrentLevel);

            GenerateAttributeRows();
            GenerateSkillRows();
            GenerateExpPlot();
            GenerateHistory();
            GenerateSkillDropdown();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void GenerateExpPlot()
        {
            MyPlotter myPlotter = new MyPlotter();
            myPlotter.PlotXPHistory(mainWindow.currentCharacter.eventHistory);
            ExpPlot.Model = myPlotter.MyModel;
        }

        private void GenerateHistory()
        {
            foreach(HistoryEntry e in mainWindow.currentCharacter.eventHistory)
            {
                HistoryStack.Children.Add(e.GenerateHistoryEntryTextBlock());
            }
        }

        private void GenerateAttributeRows()
        {
            foreach (KeyValuePair<int, int> entry in mainWindow.currentCharacter.attributeValue)
            {
                // Create row from current character's values
                var newRow = new AttributeRow(DataHandler.getAttributeDesc(entry.Key), entry.Value);
                AttributeStack.Children.Add(newRow.GenerateAttributeDisplayRow());
            };
        }

        private void GenerateSkillRows()
        {
            // Iterate through skill dictionary
            foreach (KeyValuePair<int, int> entry in mainWindow.currentCharacter.skillValue)
            {
                // Create row from current character's values
                var newRow = new SkillRow(DataHandler.getSkillDesc(entry.Key), entry.Value);
                SkillStack.Children.Add(newRow.GenerateSkillDisplayRow());
            };
        }

        public void GenerateSkillDropdown()
        {
            List<String> skills = new List<String> { };
            foreach (int skill in mainWindow.currentCharacter.skillValue.Keys)
            {
                // Create row from current character's values
                var skillDesc = DataHandler.getSkillDesc(skill);
                skills.Add(skillDesc);
            };

            EntrySkill.ItemsSource = skills;
        }

        private void NewEntry_Click(object sender, RoutedEventArgs e)
        {
            // Check for level up
            int previousLevel = this.CurrentLevel;
            this.CurrentXP += Convert.ToInt32(EntryXPValue.Text);
            if (previousLevel != this.CurrentLevel)
            {
                LevelUpSequence(previousLevel);
            }

            // Update character history
            mainWindow.currentCharacter.Add(new HistoryEntry
                    (
                        description : EntryDescription.Text,
                        isMilestone : false,
                        value : Convert.ToInt32(EntryXPValue.Text),
                        primarySkill : EntrySkill.SelectedIndex
                    )
                );
            // Update character XP/Level/Progress
            mainWindow.currentCharacter.currentXP = this.CurrentXP;

            RefreshPage();
        }

        private void AddMilestone_Click(object sender, RoutedEventArgs e)
        {
            DialogWindow popup = new DialogWindow();
            if(popup.ShowDialog() == true)
            {
                HistoryEntry newEntry = popup.result;
                mainWindow.currentCharacter.Add(newEntry);
            }
            RefreshPage();
        }

        private void LevelUpSequence(int previousLevel)
        {
            // Open level up popup
            LevelUpWindow popup = new LevelUpWindow(this.CurrentLevel - previousLevel);
            // If true, popup returns a stack panel of grids containing the updated attribute info
            if (popup.ShowDialog() == true)
            {
                // Iterate through attribute rows
                foreach (Grid attributeRow in popup.attributeStack.Children)
                {
                    // Get attribute name & value
                    string attributeName = attributeRow.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 1).First().Text;
                    int attributeValue = Convert.ToInt32(attributeRow.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).First().Text);

                    // Update atrtibute value on current character
                    int attributeId = DataHandler.getAttributeId(attributeName);
                    mainWindow.currentCharacter.attributeValue[attributeId] = attributeValue;
                }

                // Iterate through skill rows
                foreach (Grid skillRow in popup.skillStack.Children)
                {
                    // Get attribute name & value
                    string skillName = skillRow.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 1).First().Text;
                    int skillValue = Convert.ToInt32(skillRow.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).First().Text);

                    // Update atrtibute value on current character
                    int skillId = DataHandler.getSkillId(skillName);
                     mainWindow.currentCharacter.skillValue[skillId] = skillValue;
                }
            }
        }

        private void RefreshPage()
        {
            this.NavigationService.Refresh();
        }
    }
}
