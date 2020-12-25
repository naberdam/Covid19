using Covid19.Helper;
using Covid19.Models.IManagers;
using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.Managers
{
    public class CountrySickDeathsAndGdpByGdpManager : ICountrySickDeathsAndGdpByGdpManager
    {
        private MySqlDB mySqlDB;

        public CountrySickDeathsAndGdpByGdpManager(MySqlDB db)
        {
            mySqlDB = db;
        }
        public IEnumerable<CountrySickDeathsAndGdpByGdp> GetByGdp(string orderBy, string date)
        {
            List<object[]> listOfGrowth = mySqlDB.GetSqlListWithoutParameters("select distinct sick.Country, Cumulative_cases, Cumulative_deaths, gdp.year2020 as GDP " +
                "from (select Country, gdp.year2020 from gdp) gdp " +
                "inner join " +
                "(select * " +
                "from who_covid_19_global_data where Date_reported = '" + date + "') as sick on sick.Country = gdp.Country " +
                "order by GDP " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountrySickDeathsAndGdpByGdp>(listOfGrowth, ConvertObjectCountrySickDeathsAndGdpByGdp);
        }
        public static CountrySickDeathsAndGdpByGdp ConvertObjectCountrySickDeathsAndGdpByGdp(object[] infoFromDB)
        {
            try
            {
                return new CountrySickDeathsAndGdpByGdp
                {
                    Country = infoFromDB[0].ToString(),
                    CumulativeCases = Convert.ToInt32(infoFromDB[1].ToString()),
                    CumulativeDeaths = Convert.ToInt32(infoFromDB[2].ToString()),
                    Gdp = Convert.ToDouble(infoFromDB[3].ToString())
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<CountrySickDeathsAndGdpByGdp> GetByDeaths(string orderBy, string date)
        {
            List<object[]> listOfDeaths = mySqlDB.GetSqlListWithoutParameters("select distinct sick.Country, Cumulative_cases, Cumulative_deaths, gdp.year2020 as GDP " +
                "from (select Country, gdp.year2020 from gdp) gdp " +
                "inner join " +
                "(select * " +
                "from who_covid_19_global_data where Date_reported = '" + date + "') as sick on sick.Country = gdp.Country " +
                "order by Cumulative_deaths " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountrySickDeathsAndGdpByGdp>(listOfDeaths, ConvertObjectCountrySickDeathsAndGdpByGdp);
        }

        public IEnumerable<CountrySickDeathsAndGdpByGdp> GetBySick(string orderBy, string date)
        {
            List<object[]> listOfSick = mySqlDB.GetSqlListWithoutParameters("select distinct sick.Country, Cumulative_cases, Cumulative_deaths, gdp.year2020 as GDP " +
                "from (select Country, gdp.year2020 from gdp) gdp " +
                "inner join " +
                "(select * " +
                "from who_covid_19_global_data where Date_reported = '" + date + "') as sick on sick.Country = gdp.Country " +
                "order by Cumulative_cases " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountrySickDeathsAndGdpByGdp>(listOfSick, ConvertObjectCountrySickDeathsAndGdpByGdp);
        }
    }
}
