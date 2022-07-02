using Dapper;
using Invictus.Core.Interfaces;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Invictus.QueryService.PedagogicoQueries
{
    public class PedagReqQueries : IPedagReqQueries
    {
        private readonly IConfiguration _config;
        private readonly IAspNetUser _aspNetUser;
        public PedagReqQueries(IConfiguration config,IAspNetUser aspNetUser)
        {
            _config = config;
            _aspNetUser = aspNetUser;
        }
        public async Task<IEnumerable<CategoriaDto>> GetAllCategorias()
        {
            var query = @"SELECT * FROM ReqCategorias";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result  = await connection.QueryAsync<CategoriaDto>(query);

                return result;

            }
            
        }

        public async Task<CategoriaDto> GetCategoriaById(Guid id)
        {
            var query = @"SELECT * FROM ReqCategorias WHERE ReqCategorias.id = @id";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QuerySingleAsync<CategoriaDto>(query, new { id } );

                return result;

            }
        }

        public async Task<TipoDto> GetTipoById(Guid tipoId)
        {
            var query = @"SELECT * FROM ReqTipos WHERE ReqTipos.id = @tipoId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QuerySingleAsync<TipoDto>(query, new { tipoId });

                return result;

            }
        }

        public async Task<IEnumerable<TipoDto>> GetTiposByCategoriaId(Guid categoriaId)
        {
            var query = @"SELECT * FROM ReqTipos WHERE ReqTipos.categoriaId = @categoriaId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<TipoDto>(query, new { categoriaId });

                return result;

            }
        }
    }
}
