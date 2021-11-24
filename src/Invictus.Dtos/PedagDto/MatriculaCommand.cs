using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.PedagDto
{
    public class MatriculaCommand
    {
        public MatriculaCommand()
        {

        }
        // public Guid planoSelectId { get; set; }
        public PlanCommand plano { get; set; }
        public bool menorIdade { get; set; }
        public MatForm respMenor { get; set; }
        public bool temRespFin { get; set; }
        public MatForm respFin { get; set; }
    }

    public class MatForm
    {
        public MatForm()
        {

        }
        public string nome { get; set; }
        public string tipo { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public DateTime? nascimento { get; set; }
        public string parentesco { get; set; }
        public string naturalidade { get; set; }
        public string naturalidadeUF { get; set; }
        public string email { get; set; }
        public string telCelular { get; set; }
        public string telWhatsapp { get; set; }
        public string telResidencial { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string bairro { get; set; }
        public Guid matriculaId { get; set; }
    }

    public class PlanCommand
    {
        public PlanCommand()
        {

        }
        public decimal valor { get; set; }
        public decimal taxaMatricula { get; set; }
        public bool confirmacaoPagmMat { get; set; }
        public decimal bonusPontualidade { get; set; }
        public int parcelas { get; set; }
        public Guid planoId { get; set; }
    }
}
