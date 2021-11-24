using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.ProfessorAggregate;
using Invictus.Domain.Administrativo.ProfessorAggregate.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class ProfessorApplication : IProfessorApplication
    {
        private readonly IProfessorRepository _profRepository;
        private readonly IMapper _mapper;
        private readonly IAspNetUser _aspNetUser;
        private readonly IUnidadeQueries _unidadeQueries;
        public ProfessorApplication(IProfessorRepository profRepository, IMapper mapper, IAspNetUser aspNetUser, IUnidadeQueries unidadeQueries)
        {
            _profRepository = profRepository;
            _mapper = mapper;
            _aspNetUser = aspNetUser;
            _unidadeQueries = unidadeQueries;
        }

        public async Task EditProfessor(ProfessorDto editedProfessor)
        {  
            var professor = _mapper.Map<Professor>(editedProfessor);

            await _profRepository.EditProfessor(professor);

            _profRepository.Save();
        }

        public async Task SaveProfessor(ProfessorDto newProfessor)
        {
            var unidade = await _unidadeQueries.GetUnidadeBySigla(_aspNetUser.ObterUnidadeDoUsuario());

            newProfessor.unidadeId = unidade.id;

            var professor = _mapper.Map<Professor>(newProfessor);

            await _profRepository.AddProfessor(professor);

            _profRepository.Save();
        }
    }
}
