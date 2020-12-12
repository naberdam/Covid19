using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.IManagers
{
    public interface ICountriesDeathsVsDensity2020Manager
    {
        IEnumerable<CountriesDeathsVsDensity2020> GetByDensity(string orderBy);

        IEnumerable<CountriesDeathsVsDensity2020> GetByDeaths(string orderBy);

        IEnumerable<CountriesDeathsVsDensity2020> GetBySick(string orderBy);
    }
}
