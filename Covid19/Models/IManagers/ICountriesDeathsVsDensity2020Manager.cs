using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.IManagers
{
    public interface ICountriesDeathsVsDensity2020Manager
    {
        IEnumerable<CountriesDeathsVsDensity2020> GetByDensity(string orderBy, string date);

        IEnumerable<CountriesDeathsVsDensity2020> GetByDeaths(string orderBy, string date);

        IEnumerable<CountriesDeathsVsDensity2020> GetBySick(string orderBy, string date);
        public IEnumerable<CountriesDeathsVsDensity2020> GetByTotal(string orderBy, string date);
    }
}
