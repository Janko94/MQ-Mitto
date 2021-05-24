using Interface.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Model;
using Serilog;
using System;
using System.Threading.Tasks;

namespace CAP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryCodeController : ControllerBase
    {
        ICountryCodeBusiness _business;
        public CountryCodeController(ICountryCodeBusiness business)
        {
            _business = business;
        }
        [HttpGet("countries.json")]
        public async Task<IActionResult> GetAsJson()
        {
            try
            {
                return Ok(await _business.GetAll());
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpGet("countries.xml")]
        public async Task<IActionResult> GetAsXml()
        {
            try
            {
                return Ok(Helper.CreateXmlResponse<CountryCode[]>(await _business.GetAll()));
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex);
            }
        }
    }
}
