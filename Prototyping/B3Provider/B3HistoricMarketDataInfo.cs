#region License
/*
 * B3HistoricMarketDataInfo.cs
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

namespace B3Provider
{
    using System;

    public class B3HistoricMarketDataInfo
    {

        //public int Tipo { get; set; }                           //TIPO DE REGISTRO  -- N02
        public int Type { get; set; }                           //TIPO DE REGISTRO  -- N02
        //public DateTime DataPregao { get; set; }                //DATA DO PREGÃO --N08
        public DateTime TradeDate { get; set; }                //DATA DO PREGÃO --N08
        //public string CodigoBDI { get; set; }                   //CODBDI - CÓDIGO BDI --X02
        public string BDICode { get; set; }                   //CODBDI - CÓDIGO BDI --X02
        //public string CodigoNegociacao { get; set; }            //CODNEG - CÓDIGO DE NEGOCIAÇÃO DO PAPEL --X12
        public string TradeCode { get; set; }            //CODNEG - CÓDIGO DE NEGOCIAÇÃO DO PAPEL --X12
        //public string CodigoTipoMercado { get; set; }           //TPMERC - TIPO DE MERCADO  --N03
        public string MarketTypeCode { get; set; }           //TPMERC - TIPO DE MERCADO  --N03
        //public string NomeResumido { get; set; }                //NOMRES - NOME RESUMIDO DA EMPRESA EMISSORA DO PAPEL --X12
        public string ShortName { get; set; }                //NOMRES - NOME RESUMIDO DA EMPRESA EMISSORA DO PAPEL --X12
        //public string Especificacao { get; set; }               //ESPECI - ESPECIFICAÇÃO DO PAPEL --X10
        public string Specification { get; set; }               //ESPECI - ESPECIFICAÇÃO DO PAPEL --X10
        //public string PrazoDiasTermo { get; set; }              //PRAZOT - PRAZO EM DIAS DO MERCADO A TERMO--X03
        public string ForwardDaysToExpiry { get; set; }              //PRAZOT - PRAZO EM DIAS DO MERCADO A TERMO--X03
        //public string MoedaReferencia { get; set; }             //MODREF - MOEDA DE REFERÊNCIA --X04
        public string ReferenceCurrency { get; set; }             //MODREF - MOEDA DE REFERÊNCIA --X04
        //public double Abertura { get; set; }                    //PREABE - PREÇO DE ABERTURA DO PAPEL-MERCADO NO PREGÃO 11V99
        public double Opening { get; set; }                    //PREABE - PREÇO DE ABERTURA DO PAPEL-MERCADO NO PREGÃO 11V99
        //public double Maximo { get; set; }                      //PREMAX - PREÇO MÁXIMO DO PAPEL-MERCADO NO PREGÃO 11V99
        public double Maximum { get; set; }                      //PREMAX - PREÇO MÁXIMO DO PAPEL-MERCADO NO PREGÃO 11V99
        //public double Minimo { get; set; }                      //PREMIN - PREÇO MÍNIMO DO PAPEL- MERCADO NO PREGÃO 11V99
        public double Minimum { get; set; }                      //PREMIN - PREÇO MÍNIMO DO PAPEL- MERCADO NO PREGÃO 11V99
        //public double Media { get; set; }                       //PREMED - PREÇO MÉDIO DO PAPEL- MERCADO NO PREGÃO 11V99
        public double Average { get; set; }                       //PREMED - PREÇO MÉDIO DO PAPEL- MERCADO NO PREGÃO 11V99
        //public double Ultimo { get; set; }                      //PREULT - PREÇO DO ÚLTIMO NEGÓCIO DO PAPEL-MERCADO NO PREGÃO 11V99
        public double Last { get; set; }                      //PREULT - PREÇO DO ÚLTIMO NEGÓCIO DO PAPEL-MERCADO NO PREGÃO 11V99
        //public double MelhorOfertaCompra { get; set; }          //PREOFC - PREÇO DA MELHOR OFERTA DE COMPRA DO PAPEL-MERCADO 11V99
        public double BestBidPrice { get; set; }          //PREOFC - PREÇO DA MELHOR OFERTA DE COMPRA DO PAPEL-MERCADO 11V99
        //public double MelhorOfertaVenda { get; set; }           //PREOFV - PREÇO DA MELHOR OFERTA DE VENDA DO PAPEL-MERCADO 11V99
        public double BestAskPrice { get; set; }           //PREOFV - PREÇO DA MELHOR OFERTA DE VENDA DO PAPEL-MERCADO 11V99
        //public long TotalNegocios { get; set; }                 //TOTNEG - NEG. - NÚMERO DE NEGÓCIOS EFETUADOS COM O PAPEL- MERCADO NO PREGÃO N(05)public long TotalNegocios { get; set; }                 //TOTNEG - NEG. - NÚMERO DE NEGÓCIOS EFETUADOS COM O PAPEL- MERCADO NO PREGÃO N(05)
        public long TradeQuantity { get; set; }                 //TOTNEG - NEG. - NÚMERO DE NEGÓCIOS EFETUADOS COM O PAPEL- MERCADO NO PREGÃO N(05)
        //public long QuantidadeTitulos { get; set; }             //QUATOT - QUANTIDADE TOTAL DE TÍTULOS NEGOCIADOS NESTE PAPEL- MERCADO N(18)
        public long ContractQuantity { get; set; }             //QUATOT - QUANTIDADE TOTAL DE TÍTULOS NEGOCIADOS NESTE PAPEL- MERCADO N(18)
        //public double VolumeTotal { get; set; }                 //VOLTOT - VOLUME TOTAL DE TÍTULOS NEGOCIADOS NESTE PAPEL- MERCADO N(18)
        public double FinancialVolume { get; set; }                 //VOLTOT - VOLUME TOTAL DE TÍTULOS NEGOCIADOS NESTE PAPEL- MERCADO N(18)
        //public double PrecoExercicio { get; set; }              //PREEXE - PREÇO DE EXERCÍCIO PARA O MERCADO DE OPÇÕES OU VALOR DO CONTRATO PARA O MERCADO DE TERMO SECUNDÁRIO (11)V99
        public double StrikePrice { get; set; }              //PREEXE - PREÇO DE EXERCÍCIO PARA O MERCADO DE OPÇÕES OU VALOR DO CONTRATO PARA O MERCADO DE TERMO SECUNDÁRIO (11)V99
        //public int IndicadorCorrecaoPrecoExercicio { get; set; } //INDOPC - INDICADOR DE CORREÇÃO DE PREÇOS DE EXERCÍCIOS OU VALORES DE CONTRATO PARA OS MERCADOS DE OPÇÕES OU TERMO SECUNDÁRIO N(01)
        public int StrikePriceCorrectionIndicator { get; set; } //INDOPC - INDICADOR DE CORREÇÃO DE PREÇOS DE EXERCÍCIOS OU VALORES DE CONTRATO PARA OS MERCADOS DE OPÇÕES OU TERMO SECUNDÁRIO N(01)
        //public DateTime DataVencimento { get; set; }             //DATVEN - DATA DO VENCIMENTO PARA OS MERCADOS DE OPÇÕES OU TERMO SECUNDÁRIO N(08)
        public DateTime ExpiryDate { get; set; }             //DATVEN - DATA DO VENCIMENTO PARA OS MERCADOS DE OPÇÕES OU TERMO SECUNDÁRIO N(08)
        //public int FatorCotacao { get; set; }                       //FATCOT - FATOR DE COTAÇÃO DO PAPEL N(07)
        public int QuoteFactor { get; set; }                       //FATCOT - FATOR DE COTAÇÃO DO PAPEL N(07)
        //public double PrecoExercicioPontosOpcaoDolar { get; set; }  //PTOEXE - PREÇO DE EXERCÍCIO EM PONTOS PARA OPÇÕES REFERENCIADAS EM DÓLAR OU VALOR DE CONTRATO EM PONTOS PARA TERMO SECUNDÁRIO (07)V06
        public double DollarOptionPointsStrikePrice { get; set; }  //PTOEXE - PREÇO DE EXERCÍCIO EM PONTOS PARA OPÇÕES REFERENCIADAS EM DÓLAR OU VALOR DE CONTRATO EM PONTOS PARA TERMO SECUNDÁRIO (07)V06
        //public string CodigoISIN { get; set; }                      //CODISI - CÓDIGO DO PAPEL NO SISTEMA ISIN OU CÓDIGO INTERNO DO PAPEL X(12)
        public string ISIN { get; set; }                      //CODISI - CÓDIGO DO PAPEL NO SISTEMA ISIN OU CÓDIGO INTERNO DO PAPEL X(12)
        //public int NumeroDistribuicaoPapel { get; set; }         //DISMES - NÚMERO DE DISTRIBUIÇÃO DO PAPEL 9(03)
        public int InstrumentDistributionNumber { get; set; }         //DISMES - NÚMERO DE DISTRIBUIÇÃO DO PAPEL 9(03)
    }
}
