using System;

namespace StepAnalyzer
{
    /// <summary>
    /// Model containing a date and the steps made that day
    /// </summary>
    public class SADay
    {
        private readonly DateTime date;
        private readonly int steps;

        public SADay(DateTime date, int steps)
        {
            this.date = date;
            this.steps = steps;
        }

        public DateTime Date => date;
        public int Steps => steps;
    }
}