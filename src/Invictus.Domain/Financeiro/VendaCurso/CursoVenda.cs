namespace Invictus.Domain.Financeiro.VendaCurso
{
    public class CursoVenda
    {
        public CursoVenda() { }
        public CursoVenda(int id,
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
        public int ProdutoId { get; private set; } // TurmaId
        public int Quantidade { get; private set; }// REMOVE

        //public DateTime DataVenda { get; set; }
        public decimal ValorUnitario { get; private set; } // remove
        public decimal ValorTotal { get; private set; } //
        public int VendaProdutoId { get; private set; } // FK
        public virtual VendaCursoAggregate VendaCursoAggregate { get; private set; }
        //public int UnidadeId { get; set; }
        //public string MeioDePagamento { get; set; }



    }
}
