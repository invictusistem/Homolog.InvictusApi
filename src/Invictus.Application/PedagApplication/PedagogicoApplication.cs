using AutoMapper;
using Invictus.Application.PedagApplication.Interfaces;
using Invictus.Domain.Pedagogico.Responsaveis;
using Invictus.Domain.Pedagogico.Responsaveis.Interfaces;
using Invictus.Dtos.PedagDto;
using System.Threading.Tasks;

namespace Invictus.Application.PedagApplication
{
    public class PedagogicoApplication : IPedagogicoApplication
    {
        public readonly IMapper _mapper;
        public readonly IRespRepo _respRepo;
        public PedagogicoApplication(IMapper mapper, IRespRepo respRepo)
        {
            _mapper = mapper;
            _respRepo = respRepo;
        }
        public async Task EditResponsavel(ResponsavelDto responsavel)
        {
            var resp = _mapper.Map<Responsavel>(responsavel);
            resp.SetRespFinanceiro(responsavel.temRespFin);

            await _respRepo.Edit(resp);

            _respRepo.Commit();
        }
    }
}
