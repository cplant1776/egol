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
            LevelUpWindow popup = new LevelUpWindow(
                numOfLevels: (this.CurrentCharacter.CurrentXP / 100) - previousLevel,
                attributeValues: this.CurrentCharacter.AttributeValue,
                skillValues: this.CurrentCharacter.SkillValue
                );

            // Update attributes/skills
            if (popup.ShowDialog() == true)
            {
                this.CurrentCharacter.AttributeValue = popup.AttributeValue;
                this.CurrentCharacter.SkillValue = popup.SkillValue;
            }
        }

        public String LoadImage()
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                // Set filter for file extension and default file extension 
                DefaultExt = ".png",
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };

            // Get the selected file name and load a character with it
            if (dlg.ShowDialog() == true)
            {
                string source = dlg.FileName;
                string destination = AppSettings.ContactImageFullPath + System.IO.Path.GetFileName(source);
                // Copy file to resources
                System.IO.File.Copy(source, destination, true);
                return destination;
            }
            return "";
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
