using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

namespace StepAnalyzer
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string filePath = "pace_data.tlod";

        public MainPage()
        {
            this.InitializeComponent();
            Task.Run(() => loadViewModelDataAsync())
                .ContinueWith(task => updateViewModelsOneTime(task), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void loadViewModelDataAsync()
        {
            DataParser dataParser = new TLODDataParser(filePath);
            InfoCollector infoCollector = new InfoCollector(dataParser.format,dataParser.stepLengthCm , dataParser.days);
            MetaDataViewModel = new MetaDataViewModel(infoCollector.Format, infoCollector.FirstDay, infoCollector.CountedDays, infoCollector.MissingDays, infoCollector.StepSize);
            OverallViewModel = new OverallViewModel(infoCollector.StepsTotal, infoCollector.StepsMax, infoCollector.StepsMin, infoCollector.StepsAvg);
            DiagramViewModel = new DiagramViewModel(infoCollector.DailyTrendDiagramData, infoCollector.WeeklyAveragesDiagramData);
        }

        private void updateViewModelsOneTime(Task task)
        {
            // force updates for async loaded data
            this.DataContext = this;
            this.Bindings.Update();
        }

        public MetaDataViewModel MetaDataViewModel { get; private set; }
        public OverallViewModel OverallViewModel { get; private set; }
        public DiagramViewModel DiagramViewModel { get; private set; }
    }
}
