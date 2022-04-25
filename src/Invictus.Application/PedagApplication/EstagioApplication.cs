using AutoMapper;
using Invictus.Application.PedagApplication.Interfaces;
using Invictus.Domain.Padagogico.Estagio;
using Invictus.Domain.Padagogico.Estagio.Interfaces;
using Invictus.Dtos.PedagDto;
using System;
using System.Threading.Tasks;

namespace Invictus.Application.PedagApplication
{
    public class EstagioApplication : IEstagioApplication
    {
        private readonly IMapper _mapper;
        private readonly IEstagioRepo _estagioRepo;
        public EstagioApplication(IMapper mapper, IEstagioRepo estagioRepo)
        {
            _mapper = mapper;
            _estagioRepo = estagioRepo;
        }
        public async Task CreateEstagio(EstagioDto estagioDto)
        {
            var estagio = _mapper.Map<Estagio>(estagioDto);

            await _estagioRepo.CreateEstagio(estagio);

            _estagioRepo.Commit();
        }

        public async Task CreateTypeEstagio(TypeEstagioDto typeEstagio)
        {
            var type = _mapper.Map<TypeEstagio>(typeEstagio);

            await _estagioRepo.CreateEstagioType(type);

            _estagioRepo.Commit();
        }

        public async Task DeleteTypeEstagio(Guid typeEstagio)
        {
            await _estagioRepo.DeleteEstagioType(typeEstagio);

            _estagioRepo.Commit();
        }

        public async Task EditEstagio(EstagioDto estagioDto)
        {
            var estagio = _mapper.Map<Estagio>(estagioDto);

            await _estagioRepo.EditEstagio(estagio);

            _estagioRepo.Commit();
        }

        public async Task EditTypeEstagio(TypeEstagioDto typeEstagio)
        {
            var type = _mapper.Map<TypeEstagio>(typeEstagio);

            await _estagioRepo.EditEstagioType(type);

            _estagioRepo.Commit();
        }
    }
}
