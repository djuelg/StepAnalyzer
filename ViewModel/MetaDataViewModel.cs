public class MetaDataViewModel
{
    public MetaDataViewModel(string format, string firstDay, string countedDays, string missingDays, string stepSize)
    {
        this.DataformatText = format;
        this.FirstDayText = firstDay;
        this.CountedDaysText = countedDays;
        this.MissingDaysText = missingDays;
        this.StepSizeText = stepSize;
    }

    public string DataformatText { get; private set; }
    public string FirstDayText { get; private set; }
    public string CountedDaysText { get; private set; }
    public string MissingDaysText { get; private set; }
    public string StepSizeText { get; private set; }
}