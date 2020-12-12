using Covid19.Helper;
using Covid19.Models.IManagers;
using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.Managers
{
    public class CountrySickAndDeathsOrDensityManager : ICountrySickAndDeathsOrDensityManager
    {
        private MySqlDB mySqlDB;

        public CountrySickAndDeathsOrDensityManager(MySqlDB db)
        {
            mySqlDB = db;
        }
        public IEnumerable<CountrySickAndDeathsOrDensity> GetCountriesWithDensityWithSickAndDeathsPerMillion(string orderBy, string date)
        {
            List<object[]> list = mySqlDB.GetSqlListWithoutParameters("select distinct Country, PopDensity, Cumulative_cases, Cumulative_deaths,( Cumulative_deaths / PopTotal) deathPerMillion, ( Cumulative_cases / PopTotal) sickPerMillion, PopTotal " +
                "from (select distinct * " +
                "from who_covid_19_global_data " +
                "where Date_reported = '" + date + "') sick " +
                "inner join " +
                "(select distinct * " +
                "from population_worldwide " +
                "where time = 2020) " +
                "density on sick.Country = density.Location " +
                "order by sick.Cumulative_deaths " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountrySickAndDeathsOrDensity>(list, ConvertObjectCountrySickAndDeathsOrDensity);
        }

        public IEnumerable<CountrySickAndDeathsOrDensity> GetCountryDeathsAndSickPerMillionAndDensityOrderByDeaths(string orderBy, string date, int numYears)
        {
            string countingFromYear = (2020 - numYears).ToString();
            List<object[]> listOfDeaths = mySqlDB.GetSqlListWithoutParameters("select Country, growth, Cumulative_cases, Cumulative_deaths,( Cumulative_deaths*1000 / PopTotal) deathPerMillion, ( Cumulative_cases*1000 / PopTotal) sickPerMillion, PopTotal*1000 " +
                "from " +
                "(select distinct * from who_covid_19_global_data where Date_reported = '" + date + "') sick " +
                "inner join (select distinct t1.Location,  t1.PopTotal/ t2.PopTotal as growth, t1.PopDensity, t1.PopTotal PopTotal " +
                "from population_worldwide t1 ,population_worldwide t2 " +
                "where t1.Location = t2.Location and t1.Time = '2020' and t2.Time = " + countingFromYear + ") " +
                "density on sick.Country = density.Location order by deathPerMillion " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountrySickAndDeathsOrDensity>(listOfDeaths, ConvertObjectCountrySickAndDeathsOrDensity);
        }

        public IEnumerable<CountrySickAndDeathsOrDensity> GetCountryDeathsAndSickPerMillionAndDensityOrderByGrowth(string orderBy, string date, int numYears)
        {
            string countingFromYear = (2020 - numYears).ToString();
            List<object[]> listOfGrowth = mySqlDB.GetSqlListWithoutParameters("select Country, growth, Cumulative_cases, Cumulative_deaths,( Cumulative_deaths*1000 / PopTotal) deathPerMillion, ( Cumulative_cases*1000 / PopTotal) sickPerMillion, PopTotal*1000 " +
                "from " +
                "(select distinct * from who_covid_19_global_data where Date_reported = '" + date + "') sick " +
                "inner join (select distinct t1.Location,  t1.PopTotal/ t2.PopTotal as growth, t1.PopDensity, t1.PopTotal PopTotal " +
                "from population_worldwide t1 ,population_worldwide t2 " +
                "where t1.Location = t2.Location and t1.Time = '2020' and t2.Time = " + countingFromYear + ") " +
                "density on sick.Country = density.Location order by growth " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountrySickAndDeathsOrDensity>(listOfGrowth, ConvertObjectCountrySickAndDeathsOrDensity);
        }

        public IEnumerable<CountrySickAndDeathsOrDensity> GetCountryDeathsAndSickPerMillionAndDensityOrderBySick(string orderBy, string date, int numYears)
        {
            string countingFromYear = (2020 - numYears).ToString();
            List<object[]> listOfSick = mySqlDB.GetSqlListWithoutParameters("select Country, growth, Cumulative_cases, Cumulative_deaths,( Cumulative_deaths*1000 / PopTotal) deathPerMillion, ( Cumulative_cases*1000 / PopTotal) sickPerMillion, PopTotal*1000 " +
                "from " +
                "(select distinct * from who_covid_19_global_data where Date_reported = '" + date + "') sick " +
                "inner join (select distinct t1.Location,  t1.PopTotal/ t2.PopTotal as growth, t1.PopDensity, t1.PopTotal PopTotal " +
                "from population_worldwide t1 ,population_worldwide t2 " +
                "where t1.Location = t2.Location and t1.Time = '2020' and t2.Time = " + countingFromYear + ") " +
                "density on sick.Country = density.Location order by sickPerMillion " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountrySickAndDeathsOrDensity>(listOfSick, ConvertObjectCountrySickAndDeathsOrDensity);
        }

        public static CountrySickAndDeathsOrDensity ConvertObjectCountrySickAndDeathsOrDensity(object[] infoFromDB)
        {
            try
            {
                return new CountrySickAndDeathsOrDensity
                {
                    Country = infoFromDB[0].ToString(),
                    PopDensity = Convert.ToDouble(infoFromDB[1].ToString()),
                    CumulativeCases = Convert.ToInt32(infoFromDB[2].ToString()),
                    CumulativeDeaths = Convert.ToInt32(infoFromDB[3].ToString()),
                    DeathPerMillion = Convert.ToDouble(infoFromDB[4].ToString()),
                    SickPerMillion = Convert.ToDouble(infoFromDB[5].ToString()),
                    PopTotal = Convert.ToDouble(infoFromDB[6].ToString())
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
