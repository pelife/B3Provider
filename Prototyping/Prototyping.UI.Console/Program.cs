using CenterSpace.NMath.Core;
using Common.Logging;
using Prototyping.Code.Download.MarketData.Bovespa;
using Prototyping.Code.Download.MarketData.TesouroDireto.Intradia;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace Prototyping.UI.Console
{
    class Program
    {
        const string URL_ANDIMA = @"http://www.anbima.com.br/merc_sec/arqs/";

        //static void Main(string[] args)
        //{
        //    System.Console.WriteLine("press <enter> to exit");
        //    System.Console.ReadLine();
        //}

        static ILog _logger = LogManager.GetLogger<Program>();

        static void Main(string[] args)
        {
            //Example2();
            //Example3();
            //Example4();

            //Code.Calc.Runner.ConsolePricer.run();

            //Example3();
            //Example5();
            //Example6();
            var sh = Hash("teste");
            Example7();
            System.Console.WriteLine("press <enter> to exit");
            System.Console.ReadLine();
        }

        static double Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("N0"));
                }

                return double.Parse (sb.ToString());
            }
        }

        public static void Example6()
        {
            TesouroIntradia.Run();
        }

        public static void Example7()
        {
            var classi = new ClassificacaoSetorial();
            classi.DownloadFile();
        }

        public static void Example5()
        {
            var enderecoArquivoCotacao = @"G:\felipe\programming\git\prototyping\source_data\COTAHIST\COTAHIST_A2017\COTAHIST_A2017.TXT";

            //download
            //read
            //save

            //read
            _logger.Info("running example 5");
            using (var stream = new FileInfo(enderecoArquivoCotacao).Open(FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var reader = new COTAHISTReader();
                var resultado = reader.Read(stream);

                var petr = resultado.Registros.Where(x => x.CodigoNegociacao.IndexOf("petr", StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
            }
            _logger.Info("finished running example 5");
            

        }

        public static void Example4()
        {
            //Downloader.Run();
        }

        public static void Example3()
        {
            string enderecoArquivoBDI = @"G:\shared\bdi0713\BDIN";
            _logger.Info("running example 3");
            using (var stream = new FileInfo(enderecoArquivoBDI).Open(FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var reader = new BDIReader();
                var resultado = reader.Read(stream);
                
            }
            _logger.Info("finished running example 3");            
        }


        public static void Example2()
        {
            // Create a RandomNumberGenerator.UniformRandomNumber delegate object
            // from the method System.Random.NextDouble().
            var sysRand = new Random();
            var uniformDeviates = new CenterSpace.NMath.Core.RandomNumberGenerator.UniformRandomNumber(sysRand.NextDouble);

            // Now, construct a binomial random number generator using this delegate to
            // generate uniformly distributed random deviates between 0 and 1 (the 
            // binomial generator will transform these uniform deviates into binomial
            // deviates).
            int trials = 2000;
            double prob = 0.002;
            var binRand = new RandGenBinomial(trials, prob, uniformDeviates);

            double[] rnd = new double[1000];
            double[] rnd2 = new double[1000];
            Func<double> generator = () => { return binRand.NextDouble(); };
            
            rnd = Enumerable
                .Repeat(0, 999)
                .Select(i => generator())
                .ToArray();

            // Change the uniform deviate generator to use the method NextDouble() form
            // NMath Cores RandGenMTwist class.
            int seed = 0x124;

            // Construct the MT generator with the given seed.
            var mt = new RandGenMTwist(seed);
            
            // Create the delegate.
            uniformDeviates = new CenterSpace.NMath.Core.RandomNumberGenerator.UniformRandomNumber(mt.NextDouble);

            // Use the delegate to generate the uniform deviates necessary to for the 
            // binomial generator.
            binRand.UniformDeviateMethod = uniformDeviates;

            rnd2 = Enumerable
                .Repeat(0, 999)
                .Select(i => generator())
                .ToArray();
                    
        }
        public static void Example1()
        {
            DateTime dataDownload;
            dataDownload = DateTime.Today.Subtract(TimeSpan.FromDays(1));

            DownloadFile(string.Concat(URL_ANDIMA, string.Format("m{0:yy}{1}{0:dd}.xls", dataDownload, GetBRMonthName(dataDownload.Month))),
                string.Format("{0}{1}", @"c:\temp\", "merc_sec.xls"));
        }

        private static void DownloadFile(string fileURL, string localPath)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFile_Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadFile_ProgressChanged);
            webClient.DownloadFileAsync(new Uri(fileURL), localPath, localPath);
        }

        private static void DownloadFile_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            System.Console.WriteLine("{0}%", e.ProgressPercentage);
        }

        private static void DownloadFile_Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null) { System.Console.WriteLine("error"); return; }
            System.Console.WriteLine("completed");
            RunMacro();
        }

        private static string GetBRMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "jan";
                case 2:
                    return "fev";
                case 3:
                    return "mar";
                case 4:
                    return "abr";
                case 5:
                    return "mai";
                case 6:
                    return "jun";
                case 7:
                    return "jul";
                case 8:
                    return "ago";
                case 9:
                    return "set";
                case 10:
                    return "out";
                case 11:
                    return "nov";
                case 12:
                    return "dez";
                default:
                    return "jan";
            }
        }

        private static void RunMacro()
        {
            uint processID = 0;
            uint threadID = 0;

            Excel.Application xlApp = null;

            xlApp = SetupExcelApplication();
            string version = xlApp.Version;

            Excel.Workbook xlWorkBook = null;

            threadID = GetWindowThreadProcessId((IntPtr)xlApp.Hwnd, out processID);

            //WindowInterceptor d = new WindowInterceptor((IntPtr)xlApp.Hwnd, ProcessWindow1);

            try
            {
                //~~> Start Excel and open the workbook.
                xlWorkBook = xlApp.Workbooks.Open(@"c:\temp\Book1.xls");

                //~~> Run the macros by supplying the necessary arguments
                xlApp.Run("ThisWorkbook.ShowMsg", "Hello from C# Client", "Demo to run Excel macros from C#");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                if (xlWorkBook != null)
                {
                    //~~> Clean-up: Close the workbook
                    xlWorkBook.Close(false);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlWorkBook);
                    xlWorkBook = null;
                }

                if (xlApp != null)
                {
                    //~~> Clean-up: Close the workbook
                    xlApp.Quit();
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp);
                    xlApp = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //d.Stop();

                KillProcess(processID);
            }
        }

        private static Excel.Application SetupExcelApplication()
        {
            const String LOCAL_ADDIN_PATH = @"C:\Program Files\Microsoft Office\OFFICE11\Library\Analysis\";
            const String STD_ADDIN_STR = @"E:\risco\xla\bonds.xla;E:\risco\xla\Consulta_VAR.xla;E:\risco\xla\Equity.xla;E:\risco\xla\fetch2.xla;E:\risco\xla\lookhistorical.xla;E:\risco\xla\options.xla;E:\risco\xla\lookstock2.xla;F:\Xla2007\series2.xla;E:\risco\xla\liqbonds.xla;E:\risco\xla\PATROL.xla;E:\risco\xla\PATROL_new_version.xla";

            Excel.Application xlApp = null;

            //~~> Define your Excel Objects
            xlApp = new Excel.Application();
            xlApp.DisplayAlerts = false;

            string[] addins = null;

            try
            {
                //  'Carrega os Addins do excel.
                xlApp.RegisterXLL(LOCAL_ADDIN_PATH + "ATPVBAEN.XLA");
                xlApp.AddIns[1].Installed = false;
                xlApp.AddIns[1].Installed = true;

                xlApp.RegisterXLL(LOCAL_ADDIN_PATH + "ANALYS32.XLL");
                xlApp.AddIns[2].Installed = false;
                xlApp.AddIns[2].Installed = true;

                xlApp.RegisterXLL(LOCAL_ADDIN_PATH + "FUNCRES.XLA");
                xlApp.AddIns[3].Installed = false;
                xlApp.AddIns[3].Installed = true;

                xlApp.RegisterXLL(LOCAL_ADDIN_PATH + "PROCDB.XLL");
                xlApp.AddIns[4].Installed = false;
                xlApp.AddIns[4].Installed = true;

                addins = STD_ADDIN_STR.Split(';');

                for (int i = 0; i < addins.Length; i++)
                {
                    xlApp.Workbooks.Open(addins[i], false, true);

                    try { xlApp.Workbooks[System.IO.Path.GetFileName(addins[i])].RunAutoMacros(Excel.XlRunAutoMacro.xlAutoOpen); }
                    catch (Exception) { } //TODO: incluir o log
                }

                for (int i = 0; i < xlApp.Workbooks.Count; i++)
                {
                    if (xlApp.Workbooks[i].Name.ToLower().Contains("book")) { xlApp.Workbooks[i].Close(false); }
                }
            }
            catch (Exception)
            { } //TODO: incluir o log

            return xlApp;
        }

        static void ProcessWindow1(IntPtr hwnd)
        {
            //looking for button "OK" within the intercepted window
            IntPtr h =
                (IntPtr)Win32.Functions.FindWindowEx(hwnd, IntPtr.Zero, "Button", "OK");
            if (h != IntPtr.Zero)
            {
                //clicking the found button
                Win32.Functions.SendMessage
                        ((IntPtr)h, (uint)Win32.Messages.WM_LBUTTONDOWN, 0, 0);
                Win32.Functions.SendMessage
                        ((IntPtr)h, (uint)Win32.Messages.WM_LBUTTONUP, 0, 0);
            }
        }

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        private static void KillProcess(uint ProcessID)
        {
            try
            {
                Process pProcess = Process.GetProcessById((int)ProcessID);
                if (pProcess != null) { pProcess.Kill(); }
            }
            catch { }
        }
    }
}
