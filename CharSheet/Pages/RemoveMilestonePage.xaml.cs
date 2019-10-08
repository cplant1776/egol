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
                new Milestone(1, "milestone1"),
                new Milestone(2, "milestone2"),
                new Milestone(3, "milestone3"),
                new Milestone(4, "milestone4"),
                new Milestone(5, "milestone5")
            };

            foreach(Milestone ms in milestones)
            {
                MilestoneList.Items.Add(ms.Description);
            }

        }
    }


}
