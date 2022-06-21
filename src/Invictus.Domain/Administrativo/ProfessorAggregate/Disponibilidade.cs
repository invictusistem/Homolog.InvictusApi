//using Invictus.Core;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Invictus.Domain.Administrativo.ProfessorAggregate
//{
//    public class Disponibilidade : Entity
//    {
//        public Disponibilidade(bool domingo,
//                                bool segunda,
//                                bool terca,
//                                bool quarta,
//                                bool quinta,
//                                bool sexta,
//                                bool sabado,
//                                Guid unidadeId,
//                                Guid pessoaId)
//        {
//            Domingo = domingo;
//            Segunda = segunda;
//            Terca = terca;
//            Quarta = quarta;
//            Quinta = quinta;
//            Sexta = sexta;
//            Sabado = sabado;
//            UnidadeId = unidadeId;
//            PessoaId = pessoaId;
//        }
//        public bool Domingo { get; private set; }
//        public bool Segunda { get; private set; }
//        public bool Terca { get; private set; }
//        public bool Quarta { get; private set; }
//        public bool Quinta { get; private set; }
//        public bool Sexta { get; private set; }
//        public bool Sabado { get; private set; }
//        public Guid UnidadeId { get; private set; }
//        public Guid PessoaId { get; private set; }
//        public DateTime DataAtualizacao { get; private set; }

//        #region EF
//        public Disponibilidade() { }
//        //public Guid ProfessorId { get; private set; }
//        //public virtual Professor Professor { get; private set; }
//        #endregion

//    }
//}
