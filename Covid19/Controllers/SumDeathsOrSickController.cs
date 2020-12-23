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
    public class SumDeathsOrSickController : ControllerBase
    {
        private static ISumDeathsOrSickManager sumDeathsOrSickManager;
        public SumDeathsOrSickController(ISumDeathsOrSickManager iSumDeathsOrSickManager)
        {
            sumDeathsOrSickManager = iSumDeathsOrSickManager;
        }
        // GET: api/SumDeathsOrSick
        [HttpGet]
        public ActionResult<IEnumerable<SumDeathsOrSick>> GetSumDeathsOrSick([FromQuery] string deathsOrSick, [FromQuery] string date="", [FromQuery] string country="")
        {
            if (date == "" && country == "")
            {
                switch (deathsOrSick)
                {
                    case "Sick":
                        IEnumerable<SumDeathsOrSick> listSick = sumDeathsOrSickManager.GetSumSick();
                        return GlobalFunction.CheckResultAndReturnByGeneric<SumDeathsOrSick>(listSick, NotFound, Ok);
                    case "Deaths":
                        IEnumerable<SumDeathsOrSick> listDeaths = sumDeathsOrSickManager.GetSumDeaths();
                        return GlobalFunction.CheckResultAndReturnByGeneric<SumDeathsOrSick>(listDeaths, NotFound, Ok);
                    default:
                        return BadRequest();
                }
            }
            else if (date != "" && country != "")
            {
                switch (deathsOrSick)
                {
                    case "Sick":
                        IEnumerable<SumDeathsOrSick> listSick = sumDeathsOrSickManager.GetSickToday(date, country);
                        return GlobalFunction.CheckResultAndReturnByGeneric<SumDeathsOrSick>(listSick, NotFound, Ok);
                    case "Deaths":
                        IEnumerable<SumDeathsOrSick> listDeaths = sumDeathsOrSickManager.GetDeathsToday(date, country);
                        return GlobalFunction.CheckResultAndReturnByGeneric<SumDeathsOrSick>(listDeaths, NotFound, Ok);
                    case "CumulativeDeaths":
                        IEnumerable<SumDeathsOrSick> listCumulativeDeaths = sumDeathsOrSickManager.GetDeathsCumulative(date, country);
                        return GlobalFunction.CheckResultAndReturnByGeneric<SumDeathsOrSick>(listCumulativeDeaths, NotFound, Ok);
                    case "CumulativeSick":
                        IEnumerable<SumDeathsOrSick> listCumulativeSick = sumDeathsOrSickManager.GetSickCumulative(date, country);
                        return GlobalFunction.CheckResultAndReturnByGeneric<SumDeathsOrSick>(listCumulativeSick, NotFound, Ok);
                    default:
                        return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
