using System;

namespace Invictus.Dtos.Financeiro.Configuracoes
{
    public class CentroCustoDto
    {
        public Guid id { get; set; }
        public string descricao { get; set; }
        public bool ativo { get; set; }
        public bool alertaMediaGastos { get; set; }
        public Guid unidadeId { get; set; }
    }



}
