using AutoMapper;
using Invictus.Application.FinancApplication.Interfaces;
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
        public FinancConfigApp(IFinanceiroConfigRepo configRepo, IMapper mapper)
        {
            _configRepo = configRepo;
            _mapper = mapper;
        }
        public async Task SaveBanco(BancoDto bancoDto)
        {
            var banco = _mapper.Map<Banco>(bancoDto);

            await _configRepo.AddBanco(banco);

            _configRepo.Commit();
        }

        public async Task SaveCentroDeCusto(CentroCustoDto centroCustoDto)
        {
            var centro = _mapper.Map<CentroCusto>(centroCustoDto);

            await _configRepo.AddCentroCusto(centro);

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
