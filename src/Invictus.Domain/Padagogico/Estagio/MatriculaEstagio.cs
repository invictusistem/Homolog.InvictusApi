using Invictus.Core;
using Invictus.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Padagogico.Estagio
{
    public class MatriculaEstagio : Entity
    {
        public MatriculaEstagio()
        {

        }
        public MatriculaEstagio(StatusMatricula status,
                                Guid alunoId,
                                Guid matriculaId,
                                Guid estagioId)
        {
            AlunoId = alunoId;
            MatriculaId = matriculaId;
            EstagioId = estagioId;
            Documentos = new List<DocumentoEstagio>();
        }

        public Guid AlunoId { get; private set; }
        public string NumeroMatricula { get; private set; }
        public string Status { get; private set; }
        public Guid MatriculaId { get; private set; }
        public Guid EstagioId { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public List<DocumentoEstagio> Documentos { get; private set; }
        

    }
}
