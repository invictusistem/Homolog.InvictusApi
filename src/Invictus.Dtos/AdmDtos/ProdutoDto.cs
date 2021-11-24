using System;

namespace Invictus.Dtos.AdmDtos
{
    public class ProdutoDto
    {
        public Guid id { get; set; }
        public string codigoProduto { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public decimal preco { get; set; }
        public decimal precoCusto { get; set; }
        public int quantidade { get; set; }
        public int nivelMinimo { get; set; }
        public Guid unidadeId { get; set; }
        public DateTime dataCadastro { get; set; }
        public string observacoes { get; set; }
    }    
}
