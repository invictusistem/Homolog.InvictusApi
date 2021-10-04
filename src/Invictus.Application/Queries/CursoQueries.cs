using Invictus.Application.Dtos;
using Invictus.Application.Queries.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using MoreLinq;
using Invictus.Data.Context;

namespace Invictus.Application.Queries
{
    public class CursoQueries : ICursoQueries
    {
        private readonly IConfiguration _config;
        private readonly InvictusDbContext _context;
        public CursoQueries(IConfiguration config, InvictusDbContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<List<AlunoDto>> GetAlunosDaTurma(int turmaId)
        {
            var query = @"select 
                            aluno.nome, 
                            aluno.email, 
                            aluno.cpf
                            from aluno
                            where aluno.id in ( 
                            select Matriculados.AlunoId from matriculados 
                            where Matriculados.TurmaId = @turmaId)";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var alunos = await connection.QueryAsync<AlunoDto>(query, new { turmaId = turmaId });

                return alunos.ToList();

            }
        }

        public async Task<IEnumerable<TurmaViewModel>> GetCursoById(int cursoId)
        {
            var query = @"select * from Turma inner join previsoes on turma.id = previsoes.turmaid where Turma.id = @cursoId";
            //select * from turma where Turma.Descricao like '%Enfermagem%'


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<TurmaViewModel, PrivisoesDto, TurmaViewModel>(
                        query,
                        (turmaViewModel, privisoesDto) =>
                        {
                            turmaViewModel.previsoes = privisoesDto;
                            return turmaViewModel;
                        }, new { cursoId = cursoId },
                        splitOn: "Id");

                connection.Close();

                return results;//.OrderBy(d => d.prevInicio);

            }
        }

        public async Task<IEnumerable<TurmaViewModel>> GetCursos(string unidade)
        {
            //throw new NotImplementedException();
            //var query = @"SELECT dbo.Turma.Id, dbo.Turma.Identificador, 
            //            dbo.Turma.Descricao, dbo.Turma.Turno, 
            //            dbo.Turma.Vagas, 
            //            dbo.Turma.TotalAlunos, 
            //            dbo.Turma.Iniciada, 
            //            dbo.Turma.Previsao, 
            //            dbo.Turma.PrevisaoAtual
            //            dbo.Previsoes.id as PrevisoesId, 
            //            dbo.Previsoes.PrevisionStartOne, 
            //            dbo.Previsoes.PrevisionStartTwo, 
            //            dbo.Previsoes.PrevisionStartThree, 
            //            dbo.Previsoes.PrevisionEndingOne, 
            //            dbo.Previsoes.PrevisionEndingTwo, 
            //            dbo.Previsoes.PrevisionEndingThree 
            //            From dbo.Turma
            //            INNER JOIN dbo.Previsoes ON dbo.Turma.Id = dbo.Previsoes.TurmaId
            //            Order BY Previsoes.PrevisionStartOne desc 
            //            OFFSET(@currentPage - 1) * @itemsPerPage 
            //            ROWS FETCH NEXT @itemsPerPage 
            //            ROWS ONLY";

            var query = @"SELECT Turma.Id, 
                        Turma.Identificador, 
                        Turma.Descricao, 
                        Turma.StatusDaTurma, 
                        Turma.TotalAlunos, 
                        Turma.Iniciada, 
                        Turma.Previsao, 
                        Turma.PrevisaoAtual, 
                        Turma.PrevisaoTerminoAtual, 
                        Salas.Capacidade as Vagas 
                        From Turma 
                        inner join Salas on Turma.SalaId = Salas.Id 
                        Where Turma.UnidadeId = (select id from Unidade Where Unidade.Sigla = @unidade)  
                        Order BY Turma.Identificador ";
            //OFFSET(@currentPage - 1) * @itemsPerPage 
            //ROWS FETCH NEXT @itemsPerPage 
            //ROWS ONLY";
            var queryCount = "select count(*) from Turma";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                //var turmaDictionary = new Dictionary<int, TurmaViewModel>();

                connection.Open();
                var countItems = await connection.QuerySingleAsync<int>(queryCount);


                var results = await connection.QueryAsync<TurmaViewModel>(query, new { unidade = unidade });
                //var list = connection.Query<TurmaViewModel, PrivisoesDto,
                //    TurmaViewModel>(
                //    query,
                //    (turmaViewModel, privisoesDto) =>
                //    {
                //        TurmaViewModel turmaEntry;

                //        if (!turmaDictionary.TryGetValue(turmaViewModel.id, out turmaEntry))
                //        {
                //            turmaEntry = turmaViewModel;
                //            turmaEntry.previsoes = new PrivisoesDto();
                //            turmaDictionary.Add(turmaEntry.id, turmaEntry);
                //        }

                //        turmaEntry.previsoes = privisoesDto;

                //        return turmaEntry;

                //    }, new { itemsPerPage = itemsPerPage, currentPage = currentPage }, splitOn: "PrevisoesId").Distinct().ToList();

                //var paginatedItems = new PaginatedItemsViewModel<TurmaViewModel>(currentPage, itemsPerPage, countItems, list);

                //return paginatedItems;


                //var paginatedItems = new PaginatedItemsViewModel<TurmaViewModel>(currentPage, itemsPerPage, countItems, results.ToList());

                return results;

            }

            /*
             * var templateDictionary = new Dictionary<int, TemplateDTO>();
             connection.Open();

                var list = connection.Query<TemplateDTO, TemplateTaskDTO,
                    TemplateDTO>(
                    query,
                    (templateDTO, templateTaskDTO) =>
                    {
                        TemplateDTO templateEntry;

                        if (!templateDictionary.TryGetValue(templateDTO.id, out templateEntry))
                        {
                            templateEntry = templateDTO;
                            templateEntry.templatesTasks = new List<TemplateTaskDTO>();
                            templateDictionary.Add(templateEntry.id, templateEntry);
                        }

                        templateEntry.templatesTasks.Add(templateTaskDTO);

                        return templateEntry;

                    }, splitOn: "Id").Distinct().ToList();

                return list;
             */
        }

        public async Task<IEnumerable<TurmaViewModel>> GetCursosAndamento(string curso, int[] typePacoteIds, int unidadeId)
        {
            var query = @"select 
                          Turma.id, 
                          Turma.identificador, 
                          Turma.descricao, 
                          Turma.iniciada, 
                          Turma.totalAlunos, 
                          Turma.statusDaTurma, 
                          Turma.PacoteId, 
                          Turma.ModuloId, 
                          Salas.Capacidade as vagas,
                          Turma.PrevisaoAtual, 
                          Turma.PrevisaoTerminoAtual, 
                          Horarios.Id, 
                          Horarios.WeekDayOne, 
                          Horarios.InitialHourOne, 
                          Horarios.FinalHourOne 
                          from Turma 
                          inner join Horarios on 
                          Turma.Id = Horarios.TurmaId  
                          inner join Modulos on Turma.ModuloId = Modulos.Id 
                          inner join Salas on Turma.SalaId = Salas.Id 
                          where turma.pacoteId not in @typePacoteIds 
                          AND Modulos.UnidadeId = @unidadeId";
                          //WHERE turma.Descricao like '%" + curso + "%' ";
            //select * from turma where Turma.Descricao like '%Enfermagem%'

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<TurmaViewModel>(query, new { typePacoteIds = typePacoteIds, unidadeId = unidadeId });

                //var paginatedItems = new PaginatedItemsViewModel<ColaboradorDto>(currentPage, itemsPerPage, countItems, results);

                return results;//.OrderBy(d => d.prevInicio);

            }
        }

        public async Task<IEnumerable<TurmaViewModel>> GetCursosUnidade()
        {
            string query = "select Turma.Descricao from Turma";
            //select * from turma where Turma.Descricao like '%Enfermagem%'

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var cursos = await connection.QueryAsync<TurmaViewModel>(query);

                return cursos.DistinctBy(c => c.descricao);

            }
        }

        public async Task<int> GetQuantidadeTurma(int unidadeId)
        {
            //var unidadeId = _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefault();
            var query = @"select Count(*) from turma where turma.unidadeId = @unidadeId";
            //select * from turma where Turma.Descricao like '%Enfermagem%'

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var qnt = await connection.QuerySingleAsync<int>(query, new { unidadeId = unidadeId });


                //var paginatedItems = new PaginatedItemsViewModel<ColaboradorDto>(currentPage, itemsPerPage, countItems, results);

                return qnt;//.OrderBy(d => d.prevInicio);

            }
        }
    }
}

/*
 * select * from colaborador 
where colaborador.Id IN
(
select professorId from professoresTurma where TurmaId = 59)
*/
