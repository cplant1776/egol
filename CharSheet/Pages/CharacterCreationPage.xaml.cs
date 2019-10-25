using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
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
using CharSheet.classes.display;

namespace CharSheet
{
    /// <summary>
    /// Interaction logic for CharacterCreation.xaml
    /// </summary>
    public partial class CharacterCreation : Page, INotifyPropertyChanged
    {

        private MainWindow _mainWindow;
        private List<AttributeRow> _attributeRows = new List<AttributeRow> { };
        private List<SkillRow> _skillRows = new List<SkillRow> { };
        private string _imgName;

        public MainWindow MainWindow
        {
            get { return _mainWindow; }
            set
            {
                _mainWindow = value;
                OnPropertyChanged("MainWindow");
            }
        }

        public string ImgName
        {
            get { return _imgName; }
            set
            {
                _imgName = value;
                OnPropertyChanged(() => ImgName);
            }
        }

        public List<AttributeRow> AttributeRows
        {
            get { return _attributeRows; }
            set
            {
                _attributeRows = value;
                OnPropertyChanged("AttributeRows");
            }
        }

        public List<SkillRow> SkillRows
        {
            get { return _skillRows; }
            set
            {
                _skillRows = value;
                OnPropertyChanged("SkillRows");
            }
        }


        public CharacterCreation()
        {
            InitializeComponent();
            this.MainWindow = (MainWindow)Application.Current.MainWindow;
            GenerateAttributeRows();
            GenerateSkillRows();
            this.ImgName = AppSettings.ContactImageFullPath + "default.png";

            AttributeList.ItemsSource = this.AttributeRows;
            SkillList.ItemsSource = this.SkillRows;
        }

        private void GenerateAttributeRows()
        {
            foreach(KeyValuePair<int, int> entry in this.MainWindow.CurrentCharacter.AttributeValue)
            {
                AttributeRows.Add(new AttributeRow(name: DataHandler.getAttributeDesc(entry.Key), value: entry.Value));
            }
        }

        private void GenerateSkillRows()
        {
            foreach (KeyValuePair<int, int> entry in this.MainWindow.CurrentCharacter.SkillValue)
            {
                SkillRows.Add(new SkillRow(name: DataHandler.getSkillDesc(entry.Key), value: entry.Value));
            }
        }

        private void PlusAttribute_Click(object sender, RoutedEventArgs e)
        {
            // Find Sending Row
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow row)
                {
                    // Get sent attribute
                    AttributeRow tar = (AttributeRow)row.Item;
                    //Find target attribute in current character
                    foreach (AttributeRow r in this.AttributeRows)
                    {
                        if (r.AttributeName == tar.AttributeName)
                        {
                            r.AttributeValue++; //Update attribute value
                            break;
                        }
                    }
                    break;
                }
        }

        private void MinusAttribute_Click(object sender, RoutedEventArgs e)
        {
            // Find Sending Row
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow row)
                {
                    // Get sent attribute
                    AttributeRow tar = (AttributeRow)row.Item;
                    // Value cant go below zero
                    if (tar.AttributeValue > 0)
                    {
                        //Find target attribute in current character
                        foreach (AttributeRow r in this.AttributeRows)
                        {
                            if (r.AttributeName == tar.AttributeName)
                            {
                                r.AttributeValue--; //Update attribute value
                                break;
                            }
                        }
                    }
                    break;
                }
        }

        private void PlusSkill_Click(object sender, RoutedEventArgs e)
        {
            // Find Sending Row
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow row)
                {
                    // Get sent attribute
                    SkillRow tar = (SkillRow)row.Item;
                    //Find target attribute in current character
                    foreach (SkillRow r in this.SkillRows)
                    {
                        if (r.SkillName == tar.SkillName)
                        {
                            r.SkillValue++; //Update attribute value
                            break;
                        }
                    }
                    break;
                }
        }

        private void MinusSkill_Click(object sender, RoutedEventArgs e)
        {
            // Find Sending Row
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow row)
                {
                    // Get sent attribute
                    SkillRow tar = (SkillRow)row.Item;
                    // Value cant go below zero
                    if (tar.SkillValue > 0)
                    {
                        //Find target attribute in current character
                        foreach (SkillRow r in this.SkillRows)
                        {
                            if (r.SkillName == tar.SkillName)
                            {
                                r.SkillValue--; //Update attribute value
                                break;
                            }
                        }
                    }
                    break;
                }
        }
        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            this.ImgName = this.MainWindow.LoadImage();
            this.MainWindow.CurrentCharacter.ImgName = this.ImgName;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.MainWindow.NavigateTo(AppSettings.pagePaths["Start"], this.NavigationService);
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            // Set Attributes/Skills
            foreach(AttributeRow r in this.AttributeRows)
                this.MainWindow.CurrentCharacter.AttributeValue[DataHandler.getAttributeId(r.AttributeName)] = r.AttributeValue;
            foreach (SkillRow r in this.SkillRows)
                this.MainWindow.CurrentCharacter.SkillValue[DataHandler.getSkillId(r.SkillName)] = r.SkillValue;

            // Set name
            this.MainWindow.CurrentCharacter.Name = CharacterName.Text;
            // Description
            this.MainWindow.CurrentCharacter.Description = CharacterDescription.Text;

            // Go to dashboard
            this.MainWindow.NavigateTo(AppSettings.pagePaths["Dashboard"], this.NavigationService);
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
