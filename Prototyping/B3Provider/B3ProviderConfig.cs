#region License
/*
 * B3ProviderConfig.cs
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

/// <summary>
/// Default namespace of the package
/// </summary>
namespace B3Provider
{
    using System;
    using System.IO;

    /// <summary>
    /// Class that include all properties required to run the B3ProviderClient
    /// for aplications to use it.
    /// </summary>
    public class B3ProviderConfig
    {
        #region "properties"
        /// <summary>
        /// Base path to save all files including (database, download data, logs, etc)
        /// if not defined, it automatically sets it to 
        /// Environment.SpecialFolder.CommonApplicationData\B3Provider
        /// </summary>
        public string BasePath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "B3Provider");

        public string DownloadPathSufix { get; set; } = "downloads";
        public string DatabasePathSufix { get; set; } = "database";

        /// <summary>
        /// Path to where the files from B3 will be downloaded to
        /// if not defined, it automatically sets it to 
        /// Environment.SpecialFolder.CommonApplicationData\B3Provider\downloads
        /// </summary>
        public string DownloadPath { get { return Path.Combine(BasePath, DownloadPathSufix); } } 

        /// <summary>
        /// Path to where the files from B3 will be stored into database
        /// if not defined, it automatically sets it to 
        /// Environment.SpecialFolder.CommonApplicationData\B3Provider\database
        /// </summary>
        public string DatabasePath { get { return Path.Combine(BasePath, DatabasePathSufix); } }

        /// <summary>
        /// true to replace existing files if necessary otherwise false
        /// </summary>
        public bool ReplaceExistingFiles { get; set; }

        /// <summary>
        /// Reading strategy to load files
        /// </summary>
        public ReadStrategy ReadStrategy { get; set; } = ReadStrategy.ZipFileReadMostRecent;
        #endregion

    }
}
