using Covid19.Helper;
using Covid19.Models.IManagers;
using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.Managers
{
    public class CountriesSickOrDeathsThisDayManager : ICountriesSickOrDeathsThisDayManager
    {
        private MySqlDB mySqlDB;

        public CountriesSickOrDeathsThisDayManager(MySqlDB db)
        {
            mySqlDB = db;
        }

        public IEnumerable<CountriesSickOrDeathsThisDay> GetCountriesMaxSickThisDay(string dateReported)
        {
            List<object[]> listBySick = mySqlDB.GetSqlListWithoutParameters("select Country, New_cases from who_covid_19_global_data " +
                "where Date_reported='" + dateReported + "' and New_cases =" +
                "( select MAX(New_cases) from who_covid_19_global_data where Date_reported='" + dateReported + "')");
            return GlobalFunction.ConvertListObjectByGeneric<CountriesSickOrDeathsThisDay>(listBySick, ConvertObjectCountriesSickOrDeathsThisDay);
        }

        public IEnumerable<CountriesSickOrDeathsThisDay> GetCountriesMaxDeathsThisDay(string dateReported)
        {
            List<object[]> listByDeaths = mySqlDB.GetSqlListWithoutParameters("select Country, New_deaths from who_covid_19_global_data " +
                "where Date_reported='" + dateReported + "' and New_deaths =" +
                "( select MAX(New_deaths) from who_covid_19_global_data where Date_reported='" + dateReported + "')");
            return GlobalFunction.ConvertListObjectByGeneric<CountriesSickOrDeathsThisDay>(listByDeaths, ConvertObjectCountriesSickOrDeathsThisDay);
        }

        public static CountriesSickOrDeathsThisDay ConvertObjectCountriesSickOrDeathsThisDay(object[] infoFromDB)
        {
            try
            {
                return new CountriesSickOrDeathsThisDay
                {
                    Country = infoFromDB[0].ToString(),
                    NewCases = Convert.ToInt32(infoFromDB[1].ToString())
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
