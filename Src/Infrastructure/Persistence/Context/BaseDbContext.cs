using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Data;
using Dapper;

namespace Prototype.Infrastructure.Persistence.Context
{
    public abstract class BaseDbContext
    {
        private readonly DatabaseSettings _dbSettings;

        protected BaseDbContext(IOptions<DatabaseSettings> dbSettings)
            : base()
        {
            _dbSettings = dbSettings.Value;
        }

        // Used by Dapper
        //public IDbConnection Connection => Database.GetDbConnection();

    }
}
