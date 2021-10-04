//using Invictus.Data.Context;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Invictus.Api.Data
//{
//    public class GetUserUnidadeId
//    {
//        private readonly InvictusDbContext _db;
//        public GetUserUnidadeId(InvictusDbContext db)
//        {
//            _db = db;
//        }
//        public static int GetUnidadeId()
//        {
//            HttpContextAccessor _httpContext = new HttpContextAccessor();
//            var unidade = _httpContext.HttpContext.User.FindFirst("Unidade").Value;
//            InvictusDbContext _db = new InvictusDbContext();
//            var unidadeId = _db.Unidades.Where(u => u.Sigla == unidade).Select(s => s.Id).SingleOrDefault();
//            return unidadeId;
//        }

//        public int GeId()
//        {
//            HttpContextAccessor _httpContext = new HttpContextAccessor();
//            var unidade = _httpContext.HttpContext.User.FindFirst("Unidade").Value;
//            InvictusDbContext _db = new InvictusDbContext();
//            var unidadeId = _db.Unidades.Where(u => u.Sigla == unidade).Select(s => s.Id).SingleOrDefault();
//            return unidadeId;
//        }
//    }
//}
