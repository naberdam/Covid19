using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.IManagers
{
    public interface IOneIntVariableManager
    {
        public IEnumerable<OneIntVariable> GetSumDeaths();
        public IEnumerable<OneIntVariable> GetSumSick();
        public IEnumerable<OneIntVariable> GetSickToday(string dateReported, string country);
        public IEnumerable<OneIntVariable> GetSickCumulative(string dateReported, string country);
        public IEnumerable<OneIntVariable> GetDeathsToday(string dateReported, string country);
        public IEnumerable<OneIntVariable> GetDeathsCumulative(string dateReported, string country);
    }
}
