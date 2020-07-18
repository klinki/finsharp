using FinSharp.PragueStockExchange;
using FinSharp.PragueStockExchange.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace PragueStockExchangeApiTest
{
    [TestClass]
    public class CsvParserTest
    {
        [TestMethod]
        [DeploymentItem("Data/valid-input.csv")]
        public void TestValidParsing()
        {
            // test with invariant culture
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            string filePath = "Parser/Data/valid-input.csv";

            PragueStockExchangeCsvParser parser = new PragueStockExchangeCsvParser();
            var resultList = parser.GetDataFromFile(filePath).ToList();

            Assert.AreEqual(4, resultList.Count);

            var expectedList = GetData();

            var expectedResultPairs = expectedList.Zip(resultList, (expectedRow, resultRow) => new
            {
                Expected = expectedRow,
                Result = resultRow
            });

            foreach (var pair in expectedResultPairs)
            {
                var expected = pair.Expected;
                var result = pair.Result;

                foreach (PropertyInfo property in expected.GetType().GetProperties())
                {
                    Assert.AreEqual(property.GetValue(expected), property.GetValue(result), $"Property: {property.Name} is not equal");
                }
            }
        }

        private List<PragueStockExchangeCsvRow> GetData()
        {
            return new List<PragueStockExchangeCsvRow>
            {
                new PragueStockExchangeCsvRow
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
                },
                new PragueStockExchangeCsvRow
                {
                    ISIN = "CZ0008040318",
                    Name = "MONETA MONEY BANK",
                    BIC = "BAAGECBA",
                    Date = new DateTime(2019, 04, 12),
                    Close = 80.50m,
                    Change = 0.12m,
                    Previous = 80.40m,
                    YearMin = 71.50m,
                    YearMax = 86.00m,
                    Volume = 1190302,
                    TradedAmount = 95658697.25m,
                    LastTrade = new DateTime(2019, 04, 12),
                    MarketGroup = "T",
                    Mode = "2",
                    MarketCode = "O",
                    DayMin = 80.20m,
                    DayMax = 80.50m,
                    Open = 80.25m,
                    LotSize = 1
                },
                new PragueStockExchangeCsvRow
                {
                    ISIN = "CZ0008040318",
                    Name = "MONETA MONEY BANK",
                    BIC = "BAAGECBA",
                    Date = new DateTime(2020, 02, 07),
                    Close = 86.00m,
                    Change = -0.29m,
                    Previous = 86.25m,
                    YearMin = 70.20m,
                    YearMax = 86.90m,
                    Volume = 577459,
                    TradedAmount = 49702237.40m,
                    LastTrade = new DateTime(2020, 02, 07),
                    MarketGroup = "T",
                    Mode = "2",
                    MarketCode = "O",
                    DayMin = 85.80m,
                    DayMax = 86.35m,
                    Open = 86.15m,
                    LotSize = 1
                },
                new PragueStockExchangeCsvRow
                {
                    ISIN = "CS0008418869",
                    Name = "PHILIP MORRIS ČR",
                    BIC = "BAATABAK",
                    Date = new DateTime(2020, 07, 17),
                    Close = 13340.00m,
                    Change = 0m,
                    Previous = 13340.00m,
                    YearMin = 12100.00m,
                    YearMax = 15540.00m,
                    Volume = 845,
                    TradedAmount = 11320060.00m,
                    LastTrade = new DateTime(2020, 07, 17),
                    MarketGroup = "T",
                    Mode = "2",
                    MarketCode = "F",
                    DayMin = 13340.00m,
                    DayMax = 13440.00m,
                    Open = 13440.00m,
                    LotSize = 1  
                }
            };
        }

        [TestMethod]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void TestParsingMissingDirectory()
        {
            string filePath = "/this/is/some/wrong/path";

            PragueStockExchangeCsvParser parser = new PragueStockExchangeCsvParser();
            var result = parser.GetDataFromFile(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestParsingMissingFile()
        {
            string filePath = Path.Combine(Path.GetTempPath(), "blablablafile.csv");

            PragueStockExchangeCsvParser parser = new PragueStockExchangeCsvParser();
            var result = parser.GetDataFromFile(filePath);
        }
    }
}
