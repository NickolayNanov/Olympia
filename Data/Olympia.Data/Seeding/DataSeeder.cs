namespace Olympia.Data.Seeding
{
    using System.Linq;
    using Olympia.Data.Domain;

    public class DataSeeder
    {
        private readonly OlympiaDbContext context;

        public DataSeeder(OlympiaDbContext context)
        {
            this.context = context;
        }

        public void SeedAsync()
        {
            if (!this.context.Roles.Any())
            {
                this.context.Roles.Add(new OlympiaUserRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" });
                this.context.Roles.Add(new OlympiaUserRole { Name = "Trainer", NormalizedName = "TRAINER" });
                this.context.Roles.Add(new OlympiaUserRole { Name = "Client", NormalizedName = "CLIENT" });

                this.context.SaveChanges();
            }
        }
    }
}
