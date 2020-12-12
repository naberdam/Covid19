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
        // GET: api/CountrySickDeathsAndGdpByGdp
        [HttpGet]
        public ActionResult<IEnumerable<CountrySickDeathsAndGdpByGdp>> GetCountrySickDeathsAndGdpByGdp([FromQuery] string date, [FromQuery] bool desc = false)
        {
            string orderBy = GlobalFunction.ConvertToOrderBy(desc);
            IEnumerable<CountrySickDeathsAndGdpByGdp> listSick =
                        countrySickDeathsAndGdpByGdpManager.GetByGdp(orderBy, date);
            return GlobalFunction.CheckResultAndReturnByGeneric<CountrySickDeathsAndGdpByGdp>(listSick, NotFound, Ok);
        }
    }
}
