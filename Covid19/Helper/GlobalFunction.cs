using Covid19.Models.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Covid19.Helper
{
    public class GlobalFunction
    {
        public static List<T> ConvertListObjectByGeneric<T>(List<object[]> listByDensity, Func<object[], T> func)
        {
            if (listByDensity == null || listByDensity.Count == 0)
            {
                return null;
            }
            List<T> convertedList = new List<T>();
            for (int i = 0; i < listByDensity.Count; i++)
            {
                T countriesDeathsVsDensity2020 = func(listByDensity[i]);
                if (countriesDeathsVsDensity2020 != null)
                {
                    convertedList.Add(countriesDeathsVsDensity2020);
                }
            }
            if (convertedList == null || convertedList.Count == 0)
            {
                return null;
            }
            return convertedList;
        }

        public static ActionResult<IEnumerable<T>> CheckResultAndReturnByGeneric<T>(IEnumerable<T> listToCheck, Func<IEnumerable<T>, ActionResult> notFound, Func<IEnumerable<T>, ActionResult> ok)
        {
            if (listToCheck == null)
            {
                return notFound(listToCheck);
            }
            return ok(listToCheck);
        }

        public static string ConvertToOrderBy(bool orderBy)
        {
            if (orderBy)
            {
                return "desc";
            }
            return "";
        }




    }
}
