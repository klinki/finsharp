using FinSharp.PragueStockExchange;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            // await client.GetInvestmentRecordsAsync(DateTime.Now);
        }
    }
}
