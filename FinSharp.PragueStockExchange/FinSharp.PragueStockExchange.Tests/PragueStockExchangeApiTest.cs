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
            PragueStockExchangeApiClient client = new PragueStockExchangeApiClient();
            await client.GetData(DateTime.Now);
        }
    }
}
