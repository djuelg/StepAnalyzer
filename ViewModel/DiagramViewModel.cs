using StepAnalyzer.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StepAnalyzer
{
    public class DiagramViewModel
    {

        public DiagramViewModel(List<SADay> days, WeeklyAverages weeklyAverages)
        {
            this.DailyTrendList = days;
            this.WeeklyAverages = weeklyAverages;
        }

        public List<SADay> DailyTrendList { get; }
        public WeeklyAverages WeeklyAverages { get; }
    }
}