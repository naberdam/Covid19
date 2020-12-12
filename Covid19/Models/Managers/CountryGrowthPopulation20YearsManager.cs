using Covid19.Helper;
using Covid19.Models.IManagers;
using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.Managers
{
    public class CountryGrowthPopulation20YearsManager : ICountryGrowthPopulation20YearsManager
    {
        private MySqlDB mySqlDB;

        public CountryGrowthPopulation20YearsManager(MySqlDB db)
        {
            mySqlDB = db;
        }

        public IEnumerable<CountryGrowthPopulation20Years> GetDivOfAvg(string orderBy, int numYears)
        {
            string countingFromYear = (2020 - numYears).ToString();
            List<object[]> listOfAvg = mySqlDB.GetSqlListWithoutParameters("select distinct pop2020.Location ,pop2020.PopTotal/pop2000.PopTotal dividePopulation " +
                "from (select distinct * from population_worldwide " +
                "where Time = " + countingFromYear + ") pop2000 " +
                "inner join " +
                "(select distinct * " +
                "from population_worldwide " +
                "where Time = 2020) pop2020 " +
                "on pop2020.Location = pop2000.Location " +
                "order by dividePopulation " + orderBy + "");
            return GlobalFunction.ConvertListObjectByGeneric<CountryGrowthPopulation20Years>(listOfAvg, ConvertObjectCountryGrowthPopulation20Years);
        }

        public static CountryGrowthPopulation20Years ConvertObjectCountryGrowthPopulation20Years(object[] infoFromDB)
        {
            try
            {
                return new CountryGrowthPopulation20Years
                {
                    Country = infoFromDB[0].ToString(),
                    DividePopulation = Convert.ToDouble(infoFromDB[1].ToString())
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        
    }
}
