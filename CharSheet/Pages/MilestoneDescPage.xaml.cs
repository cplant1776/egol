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
    /// Interaction logic for SetValuePrompt.xaml
    /// </summary>
    public partial class MilestoneDescPage : Page
    {
        public MilestoneDescPage()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var dialogWindow = Window.GetWindow(this);
            dialogWindow.DialogResult = false;
            dialogWindow.Close();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            MilestoneValuePage newPage = new MilestoneValuePage(EntryDescription.Text);
            this.NavigationService.Navigate(newPage);
        }
    }
}
