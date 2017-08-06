using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototyping.Code.Download.MarketData.TesouroDireto.Intradia
{
    public class TesouroIntradia
    {
        const string endereco = @"http://www.tesouro.fazenda.gov.br/tesouro-direto-precos-e-taxas-dos-titulos";
        const string formatDate = @"dd/MM/yyyy";
        const string formatDateTime = @"dd/MM/yyyy HH:mm";

        public static void Run()
        {
            DateTime? dataHoraAtualizacao = null;
            List<InformacaoPrecoTitulo> informacoesPrecoTitulo = null;

            // From Web            
            var web = new HtmlWeb();
            var doc = web.Load(endereco);

            informacoesPrecoTitulo = new List<InformacaoPrecoTitulo>();

            var elementAtualizadoEm = doc.DocumentNode.SelectNodes("//*[text()[contains(., 'Atualizado em:')]]");
            if (elementAtualizadoEm != null)
            {
                var dataHoraAtualizacaoTexto = elementAtualizadoEm.Descendants("b").Last().InnerText;
                dataHoraAtualizacao = ParseDateTime(dataHoraAtualizacaoTexto, new string[] { formatDateTime });
            }

            var tables = doc.DocumentNode
                .Descendants("table")
                .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("tabelaPrecoseTaxas")).ToList();

            if (tables != null && tables.Count > 0)
            {
                var precosCompra = tables[0]
                    .Descendants("tr")
                    .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("camposTesouroDireto")).ToList();

                foreach (var umPrecoCompra in precosCompra)
                {
                    var dadosCompra = umPrecoCompra.Descendants("td").Select(o => o.InnerText.Trim()).ToList();

                    if (dadosCompra.Count() == 5)
                    {
                        var dadosTitulo = ConverterInformacaoPrecoTitulo(dataHoraAtualizacao,
                             InformacaoPrecoTituloTipo.Compra,
                             dadosCompra[0],
                             dadosCompra[1],
                             dadosCompra[2],
                             dadosCompra[3],
                             dadosCompra[4]);

                        informacoesPrecoTitulo.Add(dadosTitulo);
                    }
                }
            }

            if (tables != null && tables.Count > 1)
            {
                var precosVenda = tables[1]
                    .Descendants("tr")
                    .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("camposTesouroDireto")).ToList();

                foreach (var umPrecoVenda in precosVenda)
                {
                    var dadosVenda = umPrecoVenda.Descendants("td").Select(o => o.InnerText.Trim()).ToList();

                    if (dadosVenda.Count() == 4)
                    {
                        var dadosTitulo = ConverterInformacaoPrecoTitulo(dataHoraAtualizacao,
                             InformacaoPrecoTituloTipo.Venda,
                             dadosVenda[0],
                             dadosVenda[1],
                             dadosVenda[2],
                             null,
                             dadosVenda[3]);

                        informacoesPrecoTitulo.Add(dadosTitulo);
                    }
                }
            }
        }

        private static InformacaoPrecoTitulo ConverterInformacaoPrecoTitulo(DateTime? dataHoraAtualizacao, InformacaoPrecoTituloTipo tipoInformacao, string titulo, string vencimento, string taxaRendimento, string valorMinimo, string precoUnitario)
        {
            InformacaoPrecoTitulo informacaoPrecoTitulo = null;
            DateTime? dataVencimento = null;
            double valorAux = 0.0;

            var culture = new CultureInfo("pt-BR");

            dataVencimento = ParseDateTime(vencimento, new string[] { formatDate });

            informacaoPrecoTitulo = new InformacaoPrecoTitulo();
            informacaoPrecoTitulo.DataHoraAtualizacao = dataHoraAtualizacao.Value;
            informacaoPrecoTitulo.Vencimento = dataVencimento.Value;
            informacaoPrecoTitulo.Titulo = titulo;
            informacaoPrecoTitulo.TipoInformacao = tipoInformacao;

            if (double.TryParse(taxaRendimento, NumberStyles.Float, culture, out valorAux))
                informacaoPrecoTitulo.TaxaRendimento = valorAux;

            if (double.TryParse(precoUnitario, NumberStyles.Currency, culture, out valorAux))
                informacaoPrecoTitulo.PrecoUnitario = valorAux;

            if (double.TryParse(valorMinimo, NumberStyles.Currency, culture, out valorAux))
                informacaoPrecoTitulo.ValorMinimo = valorAux;
            
            return informacaoPrecoTitulo;
        }

        private static DateTime? ParseDateTime(string dateToParse, string[] formats = null, IFormatProvider provider = null, DateTimeStyles styles = DateTimeStyles.AssumeLocal)
        {
            string[] CUSTOM_DATE_FORMATS = new string[]
                {
                    "yyyyMMddTHHmmssZ",
                    "yyyyMMddTHHmmZ",
                    "yyyyMMddTHHmmss",
                    "yyyyMMddTHHmm",
                    "yyyyMMddHHmmss",
                    "yyyyMMddHHmm",
                    "yyyyMMdd",
                    "yyyy-MM-dd-HH-mm-ss",
                    "yyyy-MM-dd-HH-mm",
                    "yyyy-MM-dd",
                    "MM-dd-yyyy"
                };

            if (formats == null)
            {
                formats = CUSTOM_DATE_FORMATS;
            }

            DateTime validDate;

            foreach (var format in formats)
            {
                if (format.EndsWith("Z"))
                {
                    if (DateTime.TryParseExact(dateToParse, format,
                             provider,
                             DateTimeStyles.AssumeUniversal,
                             out validDate))
                    {
                        return validDate;
                    }
                }
                else
                {
                    if (DateTime.TryParseExact(dateToParse, format,
                             provider, styles, out validDate))
                    {
                        return validDate;
                    }
                }
            }

            return null;
        }
    }
    
    public enum InformacaoPrecoTituloTipo : short
    {
        Compra = 0
        , Venda = 1
    }

    public class InformacaoPrecoTitulo
    {
        public DateTime DataHoraAtualizacao { get; set; }
        public InformacaoPrecoTituloTipo TipoInformacao { get; set; }
        public string Titulo { get; set; }
        public DateTime Vencimento { get; set; }
        public double TaxaRendimento { get; set; }
        public double? ValorMinimo { get; set; }
        public double PrecoUnitario { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1} - {2:P2} - {3:C}", TipoInformacao, Titulo, (TaxaRendimento/100), PrecoUnitario);
        }
    }
}
