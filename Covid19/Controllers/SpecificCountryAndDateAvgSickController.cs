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
    public class SpecificCountryAndDateAvgSickController : ControllerBase
    {
        private static ISpecificCountryAndDateAvgSickManager avgSickWeekOfCountryByDateManager;
        public SpecificCountryAndDateAvgSickController(ISpecificCountryAndDateAvgSickManager iAvgSickWeekOfCountryByDateManager)
        {
            avgSickWeekOfCountryByDateManager = iAvgSickWeekOfCountryByDateManager;
        }
        // GET: api/SpecificCountryAndDateAvgSick?divOrAvg=Avg&country=India&date=30/04/2020
        // api/SpecificCountryAndDateAvgSick?divOrAvg=Avg&date=30/04/2020
        [HttpGet]
        public ActionResult<IEnumerable<SpecificCountryAndDateAvgSick>> GetAvgSickWeekOfCountryByDate([FromQuery] string divOrAvg, [FromQuery] string date,
            [FromQuery] string country="", [FromQuery] bool desc =false)
        {
            string orderBy = GlobalFunction.ConvertToOrderBy(desc);
            // if this is query of specific country
            if (country != "")
            {
                switch (divOrAvg)
                {
                    case "Avg":
                        IEnumerable<SpecificCountryAndDateAvgSick> listAvg = avgSickWeekOfCountryByDateManager.GetOnlyAvg(orderBy, country, date);
                        return GlobalFunction.CheckResultAndReturnByGeneric<SpecificCountryAndDateAvgSick>(listAvg, NotFound, Ok);
                    case "Div":
                        IEnumerable<SpecificCountryAndDateAvgSick> listDiv = avgSickWeekOfCountryByDateManager.GetDivOfAvg(orderBy, country, date);
                        return GlobalFunction.CheckResultAndReturnByGeneric<SpecificCountryAndDateAvgSick>(listDiv, NotFound, Ok);
                    default:
                        return BadRequest();
                }
            }
            // not specific country
            if (date != null)
            {
                switch (divOrAvg)
                {
                    case "Avg":
                        IEnumerable<SpecificCountryAndDateAvgSick> listAvg = avgSickWeekOfCountryByDateManager.GetOnlyAvg(orderBy, date);
                        return GlobalFunction.CheckResultAndReturnByGeneric<SpecificCountryAndDateAvgSick>(listAvg, NotFound, Ok);
                    case "Div":
                        IEnumerable<SpecificCountryAndDateAvgSick> listDiv = avgSickWeekOfCountryByDateManager.GetDivOfAvg(orderBy, date);
                        return GlobalFunction.CheckResultAndReturnByGeneric<SpecificCountryAndDateAvgSick>(listDiv, NotFound, Ok);
                    default:
                        return BadRequest();
                }
            }
            return BadRequest();
        }
    }
}
