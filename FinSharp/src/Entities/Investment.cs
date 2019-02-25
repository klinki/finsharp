using System.Collections.Generic;

namespace FinSharp.Api.Entities
{
    public partial class Investment
    {
        public int Id { get; set; }
        public string ISIN { get; set; }

        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public string Note { get; set; }
        public string Source { get; set; }

        public ICollection<InvestmentRecord> Records { get; set; }
    }
}
