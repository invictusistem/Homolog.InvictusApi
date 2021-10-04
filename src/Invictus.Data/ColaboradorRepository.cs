using Invictus.Data.Context;
using Invictus.Domain;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Invictus.Domain.Repository;

namespace Invictus.Data
{
    public class ColaboradorRepository  : IColaboradorRepository
    {
        private readonly InvictusDbContext _db;
        public ColaboradorRepository(InvictusDbContext db)
        {
            _db = db;
        }
        public void AddColaborador(Colaborador newColaborador)
        {
            _db.Colaboradores.Add(newColaborador);
            _db.SaveChanges();
        }

        public void EditColaborador(int colaboradorId, string perfil, bool perfilAtivo)
        {
            var colaborador = _db.Colaboradores.Find(colaboradorId);
            colaborador.SetPerfil(perfil, perfilAtivo);
            _db.Colaboradores.Update(colaborador);
            _db.SaveChanges();
        }

        public void UpdateColaborador(Colaborador colaborador)
        {
            //var colaborador = _db.Colaboradores.Find(colaboradorId);
            //colaborador.DesativarColaborador();// .Ativo = true;
            _db.Colaboradores.Update(colaborador);// .Remove(colaborador);
            _db.SaveChanges();
        }

        public void DeleteColaborador(int colaboradorId)
        {
            var colaborador = _db.Colaboradores.Find(colaboradorId);
            colaborador.DesativarColaborador();// .Ativo = true;
            _db.Colaboradores.Update(colaborador);// .Remove(colaborador);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        
    }
}
