using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.ProfessorAggregate;
using Invictus.Domain.Administrativo.ProfessorAggregate.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using System;
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

        public async Task AddDisponibilidade(DisponibilidadeDto disponibilidadeDto)
        {
            disponibilidadeDto.dataAtualizacao = DateTime.Now;

            var dispo = _mapper.Map<Disponibilidade>(disponibilidadeDto);

            await _profRepository.AddDisponibilidade(dispo);

            _profRepository.Save();
        }

        public async Task AddProfessorMateria(Guid profId, Guid materiaId)
        {
            var materia = new MateriaHabilitada(materiaId, profId);

            await _profRepository.AddProfessorMateria(materia);

            _profRepository.Save();
        }

        public async Task EditProfessor(ProfessorDto editedProfessor)
        {  
            var professor = _mapper.Map<Professor>(editedProfessor);

            await _profRepository.EditProfessor(professor);

            _profRepository.Save();
        }

        public async Task RemoveProfessorMateria(Guid profMateriaId)
        {
            await _profRepository.RemoveProfessorMateria(profMateriaId);

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
