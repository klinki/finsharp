using System;
using System.Collections.Generic;

namespace FinSharp.Api.Entities
{
    public enum DividendPaymentFrequency
    {
        YEARLY,
        QUARTERLY
    }

    public class Stock : Investment
    {
        public Stock()
        {
            Dividends = new HashSet<Dividend>();
        }

        public string Sector { get; set; }
        public string Subsector { get; set; }
        public string Type { get; set; }
        public DividendPaymentFrequency DividendPaymentFrequency { get; set; }

        public ICollection<Dividend> Dividends { get; set; }
    }
}
