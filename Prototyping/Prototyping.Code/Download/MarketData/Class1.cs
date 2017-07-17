using System;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Net.Http;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;

namespace Prototyping.Code.Download.MarketData
{
    public class Downloader
    {
        static Dictionary<string, string> _raw;
        static Dictionary<string, string> _lean;
        static string[] _tickers;
        static readonly CultureInfo _ptBR = CultureInfo.CreateSpecificCulture("pt-BR");
        static readonly CultureInfo _enUS = CultureInfo.CreateSpecificCulture("en-US");
        static readonly string _root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        #region TickChange Dictionary
        static Dictionary<string, string> TickerChange = new Dictionary<string, string>()
            {   // NEW      OLD 
                {"ABCB4", "ABCB11"}, //
                {"ABEV3", "AMBV3" }, // AMBV3 AMBV4
                {"ABRE3", "ABRE11"}, //
                {"AEDU3", "AEDU11"}, //
                {"AGEI3", "AGIN3" }, // ABYA3 KSSA3
                {"AMAR3", "MARI3" }, //
                {"ARCE3", "BELG3" }, //
                {"ARTR3", "OHLB3" }, //
                {"BHGR3", "IVTT3" }, //
                {"BICB4", "BICB11"}, //
                {"BPAN4", "BPNM4" }, //
                {"BRFS3", "PRGA3" }, //
                {"BRKM3", "CPNE3" },
                {"BRKM5", "CPNE5" }, //
                {"BRKM6", "CPNE6" }, //
                {"BRSR6", "BRSR11"}, //
                {"BRTO3", "TEPR3" },
                {"BRTO4", "TEPR4" }, //
                {"BRTP3", "TCSP3" }, //
                {"BRTP4", "TCSP4" }, //
                {"BTOW3", "SUBA3" }, //
                {"BVMF3", "BMEF3" }, // BOVH3
                {"CESP5", "CESP4" }, //
                {"CGAS5", "CGAS4" }, //
                {"CIEL3", "VNET3" }, //
                {"CLSC4", "CLSC6" }, //
                {"DAGB11", "DUFB11"}, //
                {"DTEX3", "DURA3" }, // DURA4 SATI3
                {"EMBR4", "EMBR5" }, //
                {"ENEV3", "MPXE3" }, //
                {"ENGI3", "FLCL3" }, //
                {"ENGI4", "FLCL5" }, //
                {"EQTL3", "EQTL11"}, //
                {"ESTC3", "ESTC11"}, //
                {"FRAS3", "STED3" },
                {"FRAS4", "STED4" },
                {"FIBR3", "VCPA3" }, //
                {"GGBR3", "COGU3" },
                {"GGBR4", "COGU4" }, //
                {"HGCR11", "CSBC11"}, //
                {"IBAN5", "IBAN4" },
                {"ITUB3", "ITAU3" }, //
                {"ITUB4", "ITAU4" }, //
                {"KLBN3", "KLAB3" },
                {"KLBN4", "KLAB4" }, //
                {"LIGT3", "LIGH3" }, //
                {"MAGG3", "MAGS3" }, // MAGS5 MAGS7
                {"NETC3", "PLIM3" },
                {"NETC4", "PLIM4" }, //
                {"OIBR3", "BRTO3" }, //
                {"OIBR4", "BRTO4" }, //
                {"PALF5", "PALF4" },
                {"PLAS4", "OSAO4" }, //
                {"PLAS3", "PLAS4" }, //
                {"PMET6", "BCAL6" },
                {"PRBC4", "PRBC11"}, //
                {"PRIO3", "HRTP3" },
                {"PRML3", "LLXL3" },
                {"RADL3", "DROG3" }, // RAIA3
                {"REDE3", "ELCA3" },
                {"REDE4", "ELCA4" },
                {"RUMO3", "ALLL3" },
                {"SANB3", "BESP3" }, //
                {"SANB4", "BESP4" }, //
                {"SDIA3", "SOES3" }, //
                {"SDIA4", "SOES4" }, //
                {"STBP11", "STBR11"}, //
                {"SUZB5", "SUZA4" }, // BSUL
                {"SWET3", "AORE3" },
                {"TBLE3", "GRSU3" }, //
                {"TBLE5", "GRSU5" },
                {"TBLE6", "GRSU6" }, //
                {"TERI3", "ACGU3" }, //
                {"TIMP3", "TCSL3" }, // TCSL4
                {"TMAR3", "TERJ3" },
                {"TMAR6", "TERJ4" }, //
                {"VAGR3", "ECOD3" }, //
                {"VALE5", "VALE4" }, //
                {"VCPA4", "PSIM4" }, //
                {"VIVO3", "TSPP3" }, //
                {"VIVO4", "TSPP4" }, //
                {"VIVR3", "INPR3" }, //
                {"VIVT3", "TLPP3" }, //
                {"VIVT4", "TLPP4" }, //
                {"VLID3", "ABNB3" }, //
                {"WEGE3", "ELMJ3" },
                {"WEGE4", "ELMJ4" }, // 
            };
        #endregion

        public static void Run()
        {
            var key = string.Empty;

            var periods = new string[] { "daily", "hour", "minute", "second" };

            var securities = new string[] { "equity", "options", "futures" };
            _raw = securities.ToDictionary(x => x, y => _root + @"\Data\" + y + @"\");
            _lean = securities.ToDictionary(x => x, y => _root + @"\GitHub\Lean\Data\" + y + @"\bra\");

            _tickers = new string[] { "bbas3", "mrfg3", "petr4", "usim5", "vale5" };

            var menu =
                "1. Extract raw data from COTAHIST file to QC daily zip file.\n" +
                "2. Extract raw data from NEG* files to QC tick zip file.\n" +
                "3. Make QC second/minute/hour zip files from QC tick zip file.\n" +
                "4. Write QC map files.\n" +
                "5. Write QC factor files.\n" +
                "6. Write QC holiday file.\n" +
                "7. Write Nelogica second/minute/hour csv file.\n" +
                "8. Check BMF FTP for new raw data.\n" +
                "9. Delete all QC tick zip file.\n" +
                "q. Sair\n" +
                ">> Insira opção: ";
            Console.Write(menu);

            //WriteQuantConnectFactorFilesFutures();

            do
            {
                key = Console.ReadLine().ToLower().Trim();
                var p_intersect = periods.Intersect(key.Split(' ')).ToArray();
                var s_intersect = securities.Intersect(key.Split(' ')).ToArray();
                var period = p_intersect.Count() > 0 ? p_intersect.First().Trim() : "minute";
                var security = s_intersect.Count() > 0 ? s_intersect.First().Trim() : "options";

                if (security == "options") _tickers = new string[] { "petr", "vale" };
                if (security == "futures") _tickers = new string[] { "ind", "dol", "win", "wdo" };

                switch (key.Split(' ')[0])
                {
                    case "1":
                        WriteQuantConnectDailyFile();
                        break;
                    case "2":
                        WriteQuantConnectTickFile(security);
                        break;
                    case "3":
                        WriteQuantConnectBarFile(security, period);
                        break;
                    case "4":
                        WriteQuantConnectMapFiles("daily");
                        break;
                    case "5":
                        WriteQuantConnectFactorFiles(security);
                        break;
                    case "6":
                        WriteQuantConnectHolidayFile();
                        break;
                    case "7":
                        WriteNelogicaFiles(security, period);
                        break;
                    case "8":
                        CheckForNewRawFiles();
                        break;
                    case "9":
                        FolderCleanUp(security, "tick");
                        break;
                    case "check":
                        CheckForNewRawFiles();
                        break;
                    case "show":
                        Process.Start(new ProcessStartInfo { FileName = Environment.CurrentDirectory, UseShellExecute = true });
                        break;
                    default:
                        Console.WriteLine("\nOpção Inválida!\n" + menu);
                        break;
                }
            } while (key != "q");
        }

        private static async Task CheckForNewRawFiles()
        {
            var serverUris = new Uri[]
            {
                new Uri("ftp://ftp.bmf.com.br/MarketData/Bovespa-Vista/"),
                new Uri("ftp://ftp.bmf.com.br/MarketData/Bovespa-Opcoes/"),
                new Uri("ftp://ftp.bmf.com.br/MarketData/BMF/")
            }.ToList();

            foreach (var serverUri in serverUris)
            {
                try
                {
                    Console.Write(serverUri.AbsoluteUri);
                    var request = WebRequest.Create(serverUri);
                    request.Proxy = null;
                    request.Credentials = new NetworkCredential("anonymous", "me@home.com");
                    request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                    using (var resp = (FtpWebResponse)request.GetResponse())
                    using (var sr = new StreamReader(resp.GetResponseStream(), Encoding.ASCII))
                    {
                        var data = sr.ReadToEnd().Split('\n')
                            .Where(d => d.Contains(".zip") && d.Contains("NEG_") && !d.Contains("FRAC"))
                            .Select(d => { var o = d.Split(' '); return o.Last().Replace("\r", "").Trim(); }).ToList();

                        new DirectoryInfo(_raw[_raw.Keys.ToArray()[serverUris.IndexOf(serverUri)]]).GetFiles("*.zip")
                            .Select(f => { var o = f.Name.Split('_'); return o.Last().Trim(); }).ToList()
                            .ForEach(f => data.RemoveAll(d => d.Contains(f)));

                        Console.WriteLine("\tfile(s) to download:\t" + data.Count);
                        data.ForEach(d => Console.WriteLine(d));

                        if (data.Count > 0) Process.Start("chrome.exe", serverUri.AbsoluteUri.ToString());
                    }
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
        }

        private static async Task WriteQuantConnectDailyFile()
        {
            var security = "equity";

            FolderCleanUp(security, "daily");

            var outputdir = Directory.CreateDirectory(_lean[security] + @"daily\");

            var zipfiles = new DirectoryInfo(_raw[security]).GetFiles("COTAHIST_A*zip")
                .Where(f => f.Name.Substring(f.Name.Length - 8, 4).ToInt32() > 1997).ToList();

            Console.WriteLine(zipfiles.Count + " zip files with raw data.\t" + DateTime.Now);

            var sum = 0.0;
            var total = zipfiles.Sum(z => z.Length);

            foreach (var zipfile in zipfiles)
            {
                var starttime = DateTime.Now;
                var data = await ReadAsyncZipFile(zipfile);
                data.RemoveAll(d => Filter(d));

                data.GroupBy(d => d.Substring(12, 12).Trim().ToLower() + ".csv").ToList().ForEach(d =>
                {
                    File.AppendAllLines(outputdir.FullName + d.Key, d.Select(l =>
                    {
                        return l.Substring(2, 8) + "," +
                            100 * Convert.ToInt64(l.Substring(56, 13)) + "," +
                            100 * Convert.ToInt64(l.Substring(69, 13)) + "," +
                            100 * Convert.ToInt64(l.Substring(82, 13)) + "," +
                            100 * Convert.ToInt64(l.Substring(108, 13)) + "," +
                            100 * Convert.ToInt64(l.Substring(152, 18));
                    }).OrderBy(l => l));
                });
                sum += zipfile.Length;
                Console.WriteLine((sum / total).ToString("0.00%\t") + zipfile.Name.ToUpper() +
                    " read in " + (DateTime.Now - starttime).ToString(@"ss\.ff") + " secs");
            }

            var csvFiles = outputdir.GetFiles("*.csv").ToDictionary(x => x.Name.Replace(".csv", "").Trim(), y => y);
            Console.WriteLine(csvFiles.Count + " csv files to zip.\t" + DateTime.Now);

            sum = 0.0;
            total = csvFiles.Values.Sum(c => c.Length);

            foreach (var kvp in csvFiles)
            {
                await Task.Factory.StartNew(() =>
                {
                    using (var z = new FileStream(kvp.Value.FullName.Replace(".csv", ".zip"), FileMode.Create))
                    using (var a = new ZipArchive(z, ZipArchiveMode.Create))
                        a.CreateEntryFromFile(kvp.Value.FullName, kvp.Value.Name, CompressionLevel.Optimal);

                    kvp.Value.Delete();
                });

                File.WriteAllText(kvp.Value.FullName.Replace("daily", "map_files"),
                    "19980102," + kvp.Key + "\r\n20501231," + kvp.Key);

                sum += kvp.Value.Length;
                Console.Write("\r" + (sum / total).ToString("0.00%\t") + kvp.Key.ToUpper() + "\t");
            };
            Console.WriteLine("\r... exiting routine at " + DateTime.Now);
        }

        private static async Task WriteQuantConnectTickFile(string security)
        {
            var lenght = security == "options" ? 4 : security == "futures" ? 3 : 6;
            var workdir = Directory.CreateDirectory(Environment.CurrentDirectory);
            var rootdir = Directory.CreateDirectory(_lean[security] + @"tick\");
            var subdirs = new Dictionary<string, string>();
            var datedic = new Dictionary<string, string>();

            if (rootdir.GetDirectories().Length > 0)
            {
                subdirs = rootdir.GetDirectories().ToDictionary(x => x.Name.ToLower(), y => y.FullName);
                datedic = subdirs.ToDictionary(x => x.Key, y =>
                {
                    var files = new DirectoryInfo(y.Value).GetFiles();
                    return files.Count() == 0 ? "20050101" : files.Last().Name.Substring(0, 8);
                });
            }

            var zipfiles = new DirectoryInfo(_raw[security]).GetFiles("NEG*zip").ToList();

            var sum = 0.0;
            var starttime = DateTime.Now;
            var total = zipfiles.Sum(z => z.Length);

            Console.WriteLine(starttime + " " + zipfiles.Count + " zip files with raw data. " + (total / Math.Pow(1024, 3)).ToString("0.00 GB"));

            foreach (var zipfile in zipfiles)
            {
                var lastdate = string.Empty;
                var output = new List<string[]>();

                using (var zip2open = new FileStream(zipfile.FullName, FileMode.Open, FileAccess.Read))
                using (var archive = new ZipArchive(zip2open, ZipArchiveMode.Read))
                    foreach (var entry in archive.Entries)
                        using (var file = new StreamReader(entry.Open()))
                        {
                            while (!file.EndOfStream)
                            {
                                var csv = (await file.ReadLineAsync()).Split(';');
                                if (csv.Length < 5) continue;

                                // Ticker
                                csv[1] = csv[1].Trim().ToLower();

                                if (!_tickers.Contains(security == "equity" ? csv[1] : csv[1].Substring(0, lenght))) continue;
                                if (csv[1].Length < 5 || csv[1].Length > 7) continue;

                                // TimeOfDay
                                csv[0] = csv[0].Replace("-", "");

                                if (datedic.ContainsKey(csv[1]) && string.Compare(datedic[csv[1]], csv[0]) >= 0) continue;

                                csv[2] = TimeSpan.Parse(csv[5], _enUS).TotalMilliseconds.ToString("F0") + "," +
                                    (10000 * decimal.Parse(csv[3], _enUS)).ToString("F0") + "," + csv[4].ToInt64();

                                if (output.Count == 0) lastdate = csv[0];
                                if (csv[0] == lastdate) { output.Add(csv); continue; }

                                output.GroupBy(g => g[1]).ToList()
                                    .ForEach(s => File.AppendAllLines(lastdate + "_" + s.Key + "_Trade_Tick.csv", s.Select(d => d[2])));
                                output.Clear();
                            }
                            output.GroupBy(g => g[1]).ToList()
                                .ForEach(s => File.AppendAllLines(lastdate + "_" + s.Key + "_Trade_Tick.csv", s.Select(d => d[2])));
                            output.Clear();
                        }

                var csvFiles = workdir.GetFiles("*_Trade_Tick.csv").ToList();

                subdirs.Clear();

                csvFiles.ForEach(c =>
                {
                    var ticker = c.Name.Split('_')[1];
                    if (subdirs.ContainsKey(ticker)) return;
                    var subdir = Directory.CreateDirectory(rootdir.FullName + ticker + @"\");
                    subdirs.Add(ticker, subdir.FullName);
                });

                foreach (var csvFile in csvFiles)
                {
                    // Order data in files (SLOW, but we do not know if data is ordered)
                    File.WriteAllLines(csvFile.FullName, File.ReadAllLines(csvFile.FullName).OrderBy(d => d));

                    var newFile = subdirs[csvFile.Name.Split('_')[1]] + csvFile.Name.Substring(0, 9) + "trade.zip";
                    using (var z = new FileStream(newFile, FileMode.Create))
                    using (var a = new ZipArchive(z, ZipArchiveMode.Create))
                        a.CreateEntryFromFile(csvFile.FullName, csvFile.Name, CompressionLevel.Optimal);

                    csvFile.Delete();
                }
                sum += zipfile.Length;

                Console.Write("\r" + (sum / total).ToString("0.00%") + "\t" + (DateTime.Now - starttime).ToString(@"hh\:mm\:ss") +
                    "   Last file: " + zipfile.Name.ToUpper());
            }
        }

        private static async Task WriteQuantConnectBarFile(string security, string period)
        {
            var periodDic = new Dictionary<string, double> { { "hour", 36e4 }, { "minute", 6e4 }, { "second", 1e3 } };
            if (!periodDic.ContainsKey(period)) { Console.WriteLine("Invalid period: " + periodDic); return; };
            var periodDbl = periodDic[period];

            var dirs = new DirectoryInfo(_lean[security] + @"tick\").GetDirectories().ToList();

            var sum = 0.0;
            var total = 0.0;
            dirs.ForEach(d => { total += d.GetFiles().Sum(f => f.Length); Console.Write("\r" + d.Name.ToUpper() + " " + (total / 1024 / 1024).ToString("0.00")); });
            Console.WriteLine("\r\n" + dirs.Count + " symbol directories to read.\t" + DateTime.Now);

            foreach (var dir in dirs)
            {
                var starttime = DateTime.Now;
                var outdir = Directory.CreateDirectory(dir.FullName.Replace("tick", period));
                var zipfiles = dir.GetFiles("*.zip").ToList();

                foreach (var zipfile in zipfiles)
                {
                    var output = new List<string>();
                    var data = await ReadAsyncZipFile(zipfile);

                    var ticks = double.Parse(data.First().Split(',')[0]);
                    ticks = ticks - ticks % 36e6 + 7.5 * 36e5;

                    // Group by period
                    data.GroupBy(d =>
                    {
                        var totalseconds = Math.Min(ticks, double.Parse(d.Split(',')[0], _enUS));
                        return (totalseconds - (totalseconds % periodDbl)).ToString(_enUS);
                    })
                        // For each period, define bar and save
                        .ToList().ForEach(t =>
                        {
                            var price = t.Select(l => l.Split(',')[1].ToDecimal()).ToList();
                            var qunty = t.Select(l => l.Split(',')[2].ToDecimal()).ToList().Sum();
                            var volfin = t.Select(l => { var d = l.Split(','); return d[1].ToDecimal() * d[2].ToDecimal(); }).ToList().Sum() / 10000;
                            output.Add(t.Key + "," + price.First() + "," + price.Max() + "," + price.Min() + "," + price.Last() + "," + qunty + "," + volfin);
                        });

                    await Task.Factory.StartNew(() =>
                    {
                        var newFile = zipfile.FullName.Replace("tick", period);
                        var csvFile = new FileInfo(newFile.Replace("trade.zip", dir.Name + "_" + period + "_trade.csv"));

                        File.WriteAllLines(csvFile.FullName, output);

                        using (var z = new FileStream(newFile, FileMode.Create))
                        using (var a = new ZipArchive(z, ZipArchiveMode.Create))
                            a.CreateEntryFromFile(csvFile.FullName, csvFile.Name, CompressionLevel.Optimal);

                        csvFile.Delete();
                    });
                    sum += zipfile.Length;
                }

                Console.Write("\r" + (sum / total).ToString("0.00%") + "\t" + dir.Name.ToUpper() + ": \t" + zipfiles.Count +
                    " days were read/written in " + (DateTime.Now - starttime).ToString(@"ss\.ff") + " secs.\t");
            }
            Console.WriteLine("\r\n... exiting routine at " + DateTime.Now);
        }

        private static async Task WriteQuantConnectFactorFiles(string security)
        {
            FolderCleanUp(security, "factor_files");

            var codes = new List<int>();
            var symbols = new List<string>();
            var alphabet = new List<char>();
            var startdate = new DateTime(1998, 1, 1);
            alphabet.Clear();

            #region GetSymbols
            new DirectoryInfo(_lean[security] + @"daily\").GetFiles("*.zip").ToList().ForEach(z =>
            {
                var symbol = z.Name.Replace(".zip", "").ToUpper();
                if (!alphabet.Contains(symbol[0])) alphabet.Add(symbol[0]);
                if (symbol.Length > 3 && !symbols.Contains(symbol)) symbols.Add(symbol);
            });
            #endregion

            #region GetCodes
            if (File.Exists("exception.txt"))
            {
                File.ReadAllLines("exception.txt").ToList().ForEach(l =>
                {
                    int code;
                    if (int.TryParse(l.Split(';')[0], out code) && code > 0 && !codes.Contains(code)) codes.Add(code);
                });
                File.Delete("exception.txt");
                alphabet.Clear();
            }
            foreach (var letter in alphabet)
            {
                try
                {
                    var id = 0;
                    var page = await DownloadAsync("http://www.bmfbovespa.com.br/cias-listadas/empresas-listadas/BuscaEmpresaListada.aspx?Letra=" + letter);
                    Console.Write(letter);

                    while ((id = page.IndexOf("codigoCvm", id) + 9) > 9)
                    {
                        id++;
                        var code = 0;
                        if (!int.TryParse(page.Substring(id, 5), out code) &&
                            !int.TryParse(page.Substring(id, 4), out code) &&
                            !int.TryParse(page.Substring(id, 3), out code) &&
                            !int.TryParse(page.Substring(id, 2), out code))
                            Console.WriteLine("");

                        if (!codes.Contains(code)) codes.Add(code);
                    }
                }
                catch (Exception e)
                {
                    //File.AppendAllText(errtxt, letter + ";");
                    Console.WriteLine(e.Message);
                }
            }
            if (codes.Contains(23264)) codes.Add(18112);
            codes = codes.OrderBy(i => i).ToList();
            #endregion

            if (!codes.Contains(18112)) codes.Add(18112);
            //if (!codes.Contains(4170)) codes.Add(4170); // VALE
            //if (!codes.Contains(9512)) codes.Add(9512); // PETR

            #region Get Ticker Merge
            var mergefile = new FileInfo("MergeEvent.txt");
            if (mergefile.Exists)
            {

            }
            #endregion

            foreach (var code in codes)
            {
                var index = 0;
                var page0 = await DownloadAsync("http://www.bmfbovespa.com.br/pt-br/mercados/acoes/empresas/ExecutaAcaoConsultaInfoEmp.asp?CodCVM=" + code);
                var page1 = await DownloadAsync("http://www.bmfbovespa.com.br/Cias-Listadas/Empresas-Listadas/ResumoProventosDinheiro.aspx?codigoCvm=" + code);
                var page2 = await DownloadAsync("http://www.bmfbovespa.com.br/Cias-Listadas/Empresas-Listadas/ResumoEventosCorporativos.aspx?codigoCvm=" + code);

                if ((index = page0.IndexOf("Papel=") + 6) < 6) continue;
                var length = page0.IndexOf("&", index) - index;
                page0 = page0.Substring(index, length);

                if (!page1.Contains("Proventos em Dinheiro")) page1 = string.Empty;
                if ((index = page1.IndexOf("<tbody>")) >= 0) page1 = page1.Substring(0, page1.IndexOf("</tbody>")).Substring(index);

                if (!page2.Contains("Proventos em Ações")) page2 = string.Empty;
                if ((index = page2.IndexOf("<tbody>")) >= 0) page2 = page2.Substring(0, page2.IndexOf("</tbody>")).Substring(index);

                var kind = new Dictionary<int, string> { { 3, "ON" }, { 4, "PN" }, { 5, "PNA" }, { 6, "PNB" }, { 7, "PNC" }, { 8, "PND" }, { 11, "UNT" } };
                var thiscodesymbols = symbols.Intersect(page0.Split(',')).ToList();
                if (thiscodesymbols.Count == 0) continue;

                foreach (var symbol in thiscodesymbols)
                {
                    var date = new DateTime();
                    var keys = new List<DateTime>();
                    var fkeys = new List<DateTime>();

                    var events = new Dictionary<DateTime, decimal>();
                    var factors = new Dictionary<DateTime, decimal>();

                    var dividend = new Dictionary<DateTime, decimal>();
                    var comprice = new Dictionary<DateTime, decimal>();

                    #region Dividends
                    try
                    {
                        index = 0;

                        while (page1.Length > 0 && (index = page1.IndexOf(">" + kind[symbol.Substring(4).ToInt32()] + "<", index)) > 0)
                        {
                            index++;
                            var idx = 0;
                            var cols = new List<string>();
                            var row = page1.Substring(index, page1.IndexOf("</tr>", index) - index);

                            while ((idx = row.IndexOf("\">", idx) + 2) >= 2) cols.Add(row.Substring(idx, row.IndexOf("<", idx) - idx));

                            var currentcomprice = 0m;
                            if (!decimal.TryParse(cols[5], NumberStyles.Any, _ptBR, out currentcomprice) || currentcomprice <= 0) continue;

                            if (!DateTime.TryParseExact(cols[4], "dd/MM/yyyy", _ptBR, DateTimeStyles.None, out date) &&
                                !DateTime.TryParseExact(cols[3], "dd/MM/yyyy", _ptBR, DateTimeStyles.None, out date))
                                date = DateTime.ParseExact(cols[0], "dd/MM/yyyy", _ptBR, DateTimeStyles.None);
                            if (date < startdate) continue;

                            if (!comprice.ContainsKey(date)) comprice.Add(date, currentcomprice);

                            if (dividend.ContainsKey(date))
                                dividend[date] += decimal.Parse(cols[1], _ptBR);
                            else
                                dividend.Add(date, decimal.Parse(cols[1], _ptBR));

                        }
                        events = comprice.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => 1m);
                        comprice = comprice.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                        dividend = dividend.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);

                        fkeys = comprice.Keys.ToList();
                        for (var i = 0; i < fkeys.Count; i++)
                        {
                            var factor = 1 - dividend[fkeys[i]] / comprice[fkeys[i]];

                            for (var j = i + 1; j < fkeys.Count; j++)
                                factor *= (1 - dividend[fkeys[j]] / comprice[fkeys[j]]);

                            factors.Add(fkeys[i], factor);
                        }
                        factors.Add(new DateTime(2049, 12, 31), 1m);
                    }
                    catch (Exception e)
                    {
                        File.AppendAllText("exception.txt", code + ";Dividends;" + e.Message + "\r\n");
                    }
                    #endregion

                    #region Corporate events
                    try
                    {
                        index = 0;

                        while (page2.Length > 1 && (index = page2.IndexOf("<tr", index + 1)) > 1)
                        {
                            index++;
                            var idx = 0;
                            var cols = new List<string>();
                            var row = page2.Substring(index, page2.IndexOf("/tr", index) - index);
                            if (row.Contains("Cisão")) continue;

                            while ((idx = row.IndexOf("\">", idx) + 2) >= 2) cols.Add(row.Substring(idx, row.IndexOf("<", idx) - idx));

                            if (!DateTime.TryParseExact(cols[2], "dd/MM/yyyy", _ptBR, DateTimeStyles.None, out date))
                                date = DateTime.ParseExact(cols[1], "dd/MM/yyyy", _ptBR, DateTimeStyles.None);
                            if (date < startdate) continue;

                            cols = cols[4].Split('/').ToList();

                            var event0 = 0m;
                            if (!decimal.TryParse(cols[0], NumberStyles.Any, _ptBR, out event0) || event0 <= 0) continue;

                            if (cols.Count == 1) event0 = 1 / (1m + event0 / 100m);
                            if (cols.Count == 2)
                            {
                                event0 = event0 / decimal.Parse(cols[1], _ptBR);
                                if (code == 9512) event0 = .1m;
                            }

                            if (events.ContainsKey(date))
                                events[date] = event0;
                            else
                                events.Add(date, event0);
                        }
                        events.Add(new DateTime(2049, 12, 31), 1m);
                        events = events.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                    }
                    catch (Exception e)
                    {
                        File.AppendAllText("exception.txt", code + ";CorpEvents;" + e.Message + "\r\n");
                    }
                    #endregion

                    keys = events.Keys.ToList();
                    for (var i = 0; i < keys.Count; i++) for (var j = i + 1; j < keys.Count; j++) events[keys[i]] *= events[keys[j]];

                    keys.Except(fkeys).ToList().ForEach(k => { if (!factors.ContainsKey(k)) factors.Add(k, 0m); });

                    for (var i = 0; i < keys.Count - 1; i++) if (factors[keys[i]] == 0) factors[keys[i]] = factors[keys[i + 1]];
                    factors = factors.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);

                    #region Write to file
                    var outputfile = _lean["equity"] + @"factor_files\" + symbol.ToLower() + ".csv";
                    if (File.Exists(outputfile)) File.Delete(outputfile);

                    foreach (var key in keys)
                        File.AppendAllText(outputfile, key.ToString("yyyyMMdd") + "," +
                            Math.Round(factors[key], 9).ToString(_enUS) + "," + Math.Round(events[key], 9).ToString(_enUS) + "\r\n");
                    #endregion
                }
                #region Write codes and respective symbols
                var output = code.ToString("00000") + ";";
                thiscodesymbols.ForEach(s => output += s + ";");
                output = output.Substring(0, output.Length - 1) + "\r\n";
                File.AppendAllText("codes.txt", output);
                #endregion

                Console.WriteLine(DateTime.Now.TimeOfDay + " " + ((1 + codes.IndexOf(code)) / (double)codes.Count).ToString("0.00%"));
            }
            Console.WriteLine(DateTime.Now.TimeOfDay + " Done!");
        }

        private static async Task WriteNelogicaFiles(string security, string period)
        {
            var periodDic = new Dictionary<string, string> { { "daily", "Diário" }, { "minute", "1min" }, { "second", "1sec" } };
            if (!periodDic.ContainsKey(period)) { Console.WriteLine("Invalid period: " + period); return; };

            if (period == "daily")
            {
                await WriteNelogicaDailyFiles(security);
                return;
            }

            var pStr = periodDic[period];
            var daily = period == "daily";
            var lenght = security == "options" ? 4 : security == "futures" ? 3 : 5;
            var dirs = new DirectoryInfo(_lean[security] + period + @"\").GetDirectories().ToList();

            var sum = 0.0;
            var total = 0.0;
            dirs.ForEach(d => { total += d.GetFiles().Sum(f => f.Length); Console.Write("\r" + d.Name.ToUpper() + " " + (total / 1024 / 1024).ToString("000.00") + " MB"); });
            Console.WriteLine("\r\n" + dirs.Count + " symbol directories to read.\t" + DateTime.Now);

            foreach (var dir in dirs)
            {
                var symbol = dir.Name;
                var starttime = DateTime.Now;
                var factors = GetTickerFactors(security, symbol);
                var zipfiles = dir.GetFiles("*.zip").Reverse().ToList();
                var csvFile = new FileInfo(symbol.ToUpper() + "_" + pStr + ".csv");
                if (csvFile.Exists) csvFile.Delete();

                foreach (var zipfile in zipfiles)
                {
                    var date = zipfile.Name.ToDateTime();
                    var factor = factors.FirstOrDefault(kvp => kvp.Key >= date);

                    File.AppendAllLines(csvFile.FullName, (await ReadAsyncZipFile(zipfile)).Select(l =>
                    {
                        var data = l.Split(',');

                        return symbol.ToUpper() + ";" +
                            date.AddMilliseconds(data[0].ToInt64()).ToString(@"dd/MM/yyyy;HH\:mm\:ss") + ";" +
                            Math.Round(data[1].ToDecimal() * factor.Value / 10000, 2).ToString("0.00", _ptBR) + ";" +
                            Math.Round(data[2].ToDecimal() * factor.Value / 10000, 2).ToString("0.00", _ptBR) + ";" +
                            Math.Round(data[3].ToDecimal() * factor.Value / 10000, 2).ToString("0.00", _ptBR) + ";" +
                            Math.Round(data[4].ToDecimal() * factor.Value / 10000, 2).ToString("0.00", _ptBR) + ";" +
                            (data.Length == 6 ? data[5] : data[6] + ";" + data[5]);

                    }).Reverse().ToArray());

                    sum += zipfile.Length;
                }

                if (security == "equity")
                {
                    await Task.Factory.StartNew(() =>
                    {
                        using (var z = new FileStream(csvFile.FullName.Replace(".csv", ".zip"), FileMode.Create))
                        using (var a = new ZipArchive(z, ZipArchiveMode.Create))
                            a.CreateEntryFromFile(csvFile.FullName, csvFile.Name, CompressionLevel.Optimal);

                        csvFile.Delete();
                    });
                }

                Console.Write("\r" + (sum / total).ToString("0.00%") + "\t" + dir.Name.ToUpper() + ": \t" + zipfiles.Count +
                " days were read/written in " + (DateTime.Now - starttime).ToString(@"ss\.ff") + " secs.\t");
            }

            // For options and futures
            var csvFiles = new DirectoryInfo(Environment.CurrentDirectory).GetFiles("*_" + pStr + ".csv");

            if (csvFiles.Count() > 0)
            {
                sum = 0.0;
                total = csvFiles.Sum(f => f.Length);
                Console.WriteLine("Zipping " + (total / 1024 / 1024).ToString("000.00") + " MB");

                csvFiles.GroupBy(g =>
                {
                    if (security == "equity") return g.Name.Replace(".csv", ".zip");
                    if (security == "futures") return g.Name.Substring(0, 3).ToUpper() + "FUT" + "_" + pStr + ".zip";

                    var type = ("ABCDEFGHIJKL".Contains(g.Name[4]) ? "_C" : "_P") + "_" + pStr + ".zip";
                    return g.Name.Substring(0, 4) + type;
                })
                    .ToList().ForEach(f =>
                    {
                        var outputfile = new FileInfo(f.Key);
                        if (outputfile.Exists) outputfile.Delete();

                        using (var z = new FileStream(outputfile.FullName, FileMode.Create))
                        using (var a = new ZipArchive(z, ZipArchiveMode.Create, true))
                            f.ToList().ForEach(csvFile =>
                            {
                                a.CreateEntryFromFile(csvFile.FullName, csvFile.Name, CompressionLevel.Optimal);
                                csvFile.Delete();
                                sum += csvFile.Length;
                                Console.Write("\r" + (sum / total).ToString("0.00%") + "\tLast zippped file:\t" + csvFile.Name.ToUpper());
                            });
                    });
            }
            Console.WriteLine("\r\n... exiting routine at " + DateTime.Now);
        }

        private static async Task WriteNelogicaDailyFiles(string security)
        {
            var roottime = DateTime.Now;
            var outputfile = new FileInfo("Bovespa_Diário.zip");
            if (outputfile.Exists) outputfile.Delete();

            using (var z = new FileStream(outputfile.FullName, FileMode.Create))
            using (var a = new ZipArchive(z, ZipArchiveMode.Create, true))
            {
                var zipfiles = new DirectoryInfo(_lean[security] + @"daily\").GetFiles("*.zip").ToList();

                var sum = 0.0;
                var total = zipfiles.Sum(f => f.Length);

                Console.WriteLine(zipfiles.Count + " symbol to read.\t" + DateTime.Now);

                foreach (var zipfile in zipfiles)
                {
                    var index = 0;
                    if ((index = zipfile.Name.IndexOf('.')) < 4) continue;

                    var starttime = DateTime.Now;
                    var symbol = zipfile.Name.Substring(0, index);
                    var factors = GetTickerFactors(security, symbol);
                    var csvFile = new FileInfo(symbol.ToUpper() + "_Diário.csv");

                    File.WriteAllLines(csvFile.FullName, (await ReadAsyncZipFile(zipfile)).Select(l =>
                    {
                        var data = l.Split(',');
                        var date = data[0].ToDateTime();
                        var factor = factors.FirstOrDefault(kvp => kvp.Key >= date);

                        return symbol.ToUpper() + ";" + date.ToString("dd/MM/yyyy") + ";" +
                            Math.Round(data[1].ToDecimal() * factor.Value / 10000, 2).ToString("0.00", _ptBR) + ";" +
                            Math.Round(data[2].ToDecimal() * factor.Value / 10000, 2).ToString("0.00", _ptBR) + ";" +
                            Math.Round(data[3].ToDecimal() * factor.Value / 10000, 2).ToString("0.00", _ptBR) + ";" +
                            Math.Round(data[4].ToDecimal() * factor.Value / 10000, 2).ToString("0.00", _ptBR) + ";" +
                            (data.Length == 6 ? data[5] : data[6] + ";" + data[5]);

                    }).Reverse().ToArray());

                    a.CreateEntryFromFile(csvFile.FullName, csvFile.Name, CompressionLevel.Optimal);

                    Console.Write("\r" + ((sum += zipfile.Length) / total).ToString("0.00%\t") + symbol.ToUpper() +
                        "\twas read/written in " + (DateTime.Now - starttime).ToString(@"ss\.ff") + " secs\t");

                    csvFile.Delete();
                }
            }
            Console.WriteLine("\r... exiting routine at " + DateTime.Now + ". Took " + (DateTime.Now - roottime).ToString(@"mm\:ss"));
        }

        private static SortedList<DateTime, decimal> GetTickerFactors(string security, string symbol)
        {
            var factors = new SortedList<DateTime, decimal>();
            var file = new FileInfo(_lean[security] + @"factor_files\" + symbol + ".csv");

            if (file.Exists)
            {
                File.ReadAllLines(file.FullName).ToList().ForEach(line =>
                {
                    var data = line.Split(',');
                    var factor = data[1].ToDecimal() * data[2].ToDecimal();
                    factors.Add(data[0].ToDateTime(), factor);
                });
            }
            else
                factors.Add(new DateTime(2049, 12, 31), 1m);

            return factors;
        }

        private static async Task WriteQuantConnectHolidayFile()
        {
            // America/Sao_Paulo,bra,,equity,-,-,-,-,9.5,10,16.9166667,18,9.5,10,16.9166667,18,9.5,10,16.9166667,18,9.5,10,16.9166667,18,9.5,10,16.9166667,18,-,-,-,-
            var holidays = new List<DateTime>();

            var ofile = new FileInfo(_lean["equity"].Replace("equity\\bra", "market-hours") + "holidays-bra.csv");
            var output = ofile.Exists
                ? File.ReadAllLines(ofile.FullName).ToList()
                : (new string[] { "year, month, day" }).ToList();
            var lastdate = output.Count == 1
                ? new DateTime(1997, 12, 31)
                : DateTime.ParseExact(output.Last(), "yyyy, MM, dd", _enUS);

            if (lastdate == new DateTime(DateTime.Now.Year, 12, 31)) return;

            #region Get Holidays from Bovespa page
            try
            {
                var i = 0;
                var id = 0;
                var date = new DateTime();
                var page = await DownloadAsync("http://www.bmfbovespa.com.br/pt-br/regulacao/calendario-do-mercado/calendario-do-mercado.aspx");
                page = page.Substring(0, page.IndexOf("linhaDivMais"));

                var months = new string[] {
                        "Jan", "Fev", "Mar",
                        "Abr", "Mai", "Jun",
                        "Jul", "Ago", "Set",
                        "Out", "Nov", "Dez" }.ToList();

                while (i < 12)
                {
                    while (i < 12 && (id = page.IndexOf(">" + months[i + 0] + "<")) < 0) i++;
                    var start = id + 1;

                    while (i < 11 && (id = page.IndexOf(">" + months[i + 1] + "<")) < 0) i++;
                    var count = id - start;

                    months[i] = count > 0 ? page.Substring(start, count) : page.Substring(start);

                    id = 0;
                    while ((id = months[i].IndexOf("img/ic_", id) + 6) > 6)
                    {
                        id++;
                        if (DateTime.TryParseExact(months[i].Substring(id, 2) + months[i].Substring(0, 3) + DateTime.Now.Year.ToString(),
                            "ddMMMyyyy", _ptBR, DateTimeStyles.None, out date))
                            holidays.Add(date);
                    }
                    i++;
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            #endregion

            while (lastdate < holidays.First()) holidays.Add(lastdate = lastdate.AddDays(1));
            holidays.RemoveAll(d => d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday);

            var zipfiles = new DirectoryInfo(_raw["equity"]).GetFiles("COTAHIST_A*zip").Where(zf =>
            {
                var zfyear = zf.Name.Substring(zf.Name.Length - 8, 4).ToInt32();
                return zfyear >= holidays.Min().Year && zfyear < holidays.Max().Year;
            }).ToArray();

            foreach (var zf in zipfiles)
            {
                var data = await ReadAsyncZipFile(zf);

                // Remove header and footer
                data.RemoveAt(0);
                data.RemoveAt(data.Count - 1);

                data.Select(l => l.ToDateTime(2)).ToList().ForEach(d => holidays.Remove(d));
                Console.Write("\r\n" + zf.Name + "\t" + holidays.Count);
            }

            output.AddRange(holidays.OrderBy(d => d).Select(d => d.ToString("yyyy, MM, dd")));
            File.WriteAllText(ofile.FullName, string.Join("\r\n", output.ToArray()));
            Console.WriteLine("\r\n" + ofile.Name + " written!");
        }

        private static async Task SearchTickerChange()
        {
            var index = 0;
            var page = string.Empty;
            var cancel = new List<string>();
            var merged = new List<string>();

            #region Get Cancelled and Incorporated companies from BmfBovespa site
            try
            {
                var codes = new List<string>();

                page = await DownloadAsync("http://www.bmfbovespa.com.br/cias-Listadas/empresas-com-registro-cancelado/ResumoEmpresasComRegistroCancelado.aspx?razaoSocial=");
                if ((index = page.IndexOf("<tbody>")) >= 0) page = page.Substring(0, page.IndexOf("</tbody>")).Substring(index);

                index = 0;
                while (page.Length > 1 && (index = page.IndexOf("codigo=", index) + 7) > 7)
                {
                    var code = page.Substring(index, 4);
                    if (!codes.Contains(code)) codes.Add(code);
                }

                foreach (var code in codes)
                {
                    page = await DownloadAsync("http://www.bmfbovespa.com.br/cias-Listadas/empresas-com-registro-cancelado/DetalheEmpresasComRegistroCancelado.aspx?codigo=" + code);
                    if ((index = page.IndexOf("lblMotivo")) < 0) { Console.Write(" " + code); continue; }
                    page = page.Substring(0, page.IndexOf("</tbody>")).Substring(index);
                    if ((index = page.IndexOf(">")) > 0) page = code + page.Substring(0, page.IndexOf("<")).Substring(index);
                    if (page.ToLower().Contains("incorporad") || code == "SUBA") merged.Add(page); else cancel.Add(page);
                }
            }
            catch (Exception e) { Console.WriteLine("\r\n" + page.Substring(0, 20) + "\r\n" + e.Message); }
            #endregion
            File.WriteAllLines("Canceladas.txt", cancel.OrderBy(x => x));
            File.WriteAllLines("Incorporadas.txt", merged.OrderBy(x => x));
            cancel = cancel.Select(c => c.Substring(0, 4)).ToList();

            TickerChange.Keys.ToList().ForEach(k => { if (!cancel.Contains(k.Substring(0, 4))) cancel.Add(k.Substring(0, 4)); });
            TickerChange.Values.ToList().ForEach(k => { if (!cancel.Contains(k.Substring(0, 4))) cancel.Add(k.Substring(0, 4)); });
            cancel = cancel.Select(c => c.ToLower()).OrderBy(k => k).ToList();

            var files = new DirectoryInfo(_lean["equity"] + @"daily\").GetFiles("*.csv").ToList();
            files.RemoveAll(f => cancel.Contains(f.Name.Substring(0, 4)));
            if (files.Count == 0) return;

            var tradingdays = TradingDays();
            var firstday = new Dictionary<string, DateTime>();
            var lasttday = new Dictionary<string, DateTime>();

            foreach (var file in files)
            {
                var data = File.ReadAllLines(file.FullName).OrderBy(d => d).ToList();
                var lday = data.Last().ToDateTime();
                var fday = data.First().ToDateTime();

                File.WriteAllLines(file.FullName, data);

                lasttday.Add(file.Name, lday);
                firstday.Add(file.Name, fday);
            }

            foreach (var key in firstday.Keys)
            {
                var results = lasttday.Where(d =>
                {
                    var fday = firstday[key];
                    var pday = firstday[key].AddDays(-1);
                    while (!tradingdays.Result.Contains(pday.ToString("yyyyMMdd", _enUS))) pday = pday.AddDays(-1);
                    var ltfday = d.Value < fday;
                    var gtfday5 = d.Value >= pday;

                    return ltfday && gtfday5;
                })
                    .ToDictionary(x => x.Key, y => y.Value);

                foreach (var result in results)
                {
                    var data = File.ReadAllLines(_lean["equity"] + @"daily\" + result.Key).OrderBy(d => d).ToList();

                    // We count how many trading day there were between the first and the last days
                    // and calculate the frequency the symbol was traded
                    var count1 =
                        (double)(tradingdays.Result.IndexOf(data.Last().Substring(0, 8))) -
                        (double)(tradingdays.Result.IndexOf(data.First().Substring(0, 8)));

                    var freq1 = count1 == 0 ? 0 : (data.Count - 1) / count1;

                    data.RemoveAll(d => d.ToDateTime() < new DateTime(result.Value.Year - 1, 1, 1));

                    var count2 =
                        (double)(tradingdays.Result.IndexOf(data.Last().Substring(0, 8))) -
                        (double)(tradingdays.Result.IndexOf(data.First().Substring(0, 8)));

                    var freq2 = count2 == 0 ? 0 : (data.Count - 1) / count2;

                    if (Math.Max(freq1, freq2) < .5) continue;

                    var output = "{\"" + key.Replace(".csv", "\", \"") + result.Key.Replace(".csv", "\"},");

                    var outvalue = string.Empty;
                    if (TickerChange.TryGetValue(key.Replace(".csv", "").ToUpper(), out outvalue))
                        if (outvalue == result.Key.Replace(".csv", "").ToUpper())
                        {
                            output += "//";
                            File.AppendAllText("mergedic.txt", output.ToUpper() + "\r\n");
                        }

                    File.AppendAllText("merge.txt", output.ToUpper() + "\r\n");
                }
            }
            Console.WriteLine(" Done!");
        }

        private static void WriteQuantConnectMapFiles(string folder)
        {
            var files = new DirectoryInfo(_lean["equity"] + folder + @"\").GetFiles("*.csv").ToList();
            if (files.Count == 0) return;

            var symbols = TickerChange.Keys.Intersect(TickerChange.Values).ToList();
            var leg1 = TickerChange.Where(x => symbols.Contains(x.Key)).ToDictionary(x => x.Key, y => y.Value);
            var leg2 = TickerChange.Where(x => symbols.Contains(x.Value)).ToDictionary(x => x.Key, y => y.Value);
            foreach (var kvp in leg1.Concat(leg2)) TickerChange.Remove(kvp.Key);
            TickerChange = TickerChange.Concat(leg1.Concat(leg2)).ToDictionary(x => x.Key, y => y.Value);

            var mergeevent = new FileInfo("MergeEvent.txt");
            if (mergeevent.Exists) mergeevent.Delete();

            foreach (var kvp in TickerChange)
            {
                var newfile = files.Find(f => f.Name.Contains(kvp.Key.ToLower()));
                var oldfile = files.Find(f => f.Name.Contains(kvp.Value.ToLower()));

                if (!oldfile.Exists) continue;

                var mergeddata = File.ReadAllLines(oldfile.FullName).OrderBy(d => d).ToList();
                File.AppendAllText(mergeevent.FullName, kvp.Key + "," + kvp.Value + "," + mergeddata.Last().Substring(0, 8) + ",1\r\n");

                if (newfile.Exists) mergeddata.AddRange(File.ReadAllLines(newfile.FullName));

                File.WriteAllLines(newfile.Name, mergeddata.OrderBy(d => d));
                //File.Delete(oldfile.FullName);
            }
            foreach (var symbol in symbols) { File.Delete(symbol.ToLower() + ".csv"); }
            Console.WriteLine(" Done!");
        }

        private static void WriteQuantConnectMapFilesFutures()
        {
            var mm = "0fghjkmnquvxz".ToList();
            var tradingdays = TradingDays().Result.Select(t => t.ToDateTime()).ToList();
            var mapdir = Directory.CreateDirectory(_lean["futures"] + @"map_files\");

            #region Expiration dates for index futures
            var winfut = new FileInfo(mapdir.FullName + "winfut.csv");
            if (winfut.Exists) winfut.Delete();

            var indfut = new FileInfo(mapdir.FullName + "indfut.csv");
            if (indfut.Exists) indfut.Delete();

            tradingdays.FindAll(t => t.Year >= 2005 && t.Month % 2 == 0 && t.Day > 11 && t.Day < 19)
                .GroupBy(t => t.AddDays(1 - t.Day)).ToList()
                .ForEach(t =>
                {
                    var end = mm[t.Key.Month] + (t.Key.Year - 2000).ToString("00") + "\r\n";
                    var days = new List<DateTime>();

                    foreach (var i in new int[] { 3, 4, 5, 1, 2 })
                    {
                        days = t.Where(d => (int)d.DayOfWeek == i).ToList();
                        if (days.Count == 1) break;
                    }

                    var exday = tradingdays[tradingdays.IndexOf(days.First()) - 1].ToString("yyyyMMdd");
                    File.AppendAllText(winfut.FullName, exday + ",win" + end);
                    File.AppendAllText(indfut.FullName, exday + ",ind" + end);
                });
            #endregion

            #region Expiration dates for dolar futures
            var wdofut = new FileInfo(mapdir.FullName + "wdofut.csv");
            if (winfut.Exists) winfut.Delete();

            var dolfut = new FileInfo(mapdir.FullName + "dolfut.csv");
            if (indfut.Exists) indfut.Delete();

            tradingdays.FindAll(t => t.Year >= 2005 && t.Day > 11 && t.Day < 19)
                .GroupBy(t => t.AddDays(1 - t.Day)).ToList()
                .ForEach(t =>
                {
                    var end = mm[t.Key.Month] + (t.Key.Year - 2000).ToString("00") + "\r\n";
                    var days = new List<DateTime>();

                    foreach (var i in new int[] { 3, 4, 5, 1, 2 })
                    {
                        days = t.Where(d => (int)d.DayOfWeek == i).ToList();
                        if (days.Count == 1) break;
                    }

                    var exday = tradingdays[tradingdays.IndexOf(days.First()) - 1].ToString("yyyyMMdd");
                    File.AppendAllText(wdofut.FullName, exday + ",wdo" + end);
                    File.AppendAllText(dolfut.FullName, exday + ",dol" + end);
                });
            #endregion
        }

        private static void WriteQuantConnectFactorFilesFutures()
        {
            var dirs = new DirectoryInfo(_lean["futures"] + @"tick\").GetDirectories()
                .GroupBy(d => d.Name.Substring(0, 3) + "fut").ToDictionary(x => x.Key, y => y.ToList());

            var mapdir = new DirectoryInfo(_lean["futures"] + @"map_files\");
            var mapfiles = mapdir.GetFiles().ToDictionary(x => x.Name.Substring(0, 6), y => y);

            var facdir = Directory.CreateDirectory(mapdir.FullName.Replace("map", "factor"));
            var facfiles = mapfiles.ToDictionary(x => x.Key, y => new FileInfo(y.Value.FullName.Replace("map", "factor")));
            foreach (var kvp in facfiles) { if (kvp.Value.Exists) kvp.Value.Delete(); }

            foreach (var kvp in dirs)
            {
                if (!mapfiles.ContainsKey(kvp.Key)) continue;
                var exdates = File.ReadAllLines(mapfiles[kvp.Key].FullName).ToDictionary(x => x.Substring(9), y => y.Substring(0, 8));

                for (var i = 1; i < exdates.Count; i++)
                {
                    var keyprev = exdates.Keys.ToArray()[i - 1];
                    var keycurr = exdates.Keys.ToArray()[i];
                    var dirprev = kvp.Value.Find(d => d.Name == keyprev);
                    var dircurr = kvp.Value.Find(d => d.Name == keycurr);
                    if (dirprev == null || dircurr == null) continue;

                    var fileprev = dirprev.FullName + @"\" + exdates[keyprev] + ".zip";
                    var filecurr = dircurr.FullName + @"\" + exdates[keyprev] + ".zip";

                    var x = 0;
                }
            }
        }

        #region Utils
        private static void FolderCleanUp(string security, string folder)
        {
            // Delete selected CSV files at workdir
            var files = new DirectoryInfo(Environment.CurrentDirectory).GetFiles("*.csv").ToList()
                .FindAll(f => f.Name.ToLower().Contains(folder));
            foreach (var x in files) x.Delete();

            // Delete all CSV files
            files = new DirectoryInfo(_lean[security] + folder + @"\").GetFiles("*.csv").ToList();
            foreach (var x in files) x.Delete();

            // Delete the folders
            var dires = new DirectoryInfo(_lean[security] + folder + @"\").GetDirectories().ToList();
            foreach (var x in dires) Directory.Delete(x.FullName, true);

            Console.WriteLine("\rAll clean:\t" + folder + "\t" + security);
        }

        private static bool Filter(string line)
        {
            int type;

            return !int.TryParse(line.Substring(16, 3), out type) || type < 3 || (type > 8 && type != 11)
                || line.Substring(12, 12).Trim().Contains(" ");
        }

        private static async Task<List<string>> TradingDays()
        {
            var ifile = new FileInfo(_lean["equity"].Replace("equity\\bra", "market-hours") + "holidays-bra.csv");
            if (!ifile.Exists) await WriteQuantConnectHolidayFile();

            var data = File.ReadAllLines(ifile.FullName).ToList();
            data.RemoveAt(0);   // Remove header

            var tradingdays = new List<DateTime>();
            var holidays = data.Select(d => DateTime.ParseExact(d, "yyyy, MM, dd", _enUS));

            var date = holidays.First();
            while (date < holidays.Last()) tradingdays.Add(date = date.AddDays(1));

            tradingdays = tradingdays.Except(holidays).ToList();
            tradingdays.RemoveAll(d => d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday);

            return tradingdays.Select(d => d.ToString("yyyyMMdd")).ToList();
        }

        private static async Task<List<string>> ReadAsyncZipFile(FileInfo zipfile, List<string> selected)
        {
            var data = new List<string>();

            if (!zipfile.Exists) return data;

            try
            {
                using (var zip2open = new FileStream(zipfile.FullName, FileMode.Open, FileAccess.Read))
                using (var archive = new ZipArchive(zip2open, ZipArchiveMode.Read))
                    foreach (var entry in archive.Entries)
                        using (var file = new StreamReader(entry.Open()))
                            while (!file.EndOfStream)
                            {
                                var line = await file.ReadLineAsync();
                                if (selected.Count == 0 || selected.Any(s => line.Contains(s)))
                                    data.Add(line);
                            }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

            return data;
        }

        private static async Task<List<string>> ReadAsyncZipFile(FileInfo zipfile)
        {
            return await ReadAsyncZipFile(zipfile, selected: new List<string>());
        }

        private static async Task<string> DownloadAsync(string str)
        {
            try
            {
                using (var client = new HttpClient())
                using (var response = await client.GetAsync(str))
                using (var content = response.Content)
                    return await content.ReadAsStringAsync();
            }
            catch (Exception e) { return e.Message; }
        }
        #endregion
    }
    public static class Extensions
    {
        /// <summary>
        /// Extension method for faster string to decimal conversion. 
        /// </summary>
        /// <param name="str">String to be converted to positive decimal value</param>
        /// <remarks>Method makes some assuptions - always numbers, no "signs" +,- etc.</remarks>
        /// <returns>Decimal value of the string</returns>
        public static decimal ToDecimal(this string str)
        {
            long value = 0;
            var decimalPlaces = 0;
            bool hasDecimals = false;

            for (var i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                if (ch == '.')
                {
                    hasDecimals = true;
                    decimalPlaces = 0;
                }
                else
                {
                    value = value * 10 + (ch - '0');
                    decimalPlaces++;
                }
            }

            var lo = (int)value;
            var mid = (int)(value >> 32);
            return new decimal(lo, mid, 0, false, (byte)(hasDecimals ? decimalPlaces : 0));
        }

        /// <summary>
        /// Extension method for faster string to Int32 conversion. 
        /// </summary>
        /// <param name="str">String to be converted to positive Int32 value</param>
        /// <remarks>Method makes some assuptions - always numbers, no "signs" +,- etc.</remarks>
        /// <returns>Int32 value of the string</returns>
        public static int ToInt32(this string str)
        {
            int value = 0;
            for (var i = 0; i < str.Length; i++)
            {
                value = value * 10 + (str[i] - '0');
            }
            return value;
        }

        /// <summary>
        /// Extension method for faster string to Int64 conversion. 
        /// </summary>
        /// <param name="str">String to be converted to positive Int64 value</param>
        /// <remarks>Method makes some assuptions - always numbers, no "signs" +,- etc.</remarks>
        /// <returns>Int32 value of the string</returns>
        public static long ToInt64(this string str)
        {
            long value = 0;
            for (var i = 0; i < str.Length; i++)
            {
                value = value * 10 + (str[i] - '0');
            }
            return value;
        }

        public static DateTime ToDateTime(this string str, int startIndex)
        {
            try
            {
                return DateTime.ParseExact(str.Substring(startIndex, 8), "yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US"));
            }
            catch (Exception)
            {
                return new DateTime(1978, 6, 8);
            }
        }

        public static DateTime ToDateTime(this string str)
        {
            return str.ToDateTime(0);
        }
    }
}