using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public sealed class CountryRepository : ICountryRepository
    {
        private readonly CountriesDbContext _countriesContext;

        public CountryRepository(CountriesDbContext countriesContext)
        {
            _countriesContext = countriesContext;
        }

        public Task<string[]> GetMatchedCountries(string searchTerm, int maxNumberOfFoundCountries)
        {
            if(string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException($"Invalid value of {nameof(searchTerm)}");

            return _countriesContext.Countries
                .Where(country => EF.Functions.ILike(country.Name, $"%{searchTerm}%"))
                .Take(maxNumberOfFoundCountries)
                .Select(country => country.Name)
                .ToArrayAsync();
        }

    }
}