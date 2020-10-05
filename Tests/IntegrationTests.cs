using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class IntegrationTests : IClassFixture<CustomWebApplicationFactory<Web.Startup>>
    {
        private readonly CustomWebApplicationFactory<Web.Startup> _factory;

        public IntegrationTests(CustomWebApplicationFactory<Web.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
        {
            HttpClient client = _factory.CreateClient();

            HttpResponseMessage response = await client.GetAsync("index?suggestions=a");

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}