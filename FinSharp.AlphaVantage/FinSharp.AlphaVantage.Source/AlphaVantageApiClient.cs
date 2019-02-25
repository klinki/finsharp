using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FinSharp.Api;
using FinSharp.Api.Entities;
using Flurl;
using Flurl.Http;

namespace FinSharp.AlphaVantage
{
    public interface IAlphaVantageConfiguration
    {
        string ApiKey { get; set; }
    }

    public class AlphaVantageApiClient : IInvestmentApi
    {
        public IAlphaVantageConfiguration Configuration { get; private set; }

        public AlphaVantageApiClient(IAlphaVantageConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IEnumerable<InvestmentRecord> GetInvestmentRecords()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InvestmentRecord> GetInvestmentRecords(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InvestmentRecord> GetInvestmentRecords(Investment investment)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InvestmentRecord> GetInvestmentRecords(Investment investment, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InvestmentRecord>> GetInvestmentRecordsAsync(Investment investment, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InvestmentRecord>> GetInvestmentRecordsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InvestmentRecord>> GetInvestmentRecordsAsync(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InvestmentRecord>> GetInvestmentRecordsAsync(Investment investment)
        {
            throw new NotImplementedException();
        }
    }
}
