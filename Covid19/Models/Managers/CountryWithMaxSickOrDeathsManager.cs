using Covid19.Helper;
using Covid19.Models.IManagers;
using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.Managers
{
    public class CountryWithMaxSickOrDeathsManager : ICountryWithMaxSickOrDeathsManager
    {
        private MySqlDB mySqlDB;

        public CountryWithMaxSickOrDeathsManager(MySqlDB db)
        {
            mySqlDB = db;
        }
        public IEnumerable<CountryWithMaxSickOrDeaths> GetCountriesMaxDeaths(string orderBy)
        {
            List<object[]> listCountriesMaxDeaths = mySqlDB.GetSqlListWithoutParameters("select distinct Country,  MAX(Cumulative_deaths) as deaths " +
                "from who_sick.who_sick group by (Country) " +
                "order by deaths " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountryWithMaxSickOrDeaths>(listCountriesMaxDeaths, ConvertObjectCountryWithIntVariable);

        }

        public IEnumerable<CountryWithMaxSickOrDeaths> GetCountriesMaxSick(string orderBy)
        {
            List<object[]> listCountriesMaxSick = mySqlDB.GetSqlListWithoutParameters("select distinct Country,  MAX(Cumulative_cases) as deaths " +
                "from who_sick.who_sick group by (Country) " +
                "order by deaths " + orderBy);
            return GlobalFunction.ConvertListObjectByGeneric<CountryWithMaxSickOrDeaths>(listCountriesMaxSick, ConvertObjectCountryWithIntVariable);
        }

        public static CountryWithMaxSickOrDeaths ConvertObjectCountryWithIntVariable(object[] infoFromDB)
        {
            try
            {
                return new CountryWithMaxSickOrDeaths
                {
                    Country = infoFromDB[0].ToString(),
                    MaxSickOrDeaths = Convert.ToInt32(infoFromDB[1].ToString())
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
