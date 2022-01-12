﻿using Dapper;
using Invictus.Core.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries
{
    public class UsuariosQueries : IUsuariosQueries
    {
        private readonly IConfiguration _config;
        private readonly IAspNetUser _aspNetUser;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IColaboradorQueries _colaboradorQueries;
        private UnidadeDto unidade;
        public UsuariosQueries(IConfiguration config, IAspNetUser aspNetUser, IUnidadeQueries unidadeQueries, IColaboradorQueries colaboradorQueries)
        {
            _config = config;
            _aspNetUser = aspNetUser;
            _unidadeQueries = unidadeQueries;
            _colaboradorQueries = colaboradorQueries;
            unidade = new UnidadeDto();
        }
        public async Task<PaginatedItemsViewModel<UsuarioDto>> GetUsuarios(int itemsPerPage, int currentPage, string paramsJson)
        {
            var param = JsonSerializer.Deserialize<ParametrosDTO>(paramsJson);

            var userUnidadeSigla = _aspNetUser.ObterUnidadeDoUsuario();

            unidade = await _unidadeQueries.GetUnidadeBySigla(userUnidadeSigla);

            var professores = await GeUsuariosByFilter(itemsPerPage, currentPage, param);

            var profCount = await CountUsuariosByFilter(itemsPerPage, currentPage, param);

            var paginatedItems = new PaginatedItemsViewModel<UsuarioDto>(currentPage, itemsPerPage, profCount, professores.ToList());

            return paginatedItems;
        }

        private async Task<IEnumerable<UsuarioDto>> GeUsuariosByFilter(int itemsPerPage, int currentPage, ParametrosDTO param)
        {

            StringBuilder query = new StringBuilder();
            query.Append(@"SELECT Colaboradores.id, Colaboradores.nome, Colaboradores.email, 
             Unidades.sigla as unidadeSigla,      
             AspNetRoles.Name as RoleName,             
             AspNetUserClaims.Id as ClamId, 
             AspNetUserClaims.ClaimType as clamType, 
             AspNetUserClaims.ClaimValue as clamKey 
             from Colaboradores 
             inner join Unidades on Colaboradores.UnidadeId = Unidades.Id
             inner join AspNetUsers on Colaboradores.Email = AspNetUsers.Email  
             inner join AspNetUserClaims on AspNetUsers.Id = AspNetUserClaims.UserId 
             inner join AspNetUserRoles on AspNetUsers.Id = AspNetUserRoles.UserId 
             inner join AspNetRoles on AspNetUserRoles.RoleId = AspNetRoles.Id ");

            if (param.nome != "") query.Append(" AND LOWER(Colaboradores.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.email != "") query.Append(" AND LOWER(Colaboradores.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.cpf != "") query.Append(" AND LOWER(Colaboradores.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            query.Append(" AND Colaboradores.UnidadeId = '" + unidade.id + "' ");
            //if (param.ativo == false) query.Append(" AND Professores.Ativo = 'True' ");
            query.Append(" ORDER BY Colaboradores.Nome ");
            query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var alunoDictionary = new Dictionary<Guid, UsuarioDto>();

                var usuarios = connection.Query<UsuarioDto, Claims, UsuarioDto>(query.ToString(),
                    (usuario, claim) =>
                    {
                        UsuarioDto usuarioentry;

                        if (!alunoDictionary.TryGetValue(usuario.id, out usuarioentry))
                        {
                            usuarioentry = usuario;
                            usuarioentry.claims = new List<Claims>();
                            alunoDictionary.Add(usuarioentry.id, usuarioentry);
                        }

                        return usuarioentry;

                    }, splitOn: "ClamId").Distinct().ToList();

                connection.Close();

                foreach (var item in usuarios)
                {
                    //var claim = item.claims.Where(c => c.clamType == "IsActive").Select(c => c.clamKey).FirstOrDefault();
                    item.ativo = await GetSesuarioEstaAtivo(item.id);
                }

                return usuarios;

            }
        }

        public async Task<bool> GetSesuarioEstaAtivo(Guid usuarioId)
        {
            var query = @"SELECT 
                         AspNetUserClaims.ClaimValue as ClamKey 
                         from Colaboradores 
                         inner join AspNetUsers on Colaboradores.Email = AspNetUsers.Email  
                         inner join AspNetUserClaims on AspNetUsers.Id = AspNetUserClaims.UserId 
                         inner join AspNetUserRoles on AspNetUsers.Id = AspNetUserRoles.UserId 
                         inner join AspNetRoles on AspNetUserRoles.RoleId = AspNetRoles.Id 
                        WHERE AspNetUserClaims.ClaimType = 'IsActive' 
                        AND Colaboradores.id = @usuarioId";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var ativo = await connection.QuerySingleAsync<bool>(query, new { usuarioId = usuarioId });

                connection.Close();

                return ativo;

            }

        }

        private async Task<int> CountUsuariosByFilter(int itemsPerPage, int currentPage, ParametrosDTO param)
        {
            StringBuilder query = new StringBuilder();
            query.Append(@"SELECT Count(DISTINCT Colaboradores.id)   
             from Colaboradores 
             inner join Unidades on Colaboradores.UnidadeId = Unidades.Id 
             inner join AspNetUsers on Colaboradores.Email = AspNetUsers.Email  
             inner join AspNetUserClaims on AspNetUsers.Id = AspNetUserClaims.UserId 
             inner join AspNetUserRoles on AspNetUsers.Id = AspNetUserRoles.UserId 
             inner join AspNetRoles on AspNetUserRoles.RoleId = AspNetRoles.Id ");

            if (param.nome != "") query.Append(" AND LOWER(Colaboradores.nome) like LOWER('%" + param.nome + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.email != "") query.Append(" AND LOWER(Colaboradores.email) like LOWER('%" + param.email + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            if (param.cpf != "") query.Append(" AND LOWER(Colaboradores.cpf) like LOWER('%" + param.cpf + "%') collate SQL_Latin1_General_CP1_CI_AI ");
            query.Append(" AND Colaboradores.UnidadeId = '" + unidade.id + "' ");
            //if (param.ativo == false) query.Append(" AND Professores.Ativo = 'True' ");
            //query.Append(" ORDER BY Colaboradores.Nome ");
            //query.Append(" OFFSET(" + currentPage + " - 1) * " + itemsPerPage + " ROWS FETCH NEXT " + itemsPerPage + " ROWS ONLY");


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var countItems = await connection.QuerySingleAsync<int>(query.ToString());

                connection.Close();

                return countItems;

            }
        }

        public async Task<UsuarioDto> GetUsuario(Guid colaboradorId)
        {
            StringBuilder query = new StringBuilder();
            query.Append(@"SELECT Colaboradores.id, Colaboradores.nome, Colaboradores.email, 
             AspNetUserClaims.Id as ClamId, 
             AspNetUserClaims.ClaimType as ClamType, 
             AspNetUserClaims.ClaimValue as ClamKey 
             from Colaboradores 
             inner join AspNetUsers on Colaboradores.Email = AspNetUsers.Email  
             inner join AspNetUserClaims on AspNetUsers.Id = AspNetUserClaims.UserId 
             inner join AspNetUserRoles on AspNetUsers.Id = AspNetUserRoles.UserId 
             inner join AspNetRoles on AspNetUserRoles.RoleId = AspNetRoles.Id 
             WHERE Colaboradores.id ='" + colaboradorId + "'");

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var alunoDictionary = new Dictionary<Guid, UsuarioDto>();

                var usuarios = connection.Query<UsuarioDto, Claims, UsuarioDto>(query.ToString(),
                    (usuario, claim) =>
                    {
                        UsuarioDto usuarioentry;

                        if (!alunoDictionary.TryGetValue(usuario.id, out usuarioentry))
                        {
                            usuarioentry = usuario;
                            usuarioentry.claims = new List<Claims>();
                            alunoDictionary.Add(usuarioentry.id, usuarioentry);
                        }

                        return usuarioentry;

                    }, splitOn: "ClamId").Distinct().ToList();

                connection.Close();

                return usuarios.FirstOrDefault();

            }
        }

        public async Task<CreateUsuarioView> GetCreateUsuarioViewModel(string email)
        {
            //var siglaUnidade = 
            //    EnvironmentVariableTarget unidade = 
            var query = @"SELECT  
                        colaboradores.id,
                        colaboradores.email,
                        colaboradores.nome,
                        ParametrosValue.[Value] as Cargo
                        from Colaboradores 
                        inner join ParametrosValue on Colaboradores.CargoId = ParametrosValue.Id
                        where colaboradores.email = @email ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QuerySingleAsync<CreateUsuarioView>(query, new { email = email });

                connection.Close();

                return result;

            }
        }

        public async Task<IEnumerable<string>> GetAllIdentityRoles()
        {
            var query = @"select AspNetRoles.Name FROM AspNetRoles ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<string>(query);

                connection.Close();

                return result;

            }
        }

        public async Task<IEnumerable<UsuarioAcessoViewModel>> GetUsuarioAcessoViewModel(Guid colaboradorId)
        {

            throw new NotImplementedException();
            //var colaborador = await _colaboradorQueries.GetColaboradoresById(colaboradorId);

            //var query = @"select 
            //            AspNetUserClaims.ClaimValue 
            //            from AspNetUserClaims 
            //            inner join AspNetUsers on
            //            AspNetUserClaims.UserId = AspNetUsers.Id 
            //            where AspNetUsers.Email = @colaboradorEmail
            //            and AspNetUserClaims.ClaimType = 'Unidade'";


            //await using (var connection = new SqlConnection(
            //         _config.GetConnectionString("InvictusConnection")))
            //{
            //    connection.Open();

            //    var result = await connection.QueryAsync<string>(query, new { colaboradorEmail = colaborador.email });

            //    connection.Close();

            //    var acessosView = new List<UsuarioAcessoViewModel>();

            //    //setar o default
            //    // colaborador.unidadeId == get unidade .Select SIGLA == 
            //    //return result;
            //    //var uni = await _unidadeQueries.GetUnidadeById(colaborador.unidadeId);

            //    var todasUnidades = await _unidadeQueries.GetUnidades();
            //    var unidadesSemDefault = todasUnidades.Where(u => u.id != colaborador.unidadeId);

            //    foreach (var item in todasUnidades)
            //    {
            //        acessosView.Add(new UsuarioAcessoViewModel()
            //        {
            //            id = colaborador.id,
            //            descricao = item.descricao,
            //            sigla = item.sigla,
            //            unidadeId = item.id,
            //            acesso = false,
            //            podeAlterar = true

            //        });
            //    }
            //    //set unidadeDefault
            //    foreach (var item in acessosView.Where(a => a.unidadeId == colaborador.unidadeId))
            //    {
            //        item.acesso = true;
            //        item.podeAlterar = false;
            //    }

            //    if(acessosView.Count() > 1)
            //    {
            //        // setUni com acessos
            //        foreach (var item in acessosView.Where(a => a.podeAlterar == true))
            //        {

            //            foreach (var uni in unidadesSemDefault)
            //            {


            //            }





            //        }

            //    }



            //    var uniClaimSemDefault = result.Where(c => c != uni.sigla);
            //foreach (var unid in unidadesSemDefault)
            //{

            //    var temAcesso = unidadesSemDefault.Where(u => u.sigla)
            //    var outrasUnidComAcesso = todasUnidades.Where(u => u.id != colaboradorId)

            //}

        }
    }
}

