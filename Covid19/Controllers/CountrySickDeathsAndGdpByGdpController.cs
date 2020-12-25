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
    public class CountrySickDeathsAndGdpByGdpController : ControllerBase
    {
        private static ICountrySickDeathsAndGdpByGdpManager countrySickDeathsAndGdpByGdpManager;
        public CountrySickDeathsAndGdpByGdpController(ICountrySickDeathsAndGdpByGdpManager iCountrySickDeathsAndGdpByGdpManager)
        {
            countrySickDeathsAndGdpByGdpManager = iCountrySickDeathsAndGdpByGdpManager;
        }
        // GET: api/CountrySickDeathsAndGdpByGdp?date=10/10/2020&gdpSickOrDeaths=GdpOrder
        [HttpGet]
        public ActionResult<IEnumerable<CountrySickDeathsAndGdpByGdp>> GetCountrySickDeathsAndGdpByGdp([FromQuery] string date, [FromQuery] string gdpSickOrDeaths, [FromQuery] bool desc = false)
        {
            string orderBy = GlobalFunction.ConvertToOrderBy(desc);
            switch (gdpSickOrDeaths)
            {
                case "GdpOrder":  //in use on onSubmitSpecificCountrySpecificDateDeathOrSick (complex window, gdp col, order gdp, total)
                    IEnumerable<CountrySickDeathsAndGdpByGdp> listGdp = countrySickDeathsAndGdpByGdpManager.GetByGdp(orderBy, date);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountrySickDeathsAndGdpByGdp>(listGdp, NotFound, Ok);
                case "DeathsOrder": //in use on onSubmitSpecificCountrySpecificDateDeathOrSick (complex window, gdp col, order sick total)
                    IEnumerable<CountrySickDeathsAndGdpByGdp> listDeaths = countrySickDeathsAndGdpByGdpManager.GetByDeaths(orderBy, date);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountrySickDeathsAndGdpByGdp>(listDeaths, NotFound, Ok);
                case "SickOrder": //in use on onSubmitSpecificCountrySpecificDateDeathOrSick (complex window, gdp col, order sick total)
                    IEnumerable<CountrySickDeathsAndGdpByGdp> listSick = countrySickDeathsAndGdpByGdpManager.GetBySick(orderBy, date);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountrySickDeathsAndGdpByGdp>(listSick, NotFound, Ok);
                default:
                    return BadRequest();
            }
        }
    }
}
