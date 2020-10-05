using Data;

namespace Tests
{
    public static class Utilities
    {
        public static void InitializeDbForTests(CountriesDbContext context)
        {
            context.Countries.Add(new Country { Name = "Argentina" });
            context.Countries.Add(new Country { Name = "Armenia" });
            context.Countries.Add(new Country { Name = "Russia" });
            context.SaveChanges();
        }
    }
}