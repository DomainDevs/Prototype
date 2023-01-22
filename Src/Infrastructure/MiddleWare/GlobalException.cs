using System;
using System.Globalization;

namespace Prototype.Infrastructure.MiddleWare
{
    public class GlobalException : Exception
    {
        public GlobalException() : base()
        {
        }

        public GlobalException(string message) : base(message)
        {
        }

        public GlobalException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
