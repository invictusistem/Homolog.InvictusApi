using Invictus.Core;

namespace Invictus.Domain.Financeiro
{
    public class Reparcelado : Entity
    {
        public Reparcelado(string listBoletoReparceladoId,
                           string listNewBoletoId)
        {
            ListBoletoReparceladoId = listBoletoReparceladoId;
            ListNewBoletoId = listNewBoletoId;

        }

        public string ListBoletoReparceladoId { get; private set; }
        public string ListNewBoletoId { get; private set; }
    }
}
