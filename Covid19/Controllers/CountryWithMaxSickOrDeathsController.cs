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
    public class CountryWithMaxSickOrDeathsController : ControllerBase
    {
        private static ICountryWithMaxSickOrDeathsManager countriesWithIntVariable;
        public CountryWithMaxSickOrDeathsController(ICountryWithMaxSickOrDeathsManager iCountriesWithIntVariable)
        {
            countriesWithIntVariable = iCountriesWithIntVariable;
        }

        // GET: api/CountryWithIntVariable/5
        [HttpGet]
        public ActionResult<IEnumerable<CountryWithMaxSickOrDeaths>> GetCountryWithIntVariable([FromQuery] string nameOfQuery, [FromQuery] bool desc = false)
        {
            string orderBy = GlobalFunction.ConvertToOrderBy(desc);
            switch (nameOfQuery)
            {
                case "CountriesMaxSick":
                    IEnumerable<CountryWithMaxSickOrDeaths> listCountriesMaxSick = countriesWithIntVariable.GetCountriesMaxSick(orderBy);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountryWithMaxSickOrDeaths>(listCountriesMaxSick, NotFound, Ok);
                case "CountriesMaxDeaths":
                    IEnumerable<CountryWithMaxSickOrDeaths> listCountriesMaxDeaths = countriesWithIntVariable.GetCountriesMaxDeaths(orderBy);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountryWithMaxSickOrDeaths>(listCountriesMaxDeaths, NotFound, Ok);
                default:
                    return BadRequest();
            }
        }
    }
}
