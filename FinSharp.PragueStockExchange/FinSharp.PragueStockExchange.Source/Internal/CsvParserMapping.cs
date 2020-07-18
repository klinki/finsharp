using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

[assembly: InternalsVisibleTo("FinSharp.PragueStockExchange.Tests")]
namespace FinSharp.PragueStockExchange.Internal
{
    internal class TrimmingStringConverter : ITypeConverter<string>
    {
        public Type TargetType => typeof(string);

        public bool TryConvert(string value, out string result)
        {
            bool isValueIsNotNull = value != null;
            result = string.Empty;

            if (isValueIsNotNull)
            {
                result = value.Trim();
            }

            return isValueIsNotNull;
        }
    }

    internal class CustomDecimalConverter : ITypeConverter<decimal>
    {
        public Type TargetType => typeof(decimal);
        private readonly NumberFormatInfo _nfi = new NumberFormatInfo { CurrencyDecimalSeparator = "." };

        public bool TryConvert(string value, out decimal result)
        {
            if (value == ".00")
            {
                result = 0;
                return true;
            }

            return decimal.TryParse(value, NumberStyles.Currency, _nfi, out result);
        }
    }

    internal class CsvParserMapping : CsvMapping<PragueStockExchangeCsvRow>
    {
        public CsvParserMapping(): base()
        {
            var trimmingStringConverter = new TrimmingStringConverter();
            var dateTimeConverter = new DateTimeConverter("yyyy/MM/dd");
            var customDecimalConverter = new CustomDecimalConverter();

            MapProperty(0, x => x.ISIN, trimmingStringConverter); // Example: "AT0000A1AVN7",
            MapProperty(1, x => x.Name, trimmingStringConverter); // Example: "EB GLD TL9        ",
            MapProperty(2, x => x.BIC, trimmingStringConverter); // Example: "        ",
            MapProperty(3, x => x.Date, dateTimeConverter); // "2019/01/10",
            MapProperty(4, x => x.Close, customDecimalConverter); // 383.81         ,
            MapProperty(5, x => x.Change, customDecimalConverter); // -.03     ,
            MapProperty(6, x => x.Previous, customDecimalConverter); // 383.94         ,
            MapProperty(7, x => x.YearMin, customDecimalConverter); // 182.91         ,
            MapProperty(8, x => x.YearMax, customDecimalConverter); // 597.31         ,
            MapProperty(9, x => x.Volume);  // 0            ,
            MapProperty(10, x => x.TradedAmount, customDecimalConverter); // .00                ,
            MapProperty(11, x => x.LastTrade, dateTimeConverter); // "2018/12/28",
            MapProperty(12, x => x.MarketGroup); // "E",
            MapProperty(13, x => x.Mode); // "2",
            MapProperty(14, x => x.MarketCode); // "F",

            MapProperty(15, x => x.DayMin, customDecimalConverter); // .00            ,
            MapProperty(16, x => x.DayMax, customDecimalConverter); // .00            ,
            MapProperty(17, x => x.Open, customDecimalConverter); // 395.12         ,

            MapProperty(18, x => x.LotSize); // 1      
        }
    }
}
