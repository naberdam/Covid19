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
    public class CountriesDeathsAndSickPreMillionBySickController : ControllerBase
    {
        private static ICountriesDeathsAndSickPreMillionBySickManager countriesDeathsAndSickPreMillionBySickManager;
        public CountriesDeathsAndSickPreMillionBySickController(ICountriesDeathsAndSickPreMillionBySickManager iCountriesDeathsAndSickPreMillionBySickManager)
        {
            countriesDeathsAndSickPreMillionBySickManager = iCountriesDeathsAndSickPreMillionBySickManager;
        }
        // GET: api/CountriesDeathsAndSickPreMillionBySick?date=16/11/2020
        [HttpGet]
        public ActionResult<IEnumerable<CountriesDeathsAndSickPreMillionBySick>> GetCountriesDeathsAndSickPreMillionBySick([FromQuery] string date, [FromQuery] bool desc=false)
        {
            string orderBy = GlobalFunction.ConvertToOrderBy(desc);
            IEnumerable<CountriesDeathsAndSickPreMillionBySick> list = countriesDeathsAndSickPreMillionBySickManager.GetBySick(orderBy, date);
            return GlobalFunction.CheckResultAndReturnByGeneric<CountriesDeathsAndSickPreMillionBySick>(list, NotFound, Ok);
        }
    }
}
