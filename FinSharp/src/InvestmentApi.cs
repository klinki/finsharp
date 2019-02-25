using FinSharp.Api.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinSharp.Api
{
    public interface IInvestmentApi
    {
        IEnumerable<InvestmentRecord> GetInvestmentRecords();
        IEnumerable<InvestmentRecord> GetInvestmentRecords(DateTime from, DateTime to);
        IEnumerable<InvestmentRecord> GetInvestmentRecords(Investment investment);
        IEnumerable<InvestmentRecord> GetInvestmentRecords(Investment investment, DateTime from, DateTime to);

        Task<IEnumerable<InvestmentRecord>> GetInvestmentRecordsAsync();
        Task<IEnumerable<InvestmentRecord>> GetInvestmentRecordsAsync(DateTime from, DateTime to);
        Task<IEnumerable<InvestmentRecord>> GetInvestmentRecordsAsync(Investment investment);
        Task<IEnumerable<InvestmentRecord>> GetInvestmentRecordsAsync(Investment investment, DateTime from, DateTime to);
    }
}
