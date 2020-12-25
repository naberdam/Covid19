using Covid19.Helper;
using Covid19.Models.IManagers;
using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.Managers
{
    public class LngLtdOfCountryManager : ILngLtdOfCountryManager
    {
        private MySqlDB mySqlDB;

        public LngLtdOfCountryManager(MySqlDB db)
        {
            mySqlDB = db;
        }

        public IEnumerable<LngLtdOfCountry> GetLngAndLtd(string country)
        {
            List<object[]> listOfAvg = mySqlDB.GetSqlListWithoutParameters("select * from latitude_longitude where Country = '" + country + "'");
            return GlobalFunction.ConvertListObjectByGeneric<LngLtdOfCountry>(listOfAvg, ConvertObjectLngLtdOfCountry);
        }

        public static LngLtdOfCountry ConvertObjectLngLtdOfCountry(object[] infoFromDB)
        {
            try
            {
                return new LngLtdOfCountry
                {
                    Latitude = Convert.ToDouble(infoFromDB[0].ToString()),
                    Longitude = Convert.ToDouble(infoFromDB[1].ToString())
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
