namespace Invictus.Domain.Financeiro.VendaProduto
{
    public class ProdutoVenda 
    {
        public ProdutoVenda() { }
        public ProdutoVenda(int id,
                            int produtoId,
                            int quantidade,
                            decimal valorUnitario,
                            decimal valorTotal)
        {
            Id = id;
            ProdutoId = produtoId;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            ValorTotal = valorTotal;
            //DataVenda = dataVenda;
            //CNPJ_Comprador = cnpj_Comprador;
            //ValorTotalVenda = valorTotalVenda;
            //UnidadeId = unidadeId;
            //MeioDePagamento = meioDePagamento.DisplayName;

        }


        public int Id { get; private set; }
        public int ProdutoId { get; private set; }
        public int Quantidade { get; private set; }

        //public DateTime DataVenda { get; set; }
        public decimal ValorUnitario { get; private set; }
        public decimal ValorTotal { get; private set; }
        public int VendaProdutoId { get; private set; }
        public virtual VendaProdutoAggregate VendaProdutoAggregate { get; private set; }
        //public int UnidadeId { get; set; }
        //public string MeioDePagamento { get; set; }



    }
}
