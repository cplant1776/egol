﻿using System;
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

        public void PlotXPHistory(List<HistoryEntry> entries)
        {
            this.MyModel = new PlotModel
            {
                Title = "XP / Day",
            };

            DateTime today = DateTime.Now;
            DateTime entryDate;
            int dayDifference;
            DataPoint newPoint;

            foreach (HistoryEntry entry in entries)
            {
                entryDate = DateTime.Parse(entry.timestamp.Substring(0, 10));
                if (!entry.isMilestone)
                {
                    Console.WriteLine(entry.timestamp);
                    dayDifference = (today - entryDate).Days;
                    newPoint = new DataPoint(dayDifference, entry.value);
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