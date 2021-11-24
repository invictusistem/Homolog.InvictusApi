using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Invictus.Domain.Administrativo.ColaboradorAggregate.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class ColaboradorApplication : IColaboradorApplication
    {
        private readonly IAspNetUser _aspNetUser;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IColaboradorRepository _colaboradorRepo;
        private readonly IMapper _mapper;
        public ColaboradorApplication(IAspNetUser aspNetUser, IUnidadeQueries unidadeQueries, IMapper mapper, IColaboradorRepository colaboradorRepo)
        {
            _aspNetUser = aspNetUser;
            _unidadeQueries = unidadeQueries;
            _mapper = mapper;
            _colaboradorRepo = colaboradorRepo;
        }

        public async Task EditColaborador(ColaboradorDto editedColaborador)
        {            

            var colaborador = _mapper.Map<ColaboradorDto, Colaborador>(editedColaborador);

            colaborador.TratarEmail(colaborador.Email);

            await _colaboradorRepo.EditColaborador(colaborador);

            _colaboradorRepo.Commit();
        }

        public async Task SaveColaborador(ColaboradorDto newColaborador)
        {
            var unidadeSigla = _aspNetUser.ObterUnidadeDoUsuario();// _context.Unidades.Where(u => u.Sigla == unidade).Select(s => s.Id).SingleOrDefault();

            var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);

            newColaborador.unidadeId = unidade.id;
            
            var colaborador = _mapper.Map<ColaboradorDto, Colaborador>(newColaborador);

            colaborador.TratarEmail(colaborador.Email);

            await _colaboradorRepo.AddColaborador(colaborador);

            _colaboradorRepo.Commit();
           
        }
    }
}
