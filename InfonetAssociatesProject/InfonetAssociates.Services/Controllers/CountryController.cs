using InfonetAssociates.BLLManager.ForCountry.Interface;
using InfonetAssociates.Infrustructure.BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfonetAssociates.Services.Controllers
{
    [Route("api/Country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryBLLManager _bLLManager;

        public CountryController(ICountryBLLManager bLLManager)
        {
            _bLLManager = bLLManager;
        }

        [HttpPost("AddCountry")]

        public async Task<IActionResult> AddCountry([FromBody] CountryViewModel model)
        {
            try
            {
                var result = await _bLLManager.AddCountry(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        [HttpGet("GetAllCountry")]
        public async Task<IActionResult> GetAllCountry()
        {
            try
            {
                var result = await _bLLManager.GetAllCountry();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("deleteCountryById/{countryId}")]
        public async Task<IActionResult> DeleteCountryById(int countryId)
        {
            try
            {
                var result = await _bLLManager.DeleteCountry(countryId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
