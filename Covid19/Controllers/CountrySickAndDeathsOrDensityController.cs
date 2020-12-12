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
    public class CountrySickAndDeathsOrDensityController : ControllerBase
    {
        private static ICountrySickAndDeathsOrDensityManager countrySickAndDeathsOrDensityManager;
        public CountrySickAndDeathsOrDensityController(ICountrySickAndDeathsOrDensityManager iCountrySickAndDeathsOrDensityManager)
        {
            countrySickAndDeathsOrDensityManager = iCountrySickAndDeathsOrDensityManager;
        }
        // GET: api/CountrySickAndDeathsOrDensity
        [HttpGet]
        public ActionResult<IEnumerable<CountrySickAndDeathsOrDensity>> GetCountrySickAndDeathsOrDensity([FromQuery] string date, [FromQuery] bool withGrowth=false, [FromQuery] string orderBySickDeathGrowth="", [FromQuery] int numYears=-1, [FromQuery] bool desc = false)
        {
            string orderBy = GlobalFunction.ConvertToOrderBy(desc);
            switch (orderBySickDeathGrowth)
            {
                case "Sick":
                    IEnumerable<CountrySickAndDeathsOrDensity> listSick = 
                        countrySickAndDeathsOrDensityManager.GetCountryDeathsAndSickPerMillionAndDensityOrderBySick(orderBy, date, numYears);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountrySickAndDeathsOrDensity>(listSick, NotFound, Ok);
                case "Deaths":
                    IEnumerable<CountrySickAndDeathsOrDensity> listDeaths = 
                        countrySickAndDeathsOrDensityManager.GetCountryDeathsAndSickPerMillionAndDensityOrderByDeaths(orderBy, date, numYears);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountrySickAndDeathsOrDensity>(listDeaths, NotFound, Ok);
                case "Growth":
                    IEnumerable<CountrySickAndDeathsOrDensity> listGdp = 
                        countrySickAndDeathsOrDensityManager.GetCountryDeathsAndSickPerMillionAndDensityOrderByGrowth(orderBy, date, numYears);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountrySickAndDeathsOrDensity>(listGdp, NotFound, Ok);
                case "":
                    IEnumerable<CountrySickAndDeathsOrDensity> list = 
                        countrySickAndDeathsOrDensityManager.GetCountriesWithDensityWithSickAndDeathsPerMillion(orderBy, date);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountrySickAndDeathsOrDensity>(list, NotFound, Ok);
                default:
                    return BadRequest();
            }
        }
    }
}
