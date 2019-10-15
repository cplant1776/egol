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
using System.Xml;
using CharSheet.classes.data;
using CharSheet.classes;
using CharSheet.Pages;

namespace CharSheet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private Character _currentCharacter;
        public Character CurrentCharacter
        {
            get { return _currentCharacter; }
            set
            {
                _currentCharacter = value;
                OnPropertyChanged(() => CurrentCharacter);
            }
        }

        public MainWindow()
        {
            AppSettings.InitializeSettings();
            InitializeComponent();
            this.CurrentCharacter = new Character();
        }

        public void Save(string destination)
        {
            DataHandler.SaveToXml(CurrentCharacter, destination);
        }

        public void Load(string origin)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(origin);
            this.CurrentCharacter = (Character)DataHandler.ReadFromXml(doc.OuterXml, typeof(Character));
        }
        
        public void NavigateTo(string pagePath, NavigationService navService)
        {
            Page nextPage = new Page();
            navService.Navigate(new Uri(pagePath, UriKind.Relative));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void UpdateXP(int xpValue)
        {
            int previousLevel = this.CurrentCharacter.CurrentXP / 100;
            this.CurrentCharacter.CurrentXP += xpValue;
            int currentLevel = this.CurrentCharacter.CurrentXP / 100;
            if (previousLevel < currentLevel)
                LevelUpSequence(previousLevel);
        }

        private void LevelUpSequence(int previousLevel)
        {
            // Open level up popup
            LevelUpWindow popup = new LevelUpWindow((this.CurrentCharacter.CurrentXP / 100) - previousLevel);
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
                    this.CurrentCharacter.AttributeValue[attributeId] = attributeValue;
                }

                // Iterate through skill rows
                foreach (Grid skillRow in popup.skillStack.Children)
                {
                    // Get attribute name & value
                    string skillName = skillRow.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 1).First().Text;
                    int skillValue = Convert.ToInt32(skillRow.Children.OfType<TextBlock>().Where(i => Grid.GetColumn(i) == 2).First().Text);

                    // Update atrtibute value on current character
                    int skillId = DataHandler.getSkillId(skillName);
                    this.CurrentCharacter.SkillValue[skillId] = skillValue;
                }
            }
        }

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
