using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : BaseDbContext
    {
        public ApplicationDbContext(IOptions<DatabaseSettings> dbSettings)
            : base(dbSettings)
        {
        }
    }
}
