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
    /// Interaction logic for Contact.xaml
    /// </summary>
    public partial class ContactPage : Page
    {
        public MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        public ContactPage()
        {
            InitializeComponent();
        }

        public ContactPage(Contact contact)
        {
            InitializeComponent();

            // Populate window content
            ContactName.Text = contact.Name;
            ContactDescription.Text = contact.Description;

            ContactImage.Source = new System.Windows.Media.Imaging.BitmapImage(
                new Uri(contact.ImgPath, UriKind.RelativeOrAbsolute));

            ContactReputationBar.Value = contact.Reputation;
            ContactReputationText.Text = contact.Reputation.ToString() + " / 100";
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            //Close popup
        }
    }
}
