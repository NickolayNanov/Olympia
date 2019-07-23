using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Olympia.Data;
using Olympia.Data.Domain;
using Olympia.Services.Contracts;
using Xunit;

namespace Olympia.Services.Tests
{
    public class OlympiaBlogServicesTests
    {
        private OlympiaDbContext olympiaDbContext;

        private void Seed()
        {
            DbContextOptionsBuilder<OlympiaDbContext> optionsBuilder = new DbContextOptionsBuilder<OlympiaDbContext>();
            optionsBuilder.UseInMemoryDatabase("test");

            this.olympiaDbContext = new OlympiaDbContext(optionsBuilder.Options);

            Article article = new Article();


            olympiaDbContext.Articles.AddRange(new[] { new Article(), new Article(), new Article(), new Article() });
        }

        [Fact]
        public async void ClientsAllShouldReturnFourArticles()
        {
            var mock = new Mock<IUsersService>();

            var result = mock.Setup(x => x.CalculateCalories("asd")).Returns(2014);

        }
    }
}
