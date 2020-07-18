using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCsvParser;

namespace FinSharp.PragueStockExchange.Internal
{
    internal class PragueStockExchangeCsvParser
    {
		public PragueStockExchangeCsvParser()
        {

        }

        public IEnumerable<PragueStockExchangeCsvRow> GetDataFromFile(string filePath)
        {
            CsvParserOptions options = new CsvParserOptions(false, ',');
            CsvParserMapping mapping = new CsvParserMapping();
            CsvParser<PragueStockExchangeCsvRow> parser = new CsvParser<PragueStockExchangeCsvRow>(options, mapping);

            return parser.ReadFromFile(filePath, Encoding.ASCII)
                .Select(row => row.Result)
                .ToList();
        }
    }
}
