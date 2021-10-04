using Invictus.Application.Dtos;
using Invictus.Application.Queries.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using Invictus.Application.Dtos.Pedagogico;

namespace Invictus.Application.Queries
{
    public class EstagioQueries : IEstagioQueries
    {
        private readonly IConfiguration _config;
        public EstagioQueries(IConfiguration config)
        {
            _config = config;
        }
        public async Task<List<AlunoDocViewModel>> GetDocsAnalise(int unidade)
        {
            string query = @"select 
                            Aluno.Id,
                            Aluno.Nome,
                            Aluno.CPF,
                            Aluno.UnidadeCadastrada,
                            DocumentosEstagio.Id as docId,
                            DocumentosEstagio.Descricao,
                            DocumentosEstagio.Nome,
                            DocumentosEstagio.Validado,
                            DocumentosEstagio.TipoArquivo,
                            DocumentosEstagio.DataCriacao, 
                            DocumentosEstagio.EstagioMatriculaId,
                            DocumentosEstagio.AlunoId,
                            DocumentosEstagio.ContentArquivo,
                            DocumentosEstagio.DataFile, 
                            DocumentosEstagio.Analisado 
                            from Aluno
                            right join DocumentosEstagio on DocumentosEstagio.AlunoId = Aluno.Id
                            where Aluno.UnidadeCadastrada = @unidade ";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                var alunoDictionary = new Dictionary<int, AlunoDocViewModel>();
                connection.Open();

                var list = connection.Query<AlunoDocViewModel, DocumentDto,
                    AlunoDocViewModel>(
                    query,
                    (alunoDocViewModel, documentDto) =>
                    {
                        AlunoDocViewModel alunoEntry;

                        if (!alunoDictionary.TryGetValue(alunoDocViewModel.id, out alunoEntry))
                        {
                            alunoEntry = alunoDocViewModel;
                            alunoEntry.documentos = new List<DocumentDto>();
                            alunoDictionary.Add(alunoEntry.id, alunoEntry);
                        }

                        alunoEntry.documentos.Add(documentDto);

                        return alunoEntry;

                    }, new { unidade = unidade }, splitOn: "docId").Distinct().ToList();

                return list;

            }
        }

        public async Task<List<EstagioMatriculaViewModel>> GetEstagios()
        {
            //            var query = @"select
            //                 estagio.nome, 
            //                  estagio.dataInicio, 
            //        estagio.trimestre, 
            //      estagio.vagas, 
            //    estagio.cep, 
            //       estagio.logradouro, 
            //     estagio.complemento,
            //   estagio.cidade, 
            //  estagio.uf, 
            //   estagio.bairro,
            //               estagiomatricula.Id as estagioId, 
            //        estagiomatricula.AlunoId, 
            //       estagiomatricula.Nome, 
            //         estagiomatricula.Email,
            //        estagiomatricula.CPF
            //from Estagio
            //left join EstagioMatricula on Estagio.Id = EstagioMatricula.EstagioId";
            //

            var query = @"select
estagio.id, 
                 estagio.nome, 
                  estagio.dataInicio, 
        estagio.trimestre, 
      estagio.vagas, 
    estagio.cep, 
       estagio.logradouro, 
     estagio.complemento,
   estagio.cidade, 
  estagio.uf, 
   estagio.bairro 
from Estagio ";

            var query2 = @"select
                             
                           estagiomatricula.Id, 
                    estagiomatricula.AlunoId, 
                   estagiomatricula.Nome, 
                     estagiomatricula.Email,
                    estagiomatricula.CPF
            from EstagioMatricula where estagiomatricula.id = @estadioId";
            //left join EstagioMatricula on Estagio.Id = EstagioMatricula.EstagioId";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                
                connection.Open();

                var list = connection.Query<EstagioMatriculaViewModel>(query);

                foreach (var item in list)
                {
                    var estagioMat = connection.Query<EstagioMatriculaDto>(query2, new { estadioId = item.id });
                    if (estagioMat.Count() > 0) item.inscritos.AddRange(estagioMat);
                }

                return list.ToList();

            }
        }
    }
}
