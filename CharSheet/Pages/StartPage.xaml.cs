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
using CharSheet.classes;
using CharSheet.classes.data;

namespace CharSheet.Pages
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : MyBasePage
    {

        public MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public StartPage()
        {

        }

        public void NewCharacter_Click(object sender, EventArgs e)
        {
            // Navigate to Dashboard
            mainWindow.NavigateTo(AppSettings.pagePaths["CharacterCreation"], NavigationService.GetNavigationService(this));
        }

        private void LoadCharacter_Click(object sender, RoutedEventArgs e)
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

            // Get the selected file name and load a character with it
            if (result == true)
            {
                string filename = dlg.FileName;
                mainWindow.Load(filename); // Set main window's character to loaded character
            }
            // Navigate to dashboard
            mainWindow.NavigateTo(AppSettings.pagePaths["Dashboard"], NavigationService.GetNavigationService(this));
        }


        public void DebugCharacter_Click(object sender, RoutedEventArgs e)
        {
            Character debugChar = new Character();
            debugChar = debugChar.CharacterDummy(); // Load in-memory dummy data
            mainWindow.CurrentCharacter = debugChar; // Set main window's character to dummy char
            mainWindow.NavigateTo(AppSettings.pagePaths["Dashboard"], NavigationService.GetNavigationService(this));
        }
    }
}
