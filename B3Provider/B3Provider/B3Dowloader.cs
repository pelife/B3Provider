#region License
/*
 * B3Dowloader.cs
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
    using B3Provider.Utils;
    using NLog;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;

    /// <summary>
    /// Class responsable to download files from B3 FTP and HTTP website.
    /// </summary>
    public class B3Dowloader : IB3Downloader
    {
        #region "ctor"
        /// <summary>
        /// Default constructor with the download path parameter
        /// </summary>
        /// <param name="downloadPath">Dowload path</param>
        public B3Dowloader(string downloadPath)
        {
            DownloadPath = downloadPath;
        }
        #endregion

        #region "private variables"
        private WebClient _webClient = null; // Our WebClient that will be doing the downloading for us
        private Stopwatch _sw = null;        // The stopwatch which we will be using to calculate the download speed
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region "properties"
        /// <summary>
        /// File Path to download files to
        /// </summary>
        public string DownloadPath { get; set; }

        public Logger Logger => _logger;
        #endregion

        #region "IB3Downloader methods"
        /// <summary>
        /// Method that downloads from the FTP site the file containing the Index File BVBG.087.01
        /// </summary>
        /// <param name="dateToDownload">Date of reference to download the file</param>
        /// <param name="replaceIfExists">True to replace the file in case it already exists</param>
        /// <returns>
        /// file path where the file was saved to.
        /// </returns>
        public string DownloadIndexFile(DateTime? dateToDownload, bool replaceIfExists)
        {
            var dateDowload = DefineDateIfNotDefined(dateToDownload);
            var fileName = string.Format("IR{0:yyMMdd}.zip", dateDowload);
            //var fileDirectory = "IPN/GPS/BVBG.087.01/";

            //return DownloadFTPFile(fileDirectory, fileName, string.Empty, replaceIfExists);

            return DownloadHTTPFile(@"http://www.b3.com.br/pesquisapregao/download?filelist=", fileName, string.Empty, replaceIfExists);        
        }

        /// <summary>
        /// Method that downloads from the FTP site the file containing the Indicators File BVBG.029.02
        /// </summary>
        /// <param name="dateToDownload">Date of reference to download the file</param>
        /// <param name="replaceIfExists">True to replace the file in case it already exists</param>
        /// <returns>
        /// file path where the file was saved to.
        /// </returns>
        public string DownloadIndicatorFile(DateTime? dateToDownload, bool replaceIfExists)
        {
            var dateDowload = DefineDateIfNotDefined(dateToDownload);
            var fileName = string.Format("II{0:yyMMdd}.zip", dateDowload);
            //var fileDirectory = "IPN/TS/BVBG.029.02/";

            //return DownloadFTPFile(fileDirectory, fileName, string.Empty, replaceIfExists);
            return DownloadHTTPFile(@"http://www.b3.com.br/pesquisapregao/download?filelist=", fileName, string.Empty, replaceIfExists);
        }

        /// <summary>
        /// Method that downloads from the FTP site the file containing the Instruments File BVBG.028.02
        /// </summary>
        /// <param name="dateToDownload">Date of reference to download the file</param>
        /// <param name="replaceIfExists">True to replace the file in case it already exists</param>
        /// <returns>
        /// file path where the file was saved to.
        /// </returns>
        public string DownloadInstrumentFile(DateTime? dateToDownload, bool replaceIfExists)
        {
            var dateDowload = DefineDateIfNotDefined(dateToDownload);
            var fileName = string.Format("IN{0:yyMMdd}.zip", dateDowload);
            //var fileDirectory = "IPN/TS/BVBG.028.02/";

            //return DownloadFTPFile(fileDirectory, fileName, string.Empty, replaceIfExists);
            return DownloadHTTPFile(@"http://www.b3.com.br/pesquisapregao/download?filelist=", fileName, string.Empty, replaceIfExists);
        }

        /// <summary>
        /// Method that downloads from the FTP site the file containing the Quotes File BVBG.086.01
        /// </summary>
        /// <param name="dateToDownload">Date of reference to download the file</param>
        /// <param name="replaceIfExists">True to replace the file in case it already exists</param>
        /// <returns>
        /// file path where the file was saved to.
        /// </returns>
        public string DownloadQuoteFile(DateTime? dateToDownload, bool replaceIfExists)
        {
            var dateDowload = DefineDateIfNotDefined(dateToDownload);
            var fileName = string.Format("PR{0:yyMMdd}.zip", dateDowload);
            //var fileDirectory = "IPN/TRS/BVBG.086.01/";

            //return DownloadFTPFile(fileDirectory, fileName, string.Empty, replaceIfExists);
            return DownloadHTTPFile(@"http://www.b3.com.br/pesquisapregao/download?filelist=", fileName, string.Empty, replaceIfExists);
        }

        /// <summary>
        /// Method that downloads from the HTTP site the file containing the Historic Quotes File COTAHIST
        /// </summary>
        /// <param name="yearToDownload">year that one wants to download the complete history of prices</param>
        /// <param name="replaceIfExists">True to replace the file in case it already exists</param>
        /// <returns>
        /// file path where the file was saved to.
        /// </returns>        
        public string DownloadYearHistoricFile(int yearToDownload, bool replaceIfExists)
        {
            var fileName = string.Format("COTAHIST_A{0}.ZIP", yearToDownload);
            return DownloadHTTPFile(@"http://bvmf.bmfbovespa.com.br/InstDados/SerHist/", fileName, string.Empty, replaceIfExists);
        }

        /// <summary>
        /// Method to download file containing sector classification of B3 companies
        /// </summary>
        /// <param name="replaceIfExists">if the files exists it will be replaced</param>
        /// <returns>
        /// Path where the downloader saved the file to
        /// </returns>
        public string DownloadSectorClassificationFile(bool replaceIfExists)
        {
            var destinationPath = string.Format("classificacao_setorial-{0}.zip", DateTime.Now.ToString("yyyy-MM-dd"));
            //return DownloadHTTPFile(@"http://www.bmfbovespa.com.br/lumis/portal/file/fileDownload.jsp?fileId=8AA8D0975A2D7918015A3C81693D4CA4", string.Empty, destinationPath, replaceIfExists);
            return DownloadHTTPFile(@"http://www.b3.com.br/lumis/portal/file/fileDownload.jsp?fileId=8AA8D0975A2D7918015A3C81693D4CA4", string.Empty, destinationPath, replaceIfExists);

        }
        #endregion

        #region "private methods"
        /// <summary>
        /// Define which date to download if not already defined
        /// </summary>
        /// <param name="dateToDownload">reference date to download the file</param>
        /// <returns>
        /// The correct date to download file if not defined
        /// </returns>
        private DateTime DefineDateIfNotDefined(DateTime? dateToDownload)
        {
            DateTime? value;
            if (dateToDownload.HasValue)
            {
                value = dateToDownload.Value;
            }

            if (DateTime.Now.Hour >= 17)
            {
                value = DateTime.Today;
            }
            else
            {
                value = DateTime.Today.Subtract(TimeSpan.FromDays(1));
            }

            //TODO: pass the collection of hollydays
            return value.CurrentOrPreviousBusinessDay(null);
        }
        #endregion

        #region "download files from http"
        /// <summary>
        /// Method to download files from http site
        /// </summary>
        /// <param name="httpURL">site to download file from</param>
        /// <param name="httpFileName">file to download</param>
        /// <param name="destinationFileName">destination fileName (Optional)</param>
        /// <param name="replaceIfExists">replace if file already exists</param>
        /// <returns>
        /// string with path to whe the file was saved
        /// </returns>
        private string DownloadHTTPFile(string httpURL, string httpFileName, string destinationFileName, bool replaceIfExists)
        {
            var destinationFileDownload = string.Concat(httpURL, httpFileName);
            var webRequest = (HttpWebRequest)WebRequest.Create(destinationFileDownload);
            webRequest.Method = "GET";
            webRequest.Timeout = 3000;

            if (string.IsNullOrEmpty(destinationFileName))
            {
                destinationFileName = httpFileName; // filename will be the same as directory;
            }

            var detinationPath = Path.Combine(DownloadPath, destinationFileName);
            if (!replaceIfExists && File.Exists(detinationPath)) //not to be replaced and already exists
            {
                return detinationPath;
            }

            try
            {
                var webResponse = webRequest.GetResponse();
                using (Stream fileStream = new FileStream(detinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    //Download the File.
                    webResponse.GetResponseStream().CopyTo(fileStream);
                }
            }
            catch (IOException ex)
            {
                _logger.Error(ex, "Generic Exception");
                detinationPath = string.Empty;
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Generic Exception");
                detinationPath = string.Empty;
                throw;
            }

            return detinationPath;
        }
        
        private string DownloadHTTPFile1(string httpURL, string httpFileName, string destinationFileName, bool replaceIfExists)
        {
            var destinationFileDownload = string.Concat(httpURL, httpFileName);
            var detinationPath = string.Empty;


            if (string.IsNullOrEmpty(destinationFileName))
            {
                destinationFileName = httpFileName; // filename will be the same as directory;
            }
            detinationPath = Path.Combine(DownloadPath, destinationFileName);

            if (!replaceIfExists && File.Exists(detinationPath)) //not to be replaced and already exists
            {
                return detinationPath;
            }

            using (_webClient = new WebClient())
            {
                _webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
                _webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;                
                // The variable that will be holding the url address (making sure it starts with http://)
                Uri URL = destinationFileDownload.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ? new Uri(destinationFileDownload) : new Uri("http://" + destinationFileDownload);

                // Start the stopwatch which we will be using to calculate the download speed
                _sw = Stopwatch.StartNew();

                try
                {
                    // Start downloading the file
                    _webClient.DownloadFileAsync(URL, detinationPath);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Generic Exception");
                }
            }

            return detinationPath;
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Calculate download speed and output it to labelSpeed.
            //labelSpeed.Text = string.Format("{0} kb/s", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00"));
            var speed = (e.BytesReceived / 1024d / _sw.Elapsed.TotalSeconds);
            var percentage = e.ProgressPercentage;
            var downloaded = (e.BytesReceived / 1024d / 1024d);
            var downloadTotal = (e.TotalBytesToReceive / 1024d / 1024d);

            var message = string.Format("{0} kb/s | {1} % | {2} MB's/{3} MB's", speed.ToString("0.00"), percentage, downloaded.ToString("0.00"), downloadTotal.ToString("0.00"));

            _logger.Info(message);
            System.Diagnostics.Trace.WriteLine(message);
            // Update the progressbar percentage only when the value is not the same.
            //progressBar.Value = e.ProgressPercentage;

            // Show the percentage on our label.
            //labelPerc.Text = e.ProgressPercentage.ToString() + "%";

            // Update the label with how much data have been downloaded so far and the total size of the file we are currently downloading
            //labelDownloaded.Text = string.Format("{0} MB's / {1} MB's",
            //    (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
            //    (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
        }

        private void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            // Reset the stopwatch.
            //_sw.Reset();
            _sw.Stop();

            if (e.Error != null)
            {
                _logger.Error(e.Error,  "Download has been fault.");
            }

            if (e.Cancelled == true)
            {
                _logger.Info("Download has been canceled.");
            }
            else
            {
                _logger.Info("Download completed!");
            }
        }

        #endregion

        #region "download files from ftp"
        /// <summary>
        /// Method to download files from ftp site
        /// </summary>
        /// <param name="ftpDirectory">Directory to download files from</param>
        /// <param name="ftpFileName">File name</param>
        /// <param name="destinationFileName">destinationFileName</param>
        /// <param name="replaceIfExists">true if one wants to replace if file already exists</param>
        /// <returns>
        /// Path to the saved file.
        /// </returns>
        private string DownloadFTPFile(string ftpDirectory, string ftpFileName, string destinationFileName, bool replaceIfExists)
        {
            var destinationFileDownload = string.Concat(@"ftp://ftp.bmf.com.br/", ftpDirectory, ftpFileName);
            var ftpRequest = (FtpWebRequest)WebRequest.Create(destinationFileDownload);
            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            ftpRequest.UseBinary = true;

            if (string.IsNullOrEmpty(destinationFileName))
            {
                destinationFileName = ftpFileName; // filename will be the same as directory;
            }

            var detinationPath = Path.Combine(DownloadPath, destinationFileName);
            if (!replaceIfExists && File.Exists(detinationPath)) //not to be replaced and already exists
            {
                return detinationPath;
            }

            var fileSize = GetFileSize(ftpDirectory, ftpFileName);
            var fileDate = GetFileDateTime(ftpDirectory, ftpFileName);

            try
            {
                var response = (FtpWebResponse)ftpRequest.GetResponse();
                using (Stream fileStream = new FileStream(detinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    //Download the File.
                    response.GetResponseStream().CopyTo(fileStream);
                }
            }
            catch (Exception)
            {
                detinationPath = string.Empty;
                throw;
            }

            return detinationPath;
        }
       
        /// <summary>
        /// Method that query size of the file to the server
        /// </summary>
        /// <param name="ftpDirectory">Directory of the ftp size to look</param>
        /// <param name="ftpFileName">Filename to calculate size for</param>
        /// <returns>
        /// Size of the file.
        /// </returns>
        private long GetFileSize(string ftpDirectory, string ftpFileName)
        {
            long size = 0;
            var destinationFileDownload = string.Concat(@"ftp://ftp.bmf.com.br/", ftpDirectory, ftpFileName);
            var ftpRequest = (FtpWebRequest)WebRequest.Create(destinationFileDownload);
            ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
            ftpRequest.UseBinary = true;

            var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            size = ftpResponse.ContentLength;
            ftpResponse.Close();

            return size;
        }

        /// <summary>
        /// Method that query size of the file to the server
        /// </summary>
        /// <param name="ftpDirectory">Directory of the ftp date to look</param>
        /// <param name="ftpFileName">Filename to dicover date for</param>
        /// <returns>
        /// Datetime of the file
        /// </returns>
        private DateTime GetFileDateTime(string ftpDirectory, string ftpFileName)
        {
            DateTime modified = DateTime.MinValue;
            var destinationFileDownload = string.Concat(@"ftp://ftp.bmf.com.br/", ftpDirectory, ftpFileName);
            var ftpRequest = (FtpWebRequest)WebRequest.Create(destinationFileDownload);
            ftpRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            ftpRequest.UseBinary = true;

            var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            modified = ftpResponse.LastModified;
            ftpResponse.Close();

            return modified;
        }

        #endregion
    }
}