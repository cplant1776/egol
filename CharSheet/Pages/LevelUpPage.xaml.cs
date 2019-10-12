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
                DisplayNumOfLevels = "Add " + value.ToString() + " attribute points.";
                OnPropertyChanged("DisplayNumOfLevels");
            }
        }

        public string DisplayNumOfLevels { get; set; }
        public int DistributedPoints = 0;

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

        private void SetButtonEvents()
        {
            // Iterate through each row
            foreach(Grid g in AttributeStack.Children)
            {

                // Find plus and minus buttons
                AttributeModifierButton plusBtn = g.Children.OfType<AttributeModifierButton>().Where(i => Grid.GetColumn(i) == 3).First();
                AttributeModifierButton minusBtn = g.Children.OfType<AttributeModifierButton>().Where(i => Grid.GetColumn(i) == 4).First();

                // Set starting values
                int attributeValue = Convert.ToInt32(g.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).First().Text);
                plusBtn.StartingValue = attributeValue;
                minusBtn.StartingValue = attributeValue;

                // Bind their click event
                plusBtn.Click += Plus_Click;
                minusBtn.Click += Minus_Click;
            }
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            Grid sendingGrid = (sender as Button).Parent as Grid;
            TextBlock attributeValue = sendingGrid.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).First();
            if (this.DistributedPoints < this.NumOfLevels)
            {
                attributeValue.Text = (Convert.ToInt32(attributeValue.Text) + 1).ToString();
                this.DistributedPoints++;
            }
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            Grid sendingGrid = (sender as AttributeModifierButton).Parent as Grid;
            TextBlock attributeValue = sendingGrid.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).First();

            int iAttributeValue = Convert.ToInt32(attributeValue.Text);
            if (iAttributeValue > (sender as AttributeModifierButton).StartingValue)
            {
                attributeValue.Text = (iAttributeValue - 1).ToString();
                this.DistributedPoints--;
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
            levelUpWindow.result = AttributeStack;
            levelUpWindow.DialogResult = true;
            levelUpWindow.Close();

            // Iterate through attribute rows
            foreach (Grid attributeRow in AttributeStack.Children)
            {
                // Get attribute name & value
                string attributeName = attributeRow.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 1).First().Text;
                int attributeValue = Convert.ToInt32(attributeRow.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).First().Text);

                // Update atrtibute value on current character
                int attributeId = DataHandler.getAttributeId(attributeName);
                mainWindow.currentCharacter.attributeValue[attributeId] = attributeValue;
            }
        }
    }
}
