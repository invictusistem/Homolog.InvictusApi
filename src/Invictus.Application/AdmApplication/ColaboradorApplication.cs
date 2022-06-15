using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Invictus.Domain.Administrativo.ColaboradorAggregate.Interfaces;
using Invictus.Domain.Administrativo.FuncionarioAggregate;
using Invictus.Domain.Administrativo.FuncionarioAggregate.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class ColaboradorApplication : IColaboradorApplication
    {
        public UserManager<IdentityUser> UserManager { get; set; }
        private readonly IAspNetUser _aspNetUser;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IColaboradorRepository _colaboradorRepo;
        private readonly IColaboradorQueries _colabQueries;
        private readonly IMapper _mapper;
        private readonly IPessoaRepo _pessoaRepo;
        public ColaboradorApplication(IAspNetUser aspNetUser, IUnidadeQueries unidadeQueries, IMapper mapper, IColaboradorRepository colaboradorRepo,
            IColaboradorQueries colabQueries, UserManager<IdentityUser> userMgr, IPessoaRepo pessoaRepo)
        {
            _aspNetUser = aspNetUser;
            _unidadeQueries = unidadeQueries;
            _mapper = mapper;
            _colaboradorRepo = colaboradorRepo;
            _colabQueries = colabQueries;
            UserManager = userMgr;
            _pessoaRepo = pessoaRepo;
        }

        public async Task EditColaborador(PessoaDto editedColaborador)
        {
            //var newEmail = editedColaborador.email;

            //var oldEmail = await _colabQueries.GetEmailFromColaboradorById(editedColaborador.id);

            //var colaborador = _mapper.Map<ColaboradorDto, Colaborador>(editedColaborador);            

            //await _colaboradorRepo.EditColaborador(colaborador);

            //_colaboradorRepo.Commit();

            //if (newEmail != oldEmail)
            //{
            //    if (!String.IsNullOrEmpty(newEmail))
            //    {
            //        // update se existir
            //        var user = await UserManager.FindByEmailAsync(oldEmail);
            //        if (user != null)
            //        {
            //            await UserManager.UpdateAsync(user);

            //            var claims = await UserManager.GetClaimsAsync(user);

            //            var claim = claims.Where(c => c.Type == "IsActive").FirstOrDefault();

            //            await UserManager.RemoveClaimAsync(user, claim);

            //            await UserManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsActive", editedColaborador.ativo.ToString()));
            //        }

            //    }
            //    else
            //    {
            //        var user = await UserManager.FindByEmailAsync(oldEmail);
            //        if (user != null)
            //        {
            //            await UserManager.DeleteAsync(user);
            //        }
            //    }
            //}

            //_colaboradorRepo.Commit();
        }

        public async Task EditColaboradorV2(PessoaDto editedColaborador)
        {
            var newEmail = editedColaborador.email;

            var oldEmail = await _colabQueries.GetEmailFromColaboradorById(editedColaborador.id);

            var colaborador = _mapper.Map<Pessoa>(editedColaborador);

            await _pessoaRepo.EditPessoa(colaborador);

            _pessoaRepo.Commit();

            if (newEmail != oldEmail)
            {
                if (!String.IsNullOrEmpty(newEmail))
                {
                    // update se existir
                    var user = await UserManager.FindByEmailAsync(oldEmail);
                    if (user != null)
                    {
                        await UserManager.UpdateAsync(user);

                        var claims = await UserManager.GetClaimsAsync(user);

                        var claim = claims.Where(c => c.Type == "IsActive").FirstOrDefault();

                        await UserManager.RemoveClaimAsync(user, claim);

                        await UserManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsActive", editedColaborador.ativo.ToString()));
                    }

                }
                else
                {
                    var user = await UserManager.FindByEmailAsync(oldEmail);
                    if (user != null)
                    {
                        await UserManager.DeleteAsync(user);
                    }
                }
            }

            _colaboradorRepo.Commit();
        }

        public async Task SaveColaborador(PessoaDto newColaborador)
        {
            //var unidadeSigla = _aspNetUser.ObterUnidadeDoUsuario();

            //var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);

            //newColaborador.unidadeId = unidade.id;

            ////var colaborador = _mapper.Map<ColaboradorDto, Colaborador>(newColaborador);
            //var colaborador = _mapper.Map<Pessoa>(newColaborador);

            //colaborador.SetRespCadastroId(_aspNetUser.GetUnidadeIdDoUsuario());
            ////colaborador.SetColaborador();

            //colaborador.TratarEmail(colaborador.Email);

            ////await _colaboradorRepo.AddColaborador(colaborador);

            //await _pessoaRepo.AddPessoa(colaborador);

            //_pessoaRepo.Commit();

        }

        public async Task SaveColaboradorV2(PessoaDto newColaborador)
        {
            var unidadeSigla = _aspNetUser.ObterUnidadeDoUsuario();

            var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);

            newColaborador.unidadeId = unidade.id;

            var colaborador = _mapper.Map<Pessoa>(newColaborador);

            colaborador.SetRespCadastroId(_aspNetUser.GetUnidadeIdDoUsuario());

            colaborador.TratarEmail(colaborador.Email);

            colaborador.SetTipoPessoa(Invictus.Core.Enumerations.TipoPessoa.Colaborador);

            await _pessoaRepo.AddPessoa(colaborador);

            _pessoaRepo.Commit();
        }
    }
}
