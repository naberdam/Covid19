using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.IManagers
{
    public interface ICountryGrowthPopulation20YearsManager
    {
        IEnumerable<CountryGrowthPopulation20Years> GetDivOfAvg(string orderBy, int numYears);
    }
}
