using System;

namespace FinSharp.Api.Entities
{
    public class InvestmentRecord
    {
        public string Ticker { get; set; }

        public DateTime Date { get; set; }

        public decimal? Open { get; set; }
        public decimal Close { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public long Volume { get; set; }
    }
}
