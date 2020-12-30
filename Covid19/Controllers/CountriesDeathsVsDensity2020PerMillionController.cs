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
    public class CountriesDeathsVsDensity2020PerMillionController : ControllerBase
    {
        private static ICountriesDeathsVsDensity2020PerMillionManager countriesDeathsVsDensity2020PerMillionManager;
        public CountriesDeathsVsDensity2020PerMillionController(ICountriesDeathsVsDensity2020PerMillionManager iCountriesDeathsVsDensity2020PerMillionManager)
        {
            countriesDeathsVsDensity2020PerMillionManager = iCountriesDeathsVsDensity2020PerMillionManager;
        }
        // GET: api/CountriesDeathsVsDensity2020PerMillion?densityOrDeath=Density&date=16/11/2020
        [HttpGet]
        public ActionResult<IEnumerable<CountriesDeathsVsDensity2020PerMillion>> GetCountriesDeathsVsDensity2020([FromQuery] string densityOrDeath, [FromQuery] string date, [FromQuery] bool desc = false)
        {
            string orderBy = GlobalFunction.ConvertToOrderBy(desc);
            switch (densityOrDeath)
            {
                case "DensityOrder":
                    IEnumerable<CountriesDeathsVsDensity2020PerMillion> listDensity = countriesDeathsVsDensity2020PerMillionManager.GetByDensity(orderBy, date);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountriesDeathsVsDensity2020PerMillion>(listDensity, NotFound, Ok);
                case "DeathsOrder":
                    IEnumerable<CountriesDeathsVsDensity2020PerMillion> listDeaths = countriesDeathsVsDensity2020PerMillionManager.GetByDeaths(orderBy, date);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountriesDeathsVsDensity2020PerMillion>(listDeaths, NotFound, Ok);
                case "SickOrder":
                    IEnumerable<CountriesDeathsVsDensity2020PerMillion> listSick = countriesDeathsVsDensity2020PerMillionManager.GetBySick(orderBy, date);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountriesDeathsVsDensity2020PerMillion>(listSick, NotFound, Ok);
                /*case "Total":
                    IEnumerable<CountriesDeathsVsDensity2020PerMillion> listTotal = countriesDeathsVsDensity2020.GetByTotal(orderBy, date);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountriesDeathsVsDensity2020PerMillion>(listTotal, NotFound, Ok);*/
                default:
                    return BadRequest();
            }
        }
    }
}
