using Covid19.Helper;
using Covid19.Models.IManagers;
using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.Managers
{
    public class OneIntVariableManager : IOneIntVariableManager
    {
        private MySqlDB mySqlDB;

        public OneIntVariableManager(MySqlDB db)
        {
            mySqlDB = db;
        }
        public IEnumerable<OneIntVariable> GetSumDeaths()
        {
            List<object[]> listDeaths = mySqlDB.GetSqlListWithoutParameters("select SUM(deaths) as sum_deaths " +
                "from (select distinct MAX(Cumulative_deaths) as deaths from who_covid_19_global_data group by (Country)) sum_deaths");
            return GlobalFunction.ConvertListObjectByGeneric<OneIntVariable>(listDeaths, ConvertObjectSumDeathsOrSick);
        }

        public IEnumerable<OneIntVariable> GetSumSick()
        {
            List<object[]> listSick = mySqlDB.GetSqlListWithoutParameters("select SUM(sick) as sum_sick " +
                "from (select distinct MAX(Cumulative_cases) as sick from who_covid_19_global_data group by (Country)) sum_sick");
            return GlobalFunction.ConvertListObjectByGeneric<OneIntVariable>(listSick, ConvertObjectSumDeathsOrSick);
        }

        public static OneIntVariable ConvertObjectSumDeathsOrSick(object[] infoFromDB)
        {
            try
            {
                return new OneIntVariable
                {
                    Sum = Convert.ToInt32(infoFromDB[0].ToString())
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<OneIntVariable> GetSickToday(string dateReported, string country)
        {
            List<object[]> listSick = mySqlDB.GetSqlListWithoutParameters("select New_cases " +
                "from who_covid_19_global_data " +
                "where Date_reported = '" + dateReported + "' and Country = '" + country +"'");
            return GlobalFunction.ConvertListObjectByGeneric<OneIntVariable>(listSick, ConvertObjectSumDeathsOrSick);
        }

        public IEnumerable<OneIntVariable> GetSickCumulative(string dateReported, string country)
        {
            List<object[]> listSick = mySqlDB.GetSqlListWithoutParameters("select Cumulative_cases " +
                "from who_covid_19_global_data " +
                "where Date_reported = '" + dateReported + "' and Country = '" + country + "'");
            return GlobalFunction.ConvertListObjectByGeneric<OneIntVariable>(listSick, ConvertObjectSumDeathsOrSick);
        }

        public IEnumerable<OneIntVariable> GetDeathsToday(string dateReported, string country)
        {
            List<object[]> listSick = mySqlDB.GetSqlListWithoutParameters("select New_deaths " +
                "from who_covid_19_global_data " +
                "where Date_reported = '" + dateReported + "' and Country = '" + country + "'");
            return GlobalFunction.ConvertListObjectByGeneric<OneIntVariable>(listSick, ConvertObjectSumDeathsOrSick);
        }

        public IEnumerable<OneIntVariable> GetDeathsCumulative(string dateReported, string country)
        {
            List<object[]> listSick = mySqlDB.GetSqlListWithoutParameters("select Cumulative_deaths " +
                "from who_covid_19_global_data " +
                "where Date_reported = '" + dateReported + "' and Country = '" + country + "'");
            return GlobalFunction.ConvertListObjectByGeneric<OneIntVariable>(listSick, ConvertObjectSumDeathsOrSick);
        }
    }
}
