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
    public class OneIntVariableController : ControllerBase
    {
        private static IOneIntVariableManager sumDeathsOrSickManager;
        public OneIntVariableController(IOneIntVariableManager iSumDeathsOrSickManager)
        {
            sumDeathsOrSickManager = iSumDeathsOrSickManager;
        }
        // GET: api/SumDeathsOrSick
        [HttpGet]
        public ActionResult<IEnumerable<OneIntVariable>> GetSumDeathsOrSick([FromQuery] string deathsOrSick, [FromQuery] string date="", [FromQuery] string country="")
        {
            if (date == "" && country == "")
            {
                switch (deathsOrSick)
                {
                    case "Sick":  //in use on onSubmitSpecificCountrySpecificDateDeathOrSick (main windows, down query allOver)
                        IEnumerable<OneIntVariable> listSick = sumDeathsOrSickManager.GetSumSick(); 
                        return GlobalFunction.CheckResultAndReturnByGeneric<OneIntVariable>(listSick, NotFound, Ok);
                    case "Deaths": //in use on onSubmitSpecificCountrySpecificDateDeathOrSick (main windows, down query allOver)
                        IEnumerable<OneIntVariable> listDeaths = sumDeathsOrSickManager.GetSumDeaths();
                        return GlobalFunction.CheckResultAndReturnByGeneric<OneIntVariable>(listDeaths, NotFound, Ok);
                    default:
                        return BadRequest();
                }
            }
            else if (date != "" && country != "")
            {
                switch (deathsOrSick)
                {
                    case "ThisDaySick": //in use on onSubmitSpecificCountrySpecificDateDeathOrSick (main windows, down query on)
                        IEnumerable<OneIntVariable> listSick = sumDeathsOrSickManager.GetSickToday(date, country);
                        return GlobalFunction.CheckResultAndReturnByGeneric<OneIntVariable>(listSick, NotFound, Ok);
                    case "ThisDayDeaths": //in use on onSubmitSpecificCountrySpecificDateDeathOrSick (main windows, down query on)
                        IEnumerable<OneIntVariable> listDeaths = sumDeathsOrSickManager.GetDeathsToday(date, country);
                        return GlobalFunction.CheckResultAndReturnByGeneric<OneIntVariable>(listDeaths, NotFound, Ok);
                    case "CumulativeDeaths": //in use on onSubmitSpecificCountrySpecificDateDeathOrSick (main windows, down query until)
                        IEnumerable<OneIntVariable> listCumulativeDeaths = sumDeathsOrSickManager.GetDeathsCumulative(date, country);
                        return GlobalFunction.CheckResultAndReturnByGeneric<OneIntVariable>(listCumulativeDeaths, NotFound, Ok);
                    case "CumulativeSick": //in use on onSubmitSpecificCountrySpecificDateDeathOrSick (main windows, down query until)
                        IEnumerable<OneIntVariable> listCumulativeSick = sumDeathsOrSickManager.GetSickCumulative(date, country);
                        return GlobalFunction.CheckResultAndReturnByGeneric<OneIntVariable>(listCumulativeSick, NotFound, Ok);
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
