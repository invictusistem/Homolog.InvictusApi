using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos
{
    public class Testando
    {
        public Testando(IFormFile cpf,
                        IFormFile compRes)
        {
            Cpf = cpf;
            CompRes = compRes;
        }
        public IFormFile Cpf { get; set; }
        public IFormFile CompRes { get; set; }
    }
}
