using Invictus.Core.Enums;
using System;
using System.Collections.Generic;

namespace Invictus.Application.Dtos
{
    public class AlunoDto
    {
        public int id { get; set; }
        public string nome { get; set; }
        // public bool temRespFin { get; set; }
        public string numeroMatricula { get; set; }
        public string nomeSocial { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public DateTime nascimento { get; set; }
        public string naturalidade { get; set; }
        public string naturalidadeUF { get; set; }
        public string email { get; set; }
        public string telReferencia { get; set; }
        public string nomeContatoReferencia { get; set; }
        public string telCelular { get; set; }
        public string telWhatsapp { get; set; }
        public string telResidencial { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string bairro { get; set; }
        public string cienciaCurso { get; set; }
        public string observacoes { get; set; }
        public bool temRespFin { get; set; }
        public bool temRespMenor { get; set; }
        public int unidadeCadastrada { get; set; }
        public bool ativo { get; set; }
        public List<Resp> responsaveis { get; set; }
    }

    public class Resp
    {
        public int id { get; set; }
        public string nome { get; set; }
        //public bool eRespFinanc { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public DateTime nascimento { get; set; }
        public string naturalidade { get; set; }
        public string parentesco { get; set; }
        public string telWhatsapp { get; set; }
        public string naturalidadeUF { get; set; }
        public string email { get; set; }
        public string telCelular { get; set; }
        public string telResidencial { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string bairro { get; set; }
        public string observacoes { get; set; }
        public int alunoId { get; set; }
        //public TipoResponsavel tipoResponsavel { get; set; }
        public string tipoResponsavel { get; set; }
    }

    public class RespFinancDto
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public string parentesco { get; set; }
        public string nascimento { get; set; }
        public string naturalidade { get; set; }
        public string naturalidadeUF { get; set; }
        public string email { get; set; }
        public string telCelular { get; set; }
        public string telWhatsapp { get; set; }
        public string telResidencial { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string bairro { get; set; }
        public string observacoes { get; set; }
        public int alunoId { get; set; }
    }

    public class RespMenorDto
    {
        public int id { get; set; }
        public string nome { get; set; }
        public bool eRespFinanc { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public string nascimento { get; set; }
        public string naturalidade { get; set; }
        public string parentesco { get; set; }
        public string telWhatsapp { get; set; }
        public string naturalidadeUF { get; set; }
        public string email { get; set; }
        public string telCelular { get; set; }
        public string telResidencial { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string bairro { get; set; }
        public string observacoes { get; set; }
        public int alunoId { get; set; }
    }

    public class AlunoExcelDto
    {
        public int id { get; set; }
        public string nome { get; set; }
        //public bool temRespFin { get; set; }
        public string nomeSocial { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public DateTime nascimento { get; set; }
        public string naturalidade { get; set; }
        public string naturalidadeUF { get; set; }
        public string email { get; set; }
        public string telReferencia { get; set; }
        public string nomeContatoReferencia { get; set; }
        public string telCelular { get; set; }
        public string telWhatsapp { get; set; }
        public string telResidencial { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string bairro { get; set; }
        public string cienciaCurso { get; set; }
        public bool ativo { get; set; }
        public string observacoes { get; set; }
        public int unidadeCadastrada { get; set; }
        public DateTime dataCadastro { get; set; }

    }
}
