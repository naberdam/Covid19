using Covid19.Helper;
using Covid19.Models.IManagers;
using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models.Managers
{
    public class SpecificCountryAndDateAvgSickManager : ISpecificCountryAndDateAvgSickManager
    {
        private MySqlDB mySqlDB;

        public SpecificCountryAndDateAvgSickManager(MySqlDB db)
        {
            mySqlDB = db;
        }
        public IEnumerable<SpecificCountryAndDateAvgSick> GetDivOfAvg(string orderBy, string country, string date)
        {
            List<object[]> listDivOfAvg = mySqlDB.GetSqlListWithoutParameters("select distinct this_week.Country, avg_sick_this_week/avg_sick_last_week as div_sick_weeks " +
                "from (select Country, AVG(New_cases) avg_sick_this_week " +
                "from who_covid_19_global_data where Country ='" + country +"' " +
                "and str_to_date(who_covid_19_global_data.Date_reported, '%d/%m/%Y') between str_to_date('" + date + "', '%d/%m/%Y')-6 and str_to_date('" + date + "', '%d/%m/%Y') " +
                "group by(Country) order by avg_sick_this_week " + orderBy + ") this_week " +
                "inner join (select Country,  AVG(New_cases) avg_sick_last_week from who_covid_19_global_data where Country = '" + country + "' " +
                "and str_to_date(who_covid_19_global_data.Date_reported, '%d/%m/%Y') between str_to_date('" + date + "', '%d/%m/%Y')-13 and str_to_date('" + date + "', '%d/%m/%Y')-7 " +
                "group by(Country) " +
                "order by(avg_sick_last_week) " + orderBy + ") last_week on(this_week.Country = last_week.Country) " +
                "order by(div_sick_weeks) " + orderBy + "");
            return GlobalFunction.ConvertListObjectByGeneric<SpecificCountryAndDateAvgSick>(listDivOfAvg, ConvertObjectAvgSickWeekOfCountryByDate);
        }

        public IEnumerable<SpecificCountryAndDateAvgSick> GetOnlyAvg(string orderBy, string country, string date)
        {
            List<object[]> listOfAvg = mySqlDB.GetSqlListWithoutParameters("select Country, AVG(New_cases) avg_sick_choose_week " +
                "from who_covid_19_global_data where Country = '" + country + "' " +
                "and str_to_date(who_covid_19_global_data.Date_reported, '%d/%m/%Y') between str_to_date('" + date + "', '%d/%m/%Y')-6 " +
                "and str_to_date('" + date + "', '%d/%m/%Y') " +
                "group by(Country) " +
                "order by(avg_sick_choose_week) " + orderBy + "");
            return GlobalFunction.ConvertListObjectByGeneric<SpecificCountryAndDateAvgSick>(listOfAvg, ConvertObjectAvgSickWeekOfCountryByDate);
        }

        public IEnumerable<SpecificCountryAndDateAvgSick> GetOnlyAvg(string orderBy, string date)
        {
            List<object[]> listOfAvg = mySqlDB.GetSqlListWithoutParameters("select Country, AVG(New_cases) avg_sick_this_week " +
                "from who_covid_19_global_data " +
                "where str_to_date(who_covid_19_global_data.Date_reported, '%d/%m/%Y') between str_to_date('" + date + "', '%d/%m/%Y')-6 and str_to_date('" + date + "', '%d/%m/%Y') " +
                "group by(Country) order by avg_sick_this_week " + orderBy + "");
            return GlobalFunction.ConvertListObjectByGeneric<SpecificCountryAndDateAvgSick>(listOfAvg, ConvertObjectAvgSickWeekOfCountryByDate);
        }

        public IEnumerable<SpecificCountryAndDateAvgSick> GetDivOfAvg(string orderBy, string date)
        {
            List<object[]> listDivOfAvg = mySqlDB.GetSqlListWithoutParameters("select distinct this_week.Country, avg_sick_this_week/avg_sick_last_week as div_sick_weeks " +
                "from (select Country, AVG(New_cases) avg_sick_this_week from who_covid_19_global_data " +
                "where str_to_date(who_covid_19_global_data.Date_reported, '%d/%m/%Y') between str_to_date('" + date + "', '%d/%m/%Y')-6 and str_to_date('" + date + "', '%d/%m/%Y') " +
                "group by(Country) order by avg_sick_this_week " + orderBy + ") this_week " +
                "inner join (select Country,  AVG(New_cases) avg_sick_last_week from who_covid_19_global_data " +
                "where str_to_date(who_covid_19_global_data.Date_reported, '%d/%m/%Y') between str_to_date('" + date + "', '%d/%m/%Y')-13 and str_to_date('" + date + "', '%d/%m/%Y')-7 group by(Country) " +
                "order by(avg_sick_last_week) " + orderBy + ") last_week on(this_week.Country = last_week.Country) " +
                "order by(div_sick_weeks) " + orderBy + "");
            return GlobalFunction.ConvertListObjectByGeneric<SpecificCountryAndDateAvgSick>(listDivOfAvg, ConvertObjectAvgSickWeekOfCountryByDate);
        }

        public static SpecificCountryAndDateAvgSick ConvertObjectAvgSickWeekOfCountryByDate(object[] infoFromDB)
        {
            try
            {
                return new SpecificCountryAndDateAvgSick
                {
                    Country = infoFromDB[0].ToString(),
                    AvgSick = Convert.ToDouble(infoFromDB[1].ToString())
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
        
    }
}
