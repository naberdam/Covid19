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
        // GET: api/CountrySickAndDeathsOrDensity?date=10/10/2020
        // api/CountrySickAndDeathsOrDensity?date=10/10/2020&orderBySickDeathGrowth=DeathsOrder&numYears=20
        [HttpGet]
        public ActionResult<IEnumerable<CountrySickAndDeathsOrDensity>> GetCountrySickAndDeathsOrDensity([FromQuery] string date, [FromQuery] bool withGrowth=false, 
            [FromQuery] string orderBySickDeathGrowth="", [FromQuery] int numYears=-1, [FromQuery] bool desc = false)
        {
            string orderBy = GlobalFunction.ConvertToOrderBy(desc);
            switch (orderBySickDeathGrowth)
            {
                // those cases are for population growth per million
                case "SickOrder":
                    IEnumerable<CountrySickAndDeathsOrDensity> listSick = 
                        countrySickAndDeathsOrDensityManager.GetCountryDeathsAndSickPerMillionAndDensityOrderBySick(orderBy, date, numYears);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountrySickAndDeathsOrDensity>(listSick, NotFound, Ok);
                case "DeathsOrder":
                    IEnumerable<CountrySickAndDeathsOrDensity> listDeaths = 
                        countrySickAndDeathsOrDensityManager.GetCountryDeathsAndSickPerMillionAndDensityOrderByDeaths(orderBy, date, numYears);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountrySickAndDeathsOrDensity>(listDeaths, NotFound, Ok);
                case "PopulationGrowthOrder":
                    IEnumerable<CountrySickAndDeathsOrDensity> listGdp = 
                        countrySickAndDeathsOrDensityManager.GetCountryDeathsAndSickPerMillionAndDensityOrderByGrowth(orderBy, date, numYears);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountrySickAndDeathsOrDensity>(listGdp, NotFound, Ok);
                case "": // this case is for density (people vs space) per million
                    IEnumerable<CountrySickAndDeathsOrDensity> list = 
                        countrySickAndDeathsOrDensityManager.GetCountriesWithDensityWithSickAndDeathsPerMillion(orderBy, date);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountrySickAndDeathsOrDensity>(list, NotFound, Ok);
                default:
                    return BadRequest();
            }
        }
    }
}
