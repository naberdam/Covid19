using Covid19.Helper;
using Covid19.Models.IManagers;
using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.Managers
{
    public class CountryDeathsSickPerMillionWithGdpManager : ICountryDeathsSickPerMillionWithGdpManager
    {
        private MySqlDB mySqlDB;

        public CountryDeathsSickPerMillionWithGdpManager(MySqlDB db)
        {
            mySqlDB = db;
        }
        public IEnumerable<CountryDeathsSickPerMillionWithGdp> GetDataOrderByDeaths(string orderBy, string date)
        {
            List<object[]> listOfAvg = mySqlDB.GetSqlListWithoutParameters("select perMillion.Country, perMillion.deathPerMillion, perMillion.sickPerMillion, gdp.2020 as GDP " +
                "from " +
                "(select distinct Country, ( Cumulative_deaths*1000/ PopTotal) deathPerMillion, ( Cumulative_cases*1000 / PopTotal) sickPerMillion " +
                "from (select distinct * from who_covid_19_global_data where Date_reported = '" + date + "') sick " +
                "inner join (select distinct * from population_worldwide " +
                "where time = 2020) density on sick.Country = density.Location order by sick.deathPerMillion " + orderBy + ") perMillion " +
                "inner join gdp on gdp.Country = perMillion.Country order by perMillion.deathPerMillion " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountryDeathsSickPerMillionWithGdp>(listOfAvg, ConvertObjectCountryDeathsSickPerMillionWithGdp);
        }

        public IEnumerable<CountryDeathsSickPerMillionWithGdp> GetDataOrderByGdp(string orderBy, string date)
        {
            List<object[]> listOfAvg = mySqlDB.GetSqlListWithoutParameters("select perMillion.Country, perMillion.deathPerMillion, perMillion.sickPerMillion, gdp.2020 as GDP " +
                "from " +
                "(select distinct Country, ( Cumulative_deaths*1000/ PopTotal) deathPerMillion, ( Cumulative_cases*1000 / PopTotal) sickPerMillion " +
                "from (select distinct * from who_covid_19_global_data where Date_reported = '" + date + "') sick " +
                "inner join (select distinct * from population_worldwide " +
                "where time = 2020) density on sick.Country = density.Location order by sick.deathPerMillion " + orderBy + ") perMillion " +
                "inner join gdp on gdp.Country = perMillion.Country order by perMillion.GDP " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountryDeathsSickPerMillionWithGdp>(listOfAvg, ConvertObjectCountryDeathsSickPerMillionWithGdp);
        }

        public IEnumerable<CountryDeathsSickPerMillionWithGdp> GetDataOrderBySick(string orderBy, string date)
        {
            List<object[]> listOfAvg = mySqlDB.GetSqlListWithoutParameters("select perMillion.Country, perMillion.deathPerMillion, perMillion.sickPerMillion, gdp.2020 as GDP " +
                "from " +
                "(select distinct Country, ( Cumulative_deaths*1000/ PopTotal) deathPerMillion, ( Cumulative_cases*1000 / PopTotal) sickPerMillion " +
                "from (select distinct * from who_covid_19_global_data where Date_reported = '" + date + "') sick " +
                "inner join (select distinct * from population_worldwide " +
                "where time = 2020) density on sick.Country = density.Location order by sick.deathPerMillion " + orderBy + ") perMillion " +
                "inner join gdp on gdp.Country = perMillion.Country order by perMillion.sickPerMillion " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountryDeathsSickPerMillionWithGdp>(listOfAvg, ConvertObjectCountryDeathsSickPerMillionWithGdp);
        }

        public static CountryDeathsSickPerMillionWithGdp ConvertObjectCountryDeathsSickPerMillionWithGdp(object[] infoFromDB)
        {
            try
            {
                return new CountryDeathsSickPerMillionWithGdp
                {
                    Country = infoFromDB[0].ToString(),
                    DeathPerMillion = Convert.ToDouble(infoFromDB[1].ToString()),
                    SickPerMillion = Convert.ToDouble(infoFromDB[2].ToString()),
                    Gdp = Convert.ToDouble(infoFromDB[3].ToString())                    
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
