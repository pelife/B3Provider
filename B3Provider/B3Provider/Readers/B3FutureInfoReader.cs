using B3Provider.Records;
using B3Provider.Utils;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace B3Provider.Readers
{
    public class B3FutureInfoReader : AbstractReader<B3FutureInfo>
    {
        public override IList<B3FutureInfo> ReadRecords(string filePath)
        {
            var listOfFuture = new List<B3FutureInfo>();

            var temporaryPath = GetRandomTemporaryDirectory();
            var filesToRead = UnzipFile(filePath, temporaryPath);
            if (filesToRead != null && filesToRead.Length > 0)
            {
                foreach (string oneFileToRead in filesToRead)
                {
                    var futureInfo = ReadFile(oneFileToRead);
                    listOfFuture.AddRange(futureInfo);
                }
            }

            DeleteDirectory(temporaryPath);
            return listOfFuture;
        }

        private IList<B3FutureInfo> ReadFile(string filePath)
        {
            IList<B3FutureInfo> futureInfo = null;

            var cultureNumericAmerica = new CultureInfo("en-US");

            var futureInfoDocument = new XmlDocument();
            var relevantFutureInfoQuery = "//instrument:FinInstrmAttrCmon[instrument:Mkt=2 and instrument:Sgmt=5]/..//instrument:FutrCtrctsInf";

            futureInfoDocument.Load(filePath);

            var rootNode = futureInfoDocument.DocumentElement;
            var nameSpaceManager = new XmlNamespaceManager(futureInfoDocument.NameTable);

            nameSpaceManager.AddNamespace("report", "urn:bvmf.052.01.xsd");
            nameSpaceManager.AddNamespace("instrument", "urn:bvmf.100.02.xsd");

            var dateNode = rootNode.SelectSingleNode("//report:CreDtAndTm[1]", nameSpaceManager);
            var futureNodes = rootNode.SelectNodes(relevantFutureInfoQuery, nameSpaceManager);

            if (futureNodes != null)
            {
                futureInfo = new List<B3FutureInfo>();

                foreach (XmlNode oneFutureNode in futureNodes)
                {
                    var oneFutureInfo = new B3FutureInfo();
                    var noAvo = oneFutureNode.ParentNode?.ParentNode;
                    var noIdInterno = noAvo?.SelectSingleNode("./instrument:FinInstrmId/instrument:OthrId/instrument:Id", nameSpaceManager);
                    var noDescricao = noAvo?.SelectSingleNode("./instrument:FinInstrmAttrCmon/instrument:Desc", nameSpaceManager);
                    var underlyingID = oneFutureNode.SelectSingleNode("./instrument:UndrlygInstrmId/instrument:OthrId/instrument:Id", nameSpaceManager);

                    oneFutureInfo.B3ID = noIdInterno.InnerText.ToNullable<long>();
                    oneFutureInfo.ISIN = oneFutureNode["ISIN"].InnerText;
                    oneFutureInfo.Description = noDescricao.InnerText.RemoveMultipleSpaces().Trim();

                }
            }

            return futureInfo;

        }
    }
}

/*
<Instrm>
            <RptParams>
              <ActvtyInd>true</ActvtyInd>
              <Frqcy>DAIL</Frqcy>
              <NetPosId>XXXX</NetPosId>
              <RptDtAndTm>
                <Dt>2019-03-12</Dt>
              </RptDtAndTm>
              <UpdTp>COMP</UpdTp>
            </RptParams>
            <FinInstrmId>
              <OthrId>
                <Id>100000090002</Id>
                <Tp>
                  <Prtry>8</Prtry>
                </Tp>
              </OthrId>
              <PlcOfListg>
                <MktIdrCd>BVMF</MktIdrCd>
              </PlcOfListg>
            </FinInstrmId>
            <FinInstrmAttrCmon>
              <Asst>WIN</Asst>
              <AsstDesc>Minicontrato de Ibovespa</AsstDesc>
              <Mkt>2</Mkt>
              <Sgmt>5</Sgmt>
              <Desc>IBOVESPA MINI</Desc>
            </FinInstrmAttrCmon>
            <InstrmInf>
              <FutrCtrctsInf>
                <SctyCtgy>0</SctyCtgy>
                <XprtnDt>2019-04-17</XprtnDt>
                <TckrSymb>WINJ19</TckrSymb>
                <XprtnCd>J19</XprtnCd>
                <TradgStartDt>2017-07-11</TradgStartDt>
                <TradgEndDt>2019-04-17</TradgEndDt>
                <ValTpCd>1</ValTpCd>
                <ISIN>BRBMEFWIN2E6</ISIN>
                <CFICd>FXXXXX</CFICd>
                <DlvryTp>1</DlvryTp>
                <PmtTp>0</PmtTp>
                <CtrctMltplr>0.200000000</CtrctMltplr>
                <AsstQtnQty>1.000000000</AsstQtnQty>
                <AllcnRndLot>1</AllcnRndLot>
                <TradgCcy>BRL</TradgCcy>
                <UndrlygInstrmId>
                  <OthrId>
                    <Id>100000090000</Id>
                    <Tp>
                      <Prtry>8</Prtry>
                    </Tp>
                  </OthrId>
                  <PlcOfListg>
                    <MktIdrCd>BVMF</MktIdrCd>
                  </PlcOfListg>
                </UndrlygInstrmId>
                <WdrwlDays>26</WdrwlDays>
                <WrkgDays>26</WrkgDays>
                <ClnrDays>36</ClnrDays>
              </FutrCtrctsInf>
            </InstrmInf>
          </Instrm>

    <Instrm>
            <RptParams>
              <ActvtyInd>true</ActvtyInd>
              <Frqcy>DAIL</Frqcy>
              <NetPosId>XXXX</NetPosId>
              <RptDtAndTm>
                <Dt>2019-03-12</Dt>
              </RptDtAndTm>
              <UpdTp>COMP</UpdTp>
            </RptParams>
            <FinInstrmId>
              <OthrId>
                <Id>467174</Id>
                <Tp>
                  <Prtry>8</Prtry>
                </Tp>
              </OthrId>
              <PlcOfListg>
                <MktIdrCd>BVMF</MktIdrCd>
              </PlcOfListg>
            </FinInstrmId>
            <FinInstrmAttrCmon>
              <Asst>WDO</Asst>
              <AsstDesc>Minicontrato de Dólar Comercial</AsstDesc>
              <Mkt>2</Mkt>
              <Sgmt>5</Sgmt>
              <Desc>DOLAR MINI</Desc>
            </FinInstrmAttrCmon>
            <InstrmInf>
              <FutrCtrctsInf>
                <SctyCtgy>0</SctyCtgy>
                <XprtnDt>2019-04-01</XprtnDt>
                <TckrSymb>WDOJ19</TckrSymb>
                <XprtnCd>J19</XprtnCd>
                <TradgStartDt>2016-04-01</TradgStartDt>
                <TradgEndDt>2019-03-29</TradgEndDt>
                <ValTpCd>1</ValTpCd>
                <ISIN>BRBMEFWDO287</ISIN>
                <CFICd>FXXXXX</CFICd>
                <DlvryTp>1</DlvryTp>
                <PmtTp>0</PmtTp>
                <CtrctMltplr>10.000000000</CtrctMltplr>
                <AsstQtnQty>1000.000000000</AsstQtnQty>
                <AllcnRndLot>1</AllcnRndLot>
                <TradgCcy>BRL</TradgCcy>
                <UndrlygInstrmId>
                  <OthrId>
                    <Id>467034</Id>
                    <Tp>
                      <Prtry>8</Prtry>
                    </Tp>
                  </OthrId>
                  <PlcOfListg>
                    <MktIdrCd>BVMF</MktIdrCd>
                  </PlcOfListg>
                </UndrlygInstrmId>
                <WdrwlDays>14</WdrwlDays>
                <WrkgDays>14</WrkgDays>
                <ClnrDays>20</ClnrDays>
              </FutrCtrctsInf>
            </InstrmInf>
          </Instrm>

    <Instrm>
            <RptParams>
              <ActvtyInd>true</ActvtyInd>
              <Frqcy>DAIL</Frqcy>
              <NetPosId>XXXX</NetPosId>
              <RptDtAndTm>
                <Dt>2019-03-12</Dt>
              </RptDtAndTm>
              <UpdTp>COMP</UpdTp>
            </RptParams>
            <FinInstrmId>
              <OthrId>
                <Id>200000235664</Id>
                <Tp>
                  <Prtry>8</Prtry>
                </Tp>
              </OthrId>
              <PlcOfListg>
                <MktIdrCd>BVMF</MktIdrCd>
              </PlcOfListg>
            </FinInstrmId>
            <FinInstrmAttrCmon>
              <Asst>DI1</Asst>
              <AsstDesc>Taxa Média de Depósitos Interfinanceiros de Um Dia</AsstDesc>
              <Mkt>2</Mkt>
              <Sgmt>5</Sgmt>
              <Desc>DI DE 1 DIA</Desc>
            </FinInstrmAttrCmon>
            <InstrmInf>
              <FutrCtrctsInf>
                <SctyCtgy>0</SctyCtgy>
                <XprtnDt>2031-01-02</XprtnDt>
                <TckrSymb>DI1F31</TckrSymb>
                <XprtnCd>F31</XprtnCd>
                <TradgStartDt>2018-12-10</TradgStartDt>
                <TradgEndDt>2030-12-30</TradgEndDt>
                <ValTpCd>0</ValTpCd>
                <BaseCd>252</BaseCd>
                <ConvsCrit>2</ConvsCrit>
                <MtrtyDtTrgtPt>100000</MtrtyDtTrgtPt>
                <ReqrdConvsInd>true</ReqrdConvsInd>
                <ISIN>BRBMEFD1I686</ISIN>
                <CFICd>FFDCSX</CFICd>
                <DlvryTp>1</DlvryTp>
                <PmtTp>0</PmtTp>
                <CtrctMltplr>1.000000000</CtrctMltplr>
                <AsstQtnQty>1.000000000</AsstQtnQty>
                <AllcnRndLot>1</AllcnRndLot>
                <TradgCcy>BRL</TradgCcy>
                <UndrlygInstrmId>
                  <OthrId>
                    <Id>9800334</Id>
                    <Tp>
                      <Prtry>8</Prtry>
                    </Tp>
                  </OthrId>
                  <PlcOfListg>
                    <MktIdrCd>BVMF</MktIdrCd>
                  </PlcOfListg>
                </UndrlygInstrmId>
                <WdrwlDays>2968</WdrwlDays>
                <WrkgDays>2921</WrkgDays>
                <ClnrDays>4314</ClnrDays>
              </FutrCtrctsInf>
            </InstrmInf>
          </Instrm>

    <Instrm>
            <RptParams>
              <ActvtyInd>true</ActvtyInd>
              <Frqcy>DAIL</Frqcy>
              <NetPosId>XXXX</NetPosId>
              <RptDtAndTm>
                <Dt>2019-03-12</Dt>
              </RptDtAndTm>
              <UpdTp>COMP</UpdTp>
            </RptParams>
            <FinInstrmId>
              <OthrId>
                <Id>200000235665</Id>
                <Tp>
                  <Prtry>8</Prtry>
                </Tp>
              </OthrId>
              <PlcOfListg>
                <MktIdrCd>BVMF</MktIdrCd>
              </PlcOfListg>
            </FinInstrmId>
            <FinInstrmAttrCmon>
              <Asst>DDI</Asst>
              <AsstDesc>Cupom Cambial de DI1</AsstDesc>
              <Mkt>2</Mkt>
              <Sgmt>5</Sgmt>
              <Desc>CUPOM CAMBIAL</Desc>
            </FinInstrmAttrCmon>
            <InstrmInf>
              <FutrCtrctsInf>
                <SctyCtgy>0</SctyCtgy>
                <XprtnDt>2031-01-02</XprtnDt>
                <TckrSymb>DDIF31</TckrSymb>
                <XprtnCd>F31</XprtnCd>
                <TradgStartDt>2018-12-10</TradgStartDt>
                <TradgEndDt>2030-12-30</TradgEndDt>
                <ValTpCd>0</ValTpCd>
                <BaseCd>360</BaseCd>
                <ConvsCrit>1</ConvsCrit>
                <MtrtyDtTrgtPt>100000</MtrtyDtTrgtPt>
                <ReqrdConvsInd>true</ReqrdConvsInd>
                <ISIN>BRBMEFDDI693</ISIN>
                <CFICd>FFCCSX</CFICd>
                <DlvryTp>1</DlvryTp>
                <PmtTp>0</PmtTp>
                <CtrctMltplr>0.500000000</CtrctMltplr>
                <AsstQtnQty>1.000000000</AsstQtnQty>
                <AsstSttlmInd>
                  <OthrId>
                    <Id>9800342</Id>
                    <Tp>
                      <Prtry>8</Prtry>
                    </Tp>
                  </OthrId>
                  <PlcOfListg>
                    <MktIdrCd>BVMF</MktIdrCd>
                  </PlcOfListg>
                </AsstSttlmInd>
                <AllcnRndLot>1</AllcnRndLot>
                <TradgCcy>BRL</TradgCcy>
                <UndrlygInstrmId>
                  <OthrId>
                    <Id>9800011</Id>
                    <Tp>
                      <Prtry>8</Prtry>
                    </Tp>
                  </OthrId>
                  <PlcOfListg>
                    <MktIdrCd>BVMF</MktIdrCd>
                  </PlcOfListg>
                </UndrlygInstrmId>
                <WdrwlDays>2968</WdrwlDays>
                <WrkgDays>2921</WrkgDays>
                <ClnrDays>4314</ClnrDays>
              </FutrCtrctsInf>
            </InstrmInf>
          </Instrm>

    <Instrm>
            <RptParams>
              <ActvtyInd>true</ActvtyInd>
              <Frqcy>DAIL</Frqcy>
              <NetPosId>XXXX</NetPosId>
              <RptDtAndTm>
                <Dt>2019-03-12</Dt>
              </RptDtAndTm>
              <UpdTp>COMP</UpdTp>
            </RptParams>
            <FinInstrmId>
              <OthrId>
                <Id>430339</Id>
                <Tp>
                  <Prtry>8</Prtry>
                </Tp>
              </OthrId>
              <PlcOfListg>
                <MktIdrCd>BVMF</MktIdrCd>
              </PlcOfListg>
            </FinInstrmId>
            <FinInstrmAttrCmon>
              <Asst>DAP</Asst>
              <AsstDesc>Cupom de IPCA</AsstDesc>
              <Mkt>2</Mkt>
              <Sgmt>5</Sgmt>
              <Desc>CUPOM DE IPCA</Desc>
            </FinInstrmAttrCmon>
            <InstrmInf>
              <FutrCtrctsInf>
                <SctyCtgy>0</SctyCtgy>
                <XprtnDt>2021-05-17</XprtnDt>
                <TckrSymb>DAPK21</TckrSymb>
                <XprtnCd>K21</XprtnCd>
                <TradgStartDt>2016-02-02</TradgStartDt>
                <TradgEndDt>2021-05-14</TradgEndDt>
                <ValTpCd>0</ValTpCd>
                <BaseCd>252</BaseCd>
                <ConvsCrit>2</ConvsCrit>
                <MtrtyDtTrgtPt>100000</MtrtyDtTrgtPt>
                <ReqrdConvsInd>true</ReqrdConvsInd>
                <ISIN>BRBMEFDAP1K4</ISIN>
                <CFICd>FFFCSX</CFICd>
                <DlvryTp>1</DlvryTp>
                <PmtTp>0</PmtTp>
                <CtrctMltplr>0.000250000</CtrctMltplr>
                <AsstQtnQty>1.000000000</AsstQtnQty>
                <AsstSttlmInd>
                  <OthrId>
                    <Id>10008992</Id>
                    <Tp>
                      <Prtry>8</Prtry>
                    </Tp>
                  </OthrId>
                  <PlcOfListg>
                    <MktIdrCd>BVMF</MktIdrCd>
                  </PlcOfListg>
                </AsstSttlmInd>
                <AllcnRndLot>1</AllcnRndLot>
                <TradgCcy>BRL</TradgCcy>
                <UndrlygInstrmId>
                  <OthrId>
                    <Id>9800003</Id>
                    <Tp>
                      <Prtry>8</Prtry>
                    </Tp>
                  </OthrId>
                  <PlcOfListg>
                    <MktIdrCd>BVMF</MktIdrCd>
                  </PlcOfListg>
                </UndrlygInstrmId>
                <WdrwlDays>548</WdrwlDays>
                <WrkgDays>539</WrkgDays>
                <ClnrDays>797</ClnrDays>
              </FutrCtrctsInf>
            </InstrmInf>
          </Instrm>
*/
