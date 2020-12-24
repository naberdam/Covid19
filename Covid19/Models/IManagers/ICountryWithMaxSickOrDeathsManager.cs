using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.IManagers
{
    public interface ICountryWithMaxSickOrDeathsManager
    {
        IEnumerable<CountryWithMaxSickOrDeaths> GetCountriesMaxSick(string orderBy);
        IEnumerable<CountryWithMaxSickOrDeaths> GetCountriesMaxDeaths(string orderBy);
    }
}
