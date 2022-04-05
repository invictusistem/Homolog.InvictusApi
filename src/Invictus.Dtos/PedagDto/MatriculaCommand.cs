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
            plano = new PlanCommand();
        }
        // public Guid planoSelectId { get; set; }
        public PlanCommand plano { get; set; }
        public bool menorIdade { get; set; }
        public MatForm respMenor { get; set; }
        public bool temRespFin { get; set; }
        public MatForm respFin { get; set; }



        public Guid alunoId { get; set; }
        public Guid turmaId { get; set; }
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
            infoParcelas = new List<Parcela>();
        }
        public DateTime diaDefault { get; set; }
        public decimal valor { get; set; }  // ok
        public decimal taxaMatricula { get; set; }// ??
        public bool confirmacaoPagmMat { get; set; } // true
        public decimal bonusPontualidade { get; set; } // ok
        public decimal valorParcela { get; set; } // ok - OBS: ver parc diferentes
        public int parcelas { get; set; } // count nas linhas 
        public Guid planoId { get; set; } // pegar e ver se todos sao iguais
        public string codigoDesconto { get; set; }
        public string bolsaId { get; set; }
        public string ciencia { get; set; }
        public string cienciaAlunoId { get; set; }

        public List<Parcela> infoParcelas { get; set; }
    }

    public class Parcela
    {
        public string parcelaNo { get; set; }// ideia ordenar por data
        public decimal valor { get; set; } // tem na planilha
        public DateTime vencimento { get; set; } // tem na planilha
    }
}
