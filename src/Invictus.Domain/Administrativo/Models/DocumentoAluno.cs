﻿using System;


namespace Invictus.Domain.Administrativo.Models
{
    public class DocumentoAluno
    {
        public DocumentoAluno() { }

        public DocumentoAluno(//int id,
                          int alunoId,
                          string descricao,
                          string comentario,
                          bool docEnviado,
                          //string nome,
                          bool analisado,
                          bool validado,
                          //string tipoArquivo,
                          //string contentArquivo,
                          //byte[] dataFile,
                          //DateTime? dataCriacao,
                          int turmaId)
        {
            ///Id = id;
            AlunoId = alunoId;
            Descricao = descricao;
            Comentario = comentario;
            ///Nome = nome;
            DocEnviado = docEnviado;
            Analisado = analisado;
            Validado = validado;
            //TipoArquivo = tipoArquivo;
            //ContentArquivo = contentArquivo;
            //DataFile = dataFile;
            //DataCriacao = dataCriacao;
            TurmaId = turmaId;

        }

        public int Id { get; private set; }
        public int AlunoId { get; private set; }
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
        public int TurmaId { get; private set; }

        //public int EstagioMatriculaId { get; private set; }
        //public virtual EstagioMatricula EstagioMatricula { get; private set; }
        public void SetTurmaId(int turmaId)
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
