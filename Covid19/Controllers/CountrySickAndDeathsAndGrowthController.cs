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
    public class CountrySickAndDeathsAndGrowthController : ControllerBase
    {
        private static ICountrySickAndDeathsAndGrowthManager countrySickAndDeathsAndGrowthManager;
        public CountrySickAndDeathsAndGrowthController(ICountrySickAndDeathsAndGrowthManager iCountrySickAndDeathsAndGrowthManager)
        {
            countrySickAndDeathsAndGrowthManager = iCountrySickAndDeathsAndGrowthManager;
        }
        // GET: api/CountrySickAndDeathsAndGrowth?date=10/10/2020
        //      api/CountrySickAndDeathsAndGrowth?date=10/10/2020&orderBySickDeathGrowth=DeathsOrder&numYears=20
        [HttpGet]
        public ActionResult<IEnumerable<CountrySickAndDeathsAndGrowth>> GetCountrySickAndDeathsOrDensity([FromQuery] string date,
            [FromQuery] string orderBySickDeathGrowth = "", [FromQuery] int numYears = -1, [FromQuery] bool desc = false)
        {
            string orderBy = GlobalFunction.ConvertToOrderBy(desc);
            switch (orderBySickDeathGrowth)
            {
                // those cases are for population growth per million
                case "SickOrder": //in use on popGrowthPerMillion (complex window, populationGrowth per million, order by sick)
                    IEnumerable<CountrySickAndDeathsAndGrowth> listSick =
                        countrySickAndDeathsAndGrowthManager.GetCountryDeathsAndSickAndGrowthOrderBySick(orderBy, date, numYears);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountrySickAndDeathsAndGrowth>(listSick, NotFound, Ok);
                case "DeathsOrder": //in use on popGrowthPerMillion (complex window, populationGrowth per million, order by death)
                    IEnumerable<CountrySickAndDeathsAndGrowth> listDeaths =
                        countrySickAndDeathsAndGrowthManager.GetCountryDeathsAndSickAndGrowthOrderByDeaths(orderBy, date, numYears);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountrySickAndDeathsAndGrowth>(listDeaths, NotFound, Ok);
                case "PopulationGrowthOrder": //in use on popGrowthPerMillion (complex window, populationGrowth per million, order by pop growth)
                    IEnumerable<CountrySickAndDeathsAndGrowth> listGdp =
                        countrySickAndDeathsAndGrowthManager.GetCountryDeathsAndSickAndGrowthOrderByGrowth(orderBy, date, numYears);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountrySickAndDeathsAndGrowth>(listGdp, NotFound, Ok);
                default:
                    return BadRequest();
            }
        }
    }
}
