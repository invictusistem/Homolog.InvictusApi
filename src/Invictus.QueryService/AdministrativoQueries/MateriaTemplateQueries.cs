using Dapper;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries
{
    public class MateriaTemplateQueries : IMateriaTemplateQueries
    {
        private readonly IConfiguration _config;
        public MateriaTemplateQueries(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<MateriaTemplateDto>> GetMateriaByTypePacoteId(Guid typePacoteId)
        {
            var query = @"SELECT * FROM MateriasTemplate WHERE MateriasTemplate.typepacoteid = @typePacoteId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var resultado = await connection.QueryAsync<MateriaTemplateDto>(query, new { typePacoteId = typePacoteId });

                connection.Close();

                return resultado;
            }
        }

        public async Task<PaginatedItemsViewModel<MateriaTemplateDto>> GetMateriasTemplateList(int itemsPerPage, int currentPage)
        {
            // var param = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);

            var materias = await GetMaterias(itemsPerPage, currentPage);

            var materiasCount = await CountGetMaterias();

            var paginatedItems = new PaginatedItemsViewModel<MateriaTemplateDto>(currentPage, itemsPerPage, materiasCount, materias.ToList());

            return paginatedItems;

        }

        private async Task<int> CountGetMaterias()
        {     
            var query = @"SELECT Count(*) FROM MateriasTemplate";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var count = await connection.QuerySingleAsync<int>(query);

                connection.Close();

                return count;
            }
        }

        private async Task<IEnumerable<MateriaTemplateDto>> GetMaterias(int itemsPerPage, int currentPage)
        {

            //StringBuilder query = new StringBuilder();
            //query.Append("SELECT * from Professores where ");

            //if (param.nome != "") query.Append("LOWER(Professores.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            //if (param.email != "") query.Append("LOWER(Professores.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            //if (param.cpf != "") query.Append("LOWER(Professores.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI AND ");
            //query.Append("Professores.UnidadeId = '" + unidade.id + "'");
            //if (param.ativo == false) query.Append(" AND Professores.Ativo = 'True' ");
            //query.Append(" ORDER BY Professores.Nome ");
            //query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");


            var query = @"SELECT * FROM MateriasTemplate ORDER BY MateriasTemplate.Nome  
            OFFSET(@currentPage - 1) * @itemsPerPage  ROWS FETCH NEXT @itemsPerPage ROWS ONLY";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var resultado = await connection.QueryAsync<MateriaTemplateDto>(query, new { itemsPerPage = itemsPerPage, currentPage = currentPage });

                connection.Close();

                return resultado.OrderBy(r => r.nome);
            }
        }

        public async Task<MateriaTemplateDto> GetMateriaTemplate(Guid materiaId)
        {
            var query = @"SELECT * FROM MateriasTemplate WHERE MateriasTemplate.id = @id";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var resultado = await connection.QuerySingleAsync<MateriaTemplateDto>(query, new { id = materiaId });

                connection.Close();

                return resultado;
            }
        }

        public async Task<IEnumerable<MateriaTemplateDto>> GetMateriasByListIds(List<Guid> listGuidMaterias)
        {
            var query = @"SELECT * FROM MateriasTemplate WHERE MateriasTemplate.id in (@listGuidMaterias)";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var resultado = await connection.QueryAsync<MateriaTemplateDto>(query, new { listGuidMaterias = listGuidMaterias });

                connection.Close();

                return resultado.OrderBy(r => r.nome);
            }
        }

        public async Task<IEnumerable<MateriaTemplateDto>> GetAllMaterias()
        {
            var query = @"SELECT 
                        MateriasTemplate.id,
                        MateriasTemplate.nome,
                        MateriasTemplate.descricao,
                        MateriasTemplate.modalidade,
                        MateriasTemplate.CargaHoraria,
                        MateriasTemplate.Ativo,
                        MateriasTemplate.TypePacoteId,
                        TypePacote.nome as typePacoteNome
                        FROM MateriasTemplate 
                        INNER JOIN TypePacote on MateriasTemplate.TypePacoteId = TypePacote.Id
                        ORDER BY MateriasTemplate.Nome";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var resultado = await connection.QueryAsync<MateriaTemplateDto>(query);

                connection.Close();

                return resultado.OrderBy(r => r.nome);
            }
        }

        public async Task<IEnumerable<MateriaTemplateDto>> GetMateriasByTypePacoteLiberadoParaOProfessor(Guid typePacoteId, Guid professorId)
        {
            var query = @"SELECT * FROM MateriasTemplate 
                        WHERE MateriasTemplate.typepacoteid = @typePacoteId 
                        AND MateriasTemplate.Id NOT IN (
                        SELECT ProfessoresMaterias.PacoteMateriaId FROM ProfessoresMaterias 
                        WHERE ProfessoresMaterias.ProfessorId = @professorId
                        )";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var resultado = await connection.QueryAsync<MateriaTemplateDto>(query, new { typePacoteId = typePacoteId, professorId = professorId });

                connection.Close();

                return resultado;
            }
        }
    }
}
