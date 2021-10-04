using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Services.Interface
{
    public interface IBoletoService
    {
        List<BoletoLoteResponse> EnviaRequisicaoLote(List<BoletoLoteDto> boletosLote);
    }
}
