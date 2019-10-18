using CharSheet.classes;
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

namespace CharSheet.Pages
{
    /// <summary>
    /// Interaction logic for CompleteHistoryPage.xaml
    /// </summary>
    public partial class CompleteHistoryPage : Page
    {
        public MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public CompleteHistoryPage()
        {
            InitializeComponent();

            GenerateExpPlot();
            //GenerateHistory();
        }

        private void GenerateExpPlot()
        {
            MyPlotter myPlotter = new MyPlotter();
            myPlotter.PlotXPHistory(mainWindow.CurrentCharacter.EventHistory);
            //ExpPlot.Model = myPlotter.MyModel;
        }

        private void GenerateHistory()
        {
            foreach (EventRecord e in mainWindow.CurrentCharacter.EventHistory)
            {
                //FullHistoryStack.Children.Add(e.GenerateEventRecordTextBlock());
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Click!");
            mainWindow.NavigateTo(AppSettings.pagePaths["Dashboard"], NavigationService.GetNavigationService(this));
        }
    }
}
