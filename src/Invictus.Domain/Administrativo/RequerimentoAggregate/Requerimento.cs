using Invictus.Core;
using System;
using System.Collections.Generic;

namespace Invictus.Domain.Administrativo.RequerimentoAggregate
{
    public class Requerimento : Entity
    {
        public Requerimento(Guid matriculaRequerenteId,
                            DateTime dataRequerimento,
                            Guid descricao,
                            string observacao,
                            Guid unidadeId)
        {
            MatriculaRequerenteId = matriculaRequerenteId;
            DataRequerimento = dataRequerimento;
            Descricao = descricao;
            Observacao = observacao;
            UnidadeId = unidadeId;

        }
        public Guid MatriculaRequerenteId { get; private set; }
        public DateTime DataRequerimento { get; private set; }
        public Guid Descricao { get; private set; }
        public string Observacao { get; private set; }
        public bool ChamadoEncerrado { get; private set; }
        public DateTime DataEncerramento { get; private set; }
        public Guid ResponsaveEncerramentolId { get; private set; }
        public Guid UnidadeId { get; private set; }
        //public IEnumerable<Resposta> Respostas { get; private set; }

        protected Requerimento()
        {

        }

    }
}
