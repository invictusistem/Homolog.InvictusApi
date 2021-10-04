using Invictus.Domain.Financeiro.Transacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Financeiro.Transacoes
{
    public class TransacaoBoletoEndereco
    {
        public TransacaoBoletoEndereco() { }
        public TransacaoBoletoEndereco(int id,
                                    string cep,
                                    string endereco,
                                    string numero,
                                    string bairro,
                                    string cidade,
                                    string estado
                                    )
        {
            Id = id;
            CEP = cep;
            Endereco = endereco;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;

        }
        public int Id { get; private set; }
        public string CEP { get; private set; }
        public string Endereco { get; private set; }
        public string Numero { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public int TransacaoBoletoId { get; private set; }
        public virtual TransacaoBoletoAggregate TransacaoBoleto { get; private set; }
    }
}
