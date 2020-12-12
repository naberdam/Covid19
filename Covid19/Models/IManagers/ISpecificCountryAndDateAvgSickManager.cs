using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.IManagers
{
    public interface ISpecificCountryAndDateAvgSickManager
    {
        IEnumerable<SpecificCountryAndDateAvgSick> GetDivOfAvg(string orderBy, string country, string date);
        IEnumerable<SpecificCountryAndDateAvgSick> GetOnlyAvg(string orderBy, string country, string date);
        IEnumerable<SpecificCountryAndDateAvgSick> GetOnlyAvg(string orderBy, string date);
        IEnumerable<SpecificCountryAndDateAvgSick> GetDivOfAvg(string orderBy, string date);
    }
}
