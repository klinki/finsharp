using FinSharp.PragueStockExchange;
using FinSharp.PragueStockExchange.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PragueStockExchangeApiTest
{
    [TestClass]
    public class CsvParserTest
    {
        [TestMethod]
        [DeploymentItem("Data/valid-input.csv")]
        public void TestValidParsing()
        {
            string filePath = "Parser/Data/valid-input.csv";

            PragueStockExchangeCsvParser parser = new PragueStockExchangeCsvParser();
            var result = parser.GetDataFromFile(filePath).ToList();

            Assert.AreEqual(1, result.Count);

            PragueStockExchangeCsvRow expected = new PragueStockExchangeCsvRow
            {
                ISIN = "CZ0009000121",
                Name = "KOFOLA ČS",
                BIC = "BABKOFOL",
                Date = new DateTime(2019, 2, 10),
                Close = 303.00m,
                Change = 0.66m,
                Previous = 301.00m,
                YearMin = 268.00m,
                YearMax = 418.00m,
                Volume = 1505,
                TradedAmount = 454131.00m,
                LastTrade = new DateTime(2019, 2, 10),
                MarketGroup = "T",
                Mode = "2",
                MarketCode = "F",
                DayMin = 301.00m,
                DayMax = 304.00m,
                Open = 302.00m,
                LotSize = 1
            };

            foreach (PropertyInfo property in expected.GetType().GetProperties())
            {
                Assert.AreEqual(property.GetValue(expected), property.GetValue(result[0]), $"Property: {property.Name} is not equal");
            }
        }

        [TestMethod]
        public void TestParsingMissingDirectory()
        {
            string filePath = "/this/is/some/wrong/path";

            PragueStockExchangeCsvParser parser = new PragueStockExchangeCsvParser();
            var result = parser.GetDataFromFile(filePath);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestParsingMissingFile()
        {
            string filePath = Path.Combine(Path.GetTempPath(), "blablablafile.csv");

            PragueStockExchangeCsvParser parser = new PragueStockExchangeCsvParser();
            var result = parser.GetDataFromFile(filePath);

            Assert.IsTrue(true);
        }
    }
}
