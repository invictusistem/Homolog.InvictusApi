namespace Invictus.Application.Dtos
{
    public class ProfessoresMateriaDto
    {
        public int id { get; set; }
        public int materiaId { get; set; }
        public string descricao { get; set; }
        public bool temProfessor { get; set; }
        public int profId { get; set; }
        public int turmaId { get; set; }
        public string turma { get; set; }
    }
}
