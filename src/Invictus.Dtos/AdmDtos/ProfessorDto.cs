using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class ProfessorDto
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
        public string celular { get; set; }
        public Guid unidadeId { get; set; }
        public string numero { get; set; }
        public bool ativo { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }

        public string bancoNumero { get; set; }
        public string agencia { get; set; }
        public string conta { get; set; }
        public string tipoConta { get; set; }

    }

    public class ProfessorTurmaView
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }

        public List<MateriaProfessorView> materias { get; set; }
    }

    public class MateriaProfessorView
    {
        public Guid materiaId { get; set; }
        public string nome { get; set; }
    }
}
