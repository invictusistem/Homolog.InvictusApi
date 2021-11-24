using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Domain.Administrativo.PacoteAggregate;
using Invictus.Domain.Administrativo.PacoteAggregate.Interfaces;
using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class PacoteApplication : IPacoteApplication
    {
        private readonly IMapper _mapper;
        private readonly IPacoteRepository _pacoteRepo;
        public PacoteApplication(IMapper mapper, IPacoteRepository pacoteRepo)
        {
            _mapper = mapper;
            _pacoteRepo = pacoteRepo;
        }        

        public async Task SavePacote(PacoteDto newPacote)
        {
            var pacote = _mapper.Map<Pacote>(newPacote);
            var i = 0;
            foreach (var item in pacote.Materias)
            {
                item.SetOrdem(i);
                i++;
            }
            pacote.SetCreateDate();
            await _pacoteRepo.Save(pacote);
            _pacoteRepo.Commit();
        }

        public async Task EditPacote(PacoteDto editPacote)
        {
            var pacote = _mapper.Map<Pacote>(editPacote);
            await _pacoteRepo.Edit(pacote);
            _pacoteRepo.Commit();
        }
    }
}
