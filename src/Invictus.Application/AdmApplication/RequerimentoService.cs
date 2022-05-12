using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.RequerimentoAggregate;
using Invictus.Domain.Administrativo.RequerimentoAggregate.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using System;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class RequerimentoService : IRequerimentoService
    {
        private readonly IAspNetUser _aspNetUser;
        private readonly IMapper _mapper;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IRequerimentoRepo _requerimentoRepo;
        public RequerimentoService(IMapper mapper, IRequerimentoRepo requerimentoRepo, IAspNetUser aspNetUser, IUnidadeQueries unidadeQueries)
        {
            _aspNetUser = aspNetUser;
            _mapper = mapper;
            _unidadeQueries = unidadeQueries;
            _requerimentoRepo = requerimentoRepo;
        }

        public Task EditRequerimento(RequerimentoDto requerimentoDto)
        {
            throw new NotImplementedException();
        }

        public async Task EditTipoRequerimento(TipoRequerimentoDto tipoDto)
        {
            var tipo = _mapper.Map<TipoRequerimento>(tipoDto);

            await _requerimentoRepo.EditTypeRequerimento(tipo);

            _requerimentoRepo.Commit();
        }

        public async Task SaveRequerimento(RequerimentoDto requerimentoDto)
        {
            requerimentoDto.matriculaRequerenteId = _aspNetUser.ObterUsuarioId();

            var unidadeSigla = _aspNetUser.ObterUnidadeDoUsuario();

            var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);

            requerimentoDto.unidadeId = unidade.id;

            var requerimento = _mapper.Map<Requerimento>(requerimentoDto);

            await _requerimentoRepo.SaveRequerimento(requerimento);

            _requerimentoRepo.Commit();
        }

        public async Task SaveTipoRequerimento(TipoRequerimentoDto tipoDto)
        {
            var tipo = _mapper.Map<TipoRequerimento>(tipoDto);

            await _requerimentoRepo.SaveTypeRequerimento(tipo);

            _requerimentoRepo.Commit();
        }
    }
}
