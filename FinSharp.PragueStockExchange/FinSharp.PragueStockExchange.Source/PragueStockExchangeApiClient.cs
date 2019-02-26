using FinSharp.PragueStockExchange.Internal;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace FinSharp.PragueStockExchange.Source
{
    public interface IPragueStockExchangeApiConfiguration
    {

    }

    public class PragueStockExchangeApiClient
    {
        private PragueStockExchangeCsvParser _parser;

        const string API_URL = "http://ftp.pse.cz/results.ak";
        const string ZIP_FILE_NAME = "pl{DATE}.zip";
        const string RESULT_FILE_NAME = "AK{DATE}.csv";
        const string DATE_FORMAT = "yyMMdd";
        const int MIN_YEAR = 2013;

        internal PragueStockExchangeApiClient(PragueStockExchangeCsvParser parser)
        {
            _parser = parser;
        }

        public PragueStockExchangeApiClient(): this(new PragueStockExchangeCsvParser())
        { }

        public async Task<IEnumerable<PragueStockExchangeCsvRow>> GetData(DateTime date)
        {
            ValidateDate(date);

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
    }
}
