//using Dapper;
//using Invictus.Dtos.AdmDtos;
//using Invictus.QueryService.AdministrativoQueries.Interfaces;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Invictus.QueryService.AdministrativoQueries
//{
//    public class AdmQueries : IAdmQueries
//    {
//        private readonly IConfiguration _config;
//        public AdmQueries(IConfiguration config)
//        {
//            _config = config;
//        }

//        public async Task<T> GenericSearchById<T>(string table, int id)
//        {
//            StringBuilder query = new StringBuilder();

//            query.AppendFormat(@"SELECT * FROM {0} WHERE {1}.id = @id", table, table);

//            //if (column != null)
//            //    query.AppendFormat(@"WHERE {0}.{1} = @paramOne ", table, column);
//            //if (filterByUnidade)
//            //{
//            //    var unidadeId = GetUnidadeId();
//            //    query.AppendFormat(@"AND {0}.UnidadeId = " + unidadeId, table);
//            //}


//            await using (var connection = new SqlConnection(
//                    _config.GetConnectionString("InvictusConnection")))
//            {
//                connection.Open();

//                var resultado = await connection.QuerySingleAsync<T>(query.ToString(), new { id = id });

//                connection.Close();

//                return resultado;
//            }
//        }

        
//        public async Task<ColaboradorDto> GetColaboradorByEmail(string email)
//        {
//            string query = @"select * from Colaborador where Colaborador.email = @email";

//            await using (var connection = new SqlConnection(
//                    _config.GetConnectionString("InvictusConnection")))
//            {
//                connection.Open();

//                var colaborador = await connection.QuerySingleAsync<ColaboradorDto>(query, new { email = email });

//                connection.Close();

//                return colaborador;
//            }
//        }

        
               


      

//        public async Task<IEnumerable<ParametrosValueDto>> GetParametrosValue(string paramKey)
//        {           
//            var query = @"SELECT * FROM PametrosValue WHERE PametrosValue.ParametrosKeyId = (
//                            SELECT Id FROM PametrosKey WHERE 
//                            [PametrosKey].[Key] = @paramKey)";


//            await using (var connection = new SqlConnection(
//                    _config.GetConnectionString("InvictusConnection")))
//            {
//                connection.Open();

//                var results = await connection.QueryAsync<ParametrosValueDto>(query, new { paramKey = paramKey });

//                connection.Close();

//                return results;
//            }
//        }

       

       

//        //public async Task<T> GenericSearch<T>(string table, string column, string paramOne = null, bool filterByUnidade = false)
//        //{
//        //    //var unidadeId = 0;

//        //    StringBuilder query = new StringBuilder();

//        //    query.AppendFormat(@"SELECT * FROM {0} ", table);

//        //    if (column != null)
//        //        query.AppendFormat(@"WHERE {0}.{1} = @paramOne ", table, column);
//        //    if (filterByUnidade)
//        //    {
//        //        var unidadeId = GetUnidadeId();
//        //        query.AppendFormat(@"AND {0}.UnidadeId = " + unidadeId, table);
//        //    }


//        //    await using (var connection = new SqlConnection(
//        //            _config.GetConnectionString("InvictusConnection")))
//        //    {
//        //        connection.Open();

//        //        var resultado = await connection.QuerySingleAsync<T>(query.ToString(), new { paramOne = paramOne });

//        //        connection.Close();

//        //        return resultado;
//        //    }
//        //}

//        //public async Task<IEnumerable<T>> GenericSearchList<T>(string table, string column, string paramOne)
//        //{
//        //    StringBuilder query = new StringBuilder();

//        //    query.AppendFormat(@"SELECT * FROM {0} ", table);

//        //    query.AppendFormat(@"WHERE {0}.{1} = @paramOne ", table, column);

//        //    //query.AppendFormat(@"AND {0}.{1} = @paramOne ", table, column);


//        //    await using (var connection = new SqlConnection(
//        //            _config.GetConnectionString("InvictusConnection")))
//        //    {
//        //        connection.Open();

//        //        var resultado = await connection.QueryAsync<T>(query.ToString(), new { paramOne = paramOne });

//        //        connection.Close();

//        //        return resultado;
//        //    }
//        //}

//        //public async Task<IEnumerable<PacoteDto>> GetPacotes()
//        //{
//        //    var unidadeId = GetUnidadeId();
//        //    string query = @"select * from Pacote left join Materia on Pacote.id = Materia.PacoteId 
//        //                    WHERE Pacote.UnidadeId = @unidadeId";

//        //    await using (var connection = new SqlConnection(
//        //            _config.GetConnectionString("InvictusConnection")))
//        //    {
//        //        connection.Open();

//        //        var alunoDictionary = new Dictionary<int, PacoteDto>();

//        //        var pacotes = connection.Query<PacoteDto, MateriaDto, PacoteDto>(query,
//        //            (pacotesDto, salaDto) =>
//        //            {
//        //                PacoteDto pacoteEntry;

//        //                if (!alunoDictionary.TryGetValue(pacotesDto.id, out pacoteEntry))
//        //                {
//        //                    pacoteEntry = pacotesDto;
//        //                    pacoteEntry.materias = new List<MateriaDto>();
//        //                    alunoDictionary.Add(pacoteEntry.id, pacoteEntry);
//        //                }

//        //                if (salaDto != null)
//        //                {
//        //                    pacoteEntry.materias.Add(salaDto);
//        //                }
//        //                return pacoteEntry;

//        //            }, new { unidadeId = unidadeId }, splitOn: "Id").Distinct().ToList();

//        //        connection.Close();

//        //        return pacotes;
//        //    }
//        //}

//        //public async Task<IEnumerable<ContratoDto>> GetContratos()
//        //{
//        //    //var id = await _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).SingleOrDefaultAsync();
//        //    var query = @"select 
//        //                    Contrato.Id,
//        //                    Contrato.CodigoContrato, 
//        //                    Contrato.Titulo, 
//        //                    Contrato.PodeEditar,
//        //                    Contrato.DataCriacao,
//        //                    TypePacote.Nome as pacoteNome 
//        //                    from Contrato 
//        //                    inner join TypePacote on Contrato.PacoteId = TypePacote.Id ";

//        //    await using (var connection = new SqlConnection(
//        //            _config.GetConnectionString("InvictusConnection")))
//        //    {
//        //        connection.Open();

//        //        var contratos = await connection.QueryAsync<ContratoDto>(query);

//        //        connection.Close();

//        //        return contratos;

//        //    }
//        //}

//        //public async Task<ContratoDto> GetContratoById(int contratoId)
//        //{
//        //    var query = @"SELECT * FROM Contrato WHERE Contrato.Id = @contratoId ";

//        //    var queryConteudo = @"SELECT * FROM ContratoConteudo WHERE ContratoConteudo.ContratoId = @contratoId ";

//        //    await using (var connection = new SqlConnection(
//        //            _config.GetConnectionString("InvictusConnection")))
//        //    {
//        //        connection.Open();
//        //        var contrato = await connection.QuerySingleAsync<ContratoDto>(query, new { contratoId = contratoId });

//        //        var contratoConteudo = await connection.QueryAsync<ContratoConteudoDto>(queryConteudo, new { contratoId = contratoId });

//        //        foreach (var item in contratoConteudo.OrderBy(c => c.order))
//        //        {
//        //            contrato.conteudo += item.content;
//        //        }

//        //        connection.Close();

//        //        return contrato;
//        //    }
//        //}

//        //public async Task<int> TableCount(string table, int unidadeId = 0)
//        //{
//        //    //SELECT Count(*) from Colaborador
//        //    StringBuilder query = new StringBuilder();

//        //    query.AppendFormat(@"SELECT Count(*) FROM {0} ", table);
//        //    if (unidadeId != 0)
//        //        query.AppendFormat(@"WHERE {0}.unidadeId = " + unidadeId, table);


//        //    await using (var connection = new SqlConnection(
//        //            _config.GetConnectionString("InvictusConnection")))
//        //    {
//        //        connection.Open();

//        //        var count = await connection.QuerySingleAsync<int>(query.ToString());

//        //        connection.Close();

//        //        return count;
//        //    }

//        //}

//        //private async Task<int> GetUnidadeId()
//        //{
//        //    HttpContextAccessor ctx = new HttpContextAccessor();
//        //    var unidadeSigla = ctx.HttpContext.User.FindFirst("Unidade").Value;
//        //    StringBuilder query = new StringBuilder();

//        //    query.AppendFormat(@"SELECT Id FROM Unidade Where Unidade.Sigla = @unidadeSigla ");

//        //    await using (var connection = new SqlConnection(
//        //            _config.GetConnectionString("InvictusConnection")))
//        //    {
//        //        connection.Open();

//        //        var id = await connection.QuerySingleAsync<int>(query.ToString(), new { unidadeSigla = unidadeSigla });

//        //        connection.Close();

//        //        return id;
//        //    }
//        //}
//    }
//}
