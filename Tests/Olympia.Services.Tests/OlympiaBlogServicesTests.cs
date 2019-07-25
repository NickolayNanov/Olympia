namespace Olympia.Services.Tests
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Olympia.Data;
    using Olympia.Data.Models.BindingModels.Blogs;
    using Olympia.Data.Seeding;
    using System.Threading.Tasks;
    using Xunit;

    public class OlympiaBlogServicesTests
    {
        private void InitiateInMemmoryDb()
        {
            DbContextOptionsBuilder<OlympiaDbContext> optionsBuilder = new DbContextOptionsBuilder<OlympiaDbContext>();
            optionsBuilder.UseInMemoryDatabase("test");

            OlympiaDbContext olympiaDbContext = new OlympiaDbContext(optionsBuilder.Options);

            new DataSeeder(olympiaDbContext);
        }


        [Fact]
        public async Task CreateArticleShouldCreateObject()
        {
            InitiateInMemmoryDb();
            ;
            var mockMapper = new Mock<IMapper>();
            var mockedService = new Mock<BlogServices>().Object;

            var aricle = await mockedService.CreateArticleAsync(new CreateArticleBindingModel() { Title = "asd", Content = "asd", ImgUrl = null }, "Pesho");

            Assert.NotNull(aricle);
        }
    }
}
