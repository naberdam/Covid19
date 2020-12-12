using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.IManagers
{
    public interface ICountryDeathsSickPerMillionWithGdpManager
    {
        IEnumerable<CountryDeathsSickPerMillionWithGdp> GetDataOrderByGdp(string orderBy, string date);
        IEnumerable<CountryDeathsSickPerMillionWithGdp> GetDataOrderByDeaths(string orderBy, string date);
        IEnumerable<CountryDeathsSickPerMillionWithGdp> GetDataOrderBySick(string orderBy, string date);
    }
}
