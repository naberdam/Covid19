using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.IManagers
{
    public interface ICountrySickAndDeathsAndGrowthManager
    {
        IEnumerable<CountrySickAndDeathsAndGrowth> GetCountryDeathsAndSickAndGrowthOrderBySick(string orderBy, string date, int numYears);
        IEnumerable<CountrySickAndDeathsAndGrowth> GetCountryDeathsAndSickAndGrowthOrderByDeaths(string orderBy, string date, int numYears);

        IEnumerable<CountrySickAndDeathsAndGrowth> GetCountryDeathsAndSickAndGrowthOrderByGrowth(string orderBy, string date, int numYears);
    }
}
