using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepAnalyzer.Model
{
    public class WeeklyAverages : List<DayAverage>
    {
        public WeeklyAverages(int mon, int tue, int wed, int thu, int fri, int sat, int sun) : base(7)
        {
            base.Add(new DayAverage("Montag",mon));
            base.Add(new DayAverage("Dienstag",tue));
            base.Add(new DayAverage("Mittwoch",wed));
            base.Add(new DayAverage("Donnerstag",thu));
            base.Add(new DayAverage("Freitag",fri));
            base.Add(new DayAverage("Samstag",sat));
            base.Add(new DayAverage("Sonntag",sun));
        }
    }

    public class DayAverage
    {
        public DayAverage(String day, int steps)
        {
            this.Day = day;
            this.Steps = steps;
        }

        public string Day { get; }
        public int Steps { get; }
    }
}
