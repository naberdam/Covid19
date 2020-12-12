using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.IManagers
{
    public interface ICountryWithIntVariableManager
    {
        IEnumerable<CountryWithIntVariable> GetCountriesMaxSick(string orderBy);
        IEnumerable<CountryWithIntVariable> GetCountriesMaxDeaths(string orderBy);
    }
}
