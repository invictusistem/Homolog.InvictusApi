using AutoMapper;
using Invictus.Application.PedagApplication.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.Calendarios.Interfaces;
using Invictus.Domain.Pedagogico.Responsaveis;
using Invictus.Domain.Pedagogico.Responsaveis.Interfaces;
using Invictus.Dtos.PedagDto;
using System;
using System.Threading.Tasks;

namespace Invictus.Application.PedagApplication
{
    public class PedagogicoApplication : IPedagogicoApplication
    {
        public readonly IMapper _mapper;
        public readonly IRespRepo _respRepo;
        public readonly ICalendarioRepo _calendRepo;
        public InvictusDbContext _db;
        public PedagogicoApplication(IMapper mapper, IRespRepo respRepo, ICalendarioRepo calendRepo, InvictusDbContext db)
        {
            _mapper = mapper;
            _respRepo = respRepo;
            _calendRepo = calendRepo;
            _db = db;
        }
        public async Task EditResponsavel(ResponsavelDto responsavel)
        {
            var resp = _mapper.Map<Responsavel>(responsavel);
            resp.SetRespFinanceiro(responsavel.temRespFin);

            await _respRepo.Edit(resp);

            _respRepo.Commit();
        }

        public async Task IniciarAula(Guid calendarioId)
        {
            var calendario = await _db.Calendarios.FindAsync(calendarioId);
            calendario.IniciarAula();
            await _calendRepo.UpdateCalendario(calendario);
            _calendRepo.Commit();

        }
    }
}
