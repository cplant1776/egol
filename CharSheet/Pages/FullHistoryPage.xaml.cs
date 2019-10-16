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

namespace CharSheet.Pages
{
    /// <summary>
    /// Interaction logic for FullHistoryPage.xaml
    /// </summary>
    public partial class FullHistoryPage : Page
    {
        public MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public FullHistoryPage()
        {
            InitializeComponent();

            GenerateExpPlot();
            GenerateHistory();
        }

        private void GenerateExpPlot()
        {
            MyPlotter myPlotter = new MyPlotter();
            myPlotter.PlotXPHistory(mainWindow.CurrentCharacter.EventHistory);
            ExpPlot.Model = myPlotter.MyModel;
        }

        private void GenerateHistory()
        {
            foreach (EventRecord e in mainWindow.CurrentCharacter.EventHistory)
            {
                FullHistoryStack.Children.Add(e.GenerateEventRecordTextBlock());
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.NavigateTo(AppSettings.pagePaths["Dashboard"], NavigationService.GetNavigationService(this));
        }
    }
}
