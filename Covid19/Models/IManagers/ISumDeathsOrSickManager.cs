using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.IManagers
{
    public interface ISumDeathsOrSickManager
    {
        public IEnumerable<SumDeathsOrSick> GetSumDeaths();
        public IEnumerable<SumDeathsOrSick> GetSumSick();
        public IEnumerable<SumDeathsOrSick> GetSickToday(string dateReported, string country);
        public IEnumerable<SumDeathsOrSick> GetSickCumulative(string dateReported, string country);
        public IEnumerable<SumDeathsOrSick> GetDeathsToday(string dateReported, string country);
        public IEnumerable<SumDeathsOrSick> GetDeathsCumulative(string dateReported, string country);
    }
}
