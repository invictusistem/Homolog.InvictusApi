using AutoMapper;
using Invictus.Application.PedagApplication.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Padagogico.Estagio;
using Invictus.Domain.Padagogico.Estagio.Interfaces;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using System;
using System.Threading.Tasks;

namespace Invictus.Application.PedagApplication
{
    public class EstagioApplication : IEstagioApplication
    {
        private readonly IMapper _mapper;
        private readonly IEstagioRepo _estagioRepo;
        private readonly IPedagMatriculaQueries _pedagQueries;
        private readonly InvictusDbContext _db;
        public EstagioApplication(IMapper mapper, IEstagioRepo estagioRepo, IPedagMatriculaQueries pedagQueries, InvictusDbContext db)
        {
            _mapper = mapper;
            _estagioRepo = estagioRepo;
            _pedagQueries = pedagQueries;
            _db = db;
        }

        public async Task AprovarDocumento(Guid documentoId, bool aprovar)
        {
            var doc = await _db.DocumentosEstagio.FindAsync(documentoId);

            doc.ValidarDoc(aprovar);

            await _db.DocumentosEstagio.SingleUpdateAsync(doc);

            _db.SaveChanges();
        }

        public async Task CreateEstagio(EstagioDto estagioDto)
        {
            var estagio = _mapper.Map<Estagio>(estagioDto);

            await _estagioRepo.CreateEstagio(estagio);

            _estagioRepo.Commit();
        }

        public async Task CreateTypeEstagio(TypeEstagioDto typeEstagio)
        {
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
