using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;
using Data;
using Microsoft.Extensions.DependencyInjection;

namespace Tests
{
    public class CountryRepositoryTests
    {
        [Fact]
        public async Task GetMatchedCountries_DoesCaseInsensitiveSearch()
        {
            using (var context = new CountriesDbContext(CreateNewContextOptions()))
            {
                Utilities.InitializeDbForTests(context);

                var repository = new CountryRepository(context);
                string[] searchResult = await repository.GetMatchedCountries("ar", 5);

                Assert.Equal(2, searchResult.Length);
                Assert.Contains("Argentina", searchResult);
                Assert.Contains("Armenia", searchResult);
            }
        }

        [Fact]
        public async Task GetMatchedCountries_ReturnsEmptyCollection_IfNotFound()
        {
            using (var context = new CountriesDbContext(CreateNewContextOptions()))
            {
                Utilities.InitializeDbForTests(context);

                var repository = new CountryRepository(context);
                string[] searchResult = await repository.GetMatchedCountries("GGG", 5);

                Assert.Empty(searchResult);
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task GetMatchedCountries_Throws_ForNotValidSearchTerm(string searchTerm)
        {
            using (var context = new CountriesDbContext(CreateNewContextOptions()))
            {
               Utilities.InitializeDbForTests(context);

                var repository = new CountryRepository(context);
                await Assert.ThrowsAsync<ArgumentException>(() => repository.GetMatchedCountries(searchTerm, 2));
            }
        }

        [Fact]
        public async Task GetMatchedCountries_LimitesResultLength_AccordingToTheParam()
        {
            using (var context = new CountriesDbContext(CreateNewContextOptions()))
            {
                Utilities.InitializeDbForTests(context);

                var repository = new CountryRepository(context);
                const int maxNumberOfFoundCountries = 2;
                string[] searchResult = await repository.GetMatchedCountries("a", maxNumberOfFoundCountries);

                Assert.Equal(maxNumberOfFoundCountries, searchResult.Length);
            }
        }

        private static DbContextOptions<CountriesDbContext> CreateNewContextOptions()
        {
             var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<CountriesDbContext>()
                .UseInMemoryDatabase("Test")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}
