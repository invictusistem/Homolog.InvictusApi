using Invictus.Core;
using Invictus.Core.Enumerations;
using System;
using System.Collections.Generic;

namespace Invictus.Domain.Padagogico.Estagio
{
    public class MatriculaEstagio : Entity
    {
        protected MatriculaEstagio()
        {

        }
        //public MatriculaEstagio(StatusMatricula status,
        //                        //Guid alunoId,
        //                        Guid matriculaId,
        //                        Guid typeEstagioId
        //                        )
        //{
        //    //AlunoId = alunoId;
        //    Status = status.DisplayName;
        //    MatriculaId = matriculaId;
        //    TypeEstagioId = typeEstagioId;
        //    // EstagioId = estagioId;
        //    Documentos = new List<DocumentoEstagio>();
        //}

        //public Guid AlunoId { get; private set; }
        public string NumeroMatricula { get; private set; }
        public string Status { get; private set; }
        public Guid MatriculaId { get; private set; }
        public Guid TypeEstagioId { get; private set; }
        public Guid EstagioId { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public List<DocumentoEstagio> Documentos { get; private set; }

        public static MatriculaEstagio MatriculaEstagioFactory(string matricula, Guid matriculaId, Guid typeEstagioId)
        {
            var docs = new List<DocumentoEstagio>();

            docs.Add(new DocumentoEstagio("Seguro contra acidentes pessoais", "Seguro contra acidentes pessoais", false, false, null, null, null, "", StatusDocumento.NaoEnviado, null));
            docs.Add(new DocumentoEstagio("Cartão de vacinação", "Cartão de vacinação", false, false, null, null, null, "", StatusDocumento.NaoEnviado, null));
            docs.Add(new DocumentoEstagio("Tipo sanguíneo", "Tipo sanguíneo", false, false, null, null, null, "", StatusDocumento.NaoEnviado, null));
            docs.Add(new DocumentoEstagio("Beta HGC recente (expedido nos últimos 30 dias)", "Beta HGC recente (expedido nos últimos 30 dias)", false, false, null, null, null, "apenas para o sexo feminino", StatusDocumento.NaoEnviado, null));


            var matriculaEstagio = new MatriculaEstagio()
            {
                NumeroMatricula = "E" + matricula,
                Status = StatusMatricula.AguardoDocumentacao.DisplayName,
                MatriculaId = matriculaId,
                TypeEstagioId = typeEstagioId,
                DataCadastro = DateTime.Now,
                Documentos = docs
            };

            return matriculaEstagio;
        }

        public void ChangeStatusEstagioMatricula(StatusMatricula status)
        {
            Status = status.DisplayName;
        }


    }
}
