using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Invictus.Api.Controllers.Comercial
{
    [ApiController]
    [Authorize]
    [Route("api/comercial")]
    public class ComercialController : ControllerBase
    {
       
        public ComercialController()
        {            

        }       
    }
}