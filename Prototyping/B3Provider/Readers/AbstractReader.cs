#region License
/*
 * AbstractReader.cs
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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;

    public abstract class AbstractReader<T> : IReader<T>
    {
        #region "common IReader<T> implementation"
        ReadStrategy? _readStrategy = null;

        public ReadStrategy? ReadStrategy
        {
            get
            {
                if (_readStrategy == null)
                    _readStrategy = B3Provider.ReadStrategy.ZipFileReadMostRecent;
                return _readStrategy;
            }
            set { _readStrategy = value; }
        }
        #endregion


        #region "specific IReader<T> implementation"
        public abstract IList<T> ReadRecords(string filePath);
        #endregion

        #region "protected methods"
        protected string[] UnzipFile(string zipFilePath, string destinationPath)
        {
            if (ReadStrategy == B3Provider.ReadStrategy.ZipFileReadAllOverrideRepeated)
                return UnzipAllFiles(zipFilePath, destinationPath);

            var filePath = UnzipLatestFile(zipFilePath, destinationPath);
            return new string[] { filePath };
        }

        protected void DeleteDirectory(string directoryToDelete)
        {
            Directory.Delete(directoryToDelete, true);
        }

        protected string GetRandomTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
        #endregion
        
        #region "private methods"
        private string UnzipLatestFile(string zipFilePath, string destinationPath)
        {
            string destinationFilePath = string.Empty;

            using (ZipArchive zip = ZipFile.Open(zipFilePath, ZipArchiveMode.Read))
            {
                var latestFile = zip.Entries.OrderByDescending(e => e.LastWriteTime).FirstOrDefault();
                if (latestFile != null)
                {   
                    destinationFilePath = Path.GetFullPath(Path.Combine(destinationPath, latestFile.FullName));
                    latestFile.ExtractToFile(destinationFilePath);
                }
            }

            return destinationFilePath;
        }

        private string[] UnzipAllFiles(string zipFilePath, string destinationPath)
        {   
            ZipFile.ExtractToDirectory(zipFilePath, destinationPath);
            return Directory.GetFiles(destinationPath); 
        }
        #endregion

    }
}
