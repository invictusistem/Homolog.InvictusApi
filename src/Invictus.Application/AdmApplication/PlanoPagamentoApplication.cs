using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Domain.Administrativo.Models;
using Invictus.Domain.Administrativo.PlanoPagamento;
using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class PlanoPagamentoApplication : IPlanoPagamentoApplication
    {
        private readonly IMapper _mapper;
        private readonly IPlanoPgmRepository _planoRepo;
        public PlanoPagamentoApplication(IMapper mapper, IPlanoPgmRepository planoRepo)
        {
            _mapper = mapper;
            _planoRepo = planoRepo;
        }
        public async Task EditPlano(PlanoPagamentoDto editPlano)
        {
            var plano = _mapper.Map<PlanoPagamentoTemplate>(editPlano);

            await _planoRepo.EditPlano(plano);

            _planoRepo.Commit();
        }

        public async Task SavePlano(PlanoPagamentoDto newPlano)
        {
            var plano = _mapper.Map<PlanoPagamentoTemplate>(newPlano);

            await _planoRepo.CreatePlano(plano);

            _planoRepo.Commit();
        }
    }
}
