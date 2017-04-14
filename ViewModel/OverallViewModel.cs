namespace StepAnalyzer
{
    public class OverallViewModel
    {

        public OverallViewModel(string stepsTotal, string stepsMax, string stepsMin, string stepsAvg)
        {
            this.StepsTotalText = stepsTotal;
            this.StepsMaxText = stepsMax;
            this.StepsMinText = stepsMin;
            this.StepsAvgText = stepsAvg;
        }

        public string StepsTotalText { get; private set; }
        public string StepsMaxText { get; private set; }
        public string StepsMinText { get; private set; }
        public string StepsAvgText { get; private set; }
    }
}