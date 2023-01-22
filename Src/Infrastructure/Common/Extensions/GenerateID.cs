using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Infrastructure.Common.Extensions
{
    public static class GenerateID
    {
        public static string Getid()
        {
            DateTimeOffset dto = new DateTimeOffset(DateTime.UtcNow);
            Random rnd = new Random();
            return dto.ToUnixTimeMilliseconds().ToString() + rnd.Next(1, 99).ToString().PadLeft(2, '0');
        }

    }
}
