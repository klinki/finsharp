using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using FinSharp.Api.Entities;
using FinSharp.Api;
using Flurl;
using Flurl.Http;
using TinyCsvParser;
using System.Text;
using System.Linq;

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

    public class PragueStockExchangeApiClient : IInvestmentApi
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

        public async Task<IEnumerable<PragueStockExchangeCsvRow>> GetData(DateTime date)
        {
            if (date.Year < MIN_YEAR)
            {
                throw new PragueStockExchangeApiException($"Cannot download data prior to year ${MIN_YEAR}");
            }

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

            CsvParserOptions options = new CsvParserOptions(false, ',');
            CsvParserMapping mapping = new CsvParserMapping();
            CsvParser<PragueStockExchangeCsvRow> parser = new CsvParser<PragueStockExchangeCsvRow>(options, mapping);

            var result = parser.ReadFromFile(destinationPath, Encoding.ASCII).ToList();
            return result.Select(x => x.Result);
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

        public async Task<IEnumerable<InvestmentRecord>> GetInvestmentRecordsAsync()
        {
            var result = await GetData(DateTime.Now);
            return result.Select(x => new InvestmentRecord
            {
                Date = x.Date,
                High = x.DayMax,
                Low = x.DayMin,
                Open = x.Open,
                Close = x.Close,
                Volume = x.Volume
            });
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
