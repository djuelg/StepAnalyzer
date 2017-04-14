using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace StepAnalyzer
{
    /// <summary>
    /// Implementation of DataParser to support files from TodoListOfDeath
    /// </summary>
    internal class TLODDataParser : DataParser
    {
        private const string _format = "TodoList Of Death";

        public TLODDataParser(string filePath)
        {
            var content = Normalize(File.ReadAllText(filePath));
            var year = getYearFromMetadata(content);
            this.stepLengthCm = getStepLengthFromMetadata(content);
            content = removeMetadata(content);

            this.days = GenerateSADays(content, year);
            this.format = _format;
        }

        private string removeMetadata(string content)
        {
            StringBuilder newContent = new StringBuilder();
            List<string> splittedContent = new List<string>(Regex.Split(content, "##"));
            for (int i=2; i < splittedContent.Count; i++)
            {
                newContent.Append("##");
                newContent.Append(splittedContent[i]);
            }
            return newContent.ToString();
        }

        private int getStepLengthFromMetadata(string content)
        {
            content = Regex.Split(content, "##")[1];
            string stepLength = Regex.Split(content, "-")[1];
            return int.Parse(stepLength.Substring(12));
        }

        private int getYearFromMetadata(string content)
        {
            content = Regex.Split(content, "##")[1];
            string year = Regex.Split(content, "-")[2];
            return int.Parse(year.Substring(9));
        }

        /// <summary>
        /// List of Days for the UI to display
        /// </summary>
        public List<SADay> days { get; }

        /// <summary>
        /// Name of the Dataformat this DataParser is implementing
        /// </summary>
        public string format { get; }

        /// <summary>
        /// stepLength to calculate kilometers
        /// </summary>
        public int stepLengthCm { get; }


        /// <summary>
        /// Normalize input; assume the syntax is always correct
        /// </summary>
        /// <param name="content">of a TLOD file.</param>
        /// <returns></returns>
        private string Normalize(string content)
        {
            // remove unneccesary newlines
            content = Regex.Replace(content, "[\r\n]+","", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            // remove Details in braces
            content = Regex.Replace(content, "\\(.*?\\)", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            // remove whitespaces
            content = Regex.Replace(content, "[ ]", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            // Output is something like: "##April-08.8762-07.9673-06.9006-05.10062"
            return content;
        }

        /// <summary>
        /// Helper to generate a List of the Models later used by the UI
        /// </summary>
        /// <param name="content">of a TLOD file. Assume content is normalized</param>
        /// <param name="year">in format YYYY</param>
        /// <returns></returns>
        private List<SADay> GenerateSADays(string content, int year)
        {
            List<SADay> days = new List<SADay>();
            List<string> months = new List<string>(Regex.Split(content, "##"));
            // remove first item which is always empty
            months.RemoveAt(0);
            foreach (string month in months)
            {
                days.AddRange(generateDaysPerMonth(month, year));
            }
            return days;
        }

        /// <summary>
        /// Helper to generate a List of the Models of a specific month
        /// </summary>
        /// <param name="month">A written month in German</param>
        /// <param name="year">in format YYYY</param>
        /// <returns></returns>
        private IEnumerable<SADay> generateDaysPerMonth(string month, int year)
        {
            List<SADay> days = new List<SADay>();
            List<string> daysString = new List<string>(Regex.Split(month, "-"));
            string monthName = daysString[0];
            daysString.RemoveAt(0);
            foreach (string day in daysString) {
                string dateString = day.Split('.')[0] + monthName + year;
                DateTime date = DateTime.ParseExact(dateString, "ddMMMMyyyy", new CultureInfo("de-DE"));
                int steps = Int32.Parse(day.Split('.')[1]);
                days.Add(new SADay(date, steps));
            }
            return days;
        }
    }
}