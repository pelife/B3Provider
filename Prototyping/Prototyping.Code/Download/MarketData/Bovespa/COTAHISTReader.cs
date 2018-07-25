using Common.Logging;
using FlatFile.Core;
using FlatFile.FixedLength;
using FlatFile.FixedLength.Implementation;
using Prototyping.Code.Download.MarketData.Bovespa.Converter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Prototyping.Code.Download.MarketData.Bovespa
{
    public class COTAHISTHeader
    {
        public int Tipo { get; set; }                //TIPO DE REGISTRO
        public string NomeArquivo { get; set; }      //NOME DO ARQUIVO
        public string Origem { get; set; }           //CÓDIGO DA ORIGEM
        public DateTime DataGeracao { get; set; }    //DATA DA GERAÇÃO DO ARQUIVO
    }

    /*
012017010202AALR3       010ALLIAR      ON      NM   R$  000000000146200000000014880000000001440000000000145800000000014600000000001460000000000147300087000000000000035900000000000052350500000000000000009999123100000010000000000000BRAALRACNOR6100
    */

    public class COTAHISTHistorico
    {
        public int Tipo { get; set; }                           //TIPO DE REGISTRO  -- N02
        public DateTime DataPregao { get; set; }                //DATA DO PREGÃO --N08
        public string CodigoBDI { get; set; }                   //CODBDI - CÓDIGO BDI --X02
        public string CodigoNegociacao { get; set; }            //CODNEG - CÓDIGO DE NEGOCIAÇÃO DO PAPEL --X12
        public string CodigoTipoMercado { get; set; }           //TPMERC - TIPO DE MERCADO  --N03
        public string NomeResumido { get; set; }                //NOMRES - NOME RESUMIDO DA EMPRESA EMISSORA DO PAPEL --X12
        public string Especificacao { get; set; }               //ESPECI - ESPECIFICAÇÃO DO PAPEL --X10
        public string PrazoDiasTermo { get; set; }              //PRAZOT - PRAZO EM DIAS DO MERCADO A TERMO--X03
        public string MoedaReferencia { get; set; }             //MODREF - MOEDA DE REFERÊNCIA --X04
        public double Abertura { get; set; }                    //PREABE - PREÇO DE ABERTURA DO PAPEL-MERCADO NO PREGÃO 11V99
        public double Maximo { get; set; }                      //PREMAX - PREÇO MÁXIMO DO PAPEL-MERCADO NO PREGÃO 11V99
        public double Minimo { get; set; }                      //PREMIN - PREÇO MÍNIMO DO PAPEL- MERCADO NO PREGÃO 11V99
        public double Media { get; set; }                       //PREMED - PREÇO MÉDIO DO PAPEL- MERCADO NO PREGÃO 11V99
        public double Ultimo { get; set; }                      //PREULT - PREÇO DO ÚLTIMO NEGÓCIO DO PAPEL-MERCADO NO PREGÃO 11V99
        public double MelhorOfertaCompra { get; set; }          //PREOFC - PREÇO DA MELHOR OFERTA DE COMPRA DO PAPEL-MERCADO 11V99
        public double MelhorOfertaVenda { get; set; }           //PREOFV - PREÇO DA MELHOR OFERTA DE VENDA DO PAPEL-MERCADO 11V99
        public long TotalNegocios { get; set; }                 //TOTNEG - NEG. - NÚMERO DE NEGÓCIOS EFETUADOS COM O PAPEL- MERCADO NO PREGÃO N(05)
        public long QuantidadeTitulos { get; set; }             //QUATOT - QUANTIDADE TOTAL DE TÍTULOS NEGOCIADOS NESTE PAPEL- MERCADO N(18)
        public double VolumeTotal { get; set; }                 //VOLTOT - VOLUME TOTAL DE TÍTULOS NEGOCIADOS NESTE PAPEL- MERCADO N(18)
        public double PrecoExercicio { get; set; }              //PREEXE - PREÇO DE EXERCÍCIO PARA O MERCADO DE OPÇÕES OU VALOR DO CONTRATO PARA O MERCADO DE TERMO SECUNDÁRIO (11)V99
        public int IndicadorCorrecaoPrecoExercicio { get; set; } //INDOPC - INDICADOR DE CORREÇÃO DE PREÇOS DE EXERCÍCIOS OU VALORES DE CONTRATO PARA OS MERCADOS DE OPÇÕES OU TERMO SECUNDÁRIO N(01)
        public DateTime DataVencimento { get; set; }             //DATVEN - DATA DO VENCIMENTO PARA OS MERCADOS DE OPÇÕES OU TERMO SECUNDÁRIO N(08)
        public int FatorCotacao { get; set; }                       //FATCOT - FATOR DE COTAÇÃO DO PAPEL N(07)
        public double PrecoExercicioPontosOpcaoDolar { get; set; }  //PTOEXE - PREÇO DE EXERCÍCIO EM PONTOS PARA OPÇÕES REFERENCIADAS EM DÓLAR OU VALOR DE CONTRATO EM PONTOS PARA TERMO SECUNDÁRIO (07)V06
        public string CodigoISIN { get; set; }                      //CODISI - CÓDIGO DO PAPEL NO SISTEMA ISIN OU CÓDIGO INTERNO DO PAPEL X(12)
        public int NumeroDistribuicaoPapel { get; set; }         //DISMES - NÚMERO DE DISTRIBUIÇÃO DO PAPEL 9(03)
    }

    public class COTAHISTTrailer
    {
        public int Tipo { get; set; }            //TIPO DE REGISTRO
        public string NomeArquivo { get; set; }     //NOME DO ARQUIVO
        public string Origem { get; set; }          //CÓDIGO DA ORIGEM
        public DateTime DataGeracao { get; set; }   //DATA DA GERAÇÃO DO ARQUIVO
        public int TotalRegistros { get; set; }     //TOTAL DE REGISTROS
    }

    public class COTAHISTResultado
    {
        public COTAHISTHeader Header {get;set;}
        public IList<COTAHISTHistorico> Registros { get; set; }
        public COTAHISTTrailer Trailer { get;set;}
    }

    public sealed class COTAHISTHeaderLayout : FixedLayout<COTAHISTHeader>
    {
        public COTAHISTHeaderLayout()
        {
            this.WithMember(x => x.Tipo, c => c.WithLength(2))
                .WithMember(x => x.NomeArquivo, c => c.WithLength(13))
                .WithMember(x => x.Origem, c => c.WithLength(8))
                .WithMember(x => x.DataGeracao, c => c.WithLength(8).WithTypeConverter<BDIDateTypeConverter>());
        }
    }

    /*
    012017010202AALR3       010ALLIAR      ON      NM   R$  
    0000000001462 PREABE
    0000000001488 PREMAX
    0000000001440 PREMIN
    0000000001458 PREMED
    0000000001460 PREULT
    0000000001460 PREOFC
    0000000001473 PREOFV
    00087 TOTNEG
    000000000000035900 QUATOT
    000000000052350500
    000000000000
    0
    09999123100000010000000000000BRAALRACNOR6100
    */
    public sealed class COTAHISTHistoricoLayout : FixedLayout<COTAHISTHistorico>
    {
        public COTAHISTHistoricoLayout()
        {
            this.WithMember(x => x.Tipo, c => c.WithLength(2))                                                                              //2     -   2
                .WithMember(x => x.DataPregao, c => c.WithLength(8).WithTypeConverter<BDIDateTypeConverter>())                              //10    -   8
                .WithMember(x => x.CodigoBDI, c => c.WithLength(2).WithTypeConverter<BDIStringTypeConverter>())                             //12    -   2
                .WithMember(x => x.CodigoNegociacao, c => c.WithLength(12).WithTypeConverter<BDIStringTypeConverter>())                     //24    -   12
                .WithMember(x => x.CodigoTipoMercado, c => c.WithLength(3))                                                                 //27    -   3
                .WithMember(x => x.NomeResumido, c => c.WithLength(12).WithTypeConverter<BDIStringTypeConverter>())                         //39    -   12
                .WithMember(x => x.Especificacao, c => c.WithLength(10).WithTypeConverter<BDIStringTypeConverter>())                        //40    -   10
                .WithMember(x => x.PrazoDiasTermo, c => c.WithLength(3))                                                                    //43    -   3
                .WithMember(x => x.MoedaReferencia, c => c.WithLength(4).WithTypeConverter<BDIStringTypeConverter>())                                                                   //47    -   4
                .WithMember(x => x.Abertura, c => c.WithLength(13).WithTypeConverter<BDIDouble100TypeConverter>())                          //60    -   13 
                .WithMember(x => x.Maximo, c => c.WithLength(13).WithTypeConverter<BDIDouble100TypeConverter>())                            //73    -   13
                .WithMember(x => x.Minimo, c => c.WithLength(13).WithTypeConverter<BDIDouble100TypeConverter>())                            //86    -   13
                .WithMember(x => x.Media, c => c.WithLength(13).WithTypeConverter<BDIDouble100TypeConverter>())                             //99    -   13
                .WithMember(x => x.Ultimo, c => c.WithLength(13).WithTypeConverter<BDIDouble100TypeConverter>())                            //112   -   13
                .WithMember(x => x.MelhorOfertaCompra, c => c.WithLength(13).WithTypeConverter<BDIDouble100TypeConverter>())                //125   -   13
                .WithMember(x => x.MelhorOfertaVenda, c => c.WithLength(13).WithTypeConverter<BDIDouble100TypeConverter>())                 //138   -   13
                .WithMember(x => x.TotalNegocios, c => c.WithLength(5))                                                                     //143   -   5
                .WithMember(x => x.QuantidadeTitulos, c => c.WithLength(18))                                                                //161   -   18
                .WithMember(x => x.VolumeTotal, c => c.WithLength(18).WithTypeConverter<BDIDouble100TypeConverter>())                       //178   -   17
                .WithMember(x => x.PrecoExercicio, c => c.WithLength(13).WithTypeConverter<BDIDouble100TypeConverter>())                    //191   -   13
                .WithMember(x => x.IndicadorCorrecaoPrecoExercicio, c => c.WithLength(1))                                                   //192   -   1
                .WithMember(x => x.DataVencimento, c => c.WithLength(8).WithTypeConverter<BDIDateVencimentoTypeConverter>())                          //200   -   8
                .WithMember(x => x.FatorCotacao, c => c.WithLength(7))                                                                      //207   -   7
                .WithMember(x => x.PrecoExercicioPontosOpcaoDolar, c => c.WithLength(13).WithTypeConverter<BDIDouble100TypeConverter>())    //220   -   13
                .WithMember(x => x.CodigoISIN, c => c.WithLength(12).WithTypeConverter<BDIStringTypeConverter>())                           //233   -   13
                .WithMember(x => x.NumeroDistribuicaoPapel, c => c.WithLength(3));                                                          //236   -   3
        }
    }

    public sealed class COTAHISTTrailerLayout : FixedLayout<COTAHISTTrailer>
    {
        public COTAHISTTrailerLayout()
        {
            this.WithMember(x => x.Tipo, c => c.WithLength(2))
                .WithMember(x => x.NomeArquivo, c => c.WithLength(13))
                .WithMember(x => x.Origem, c => c.WithLength(8))
                .WithMember(x => x.DataGeracao, c => c.WithLength(8).WithTypeConverter<BDIDateTypeConverter>())
                .WithMember(x => x.TotalRegistros, c => c.WithLength(11));
        }
    }

    

    public class COTAHISTReader
    {
        private ILog _logger = LogManager.GetLogger<COTAHISTReader>();
        public COTAHISTResultado Read(Stream streamToRead)
        {
            COTAHISTResultado resultado = null;

            //
            var factory = new FixedLengthFileEngineFactory();

            // If using attribute mapping, pass an array of record types
            // rather than layout instances
            var layouts = new ILayoutDescriptor<IFixedFieldSettingsContainer>[]
            {
                    new COTAHISTHeaderLayout()
                    ,new COTAHISTHistoricoLayout()
                    ,new COTAHISTTrailerLayout()
            };

            var flatFile = factory.GetEngine(layouts,
                  (line, lineNumber) =>
                  {
                      // For each line, return the proper record type.
                      // The mapping for this line will be loaded based on that type.
                      // In this simple example, the first character determines the
                      // record type.
                      if (string.IsNullOrEmpty(line) || line.Length < 1) return null;
                      switch (line.Substring(0, 2))
                      {
                          case "00":
                              return typeof(COTAHISTHeader);
                          case "01":
                              return typeof(COTAHISTHistorico);
                          case "99":
                              return typeof(COTAHISTTrailer);
                      }
                      return null;
                  });

            _logger.Trace("stated reading file");
            flatFile.Read(streamToRead);
            _logger.Trace("finished reading file");

            resultado = new COTAHISTResultado();
            resultado.Header = flatFile.GetRecords<COTAHISTHeader>().FirstOrDefault();
            resultado.Registros = flatFile.GetRecords<COTAHISTHistorico>().ToList();
            resultado.Trailer = flatFile.GetRecords<COTAHISTTrailer>().FirstOrDefault();
            
            return resultado;
        }
    }
}
