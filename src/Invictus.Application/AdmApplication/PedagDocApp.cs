using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Administrativo.AlunoAggregate.Interface;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class PedagDocApp : IPedagDocApp
    {
        private readonly IPedagDocsQueries _pedagDocQueries;
        private readonly IMapper _mapper;
        private readonly IAlunoRepo _alunoRepo;
        public PedagDocApp(IPedagDocsQueries pedagDocQueries, IMapper mapper, IAlunoRepo alunoRepo)
        {
            _pedagDocQueries = pedagDocQueries;
            _mapper = mapper;
            _alunoRepo = alunoRepo;
        }
        public async Task ADdFileToDocument(Guid documentId, IFormFile file)
        {
            var docDto = await _pedagDocQueries.GetDocumentById(documentId);
            var doc = _mapper.Map<AlunoDocumento>(docDto);

            var fileName = Path.GetFileName(file.FileName);           

            var fileExtension = Path.GetExtension(fileName);

            //var newFileName = String.Concat(System.Convert.ToString(Guid.NewGuid()), fileExtension);

            byte[] arquivo = null;

            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                arquivo = target.ToArray();
            }

            var tamanho = file.Length / 1024;

            doc.AddDocumento(arquivo, fileName, fileExtension, file.ContentType, System.Convert.ToInt32(tamanho));
           

            await _alunoRepo.EditAlunoDoc(doc);



            _alunoRepo.Commit();
        }

        public async Task ExcluirDoc(Guid documentId)
        {
            var docDto = await _pedagDocQueries.GetDocumentById(documentId);
            var doc = _mapper.Map<AlunoDocumento>(docDto);

            doc.RemoveDocumento();

            await _alunoRepo.EditAlunoDoc(doc);

            _alunoRepo.Commit();
        }
    }
}
