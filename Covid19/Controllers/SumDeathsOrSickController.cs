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
        public ActionResult<IEnumerable<SumDeathsOrSick>> GetSumDeathsOrSick([FromQuery] string deathsOrSick)
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
    }
}
