using Engine.Models;
using Engine.Utils;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Engine.ViewModels
{
    public class FullHistoryViewModel : BaseViewModel, IPageViewModel
    {
        #region Fields
        private ObservableCollection<EventRecordModel> _eventRecords;
        private MyPlotModel _expPlotModel;

        private ICommand _goToQuest;
        #endregion

        #region Constructors
        public FullHistoryViewModel()
        {
            if(this.UserCharacter.EventHistory.Any())
            {
                this._eventRecords = new ObservableCollection<EventRecordModel>(this.UserCharacter.EventHistory);
                this._expPlotModel = new MyPlotModel(this.UserCharacter.EventHistory);
            }
        }
        #endregion

        #region Public Properties/Commands
        
        public string Name
        {
            get { return "FullHistory"; }
        }

        public ObservableCollection<EventRecordModel> EventRecords { get { return _eventRecords; } set { _eventRecords = value; } }

        public MyPlotModel ExpPlotModel { get{ return _expPlotModel; } set { _expPlotModel = value; } }

        public ICommand GoToQuestCommand
        {
            get
            {
                if (_goToQuest == null)
                {
                    _goToQuest = new RelayCommand(
                        param => GoToQuest(param)
                    );
                }
                return _goToQuest;
            }
        }

        #endregion

        #region Methods
        private void GoToQuest(object sender)
        {
            QuestModel selectedQuest = new QuestModel();
            ListView senderList = (ListView)sender;
            int targetId = -1;
            // Find selected quest
            try // Double clicked "ACTIVE QUESTS" item
            {
                selectedQuest = (QuestModel)senderList.SelectedItems[0];
                targetId = selectedQuest.Id;

            }
            catch (InvalidCastException) // Double Clicked "RECENT EVENTS" item
            {
                EventRecordModel selectedEvent = (EventRecordModel)senderList.SelectedItems[0];
                if (selectedEvent.GetType() == typeof(MilestoneModel)) //If a milestone was clicked, do nothing
                    return;
                else
                    targetId = selectedEvent.AssociatedEventId;
            }
            catch (ArgumentOutOfRangeException) // Double clicked on contact's chip; not implemented (yet?)
            {
                return;
            }


            // Update default quest displayed when opening quest log
            AppSettings.UpdateDefaultSelectedQuestId(targetId);
            NavigateTo("Quest Log");
        }
        #endregion
    }
}
