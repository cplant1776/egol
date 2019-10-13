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
    /// Interaction logic for MilestoneValuePage.xaml
    /// </summary>
    public partial class MilestoneValuePage : Page
    {

        private String description;
        public MilestoneValuePage()
        {
            InitializeComponent();
        }

        public MilestoneValuePage(String description)
        {
            InitializeComponent();
            this.description = description;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var dialogWindow = Window.GetWindow(this);
            dialogWindow.DialogResult = false;
            dialogWindow.Close();
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            var dialogWindow = Application.Current.Windows.OfType<DialogWindow>().SingleOrDefault(w => w.IsActive);
            dialogWindow.DialogResult = true;
            dialogWindow.result = new HistoryEntry(
                                                    description : this.description,
                                                    isMilestone : true,
                                                    value : Convert.ToInt32(SelectedValue.Text),
                                                    primarySkill : DataHandler.attributeIdDict[SelectedAttribute.Text]
                                                    );
            dialogWindow.Close();
        }
    }
}
