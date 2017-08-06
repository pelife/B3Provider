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
                using (var ms = new MemoryStream())
                {
                    var entry = zip.Entries.First();
                    if (entry != null)
                    {
                        using (var stream = entry.Open())
                        {
                            stream.CopyTo(ms);
                            ms.Position = 0; // rewind
                        }

                        using (var reader = ExcelReaderFactory.CreateReader(ms))
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
                        }
                    }
                }
            }
        }
    }
}
