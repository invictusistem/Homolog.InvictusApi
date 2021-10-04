namespace Invictus.Application.Dtos
{
    public class EstagioDto
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string dataInicio { get; set; }
        public int trimestre { get; set; }
        public int vagas { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string bairro { get; set; }
    }
}
