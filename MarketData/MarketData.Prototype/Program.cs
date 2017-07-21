using Common.Logging;
using FlatFiles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MarketData.Prototype
{
    class Program
    {
        const string config_key_ibovespa_path = "ibov_path";

        static ILog log = LogManager.GetLogger<Program>();
        static void Main(string[] args)
        {
            log.Info("started");
            Console.WriteLine("99COTAHIST.2017BOVESPA 2017071100000251454".Length);
            var files = GetIbovespaFiles();

            if (files == null)
            {
                log.Error("files are null");
            }

            foreach (var oneFile in files)
            {
                var records = ReadBovespaFile(oneFile);
                
            }

            log.Info("finished");

            Console.WriteLine("press <ENTER> to quit.");
            Console.ReadLine();
        }

        private static DataTable ReadBovespaFile(string oneFile)
        {
            var schema = CreteBovespaSchema();
            var options = new FixedLengthOptions()
            {
                IsFirstRecordHeader = true
                ,UnpartitionedRecordFilter = (record) => record.StartsWith("99")
            };

            using (var fileReader = File.OpenText(oneFile))
            {
                var reader = new FixedLengthReader(fileReader, schema, options);

                DataTable quotesTable = new DataTable("quotes");

                quotesTable.ReadFlatFile(reader);

                string origAssemblyLocation = Directory.GetCurrentDirectory();

                var path = Path.Combine(origAssemblyLocation, "output");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                path = Path.Combine(path, "teste.csv");

                quotesTable.WriteToCsvFile(path);
                return quotesTable;
            } 

            return null;
        }

        private static FixedLengthSchema CreteBovespaSchema()
        {
            const string text = @"012017010202AALR3       010ALLIAR      ON      NM   R$  000000000146200000000014880000000001440000000000145800000000014600000000001460000000000147300087000000000000035900000000000052350500000000000000009999123100000010000000000000BRAALRACNOR6100";
            FixedLengthSchema schema = new FixedLengthSchema();
            schema.AddColumn(new Int32Column("TIPREG"), 2);      // 0                                                                      // FIXED “01”
            schema.AddColumn(new DateTimeColumn("DATE_OF_EXCHANGE") { InputFormat = "yyyyMMdd", OutputFormat = "yyyyMMdd" }, 8);       // DATE OF EXCHANGE FORMAT “YYYYMMDD”
            schema.AddColumn(new StringColumn("CODBDI"), 2 );     // 2                                                                      // CODBDI – BDI CODE  (USED TO CLASSIFY THE PAPERS IN THE DAILY INFORMATION BULLETIN SEE ATTACHED TABLE)
            schema.AddColumn(new StringColumn("CODNEG"), 12);    // 3                                                                      // PAPER NEGOTIATION CODE
            schema.AddColumn(new Int32Column("TPMERC"), 3);      // 4
            schema.AddColumn(new StringColumn("NOMRES"), 12);    // 5
            schema.AddColumn(new StringColumn("ESPECI"), 10);    // 6
            schema.AddColumn(new StringColumn("PRAZOT"), 3);     // 7
            schema.AddColumn(new StringColumn("MODREF"), 4);     // 8
            schema.AddColumn(new DoubleColumn("PREABE"), 13);    // 9

           
            schema.AddColumn(new DoubleColumn("PREMAX"), 13);
            schema.AddColumn(new DoubleColumn("PREMIN"), 13);
            schema.AddColumn(new DoubleColumn("PREMED"), 13);
            schema.AddColumn(new DoubleColumn("PREULT"), 13);
            schema.AddColumn(new DoubleColumn("PREOFC"), 13);
            schema.AddColumn(new DoubleColumn("PREOFV"), 13);
            schema.AddColumn(new Int64Column("TOTNEG"), 5);
            schema.AddColumn(new Int64Column("QUATOT"), 18);

            
             schema.AddColumn(new DoubleColumn("VOLTOT"), 18);
             schema.AddColumn(new DoubleColumn("PREEXE"), 13);
             schema.AddColumn(new Int32Column("INDOPC"), 1);
             schema.AddColumn(new DateTimeColumn("DATVEN") { InputFormat = "yyyyMMdd", OutputFormat = "yyyyMMdd" }, 8);
             schema.AddColumn(new Int32Column("FATCOT"), 7);
             schema.AddColumn(new DoubleColumn("PTOEXE"), 13);
             schema.AddColumn(new StringColumn("CODISI"), 12);
             schema.AddColumn(new Int32Column("DISMES"), 3);
             

            StringReader stringReader = new StringReader(text);
            FixedLengthReader parser = new FixedLengthReader(stringReader, schema);
            var test = parser.Read();

            return schema;
        }

        static string[] GetIbovespaFiles()
        {

            var bovespaPath = ConfigurationManager.AppSettings[config_key_ibovespa_path] as string;

            if (string.IsNullOrEmpty(bovespaPath))
            {
                log.Error("could not load bovespa path");
                return null;
            }

            if (!Directory.Exists(bovespaPath))
            {
                log.Error("directory does not exist");
                return null;
            }

            return Directory.GetFiles(bovespaPath);

        }
    }
}
