using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddDbContext<CountriesDbContext>(options => options
                .UseNpgsql(
                    Configuration.GetConnectionString("AliExpressDb"),
                    o => o.MigrationsAssembly("Data")));
            services.AddTransient<ICountryRepository, CountryRepository>();
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            CountriesDbContext dbContext)
        {
            if (dbContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                dbContext.Database.Migrate();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
