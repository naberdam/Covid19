using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.IManagers
{
    public interface ICountrySickAndDeathsPerMillionAndGrowthManager
    {
        /*IEnumerable<CountrySickAndDeathsOrDensity> GetCountriesWithDensityWithSickAndDeathsPerMillion(string orderBy, string date);*/
        IEnumerable<CountrySickAndDeathsPerMillionAndGrowth> GetCountryDeathsAndSickPerMillionAndGrowthOrderBySick(string orderBy, string date, int numYears);
        IEnumerable<CountrySickAndDeathsPerMillionAndGrowth> GetCountryDeathsAndSickPerMillionAndGrowthOrderByDeaths(string orderBy, string date, int numYears);

        IEnumerable<CountrySickAndDeathsPerMillionAndGrowth> GetCountryDeathsAndSickPerMillionAndGrowthOrderByGrowth(string orderBy, string date, int numYears);
    }
}
