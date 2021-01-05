using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid19.Helper;
using Covid19.Models.IManagers;
using Covid19.Models.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Covid19
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Add(new ServiceDescriptor(typeof(MySqlDB), new MySqlDB(Configuration.GetConnectionString("DefaultConnection"))));
            services.AddSingleton<ICountriesDeathsVsDensity2020Manager, CountriesDeathsVsDensity2020Manager>();
            services.AddSingleton<ICountriesSickOrDeathsThisDayManager, CountriesSickOrDeathsThisDayManager>();
            services.AddSingleton<ICountryWithMaxSickOrDeathsManager, CountryWithMaxSickOrDeathsManager>();
            services.AddSingleton<IOneIntVariableManager, OneIntVariableManager>();
            services.AddSingleton<ISpecificCountryAndDateAvgSickManager, SpecificCountryAndDateAvgSickManager>();
            services.AddSingleton<ICountryDeathsSickPerMillionWithGdpManager, CountryDeathsSickPerMillionWithGdpManager>(); 
            services.AddSingleton<ICountrySickAndDeathsPerMillionAndGrowthManager, CountrySickAndDeathsPerMillionAndGrowthManager>(); 
            services.AddSingleton<ICountrySickDeathsAndGdpByGdpManager, CountrySickDeathsAndGdpByGdpManager>(); 
            services.AddSingleton<ICountryGrowthPopulation20YearsManager, CountryGrowthPopulation20YearsManager>(); 
            services.AddSingleton<ILngLtdOfCountryManager, LngLtdOfCountryManager>();
            services.AddSingleton<ICountriesDeathsAndSickPerMillionBySickManager, CountriesDeathsAndSickPerMillionBySickManager>();
            services.AddSingleton<ICountriesDeathsVsDensity2020PerMillionManager, CountriesDeathsVsDensity2020PerMillionManager>();
            services.AddSingleton<ICountrySickAndDeathsAndGrowthManager, CountrySickAndDeathsAndGrowthManager>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseFileServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
