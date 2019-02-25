using System;

namespace FinSharp.Api.Entities
{
    public enum DividendStatus
    {
        ESTIMATED,
        ANNOUNCED,
        PAYED
    }

    public partial class Dividend
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public DividendStatus Status { get; set; }
    }
}
