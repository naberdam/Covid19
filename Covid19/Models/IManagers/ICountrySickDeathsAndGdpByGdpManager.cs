using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.IManagers
{
    public interface ICountrySickDeathsAndGdpByGdpManager
    {
        IEnumerable<CountrySickDeathsAndGdpByGdp> GetByGdp(string orderBy, string date);
    }
}
