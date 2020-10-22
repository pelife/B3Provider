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
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Reflection;
    using ZipFile = System.IO.Compression.ZipFile;

    /// <summary>
    /// Class that implement common methods to all readers to inherit from
    /// </summary>
    /// <typeparam name="T">Type os the register the Readers is supposed to return</typeparam>
    public abstract class AbstractReader<T> : IReader<T>
    {
        #region "constantes"
        const char CR = '\r';
        const char LF = '\n';
        const char NULL = (char)0;
        #endregion

        #region "common IReader<T> implementation"
        /// <summary>
        /// Strategy to read files, what should the system do in case there are many files or.
        /// </summary>
        public ReadStrategy ReadStrategy { get; set; } = ReadStrategy.ZipFileReadMostRecent;
        #endregion

        #region "specific IReader<T> implementation"
        /// <summary>
        /// Abstract method that every reader must implement in order to function properly
        /// </summary>
        /// <param name="filePath">file path to read registers from</param>
        /// <returns>
        /// A collection of all registers found in the files
        /// </returns>
        public abstract IList<T> ReadRecords(string filePath);
        #endregion

        #region "protected methods"
        /// <summary>
        /// Unzip files contained in a zip file 
        /// </summary>
        /// <param name="zipFilePath">zipfile to extract files from</param>
        /// <param name="destinationPath">destination path to extract files to</param>
        /// <returns>
        /// a list of paths to all files extracted from the zip file
        /// </returns>
        protected string[] UnzipFile(string zipFilePath, string destinationPath)
        {
            if (ReadStrategy == B3Provider.ReadStrategy.ZipFileReadAllOverrideRepeated)
            {
                return UnzipAllFiles(zipFilePath, destinationPath);
            }

            var filePath = UnzipLatestFile(zipFilePath, destinationPath);
            return new string[] { filePath };
        }

        /// <summary>
        /// Delete one directory with all its content
        /// </summary>
        /// <param name="directoryToDelete">path to the directory to delete</param>
        protected void DeleteDirectory(string directoryToDelete)
        {
            Directory.Delete(directoryToDelete, true);
        }

        /// <summary>
        /// Create a random directory in the system temporary foolder to use it while reading files
        /// </summary>
        /// <returns>
        /// A random created directory within the system temporary folder
        /// </returns>
        protected string GetRandomTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        /// <summary>
        /// Returns the number of lines in the given <paramref name="stream"/>.
        /// </summary>        
        public long CountLines(Stream stream)
        {
            //http://www.nimaara.com/2018/03/20/counting-lines-of-a-text-file/
            //https://github.com/NimaAra/Easy.Common/blob/master/Easy.Common/Extensions/StreamExtensions.cs

            var lineCount = 0L;
            var byteBuffer = new byte[1024 * 1024];
            var detectedEOL = NULL;
            var currentChar = NULL;

            int bytesRead;
            while ((bytesRead = stream.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
            {
                for (var i = 0; i < bytesRead; i++)
                {
                    currentChar = (char)byteBuffer[i];

                    if (detectedEOL != NULL)
                    {
                        if (currentChar == detectedEOL)
                        {
                            lineCount++;
                        }
                    }
                    else if (currentChar == LF || currentChar == CR)
                    {
                        detectedEOL = currentChar;
                        lineCount++;
                    }
                }
            }

            if (currentChar != LF && currentChar != CR && currentChar != NULL)
            {
                lineCount++;
            }

            return lineCount;
        }
        #endregion

        #region "private methods"
        /// <summary>
        /// Method that will extract from file only the most recent file.
        /// </summary>
        /// <param name="zipFilePath">zip file to extract files from</param>
        /// <param name="destinationPath">destination path to extract files to</param>
        /// <returns>
        /// returns the file path of the most recent file extracted from zip.
        /// </returns>
        private string UnzipLatestFile(string zipFilePath, string destinationPath)
        {
            string destinationFilePath = string.Empty;

            using (ZipArchive zip = new ZipArchive(File.OpenRead(zipFilePath), ZipArchiveMode.Read))
            {
                if (zip.Entries != null && zip.Entries.Count > 0)
                {
                    var latestFile = zip.Entries.OrderByDescending(e => e.LastWriteTime).FirstOrDefault();
                    destinationFilePath = Path.GetFullPath(Path.Combine(destinationPath, latestFile.FullName));
                    latestFile.ExtractToFile(destinationFilePath);

                    if (zip.Entries.Count == 1 && Path.GetExtension(latestFile.Name).Equals(".zip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        using (ZipArchive zipInterno = new ZipArchive(File.OpenRead(destinationFilePath), ZipArchiveMode.Read))
                        {
                            latestFile = zipInterno.Entries.OrderByDescending(e => e.LastWriteTime).FirstOrDefault();
                            destinationFilePath = Path.GetFullPath(Path.Combine(destinationPath, latestFile.FullName));
                            latestFile.ExtractToFile(destinationFilePath);
                        }
                    }
                }
            }
            return destinationFilePath;
        }

        /// <summary>
        /// Method that will extract from file all files.
        /// </summary>
        /// <param name="zipFilePath">zip file to extract files from</param>
        /// <param name="destinationPath">destination path to extract files to</param>
        /// <returns>
        /// returns the file path of all the files extracted from zip.
        /// </returns>
        private string[] UnzipAllFiles(string zipFilePath, string destinationPath)
        {
            ZipFile.ExtractToDirectory(zipFilePath, destinationPath);
            return Directory.GetFiles(destinationPath);
        }
        #endregion

    }
}
