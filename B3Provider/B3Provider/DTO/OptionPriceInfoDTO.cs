using B3Provider.Records;

namespace B3Provider.DTO
{ 
    /// <summary>
    /// Class used to show equity detailed information and 
    /// price change data
    /// </summary>
public class OptionPriceInfoDTO
    {
        /// <summary>
        /// Data about the Option
        /// </summary>
        public B3OptionOnEquityInfo Option { get; set; }

        /// <summary>
        /// Option greeks 
        /// </summary>
        public OptionGreeksInfo Greeks { get; set; }

        #region "nominal variation"
        /// <summary>
        /// Nominal variation of 1 day
        /// </summary>
        public double? DailyNominal { get; set; }

        /// <summary>
        /// Nominal variation of 1 week
        /// </summary>
        public double? WeeklyNominal { get; set; }

        /// <summary>
        /// Nominal variation of 1 month
        /// </summary>
        public double? MonthlyNominal { get; set; }

        /// <summary>
        /// Nominal variation of 1 quarter
        /// </summary>
        public double? QuaterlyNominal { get; set; }

        /// <summary>
        /// Nominal variation of 1 year
        /// </summary>
        public double? YearlyNominal { get; set; }
        #endregion

        #region "percentage variation"
        /// <summary>
        /// Percentage variation of 1 day
        /// </summary>
        public double? DailyPercentage { get; set; }

        /// <summary>
        /// Percentage variation of 1 week
        /// </summary>
        public double? WeeklyPercentage { get; set; }

        /// <summary>
        /// Percentage variation of 1 month
        /// </summary>
        public double? MonthlyPercentage { get; set; }

        /// <summary>
        /// Percentage variation of 1 quarter
        /// </summary>
        public double? QuaterlyPercentage { get; set; }

        /// <summary>
        /// Percentage variation of 1 year
        /// </summary>
        public double? YearlyPercentage { get; set; }
        #endregion

    }
}
