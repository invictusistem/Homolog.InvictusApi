using Invictus.Core;
using Invictus.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.TurmaAggregate
{
    public class TurmaMaterias : Entity
    {
        public TurmaMaterias(string nome,
                                string descricao,
                                ModalidadeCurso modalidade,
                                int cargaHoraria,
                                Guid typePacoteId,
                                Guid materiaId,
                                bool ativo
                                )
        {
            Nome = nome;
            Descricao = descricao;
            Modalidade = modalidade.DisplayName;
            CargaHoraria = cargaHoraria;
            TypePacoteId = typePacoteId;
            MateriaId = materiaId;
            Ativo = ativo;

        }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public string Modalidade { get; private set; }
        public int CargaHoraria { get; private set; }
        public bool Ativo { get; private set; }
        public Guid TypePacoteId { get; private set; }
        public Guid TurmaId { get; private set; }
        public Guid MateriaId { get; private set; } // referência ao Id do MateriaTemplate

        #region EF

        public TurmaMaterias() { }
        public virtual Turma Turma { get; private set; }

        #endregion

    }
}
