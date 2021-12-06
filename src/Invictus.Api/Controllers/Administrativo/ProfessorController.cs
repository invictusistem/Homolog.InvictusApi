﻿using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.Utilitarios.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace Invictus.Api.Controllers
{
    [Route("api/professor")]
    [Authorize]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorApplication _profApplication;
        private readonly IProfessorQueries _profQueries;
        private readonly IUnidadeQueries _unidadeQuery;
        private readonly ITypePacoteQueries _typeQueries;
        private readonly IUtils _utils;
        public ProfessorController(IProfessorApplication profApplication, IProfessorQueries profQueries, IUtils utils,
            IUnidadeQueries unidadeQuery, ITypePacoteQueries typeQueries)
        {
            _profApplication = profApplication;
            _profQueries = profQueries;
            _utils = utils;
            _unidadeQuery = unidadeQuery;
            _typeQueries = typeQueries;

        }

        [HttpGet]
        public async Task<ActionResult<PaginatedItemsViewModel<ColaboradorDto>>> GetProfessor([FromQuery] int itemsPerPage, [FromQuery] int currentPage, [FromQuery] string paramsJson)
        {

            var results = await _profQueries.GetProfessores(itemsPerPage, currentPage, paramsJson);

            // var cargos = await _db.Cargos.ToListAsync();
            if (results.Data.Count() == 0) return NotFound();

            return Ok(new { results = results });
        }

        [HttpGet]
        [Route("materias/{professorId}")]
        public async Task<IActionResult> ViewProfMateriasViewModel(Guid professorId)
        {
            var typePacotes = await _typeQueries.GetTypePacotes();
            var disponibilidades = await _profQueries.GetProfessorDisponibilidade(professorId);
            var profMaterias = await _profQueries.GetProfessoresMaterias(professorId);
            var unidades = await _profQueries.GetProfessoresUnidadesDisponiveis(professorId);

            //await _profApplication.EditProfessor(editedProfessor);

            return Ok(new { typePacotes = typePacotes, unidades = unidades, profMaterias = profMaterias, disponibilidades = disponibilidades });// NoContent();
        }

        [HttpGet]
        [Route("materias-professor/{professorId}")]
        public async Task<IActionResult> GetProfessorMaterias(Guid professorId)
        {            
            var profMaterias = await _profQueries.GetProfessoresMaterias(professorId);

            return Ok(new { profMaterias = profMaterias });
        }

        [HttpGet]
        [Route("unidades-disponibilidades/{professorId}")]
        public async Task<IActionResult> GetUnidadesDisponiveis(Guid professorId)
        {
            var unidades = await _profQueries.GetProfessoresUnidadesDisponiveis(professorId);

            return Ok(new { unidades = unidades });
        }

        [HttpGet]
        [Route("{professorId}")]
        public async Task<IActionResult> GetProfessorById(Guid professorId)
        {

            var result = await _profQueries.GetProfessorById(professorId);


            return Ok(new { result = result });
        }

        [HttpPost]
        public async Task<IActionResult> SaveProfessor([FromBody] ProfessorDto newProfessor)
        {
            var msg = await _utils.ValidaDocumentosColaborador(newProfessor.cpf, null, newProfessor.email);
            if (msg.Count() > 0) return Conflict(new { msg = msg });
            await _profApplication.SaveProfessor(newProfessor);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> EditProfessor([FromBody] ProfessorDto editedProfessor)
        {
            await _profApplication.EditProfessor(editedProfessor);

            return NoContent();
        }

        [HttpPost]
        [Route("materia/{profId}/{materiaId}")]
        public async Task<IActionResult> AddProfessorMateria(Guid profId, Guid materiaId)
        {
            await _profApplication.AddProfessorMateria(profId,materiaId);
            //var msg = await _utils.ValidaDocumentosColaborador(newProfessor.cpf, null, newProfessor.email);
            //if (msg.Count() > 0) return Conflict(new { msg = msg });
            //await _profApplication.SaveProfessor(newProfessor);

            return Ok();
        }

        [HttpPost]
        [Route("disponibilidade")]
        public async Task<IActionResult> AddProfessorDisponibilidade([FromBody] DisponibilidadeDto dispo)
        {
            await _profApplication.AddDisponibilidade(dispo);

            return Ok();
        }

        [HttpDelete]
        [Route("materia/{profMateriaId}")]
        public async Task<IActionResult> RemoveMateriaProfessor(Guid profMateriaId)
        {
            await _profApplication.RemoveProfessorMateria(profMateriaId);

            return Ok();
        }
        /*
         marcar disponibilidade:
            1 - dias da semana
            2 - materias (pegar pelo Id)
            3 - unidades
            como?
            add unidade na lista
            em cada unidade, add os dias da semana q pode estar la (depende da unidade)
            add turno!?????
            add matérias q dá aula (independe da unidade)

            chegar na tela de ADD PROF NA TURMA
            1 - vai ver os PROF que marcaram para aquela unidade
            2 - ver se o o dia da semana q ele escolheu na disponidadlide para aquela unidade bate com os dias das semanas das aulas
            3 - filatrado o dia, ver se a materia q ele leciona tem naquela turma

            add materia para o professor seleciona (acima)
            1) trazer materia q ele marcou como prof
            refazer paço acima para n ter problema???????
         
         
            Problemas? e se ele desmacar? ele fica sem dar aula??? deixa ele agendado ou automaticamente o sistema tira????

        abrir aula:
            trazer booleano do backEnd pelo Get na Api! Qnd for salvar algo VALIDAR ISSO DE NOVO para evitar BURLA
            1 - um unico booleano: ver se pode abrir a aula (baseado no horário) e se ele é o prof daquela aula do dia

        EXCEPCIONALIDADE:
        prof substituto:
            QUEBRA AS REGRAS ACIMA
                naquela tela feito do calendario onde mostra comentario do prof e tal mais um ícone: (colocar prof substituto)
                    ai vc coloca qualquer professor independente da regra acima. Como salva no banco? de repente uma coluna na table OBS sei la
                       
            



         */
    }
}
