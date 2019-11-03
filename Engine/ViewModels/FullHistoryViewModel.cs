using Engine.Models;
using Engine.Utils;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Engine.ViewModels
{
    public class FullHistoryViewModel : BaseViewModel, IPageViewModel
    {
        #region Fields
        private ObservableCollection<EventRecordModel> _eventRecords;
        private MyPlotModel _expPlotModel;
        private ICommand _goBack;
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

        public ICommand GoBackCommand
        {
            get
            {
                if (_goBack == null)
                {
                    _goBack = new RelayCommand(
                        param => GoBack()
                    );
                }
                return _goBack;
            }
        }
        #endregion

        #region Methods
        private void GoBack()
        {
            NavigateTo("Dashboard");
        }
        #endregion
    }
}
