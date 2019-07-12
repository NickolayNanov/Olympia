namespace Olympia.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(OlympiaDbContext dbContext, IServiceProvider serviceProvider);
    }
}
