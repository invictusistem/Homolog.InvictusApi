using Invictus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.AlunoAggregate
{
    public class AlunoDocumento : Entity
    {
        public AlunoDocumento() { }

        public AlunoDocumento(//int id,
                          Guid matriculaId,
                          string descricao,
                          string comentario,
                          bool docEnviado,
                          //string nome,
                          bool analisado,
                          bool validado,
                          int prazoValidade,
                          //string tipoArquivo,
                          //string contentArquivo,
                          //byte[] dataFile,
                          //DateTime? dataCriacao,
                          Guid turmaId)
        {
            ///Id = id;
            MatriculaId = matriculaId;
            Descricao = descricao;
            Comentario = comentario;
            ///Nome = nome;
            DocEnviado = docEnviado;
            Analisado = analisado;
            Validado = validado;
            PrazoValidade = prazoValidade;
            //TipoArquivo = tipoArquivo;
            //ContentArquivo = contentArquivo;
            //DataFile = dataFile;
            //DataCriacao = dataCriacao;
            TurmaId = turmaId;

        }

       // public Guid Id { get; private set; }
        public Guid MatriculaId { get; private set; }
        public string Descricao { get; private set; }
        public string Comentario { get; private set; }
        public string Nome { get; private set; }
        public bool DocEnviado { get; private set; }
        public bool Analisado { get; private set; }
        public int Tamanho { get; private set; }
        public bool Validado { get; private set; }
        public string TipoArquivo { get; private set; }
        public string ContentArquivo { get; private set; }
        public byte[] DataFile { get; private set; }
        public DateTime? DataCriacao { get; private set; }
        public int PrazoValidade { get; private set; }
        public Guid TurmaId { get; private set; }

        //public int EstagioMatriculaId { get; private set; }
        //public virtual EstagioMatricula EstagioMatricula { get; private set; }
        public void SetTurmaId(Guid turmaId)
        {
            TurmaId = turmaId;
        }
        public void AddDocumento(byte[] bytes, string nome, string tipoArquivo, string contentType, int tamanho)
        {
            Nome = nome;
            TipoArquivo = tipoArquivo;
            DocEnviado = true;
            DataFile = bytes;
            ContentArquivo = contentType;
            Tamanho = tamanho;

            DataCriacao = DateTime.Now;
        }

        public void SetDataCriacao()
        {
            DataCriacao = DateTime.Now;
        }

        public void ValidarDoc(bool validado)
        {
            Analisado = true;
            Validado = validado;
        }
    }
}
