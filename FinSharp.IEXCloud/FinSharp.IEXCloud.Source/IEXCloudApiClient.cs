using FinSharp.Api;
using FinSharp.Api.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace FinSharp.IEXCloud
{
    public interface IEXCloudApiClientConfiguration
    {
        string ApiKey { get; set; }
    }

    public class IEXCloudApiClient : IInvestmentApi
    {
        public IEXCloudApiClientConfiguration Configuration { get; private set; }


        public IEXCloudApiClient(IEXCloudApiClientConfiguration configuration)
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

        public Task<IEnumerable<InvestmentRecord>> GetInvestmentRecordsAsync(Investment investment, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}
