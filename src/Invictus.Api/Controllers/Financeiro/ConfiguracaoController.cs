using Invictus.Application.FinancApplication.Interfaces;
using Invictus.Dtos.Financeiro.Configuracoes;
using Invictus.QueryService.FinanceiroQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Financeiro
{
    [Route("api/configuracao-financ")]
    [Authorize]
    [ApiController]
    public class ConfiguracaoController : ControllerBase
    {
        private readonly IFinancConfigApp _financApp;
        private readonly IFinConfigQueries _finConfigQuereis;
        public ConfiguracaoController(IFinancConfigApp financApp, IFinConfigQueries finConfigQuereis)
        {
            _financApp = financApp;
            _finConfigQuereis = finConfigQuereis;
        }


        [HttpGet]
        [Route("banco")]
        public async Task<IActionResult> GetBancos()
        {
            var result = await _finConfigQuereis.GetAllBancos();

            return Ok(new { result = result });
        }

        [HttpGet]
        [Route("banco/ativos")]
        public async Task<IActionResult> GetBancosAtivos()
        {
            var result = await _finConfigQuereis.GetAllBancosAtivosFromUnidade();

            return Ok(new { result = result });
        }

        [HttpGet]
        [Route("centro-custo")]
        public async Task<IActionResult> GetCentroCusto()
        {
            var result = await _finConfigQuereis.GetAllCentroCusto();

            return Ok(new { result = result });
        }

        [HttpGet]
        [Route("meio-pgm")]
        public async Task<IActionResult> GetMeioPgm()
        {
            var result = await _finConfigQuereis.GetAllMeiosPagamento();

            return Ok(new { result = result });
        }

        [HttpGet]
        [Route("plano")]
        public async Task<IActionResult> GetPlano()
        {
            var result = await _finConfigQuereis.GetAllPlanos();

            return Ok(new { result = result });
        }

        [HttpGet]
        [Route("subconta")]
        public async Task<IActionResult> GetSubConta()
        {
            var result = await _finConfigQuereis.GetAllSubContas();

            return Ok(new { result = result });
        }

        [HttpGet]
        [Route("subconta/ativas")]
        public async Task<IActionResult> GetSubContasAtivas()
        {
            var result = await _finConfigQuereis.GetAllSubContasAtivas();

            return Ok(new { result = result });
        }

        [HttpGet]
        [Route("subconta/ativas/debito")]
        public async Task<IActionResult> GetSubContasAtivasDebito()
        {
            var result = await _finConfigQuereis.GetAllSubContasAtivasDebitos();

            return Ok(new { result = result });
        }

        [HttpGet]
        [Route("forma-recebimento")]
        public async Task<IActionResult> GetFormasRecebimentos()
        {
            var result = await _finConfigQuereis.GetAllFormasRecebimentos();

            return Ok(new { result = result });
        }


        [HttpGet]
        [Route("forma-recebimento/ativo")]
        public async Task<IActionResult> GetFormasRecebimentosAtivas()
        {
            var result = await _finConfigQuereis.GetAllFormasRecebimentosAtivas();

            return Ok(new { result = result });
        }



        [HttpGet]
        [Route("forma-recebimento/create")]
        public async Task<IActionResult> GetFormaRecebimentoCreate()
        {
            /*
             * 'compensar crédito no banco: ' - trazer bancos 
* traz só subconta do tipo débito
* 'Vincular ao Centro de Custo: ' - trazer Centro de custos
* 'Vincular ao fornecedor' - trazer fornecedores' 
             */

            var bancos = await _finConfigQuereis.GetAllBancos();

            var centroCustos = await _finConfigQuereis.GetAllCentroCusto();

            var subContas = await _finConfigQuereis.GetAllSubContas();

            var forncedores = await _finConfigQuereis.GetFornecedoresForCreateFormaRecebimento();

            return Ok(new
            {
                bancos = bancos.OrderBy(c => c.nome),
                centroCustos = centroCustos.OrderBy(c => c.descricao),
                subContas = subContas.OrderBy(c => c.descricao),
                forncedores = forncedores.OrderBy(c => c.nome)
            });
        }

        // Get By Id

        [HttpGet]
        [Route("banco/{id}")]
        public async Task<IActionResult> GetBancoById(Guid id)
        {
            var result = await _finConfigQuereis.GetBancoById(id);

            return Ok(new { result = result });
        }

        [HttpGet]
        [Route("centro-custo/{id}")]
        public async Task<IActionResult> GetCentroCustoById(Guid id)
        {
            var result = await _finConfigQuereis.GetCentroCustoById(id);

            return Ok(new { result = result });
        }

        [HttpGet]
        [Route("meio-pgm/{id}")]
        public async Task<IActionResult> GetMeioPgmById(Guid id)
        {
            var result = await _finConfigQuereis.GetMeiosPagamentoById(id);

            return Ok(new { result = result });
        }

        [HttpGet]
        [Route("plano/{id}")]
        public async Task<IActionResult> GetPlanoById(Guid id)
        {
            var result = await _finConfigQuereis.GetPlanosById(id);

            return Ok(new { result = result });
        }

        [HttpGet]
        [Route("subconta/{id}")]
        public async Task<IActionResult> GetSubContaById(Guid id)
        {
            var result = await _finConfigQuereis.GetSubContasById(id);

            return Ok(new { result = result });
        }

        [HttpGet]
        [Route("subconta/plano/{id}")]
        public async Task<IActionResult> GetSubContaByPlanoId(Guid id)
        {
            var result = await _finConfigQuereis.GetSubContasByPlanoId(id);

            return Ok(new { result = result });
        }

        [HttpGet]
        [Route("forma-recebimento/{id}")]
        public async Task<IActionResult> GetFormaRecebimentoById(Guid id)
        {
            var result = await _finConfigQuereis.GetFormaRecebimentoById(id);

            return Ok(new { result = result });
        }

        // POST

        [HttpPost]
        [Route("banco")]
        public async Task<IActionResult> SaveBanco([FromBody] BancoDto bancoDto)
        {
            await _financApp.SaveBanco(bancoDto);
            return Ok();
        }

        [HttpPost]
        [Route("centro-custo")]
        public async Task<IActionResult> SaveCentroCusto([FromBody] CentroCustoDto centroCustoDto)
        {
            await _financApp.SaveCentroDeCusto(centroCustoDto);
            return Ok();
        }

        [HttpPost]
        [Route("meio-pgm")]
        public async Task<IActionResult> SaveMeioPgm([FromBody] MeioPagamentoDto meioPagamentoDto)
        {
            await _financApp.SaveMeioDePagamento(meioPagamentoDto);
            return Ok();
        }

        [HttpPost]
        [Route("plano")]
        public async Task<IActionResult> SavePlano([FromBody] PlanoContaDto planoContaDto)
        {
            await _financApp.SavePlanoDeConta(planoContaDto);
            return Ok();
        }

        [HttpPost]
        [Route("subconta")]
        public async Task<IActionResult> SaveSubConta([FromBody] SubContaDto subContaDto)
        {
            await _financApp.SaveSubConta(subContaDto);
            return Ok();
        }

        [HttpPost]
        [Route("forma-recebimento")]
        public async Task<IActionResult> SaveFormRecebimento([FromBody] FormaRecebimentoDto formarecebimento)
        {
            await _financApp.SaveFormRecebimento(formarecebimento);
            return Ok();
        }

        // PUT

        [HttpPut]
        [Route("banco")]
        public async Task<IActionResult> EditBanco([FromBody] BancoDto bancoDto)
        {
            await _financApp.EditBanco(bancoDto);
            return Ok();
        }

        [HttpPut]
        [Route("centro-custo")]
        public async Task<IActionResult> EditCentroCusto([FromBody] CentroCustoDto centroCustoDto)
        {
            await _financApp.EditCentroDeCusto(centroCustoDto);
            return Ok();
        }

        [HttpPut]
        [Route("meio-pgm")]
        public async Task<IActionResult> EditMeioPgm([FromBody] MeioPagamentoDto meioPagamentoDto)
        {
            await _financApp.EditMeioDePagamento(meioPagamentoDto);
            return Ok();
        }

        [HttpPut]
        [Route("plano")]
        public async Task<IActionResult> EditPlano([FromBody] PlanoContaDto planoContaDto)
        {
            await _financApp.EditPlanoDeConta(planoContaDto);
            return Ok();
        }

        [HttpPut]
        [Route("subconta")]
        public async Task<IActionResult> EditSubConta([FromBody] SubContaDto subContaDto)
        {
            await _financApp.EditSubConta(subContaDto);
            return Ok();
        }

        [HttpPut]
        [Route("forma-recebimento")]
        public async Task<IActionResult> EditFormaRecebimento([FromBody] FormaRecebimentoDto formaRecebimento)
        {
            await _financApp.EditFormaRecebimento(formaRecebimento);
            return Ok();
        }

        // DELETE

        [HttpDelete]
        [Route("banco/{bancoId}")]
        public async Task<IActionResult> DeleteBanco(Guid bancoId)
        {
            await _financApp.DeleteBanco(bancoId);
            return Ok();
        }

        [HttpDelete]
        [Route("centro-custo/{centroCustoId}")]
        public async Task<IActionResult> DeleteCentroCusto(Guid centroCustoId)
        {
            await _financApp.DeleteCentroDeCusto(centroCustoId);
            return Ok();
        }

        [HttpDelete]
        [Route("meio-pgm/{meioPgmId}")]
        public async Task<IActionResult> DeleteMeioPgm(Guid meioPgmId)
        {
            await _financApp.DeleteMeioDePagamento(meioPgmId);
            return Ok();
        }

        [HttpDelete]
        [Route("plano/{planoId}")]
        public async Task<IActionResult> DeletePlano(Guid planoId)
        {
            await _financApp.DeletePlanoDeConta(planoId);
            return Ok();
        }

        [HttpDelete]
        [Route("subconta/{subContaId}")]
        public async Task<IActionResult> DeleteSubConta(Guid subContaId)
        {
            await _financApp.DeleteSubConta(subContaId);
            return Ok();
        }

        [HttpDelete]
        [Route("forma-recebimento/{id}")]
        public async Task<IActionResult> DeleteFormaRecebimento(Guid id)
        {
            await _financApp.DeleteFormaRecebimento(id);
            return Ok();
        }
    }
}
