using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.PedagDto
{
    public class ViewMatriculadosDto
    {
        public Guid id { get; set; }
        public string numeroMatricula { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public string email { get; set; }
        public string nome { get; set; }
        public string status { get; set; }
        public Guid matriculaId { get; set; }
        public Guid estagioMatriculaId { get; set; }
        public DateTime nascimento { get; set; }
        public DateTime dataCadastro { get; set; }
        public string sigla { get; set; }
        public bool ativo { get; set; }
        public bool acessoSistema { get; set; }
        public bool MatriculaConfirmada { get; set; }
        public string turmaDescricao { get; set; }
        public string turmaIdentificador { get; set; }
    }
}
