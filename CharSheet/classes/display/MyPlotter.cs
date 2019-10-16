using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;

namespace CharSheet.classes
{
    class MyPlotter
    {

        public PlotModel MyModel { get; private set; }
        public IList<DataPoint> points { get; private set; }

        public MyPlotter()
        {
            this.points = new List<DataPoint> { };
        }

        public void PlotXPHistory(List<EventRecord> entries)
        {
            this.MyModel = new PlotModel
            {
                Title = "XP / Day",
            };

            DateTime today = DateTime.Now;
            DateTime entryDate;
            int dayDifference;
            DataPoint newPoint;

            foreach (EventRecord entry in entries)
            {
                if (entry.GetType() == typeof(XPEvent))
                {
                    dayDifference = (today - entry.Timestamp).Days;
                    newPoint = new DataPoint(dayDifference, entry.Value);
                    this.points.Add(newPoint);
                }

                LineSeries s = new LineSeries();
                foreach(DataPoint p in points)
                {
                    s.Points.Add(p);
                }

                this.MyModel.Series.Add(s);
            }
        }
    }
}
