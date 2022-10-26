using InfonetAssociates.BLLManager.ForMemberPersonInfo.Interfaces;
using InfonetAssociates.Infrustructure.BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfonetAssociates.Services.Controllers
{
    [Route("api/MemberInfo")]
    [ApiController]
    public class MemberPersonController : ControllerBase
    {
        private readonly IMemberPersonInfoBLLManager _bLLManager;

        public MemberPersonController(IMemberPersonInfoBLLManager bLLManager)
        {
            _bLLManager = bLLManager;
        }


        [HttpPost("AddPersonalInfo")]

        public async Task<IActionResult> AddPersonalInfo([FromForm] MemberPersonalInformationViewModel model)
        {
            try
            {
                model.ImageFile = Request.Form.Files["ImageFile"];
                var result = await _bLLManager.AddPersonalInfo(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        [HttpPost("AddPersonalLanguage")]

        public async Task<IActionResult> AddPersonalLanguage([FromBody] PersonalLanguageViewModel model)
        {
            try
            {
                var result = await _bLLManager.AddPersonalLanguage(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        
        [HttpGet("GetAllAddPersonalInfo")]
        public async Task<IActionResult> GetAllAddPersonalInfo()
        {
            try
            {
                var result = await _bLLManager.GetAllAddPersonalInfo();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpDelete("DeleteMemberById/{id}")]
        public async Task<IActionResult> DeleteMemberById(int id)
        {
            try
            {
                var result = await _bLLManager.DeleteMemberById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetMemberById/{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            try
            {
                var result = await _bLLManager.GetMemberById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
