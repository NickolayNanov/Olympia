using Microsoft.EntityFrameworkCore;
using Olympia.Data;
using Olympia.Data.Domain;
using Olympia.Data.Seeding;
using Xunit;

namespace Olympia.Services.Tests
{
    public class OlympiaAccountsServicesTests
    {
        private void InitiateInMemmoryDb()
        {
            DbContextOptionsBuilder<OlympiaDbContext> optionsBuilder = new DbContextOptionsBuilder<OlympiaDbContext>();
            optionsBuilder.UseInMemoryDatabase("test");

            OlympiaDbContext olympiaDbContext = new OlympiaDbContext(optionsBuilder.Options);

            new DataSeeder(olympiaDbContext);
        }

    }
}

