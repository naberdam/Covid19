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
    public class CountrySickAndDeathsPerMillionAndGrowthController : ControllerBase
    {
        private static ICountrySickAndDeathsPerMillionAndGrowthManager countrySickAndDeathsPerMillionAndGrowthManager;
        public CountrySickAndDeathsPerMillionAndGrowthController(ICountrySickAndDeathsPerMillionAndGrowthManager iCountrySickAndDeathsPerMillionAndGrowthManager)
        {
            countrySickAndDeathsPerMillionAndGrowthManager = iCountrySickAndDeathsPerMillionAndGrowthManager;
        }
        // GET: api/CountrySickAndDeathsPerMillionAndGrowth?date=10/10/2020
        // api/CountrySickAndDeathsPerMillionAndGrowth?date=10/10/2020&orderBySickDeathGrowth=DeathsOrder&numYears=20
        [HttpGet]
        public ActionResult<IEnumerable<CountrySickAndDeathsPerMillionAndGrowth>> GetCountrySickAndDeathsOrDensity([FromQuery] string date, 
            [FromQuery] string orderBySickDeathGrowth="", [FromQuery] int numYears=-1, [FromQuery] bool desc = false)
        {
            string orderBy = GlobalFunction.ConvertToOrderBy(desc);
            switch (orderBySickDeathGrowth)
            {
                // those cases are for population growth per million
                case "SickOrder": //in use on popGrowthPerMillion (complex window, populationGrowth per million, order by sick)
                    IEnumerable<CountrySickAndDeathsPerMillionAndGrowth> listSick = 
                        countrySickAndDeathsPerMillionAndGrowthManager.GetCountryDeathsAndSickPerMillionAndGrowthOrderBySick(orderBy, date, numYears);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountrySickAndDeathsPerMillionAndGrowth>(listSick, NotFound, Ok);
                case "DeathsOrder": //in use on popGrowthPerMillion (complex window, populationGrowth per million, order by death)
                    IEnumerable<CountrySickAndDeathsPerMillionAndGrowth> listDeaths =  
                        countrySickAndDeathsPerMillionAndGrowthManager.GetCountryDeathsAndSickPerMillionAndGrowthOrderByDeaths(orderBy, date, numYears);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountrySickAndDeathsPerMillionAndGrowth>(listDeaths, NotFound, Ok);
                case "PopulationGrowthOrder": //in use on popGrowthPerMillion (complex window, populationGrowth per million, order by pop growth)
                    IEnumerable<CountrySickAndDeathsPerMillionAndGrowth> listGdp = 
                        countrySickAndDeathsPerMillionAndGrowthManager.GetCountryDeathsAndSickPerMillionAndGrowthOrderByGrowth(orderBy, date, numYears);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountrySickAndDeathsPerMillionAndGrowth>(listGdp, NotFound, Ok);
                default:
                    return BadRequest();
            }
        }
    }
}
