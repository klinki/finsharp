using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinSharp.Api.Entities;
using FinSharp.Api;
using System.Linq;

namespace FinSharp.PragueStockExchange
{
    public interface IPragueStockExchangeApiConfiguration
    {

    }

    public class PragueStockExchangeFinSharpClient : IInvestmentApi
    {
        private readonly PragueStockExchangeApiClient _client;

        public PragueStockExchangeFinSharpClient(): this(new PragueStockExchangeApiClient())
        {

        }

        public PragueStockExchangeFinSharpClient(PragueStockExchangeApiClient client)
        {
            _client = client;
        }

        public IEnumerable<InvestmentRecord> GetInvestmentRecords()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InvestmentRecord> GetInvestmentRecords(DateTime from, DateTime to)
        {
            ValidateDates(from, to);

            throw new NotImplementedException();
        }

        public IEnumerable<InvestmentRecord> GetInvestmentRecords(Investment investment)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InvestmentRecord> GetInvestmentRecords(Investment investment, DateTime from, DateTime to)
        {
            ValidateDates(from, to);

            throw new NotImplementedException();
        }

        public Task<IEnumerable<InvestmentRecord>> GetInvestmentRecordsAsync(Investment investment, DateTime from, DateTime to)
        {
            ValidateDates(from, to);

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InvestmentRecord>> GetInvestmentRecordsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InvestmentRecord>> GetInvestmentRecordsAsync(DateTime from, DateTime to)
        {
            ValidateDates(from, to);
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InvestmentRecord>> GetInvestmentRecordsAsync(Investment investment)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InvestmentRecord>> GetInvestmentRecordsAsync(DateTime date)
        {
            _client.ValidateDate(date);
            var result = await _client.GetData(date);

            return result.Select(x => TransformData(x));
        }

        public async Task<IEnumerable<InvestmentRecord>> GetInvestmentRecordsAsync(Investment investment, DateTime date)
        {
            _client.ValidateDate(date);
            var result = await _client.GetData(date);

            return result.Where(x => x.BIC == investment.Code).Select(x => TransformData(x));
        }

        protected InvestmentRecord TransformData(PragueStockExchangeCsvRow row)
        {
            return new InvestmentRecord
            {
                Date = row.Date,
                High = row.DayMax,
                Low = row.DayMin,
                Open = row.Open,
                Close = row.Close,
                Volume = row.Volume
            };
        }

        protected void ValidateDates(DateTime from, DateTime to)
        {
            if (from > to)
            {
                throw new ArgumentOutOfRangeException(nameof(from), $"Cannot be after {nameof(to)}");
            }

            _client.ValidateDate(from);
            _client.ValidateDate(to);
        }
    }
}
