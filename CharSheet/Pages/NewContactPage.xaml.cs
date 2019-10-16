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
using System.ComponentModel;
using System.Linq.Expressions;
using CharSheet.classes;

namespace CharSheet.Pages
{
    /// <summary>
    /// Interaction logic for NewContact.xaml
    /// </summary>
    public partial class NewContact : Page, INotifyPropertyChanged
    {

        public MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        private string _imgName;

        public string ImgName
        {
            get { return _imgName; }
            set
            {
                _imgName = value;
                OnPropertyChanged(() => ImgName);
            }
        }

        public NewContact()
        {
            InitializeComponent();
            this.ImgName = AppSettings.ContactImageFullPath + "default.png";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var contactWindow = Window.GetWindow(this);
            contactWindow.DialogResult = false;
            contactWindow.Close();
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            this.ImgName = mainWindow.LoadImage();
        }

        private void AddContact_Click(object sender, RoutedEventArgs e)
        {
            var contactWindow = Application.Current.Windows.OfType<ContactWindow>().SingleOrDefault(w => w.IsActive);
            contactWindow.DialogResult = true;

            contactWindow.result = new Contact(
                name : ContactName.Text,
                description : ContactDescription.Text,
                reputation : (int)ContactReputation.Value,
                imgName: this.ImgName
                );

            contactWindow.Close();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        // Just so we can call it via lambda which is nicer
        public void OnPropertyChanged<T>(Expression<Func<T>> propertyNameExpression)
        {
            OnPropertyChanged(((MemberExpression)propertyNameExpression.Body).Member.Name);
        }
    }
}
