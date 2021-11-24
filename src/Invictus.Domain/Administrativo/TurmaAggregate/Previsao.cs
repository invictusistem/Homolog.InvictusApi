using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.TurmaAggregate
{
    public class Previsao
    {
        public Previsao(DateTime previsaoAtual,
                        DateTime previsaoTerminoAtual,
                        string previsaoInfo,
                        DateTime dataCriacao
                        )
        {
            PrevisaoAtual = previsaoAtual;
            PrevisaoTerminoAtual = previsaoTerminoAtual;
            PrevisaoInfo = previsaoInfo;
            DataCriacao = dataCriacao;

        }
        public DateTime PrevisaoAtual { get; private set; }
        public DateTime PrevisaoTerminoAtual { get; private set; }
        public string PrevisaoInfo { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public Guid TurmaId { get; private set; }

        public void AdiarInicio(string prevAtual, Previsoes previsoes)
        {
            if (prevAtual == "1ª previsão")
            {
                PrevisaoInfo = "2ª previsão";
                PrevisaoAtual = previsoes.PrevisionStartTwo;
                PrevisaoTerminoAtual = previsoes.PrevisionEndingTwo;

            }
            else if (prevAtual == "2ª previsão")
            {
                PrevisaoInfo = "3ª previsão";
                PrevisaoAtual = previsoes.PrevisionStartThree;
                PrevisaoTerminoAtual = previsoes.PrevisionEndingThree;

            }
            else if (prevAtual == "3ª previsão")
            {
                //throw new NotImplementedException();
            }

        }

        #region EF
        public Previsao() { }
        public virtual Turma Turma { get; private set; }

        #endregion
    }
}
