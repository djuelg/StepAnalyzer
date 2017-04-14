using StepAnalyzer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StepAnalyzer
{
    class InfoCollector
    {
        private readonly List<SADay> days;
        private readonly int stepSize;

        internal readonly string CountedDays;
        internal readonly string MissingDays;
        internal readonly string FirstDay;
        internal readonly string Format;
        internal readonly string StepsTotal;
        internal readonly string StepsMax;
        internal readonly string StepsMin;
        internal readonly string StepsAvg;
        internal readonly string StepSize;

        internal List<SADay> DailyTrendDiagramData { get; private set; }
        internal WeeklyAverages WeeklyAveragesDiagramData { get; private set; }

        public InfoCollector(string format, int stepSize, List<SADay> days)
        {
            this.days = days;
            this.stepSize = stepSize;

            // metadata
            this.CountedDays = getCountedDays();
            this.MissingDays = getMissingDays();
            this.FirstDay = getFirstDay();
            this.Format = format;
            this.StepSize = getStepSize();

            // overall data
            this.StepsTotal = getTotalStepsCount();
            this.StepsMax = getMaxStepsPerDay();
            this.StepsMin = getMinStepsPerDay();
            this.StepsAvg = getAvgStepsPerDay();

            // Diagram data
            this.DailyTrendDiagramData = days;
            this.WeeklyAveragesDiagramData = getWeeklyAverages();
            
            // Graphen: 
            // Entwickung der Schritte (Linie)
            // Wochen Tagesdurchschnitte (Balken)
            // Schritte pro Monat (Balken)
        }

        /// <summary>
        /// 1. Sum the number of steps for each week day
        /// 2. If there are multiple entries for one week day divide it to get the avg value
        /// 
        /// Sunday is weekDays[0] whereas Saturday is weekDays[6]
        /// </summary>
        /// <returns>The average number of steps for a week day.</returns>
        private WeeklyAverages getWeeklyAverages()
        {
            int[] weekDays = new int[7];
            int[] weekDayCount = new int[7];

            foreach (SADay day in days)
            {
                weekDays[(int)day.Date.DayOfWeek] += day.Steps;
                weekDayCount[(int)day.Date.DayOfWeek]++;
            }
            for (int i=0; i < weekDays.Length; i++) {
                if (weekDayCount[i] > 1) weekDays[i] = weekDays[i] / weekDayCount[i];
            }
            // The Week in WeeklyAverages Starts with Monday not Sunday
            return new WeeklyAverages(weekDays[1], weekDays[2], weekDays[3], weekDays[4], weekDays[5], weekDays[6], weekDays[0]);
        }

        private string getStepSize()
        {
            return stepSize + " cm";
        }

        private string getAvgStepsPerDay()
        {
            double steps = (days.Sum(item => item.Steps) / days.Count);
            return string.Format("{0:n0}", steps) + stepsToKilometers(steps);
        }

        private string getMinStepsPerDay()
        {
            double steps = days.Min(item => item.Steps);
            return string.Format("{0:n0}", steps) + stepsToKilometers(steps);
        }

        private string getMaxStepsPerDay()
        {
            double steps = days.Max(item => item.Steps);
            return string.Format("{0:n0}", steps) + stepsToKilometers(steps);
        }

        private string getTotalStepsCount()
        {
            double steps = days.Sum(item => item.Steps);
            return string.Format("{0:n0}", steps) + stepsToKilometers(steps);
        }

        private string getMissingDays()
        {
            int number = 0;
            for (int i=1; i < days.Count; i++)
            {
                number = number + ((days[i-1].Date-days[i].Date).Days -1);
            }
            return number + "";
        }

        private string getFirstDay()
        {
            return string.Format("{0:dd. MMMM yyyy}" , days[days.Count - 1].Date);
        }

        private string getCountedDays()
        {
            return string.Format("{0:n0}", days.Count);
        }

        private string stepsToKilometers(double steps)
        {
            return string.Format(" ({0:n} km)", Convert.ToDouble((steps * (double)stepSize) / 100000));
        }
    }
}