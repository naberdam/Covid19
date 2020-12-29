using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.IManagers
{
    public interface ICountriesDeathsVsDensity2020PerMillionManager
    {
        IEnumerable<CountriesDeathsVsDensity2020PerMillion> GetByDensity(string orderBy, string date);

        IEnumerable<CountriesDeathsVsDensity2020PerMillion> GetByDeaths(string orderBy, string date);

        IEnumerable<CountriesDeathsVsDensity2020PerMillion> GetBySick(string orderBy, string date);
    }
}
