using System;

namespace FinSharp.PragueStockExchange
{
    /// <summary>
    /// Entita popisující jednotlivé řádky v CSV z webu BCPP
    /// </summary>
    public class PragueStockExchangeCsvRow
    {
        /// <summary>
        /// Kód cenného papíru (ISIN) A 12
        /// </summary>
        public string ISIN { get; set; }

        /// <summary>
        /// Název cenného papíru A 18
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// BIC A 8
        /// </summary>
        public string BIC { get; set; }

        /// <summary>
        /// Datum burzovního dne D
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Závěrečný kurs N 13 2
        /// </summary>
        public decimal Close { get; set; }

        /// <summary>
        /// Změna dnešní/předchozí v % N 7 2
        /// </summary>
        public decimal Change { get; set; }

        /// <summary>
        /// Předchozí kurs N 13 2
        /// </summary>
        public decimal Previous { get; set; }

        /// <summary>
        /// Minimální kurs za poslední rok N 13 2
        /// </summary>
        public decimal YearMin { get; set; }

        /// <summary>
        /// Maximální kurs za poslední rok N 13 2
        /// </summary>
        public decimal YearMax { get; set; }

        /// <summary>
        /// Počet zobchodovaných c.p. N 11 0
        /// </summary>
        public int Volume { get; set; }

        /// <summary>
        /// Objem obchodů N 17 2
        /// </summary>
        public decimal TradedAmount { get; set; }

        /// <summary>
        /// Datum posledního obchodu D
        /// </summary>
        public DateTime LastTrade { get; set; }

        /// <summary>
        /// Obchodní skupina A 1
        /// </summary>
        public string MarketGroup { get; set; }

        /// <summary>
        /// Mód obchodování A 1
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// Kód trhu A 1
        /// </summary>
        public string MarketCode { get; set; }

        /// <summary>
        /// Denní minimální cena N 13 2
        /// </summary>
        public decimal DayMin { get; set; }

        /// <summary>
        /// Denní maximální cena N 13 2
        /// </summary>
        public decimal DayMax { get; set; }

        /// <summary>
        /// Otevírací cena N 13 2
        /// </summary>
        public decimal Open { get; set; }

        /// <summary>
        /// Velikost lotu N 5 0
        /// </summary>
        public int LotSize { get; set; }
    }
}
