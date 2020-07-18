using FinSharp.Api.Entities;
using FinSharp.PragueStockExchange;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PragueStockExchangeApiTest
{
    [TestClass]
    public class PragueStockExchangeApiTest
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            PragueStockExchangeFinSharpClient client = new PragueStockExchangeFinSharpClient();
            var results = await client.GetInvestmentRecordsAsync(new Investment { Code = "BAAGECBA" }, new DateTime(2018, 7, 27));
            var resultsList = results.ToList();

            Assert.AreEqual(1, resultsList.Count);
        }
    }
}
