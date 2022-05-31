using Dapper;
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
    public class ContratoQueries : IContratoQueries
    {
        private readonly IConfiguration _config;
        public ContratoQueries(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int> CountContratos()
        {
            string query = @"SELECT Count(*) FROM Contratos";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var count = await connection.QuerySingleAsync<int>(query);

                connection.Close();

                return count;
            }
        }

        public async Task<ContratoDto> GetContratoById(Guid contratoId)
        {
            var query = @"select * from Contratos Where Contratos.Id = @contratoId ";

            var queryConteudo = @"select * from ConteudoContratos Where ConteudoContratos.ContratoId = @contratoId ";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                var contrato = await connection.QuerySingleAsync<ContratoDto>(query, new { contratoId = contratoId });

                var contratoConteudo = await connection.QueryAsync<ContratoConteudoDto>(queryConteudo, new { contratoId = contratoId });

                foreach (var item in contratoConteudo.OrderBy(c => c.order))
                {
                    contrato.conteudo += item.content;
                }

                connection.Close();

                return contrato;

            }
        }

        public async Task<IEnumerable<ContratoDto>> GetContratoByTypePacote(Guid typePacoteId, bool ativo)
        {
            //var query = "SELECT * FROM Contratos WHERE Contratos.typePacoteId = @typePacoteId AND Contratos.ativo = 'True'";

            StringBuilder query = new StringBuilder();
            query.Append(@"SELECT * FROM Contratos WHERE Contratos.typePacoteId = @typePacoteId ");

            if (ativo == true) query.Append(" AND Contratos.ativo = 'True'");


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var resultado = await connection.QueryAsync<ContratoDto>(query.ToString(), new { typePacoteId = typePacoteId });

                connection.Close();

                return resultado;
            }
        }

        public async Task<ContratoDto> GetContratoCompletoById(Guid contratoId)
        {
            //var unidadeId = GetUnidadeId();
            string query = @"select * from Contratos inner join ConteudoContratos on Contratos.id = ConteudoContratos.ContratoId 
                             WHERE Contratos.Id = @contratoId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var alunoDictionary = new Dictionary<Guid, ContratoDto>();

                var contrato = connection.Query<ContratoDto, ContratoConteudoDto, ContratoDto>(query,
                    (contratoDto, conteudoDto) =>
                    {
                        ContratoDto contratoEntry;

                        if (!alunoDictionary.TryGetValue(contratoDto.id, out contratoEntry))
                        {
                            contratoEntry = contratoDto;
                            contratoEntry.conteudos = new List<ContratoConteudoDto>();
                            alunoDictionary.Add(contratoEntry.id, contratoEntry);
                        }

                        if (conteudoDto != null)
                        {
                            contratoEntry.conteudos.Add(conteudoDto);
                        }
                        return contratoEntry;

                    }, new { contratoId = contratoId }, splitOn: "Id").Distinct().ToList();

                connection.Close();

                return contrato.FirstOrDefault();
            }
        }

        public async Task<ContratoDto> GetContratoCompletoByTypeId(Guid typePacoteId)
        {
            string query = @"select * from Contratos inner join ConteudoContratos on Contratos.id = ConteudoContratos.ContratoId 
                             WHERE Contratos.typePacoteId = @typePacoteId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var alunoDictionary = new Dictionary<Guid, ContratoDto>();

                var contrato = connection.Query<ContratoDto, ContratoConteudoDto, ContratoDto>(query,
                    (contratoDto, conteudoDto) =>
                    {
                        ContratoDto contratoEntry;

                        if (!alunoDictionary.TryGetValue(contratoDto.id, out contratoEntry))
                        {
                            contratoEntry = contratoDto;
                            contratoEntry.conteudos = new List<ContratoConteudoDto>();
                            alunoDictionary.Add(contratoEntry.id, contratoEntry);
                        }

                        if (conteudoDto != null)
                        {
                            contratoEntry.conteudos.Add(conteudoDto);
                        }
                        return contratoEntry;

                    }, new { typePacoteId = typePacoteId }, splitOn: "Id").Distinct().ToList();

                connection.Close();

                return contrato.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<ContratoDto>> GetContratosViewModel()
        {
            var query = @"select 
                            Contratos.Id,
                            Contratos.CodigoContrato, 
                            Contratos.Titulo, 
                            Contratos.PodeEditar,
                            Contratos.DataCriacao,
                            TypePacote.Nome as pacoteNome 
                            from Contratos 
                            inner join TypePacote on Contratos.PacoteId = TypePacote.Id ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var contratos = await connection.QueryAsync<ContratoDto>(query);

                connection.Close();

                return contratos;

            }
        }
    }
}
