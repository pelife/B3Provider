namespace B3ProviderExplorer.DTO
{
    /// <summary>
    /// Class that stores values of 
    /// O - Open
    /// H - High
    /// C - Close
    /// L - Low
    /// V - Volume
    /// </summary>
    public class OHCLVDTO
    {
        /// <summary>
        /// Open price or the first price
        /// </summary>
        public double Open { get; set; }

        /// <summary>
        /// Highest price or the maximum price
        /// </summary>
        public double High { get; set; }

        /// <summary>
        /// Close price or the last price
        /// </summary>
        public double Close { get; set; }

        /// <summary>
        /// Lowest price or the minimal price
        /// </summary>
        public double Low { get; set; }

        /// <summary>
        /// Financial Volume
        /// </summary>
        public double FinancialVolume { get; set; }

        /// <summary>
        /// Quantity Volume
        /// </summary>
        public double QuantityVolume { get; set; }

        /// <summary>
        /// Transacion Volume
        /// </summary>
        public double TransactionVolume { get; set; }

    }
}
