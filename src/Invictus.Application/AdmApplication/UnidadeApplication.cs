using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Application.FinancApplication.Interfaces;
using Invictus.Domain.Administrativo.UnidadeAggregate;
using Invictus.Domain.Administrativo.UnidadeAggregate.Interfaces;
using Invictus.Domain.Financeiro.Configuracoes;
using Invictus.Domain.Financeiro.Configuracoes.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.Financeiro.Configuracoes;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class UnidadeApplication : IUnidadeApplication
    {
        private readonly IMapper _mapper;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IUnidadeRepository _unidadeRepo;
        private readonly IFinanceiroConfigRepo _configRepo;
        //private readonly IColaboradorRepository _colaboradorRepo;
        //private readonly IContratoRepository _contratoRepo;
        public UnidadeApplication(
            //IContratoRepository contratoRepo,
            IMapper mapper,
            IUnidadeRepository unidadeRepo,
            //IColaboradorRepository colaboradorRepo,
            IUnidadeQueries unidadeQueries,
            IFinanceiroConfigRepo configRepo
            )
        {
            _mapper = mapper;
            _unidadeRepo = unidadeRepo;
            //_colaboradorRepo = colaboradorRepo;
            //_contratoRepo = contratoRepo;
            _unidadeQueries = unidadeQueries;
            _configRepo = configRepo;


        }

        public async Task AddSala(SalaDto newSala)
        {
            var sala = _mapper.Map<SalaDto, Sala>(newSala);

            var totalSalas = await _unidadeQueries.CountSalaUnidade(sala.UnidadeId);

            sala.SetSalaTitulo(totalSalas);

            await _unidadeRepo.SaveSala(sala);

            _unidadeRepo.Save();
        }

        public async Task CreateUnidade(UnidadeDto unidadeDto)
        {
            var unidade = _mapper.Map<UnidadeDto, Unidade>(unidadeDto);

            await _unidadeRepo.AddUnidade(unidade);

            _unidadeRepo.Save();

            var bancoDto = new BancoDto();
            bancoDto.ativo = true;
            bancoDto.dataCadastro = DateTime.Now;
            bancoDto.ehCaixaEscola = true;
            bancoDto.unidadeId = unidade.Id;
            bancoDto.nome = "CAIXA DA ESCOLA";

            var banco = _mapper.Map<Banco>(bancoDto);

            await _configRepo.AddBanco(banco);
            _configRepo.Commit();
        }

        public async Task EditUnidade(UnidadeDto editedUnidade)
        {
            var unidade = _mapper.Map<UnidadeDto, Unidade>(editedUnidade);

            await _unidadeRepo.EditUnidade(unidade);

            _unidadeRepo.Save();
        }

        public async Task EditSala(SalaDto editedSala)
        {
            var sala = _mapper.Map<SalaDto, Sala>(editedSala);

            await _unidadeRepo.EditSala(sala);

            _unidadeRepo.Save();
        }
    }
}
