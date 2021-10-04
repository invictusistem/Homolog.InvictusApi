using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos.Pedagogico
{
    public class EstagioMatriculaViewModel
    {
        public EstagioMatriculaViewModel()
        {
            inscritos = new List<EstagioMatriculaDto>();
        }
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

        public List<EstagioMatriculaDto> inscritos {get;set;}
    }

    public class EstagioMatriculaDto
    {
        public int id { get; set; }
        public int alunoId { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
    }
}
