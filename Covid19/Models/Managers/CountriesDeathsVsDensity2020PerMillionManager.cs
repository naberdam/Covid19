using Covid19.Helper;
using Covid19.Models.IManagers;
using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.Managers
{
    public class CountriesDeathsVsDensity2020PerMillionManager : ICountriesDeathsVsDensity2020PerMillionManager
    {
        private MySqlDB mySqlDB;

        public CountriesDeathsVsDensity2020PerMillionManager(MySqlDB db)
        {
            mySqlDB = db;
        }
        public IEnumerable<CountriesDeathsVsDensity2020PerMillion> GetByDeaths(string orderBy, string date)
        {
            List<object[]> listByDeaths = mySqlDB.GetSqlListWithoutParameters("select distinct Country, ( Cumulative_deaths*1000 / PopTotal) deathPerMillion, ( Cumulative_cases*1000 / PopTotal) sickPerMillion, PopDensity " +
                "from (select distinct * from who_covid_19_global_data where Date_reported = '" + date + "') sick " +
                "inner join (select distinct * from population_worldwide where time = 2020) density on sick.Country = density.Location " +
                "order by sick.Cumulative_deaths " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountriesDeathsVsDensity2020PerMillion>(listByDeaths, ConvertObjectCountriesDeathsVsDensity2020PerMillion);
        }

        public IEnumerable<CountriesDeathsVsDensity2020PerMillion> GetByDensity(string orderBy, string date)
        {
            List<object[]> listByDensity = mySqlDB.GetSqlListWithoutParameters("select distinct Country, ( Cumulative_deaths*1000 / PopTotal) deathPerMillion, ( Cumulative_cases*1000 / PopTotal) sickPerMillion,  PopDensity " +
                "from (select distinct * from who_covid_19_global_data where Date_reported = '" + date + "') sick " +
                "inner join (select distinct * from population_worldwide where time = 2020) density on sick.Country = density.Location " +
                "order by density.PopDensity " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountriesDeathsVsDensity2020PerMillion>(listByDensity, ConvertObjectCountriesDeathsVsDensity2020PerMillion);
        }
        public IEnumerable<CountriesDeathsVsDensity2020PerMillion> GetBySick(string orderBy, string date)
        {
            List<object[]> listBySick = mySqlDB.GetSqlListWithoutParameters("select distinct Country, ( Cumulative_deaths*1000 / PopTotal) deathPerMillion, ( Cumulative_cases*1000 / PopTotal) sickPerMillion, PopDensity " +
                "from (select distinct * from who_covid_19_global_data where Date_reported = '" + date + "') sick " +
                "inner join (select distinct * from population_worldwide where time = 2020) density on sick.Country = density.Location " +
                "order by Cumulative_cases " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountriesDeathsVsDensity2020PerMillion>(listBySick, ConvertObjectCountriesDeathsVsDensity2020PerMillion);
        }
        /*public IEnumerable<CountriesDeathsVsDensity2020PerMillion> GetByTotal(string orderBy, string date)
        {
            List<object[]> listBySick = mySqlDB.GetSqlListWithoutParameters("select distinct Country, Cumulative_cases, Cumulative_deaths, PopDensity " +
                "from (select distinct * from who_covid_19_global_data where Date_reported = '" + date + "') sick " +
                "inner join (select distinct * from population_worldwide where time = 2020) density on sick.Country = density.Location " +
                "order by PopTotal " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountriesDeathsVsDensity2020PerMillion>(listBySick, ConvertObjectCountriesDeathsVsDensity2020);
        }*/

        public static CountriesDeathsVsDensity2020PerMillion ConvertObjectCountriesDeathsVsDensity2020PerMillion(object[] infoFromDB)
        {
            try
            {
                return new CountriesDeathsVsDensity2020PerMillion
                {
                    Country = infoFromDB[0].ToString(),
                    DeathsPerMillion = Convert.ToDouble(infoFromDB[1].ToString()),
                    SickPerMillion = Convert.ToDouble(infoFromDB[2].ToString()),
                    PopDensity = Convert.ToDouble(infoFromDB[3].ToString())
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
