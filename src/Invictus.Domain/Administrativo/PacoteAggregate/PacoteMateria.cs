using Invictus.Core;
using Invictus.Core.Enums;
using System;

namespace Invictus.Domain.Administrativo.PacoteAggregate
{
    public class PacoteMateria : Entity
    {   
        public PacoteMateria(//int id,
                        string nome,
                        Guid materiaId,
                        ModalidadeCurso modalidade,
                        int cargaHoraria
            )
        {
            Nome = nome;
            MateriaId = materiaId;
            Modalidade = modalidade.DisplayName;
            CargaHoraria = cargaHoraria;
            
        }

        public PacoteMateria(//int id,
                        string nome,
                        Guid materiaId,
                        int ordem,
                        int cargaHoraria,//string modalidade,
                        ModalidadeCurso modalidade,
                        Guid pacoteId
                       // int cargaHoraria
                       //int moduloId,
                       //int qntProvas,
                       //bool temRecuperacao
            )
        {
            //Id = id;
            Nome = nome;
            MateriaId = materiaId;
            //QntAulas = qntAulas;
            Ordem = ordem;
            CargaHoraria = cargaHoraria;
            Modalidade = modalidade.DisplayName;
            PacoteId = pacoteId;
            //Modalidade = modalidade.DisplayName;
            //Semestre = semestre;
            

        }

        //public string Descricao { get; private set; } // Nome do MateriaTemplate
        public string Nome { get; private set; }
        public Guid MateriaId { get; private set; }
        public int Ordem { get; private set; }
        public int CargaHoraria { get; private set; }
      
        public string Modalidade { get; private set; }

        public void SetOrdem(int ordem)
        {
            Ordem = ordem + 1;
        }
        public void SetModalidadeCurso(string mod)
        {
            var modalidade = ModalidadeCurso.TryParse(mod);

            Modalidade = modalidade.DisplayName;
        }

        #region EF

        public PacoteMateria() { }
        public Guid PacoteId { get; private set; }
        public virtual Pacote Pacote { get; private set; }

        #endregion
    }
}
