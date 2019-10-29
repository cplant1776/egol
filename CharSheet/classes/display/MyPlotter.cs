using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
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
                Title = "XP Progression",
                Background = OxyPlot.OxyColor.FromRgb(0, 0, 0),
                TextColor = OxyPlot.OxyColor.FromRgb(255, 255, 255),
                PlotAreaBorderColor = OxyPlot.OxyColor.FromRgb(255, 255, 255),               
            };

            DateTime today = DateTime.Now.Date;
            DateTime entryDate;
            int xPoint;
            int cumulativeXP = 0;
            DataPoint newPoint;
            Dictionary<DateTime, int> dailyXP = new Dictionary<DateTime, int> { };

            entries.Sort((x, y) => x.Timestamp.CompareTo(y.Timestamp)); // Sort history by date
            foreach (EventRecord entry in entries)
            {
                if (entry.GetType() == typeof(XPEvent))
                {
                    cumulativeXP += entry.Value;
                    if (dailyXP.ContainsKey(entry.Timestamp.Date))
                    {
                        dailyXP[entry.Timestamp.Date] += cumulativeXP;
                    }
                    else
                    {
                        dailyXP[entry.Timestamp.Date] = cumulativeXP;
                    }
                }
            }

            int maxDifference = FindLargestTimeDifference(dailyXP.Keys.ToList(), today);

            foreach (KeyValuePair<DateTime, int> day in dailyXP)
            {

                xPoint = Math.Abs((today - day.Key).Days - maxDifference);
                newPoint = new DataPoint(xPoint, day.Value);
                this.points.Add(newPoint);
            }

            LineSeries s = new LineSeries();
            foreach(DataPoint p in points)
            {
                s.Points.Add(p);
            }
            this.MyModel.Series.Add(s);

            // Add x-axis
            this.MyModel.Axes.Add(new LinearAxis {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = maxDifference,
            });

            // Add y-axis
            this.MyModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = dailyXP.Values.ToList().Max(), // Max value is limit
            });
        }

        public int FindLargestTimeDifference(List<DateTime> days, DateTime today)
        {
            int maxDifference = 0;
            int dayDifference = 0;
            foreach(DateTime day in days)
            {
                dayDifference = (today - day).Days;
                if (dayDifference > maxDifference)
                {
                    maxDifference = dayDifference;
                }
            }
            return maxDifference;
        }
    }
}
