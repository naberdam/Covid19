using Covid19.Helper;
using Covid19.Models.IManagers;
using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.Managers
{
    public class SumDeathsOrSickManager : ISumDeathsOrSickManager
    {
        private MySqlDB mySqlDB;

        public SumDeathsOrSickManager(MySqlDB db)
        {
            mySqlDB = db;
        }
        public IEnumerable<SumDeathsOrSick> GetSumDeaths()
        {
            List<object[]> listDeaths = mySqlDB.GetSqlListWithoutParameters("select SUM(deaths) as sum_deaths " +
                "from (select distinct MAX(Cumulative_deaths) as deaths from who_covid_19_global_data group by (Country)) sum_deaths");
            return GlobalFunction.ConvertListObjectByGeneric<SumDeathsOrSick>(listDeaths, ConvertObjectSumDeathsOrSick);
        }

        public IEnumerable<SumDeathsOrSick> GetSumSick()
        {
            List<object[]> listSick = mySqlDB.GetSqlListWithoutParameters("select SUM(sick) as sum_sick " +
                "from (select distinct MAX(Cumulative_cases) as sick from who_covid_19_global_data group by (Country)) sum_sick");
            return GlobalFunction.ConvertListObjectByGeneric<SumDeathsOrSick>(listSick, ConvertObjectSumDeathsOrSick);
        }

        public static SumDeathsOrSick ConvertObjectSumDeathsOrSick(object[] infoFromDB)
        {
            try
            {
                return new SumDeathsOrSick
                {
                    Sum = Convert.ToInt32(infoFromDB[0].ToString())
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
