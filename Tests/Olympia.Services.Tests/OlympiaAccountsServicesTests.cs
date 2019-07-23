using Microsoft.EntityFrameworkCore;
using Olympia.Data;
using Olympia.Data.Domain;
using System;

namespace Olympia.Services.Tests
{
    public class OlympiaAccountsServicesTests
    {
        private void Initiate()
        {
            DbContextOptionsBuilder<OlympiaDbContext> optionsBuilder = new DbContextOptionsBuilder<OlympiaDbContext>();
            optionsBuilder.UseInMemoryDatabase("test");

            OlympiaDbContext olympiaDbContext = new OlympiaDbContext(optionsBuilder.Options);
        }
        public void RegisterShouldReturnLoggedInUser()
        {
        }
    }
}

