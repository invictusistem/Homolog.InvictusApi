
namespace Invictus.Application.Dtos.Administrativo
{
    public class PlanoPagamentoDto
    {

        public int id { get; set; }
        public int pacoteId { get; set; } //Criar SERÁ O TIPO
        public string descricao { get; set; }
        public decimal valor { get; set; }
        public decimal taxaMatricula { get; set; }
        public string parcelamento { get; set; }
        public bool materialGratuito { get; set; }
        public decimal bonusMensalidade { get; set; }
        public int contratoId { get; set; }

    }
}
