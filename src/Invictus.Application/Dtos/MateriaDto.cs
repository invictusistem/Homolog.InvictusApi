namespace Invictus.Application.Dtos
{
    public class MateriaDto
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public int qntAulas { get; set; }
        public string PrimeiroDiaAula { get; set; }
        public bool PrimeiroDaLista { get; set; }

        public int semestre { get; set; }
        public int cargaHoraria { get; set; }
        public int qntProvas { get; set; }
        public bool temRecuperacao { get; set; }
        public string modalidade { get; set; }
        public int moduloId { get; set; }
    }
}
