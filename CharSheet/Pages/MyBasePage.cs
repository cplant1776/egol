using CharSheet.classes;
using CharSheet.classes.data;
using CharSheet.classes.display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Xml;

namespace CharSheet.Pages
{
    /*
     * Contains methods that are common across all pages
     */
    public class MyBasePage : Page
    {
        private QuickMenuActions _quickMenuActions = new QuickMenuActions();

        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        public QuickMenuActions QuickMenuActions
        {
            get { return _quickMenuActions; }
            set { _quickMenuActions = value; }
        }

        public MyBasePage()
        {

        }

        protected void NavigateToPage(object sender, RoutedEventArgs e)
        {
            string pageName = (sender as Button).Tag.ToString();
            string pagePath = AppSettings.pagePaths[pageName];
            NavigationService.GetNavigationService(this).Navigate(new Uri(pagePath, UriKind.Relative));
        }

        protected void NavigateToPage(string pageName)
        {
            string pagePath = AppSettings.pagePaths[pageName];
            NavigationService.GetNavigationService(this).Navigate(new Uri(pagePath, UriKind.Relative));
        }

        public void LoadCharacter(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                // Set filter for file extension and default file extension 
                DefaultExt = ".xml",
                Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*"

            };

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Read selected XML file and set Character object from it
            if (result == true)
            {
                string filename = dlg.FileName; 
                XmlDocument doc = new XmlDocument(); 
                doc.Load(filename); 
                this.mainWindow.CurrentCharacter = (Character)DataHandler.ReadFromXml(doc.OuterXml, typeof(Character));
                AppSettings.UpdateSaveLocation(filename); 
            }

            // Navigate to dashboard
            NavigateToPage("Dashboard");
            RefreshPage();
        }

        protected void RefreshPage()
        {
            NavigationService.GetNavigationService(this).Refresh();
        }

        protected void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected void SaveCharacter(object sender, RoutedEventArgs e)
        {
            Character c = mainWindow.CurrentCharacter;
            if (AppSettings.SaveDestination == null)
            {
                SaveCharacterAs(c);
            }
            else
            {
                DataHandler.SaveToXml(c, AppSettings.SaveDestination);
            }
        }

        protected void SaveCharacterAs(object sender, RoutedEventArgs e)
        {
            Character c = mainWindow.CurrentCharacter;
            SaveCharacterAs(c);
        }

        protected void SaveCharacterAs(Character c)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
            {
                // Set filter for file extension and default file extension 
                DefaultExt = ".xml",
                Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*"

            };

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected/new file name and write file to it
            if (result == true)
            {
                string filename = dlg.FileName;
                DataHandler.SaveToXml(c, filename);
                AppSettings.UpdateSaveLocation(filename);
            }
        }
    }

}
