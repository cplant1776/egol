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
            this.MyModel = new PlotModel
            {
                Title = "XP / Day",
                
            };

            this.points = new List<DataPoint>
                              {
                                  new DataPoint(0, 4),
                                  new DataPoint(10, 13),
                                  new DataPoint(20, 15),
                                  new DataPoint(30, 16),
                                  new DataPoint(40, 12),
                                  new DataPoint(50, 12)
                              };

            LineSeries s = new LineSeries();
            foreach(DataPoint x in points)
            {
                s.Points.Add(x);
            }
            this.MyModel.Series.Add(s);
        }
    }
}
