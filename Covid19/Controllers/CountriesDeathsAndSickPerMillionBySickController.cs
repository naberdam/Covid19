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
    public class CountriesDeathsAndSickPerMillionBySickController : ControllerBase
    {
        private static ICountriesDeathsAndSickPerMillionBySickManager countriesDeathsAndSickPreMillionBySickManager;
        public CountriesDeathsAndSickPerMillionBySickController(ICountriesDeathsAndSickPerMillionBySickManager iCountriesDeathsAndSickPreMillionBySickManager)
        {
            countriesDeathsAndSickPreMillionBySickManager = iCountriesDeathsAndSickPreMillionBySickManager;
        }
        // GET: api/CountriesDeathsAndSickPreMillionBySick?date=16/11/2020
        [HttpGet]
        public ActionResult<IEnumerable<CountriesDeathsAndSickPerMillionBySick>> GetCountriesDeathsAndSickPreMillionBySick([FromQuery] string date, [FromQuery] bool desc=false)
        {
            string orderBy = GlobalFunction.ConvertToOrderBy(desc);
            IEnumerable<CountriesDeathsAndSickPerMillionBySick> list = countriesDeathsAndSickPreMillionBySickManager.GetBySick(orderBy, date);
            return GlobalFunction.CheckResultAndReturnByGeneric<CountriesDeathsAndSickPerMillionBySick>(list, NotFound, Ok);
        }
    }
}
