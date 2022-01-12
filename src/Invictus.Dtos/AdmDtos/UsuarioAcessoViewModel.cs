

using System;

namespace Invictus.Dtos.AdmDtos
{
    public class UsuarioAcessoViewModel
    {
        public Guid id { get; set; }
        public string descricao { get; set; }
        public string sigla { get; set; }
        public Guid unidadeId { get; set; }
        public bool acesso { get; set; }
        public bool podeAlterar { get; set; }


    }
}
