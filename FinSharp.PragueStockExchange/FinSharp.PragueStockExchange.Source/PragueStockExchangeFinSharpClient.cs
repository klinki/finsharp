using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using FinSharp.Api.Entities;
using FinSharp.Api;
using Flurl.Http;
using System.Linq;
using FinSharp.PragueStockExchange.Internal;

namespace FinSharp.PragueStockExchange
{
    public class PragueStockExchangeApiException : Exception, IInvestmentApiException
    {
        public PragueStockExchangeApiException(): base() { }

        public PragueStockExchangeApiException(string message): base(message) { }
    }

    public interface IPragueStockExchangeApiConfiguration
    {

    }

    public class PragueStockExchangeFinSharpClient : IInvestmentApi
    {
        const string API_URL = "http://ftp.pse.cz/results.ak";
        const string ZIP_FILE_NAME = "pl{DATE}.zip";
        const string RESULT_FILE_NAME = "AK{DATE}.csv";
        const string DATE_FORMAT = "yyMMdd";
        const int MIN_YEAR = 2013;

        protected string GetFileUrl(DateTime date)
        {
            string url = API_URL;

            if (date.Year != DateTime.Now.Year)
            {
                url += "/" + date.Year.ToString();
            }

            url += "/" + ZIP_FILE_NAME.Replace("{DATE}", date.ToString(DATE_FORMAT));

            return url;
        }

        protected async Task<IEnumerable<PragueStockExchangeCsvRow>> GetData(DateTime date)
        {
            string url = GetFileUrl(date);
            string filePath = await url.DownloadFileAsync(Path.GetTempPath());

            string requiredFile = RESULT_FILE_NAME.Replace("{DATE}", date.ToString(DATE_FORMAT));
            string destinationPath = Path.GetFullPath(Path.Combine(Path.GetTempPath(), requiredFile));

            using (ZipArchive archive = ZipFile.OpenRead(filePath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.Name == requiredFile)
                    {
                        if (File.Exists(destinationPath))
                        {
                            continue;
                        }

                        entry.ExtractToFile(destinationPath);
                    }
                }
            }

            PragueStockExchangeCsvParser parser = new PragueStockExchangeCsvParser();
            return parser.GetDataFromFile(destinationPath);
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
            ValidateDate(date);
            var result = await GetData(date);

            return result.Select(x => TransformData(x));
        }

        public async Task<IEnumerable<InvestmentRecord>> GetInvestmentRecordsAsync(Investment investment, DateTime date)
        {
            ValidateDate(date);
            var result = await GetData(date);

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

        protected void ValidateDate(DateTime date)
        {
            if (date > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(nameof(date), $"Cannot be in future");
            }

            if (date.Year < MIN_YEAR)
            {
                throw new ArgumentOutOfRangeException(nameof(date), $"Minimal year is ${MIN_YEAR}");
            }
        }

        protected void ValidateDates(DateTime from, DateTime to)
        {
            if (from > to)
            {
                throw new ArgumentOutOfRangeException(nameof(from), $"Cannot be after {nameof(to)}");
            }

            if (to > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(nameof(to), $"Cannot be in future");
            }

            if (from.Year < MIN_YEAR)
            {
                throw new ArgumentOutOfRangeException(nameof(from), $"Minimal year is ${MIN_YEAR}");
            }
        }
    }
}
