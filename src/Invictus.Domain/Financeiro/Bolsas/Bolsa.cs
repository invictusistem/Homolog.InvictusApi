using Invictus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Financeiro.Bolsas
{
    public class Bolsa : Entity
    {
        public Bolsa(string nome,
                    int percentualDesconto,
                    string senha,
                    Guid colaborador,
                    Guid unidadeId,
                    Guid typePacoteId,
                    DateTime dataCriacao,
                    DateTime dataExpiracao
                    )
        {
            Nome = nome;
            PercentualDesconto = percentualDesconto;
            Senha = senha;
            Colaborador = colaborador;
            UnidadeId = unidadeId;
            TypePacoteId = typePacoteId;
            DataCriacao = dataCriacao;
            DataExpiracao = dataExpiracao;

        }
        public string Nome { get; private set; }
        public int PercentualDesconto { get; private set; }
        public string Senha { get; private set; }
        public Guid Colaborador { get; private set; }
        public Guid UnidadeId { get; private set; }
        public Guid TypePacoteId { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime DataExpiracao { get; private set; }

        public void SetDataCriacao(DateTime dia)
        {
            DataCriacao = dia;
        }

        public void SetUnidadeId(Guid unidadeId)
        {
            UnidadeId = unidadeId;
        }

        public void SetColaboradorId(Guid colaboradorId)
        {
            Colaborador = colaboradorId;
        }

        public void SetSenha(string senha)
        {
            Senha = senha;
        }
    }
}
