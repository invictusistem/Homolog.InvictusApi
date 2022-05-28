using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [Route("api/typepacote")]
    [Authorize]
    [ApiController]
    public class TypePacoteController : ControllerBase
    {
        private readonly ITypePacoteQueries _typeQueries;
       
        public TypePacoteController(ITypePacoteQueries typeQueries)
        {
            _typeQueries = typeQueries;
        }

        [HttpGet]       
        public async Task<IActionResult> GetTypePacotes()
        {
            var typePacotes = await _typeQueries.GetTypePacotes();           
            
            if (typePacotes.Count() == 0) return NotFound();
            
            return Ok(new { typePacotes = typePacotes });
        }
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
}
