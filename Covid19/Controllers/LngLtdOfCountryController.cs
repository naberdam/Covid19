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
    public class LngLtdOfCountryController : ControllerBase
    {
        private static ILngLtdOfCountryManager lngLtdOfCountryManager;
        public LngLtdOfCountryController(ILngLtdOfCountryManager iLngLtdOfCountryManager)
        {
            lngLtdOfCountryManager = iLngLtdOfCountryManager;
        }
        // GET: api/LngLtdOfCountry?country=India
        [HttpGet]
        public ActionResult<IEnumerable<LngLtdOfCountry>> GetLngLtdOfCountry([FromQuery] string country)
        {
            IEnumerable<LngLtdOfCountry> listLngLtd = lngLtdOfCountryManager.GetLngAndLtd(country);
            return GlobalFunction.CheckResultAndReturnByGeneric<LngLtdOfCountry>(listLngLtd, NotFound, Ok);
        }
    }
}
