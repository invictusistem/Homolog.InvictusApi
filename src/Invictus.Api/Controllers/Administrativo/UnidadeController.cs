using Invictus.Application.AdmApplication;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.Utilitarios.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [Route("api/unidade")]
    [ApiController]
    [Authorize]
    public class Unidadecontroller : ControllerBase
    {
        private readonly IUnidadeQueries _unidadeQuery;
        private readonly IUnidadeApplication _unidadeApplication;
        private readonly IUtils _utils;
        public Unidadecontroller(IUnidadeQueries unidadeQuery,
                                 IUnidadeApplication unidadeApplication,
                                 IUtils utils
            )
        {
            _unidadeQuery = unidadeQuery;
            _unidadeApplication = unidadeApplication;
            _utils = utils;
        }

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetUnidades()
        {
            var unidades = await _unidadeQuery.GetUnidades();//.CreateUnidade(command);

            return Ok(new { unidades = unidades });
        }

        
        [HttpGet]
        [Route("salas/{unidadeId}")]
        public async Task<IActionResult> GetSalas(Guid unidadeId)
        {
            var salas = await _unidadeQuery.GetSalas(unidadeId);//.CreateUnidade(command);

            return Ok(new { salas = salas });
        }

        [HttpGet]
        [Route("sala/{salaId}")]
        public async Task<IActionResult> GetSala(Guid salaId)
        {
            var sala = await _unidadeQuery.GetSala(salaId);//.CreateUnidade(command);

            return Ok(new { sala = sala });
        }

        

       
        

        #endregion

        #region POST

        //[HttpPost]
        //public IActionResult SaveUnidade([FromBody] CreateUnidadeDto command)
        [HttpPost]
        public async Task<IActionResult> SaveUnidade([FromBody] UnidadeDto command)
        {
            var msg = await _utils.ValidaUnidade(command.sigla);
            if (msg.Count() > 0) return Conflict(new { msg = msg });

            await _unidadeApplication.CreateUnidade(command);

            return Ok();
        }

        [HttpPost]
        [Route("sala-create")]
        public async Task<IActionResult> SaveSala([FromBody] SalaDto newSala)
        {
            await _unidadeApplication.AddSala(newSala);

            return Ok();
        }

        
        #endregion

        #region PUT

        [HttpPut]
        public async Task<IActionResult> EditUnidade([FromBody] UnidadeDto command)
        {
            await _unidadeApplication.EditUnidade(command);

            return Ok();
        }

        [HttpPut]
        [Route("sala-editar")]
        public async Task<IActionResult> EditSala([FromBody] SalaDto editedSala)
        {
            await _unidadeApplication.EditSala(editedSala);

            return Ok();
        }

        

        #endregion

        #region DELETE

        #endregion
    }
}
