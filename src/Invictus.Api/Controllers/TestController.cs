using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/teste")]
    public class TestController : ControllerBase
    {       
        public UserManager<IdentityUser> UserManager { get; set; }
        public RoleManager<IdentityRole> RoleManager { get; set; }
        public TestController(
            UserManager<IdentityUser> userMgr,
                                RoleManager<IdentityRole> roleMgr)
        {            
            UserManager = userMgr;
            RoleManager = roleMgr;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetInfo()
        //{
        //    //var obj = new { paramOne = "um", unidadeSigla = "dois" };
        //    //var obj2 = new { obj.paramOne, obj.unidadeSigla };
        //    ////
        //    //var result = await _admQueries.GenericSearch<ColaboradorDto>("Colaborador", "Nome", "João");

        //    //return Ok(new { resultado = result });
        //    return Ok();
        //}

        //[HttpGet]
        //[Route("addrole")]
        //public async Task<ActionResult> AddRole()
        //{
        //    //var email = "invictus@teste.com";
        //    var role = "Aluno";
        //    var role1 = "Administrador";
        //    var role2 = "Professor";
        //    var role3 = "SuperAdm";
        //    var role4 = "MasterAdm";
        //    //if (ModelState.IsValid)
        //    //{
        //    IdentityResult result = IdentityResult.Success;
        //    //if (result.Process(ModelState))
        //    //{
        //    //IdentityUser user = await UserManager.FindByEmailAsync(email);

        //    await RoleManager.CreateAsync(new IdentityRole(role));
        //    await RoleManager.CreateAsync(new IdentityRole(role1));
        //    //var role2 = "Professor";
        //    await RoleManager.CreateAsync(new IdentityRole(role2));
        //    //var role3 = "MasterAdm";
        //    await RoleManager.CreateAsync(new IdentityRole(role3));
        //    await RoleManager.CreateAsync(new IdentityRole(role4));
        //    //if (!await UserManager.IsInRoleAsync(user, role))
        //    //{
        //    //    result = await UserManager.AddToRoleAsync(user, role);
        //    //}

        //    //}
        //    //}

        //    return Ok();
        //}
    }
}
