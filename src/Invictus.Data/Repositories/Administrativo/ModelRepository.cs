//using Invictus.Data.Context;
//using Invictus.Domain.Administrativo.Models;
//using Invictus.Domain.Administrativo.Models.Interfaces;
//using Invictus.Domain.Administrativo.PlanoPagamento;
//using System.Threading.Tasks;

//namespace Invictus.Data.Repositories.Administrativo
//{
//    public class ModelRepository : IModelRepository
//    {
//        private readonly InvictusDbContext _db;
//        public ModelRepository(InvictusDbContext db)
//        {
//            _db = db;
//        }



//        public void Dispose()
//        {
//            _db.Dispose();
//        }

//        public void Save()
//        {
//            _db.SaveChanges();
//        }

//        public async Task AddMateriaTemplate(MateriaTemplate materia)
//        {
//            await _db.MateriasTemplates.AddAsync(materia);
//        }

//        public async Task EditMateriaTemplate(MateriaTemplate materia)
//        {
//            await _db.MateriasTemplates.SingleUpdateAsync(materia);
//        }

//        public async Task AddPlanoPagamento(PlanoPagamentoTemplate newPlano)
//        {
//            await _db.PlanosPgmTemplate.AddAsync(newPlano);
//        }

//        public async Task EditPlanoPagamento(PlanoPagamentoTemplate newPlano)
//        {
//            await _db.PlanosPgmTemplate.SingleUpdateAsync(newPlano);
//        }



//        // PLANO PGM
       
//    }
//}
