using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IConfiguration _configuration;

        public IndexModel(ICountryRepository countryRepository, IConfiguration configuration)
        {
            _countryRepository = countryRepository;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetSuggestionsAsync(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return new JsonResult(new string[0]);

            int maxNumberOfFoundCountries = int.Parse(_configuration["MaxNumberOfFoundCountries"]);
            string[] countries = await _countryRepository.GetMatchedCountries(term, maxNumberOfFoundCountries);

            return new JsonResult(countries);
        }
    }
}
