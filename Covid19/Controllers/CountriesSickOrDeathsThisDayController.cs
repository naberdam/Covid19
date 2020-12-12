using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
    public class CountriesSickOrDeathsThisDayController : ControllerBase
    {
        private static ICountriesSickOrDeathsThisDayManager countriesSickOrDeathsThisDay;
        public CountriesSickOrDeathsThisDayController(ICountriesSickOrDeathsThisDayManager iCountriesSickOrDeathsThisDay)
        {
            countriesSickOrDeathsThisDay = iCountriesSickOrDeathsThisDay;
        }

        // GET: api/CountriesSickOrDeathsThisDay?sickOrDeath=Deaths&dateReported=04/05/2020
        [HttpGet]
        public ActionResult<IEnumerable<CountriesSickOrDeathsThisDay>> GetCountriesSickOrDeathsThisDay([FromQuery] string sickOrDeath, [FromQuery] string dateReported)
        {
            string urlRequest = Request.QueryString.Value;
            string patternUrl = @"^(\?sickOrDeath=Deaths)+&(dateReported=(\d{2})\/(\d{2})\/(\d{4}))$";
            if (!Regex.IsMatch(urlRequest, patternUrl))
            {
                return BadRequest();
            }
            string patternDateReported = @"^(\d{2})\/(\d{2})\/(\d{4})?";
            if (!Regex.IsMatch(dateReported, patternDateReported))
            {
                return BadRequest();
            }
            switch (sickOrDeath)
            {
                case "Deaths":
                    IEnumerable<CountriesSickOrDeathsThisDay> listDeaths = countriesSickOrDeathsThisDay.GetCountriesMaxDeathsThisDay(dateReported);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountriesSickOrDeathsThisDay>(listDeaths, NotFound, Ok);
                case "Sick":
                    IEnumerable<CountriesSickOrDeathsThisDay> listSick = countriesSickOrDeathsThisDay.GetCountriesMaxSickThisDay(dateReported);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountriesSickOrDeathsThisDay>(listSick, NotFound, Ok);
                default:
                    return BadRequest();
            }
        }

        /*private ActionResult<IEnumerable<CountriesSickOrDeathsThisDay>> CheckResultAndReturn(IEnumerable<CountriesSickOrDeathsThisDay> countriesSickOrDeathsThisDays)
        {
            if (countriesSickOrDeathsThisDay == null)
            {
                return NotFound(countriesSickOrDeathsThisDay);
            }
            return Ok(countriesSickOrDeathsThisDay);
        }*/
    }
}
