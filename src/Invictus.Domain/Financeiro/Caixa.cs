using Invictus.Core;
using System;

namespace Invictus.Domain.Financeiro
{
    public class Caixa : Entity
    {
        public Caixa(Guid usuarioId,
                    Guid unidadeId,
                    DateTime horarioTransacao,
                    Guid boletoId
                    )
        {
            UsuarioId = usuarioId;
            UnidadeId = unidadeId;
            HorarioTransacao = horarioTransacao;
            BoletoId = boletoId;
        }

        public Guid UsuarioId { get; set; }
        public Guid UnidadeId { get; set; }
        public DateTime HorarioTransacao {get;set;}
        public Guid BoletoId { get; set; }
    }
}
