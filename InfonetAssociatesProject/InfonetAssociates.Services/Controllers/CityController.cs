using InfonetAssociates.BLLManager.ForCity.Interface;
using InfonetAssociates.Infrustructure.BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfonetAssociates.Services.Controllers
{
    [Route("api/City")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityBLLManager _bLLManager;

        public CityController(ICityBLLManager bLLManager)
        {
            _bLLManager = bLLManager;
        }

        [HttpPost("AddCity")]

        public async Task<IActionResult> AddCity([FromBody] CityViewModel model)
        {
            try
            {
                var result = await _bLLManager.AddCity(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        [HttpGet("GetAllCity")]
        public async Task<IActionResult> GetAllCity()
        {
            try
            {
                var result = await _bLLManager.GetAllCity();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet("GetAllCityByCountry/{countryId}")]

        public async Task<IActionResult> GetAllCityByCountry(int countryId)
        {
            try
            {
                var result = await _bLLManager.GetAllCityByCountry(countryId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
