#region License
/*
 * B3DateUtilsExtensions.cs
 *
 * The MIT License
 *
 * Copyright (c) 2018 Felipe Bahiana Almeida
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 * 
 * Contributors:
 * - Felipe Bahiana Almeida <felipe.almeida@gmail.com> https://www.linkedin.com/in/felipe-almeida-ba222577
 * - Mark Ashley Bell https://markb.co.uk/csharp-datetime-get-first-last-day-of-week-or-month.html
 */
#endregion

namespace B3Provider.Utils
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Utilitary class to perform operations on dates
    /// </summary>
    public static partial class B3DateUtilsExtensions
    {
        #region "public methods"
        #region "actual days"
        public static DateTime FirstDayOfWeek(this DateTime? dt)
        {
            if (!dt.HasValue)
                dt = DateTime.Today;

            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = dt.Value.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return dt.Value.AddDays(-diff).Date;
        }

        public static DateTime FirstDayOfWeek(this DateTime? dt, DayOfWeek startOfWeek)
        {
            if (!dt.HasValue)
                dt = DateTime.Today;

            int diff = (7 + (dt.Value.DayOfWeek - startOfWeek)) % 7;
            return dt.Value.AddDays(-1 * diff).Date;
        }

        public static DateTime LastDayOfWeek(this DateTime? dt)
        {
            return dt.FirstDayOfWeek().AddDays(6);
        }

        public static DateTime FirstDayOfMonth(this DateTime? dt)
        {
            if (!dt.HasValue)
                dt = DateTime.Today;

            return new DateTime(dt.Value.Year, dt.Value.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime? dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        public static DateTime FirstDayOfQuarter(this DateTime? dt)
        {
            int quarterNumber = 0;

            if (!dt.HasValue)
                dt = DateTime.Today;

            quarterNumber = (dt.Value.Month - 1) / 3 + 1;

            return new DateTime(dt.Value.Year, (quarterNumber - 1) * 3 + 1, 1);
        }

        public static DateTime FirstDayOfYear(this DateTime? dt)
        {
            if (!dt.HasValue)
                dt = DateTime.Today;

            return new DateTime(dt.Value.Year, 1, 1);
        }
        
        public static DateTime FirstDayOfNextMonth(this DateTime? dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1);
        }
        #endregion

        #region "business days"
        /// <summary>
        /// Calculates the previous work date according to a date informed
        /// </summary>
        /// <param name="referenceDate">Reference date to calculate previous for</param>
        /// <returns>
        /// Previous working day 
        /// </returns>
        public static DateTime PreviousBusinessDay(this DateTime? dt, IList<DateTime> holidays)
        {  
            if (!dt.HasValue)
                dt = DateTime.Today;

            return dt.NBusinessDaysAgo(1, holidays);
        }

        public static DateTime FirstBusinessDayOfWeek(this DateTime? dt, IList<DateTime> holidays)
        {
            DateTime suposedFirstDay;
            suposedFirstDay = dt.FirstDayOfWeek();
            while (IsHoliday(suposedFirstDay, holidays) || IsWeekend(suposedFirstDay))
            {
                suposedFirstDay = suposedFirstDay.Add(TimeSpan.FromDays(1));
            }

            return suposedFirstDay;
        }

        public static DateTime FirstBusinessDayOfWeek(this DateTime? dt, DayOfWeek startOfWeek, IList<DateTime> holidays)
        {
            DateTime suposedFirstDay;
            suposedFirstDay = dt.FirstDayOfWeek(startOfWeek);
            while (IsHoliday(suposedFirstDay, holidays) || IsWeekend(suposedFirstDay))
            {
                suposedFirstDay = suposedFirstDay.Add(TimeSpan.FromDays(1));
            }
            return suposedFirstDay;
        }

        public static DateTime FirstBusinessDayOfMonth(this DateTime? dt, IList<DateTime> holidays)
        {
            DateTime suposedFirstDay;
            suposedFirstDay = dt.FirstDayOfMonth();
            while (IsHoliday(suposedFirstDay, holidays) || IsWeekend(suposedFirstDay))
            {
                suposedFirstDay = suposedFirstDay.Add(TimeSpan.FromDays(1));
            }

            return suposedFirstDay;
        }

        public static DateTime FirstBusinessDayOfQuarter(this DateTime? dt, IList<DateTime> holidays)
        {
            DateTime suposedFirstDay;
            suposedFirstDay = dt.FirstDayOfQuarter();
            while (IsHoliday(suposedFirstDay, holidays) || IsWeekend(suposedFirstDay))
            {
                suposedFirstDay = suposedFirstDay.Add(TimeSpan.FromDays(1));
            }

            return suposedFirstDay;
        }

        public static DateTime FirstBusinessDayOfYear(this DateTime? dt, IList<DateTime> holidays)
        {
            DateTime suposedFirstDay;
            suposedFirstDay = dt.FirstDayOfYear();
            while (IsHoliday(suposedFirstDay, holidays) || IsWeekend(suposedFirstDay))
            {
                suposedFirstDay = suposedFirstDay.Add(TimeSpan.FromDays(1));
            }

            return suposedFirstDay;
        }
        
        public static DateTime NDaysAgo(this DateTime? dt, int numberOfDaysAgo)
        {
            DateTime resultDate;

            if (!dt.HasValue)
                dt = DateTime.Today;

            resultDate = dt.Value;

            resultDate = resultDate.Subtract(TimeSpan.FromDays(Math.Abs(numberOfDaysAgo)));
            return resultDate;
        }

        public static DateTime NBusinessDaysAgo(this DateTime? dt, int numberOfDaysAgo, IList<DateTime> holidays)
        {
            DateTime resultDate;

            if (!dt.HasValue)
                dt = DateTime.Today;

            resultDate = dt.Value;
            numberOfDaysAgo = Math.Abs(numberOfDaysAgo);

            while (numberOfDaysAgo > 0)
            {
                resultDate = resultDate.Subtract(TimeSpan.FromDays(1));
                if (!IsHoliday(resultDate, holidays) && !IsWeekend(resultDate))
                    numberOfDaysAgo--;
            }
            return resultDate;
        }
        #endregion

        #region "Util dates"
        public static DateUtilCalculation UtilDates(this DateTime? dt, IList<DateTime> holidays)
        {
            DateUtilCalculation _results = null;

            if (!dt.HasValue)
                dt = DateTime.Today;

            while (IsHoliday(dt.Value, holidays) || IsWeekend(dt.Value))
            {
                dt= dt.Value.Subtract(TimeSpan.FromDays(1));
            }

            _results = new DateUtilCalculation();

            _results.CurrentDate = dt.Value;
            //XTD calculation
            _results.YTDDate = dt.FirstBusinessDayOfYear(holidays);
            _results.QTDDate = dt.FirstBusinessDayOfQuarter(holidays);
            _results.MTDDate = dt.FirstBusinessDayOfMonth(holidays);
            _results.WTDDate = dt.FirstBusinessDayOfWeek(DayOfWeek.Monday, holidays);

            //One NDays calculation
            _results.OneYearDate = dt.NBusinessDaysAgo(252, holidays);
            _results.OneQuarterDate = dt.NBusinessDaysAgo((252/4), holidays);
            _results.OneMonthDate = dt.NBusinessDaysAgo((252 / 12), holidays);
            _results.OneWeekDate = dt.NBusinessDaysAgo(5, holidays);
            _results.OneDayDate = dt.NBusinessDaysAgo(1, holidays); 

            return _results;
        }
        #endregion
        #endregion

        #region "private methods"
        /// <summary>
        /// Method to check if a date is weekend
        /// </summary>
        /// <param name="dateToCheck">date to check if it is weekend</param>
        /// <returns>
        /// true if the date is saturday or sunday
        /// </returns>
        private static bool IsWeekend(DateTime dateToCheck)
        {
            return dateToCheck.DayOfWeek == DayOfWeek.Saturday || dateToCheck.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Method to check if a date is holiday
        /// </summary>
        /// <param name="dateToCheck">date to check if it is holiday</param>
        /// <returns>
        /// true if the date is a exchange holiday
        /// </returns>
        private static bool IsHoliday(DateTime dateToCheck, IList<DateTime> holidays)
        {
            if (holidays == null)
                return false;

            if (holidays.Contains(dateToCheck))
                return true;

            return false;
        }
        #endregion  
    }

    [Serializable()]
    public class DateUtilCalculation
    {
        public DateTime CurrentDate { get; set; }
        public DateTime YTDDate { get; set; }
        public DateTime QTDDate { get; set; }
        public DateTime MTDDate { get; set; }
        public DateTime WTDDate { get; set; }
        public DateTime OneYearDate { get; set; }
        public DateTime OneQuarterDate { get; set; }
        public DateTime OneMonthDate { get; set; }
        public DateTime OneWeekDate { get; set; }
        public DateTime OneDayDate { get; set; }

    }
}
