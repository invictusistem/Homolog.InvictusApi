using Invictus.Data.Context;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Administrativo.TurmaAggregate.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Data.Repositories.Administrativo
{
    public class TurmaRepo : ITurmaRepo
    {
        private readonly InvictusDbContext _db;
        public TurmaRepo(InvictusDbContext db)
        {
            _db = db;
        }

        public async Task AddProfsNaTurma(IEnumerable<TurmaProfessor> professores)
        {
            await _db.TurmasProfessores.AddRangeAsync(professores);
        }

        public async Task AdiarInicio(Guid turmaId)
        {
            var turma = await _db.Turmas.FindAsync(turmaId);
            var previsoes = await _db.Previsoes.Where(previsoes => previsoes.TurmaId == turmaId).FirstOrDefaultAsync();
            turma.Previsao.AdiarInicio(turma.Previsao.PrevisaoInfo, previsoes);
            await _db.Turmas.SingleUpdateAsync(turma);
            Commit();
        }

        public void Commit()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Edit(Turma turma)
        {
            await _db.Turmas.SingleUpdateAsync(turma);
        }

        public async Task IniciarTurma(Guid turmaId)
        {
            var turmas = await _db.Turmas.FindAsync(turmaId);
            turmas.IniciarTurma();
            await _db.Turmas.SingleUpdateAsync(turmas);
            _db.SaveChanges();
        }

        public async Task Save(Turma turma)
        {
            await _db.Turmas.AddAsync(turma);
        }

        public async Task SavePrevisoes(Previsoes previ)
        {
            await _db.Previsoes.AddAsync(previ);
        }

        public async Task UpdateMateriaDaTurma(TurmaMaterias turmaMateria)
        {
            await _db.TurmasMaterias.SingleUpdateAsync(turmaMateria);
        }

        public async Task RemoverProfessorDaTurma(TurmaProfessor professor)
        {   
            await _db.TurmasProfessores.SingleDeleteAsync(professor);
        }

        public void AtualizarTurmasMaterias(IEnumerable<TurmaMaterias> turmasMaterias)
        {
            _db.TurmasMaterias.UpdateRange(turmasMaterias);
        }
    }
}
