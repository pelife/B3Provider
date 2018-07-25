#region License
/*
 * IB3Downloader.cs
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
    using System;    
    
    /// <summary>
    /// Interface that define the common methods to diferent types of downloaders
    /// </summary>
    interface IB3Downloader
    {
        /// <summary>
        /// Method to download file containing instruments
        /// </summary>
        /// <param name="dateToDownload">date reference of the file optional, if not provided classes must created acording to their rules</param>
        /// <param name="replaceIfExists">if the files exists it will be replaced</param>
        /// <returns>
        /// Path where the downloader saved the file to
        /// </returns>
        string DownloadInstrumentFile(DateTime? dateToDownload, bool replaceIfExists);

        /// <summary>
        /// Method to download file containing indicators
        /// </summary>
        /// <param name="dateToDownload">date reference of the file optional, if not provided classes must created acording to their rules</param>
        /// <param name="replaceIfExists">if the files exists it will be replaced</param>
        /// <returns>
        /// Path where the downloader saved the file to
        /// </returns>
        string DownloadIndicatorFile(DateTime? dateToDownload, bool replaceIfExists);

        /// <summary>
        /// Method to download file containing quotes
        /// </summary>
        /// <param name="dateToDownload">date reference of the file optional, if not provided classes must created acording to their rules</param>
        /// <returns>
        /// Path where the downloader saved the file to
        /// </returns>
        string DownloadQuoteFile(DateTime? dateToDownload, bool replaceIfExists);

        /// <summary>
        /// Method to download file containing indicators
        /// </summary>
        /// <param name="dateToDownload">date reference of the file optional, if not provided classes must created acording to their rules</param>
        /// <param name="replaceIfExists">if the files exists it will be replaced</param>
        /// <returns>
        /// Path where the downloader saved the file to
        /// </returns>
        string DownloadIndexFile(DateTime? dateToDownload, bool replaceIfExists);

        /// <summary>
        /// Method to download file containing quotes for a whole year
        /// </summary>
        /// <param name="yearToDownload">year to download the file containing quotes</param>
        /// <returns>
        /// Path where the downloader saved the file to
        /// </returns>
        string DownloadYearHistoricFile(int yearToDownload, bool replaceIfExists);
    }
}
