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
    public class CountryDeathsSickPerMillionWithGdpController : ControllerBase
    {
        private static ICountryDeathsSickPerMillionWithGdpManager countryDeathsSickPerMillionWithGdpManager;
        public CountryDeathsSickPerMillionWithGdpController(ICountryDeathsSickPerMillionWithGdpManager iCountryDeathsSickPerMillionWithGdpManager)
        {
            countryDeathsSickPerMillionWithGdpManager = iCountryDeathsSickPerMillionWithGdpManager;
        }
        // GET: api/CountryDeathsSickPerMillionWithGdp?deathsGdpSick=GdpOrder&date=10/10/2020
        [HttpGet]
        public ActionResult<IEnumerable<CountryDeathsSickPerMillionWithGdp>> GetCountryDeathsSickPerMillionWithGdp([FromQuery] string deathsGdpSick, [FromQuery] string date, [FromQuery] bool desc = false)
        {
            string orderBy = GlobalFunction.ConvertToOrderBy(desc);
            switch (deathsGdpSick)
            {
                case "SickOrder": //in use on onSubmitSpecificCountrySpecificDateDeathOrSick (complex window, gdp col, order sick, per million)
                    IEnumerable<CountryDeathsSickPerMillionWithGdp> listSick = countryDeathsSickPerMillionWithGdpManager.GetDataOrderBySick(orderBy, date);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountryDeathsSickPerMillionWithGdp>(listSick, NotFound, Ok);
                case "DeathsOrder": //in use on onSubmitSpecificCountrySpecificDateDeathOrSick (complex window, gdp col, order death, per million)
                    IEnumerable<CountryDeathsSickPerMillionWithGdp> listDeaths = countryDeathsSickPerMillionWithGdpManager.GetDataOrderByDeaths(orderBy, date);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountryDeathsSickPerMillionWithGdp>(listDeaths, NotFound, Ok);
                case "GdpOrder": //in use on onSubmitSpecificCountrySpecificDateDeathOrSick (complex window, gdp col, order sick, per million) --- there is a problem in the order!!
                    IEnumerable<CountryDeathsSickPerMillionWithGdp> listGdp = countryDeathsSickPerMillionWithGdpManager.GetDataOrderByGdp(orderBy, date);
                    return GlobalFunction.CheckResultAndReturnByGeneric<CountryDeathsSickPerMillionWithGdp>(listGdp, NotFound, Ok);
                default:
                    return BadRequest();
            }
        }
    }
}
