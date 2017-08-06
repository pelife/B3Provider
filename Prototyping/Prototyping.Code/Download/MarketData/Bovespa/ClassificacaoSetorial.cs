using ExcelDataReader;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Data;
using System.Linq;

namespace Prototyping.Code.Download.MarketData.Bovespa
{

    public class ClassificacaoSetorial
    {
        const string ENDERECO_ARQUIVO = @"http://www.bmfbovespa.com.br/lumis/portal/file/fileDownload.jsp?fileId=8AA8D0975A2D7918015A3C81693D4CA4";
        const string NOME_ARQUIVO = "classificacao_setorial-{0}.zip";
        public void DownloadFile()
        {
            using (var client = new WebClient())
            {
                // download file
                var temporaryFolder = Path.GetTempPath();
                var filePath = Path.Combine(temporaryFolder, string.Format(NOME_ARQUIVO, DateTime.Now.ToString("yyyy-MM-dd")));
                client.DownloadFile(ENDERECO_ARQUIVO, filePath);

                // uncompress file
                using (ZipArchive zip = ZipFile.Open(filePath, ZipArchiveMode.Read))
                {
                    if (zip.Entries.Count > 0)
                    {
                        var streamArquvoExcel = zip.Entries[0].Open(); // possivel ler sem salvar o arquivo

                        // Auto-detect format, supports:
                        //  - Binary Excel files (2.0-2003 format; *.xls)
                        //  - OpenXml Excel files (2007 format; *.xlsx)
                        using (var reader = ExcelReaderFactory.CreateReader(streamArquvoExcel))
                        {

                            // Choose one of either 1 or 2:

                            // 1. Use the reader methods
                            //do
                            //{
                            //    while (reader.Read())
                            //    {
                            //        // reader.GetDouble(0);
                            //    }
                            //} while (reader.NextResult());

                            // 2. Use the AsDataSet extension method
                            var result = reader.AsDataSet();

                            // The result of each spreadsheet is in result.Tables
                        }
                    }
                }
                
            }

        }

    }
}
