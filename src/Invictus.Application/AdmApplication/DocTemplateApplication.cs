using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Domain.Administrativo.DocumentacaoTemplateAggregate;
using Invictus.Domain.Administrativo.DocumentacaoTemplateAggregate.Interface;
using Invictus.Dtos.AdmDtos;
using System;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class DocTemplateApplication : IDocTemplateApplication
    {
        private readonly IDocTemplateRepository _docRepo;
        private readonly IMapper _mapper;
        public DocTemplateApplication(IDocTemplateRepository docRepo, IMapper mapper)
        {
            _docRepo = docRepo;
            _mapper = mapper;
        }
        public async Task AddDoc(DocumentacaoTemplateDto newDoc)
        {
            var doc = _mapper.Map<DocumentacaoTemplate>(newDoc);
            await _docRepo.SaveDoc(doc);
            _docRepo.Commit();
        }

        public async Task EditDoc(DocumentacaoTemplateDto editedDoc)
        {
            var doc = _mapper.Map<DocumentacaoTemplate>(editedDoc);
            await _docRepo.EditDoc(doc);
            _docRepo.Commit();
        }

        public async Task RemoveDoc(Guid documentoId)
        {
            await _docRepo.Delete(documentoId);
            _docRepo.Commit();
        }
    }
}
