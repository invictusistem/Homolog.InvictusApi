using AutoMapper;
using Invictus.Application.PedagApplication.Interfaces;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Padagogico.Estagio;
using Invictus.Domain.Padagogico.Estagio.Interfaces;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Application.PedagApplication
{
    public class EstagioApplication : IEstagioApplication
    {
        private readonly IMapper _mapper;
        private readonly IEstagioRepo _estagioRepo;
        private readonly IPedagMatriculaQueries _pedagQueries;
        private readonly IAspNetUser _aspNetUser;
        private readonly InvictusDbContext _db;
        public EstagioApplication(IMapper mapper, IEstagioRepo estagioRepo, IPedagMatriculaQueries pedagQueries, InvictusDbContext db, IAspNetUser aspNetUser)
        {
            _mapper = mapper;
            _estagioRepo = estagioRepo;
            _pedagQueries = pedagQueries;
            _db = db;
            _aspNetUser = aspNetUser;
        }

        public async Task AprovarDocumento(Guid documentoId, bool aprovar)
        {
            var doc = await _db.DocumentosEstagio.FindAsync(documentoId);

            var userId = _aspNetUser.ObterUsuarioId();

            doc.ValidarDoc(aprovar, userId);

            await _db.DocumentosEstagio.SingleUpdateAsync(doc);

            _db.SaveChanges();

            // verificar status matricula
            var docs = await _db.DocumentosEstagio.Where(d => d.MatriculaEstagioId == doc.MatriculaEstagioId).ToListAsync();

            var docsAprovados = docs.Where(d => d.Status == "Aprovado");
            if (docsAprovados.Count() == docs.Count())
            {
                var estagioMatricula = await _db.MatriculasEstagios.FindAsync(doc.MatriculaEstagioId);

                estagioMatricula.ChangeStatusEstagioMatricula(StatusMatricula.AguardoEscolha);

                await _db.MatriculasEstagios.SingleUpdateAsync(estagioMatricula);

                _db.SaveChanges();
            }
            
        }

        public async Task CreateEstagio(EstagioDto estagioDto)
        {
            var estagio = _mapper.Map<Estagio>(estagioDto);

            await _estagioRepo.CreateEstagio(estagio);

            _estagioRepo.Commit();
        }

        public async Task CreateTypeEstagio(TypeEstagioDto typeEstagio)
        {
            //var exceptions = new List<string>();

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

        public async Task LiberarMatricula(LiberarEstagioCommand command)
        {
            var matricula = await _pedagQueries.GetMatriculaById(command.matriculaId);
            
            var estagioMatricula = MatriculaEstagio.MatriculaEstagioFactory(matricula.numeroMatricula, command.matriculaId, command.tipoEstagioId);

            await _estagioRepo.CreateMatricula(estagioMatricula);

            _estagioRepo.Commit();
        }
    }
}
