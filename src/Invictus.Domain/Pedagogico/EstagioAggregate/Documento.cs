using System;

namespace Invictus.Domain.Pedagogico.EstagioAggregate
{
    public class Documento
    {
        public Documento() { }

        public Documento(int id,
                          int alunoId,
                          string descricao,
                          string nome,
                          bool analisado,
                          bool validado,
                          string tipoArquivo,
                          string contentArquivo,
                          byte[] dataFile,
                          DateTime? dataCriacao)
        {
            Id = id;
            AlunoId = alunoId;
            Descricao = descricao;
            Nome = nome;
            Analisado = analisado;
            Validado = validado;
            TipoArquivo = tipoArquivo;
            ContentArquivo = contentArquivo;
            DataFile = dataFile;
            DataCriacao = dataCriacao;

        }

        public int Id { get; private set; }
        public int AlunoId { get; private set; }
        public string Descricao { get; private set; }
        public string Nome { get; private set; }
        public bool Analisado { get; private set; }
        public bool Validado { get; private set; }
        public string TipoArquivo { get; private set; }
        public string ContentArquivo { get; private set; }
        public byte[] DataFile { get; private set; }
        public DateTime? DataCriacao { get; private set; }
        
        //public int EstagioMatriculaId { get; private set; }
        //public virtual EstagioMatricula EstagioMatricula { get; private set; }

        public void AddDataByte(byte[] bytes)
        {
            DataFile = bytes;
        }

        public void ValidarDoc(bool validado)
        {
            Analisado = true;
            Validado = validado;
        }




    }
}
