using Invictus.Core;
using Invictus.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.Models
{
    public class MateriaTemplate : Entity
    {
        public MateriaTemplate(string nome,
                                string descricao,
                                ModalidadeCurso modalidade,
                                int cargaHoraria,
                                Guid typePacoteId,
                                bool ativo
                                )
        {
            Nome = nome;
            Descricao = descricao;
            Modalidade = modalidade.DisplayName;
            CargaHoraria = cargaHoraria;
            TypePacoteId = typePacoteId;
            Ativo = ativo;

        }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public string Modalidade { get; private set; }
        public int CargaHoraria { get; private set; }
        public bool Ativo { get; private set; }
        public Guid TypePacoteId { get; private set; }

        #region EF

        public MateriaTemplate(){ }

        #endregion

    }
}
