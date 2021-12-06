using Invictus.Data.Context;
using Invictus.Domain.Administrativo.ProfessorAggregate;
using Invictus.Domain.Administrativo.ProfessorAggregate.Interfaces;
using System;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly InvictusDbContext _db;
        public ProfessorRepository(InvictusDbContext db)
        {
            _db = db;
        }

        public async Task AddProfessor(Professor newProfessor)
        {
            await _db.Professores.AddAsync(newProfessor);
        }

        public async Task EditProfessor(Professor professor)
        {
            await _db.Professores.SingleUpdateAsync(professor);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task AddProfessorMateria(MateriaHabilitada profMateria)
        {
            await _db.MateriasHabilitadas.AddAsync(profMateria);
        }

        public async Task RemoveProfessorMateria(Guid profMateriaId)
        {
            await _db.MateriasHabilitadas.DeleteByKeyAsync(profMateriaId);//.AddAsync(profMateria);
        }

        public async Task AddDisponibilidade(Disponibilidade disponibilidade)
        {
            await _db.Disponibilidades.AddAsync(disponibilidade);
        }
    }
}
