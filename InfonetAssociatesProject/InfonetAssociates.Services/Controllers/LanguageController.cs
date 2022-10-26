using InfonetAssociates.BLLManager.ForLanguage.Interface;
using InfonetAssociates.Infrustructure.BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfonetAssociates.Services.Controllers
{
    [Route("api/Language")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageBLLManager _bLLManager;

        public LanguageController(ILanguageBLLManager bLLManager)
        {
            _bLLManager = bLLManager;
        }


        [HttpPost("AddLanguage")]

        public async Task<IActionResult> AddLanguage([FromBody] LanguageViewModel model)
        {
            try
            {
                var result = await _bLLManager.AddLanguage(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        [HttpGet("GetAllLanguage")]
        public async Task<IActionResult> GetAllLanguage()
        {
            try
            {
                var result = await _bLLManager.GetAllLanguage();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
