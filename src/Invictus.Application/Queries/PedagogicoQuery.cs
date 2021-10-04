using Invictus.Application.Dtos;
using Invictus.Application.Queries.Interfaces;
using System;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invictus.Application.Dtos.Pedagogico;

namespace Invictus.Application.Queries
{
    public class PedagogicoQuery : IPedagogicoQueries
    {
        private readonly IConfiguration _config;
        public PedagogicoQuery(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<AgendasProvasDto>> GetAgendasProvas(int turmaId)
        {
            var query = @"select * from ProvasAgenda where turmaId = @turmaId";
            /* select * from agendaprovas where turmaId = 1070*/
            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var materias = await connection.QueryAsync<AgendasProvasDto>(query, new { turmaId = turmaId });

                return materias;
            }
        }

        public async Task<IEnumerable<MateriaDto>> GetMaterias()
        {
            var query = @"SELECT * FROM Materias WHERE Materias.ModuloId = 1";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var materias = await connection.QueryAsync<MateriaDto>(query);

                //var paginatedItems = new PaginatedItemsViewModel<ColaboradorDto>(currentPage, itemsPerPage, countItems, results);

                return materias;

            }
        }

        public async Task<List<NotasViewModel>> GetNotaAlunos(int turmaId)
        {
            var query = @"select 
                        agendaProvas.id, 
                        agendaProvas.materia, 
                        agendaProvas.turmaId, 
                        agendaProvas.AvaliacaoUm as dataAv1, 
                        agendaProvas.Avaliacaodois as dataAv2, 
                        agendaProvas.AvaliacaoTres as dataAv3
                        from agendaProvas 
                        where agendaProvas.turmaid = @turmaId";

            var query2 = @"select 
                        aluno.nome, 
                        historicoaluno.alunoId,
                        historicoaluno.turmaId,
                        listaNotas.id as listaNotasId,
                        listaNotas.Materia,
                        listaNotas.NotaChamadaUm as av1,
                        listaNotas.NotaChamadaDois as av2,
                        listaNotas.NotaChamadaTres as av3,
                        listaNotas.HistoricoId
                        from HistoricoAluno 
                        inner join listaNotas on HistoricoAluno.Id = ListaNotas.HistoricoId
                        inner join Aluno on historicoaluno.id = aluno.id 
                        where HistoricoAluno.AlunoId in (
                        select alunoId from AlunosTurma where turmaId = @turmaId)";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                var notasViewModelDictionary = new Dictionary<int, NotasViewModel>();

                connection.Open();
                var result = await connection.QueryAsync<NotasViewModel>(query, new { turmaId = turmaId });
                //var result = connection.Query<NotasViewModel, AlunosENotas,
                //    NotasViewModel>(
                //    query,
                //    (notasViewModel, alunosENotas) =>
                //    {
                //        NotasViewModel notasEntry;

                //        if (!notasViewModelDictionary.TryGetValue(notasViewModel.id, out notasEntry))
                //        {
                //            notasEntry = notasViewModel;
                //            notasEntry.alunos = new List<AlunosENotas>();
                //            notasViewModelDictionary.Add(notasEntry.id, notasEntry);
                //        }

                //        notasEntry.alunos.Add(alunosENotas);

                //        return notasEntry;

                //    }, new { turmaId = turmaId }).Distinct().ToList();

                var listaNotas = await connection.QueryAsync<AlunosENotas>(query2, new { turmaId = turmaId });

                foreach (var lista in result)
                {
                    var disabledV1 = true;
                    var disabledV2 = true;
                    var disabledV3 = true;
                    var novaDataFormatoV1 = Convert.ToDateTime(lista.dataAv1);
                    var novaDataFormatoV2 = Convert.ToDateTime(lista.dataAv2);
                    var novaDataFormatoV3 = Convert.ToDateTime(lista.dataAv3);
                    var dataPesquisaNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    var dataPesquisaLimite = dataPesquisaNow.AddDays(5);

                    if (novaDataFormatoV1 > dataPesquisaNow && novaDataFormatoV1 <= dataPesquisaLimite)
                    {
                        disabledV1 = false;
                    }

                    if (novaDataFormatoV2 > dataPesquisaNow && novaDataFormatoV2 <= dataPesquisaLimite)
                    {
                        disabledV2 = false;
                    }

                    if (novaDataFormatoV3 > dataPesquisaNow && novaDataFormatoV3 <= dataPesquisaLimite)
                    {
                        disabledV3 = false;
                    }



                    //var disabled = lista.
                    var materia = lista.materia;
                    lista.alunos.AddRange(Mapping(materia, listaNotas.ToList(), disabledV1, disabledV2, disabledV3));
                }



                return result.ToList();

            }
        }

        private List<AlunosENotas> Mapping(string materia, List<AlunosENotas> listaNotas, bool disabledV1, bool disabledV2, bool disabledV3)
        {
            var lista = new List<AlunosENotas>();

            foreach (var item in listaNotas)
            {
                if (item.materia == materia)
                {
                    item.disabledv1 = disabledV1;
                    item.disabledv2 = disabledV2;
                    item.disabledv3 = disabledV3;
                    lista.Add(item);
                }
            }

            return lista;
        }


        public async Task<IEnumerable<TurmaViewModel>> GetTurmas(int unidadeId)
        {
            var query = @"SELECT id, identificador, descricao, statusDaTurma, moduloId  
                          FROM Turma WHERE Turma.UnidadeId = @unidadeId 
                          AND Turma.statusDaTurma <> 'Aguardando início' ";

            var query2 = @"select 
                            DiaAula, 
                            HoraInicial, 
                            HoraFinal
                            from calendarios where turmaId = @turmaId  
                            and DiaAula = @DataPesquisa
                            order by DiaAula asc";

            var DataPesquisa = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            //var DataPesquisa = DateTime.Now;

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<TurmaViewModel>(query, new { unidadeId = unidadeId });

                //var paginatedItems = new PaginatedItemsViewModel<ColaboradorDto>(currentPage, itemsPerPage, countItems, results);

                foreach (var item in results)
                {
                    var dateNow = DateTime.Now;

                    var result = await connection.QueryAsync<Result>(query2, new { turmaId = item.id, DataPesquisa = DataPesquisa });


                    var quantasAulasNoDia = result.Where(d => d.DiaAula == DataPesquisa).Count();

                    if (quantasAulasNoDia == 0)
                    {
                        item.podeIniciar = false;

                    }
                    else if (quantasAulasNoDia == 1)
                    {

                        var agora = DateTime.Now;
                        var timespantest = new TimeSpan(0, 10, 0);

                        var timespan1 = result.ToArray()[0].HoraInicial;
                        var timespan2 = result.ToArray()[0].HoraFinal;
                        var horaMinuto1 = timespan1.Split(":");
                        var horaMinuto2 = timespan2.Split(":");
                        var horaInicio = new DateTime(result.ToArray()[0].DiaAula.Year, result.ToArray()[0].DiaAula.Month, result.ToArray()[0].DiaAula.Day, Convert.ToInt32(horaMinuto1[0]), Convert.ToInt32(horaMinuto1[1]), 0).Subtract(timespantest);
                        ///var podeLiberar = horaInicio1.Subtract(timespantest);
                        var horaFim = new DateTime(result.ToArray()[0].DiaAula.Year, result.ToArray()[0].DiaAula.Month, result.ToArray()[0].DiaAula.Day, Convert.ToInt32(horaMinuto2[0]), Convert.ToInt32(horaMinuto2[1]), 0);

                        if (agora >= horaInicio && agora <= horaFim)
                        {
                            item.podeIniciar = true;
                        }
                        else
                        {
                            item.podeIniciar = false;
                        }


                    }
                    //else if (quantasAulasNoDia > 1)
                    //{

                    //}


                }

                return results;

            }
        }

        public async Task<TransfViewModel> GetDadosParaTransferencia(int alunoId)
        {
            string query = @"select Aluno.id, 
                            Aluno.nome, 
                            Aluno.cpf, 
                            Aluno.rg, 
                            Aluno.nascimento, 
                            Aluno.nomeSocial, 
                            Aluno.naturalidade, 
                            Aluno.uf, 
                            Aluno.email, 
                            Turma.id as TurmaId, 
                            Turma.identificador, 
                            Turma.descricao, 
                            Turma.unidade 
                            from Aluno 
                            inner join Turma on 
                            Turma.id = (select TurmaId 
                            from AlunosTurma where AlunoId = @alunoId) 
                            where aluno.id = @alunoId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<TransfViewModel>(query, new { alunoId = alunoId });

                //var paginatedItems = new PaginatedItemsViewModel<ColaboradorDto>(currentPage, itemsPerPage, countItems, results);

                //return results;

            }
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MateriaDto>> GetMateriasDoProfessor(int turmaId, int profId)
        {
            /*
            string query = @"select 
                            Materias.id,
                            Materias.Descricao
                            from materias 
                            where id in (
                            select materiaId from materiasDaTurma 
                            where ProfId = @profId 
                            )";
            */

            string query = @"select 
                            Materias.id,
                            Materias.Descricao
                            from materias 
                            where moduloId = 2";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<MateriaDto>(query, new { profId = profId });

                //var paginatedItems = new PaginatedItemsViewModel<ColaboradorDto>(currentPage, itemsPerPage, countItems, results);

                return results;

            }
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<NotasDisciplinasDto>> GetNotaByMateriaAndTurmaId(int materiaId, int turmaId)
        {
            var query = @"select aluno.nome, 
                        NotasDisciplinas.Id, 
                        NotasDisciplinas.AvaliacaoUm, 
                        NotasDisciplinas.SegundaChamadaAvaliacaoUm, 
                        NotasDisciplinas.AvaliacaoDois, 
                        NotasDisciplinas.SegundaChamadaAvaliacaoDois, 
                        NotasDisciplinas.AvaliacaoDois, 
                        NotasDisciplinas.SegundaChamadaAvaliacaoTres, 
                        NotasDisciplinas.MateriaId, 
                        NotasDisciplinas.MateriaDescricao, 
                        NotasDisciplinas.Resultado, 
                        NotasDisciplinas.AlunoId, 
                        NotasDisciplinas.TurmaId 
                        from NotasDisciplinas 
                        left join Aluno on NotasDisciplinas.AlunoId = aluno.Id
                        where NotasDisciplinas.TurmaId = @turmaId AND
                        NotasDisciplinas.MateriaId = @materiaId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                
                var results = await connection.QueryAsync<NotasDisciplinasDto>(query, new { materiaId = materiaId, turmaId = turmaId });

                return results;

            }
            //throw 

           
        }

        public async Task<ListaPresencaViewModel> GetInfoDiaPresencaLista(int materiaId, int turmaId,int  calendarioId)
        {
            var listaPresencaViewModel = new ListaPresencaViewModel();

            var queryOne = @"select 
                        Calendarios.id,
                        Calendarios.DiaAula,
                        Salas.Titulo, 
                        Materias.Descricao,
                        Colaborador.nome
                        from Calendarios 
                        inner join Salas on Calendarios.SalaId = Salas.Id
                        inner join Materias on Calendarios.MateriaId = Materias.id
                        inner join Colaborador on Calendarios.ProfessorId = Colaborador.Id
                        where Calendarios.id = @calendarioId ";

            var queryTwo = @"select
                            Aluno.Id as alunoId,
                            Aluno.Nome,
                            LivroPresencas.Id,
                            LivroPresencas.CalendarioId,
                            LivroPresencas.IsPresent
                            from LivroPresencas
                            right join Aluno on LivroPresencas.AlunoId = Aluno.Id
                            inner join Matriculados on Aluno.Id = Matriculados.AlunoId
                            where Matriculados.TurmaId = @turmaId";
            

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var infoDia = await connection.QuerySingleAsync<InfoDia>(queryOne, new { calendarioId = calendarioId });

                var listaPresenca = await connection.QueryAsync<ListaPresencaDto>(queryTwo, new { turmaId = turmaId });

                listaPresencaViewModel.infos = infoDia;
                listaPresencaViewModel.listaPresencas.AddRange(listaPresenca);

                foreach (var item in listaPresencaViewModel.listaPresencas)
                {
                    item.calendarioId = calendarioId;
                }
                //var listaPresenca = 

                return listaPresencaViewModel;

            }
        }

        public async Task<IEnumerable<AlunoDto>> GetDocsPendenciasLista()
        {
            var query = @"select 
Aluno.id,
Aluno.Nome,
Aluno.Cpf
from Aluno 
inner join Documento_Aluno on Aluno.id = Documento_Aluno.AlunoId
where Documento_Aluno.Analisado = 'false'";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var alunos = await connection.QueryAsync<AlunoDto>(query);



                return alunos.Distinct();

            }
        }
    }

    public class Result
    {
        public DateTime DiaAula { get; set; }
        public string HoraInicial { get; set; }
        public string HoraFinal { get; set; }
    }
}
