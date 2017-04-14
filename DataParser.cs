using System.Collections.Generic;

namespace StepAnalyzer
{
    /// <summary>
    /// Interface to support different data types as input
    /// </summary>
    interface DataParser
    {
        List<SADay> days { get; }
        string format { get;}
        int stepLengthCm { get; }
    }
}