using Invictus.Application.AdmApplication;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [Route("api/parametro")]
    [ApiController]
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

        [HttpPost]
        [Route("value/{key}")]
        public async Task<IActionResult> SaveParamValue(string key, [FromBody] ParametroValueDto parametro)
        {
            // validar se nao há cargo com mesmo valor salvo
            await _paramApp.SaveCargo(key, parametro);

            return Ok();
        }

        

    }
}
