using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using Common.Logging;

namespace Prototyping.UI.Console.Examples
{
    public class Example11
    {

        static ILog _logger = LogManager.GetLogger<Example11>();

        public void Run()
        {
            XmlNamespaceManager manager;
            XPathNavigator nav;
            XPathDocument docNav;
            XPathNodeIterator NodeIter;
            String strExpression;


            _logger.Info("reading file xml");
            // Open the XML.
            docNav = new XPathDocument(@"C:\Users\Master\Downloads\pesquisa-pregao\IN180706\BVBG.028.02_BV000327201807060327123794076363551.xml");
            // Create a navigator to query with XPath.
            nav = docNav.CreateNavigator();
            // Create a Namespace resolver.
            manager = new XmlNamespaceManager(nav.NameTable);
            manager.AddNamespace("report", "urn:bvmf.052.01.xsd");
            manager.AddNamespace("instrument", "urn:bvmf.100.02.xsd");


            // Find the title of the books that are greater then $10.00.
            //strExpression = "//report:FinInstrmAttrCmon[report:Mkt=2 or report:Mkt=3 or report:Mkt=4 or report:Mkt=5]/parent::*";
            strExpression = "//instrument:EqtyInf";

            // Select the node and place the results in an iterator.
            NodeIter = nav.Select(strExpression, manager);

            //Iterate through the results showing the element value.
            while (NodeIter.MoveNext())
            {
                _logger.Info(string.Format("Item: {0}", NodeIter.Current.Value));
            };

            // Find the title of the books that are greater then $10.00.
            strExpression = "//report:CreDtAndTm[1]";

            // Select the node and place the results in an iterator.
            NodeIter = nav.Select(strExpression, manager);

            //Iterate through the results showing the element value.
            while (NodeIter.MoveNext())
            {
                _logger.Info(string.Format("report date: {0}", NodeIter.Current.Value));
            };


        }

        public void ReadBVBG02802()
        {

            //http://www.bmfbovespa.com.br/pesquisapregao/download?filelist=IR180706.zip,PR180706.zip,IN180706.zip,II180706.zip,
            XmlDocument doc;
            XmlNode root;
            XmlNamespaceManager manager;
            XmlNode node;
            XmlNodeList equityInfo;
            XmlNodeList optionsInfo;

            doc = new XmlDocument();
            doc.Load(@"C:\Users\Master\Downloads\pesquisa-pregao\IN180706\BVBG.028.02_BV000327201807060327123794076363551.xml");
            root = doc.DocumentElement;
            // Add the namespace.  
            manager = new XmlNamespaceManager(doc.NameTable);
            manager.AddNamespace("report", "urn:bvmf.052.01.xsd");
            manager.AddNamespace("instrument", "urn:bvmf.100.02.xsd");

            // Select and display the first node in which the author's   
            // last name is Kingsolver.  
            node = root.SelectSingleNode("//report:CreDtAndTm[1]", manager);
            _logger.Info(string.Format("report date: {0}", node.InnerText));

            /*
             Mkt - Market (ExternalMarketCode)

             Representa o segundo nível da classificação de mercado no processo de pós-negociação.exemplo                          A Market represents the Second level of market classification in the post trade process.Example:
             1 -MERCADO DISPONIVEL                                                                                                 1 -Spot
             2 -MERCADO FUTURO                                                                                                     2 -Future
             3 -OPCOES SOBRE DISPONIVEL                                                                                            3 -Options on Spot
             4 -OPCOES SOBRE FUTURO                                                                                                4 -Options on Future
             5 -MERCADO TERMO                                                                                                      5 -Forward
             10 -Vista                                                                                                             10 -Cash
             12 -Exercício de opções de compra                                                                                     12 -Options exercise (call)
             13 -Exercício de opções de venda                                                                                      13 -Options exercise (put)
             17 -Leilão                                                                                                            17 -Auction
             20 -Fracionário                                                                                                       20 -Odd Lot
             30 -Termo                                                                                                             30 -Equity Forward
             70 -OPC                                                                                                               70 -Equity Call
             80 -OPV                                                                                                               80 -Equity Put                                                                                                                                                
             Este campo requer uma lista de código externo. Esses códigos e os valores foram criados em uma                        This field requires an external code list. Those codes and values have been made 
             planilha externa para permitir uma manutenção flexível de acordo com os requisitos de atualizações da BVMF.           external spreadsheet files to allow a flexible maintenance according to the updates requirements
             Neste caso, o externo é ExternalMarketCode no arquivo ExternalCodeLists_BVMF.xls                                      from BVMF. In this case the external is ExternalMarketCode

             Sgmt - Segment (ExternalSegmentCode)
            
             Segmento representa o primeiro nível da classificação de mercado no processo de pós-negociação.exemplo:                A Segment represents the first level of market classification in the post trade process.Example:
             1 -Ações -Vista                                                                                                        1 -Equity -Cash
             2 -Ações -Derivativos                                                                                                  2 -Equity derivative
             3 -Renda fixa privada                                                                                                  3 -Corporate bonds
             4 -Agronegócio                                                                                                         4 -Agribusiness
             5 -Financeiro                                                                                                          5 -Financial 
             6 -Metais                                                                                                              6 -Metal
             7 -Energia elétrica                                                                                                    7 -Energy
             8 -Títulos públicos                                                                                                    8 -Gov. Bonds
             9 -Câmbio                                                                                                              9 -FX
                                                                                                                                    This field requires an external code list. Those codes andvalues have been made external spreadsheet files to
            Este campo requeruma lista de código externo. Esses códigos e valores foram feitas em planilhas externas para           allow a flexible maintenance according to the updates requirements from BVMF. In this case the external is 
            permitir a manutenção flexível de acordo com os requisitos de atualizações da BM&FBOVESPA.                              ExternalSegmentCode in the file ExternalCodeLists_BVMF.xls
            Neste caso o externo é ExternalSegmentCode no arquivo ExternalCodeLists_BVMF.xls

            */
            //TODO: deixar os filtros flexiveis
            // LastPric > 0.0 (itens que os preços sejam maiores que 0
            var query = "//instrument:FinInstrmAttrCmon[instrument:Mkt=10 and instrument:Sgmt=1]/..//instrument:EqtyInf[instrument:LastPric>0.00 and not(normalize-space(instrument:CrpnNm)='TAXA DE FINANCIAMENTO')]";
            equityInfo = root.SelectNodes(query, manager);

            foreach (XmlNode oneEquityInfo in equityInfo)
            {
                var noAvo = oneEquityInfo.ParentNode?.ParentNode;
                var noIdInterno = noAvo?.SelectSingleNode("./instrument:FinInstrmId/instrument:OthrId/instrument:Id", manager);

                string isin = oneEquityInfo["ISIN"].InnerText;
                string nomeEmpresa = oneEquityInfo["CrpnNm"].InnerText;
                string ticker = oneEquityInfo["TckrSymb"].InnerText;
                string moeda = oneEquityInfo["TradgCcy"].InnerText;
                string marketCap = oneEquityInfo["MktCptlstn"].InnerText;
                string lastPrice = oneEquityInfo["LastPric"].InnerText;
                string idInterno = noIdInterno?.InnerText ?? string.Empty;

                _logger.Info(string.Format("ISIN:{0}\tID:{6}\tCompany:{1}\tTicker:{2}\tMoeda:{3}\tMarket:{4:c}\tLast:{5:c}"
                    , isin
                    , nomeEmpresa
                    , ticker
                    , moeda
                    , marketCap
                    , lastPrice
                    , idInterno));
            }

            optionsInfo = root.SelectNodes("//instrument:FinInstrmAttrCmon[(instrument:Mkt=70 or instrument:Mkt=80) and instrument:Sgmt=2]/..//instrument:OptnOnEqtsInf", manager);
            foreach (XmlNode oneOptionInfo in optionsInfo)
            {
                var noAvo = oneOptionInfo.ParentNode?.ParentNode;
                var noIdInterno = noAvo?.SelectSingleNode("./instrument:FinInstrmId/instrument:OthrId/instrument:Id", manager);
                var underlyingID = oneOptionInfo.SelectSingleNode("./instrument:UndrlygInstrmId/instrument:OthrId/instrument:Id", manager);

                string isin = oneOptionInfo["ISIN"].InnerText;
                string ticker = oneOptionInfo["TckrSymb"].InnerText;
                string moeda = oneOptionInfo["TradgCcy"].InnerText;
                string precoExecicio = oneOptionInfo["ExrcPric"].InnerText;
                string estiloOpcao = oneOptionInfo["OptnStyle"].InnerText; //american or european
                string dataExercicio = oneOptionInfo["XprtnDt"].InnerText; //expiration date
                string tipoOpcao = oneOptionInfo["OptnTp"].InnerText; //call put
                string underlying = underlyingID?.InnerText ?? string.Empty;
                string idInterno = noIdInterno?.InnerText ?? string.Empty;

                _logger.Info(string.Format("ISIN:{0}\tID:{1}\tTicker:{2}\tMoeda:{3}\tExecicio:{4}\tDataExercicio:{5}\tTipo:{6}\tEstilo:{7}"
                    , isin
                    , idInterno
                    , ticker
                    , moeda
                    , precoExecicio
                    , dataExercicio
                    , tipoOpcao
                    , estiloOpcao));
            }
        }

        public void ReadBVBG08601()
        {
            XmlDocument doc;
            XDocument xDoc;
            XmlNode root;
            XmlNamespaceManager manager;
            XmlNode node;
            XmlNode nodeAttributos;
            XmlNodeList priceInfo;

            doc = new XmlDocument();
            doc.Load(@"C:\Users\Master\Downloads\pesquisa-pregao\PR180706\BVBG.086.01_BV000328201807060328000001955213528.xml");
            xDoc = XDocument.Load(@"C:\Users\Master\Downloads\pesquisa-pregao\PR180706\BVBG.086.01_BV000328201807060328000001955213528.xml");
            

            root = doc.DocumentElement;
            // Add the namespace.  
            manager = new XmlNamespaceManager(doc.NameTable);
            manager.AddNamespace("report", "urn:bvmf.052.01.xsd");
            manager.AddNamespace("price", "urn:bvmf.217.01.xsd");

            // Select and display the first node in which the author's   
            // last name is Kingsolver.  
            node = root.SelectSingleNode("//report:CreDtAndTm[1]", manager);
            _logger.Info(string.Format("report date: {0}", node.InnerText));

            var query = "//price:PricRpt";
            priceInfo = root.SelectNodes(query, manager);


            //xDoc.XPathSelectElements("//price:FinInstrmAttrbts").Where(x => x.des)
            foreach (XmlNode onePriceInfo in priceInfo)
            {
                string symbol = string.Empty;
                string date = string.Empty;
                string idInterno = string.Empty;
                string quantidade = string.Empty;

                node = onePriceInfo.SelectSingleNode("./price:SctyId/price:TckrSymb", manager);
                symbol = node.InnerText;

                node = onePriceInfo.SelectSingleNode("./price:TradDt/price:Dt", manager);
                date = node.InnerText;

                node = onePriceInfo.SelectSingleNode("./price:FinInstrmId/price:OthrId/price:Id", manager);
                idInterno = node.InnerText;

                node = onePriceInfo.SelectSingleNode("./price:TradDtls/price:TradQty", manager);
                quantidade = node?.InnerText ?? "0";
                nodeAttributos = onePriceInfo.SelectSingleNode("./price:FinInstrmAttrbts", manager);                
                var filhos = ConverterPrecosFilhos(nodeAttributos);

                //node = nodeAttributos?.SelectSingleNode("./price:LastPric", manager);
                //var ultimoPreco = nodeAttributos?.SelectSingleNode("./price:LastPric")?.InnerText ?? "";

                _logger.Info(string.Format("symbol:{0}\tdate:{1}", symbol, date));

            }

            //ticker < -xmlValue(node[['SctyId']][['TckrSymb']])
            //trd_dt < -xmlValue(node[['TradDt']][['Dt']])
            //attrib < -node[['FinInstrmAttrbts']]
            //PREABE < - as.numeric(xmlValue(attrib[['FrstPric']]))
            //PREMIN < - as.numeric(xmlValue(attrib[['MinPric']]))
            //PREMED < - as.numeric(xmlValue(attrib[['TradAvrgPric']]))
            //PREULT < - as.numeric(xmlValue(attrib[['LastPric']]))
            //PREMAX < - as.numeric(xmlValue(attrib[['MaxPric']]))
            //OSCILA < - as.numeric(xmlValue(attrib[['OscnPctg']]))
            //neg1 < - as.numeric(xmlValue(attrib[['RglrTxsQty']]))
            //neg1 < - if (is.na(neg1)) 0 else neg1
            //neg2 < - as.numeric(xmlValue(attrib[['NonRglrTxsQty']]))
            //neg2 < - if (is.na(neg2)) 0 else neg2
            //TOTNEG < -neg1 + neg2
            //QUATOT < - as.numeric(xmlValue(attrib[['FinInstrmQty']]))
            //VOLTOT < - as.numeric(xmlValue(attrib[['NtlFinVol']]))
        }

        private Dictionary<string, Amount> ConverterPrecosFilhos(XmlNode noParaConverter)
        {
            Dictionary<string, Amount> _retorno = null;
            StringBuilder _currencyBuilder = null;
            if (noParaConverter != null && noParaConverter.HasChildNodes)
            {
                _currencyBuilder = new StringBuilder(3);
                _retorno = new Dictionary<string, Amount>();
                foreach (XmlNode umNoFilho in noParaConverter.ChildNodes)
                {
                    _currencyBuilder.Clear();
                    _currencyBuilder.Append(umNoFilho.Attributes["Ccy"]?.Value ?? string.Empty);
                    _retorno[umNoFilho.LocalName] = new Amount() { Value = Convert.ToDouble(umNoFilho.InnerText), Currency = _currencyBuilder.ToString() };
                }
            }
            return _retorno;
        }

        internal class Amount
        {
            public double Value { get; set; }
            public string Currency { get; set; }
        }
    }
}
