namespace Olympia.Data
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Olympia.Data.Common;

    public class DbQueryRunner : IDbQueryRunner
    {
        public DbQueryRunner(OlympiaDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public OlympiaDbContext Context { get; set; }

        public Task RunQueryAsync(string query, params object[] parameters)
        {
            return this.Context.Database.ExecuteSqlCommandAsync(query, parameters);
        }

        public void Dispose()
        {
            this.Context?.Dispose();
        }
    }
}
