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
using CharSheet.classes.display;

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
        private Dictionary<int, int> AttributeValue;
        private Dictionary<int, int> SkillValue;

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

            this.AttributeValue = levelUpWindow.AttributeValue;
            this.SkillValue = levelUpWindow.SkillValue;

            AttributeControl.ItemsSource = this.AttributeValue;
            SkillControl.ItemsSource = this.SkillValue;
            
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            Grid sendingGrid = (sender as Button).Parent as Grid;
            TextBlock elementValue = sendingGrid.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).FirstOrDefault();
            int iElementValue = Convert.ToInt32(elementValue.Text);
            TextBlock elementName = sendingGrid.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 1).FirstOrDefault();


            // Attribute button pressed
            if (sendingGrid.Name == "AttributeValueGrid")
            {
                if (this.DistributedAttributePoints < this.NumOfLevels * AppSettings.AttributePointsPerLevel)
                {
                    // Update attribute value
                    int attributeId = DataHandler.getAttributeId(elementName.Text);
                    this.AttributeValue[attributeId]++;
                    this.DistributedAttributePoints++;

                    //Update displayed value
                    elementValue.Text = (Convert.ToInt32(elementValue.Text) + 1).ToString();
                }
            }

            // Skill button pressed
            if (sendingGrid.Name == "SkillValueGrid")
            {
                if (this.DistributedSkillPoints < this.NumOfLevels * AppSettings.SkillPointsPerLevel)
                {
                    // Update skill value
                    int skillId = DataHandler.getSkillId(elementName.Text);
                    this.SkillValue[skillId]++;
                    this.DistributedSkillPoints++;

                    //Update displayed value
                    elementValue.Text = (Convert.ToInt32(elementValue.Text) + 1).ToString();
                }
            }

        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            Grid sendingGrid = (sender as Button).Parent as Grid;
            TextBlock elementValue = sendingGrid.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).FirstOrDefault();
            int iElementValue = Convert.ToInt32(elementValue.Text);
            TextBlock elementName = sendingGrid.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 1).FirstOrDefault();

            // Attribute button pressed 
            if (sendingGrid.Name == "AttributeValueGrid")
            {
                var sendingButton = sendingGrid.Children.OfType<Button>().Where(i => Grid.GetColumn(i) == 4).FirstOrDefault();

                if (iElementValue > (sendingButton as AttributeModifierButton).StartingValue)
                {
                    // Update attribute value
                    int attributeId = DataHandler.getAttributeId(elementName.Text);
                    this.AttributeValue[attributeId]--;
                    this.DistributedAttributePoints--;

                    // Update displayed value
                    elementValue.Text = (iElementValue - 1).ToString();
                }
            }

            // Skill button pressed
            if (sendingGrid.Name == "SkillValueGrid")
            {
                var sendingButton = sendingGrid.Children.OfType<Button>().Where(i => Grid.GetColumn(i) == 4).FirstOrDefault();

                if (iElementValue > (sendingButton as SkillModifierButton).StartingValue)
                {
                    // Update skill value
                    int skillId = DataHandler.getSkillId(elementName.Text);
                    this.SkillValue[skillId]--;
                    this.DistributedSkillPoints--;

                    // Update displayed value
                    elementValue.Text = (iElementValue - 1).ToString();
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
            levelUpWindow.AttributeValue = this.AttributeValue;
            levelUpWindow.SkillValue = this.SkillValue;
            levelUpWindow.DialogResult = true;
            levelUpWindow.Close();
        }
    }
}
