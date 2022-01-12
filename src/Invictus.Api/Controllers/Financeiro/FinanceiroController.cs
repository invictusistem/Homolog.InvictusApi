using Invictus.QueryService.FinanceiroQueries.Interfaces;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Financeiro
{
    [Route("api/financeiro")]
    [Authorize]
    [ApiController]
    public class FinanceiroController : ControllerBase
    {
        private readonly IFinanceiroQueries _finQueries;
        private readonly ITurmaPedagQueries _turmaQueries;
        public FinanceiroController(IFinanceiroQueries finQueries, ITurmaPedagQueries turmaQueries)
        {
            _finQueries = finQueries;
            _turmaQueries = turmaQueries;
        }


        [HttpGet]
        [Route("debitos/{matriculaId}")]
        public async Task<IActionResult> AlunoDebitosV2(Guid matriculaId)
        {
            var debitos = await _finQueries.GetDebitoAlunos(matriculaId);
            var turma = await _turmaQueries.GetTurmaByMatriculaId(matriculaId);
            
            return Ok(new { debitos = debitos.OrderBy(c => c.vencimento), turma = turma });
            
        }

        [HttpGet]
        [Route("alunos")]
        public async Task<IActionResult> BuscarCadastroAlunoFin([FromQuery] int itemsPerPage, [FromQuery] int currentPage, [FromQuery] string paramsJson)
        {

            var alunos = await _finQueries.GetAlunosFinanceiro(itemsPerPage, currentPage, paramsJson);
            if (alunos.Data.Count() == 0) return NotFound();

            return Ok(new { alunos = alunos });
            // OLD
            //var param = JsonConvert.DeserializeObject<ParametrosDTO>(paramsJson);
            //var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefaultAsync();
            //var pessoas = await _financeiroQueries.GetAlunoFin(itemsPerPage, currentPage, param, unidadeId);// _matriculaQueries.BuscaAlunos(param.email, param.cpf, param.nome);
            ////  var pessoas = await _matriculaQueries.BuscaAlunos(itemsPerPage, currentPage, parametros, unidadeId);
            //pessoas.Data = SetCPFBind(pessoas.Data);
            //return Ok(pessoas);
            //return Ok(pessoas);
        }

       
        //public List<AlunoDto> SetCPFBind(List<AlunoDto> datas)
        //{
        //    foreach (var data in datas)
        //    {

        //        var newValue = "***.***." + data.cpf.Substring(6, 3) + "-**";

        //        data.cpf = newValue;

        //    }
        //    //var newValue = "***.***." + CPF.Substring(6, 3) + "-**";

        //    //CPF = newValue;

        //    return datas;
        //}
    }
}
