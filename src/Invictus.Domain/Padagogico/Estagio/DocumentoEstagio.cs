using Invictus.Core;
using System;

namespace Invictus.Domain.Padagogico.Estagio
{
    public class DocumentoEstagio : Entity
    {
        protected DocumentoEstagio() { }

        public DocumentoEstagio(string descricao,
                              string nome,
                              bool analisado,
                              bool validado,
                              string tipoArquivo,
                              string contentArquivo,
                              byte[] dataFile,
                              string observacao,
                              DateTime? dataCriacao)
        {  
            Descricao = descricao;
            Nome = nome;
            Analisado = analisado;
            Validado = validado;
            TipoArquivo = tipoArquivo;
            ContentArquivo = contentArquivo;
            DataFile = dataFile;
            Observacao = observacao;
            DataCriacao = dataCriacao;

        }

        //public int Id { get; private set; }
        //public int AlunoId { get; private set; }
        public string Descricao { get; private set; }
        public string Nome { get; private set; }
        public bool Analisado { get; private set; }
        public bool Validado { get; private set; }
        public string TipoArquivo { get; private set; }
        public string ContentArquivo { get; private set; }
        public byte[] DataFile { get; private set; }
        public string NomeArquivo { get; private set; }
        public DateTime? DataCriacao { get; private set; }
        public string Observacao { get; private set; }
        public Guid MatriculaEstagioId { get; private set; }
        public virtual MatriculaEstagio MatriculaEstagio { get; private set; }

        //public void AddDataByte(byte[] bytes)
        //{
        //    DataFile = bytes;
        //}

        public void ValidarDoc(bool validado)
        {
            Analisado = true;
            Validado = validado;
        }

        public void AddFileByAluno(byte[] dataFile, string content, string fileName)
        {
            DataCriacao = DateTime.Now;
            DataFile = dataFile;
            ContentArquivo = content;
            NomeArquivo = fileName;
        }
    }
}
