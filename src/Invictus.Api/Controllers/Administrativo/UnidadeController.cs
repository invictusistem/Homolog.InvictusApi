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
        [Route("{unidadeId}")]
        public async Task<IActionResult> GetUnidade(Guid unidadeId)
        {
            var unidade = await _unidadeQuery.GetUnidadeById(unidadeId);

            return Ok(new { unidade = unidade });
        }


        [HttpGet]
        [Route("salas/{unidadeId}")]
        public async Task<IActionResult> GetSalas(Guid unidadeId)
        {
            var salas = await _unidadeQuery.GetSalas(unidadeId);//.CreateUnidade(command);

            if (!salas.Any()) return NotFound();

            return Ok(new { salas = salas });
        }

        [HttpGet]
        [Route("sigla/{sigla}")]
        public async Task<IActionResult> GetSalas(string sigla)
        {
            var unidade = await _unidadeQuery.GetUnidadeBySigla(sigla);//.CreateUnidade(command);
            

            return Ok(new { unidade = unidade });
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
        public async Task<IActionResult> SaveUnidade([FromBody] UnidadeDto newUnidade)
        {
            var msg = await _utils.ValidaUnidade(newUnidade);
            if (msg.Count() > 0) return Conflict(new { msg = msg });

            await _unidadeApplication.CreateUnidade(newUnidade);

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
        public async Task<IActionResult> EditUnidade([FromBody] UnidadeDto editedUnidade)
        {
            var msg = await _utils.ValidaUnidade(editedUnidade);
            if (msg.Count() > 0) return Conflict(new { msg = msg });

            await _unidadeApplication.EditUnidade(editedUnidade);

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
