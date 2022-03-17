using AutoMapper;
using Invictus.Application.PedagApplication.Interfaces;
using Invictus.Domain.Padagogico.Estagio;
using Invictus.Domain.Padagogico.Estagio.Interfaces;
using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task EditEstagio(EstagioDto estagioDto)
        {
            var estagio = _mapper.Map<Estagio>(estagioDto);

            await _estagioRepo.EditEstagio(estagio);

            _estagioRepo.Commit();
        }
    }
}
