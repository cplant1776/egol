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

        private List<AttributeRow> _attributeRows = new List<AttributeRow> { };
        private List<SkillRow> _skillRows = new List<SkillRow> { };

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

        public List<AttributeRow> AttributeRows
        {
            get { return _attributeRows; }
            set
            {
                _attributeRows = value;
                OnPropertyChanged("AttributeRows");
            }
        }

        public List<SkillRow> SkillRows
        {
            get { return _skillRows; }
            set
            {
                _skillRows = value;
                OnPropertyChanged("SkillRows");
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

            LevelUpWindow levelUpWindow = Application.Current.Windows.OfType<LevelUpWindow>().SingleOrDefault(w => w.IsActive);
            this.NumOfLevels = levelUpWindow.numOfLevels;

            GenerateAttributeRows(levelUpWindow);
            GenerateSkillRows(levelUpWindow);

            AttributeControl.ItemsSource = this.AttributeRows;
            SkillControl.ItemsSource = this.SkillRows;
            
        }

        private void GenerateAttributeRows(LevelUpWindow w)
        {
            foreach (KeyValuePair<int, int> entry in w.AttributeValue)
            {
                AttributeRows.Add(new AttributeRow(name: DataHandler.getAttributeDesc(entry.Key), value: entry.Value));
            }
        }

        private void GenerateSkillRows(LevelUpWindow w)
        {
            foreach (KeyValuePair<int, int> entry in w.SkillValue)
            {
                SkillRows.Add(new SkillRow(name: DataHandler.getSkillDesc(entry.Key), value: entry.Value));
            }
        }

        private void PlusAttribute_Click(object sender, RoutedEventArgs e)
        {
            // Check if max points has already been distributed 
            if (this.DistributedAttributePoints < this.NumOfLevels * AppSettings.AttributePointsPerLevel)
            {

                // Find Sending Row
                for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                    if (vis is DataGridRow row)
                    {
                        // Get sent attribute
                        AttributeRow tar = (AttributeRow)row.Item;
                        //Find target attribute in current character
                        foreach (AttributeRow r in this.AttributeRows)
                        {
                            if (r.AttributeName == tar.AttributeName)
                            {
                                r.AttributeValue++; //Update attribute value
                                this.DistributedAttributePoints++;
                                break;
                            }
                        }
                        break;
                    }
            }
        }

        private void MinusAttribute_Click(object sender, RoutedEventArgs e)
        {
            // Find Sending Row
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow row)
                {
                    // Get sent attribute
                    AttributeRow tar = (AttributeRow)row.Item;
                    // Value cant go below zero or starting value
                    if (tar.AttributeValue > 0 && tar.AttributeValue > tar.StartingValue)
                    {
                        //Find target attribute in current character
                        foreach (AttributeRow r in this.AttributeRows)
                        {
                            if (r.AttributeName == tar.AttributeName)
                            {
                                r.AttributeValue--; //Update attribute value
                                this.DistributedAttributePoints--;
                                break;
                            }
                        }
                    }
                    break;
                }
        }

        private void PlusSkill_Click(object sender, RoutedEventArgs e)
        {
            if (this.DistributedSkillPoints < this.NumOfLevels * AppSettings.SkillPointsPerLevel)
            {
                // Find Sending Row
                for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                    if (vis is DataGridRow row)
                    {
                        // Get sent attribute
                        SkillRow tar = (SkillRow)row.Item;
                        //Find target attribute in current character
                        foreach (SkillRow r in this.SkillRows)
                        {
                            if (r.SkillName == tar.SkillName)
                            {
                                r.SkillValue++; //Update attribute value
                                this.DistributedSkillPoints++;
                                break;
                            }
                        }
                        break;
                    }
            }
        }

        private void MinusSkill_Click(object sender, RoutedEventArgs e)
        {
            // Find Sending Row
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow row)
                {
                    // Get sent attribute
                    SkillRow tar = (SkillRow)row.Item;
                    // Value cant go below zero or starting value
                    if (tar.SkillValue > 0 && tar.SkillValue > tar.StartingValue)
                    {
                        //Find target attribute in current character
                        foreach (SkillRow r in this.SkillRows)
                        {
                            if (r.SkillName == tar.SkillName)
                            {
                                r.SkillValue--; //Update attribute value
                                this.DistributedSkillPoints--;
                                break;
                            }
                        }
                    }
                    break;
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
            // Set Attributes/Skills
            foreach (AttributeRow r in this.AttributeRows)
                levelUpWindow.AttributeValue[DataHandler.getAttributeId(r.AttributeName)] = r.AttributeValue;
            foreach (SkillRow r in this.SkillRows)
                levelUpWindow.SkillValue[DataHandler.getSkillId(r.SkillName)] = r.SkillValue;
            levelUpWindow.DialogResult = true;
            levelUpWindow.Close();
        }
    }
}
