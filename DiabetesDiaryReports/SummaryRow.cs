using DiabetesDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Class for one row in summary report
/// </summary>
namespace DiabetesDiaryReports
{
    class SummaryRow
    {
        public int Date { get; set; }

        public String DateStr
        {
            get
            {
                return DiabetesDiary.Misc.DateToString(Date);
            }
        }

        public Entry Breakfast { get; set; }
        public Entry Lunch { get; set; }
        public Entry Dinner { get; set; }
        public Entry Bedtime { get; set; }

        /// <summary>
        /// Calculate average glucose for a day's worth of entries
        /// </summary>
        public int Average 
        {
            get 
            {
                int total = 0;
                int count = 0;

                if (Breakfast.Glucose > 0)
                {
                    total += Breakfast.Glucose;
                    count++;
                }

                if (Lunch.Glucose > 0)
                {
                    total += Lunch.Glucose;
                    count++;
                }

                if (Dinner.Glucose > 0)
                {
                    total += Dinner.Glucose;
                    count++;
                }

                if (Bedtime.Glucose > 0)
                {
                    total += Bedtime.Glucose;
                    count++;
                }

                return (total > 0) ? (int)total / count : 0;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SummaryRow()
        {
            Date = 0;
            Breakfast = new Entry();
            Lunch = new Entry();
            Dinner = new Entry();
            Bedtime = new Entry();
        }

        /// <summary>
        /// format an entry for the log report
        /// </summary>
        /// <param name="ob">entry object</param>
        /// <returns>string for entry information</returns>
        public string FormatEntry(Entry ob)
        {
            string g = "";
            string bd = ""; 
            string adj = "";
            string fast = "";
            string slow = "";
            string carb = "";
            string avg = "";

            if (ob.Glucose > 0) g = ob.Glucose.ToString();
            else return String.Format("{0,23}", "");

            if (ob.BaseDose > 0) bd = ob.BaseDose.ToString();
            if (ob.GlucoseAdj > 0) adj = ob.GlucoseAdj.ToString();
            if (ob.FastActingDose > 0) fast = ob.FastActingDose.ToString();
            if (ob.SlowActingDose > 0) slow = ob.SlowActingDose.ToString();
            if (ob.CarbGrams > 0) carb = ob.CarbGrams.ToString();
            avg = Average.ToString();

            // Gluc  Lunch Doses   Carbs
            // 140   06+8=14, 10    32

            string x = String.Format("{0,4} ({1,2}+{2,1}={3,2}, {4,2}) {5,3}",
                g, bd, adj, fast, slow, carb);
            return String.Format("{0,-23}", x);
        }

        /// <summary>
        /// Mostly for troubleshooting, this shows all information for this
        /// row in string form
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format(
                "{0,10} {1} .. {2} .. {3} .. {4} .. Avg: {5}",
                DateStr, FormatEntry(Breakfast), FormatEntry(Lunch),
                FormatEntry(Dinner), FormatEntry(Bedtime), Average);
        }

        

    }
}

//public void AddEntry(int type, int gluc, int baseDose, int adj,
//    int total, int fast, int slow, int carbs)
//{

//}

