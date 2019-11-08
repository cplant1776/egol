using Engine.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Engine.ViewModels
{
    public class MilestoneViewModel: BaseViewModel, IPageViewModel, INotifyPropertyChanged
    {

        #region Fields
        private string _milestoneDescription;
        private string _milestoneStat;
        private int _milestoneValue;
        private List<string> _milestoneStatList = new List<string> { };

        private ICommand _done;
        #endregion

        #region Constructors
        public MilestoneViewModel()
        {
            GenerateMilestoneStats();
            this.MilestoneStat = this.MilestoneStatList[0];
            this.MilestoneValue = 1;
        }
        #endregion

        #region Public Properties/Commands
        public string Name { get { return "Milestone"; } }

        public string MilestoneDescription { get { return _milestoneDescription; } set { _milestoneDescription = value; OnPropertyChanged("MilestoneDescription"); OnPropertyChanged("DoneButtonIsEnabled"); } }
        public string MilestoneStat { get { return _milestoneStat; } set { _milestoneStat = value; OnPropertyChanged("MilestoneStat"); } }
        public int MilestoneValue { get { return _milestoneValue; } set { _milestoneValue = value; OnPropertyChanged("MilestoneValue"); } }
        public List<string> MilestoneStatList { get { return _milestoneStatList; } set { _milestoneStatList = value; OnPropertyChanged("MilestoneStatList"); } }

        public bool DoneButtonIsEnabled
        {
            get
            {
                if (!String.IsNullOrEmpty(_milestoneDescription))
                    return true;
                else
                    return false;
            }
        }


        public ICommand DoneCommand
        {
            get
            {
                if (_done == null)
                {
                    _done = new RelayCommand(
                        param => Done()
                    );
                }
                return _done;
            }
        }
        #endregion

        #region Methods
        private void GenerateMilestoneStats()
        {
            foreach (string val in AppSettings.Attributes.Values)
                this.MilestoneStatList.Add(val);
        }

        private void Done()
        {
            // generate random id
            Random rnd = new Random();
            int newId = rnd.Next(1, 600000);

            // Get attribute id and added value
            int attributeId = DataHandler.getAttributeId(MilestoneStat);
            int attributeValue = MilestoneValue;

            MilestoneModel newMilestone = new MilestoneModel(
                                                description: MilestoneDescription,
                                                eventId: newId,
                                                attributeId: attributeId,
                                                value: attributeValue
                                             );

            // Add record of milestone
            this.UserCharacter.EventHistory.Add(newMilestone);

            // Update attribute
            this.UserCharacter.AttributeValue[attributeId] += attributeValue;

            // Close dialog host
            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        }
        #endregion

    }
}
