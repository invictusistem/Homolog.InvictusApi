using Dapper;
using Invictus.Application.Dtos;
using Invictus.Application.Queries.Interfaces;
using Invictus.Core;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Queries
{
    public class ColaboradorQueries : IColaboradorQueries
    {
        private readonly IConfiguration _config;
        public ColaboradorQueries(IConfiguration config)
        {
            _config = config;
        }

        public async Task<PaginatedItemsViewModel<ColaboradorDto>> GetColaboradores(int itemsPerPage, int currentPage, ParametrosDTO param, int unidadeId)
        {
            //var ativos = param.ativo;
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * from Colaborador where ");
            if (param.nome != "") query.Append("LOWER(colaborador.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.email != "") query.Append("AND LOWER(colaborador.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.cpf != "") query.Append("AND LOWER(colaborador.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            query.Append("AND Colaborador.UnidadeId = " + unidadeId);
            if (param.ativo == false) query.Append(" AND Colaborador.Ativo = 'True' ");
            query.Append(" ORDER BY Colaborador.Nome ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append("SELECT Count(*) from Colaborador where ");
            if (param.nome != "") queryCount.Append("LOWER(colaborador.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.email != "") queryCount.Append("AND LOWER(colaborador.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.cpf != "") queryCount.Append("AND LOWER(colaborador.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            queryCount.Append("AND Colaborador.UnidadeId = " + unidadeId);
            if (param.ativo == false) queryCount.Append(" AND Colaborador.Ativo = 'True' ");
           

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                var countItems = await connection.QuerySingleAsync<int>(queryCount.ToString(), new { unidadeId = unidadeId });

                var results = await connection.QueryAsync<ColaboradorDto>(query.ToString(), new { currentPage = currentPage, itemsPerPage = itemsPerPage });

                var paginatedItems = new PaginatedItemsViewModel<ColaboradorDto>(currentPage, itemsPerPage, countItems, results.ToList());

                connection.Close();

                return paginatedItems;

            }
        }

        //public async Task<PaginatedItemsViewModel<ColaboradorDto>> GetProfessores(string unidade)
        public async Task<IEnumerable<ColaboradorDto>> GetProfessores(string unidade, int turmaId)
        {
            var query = @"select * from
                        colaborador 
                        where colaborador.id not in(
                        select ProfId 
                        from ProfessorNew 
                        where ProfessorNew.TurmaId = @turmaId
                        )
                        and Colaborador.Cargo = 'Professor' 
                        and Colaborador.UnidadeId = (select id from Unidade where Unidade.Sigla = @unidade) ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<ColaboradorDto>(query, new { unidade = unidade, turmaId = turmaId });

                return results;
            }
        }

        public async Task<IEnumerable<ProfessoresDto>> GetProfessoresByTurmaId(int turmaId)
        {
            //var query = @"select 
            //            Colaborador.id, 
            //            Colaborador.nome, 
            //            Colaborador.email,
            //            MateriasDaTurma.id, 
            //            MateriasDaTurma.materiaid,
            //            MateriasDaTurma.descricao
            //            from Colaborador 
            //            left join MateriasDaTurma on
            //            Colaborador.Id = MateriasDaTurma.ProfId 
            //            where Colaborador.Id in (
            //            select profId from ProfessorNew 
            //            where ProfessorNew.TurmaId = @turmaId )";

            var query = @"select 
                        Colaborador.Id,
                        Colaborador.Nome, 
                        Colaborador.Email,
                        MateriasDaTurma.Id, 
                        Materias.Id as materiaId,
                        Materias.Descricao
                         from ProfessorNew 
                        left join MateriasDaTurma on ProfessorNew.Id = MateriasDaTurma.ProfessorId
                        left join Colaborador on ProfessorNew.ProfId = Colaborador.Id
                        --left join MateriasDaTurma on ProfessorNew.Id = MateriasDaTurmaProfessorId 
                        left join Materias on MateriasDaTurma.MateriaId = Materias.Id 
                        where ProfessorNew.TurmaId = @turmaId";



            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                //var profsDictionary = new Dictionary<int, ProfessoresDto>();
                connection.Open();

                //var profs = await connection.QueryAsync<ProfessoresDto>(query, new { turmaId = turmaId });

                //var listIds = new List<int>();
                //if (profs.Count() > 0)
                //{
                //    foreach (var item in profs)
                //    {
                //        var id = item.id;
                //        item.materias = await connection.QueryAsync<MateriaDto>(query2, new { turmaId = turmaId, id = id });
                //    }
                //}            


                var profsDictionary = new Dictionary<int, ProfessoresDto>();

                var list = connection.Query<ProfessoresDto, ProfessoresMateriaDto,
                    ProfessoresDto>(
                    query,
                    (professoresDto, materiaDto) =>
                    {
                        ProfessoresDto professoresEntry;

                        if (!profsDictionary.TryGetValue(professoresDto.id, out professoresEntry))
                        {
                            professoresEntry = professoresDto;
                            professoresEntry.materias = new List<ProfessoresMateriaDto>();
                            profsDictionary.Add(professoresEntry.id, professoresEntry);
                        }

                        if (materiaDto != null)
                        {
                            professoresEntry.materias.Add(materiaDto);
                        }
                        return professoresEntry;

                    }, new { turmaId = turmaId }, splitOn: "Id").Distinct().ToList();

                //foreach (var item in list)
                //{
                //    if (item.materias[0].id == 0)
                //    {
                //        item.materias = null;
                //        item.materias = new List<ProfessoresMateriaDto>();
                //    }
                //}



                return list;


            }

        }

        public async Task<PaginatedItemsViewModel<ColaboradorDto>> GetUsuarios(QueryDto param, int itemsPerPage, int currentPage)
        {

            var query = "SELECT Colaborador.id, Colaborador.nome, Colaborador.email, Colaborador.perfil, Colaborador.perfilAtivo from Colaborador " +
                        "inner join AspNetUsers on Colaborador.Email = AspNetUsers.Email " +
                        "where LOWER(Colaborador.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND " +
                        "LOWER(Colaborador.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND " +
                        "LOWER(Colaborador.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI " +
                        "ORDER BY Colaborador.nome " +
                        "OFFSET(@currentPage - 1) * @itemsPerPage ROWS FETCH NEXT @itemsPerPage ROWS ONLY";
            //var query = @"SELECT id, nome, email, perfil, perfilAtivo from Colaborador 
            //            where colaborador.perfil IS NOT NULL AND 
            //            Colaborador.Ativo = 'True' ORDER BY Colaborador.Nome   
            //            OFFSET(@currentPage - 1) * @itemsPerPage ROWS FETCH NEXT @itemsPerPage ROWS ONLY";

            var queryCount = "select count(*) from Colaborador";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<ColaboradorDto>(query, new { currentPage = currentPage, itemsPerPage = itemsPerPage });

                var paginatedItems = new PaginatedItemsViewModel<ColaboradorDto>(currentPage, itemsPerPage, countItems, results.ToList());

                return paginatedItems;

            }
        }

        public async Task<ColaboradorDto> SearhColaborador(string email)
        {
            string query = "select id, nome, email, celular, cargo, perfil from Colaborador " +
                           "where LOWER(Colaborador.email) = @email collate SQL_Latin1_General_CP1_CI_AI";// AND ";// +
                                                                                                          //"Colaborador.Unidade = 'CGI' ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                // var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var result = await connection.QueryAsync<ColaboradorDto>(query, new { email = email });

                ///var paginatedItems = new PaginatedItemsViewModel<ColaboradorDto>(currentPage, itemsPerPage, countItems, results);

                return result.FirstOrDefault();

            }
        }
    }
}
