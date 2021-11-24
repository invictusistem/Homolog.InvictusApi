using Dapper;
using Invictus.Core.Interfaces;
using Invictus.Dtos.AdmDtos;
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
    public class UnidadeQueries : IUnidadeQueries
    {
        private readonly IConfiguration _config;
        private readonly IAspNetUser _aspNetUser;
        public UnidadeQueries(IConfiguration config, IAspNetUser aspNetUser)
        {
            _config = config;
            _aspNetUser = aspNetUser;
        }


        public async Task<UnidadeDto> GetUnidadeDoUsuario()
        {
            var unidadeSigle = _aspNetUser.ObterUnidadeDoUsuario();
            var unidade = await GetUnidadeBySigla(unidadeSigle);

            return unidade;
        }
        public async Task<SalaDto> GetSala(Guid salaId)
        {
            string query = @"SELECT * FROM UnidadesSalas WHERE UnidadesSalas.Id = @salaId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var sala = await connection.QuerySingleAsync<SalaDto>(query, new { salaId = salaId });

                connection.Close();

                return sala;
            }
        }

        public async Task<IEnumerable<SalaDto>> GetSalasByUserUnidade()
        {

            var siglaUnidade = _aspNetUser.ObterUnidadeDoUsuario();
            var unidade = await GetUnidadeBySigla(siglaUnidade);
            var salas = await GetSalas(unidade.id);

            return salas;
        }

        public async Task<IEnumerable<SalaDto>> GetSalas(Guid unidadeId)
        {
            string query = @"SELECT * FROM UnidadesSalas WHERE UnidadesSalas.unidadeId = @unidadeId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var salas = await connection.QueryAsync<SalaDto>(query, new { unidadeId = unidadeId });

                connection.Close();

                return salas;
            }
        }

        public async Task<UnidadeDto> GetUnidadeById(Guid id)
        {
            var query = "SELECT * FROM Unidades WHERE Unidades.id = @id";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var resultado = await connection.QuerySingleAsync<UnidadeDto>(query, new { id = id });

                connection.Close();

                return resultado;
            }
        }

        public async Task<UnidadeDto> GetUnidadeBySigla(string sigla)
        {
            var query = @"select * from Unidades Where Unidades.Sigla = @sigla";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var unidade = await connection.QuerySingleAsync<UnidadeDto>(query, new { sigla = sigla });

                connection.Close();

                return unidade;

            }
        }

        public async Task<int> CountSalaUnidade(Guid unidadeId)
        {
            string query = @"SELECT Count(*) FROM UnidadesSalas WHERE UnidadesSalas.unidadeId = @unidadeId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var count = await connection.QuerySingleAsync<int>(query, new { unidadeId = unidadeId });

                connection.Close();

                return count;
            }
        }

        public async Task<IEnumerable<UnidadeDto>> GetUnidades()
        {
            StringBuilder query = new StringBuilder();

            query.Append(@"select * from Unidades left join UnidadesSalas on Unidades.id = UnidadesSalas.UnidadeId ");

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var alunoDictionary = new Dictionary<Guid, UnidadeDto>();

                var unidades = connection.Query<UnidadeDto, SalaDto, UnidadeDto>(query.ToString(),
                    (unidadeDto, salaDto) =>
                    {
                        UnidadeDto unidadeEntry;

                        if (!alunoDictionary.TryGetValue(unidadeDto.id, out unidadeEntry))
                        {
                            unidadeEntry = unidadeDto;
                            unidadeEntry.salas = new List<SalaDto>();
                            alunoDictionary.Add(unidadeEntry.id, unidadeEntry);
                        }

                        if (salaDto != null)
                        {
                            unidadeEntry.salas.Add(salaDto);
                        }
                        return unidadeEntry;

                    }, splitOn: "Id").Distinct().ToList();

                connection.Close();

                return unidades;
            }
        }

        public async Task<IEnumerable<UnidadeDto>> GetUnidadesDonatarias(string unidadeSigla)
        {
            var query = @"select * from Unidades WHERE Unidades.sigla <> @unidadeSigla";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var unidades = await connection.QueryAsync<UnidadeDto>(query, new { unidadeSigla = unidadeSigla });

                connection.Close();

                return unidades;
            }
        }

    }
}
