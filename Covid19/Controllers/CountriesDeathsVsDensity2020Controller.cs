using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid19.Helper;
using Covid19.Models.IManagers;
using Covid19.Models.ObjectClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Covid19.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesDeathsVsDensity2020Controller : ControllerBase
    {
        private static ICountriesDeathsVsDensity2020Manager countriesDeathsVsDensity2020;
        public CountriesDeathsVsDensity2020Controller(ICountriesDeathsVsDensity2020Manager iCountriesDeathsVsDensity2020)
        {
            countriesDeathsVsDensity2020 = iCountriesDeathsVsDensity2020;
        }

        // GET: api/CountriesDeathsVsDensity2020?densityOrDeath=Density&date=16/11/2020
        [HttpGet]
        public ActionResult<IEnumerable<CountriesDeathsVsDensity2020>> GetCountriesDeathsVsDensity2020([FromQuery] string densityOrDeath,[FromQuery] string date, [FromQuery] bool desc=false)
        {
            string orderBy = GlobalFunction.ConvertToOrderBy(desc);
            switch (densityOrDeath)
            {
                case "Density":
                    IEnumerable<CountriesDeathsVsDensity2020> listDensity = countriesDeathsVsDensity2020.GetByDensity(orderBy, date);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountriesDeathsVsDensity2020>(listDensity, NotFound, Ok);
                case "Deaths":
                    IEnumerable<CountriesDeathsVsDensity2020> listDeaths = countriesDeathsVsDensity2020.GetByDeaths(orderBy, date);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountriesDeathsVsDensity2020>(listDeaths, NotFound, Ok);
                case "Sick":
                    IEnumerable<CountriesDeathsVsDensity2020> listSick = countriesDeathsVsDensity2020.GetBySick(orderBy, date);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountriesDeathsVsDensity2020>(listSick, NotFound, Ok);
                case "Total":
                    IEnumerable<CountriesDeathsVsDensity2020> listTotal = countriesDeathsVsDensity2020.GetByTotal(orderBy, date);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountriesDeathsVsDensity2020>(listTotal, NotFound, Ok);
                default:
                    return BadRequest();
            }           
        }



        /*private ActionResult<IEnumerable<CountriesDeathsVsDensity2020>> CheckResultAndReturn(IEnumerable<CountriesDeathsVsDensity2020> countriesDeathsVsDensity2020)
        {
            if (countriesDeathsVsDensity2020 == null)
            {
                return NotFound(countriesDeathsVsDensity2020);
            }
            return Ok(countriesDeathsVsDensity2020);
        }*/
    }
}
