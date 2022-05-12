using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [Route("api/parametro")]
    [ApiController]
    [Authorize]
    public class ParametrosController : ControllerBase
    {
        private readonly IParametroApplication _paramApp;
        private readonly IParametrosQueries _paramQueries;
        public ParametrosController(IParametroApplication paramApp, IParametrosQueries paramQueries)
        {
            _paramApp = paramApp;
            _paramQueries = paramQueries;

        }

        [HttpGet]
        [Route("value/{key}")]
        public async Task<IActionResult> GetParamValue(string key)
        {
           
            var values = await _paramQueries.GetParamValue(key);

            return Ok(new { values = values });
        }

        [HttpGet]
        [Route("get-value/{valueId}")]
        public async Task<IActionResult> GetKey(Guid valueId)
        {

            var value = await _paramQueries.GetParamKeyById(valueId);

            return Ok(new { value = value });
        }

        [HttpPost]
        [Route("value/{key}")]
        public async Task<IActionResult> SaveParamValue(string key, [FromBody] ParametroValueDto parametro)
        {
            // validar se nao há cargo com mesmo valor salvo
            await _paramApp.SaveCargo(key, parametro);

            return Ok();
        }

        [HttpPut]
        [Route("value")]
        public async Task<IActionResult> EditParamValue([FromBody] ParametroValueDto parametro)
        {
            // validar se nao há cargo com mesmo valor salvo
            await _paramApp.EditCargo(parametro);

            return NoContent();
        }

        [HttpDelete]
        [Route("value/{paramId}")]
        public async Task<IActionResult> RemoveParamValue(Guid paramId)
        {
            await _paramApp.RemoeValueById(paramId);

            return NoContent();
        }


    }
}
