using Invictus.Core.Enums;

namespace Invictus.Domain.Administrativo.PacoteAggregate
{
    public class Materia
    {
        public Materia() { }
        public Materia(int id,
                        string descricao,
                        int qntAulas,
                        int semestre,
                        string modalidade,
                        //ModalidadeCurso modalidade,
                        int cargaHoraria,
                        int moduloId,
                        int qntProvas,
                        bool temRecuperacao)
        {
            Id = id;
            Descricao = descricao;
            QntAulas = qntAulas;
            Modalidade = Parse(modalidade);
            //Modalidade = modalidade.DisplayName;
            Semestre = semestre;
            CargaHoraria = cargaHoraria;
            // ModuloId = moduloId;
            QntProvas = qntProvas;
            TemRecuperacao = temRecuperacao;
        }

        public string Parse(string mod)
        {
            var modalidade = ModalidadeCurso.TryParse(mod);

            return modalidade.DisplayName;
        }

        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public int QntAulas { get; private set; }
        public string PrimeiroDiaAula { get; private set; }
        public bool PrimeiroDaLista { get; private set; }
        public int Semestre { get; private set; }
        public int CargaHoraria { get; private set; }
        public int QntProvas { get; private set; }
        public bool TemRecuperacao { get; private set; }
        public string Modalidade { get; private set; }
        public int ModuloId { get; private set; }
        // Modalidade = OnLine / Presencial
        public virtual Pacote Modulo { get; private set; }

        public void SetModalidadeCurso(string mod)
        {
            var modalidade = ModalidadeCurso.TryParse(mod);

            Modalidade = modalidade.DisplayName;
        }
    }
}
