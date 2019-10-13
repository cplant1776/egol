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
            InitializeComponent();
            this.CurrentCharacter = new Character();
            AppSettings.InitializeSettings();
        }

        public void Save(string destination)
        {
            CurrentCharacter.dataHandler.SaveToXml(CurrentCharacter, destination);
        }

        public void Load(string origin)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(origin);
            this.CurrentCharacter = (Character)CurrentCharacter.dataHandler.ReadFromXml(doc.OuterXml, typeof(Character));
        }
        
        public void NavigateTo(string pagePath, NavigationService navService)
        {
            Dashboard nextPage = new Dashboard();
            navService.Navigate(new Uri(pagePath, UriKind.Relative));
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
