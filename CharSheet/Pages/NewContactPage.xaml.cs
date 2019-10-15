using CharSheet.classes.data;
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
using CharSheet.window;

namespace CharSheet.Pages
{
    /// <summary>
    /// Interaction logic for NewContact.xaml
    /// </summary>
    public partial class NewContact : Page
    {
        public NewContact()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var contactWindow = Window.GetWindow(this);
            contactWindow.DialogResult = false;
            contactWindow.Close();
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddContact_Click(object sender, RoutedEventArgs e)
        {
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().SingleOrDefault(w => w.IsActive);
            contactWindow.DialogResult = true;

            contactWindow.result = new Contact(
                name : ContactName.Text,
                description : ContactDescription.Text,
                reputation : (int)ContactReputation.Value,
                imgName: ""
                );

            contactWindow.Close();
        }
    }
}
