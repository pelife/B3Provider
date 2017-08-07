using ExcelDataReader;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Data;
using System.Linq;
using Prototyping.Code.Utils;
using System.Collections.Generic;

namespace Prototyping.Code.Download.MarketData.Bovespa
{

    public class ClassificacaoSetorial
    {
        const string ENDERECO_ARQUIVO = @"http://www.bmfbovespa.com.br/lumis/portal/file/fileDownload.jsp?fileId=8AA8D0975A2D7918015A3C81693D4CA4";
        const string NOME_ARQUIVO = "classificacao_setorial-{0}.zip";

        const string HEADER_TEXTO_1 = "SETOR ECONÔMICO";
        const string HEADER_TEXTO_2 = "SUBSETOR";
        const string HEADER_TEXTO_3 = "SEGMENTO";

        public void DownloadFile()
        {
            string valorColuna1 = string.Empty;
            string valorColuna2 = string.Empty;
            string valorColuna3 = string.Empty;
            string valorColuna4 = string.Empty;
            string valorColuna5 = string.Empty;

            string setorEconomico = string.Empty;
            string subsetor = string.Empty;
            string segmento = string.Empty;
            string empresa = string.Empty;
            string empresaCodigo = string.Empty;
            string empresaSegmento = string.Empty;

            List<EmpresaSetorInfo> empresas = null;           

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
                            // 1. Use the reader methods
                            do
                            {
                                empresas = new List<EmpresaSetorInfo>();
                                while (reader.Read())
                                {
                                    valorColuna1 = reader.GetString(0);
                                    valorColuna2 = reader.GetString(1);
                                    valorColuna3 = reader.GetString(2);
                                    valorColuna4 = reader.GetString(3);
                                    valorColuna5 = reader.GetString(4);

                                    if ( 
                                        !string.IsNullOrEmpty(valorColuna1) 
                                        && !string.IsNullOrEmpty(valorColuna2) 
                                        && !string.IsNullOrEmpty(valorColuna3)
                                        && string.IsNullOrEmpty(valorColuna4) 
                                        && string.IsNullOrEmpty(valorColuna5))
                                    {
                                        //primeira categoria
                                        setorEconomico = valorColuna1.Trim();
                                        subsetor = valorColuna2.Trim();
                                        segmento = valorColuna3.Trim();
                                    }

                                    if (
                                        string.IsNullOrEmpty(valorColuna1)
                                        && string.IsNullOrEmpty(valorColuna2)
                                        && !string.IsNullOrEmpty(valorColuna3)
                                        && string.IsNullOrEmpty(valorColuna4)
                                        && string.IsNullOrEmpty(valorColuna5))
                                    {
                                        //trocou segmento
                                        segmento = valorColuna3.Trim();
                                    }

                                   if (
                                        string.IsNullOrEmpty(valorColuna1)
                                        && !string.IsNullOrEmpty(valorColuna2)
                                        && !string.IsNullOrEmpty(valorColuna3)
                                        && string.IsNullOrEmpty(valorColuna4)
                                        && string.IsNullOrEmpty(valorColuna5))
                                    {
                                        //trocou subsetor
                                        subsetor = valorColuna2.Trim();
                                        segmento = valorColuna3.Trim();
                                    }

                                    if (
                                        string.IsNullOrEmpty(valorColuna1)
                                        && string.IsNullOrEmpty(valorColuna2)
                                        && !string.IsNullOrEmpty(valorColuna3)
                                        && !string.IsNullOrEmpty(valorColuna4))
                                    {
                                        //empresa
                                        empresa = valorColuna3.Trim();
                                        empresaCodigo = valorColuna4.Trim();
                                        empresaSegmento = valorColuna5.Trim();
                                    }

                                    if (!string.IsNullOrEmpty(setorEconomico) &&
                                        !string.IsNullOrEmpty(subsetor) &&
                                        !string.IsNullOrEmpty(segmento) &&
                                        !string.IsNullOrEmpty(empresa) &&
                                        !string.IsNullOrEmpty(empresaCodigo))
                                    {
                                        empresas.Add(new EmpresaSetorInfo()
                                        {
                                            SetorEconomico = setorEconomico,
                                            SubSetorEconomico = subsetor,
                                            SegmentoEconomico = segmento,
                                            Empresa = empresa,
                                            Codigo = empresaCodigo,
                                            Segmento = empresaSegmento
                                        });

                                        empresa = string.Empty;
                                        empresaCodigo = string.Empty;
                                        empresaSegmento = string.Empty;
                                    }
                                    // adicionar informação empresa na lista
                                }
                            } while (reader.NextResult());
                        }
                    }
                }
            }
        }


    }

    public class EmpresaSetorInfo
    {
       public string SetorEconomico { get; set; }
        public string SubSetorEconomico { get; set; }
        public string SegmentoEconomico { get; set; }
        public string Empresa { get; set; }
        public string Codigo { get; set; }
        public string Segmento { get; set; }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}-{3}-{4}-{5}", SetorEconomico, SubSetorEconomico, SegmentoEconomico, Empresa, Codigo, Segmento);
        }
    }
}
