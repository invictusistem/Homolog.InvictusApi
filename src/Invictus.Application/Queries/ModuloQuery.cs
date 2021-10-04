using Invictus.Application.Dtos;
using Invictus.Application.Queries.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;

namespace Invictus.Application.Queries
{
    public class ModuloQuery : IModuloQueries
    {
        private readonly IConfiguration _config;
        public ModuloQuery(IConfiguration config)
        {
            _config = config;
        }


        public async Task<IEnumerable<MateriaDto>> GetMateriasModulos(int moduloId)
        {
            var query = "SELECT * FROM Materias WHERE Materias.ModuloId = @moduloId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<MateriaDto>(query, new { moduloId = moduloId });

                //var paginatedItems = new PaginatedItemsViewModel<ColaboradorDto>(currentPage, itemsPerPage, countItems, results);

                return results;

            }

        }

        public async Task<IEnumerable<ModuloDto>> GetModulos(string moduloCode)
        {
            var query = @"select * from modulos 
                        where unidadeId = 
                        (select id from unidade where Sigla = @moduloCode)";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<ModuloDto>(query, new { moduloCode = moduloCode });

                //var paginatedItems = new PaginatedItemsViewModel<ColaboradorDto>(currentPage, itemsPerPage, countItems, results);

                return results;

            }
        }

        public async Task<IEnumerable<ModuloDto>> GetModulosCreateTurma(int unidadeId)
        {
            var query = @"select 
                        Modulos.id, 
                        Modulos.DuracaoMeses, 
                        Modulos.Preco, 
                        Modulos.TotalHoras, 
                        Modulos.TypePacoteId, 
                        TypePacote.Nome as Descricao 
                        from modulos 
                        inner join TypePacote 
                        on Modulos.TypePacoteId = TypePacote.Id 
                        where unidadeId = @unidadeId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var results = await connection.QueryAsync<ModuloDto>(query, new { unidadeId = unidadeId });

                //var paginatedItems = new PaginatedItemsViewModel<ColaboradorDto>(currentPage, itemsPerPage, countItems, results);

                return results;

            }
        }
    }
}
