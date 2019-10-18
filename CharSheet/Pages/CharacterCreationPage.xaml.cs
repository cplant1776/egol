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
using CharSheet.classes;

namespace CharSheet
{
    /// <summary>
    /// Interaction logic for CharacterCreation.xaml
    /// </summary>
    public partial class CharacterCreation : Page, INotifyPropertyChanged
    {

        private MainWindow _mainWindow;
        private Dictionary<int, int> AttributeValue;
        private Dictionary<int, int> SkillValue;
        private string _imgName;

        public MainWindow MainWindow
        {
            get { return _mainWindow; }
            set
            {
                _mainWindow = value;
                OnPropertyChanged("MainWindow");
            }
        }

        public string ImgName
        {
            get { return _imgName; }
            set
            {
                _imgName = value;
                OnPropertyChanged(() => ImgName);
            }
        }

        public CharacterCreation()
        {
            InitializeComponent();
            this.MainWindow = (MainWindow)Application.Current.MainWindow;
            this.AttributeValue = this.MainWindow.CurrentCharacter.AttributeValue;
            this.SkillValue = this.MainWindow.CurrentCharacter.SkillValue;
            this.ImgName = AppSettings.ContactImageFullPath + "default.png";

            AttributeControl.ItemsSource = MainWindow.CurrentCharacter.AttributeValue;
            SkillControl.ItemsSource = MainWindow.CurrentCharacter.SkillValue;
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            Grid sendingGrid = (sender as Button).Parent as Grid;
            TextBlock elementValue = sendingGrid.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).FirstOrDefault();
            int iElementValue = Convert.ToInt32(elementValue.Text);
            TextBlock elementName = sendingGrid.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 1).FirstOrDefault();

            if (sendingGrid.Name == "AttributeValueGrid") //Attribute button clicked
            {
                int attributeId = DataHandler.getAttributeId(elementName.Text);
                AttributeValue[attributeId]++;
            }
            else if (sendingGrid.Name == "SkillValueGrid") //Skill button clicked
            {
                int skillId = DataHandler.getSkillId(elementName.Text);
                SkillValue[skillId]++;
            }

            // Increment displayed value
            // TODO: look into implementing ObservableCollection w/ dictionary functionality
            elementValue.Text = (Convert.ToInt32(elementValue.Text) + 1).ToString();

        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            Grid sendingGrid = (sender as Button).Parent as Grid;
            TextBlock elementValue = sendingGrid.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).FirstOrDefault();
            int iElementValue = Convert.ToInt32(elementValue.Text);
            TextBlock elementName = sendingGrid.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 1).FirstOrDefault();

            // Decrement displayed value (cant go below 0)
            if (iElementValue > 0)
            {
                if (sendingGrid.Name == "AttributeValueGrid") //Attribute button clicked
                {
                    int attributeId = DataHandler.getAttributeId(elementName.Text);
                    AttributeValue[attributeId]--;
                }
                else if (sendingGrid.Name == "SkillValueGrid") //Skill button clicked
                {
                    int skillId = DataHandler.getSkillId(elementName.Text);
                    SkillValue[skillId]--;
                }

                // Udate displayed text
                elementValue.Text = (iElementValue - 1).ToString();
            }
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            this.ImgName = this.MainWindow.LoadImage();
            this.MainWindow.CurrentCharacter.ImgName = this.ImgName;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.MainWindow.NavigateTo(AppSettings.pagePaths["Start"], this.NavigationService);
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            // Set Attributes/Skill
            this.MainWindow.CurrentCharacter.AttributeValue = this.AttributeValue;
            this.MainWindow.CurrentCharacter.SkillValue = this.SkillValue;

            // Set name
            this.MainWindow.CurrentCharacter.Name = CharacterName.Text;
            // Description
            this.MainWindow.CurrentCharacter.Description = CharacterDescription.Text;

            // Go to dashboard
            this.MainWindow.NavigateTo(AppSettings.pagePaths["Dashboard"], this.NavigationService);
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
