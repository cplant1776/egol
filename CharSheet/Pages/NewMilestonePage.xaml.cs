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
    /// Interaction logic for NewMilestonePage.xaml
    /// </summary>
    public partial class NewMilestonePage : Page
    {
        public NewMilestonePage()
        {
            InitializeComponent();
            GenerateAttributeList();
        }

        private void GenerateAttributeList()
        {
            List<string> attributeNames = new List<string> { };
            foreach (string val in AppSettings.Attributes.Values)
            {
                attributeNames.Add(val);
            }
            AttributeInput.ItemsSource = attributeNames;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var dialogWindow = Window.GetWindow(this);
            dialogWindow.DialogResult = false;
            dialogWindow.Close();
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            var dialogWindow = Application.Current.Windows.OfType<DialogWindow>().SingleOrDefault(w => w.IsActive);
            dialogWindow.DialogResult = true;
            // Generate Id
            Random rnd = new Random();
            int newId = rnd.Next(1, 600000); // generate random id
            dialogWindow.result = new Milestone(
                                                description: this.DescriptionInput.Text,
                                                eventId: newId,
                                                attributeId: DataHandler.getAttributeId(AttributeInput.Text),
                                                value: Convert.ToInt32(ValueInput.Text)
                                             );
            dialogWindow.Close();
        }
    }
}
