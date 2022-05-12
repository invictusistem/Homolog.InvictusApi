using System;

namespace Invictus.Dtos.Financeiro.Configuracoes
{
    public class MeioPagamentoDto
    {
        public Guid id { get; set; }
        public string descricao { get; set; }
        public bool ativo { get; set; }
    }



}
