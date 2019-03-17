#region License
/*
 * B3FutureInfo.cs
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

using System;

namespace B3Provider.Records
{
    /// <summary>
    /// Class that represents a future 
    /// </summary>
    public class B3FutureInfo
    {
        /// <summary>
        /// B3 Internal identification of the instrument
        /// </summary>
        public long? B3ID { get; set; }

        /// <summary>
        /// ISIN world public instrument identification
        /// </summary>
        public string ISIN { get; set; }

        /// <summary>
        /// Name of the Asset
        /// </summary>
        public string AssetName { get; set; }

        /// <summary>
        /// Description of the Asset
        /// </summary>
        public string AssetDescription { get; set; }

        /// <summary>
        /// Description of the instrument
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Category of the instrument acording to the external classification of B3
        /// TODO: find the file ExternalCodeLists_BVMF.xls
        /// </summary>
        public int SecurityCategoryCode { get; set; }

        /// <summary>
        /// Ticker of most common refered to symbol of the future
        /// </summary>
        public string Ticker { get; set; }        

        /// <summary>
        /// Expiration date of the future
        /// </summary>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// Expiration date of the future
        /// This attribute has two types of format:
        /// Format:  MYYM = Month CodeY = Year 
        /// CodeFormat: MYOA
        /// Example: M19 (19 = year)
        /// 
        /// Código	Vencimento
        /// F       JAN
        /// G       FEV
        /// H       MAR
        /// J       ABR
        /// K       MAI
        /// M       JUN
        /// N       JUL
        /// Q       AGO
        /// U       SET
        /// V       OUT
        /// X       NOV
        /// Z       DEZ
        /// </summary>
        public string ExpirationCode { get; set; }

        /// <summary>
        /// Trade Start date of the future
        /// </summary>
        public DateTime TradeStart { get; set; }
        
        /// <summary>
        /// Trade End date of the future
        /// </summary>
        public DateTime TradeEnd { get; set; }

        /// <summary>
        /// Value type code
        /// TODO: find the file ExternalCodeLists_BVMF.xls
        /// Code that defines the type of value of instrument, e.g.,price or rate
        /// EX: 0 -Rate1 -Price
        /// </summary>
        public int ValueTypeCode { get; set; }

        /// <summary>
        /// Day count base code
        /// Basis of counting days. Number of days in the period of calculating, e.g., 252, 360, 365        
        /// </summary>
        /// <remarks>
        /// Note:This field is used only for conversion from RATE to PRICE.
        /// This situation only applies to the following commodities:
        /// -DDI
        /// -DAP
        /// -DDM
        /// -DI1
        /// -DIL
        /// Note: SCC is traded in RATE but it is not meant to be converted to price
        /// </remarks>
        public int? DaycountBase { get; set; }

        /// <summary>
        /// Type of criteria of conversion, e.g., linear, exponential, non available.
        /// This field is used only for contracts negotiated in rate and need to be converted to price. 
        /// Currently this situation only occurs in the following commodities:
        /// -DDI
        /// -DAP
        /// -DDM
        /// -DI1
        /// -DIL
        /// Note: The foreign exchange swap rate is negotiated on but is not converted to price.
        /// This field requires a list of external code. 
        /// These codes and values were made in external spreadsheets to enable flexible maintenance in accordance with the
        /// requirements of the BM&FBOVESPA updates. 
        /// In this case the external file is in ExternalConversionCriteriaTypeCode ExternalCodeLists_BVMF.xls
        /// </summary>
        public int? ConversionCriteriaCode { get; set; }

        /// <summary>
        /// Contract value in points.
        /// This field is used along with the Base Code and Conversion Criteria Type to allow conversion from rate to price
        /// Esse campo é utilizado em conjunto com os 2campos anteriores (Base e Conversão Requerida) 
        /// para permitir a conversão de taxa para preço, fornecendo o número de pontos no vencimento para os cotratos negociados em taxa
        /// </summary>
        public double? MaturityContractValueInPoints { get; set; }

        /// <summary>
        /// Indicates whether a interest rate contract must be converted to price or not.
        /// Currently the only contract on rate that does not need to be converted is the foreign exchange swap. 
        /// This field will not be filled in contracts traded price.
        /// </summary>
        public bool? RequiredConversionIndicator { get; set; }

        /// <summary>
        /// Code that classifies the instrument.
        /// </summary>
        public string CFICategoryCode { get; set; }

        /// <summary>
        /// Starting date of delivery notice. A notice written by the holder of the short position in a futures contract informing the 
        /// clearing house of the intent and details of delivering a commodity  for settlement.
        /// </summary>
        public DateTime? DeliveryNoticeStart { get; set; }

        /// <summary>
        /// Final date of delivery notice. A notice written by the holder of the short position 
        /// in a futures contract informing the clearing house of the intent and details of delivering a commodity  for settlement
        /// </summary>
        public DateTime? DeliveryNoticeEnd { get; set; }

        /// <summary>
        /// Code that identifies the  typeof delivery at maturity,e.g., Physical Delivery,  
        /// Financial Delivery.This field requires a list of external code. 
        /// These codes and values were made in external spreadsheets to enable flexible maintenance in accordance with the 
        /// requirements of the BM&FBOVESPA updates. 
        /// In this case the external file is in ExternalDeliveryTypeCode ExternalCodeLists_BVMF.xls
        /// EX: 
        /// 0 -Physical Delivery
        /// 1 -Financial
        /// </summary>
        public int DeliveryTypeCode { get; set; }

        /// <summary>
        ///  Specifies how the transaction is to be settled, 
        ///  for example, 
        ///  0 -Netting
        ///  1 -Gross
        ///  2 -Without Financial Transfer
        ///  3 -Between Parts
        ///  This field requires a list of external code. These codes and values were made in external spreadsheets 
        ///  to enable flexible maintenance in accordance with the requirements of the BM&FBOVESPA updates. 
        ///  In this case the external file is in ExternalPaymentTypeCode ExternalCodeLists_BVMF.x
        /// </summary>
        public int PaymentTypeCode { get; set; }

        /// <summary>
        /// It is the ratio between the contract size and the trading reference quantity. 
        /// For Instance, Cattle is a 330 arrobas contract, but trade price refers to 1 arroba, 
        /// so the multiplier is 330. Dollar contracts are 50000 USD but the
        /// price refers to 1000 USD, so the multiplier is 50.
        /// For contracts traded in rate instead of price, this attribute represents the ratio between target 
        /// points and contract size
        /// </summary>
        /// <remarks>
        /// É a razão entre o tamanho do contrato e a quantidade de cotação da mercadoria. 
        /// Por exemplo, o contrato futuro de boi (BGI) é composto  de 330 arrobas, mas o preço de negociação é baseado em 1 arroba. 
        /// Logo para calcular o valor financeiro de uma operação, é necessário multiplicar o valor negociado por 330 
        /// (Multiplicador do Contrato). Outro exemplo são os contratos de dólar, definidos em US$ 50.000, mas cujo preço negociado 
        /// refere-se a US$ 1.000.
        /// Para contratos negociados em taxa ao invés de preço, este atributo representa a razão entre os pontos no vencimento e o 
        /// tamanho do contrato.
        /// </remarks>
        public double ContractMultiplier { get; set; }

        /// <summary>
        /// Indicates the commodity quantity the trading price is based on. 
        /// For example: Cattle trading price is based on 1 arroba. 
        /// Dollar trading price is based on 1000 dollars.
        /// This field is filled in with “1” if the instrument is traded at interest rate
        /// </summary>
        /// <remarks>
        /// Indica a quantidade da mercadoria na qual o preço do negócio é baseado. 
        /// Por exemplo, preço de negócios de boi são baseados em 1 arroba. 
        /// Preço de negócios de dólar são baseados em 1000 dólares;
        /// Esse atributo é preenchido com “1” se o instrumento for negociado em taxa.
        /// </remarks>
        public double AssetQuotationQuantity { get; set; }

        /// <summary>
        /// This block has the identification of the economic indicator to be applied for settlement purposesThe indicator itself is identified 
        /// as any other instrument,it means, by a 3-field key from FIXCurrently contracts that demand settlement indicator are those 
        /// traded in foreign exchange or those that demands indicators to convert points in price, like inflation contracts.
        /// </summary>
        public B3FutureDerivativeInfo SettlementIndexInfo { get; set; }

        /// <summary>
        /// Pre-defined lot size for allocation purposes.
        /// </summary>
        public double AllocationRoundLot { get; set; }

        /// <summary>
        /// Trading currency of the future
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Underlying info of the future
        /// </summary>
        public B3FutureDerivativeInfo UnderlyingIntrument { get; set; }

        /// <summary>
        /// It provides the number of days of withdrawal, 
        /// considering the date of the session until the contract expiration date (inclusive).
        /// </summary>
        public int WithdrawalDays { get; set; }

        /// <summary>
        /// It provides the number of working days, 
        /// considering the date of the session until the date of contract expiration (inclusive).
        /// </summary>
        public int WorkingDays { get; set; }

        /// <summary>
        /// It provides the number of calendar days, 
        /// considering the date of trading until the date of contract expiration (inclusive).
        /// </summary>
        public int CalendarDays { get; set; }

        /// <summary>
        /// Date of update        
        /// </summary>
        public DateTime? LoadDate { get; set; }

    }

    /// <summary>
    /// Identification of the index to be used to settle the future contract
    /// </summary>
    public class B3FutureDerivativeInfo
    {
        /// <summary>
        /// Identification of a security. Instrument sequential code in the Trade Structure system. (Security ID)
        /// </summary>
        public long Identifier { get; set; }

        /// <summary>
        /// Unique and unambiguous identification source using a proprietary identification scheme.
        /// </summary>
        public int IdentifierTypeCode { get; set; }
        
        /// <summary>
        /// Stock exchange of listing
        /// </summary>
        public string PlaceOfListing { get; set; }
    }
}
