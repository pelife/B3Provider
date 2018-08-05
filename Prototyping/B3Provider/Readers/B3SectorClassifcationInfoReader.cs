#region License
/*
 * B3SectorClassifcationInfoReader.cs
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

namespace B3Provider.Readers
{
    using B3Provider.Records;
    using ExcelDataReader;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class B3SectorClassifcationInfoReader : AbstractReader<B3SectorClassifcationInfo>
    {
        #region "public methods"
        /// <summary>
        /// Method responsible to read records from file of sector information
        /// </summary>
        /// <param name="filePath">File to read records from</param>
        /// <returns>
        /// List of all market sector information found
        /// </returns>
        public override IList<B3SectorClassifcationInfo> ReadRecords(string filePath)
        {
            var listOfClassification = new List<B3SectorClassifcationInfo>();

            var temporaryPath = GetRandomTemporaryDirectory();
            var filesToRead = UnzipFile(filePath, temporaryPath);
            if (filesToRead != null && filesToRead.Length > 0)
            {
                foreach (string oneFileToRead in filesToRead)
                {
                    var classificationInfo = ReadFile(oneFileToRead);
                    listOfClassification.AddRange(classificationInfo);
                }
            }

            DeleteDirectory(temporaryPath);
            return listOfClassification;            
        }
        #endregion

        #region "private methods"
        /// <summary>
        /// Internal private file responsible to read records of company market sector
        /// </summary>
        /// <param name="filePath">File path to read company market sector records from</param>
        /// <returns>
        /// All company market sector found in file
        /// </returns>
        private static IList<B3SectorClassifcationInfo> ReadFile(string filePath)
        {
            var listOfSectorClassification = new List<B3SectorClassifcationInfo>();

            //variables to read data from spreadsheet
            string valorColuna1 = string.Empty;
            string valorColuna2 = string.Empty;
            string valorColuna3 = string.Empty;
            string valorColuna4 = string.Empty;
            string valorColuna5 = string.Empty;

            //variables to define data
            string setorEconomico = string.Empty;
            string subsetor = string.Empty;
            string segmento = string.Empty;
            string empresa = string.Empty;
            string empresaCodigo = string.Empty;
            string empresaSegmento = string.Empty;

            using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
            using (var reader = ExcelReaderFactory.CreateReader(fileStream))
            {
                //https://github.com/ExcelDataReader/ExcelDataReader
                // Choose one of either 1 or 2:
                // 1. Use the reader methods
                do
                {
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
                            //first category
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
                            //change in segment
                            segmento = valorColuna3.Trim();
                        }

                        if (
                            string.IsNullOrEmpty(valorColuna1)
                            && !string.IsNullOrEmpty(valorColuna2)
                            && !string.IsNullOrEmpty(valorColuna3)
                            && string.IsNullOrEmpty(valorColuna4)
                            && string.IsNullOrEmpty(valorColuna5))
                        {
                            //change in subsetor
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
                            empresaSegmento = string.IsNullOrEmpty(valorColuna5) ? string.Empty : valorColuna5.Trim();
                        }

                        if (!string.IsNullOrEmpty(setorEconomico) &&
                                       !string.IsNullOrEmpty(subsetor) &&
                                       !string.IsNullOrEmpty(segmento) &&
                                       !string.IsNullOrEmpty(empresa) &&
                                       !string.IsNullOrEmpty(empresaCodigo))
                        {
                            listOfSectorClassification.Add(new B3SectorClassifcationInfo()
                            {
                                EconomicSector = setorEconomico,
                                EconomicSubSector = subsetor,
                                EconomicSegment = segmento,
                                Company = empresa,
                                CompanyCode = empresaCodigo,
                                CompanySegment = empresaSegmento
                            });

                            empresa = string.Empty;
                            empresaCodigo = string.Empty;
                            empresaSegmento = string.Empty;
                        }
                        // adicionar informação empresa na lista
                    }
                } while (reader.NextResult());

                return listOfSectorClassification;
            }
        }
        #endregion

    }
}
