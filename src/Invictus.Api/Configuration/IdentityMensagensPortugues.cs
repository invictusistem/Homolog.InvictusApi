using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Configuration
{
    public class IdentityMensagensPortugues : IdentityErrorDescriber
    {
        public override IdentityError DefaultError(){ return new IdentityError { Code = nameof(DefaultError), Description = $"Ocorreu um erro desconhecido." }; }

    }
}
