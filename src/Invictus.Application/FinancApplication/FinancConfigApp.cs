using AutoMapper;
using Invictus.Application.FinancApplication.Interfaces;
using Invictus.Core.Interfaces;
using Invictus.Domain.Financeiro.Configuracoes;
using Invictus.Domain.Financeiro.Configuracoes.Interfaces;
using Invictus.Dtos.Financeiro.Configuracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.FinancApplication
{
    public class FinancConfigApp : IFinancConfigApp
    {
        private readonly IFinanceiroConfigRepo _configRepo;
        private readonly IMapper _mapper;
        private readonly IAspNetUser _aspNetUser;
        public FinancConfigApp(IFinanceiroConfigRepo configRepo, IMapper mapper, IAspNetUser aspNetUser)
        {
            _configRepo = configRepo;
            _mapper = mapper;
            _aspNetUser = aspNetUser;
        }

        // DELETE 
        public async Task DeleteBanco(Guid bancoId)
        {
            await _configRepo.DeleteBanco(bancoId);

            _configRepo.Commit();
        }

        public async Task DeleteCentroDeCusto(Guid centroCustoId)
        {
            await _configRepo.DeleteCentroCusto(centroCustoId);

            _configRepo.Commit();
        }

        public async Task DeleteFormaRecebimento(Guid formarecebimentoId)
        {
            await _configRepo.DeleteFormaRecebimento(formarecebimentoId);

            _configRepo.Commit();
        }

        public async Task DeleteMeioDePagamento(Guid meioPgmId)
        {
            await _configRepo.DeleteMeioPagamento(meioPgmId);

            _configRepo.Commit();
        }

        public async Task DeletePlanoDeConta(Guid planoId)
        {
            await _configRepo.DeletePlanoConta(planoId);

            _configRepo.Commit();
        }

        public async Task DeleteSubConta(Guid subContaId)
        {
            await _configRepo.DeleteSubConta(subContaId);

            _configRepo.Commit();
        }

        // EDIT

        public async Task EditBanco(BancoDto bancoDto)
        {
            var banco = _mapper.Map<Banco>(bancoDto);

            await _configRepo.EditBanco(banco);

            _configRepo.Commit();
        }

        public async Task EditCentroDeCusto(CentroCustoDto centroCustoDto)
        {
            var centroCusto = _mapper.Map<CentroCusto>(centroCustoDto);

            await _configRepo.EditCentroCusto(centroCusto);

            _configRepo.Commit();
        }

        public async Task EditFormaRecebimento(FormaRecebimentoDto editedFormaRecebimento)
        {
            if (!editedFormaRecebimento.ehCartao)
            {   
                editedFormaRecebimento.taxa = null;
                editedFormaRecebimento.diasParaCompensacao = null;
                editedFormaRecebimento.permiteParcelamento = null;
                editedFormaRecebimento.centroDeCustoTaxaVinculadaId = null;
                editedFormaRecebimento.bancoPermitidoParaCreditoId = null;
                editedFormaRecebimento.compensarAutomaticamenteId = null;
                editedFormaRecebimento.fornecedorTaxaVinculadaId = null;
                editedFormaRecebimento.subcontaTaxaVinculadaId = null;
            }

            var forma = _mapper.Map<FormaRecebimento>(editedFormaRecebimento);

            await _configRepo.EditFormaRecebimento(forma);

            _configRepo.Commit();
        }

        public async Task EditMeioDePagamento(MeioPagamentoDto meioPgmDto)
        {
            var meioPgm = _mapper.Map<MeioPagamento>(meioPgmDto);

            await _configRepo.EditMeioPagamento(meioPgm);

            _configRepo.Commit();
        }

        public async Task EditPlanoDeConta(PlanoContaDto planoDto)
        {
            var plano = _mapper.Map<PlanoConta>(planoDto);

            await _configRepo.EditPlanoConta(plano);

            _configRepo.Commit();
        }

        public async Task EditSubConta(SubContaDto subContaDto)
        {
            var subConta = _mapper.Map<SubConta>(subContaDto);

            await _configRepo.EditSubConta(subConta);

            _configRepo.Commit();
        }

        public async Task SaveBanco(BancoDto bancoDto)
        {
            bancoDto.unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();

            bancoDto.dataCadastro = DateTime.Now;

            var banco = _mapper.Map<Banco>(bancoDto);

            await _configRepo.AddBanco(banco);

            _configRepo.Commit();
        }

        public async Task SaveCentroDeCusto(CentroCustoDto centroCustoDto)
        {
            centroCustoDto.unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();
            var centro = _mapper.Map<CentroCusto>(centroCustoDto);

            await _configRepo.AddCentroCusto(centro);

            _configRepo.Commit();
        }

        public async Task SaveFormRecebimento(FormaRecebimentoDto newFormaRecebimento)
        {
            newFormaRecebimento.unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();

            //bancoDto.dataCadastro = DateTime.Now;

            if (!newFormaRecebimento.ehCartao)
            {
                newFormaRecebimento.taxa = null;
                newFormaRecebimento.diasParaCompensacao = null;
                newFormaRecebimento.permiteParcelamento = null;
                newFormaRecebimento.centroDeCustoTaxaVinculadaId = null;
                newFormaRecebimento.bancoPermitidoParaCreditoId = null;
                newFormaRecebimento.compensarAutomaticamenteId = null;
                newFormaRecebimento.fornecedorTaxaVinculadaId = null;
                newFormaRecebimento.subcontaTaxaVinculadaId = null;
            }

            var forma = _mapper.Map<FormaRecebimento>(newFormaRecebimento);

            await _configRepo.AddFormaRecebimento(forma);

            _configRepo.Commit();
        }

        public async Task SaveMeioDePagamento(MeioPagamentoDto meioPgmDto)
        {
            var meioPgm = _mapper.Map<MeioPagamento>(meioPgmDto);

            await _configRepo.AddMeioPagamento(meioPgm);

            _configRepo.Commit();
        }

        public async Task SavePlanoDeConta(PlanoContaDto planoDto)
        {
            var plano = _mapper.Map<PlanoConta>(planoDto);

            await _configRepo.AddPlanoConta(plano);

            _configRepo.Commit();
        }

        public async Task SaveSubConta(SubContaDto subContaDto)
        {
            var subConta = _mapper.Map<SubConta>(subContaDto);

            await _configRepo.AddSubConta(subConta);

            _configRepo.Commit();
        }
    }
}
