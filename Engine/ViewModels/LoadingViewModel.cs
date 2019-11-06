using Engine.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Engine.ViewModels
{
    public class LoadingViewModel : BaseViewModel, IPageViewModel, INotifyPropertyChanged
    {

        #region Fields
        
        #endregion

        #region Constructors
        public LoadingViewModel()
        {
            if(AppSettings.LoadDestination != null)
                StartTransition();

        }
        #endregion

        #region Public Properties/Commands
        public string Name { get { return "Loading Screen"; } }

        public string Destination;
        public int Duration;
        #endregion

        #region Methods
        private void StartTransition()
        {
            // Fire a one-off event after AppSettings.LoadDuration seconds
            DispatcherTimer transitionTimer = new DispatcherTimer();
            transitionTimer.Tick += new EventHandler(TransitionTimer_Tick);
            transitionTimer.Interval = new TimeSpan(0, 0, AppSettings.LoadDuration);
            transitionTimer.Start();
        }

        private void TransitionTimer_Tick(object sender, EventArgs e)
        {
            // Navigate to destination then cancel future ticks
            NavigateTo(AppSettings.LoadDestination);
            (sender as DispatcherTimer).Stop();
        }

        #endregion

    }
}
