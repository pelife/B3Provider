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
    using B3Provider.Util;
    using System;
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

        #region "properties"
        /// <summary>
        /// File Path to download files to
        /// </summary>
        public string DownloadPath { get; set; }
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
            var fileDirectory = "IPN/GPS/BVBG.087.01/";

            return DownloadFTPFile(fileDirectory, fileName, string.Empty, replaceIfExists);
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
            var fileDirectory = "IPN/TS/BVBG.029.02/";

            return DownloadFTPFile(fileDirectory, fileName, string.Empty, replaceIfExists);            
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
            var fileDirectory = "IPN/TS/BVBG.028.02/";

            return DownloadFTPFile(fileDirectory, fileName, string.Empty, replaceIfExists);
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
            var fileDirectory = "IPN/TRS/BVBG.086.01/";

            return DownloadFTPFile(fileDirectory, fileName, string.Empty, replaceIfExists);
        }

        /// <summary>
        /// Method that downloads from the FTP site the file containing the Historic Quotes File COTAHIST
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
            if (dateToDownload.HasValue)
                return dateToDownload.Value;
            if (DateTime.Now.Hour >= 17)
                return DateTime.Today;

            return B3DateUtils.PreviousWorkDate(dateToDownload);
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
                destinationFileName = httpFileName; // filename will be the same as directory;

            var detinationPath = Path.Combine(DownloadPath, destinationFileName);
            if (!replaceIfExists && File.Exists(detinationPath)) //not to be replaced and already exists
                return detinationPath;

            try
            {
                var webResponse = webRequest.GetResponse();
                using (Stream fileStream = new FileStream(detinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    //Download the File.
                    webResponse.GetResponseStream().CopyTo(fileStream);
                }
            }
            catch (Exception)
            {

                detinationPath = string.Empty;
                throw;
            }

            return detinationPath;
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
                destinationFileName = ftpFileName; // filename will be the same as directory;

            var detinationPath = Path.Combine(DownloadPath, destinationFileName);
            if (!replaceIfExists && File.Exists(detinationPath)) //not to be replaced and already exists
                return detinationPath;

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