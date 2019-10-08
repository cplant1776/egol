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
using CharSheet.objects;

namespace CharSheet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Character currentCharacter = new Character();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Save(string destination)
        {
            currentCharacter.dataHandler.SaveToXml(currentCharacter, destination);
        }
    }


}
