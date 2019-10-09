using System;
using System.Collections.Generic;
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
using System.Xml;
using CharSheet.objects;
using CharSheet.Pages;

namespace CharSheet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Character currentCharacter { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.currentCharacter = new Character();
            AppSettings.InitializeSettings();
        }

        public void Save(string destination)
        {
            currentCharacter.dataHandler.SaveToXml(currentCharacter, destination);
        }

        public void Load(string origin)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(origin);
            this.currentCharacter = (Character)currentCharacter.dataHandler.ReadFromXml(doc.OuterXml, typeof(Character));
        }
        
        public void NavigateTo(string pagePath, NavigationService navService)
        {
            Dashboard nextPage = new Dashboard();
            navService.Navigate(new Uri(pagePath, UriKind.Relative));
        }
    }




}
