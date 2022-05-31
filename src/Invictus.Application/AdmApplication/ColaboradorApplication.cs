using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Invictus.Domain.Administrativo.ColaboradorAggregate.Interfaces;
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
        public ColaboradorApplication(IAspNetUser aspNetUser, IUnidadeQueries unidadeQueries, IMapper mapper, IColaboradorRepository colaboradorRepo,
            IColaboradorQueries colabQueries, UserManager<IdentityUser> userMgr)
        {
            _aspNetUser = aspNetUser;
            _unidadeQueries = unidadeQueries;
            _mapper = mapper;
            _colaboradorRepo = colaboradorRepo;
            _colabQueries = colabQueries;
            UserManager = userMgr;
        }

        public async Task EditColaborador(ColaboradorDto editedColaborador)
        {
            var newEmail = editedColaborador.email;

            var oldEmail = await _colabQueries.GetEmailFromColaboradorById(editedColaborador.id);

            var colaborador = _mapper.Map<ColaboradorDto, Colaborador>(editedColaborador);

            //professor.SetDataEntrada(editedProfessor.dataEntrada);

            //professor.SetDataSaida(editedProfessor.dataSaida);

            await _colaboradorRepo.EditColaborador(colaborador);

            _colaboradorRepo.Commit();

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

        public async Task SaveColaborador(ColaboradorDto newColaborador)
        {
            var unidadeSigla = _aspNetUser.ObterUnidadeDoUsuario();

            var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);

            newColaborador.unidadeId = unidade.id;
            
            var colaborador = _mapper.Map<ColaboradorDto, Colaborador>(newColaborador);

            colaborador.SetColaborador();

            colaborador.TratarEmail(colaborador.Email);

            await _colaboradorRepo.AddColaborador(colaborador);

            _colaboradorRepo.Commit();
           
        }
    }
}
