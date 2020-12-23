using Covid19.Helper;
using Covid19.Models.IManagers;
using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.Managers
{
    public class CountriesDeathsVsDensity2020Manager : ICountriesDeathsVsDensity2020Manager
    {
        private MySqlDB mySqlDB;

        public CountriesDeathsVsDensity2020Manager(MySqlDB db)
        {
            mySqlDB = db;
        }
        public IEnumerable<CountriesDeathsVsDensity2020> GetByDeaths(string orderBy)
        {
            List<object[]> listByDeaths = mySqlDB.GetSqlListWithoutParameters("select distinct Country, Cumulative_cases, Cumulative_deaths, PopTotal, PopDensity " +
                "from (select distinct * from who_covid_19_global_data where Date_reported = '16/11/2020') sick " +
                "inner join (select distinct * from population_worldwide where time = 2020) density on sick.Country = density.Location " +
                "order by sick.Cumulative_deaths " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountriesDeathsVsDensity2020>(listByDeaths, ConvertObjectCountriesDeathsVsDensity2020);
        }

        public IEnumerable<CountriesDeathsVsDensity2020> GetByDensity(string orderBy)
        {
            List<object[]> listByDensity = mySqlDB.GetSqlListWithoutParameters("select distinct Country, Cumulative_cases, Cumulative_deaths, PopTotal, PopDensity " +
                "from (select distinct * from who_covid_19_global_data where Date_reported = '16/11/2020') sick " +
                "inner join (select distinct * from population_worldwide where time = 2020) density on sick.Country = density.Location " +
                "order by density.PopDensity " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountriesDeathsVsDensity2020>(listByDensity, ConvertObjectCountriesDeathsVsDensity2020);
        }
        public IEnumerable<CountriesDeathsVsDensity2020> GetBySick(string orderBy)
        {
            List<object[]> listBySick = mySqlDB.GetSqlListWithoutParameters("select distinct Country, Cumulative_cases, Cumulative_deaths, PopTotal, PopDensity " +
                "from (select distinct * from who_covid_19_global_data where Date_reported = '16/11/2020') sick " +
                "inner join (select distinct * from population_worldwide where time = 2020) density on sick.Country = density.Location " +
                "order by Cumulative_cases " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountriesDeathsVsDensity2020>(listBySick, ConvertObjectCountriesDeathsVsDensity2020);
        }

        public static CountriesDeathsVsDensity2020 ConvertObjectCountriesDeathsVsDensity2020(object[] infoFromDB)
        {
            try
            {
                return new CountriesDeathsVsDensity2020
                {
                    Country = infoFromDB[0].ToString(),
                    CumulativeCases = Convert.ToInt32(infoFromDB[1].ToString()),
                    CumulativeDeaths = Convert.ToInt32(infoFromDB[2].ToString()),
                    PopTotal = Convert.ToDouble(infoFromDB[3].ToString()),
                    PopDensity = Convert.ToDouble(infoFromDB[4].ToString())
                };
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
