using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.IManagers
{
    public interface ICountrySickAndDeathsOrDensityManager
    {
        IEnumerable<CountrySickAndDeathsOrDensity> GetCountriesWithDensityWithSickAndDeathsPerMillion(string orderBy, string date);
        IEnumerable<CountrySickAndDeathsOrDensity> GetCountryDeathsAndSickPerMillionAndDensityOrderBySick(string orderBy, string date, int numYears);
        IEnumerable<CountrySickAndDeathsOrDensity> GetCountryDeathsAndSickPerMillionAndDensityOrderByDeaths(string orderBy, string date, int numYears);

        IEnumerable<CountrySickAndDeathsOrDensity> GetCountryDeathsAndSickPerMillionAndDensityOrderByGrowth(string orderBy, string date, int numYears);
    }
}
