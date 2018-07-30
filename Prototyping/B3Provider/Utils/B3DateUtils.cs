#region License
/*
 * B3DateUtils.cs
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
 */
#endregion
 
namespace B3Provider.Utils
{
    using System;
    /// <summary>
    /// Utilitary class to perform operations on dates
    /// </summary>
    public class B3DateUtils
    {
        #region "public methods"
        /// <summary>
        /// Calculates the previous work date according to a date informed
        /// </summary>
        /// <param name="referenceDate">Reference date to calculate previous for</param>
        /// <returns>
        /// Previous working day 
        /// </returns>
        public static DateTime PreviousWorkDate(DateTime? referenceDate)
        {
            DateTime _previousDay;
            if (!referenceDate.HasValue)
                referenceDate = DateTime.Today;

            _previousDay = referenceDate.Value;

            do
            {
                _previousDay  = _previousDay.Subtract(TimeSpan.FromDays(1));
            }
            while (IsHoliday(_previousDay) || IsWeekend(_previousDay));

            return _previousDay;
        }
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
        private static bool IsHoliday(DateTime dateToCheck)
        {
            //TODO: implement the holidays calendar
            return false;
        }
        #endregion  
    }
}
