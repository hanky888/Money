using System;

namespace Money.Utility.ObjException
{
    [Serializable]
    public class CoinDeskException : Exception
    {
        public CoinDeskException()
        {
        }

        public CoinDeskException(string? message) : base(message)
        {
        }

        public CoinDeskException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}