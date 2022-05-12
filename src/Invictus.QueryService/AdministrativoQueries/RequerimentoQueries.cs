using Dapper;
using Invictus.Core.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries
{
    public class RequerimentoQueries : IRequerimentoQueries
    {
        private readonly IConfiguration _config;
        private readonly IAspNetUser _aspNetUser;
        private readonly IUnidadeQueries _unidadeQueries;
        public RequerimentoQueries(IConfiguration config, IAspNetUser aspNetUser, IUnidadeQueries unidadeQueries)
        {   
            _config = config;
            _aspNetUser = aspNetUser;
            _unidadeQueries = unidadeQueries;
        }

        public async Task<IEnumerable<RequerimentoDto>> GetRequerimentosByMatriculaId(Guid matriculaId)
        {
            var query = @"SELECT * FROM Requerimentos WHERE Requerimentos.matriculaRequerenteId = @matriculaId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<RequerimentoDto>(query, new { matriculaId = matriculaId });

                connection.Close();

                return results;
            }
        }

        public async Task<IEnumerable<RequerimentoDto>> GetRequerimentosByUnidadeId()
        {
            var unidadeSigla = _aspNetUser.ObterUnidadeDoUsuario();

            var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);

            var query = @"SELECT * FROM Requerimentos WHERE Requerimentos.unidadeId = @unidadeId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<RequerimentoDto>(query, new { unidadeId = unidade.id });

                connection.Close();

                return results;
            }
        }

        public async Task<TipoRequerimentoDto> GetTipoRequerimentoById(Guid tipoId)
        {
            var query = @"SELECT * FROM TypeRequerimento WHERE TypeRequerimento.id = @tipoId ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QuerySingleAsync<TipoRequerimentoDto>(query, new { tipoId = tipoId });

                connection.Close();

                return results;
            }
        }

        public async Task<IEnumerable<TipoRequerimentoDto>> GetTiposRequerimentos()
        {
            var query = @"SELECT * FROM TypeRequerimento";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var results = await connection.QueryAsync<TipoRequerimentoDto>(query);

                connection.Close();

                return results;
            }
        }
    }
}