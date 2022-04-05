using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.PedagDto
{
    public class ResponsavelDto
    {
        public Guid id { get; set; }
        public string tipo { get; set; }
        public string nome { get; set; }
        public string parentesco { get; set; }
        // public string NomeSocial { get; private set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public DateTime? nascimento { get; set; }
        public string naturalidade { get; set; }
        public string naturalidadeUF { get; set; }
        public string email { get; set; }
        //public string TelReferencia { get; private set; }
        //public string NomeContatoReferencia { get; private set; }
        public string telCelular { get; set; }
        public string telResidencial { get; set; }
        public string telWhatsapp { get; set; }
        public bool temRespFin { get; set; }
        public Guid matriculaId { get; set; }
        //
        public string bairro { get; set; }
        public string cep { get; set; }
        public string complemento { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
    }
}
