//using AutoMapper;
//using Invictus.Application.AdmApplication.Interfaces;
//using Invictus.Domain.Administrativo.Models;
//using Invictus.Dtos.AdmDtos;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Invictus.Application.AdmApplication
//{
//    public class TemplateApplication : ITemplateApplication
//    {
//        //private readonly IModelRepository _modelRepo;
//        private readonly IMapper _mapper;
//        public TemplateApplication(//IModelRepository modelRepo, 
//            IMapper mapper)
//        {
//            //_modelRepo = modelRepo;
//            _mapper = mapper;
//        }
//        public async Task AddMateria(MateriaTemplateDto materiaDto)
//        {
//            var materia = _mapper.Map<MateriaTemplate>(materiaDto);
//            await _modelRepo.AddMateriaTemplate(materia);
//            _modelRepo.Save();
//        }

//        public async Task AddPlano(PlanoPagamentoDto planoDto)
//        {
//            var plano = _mapper.Map<PlanoPagamentoTemplate>(planoDto);
//            await _modelRepo.AddPlanoPagamento(plano);
//            _modelRepo.Save();
//        }

//        public async Task EditMateria(MateriaTemplateDto materiaDto)
//        {
//            var materia = _mapper.Map<MateriaTemplate>(materiaDto);
//            await _modelRepo.EditMateriaTemplate(materia);
//            _modelRepo.Save();
//        }

//        public async Task EditPlano(PlanoPagamentoDto planoDto)
//        {
//            var plano = _mapper.Map<PlanoPagamentoTemplate>(planoDto);
//            await _modelRepo.EditPlanoPagamento(plano);
//            _modelRepo.Save();
//        }
//    }
//}
