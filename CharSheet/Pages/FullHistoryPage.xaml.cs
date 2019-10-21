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
        public MainWindow MainWindow = (MainWindow)Application.Current.MainWindow;

        public FullHistoryPage()
        {
            InitializeComponent();

            GenerateExpPlot();
            // Show n most recent entries entries
            HistoryControl.ItemsSource = this.MainWindow.CurrentCharacter.EventHistory;
        }
        
        private void GenerateExpPlot()
        {
            MyPlotter myPlotter = new MyPlotter();
            myPlotter.PlotXPHistory(MainWindow.CurrentCharacter.EventHistory);
            ExpPlot.Model = myPlotter.MyModel;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateTo(AppSettings.pagePaths["Dashboard"], NavigationService.GetNavigationService(this));
        }
    }
}
