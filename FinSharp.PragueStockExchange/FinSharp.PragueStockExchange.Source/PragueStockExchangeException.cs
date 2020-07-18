using FinSharp.Api;
using System;
using System.Runtime.Serialization;

namespace FinSharp.PragueStockExchange
{
    [Serializable]
    class DataNotFoundException : PragueStockExchangeException
    {
        public DataNotFoundException()
        {
        }

        public DataNotFoundException(string message) : base(message)
        {
        }

        public DataNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DataNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    class PragueStockExchangeException : InvestmentApiException
    {
        public PragueStockExchangeException()
        {
        }

        public PragueStockExchangeException(string message) : base(message)
        {
        }

        public PragueStockExchangeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PragueStockExchangeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
