using DiabetesDiary;
using DiabetesDiary.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

/// <summary>
/// Main window, this launches everything
/// </summary>
namespace DiabetesDiaryReports
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Entry> entryLog;
        CollectionViewSource entryLogViewSource;

        List<string> SummaryList;
        public ObservableCollection<string> Summary { get; set; }

        /// <summary>
        /// This is where everything starts when program loads
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Bind the Entry Log
            entryLog = Db.GetEntryLog();
            entryLogViewSource =
                (CollectionViewSource)(FindResource("EntryLogViewSource"));
            entryLogViewSource.Source = entryLog;

            // Bind the Summary Report
            SummaryList = Db.GetSummary();
            Summary = new ObservableCollection<string>(SummaryList);
            summaryListBox.ItemsSource = SummaryList;
        }

        /// <summary>
        /// Event handler for create log report button
        /// Saves the report to a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateLogReport_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder str = new StringBuilder();
            str.Append("  Date      Type       Time  Glucose  Base  Adj  Fast  Slow  Carbs  Notes\n"); 

            foreach (Entry ob in entryLog)
            {
                string type = "unknown";
                switch (ob.EntryType)
                {
                    case 1: type = "Breakfast"; break;
                    case 2: type = "Lunch"; break;
                    case 3: type = "Dinner"; break;
                    case 4: type = "Bedtime"; break;
                }

                str.Append(String.Format("{0,10}  ", ob.DateStr));
                str.Append(String.Format("{0,-9}  ", type));
                str.Append(String.Format("{0,-4}  ", ob.Time.ToString()));
                str.Append(String.Format("{0,-7}  ", ob.Glucose.ToString()));
                str.Append(String.Format("{0,-4}  ", ob.BaseDose.ToString()));
                str.Append(String.Format("{0,-3}  ", ob.GlucoseAdj.ToString()));
                str.Append(String.Format("{0,-4}  ", ob.FastActingDose.ToString()));
                str.Append(String.Format("{0,-4}  ", ob.SlowActingDose.ToString()));
                str.Append(String.Format("{0,-5}  ", ob.CarbGrams.ToString()));
                str.Append(ob.Notes);
                str.Append("\n");
            }

            TextWriter tw = new StreamWriter("LogReport.txt");
            tw.WriteLine(str); ;
            tw.Close();

            MessageBox.Show("Log Entry Report was saved to LogReport.txt");
        }

        /// <summary>
        /// Event handler for create summary report button
        /// Saves the report to a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateSummary_Click(object sender, RoutedEventArgs e)
        {            
            TextWriter tw = new StreamWriter("SummaryReport.txt");

            foreach (String s in SummaryList)
            {
                tw.WriteLine(s);
            }

            tw.Close();
            MessageBox.Show("Summary report was saved to SummaryReport.txt");
         }
    }
}
