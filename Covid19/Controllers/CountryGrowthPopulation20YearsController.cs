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
    public class CountryGrowthPopulation20YearsController : ControllerBase
    {
        private static ICountryGrowthPopulation20YearsManager countryGrowthPopulation20YearskManager;
        public CountryGrowthPopulation20YearsController(ICountryGrowthPopulation20YearsManager iCountryGrowthPopulation20YearskManager)
        {
            countryGrowthPopulation20YearskManager = iCountryGrowthPopulation20YearskManager;
        }
        // GET: api/CountryGrowthPopulation20Years?numYears=20
        [HttpGet]
        public ActionResult<IEnumerable<CountryGrowthPopulation20Years>> GetCountryGrowthPopulation20Years([FromQuery] int numYears, [FromQuery] bool desc=false)
        {
            string orderBy = GlobalFunction.ConvertToOrderBy(desc);
            IEnumerable<CountryGrowthPopulation20Years> listAvg = countryGrowthPopulation20YearskManager.GetDivOfAvg(orderBy, numYears);
            return GlobalFunction.CheckResultAndReturnByGeneric<CountryGrowthPopulation20Years>(listAvg, NotFound, Ok);
        }
    }
}
