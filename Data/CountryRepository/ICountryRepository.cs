using System.Threading.Tasks;

namespace Data
{
    public interface ICountryRepository
    {
        Task<string[]> GetMatchedCountries(string searchTerm, int maxNumberOfFoundCountries);
    }
}