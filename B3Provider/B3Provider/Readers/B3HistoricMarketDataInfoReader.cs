#region License
/*
 * B3HistoricMarketDataInfoReader.cs
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
    using B3Provider.Layouts;
    using B3Provider.Records;
    using FlatFile.Core;
    using FlatFile.FixedLength;
    using FlatFile.FixedLength.Implementation;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Class responsible for reading all the Historic Market Data info from B3 Historic quotes file
    /// </summary>
    public class B3HistoricMarketDataInfoReader : AbstractReader<B3HistoricMarketDataInfo>
    {
        #region "public methods"
        /// <summary>
        /// Method responsible to read records from file of historic quotes
        /// </summary>
        /// <param name="filePath">File to read records from</param>
        /// <returns>
        /// List of all historic quotes found inf file
        /// </returns>
        public override IList<B3HistoricMarketDataInfo> ReadRecords(string filePath)
        {
            var listOfQuotes = new List<B3HistoricMarketDataInfo>();

            var temporaryPath = GetRandomTemporaryDirectory();
            var filesToRead = UnzipFile(filePath, temporaryPath);
            if (filesToRead != null && filesToRead.Length > 0)
            {
                foreach (string oneFileToRead in filesToRead)
                {
                    var quotesInfo = ReadFile(oneFileToRead);
                    listOfQuotes.AddRange(quotesInfo);
                }
            }

            DeleteDirectory(temporaryPath);
            return listOfQuotes;
        }
        #endregion

        #region "private methods"

        /// <summary>
        /// Internal private file responsible to read records of historic quotes from file
        /// </summary>
        /// <param name="filePath">File path to read histori quotes records from</param>
        /// <returns>
        /// All historic quotes found in the file
        /// </returns>
        private IList<B3HistoricMarketDataInfo> ReadFile(string filePath)
        {
            IList<B3HistoricMarketDataInfo> historicQuote = null;

            using (var stream = new FileInfo(filePath).Open(FileMode.Open, FileAccess.Read, FileShare.Read))
            {   
                var resultado = ReadStream(stream);
                historicQuote = resultado.Records;
            }
            return historicQuote;
        }

        /// <summary>
        /// Internal private file responsible to read records of historic quotes from stream
        /// </summary>
        /// <param name="streamToRead">Stream to read records from</param>
        /// <returns>
        /// All historic quotes found in the stream
        /// </returns>
        private B3HistoricMarketDataContentInfo ReadStream(Stream streamToRead)
        {
            B3HistoricMarketDataContentInfo resultado = null;

            //
            var factory = new FixedLengthFileEngineFactory();

            // If using attribute mapping, pass an array of record types
            // rather than layout instances
            var layouts = new ILayoutDescriptor<IFixedFieldSettingsContainer>[]
            {
                    new B3COTAHISTHeaderLayout()
                    ,new B3COTAHISTHistoricoLayout()
                    ,new B3COTAHISTTrailerLayout()
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
                              return typeof(B3HistoricMarketDataHeaderInfo);
                          case "01":
                              return typeof(B3HistoricMarketDataInfo);
                          case "99":
                              return typeof(B3HistoricMarketDataTrailerInfo);
                      }
                      return null;
                  });

            flatFile.Read(streamToRead);           

            resultado = new B3HistoricMarketDataContentInfo();
            resultado.Header = flatFile.GetRecords<B3HistoricMarketDataHeaderInfo>().FirstOrDefault();
            resultado.Records = flatFile.GetRecords<B3HistoricMarketDataInfo>().ToList();
            resultado.Trailer = flatFile.GetRecords<B3HistoricMarketDataTrailerInfo>().FirstOrDefault();

            return resultado;
        }
        #endregion
    }
}
