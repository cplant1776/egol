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
    /// Interaction logic for RemoveMilestonePage.xaml
    /// </summary>
    public partial class RemoveMilestonePage : Page
    {
        public RemoveMilestonePage()
        {
            InitializeComponent();

            GenerateMilestoneList();
        }

        private void GenerateMilestoneList()
        {
            List<Milestone> milestones = new List<Milestone>
            {
                new Milestone("milestone1", -1, 0),
                new Milestone("milestone2", -1, 0),
                new Milestone("milestone3", -1, 0),
                new Milestone("milestone4", -1, 0),
                new Milestone("milestone5", -1, 0)
            };

            foreach(Milestone ms in milestones)
            {
                MilestoneList.Items.Add(ms.Description);
            }

        }
    }


}
