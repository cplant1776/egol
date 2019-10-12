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
using static CharSheet.classes.AttributeRow;
using static CharSheet.classes.SkillRow;

namespace CharSheet
{
    /// <summary>
    /// Interaction logic for LevelUpPage.xaml
    /// </summary>
    /// 

       
    public partial class LevelUpPage : Page, INotifyPropertyChanged
    {

        public MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        private int _numOfLevels;

        public int NumOfLevels
        {
            get { return _numOfLevels; }
            set
            {
                _numOfLevels = value;
                DisplayNumOfLevels = "Add " + (value * AppSettings.AttributePointsPerLevel).ToString() 
                    +" attribute points and " + (value * AppSettings.SkillPointsPerLevel).ToString()
                    + " skill points.";
                OnPropertyChanged("DisplayNumOfLevels");
            }
        }

        public string DisplayNumOfLevels { get; set; }
        public int DistributedAttributePoints = 0;
        public int DistributedSkillPoints = 0;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public LevelUpPage()
        {
            InitializeComponent();

            var levelUpWindow = Application.Current.Windows.OfType<LevelUpWindow>().SingleOrDefault(w => w.IsActive);
            this.NumOfLevels = levelUpWindow.numOfLevels;

            GenerateAttributeRows();
            GenerateSkillRows();
            SetButtonEvents();
            
        }

        private void GenerateAttributeRows()
        {
            foreach (KeyValuePair<int, int> entry in mainWindow.currentCharacter.attributeValue)
            {
                // Create row from current character's values
                var newRow = new AttributeRow(DataHandler.getAttributeDesc(entry.Key), entry.Value);
                AttributeStack.Children.Add(newRow.GenerateAttributeRow());
            };
        }

        private void GenerateSkillRows()
        {
            foreach (KeyValuePair<int, int> entry in mainWindow.currentCharacter.skillValue)
            {
                // Create row from current character's values
                var newRow = new SkillRow(DataHandler.getSkillDesc(entry.Key), entry.Value);
                SkillStack.Children.Add(newRow.GenerateSkillRow());
            };
        }

        private void SetButtonEvents()
        {
            BindAttributeButtons();
            BindSkillButtons();
        }

        private void BindAttributeButtons()
        {
            // Iterate through each row
            foreach (Grid g in AttributeStack.Children)
            {

                // Find plus and minus buttons
                AttributeModifierButton plusBtn = g.Children.OfType<AttributeModifierButton>().Where(i => Grid.GetColumn(i) == 3).FirstOrDefault();
                AttributeModifierButton minusBtn = g.Children.OfType<AttributeModifierButton>().Where(i => Grid.GetColumn(i) == 4).FirstOrDefault();

                // Set starting values
                int attributeValue = Convert.ToInt32(g.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).FirstOrDefault().Text);
                plusBtn.StartingValue = attributeValue;
                minusBtn.StartingValue = attributeValue;

                // Bind their click event
                plusBtn.Click += Plus_Click;
                minusBtn.Click += Minus_Click;
            }
        }

        private void BindSkillButtons()
        {
            // Iterate through each row
            foreach (Grid g in SkillStack.Children)
            {

                // Find plus and minus buttons
                SkillModifierButton plusBtn = g.Children.OfType<SkillModifierButton>().Where(i => Grid.GetColumn(i) == 3).FirstOrDefault();
                SkillModifierButton minusBtn = g.Children.OfType<SkillModifierButton>().Where(i => Grid.GetColumn(i) == 4).FirstOrDefault();

                // Set starting values
                int skillValue = Convert.ToInt32(g.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).FirstOrDefault().Text);
                plusBtn.StartingValue = skillValue;
                minusBtn.StartingValue = skillValue;

                // Bind their click event
                plusBtn.Click += Plus_Click;
                minusBtn.Click += Minus_Click;
            }
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            Grid sendingGrid = (sender as Button).Parent as Grid;
            TextBlock elementValue = sendingGrid.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).FirstOrDefault();

            // Attribute button pressed
            if (sender.GetType() == typeof(AttributeModifierButton))
            {
                if (this.DistributedAttributePoints < this.NumOfLevels * AppSettings.AttributePointsPerLevel)
                {
                    elementValue.Text = (Convert.ToInt32(elementValue.Text) + 1).ToString();
                    this.DistributedAttributePoints++;
                }
            }

            // Skill button pressed
            if (sender.GetType() == typeof(SkillModifierButton))
            {
                if (this.DistributedSkillPoints < this.NumOfLevels * AppSettings.SkillPointsPerLevel)
                {
                    elementValue.Text = (Convert.ToInt32(elementValue.Text) + 1).ToString();
                    this.DistributedSkillPoints++;
                }
            }

        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            Grid sendingGrid = (sender as Button).Parent as Grid;
            TextBlock elementValue = sendingGrid.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).FirstOrDefault();
            int iElementValue = Convert.ToInt32(elementValue.Text);

            // Attribute button pressed 
            if (sender.GetType() == typeof(AttributeModifierButton))
            {
                if (iElementValue > (sender as AttributeModifierButton).StartingValue)
                {
                    elementValue.Text = (iElementValue - 1).ToString();
                    this.DistributedAttributePoints--;
                }
            }

            // Skill button pressed
            if (sender.GetType() == typeof(SkillModifierButton))
            {
                if (iElementValue > (sender as SkillModifierButton).StartingValue)
                {
                    elementValue.Text = (iElementValue - 1).ToString();
                    this.DistributedSkillPoints--;
                }
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var levelUpWindow = Window.GetWindow(this);
            levelUpWindow.DialogResult = false;
            levelUpWindow.Close();
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            var levelUpWindow = Application.Current.Windows.OfType<LevelUpWindow>().SingleOrDefault(w => w.IsActive);
            levelUpWindow.attributeStack = AttributeStack;
            levelUpWindow.skillStack = SkillStack;
            levelUpWindow.DialogResult = true;
            levelUpWindow.Close();
        }
    }
}
