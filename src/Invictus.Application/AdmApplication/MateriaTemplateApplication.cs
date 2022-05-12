using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Domain.Administrativo.MatTemplate.Interfaces;
using Invictus.Domain.Administrativo.Models;
using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class MateriaTemplateApplication : IMateriaTemplateApplication
    {
        private readonly IMateriaRepo _materiaRepo;
        private readonly IMapper _mapper;
        public MateriaTemplateApplication(IMateriaRepo materiaRepo, IMapper mapper)
        {
            _materiaRepo = materiaRepo;
            _mapper = mapper;
        }
        public async Task AddMateria(MateriaTemplateDto materiaDto)
        {
            var mat = _mapper.Map<MateriaTemplate>(materiaDto);
            await _materiaRepo.Save(mat);
            _materiaRepo.Commit();
        }

        public async Task DeleteMateria(Guid materiaId)
        {   
            await _materiaRepo.Remove(materiaId);
            _materiaRepo.Commit();
        }

        public async Task EditMateria(MateriaTemplateDto materiaDto)
        {
            var mat = _mapper.Map<MateriaTemplate>(materiaDto);
            await _materiaRepo.Edit(mat);
            _materiaRepo.Commit();
        }
    }
}
