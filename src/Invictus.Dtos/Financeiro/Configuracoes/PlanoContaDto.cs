using System;

namespace Invictus.Dtos.Financeiro.Configuracoes
{
    public class PlanoContaDto
    {
        public Guid id { get; set; }
        public string descricao { get; set; }
        public bool ativo { get; set; }
        public Guid unidadeId { get; set; }
        //public IEnumerable<SubConta> subcontas { get; set; }
    }



}
