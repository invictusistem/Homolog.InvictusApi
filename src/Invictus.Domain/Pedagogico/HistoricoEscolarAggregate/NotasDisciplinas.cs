using Invictus.Core.Enums;
using Invictus.Domain.Administrativo.PacoteAggregate;
using Invictus.Domain.Administrativo.UnidadeAggregate;
using System;
using System.Collections.Generic;

namespace Invictus.Domain.Pedagogico.HistoricoEscolarAggregate
{
    public class NotasDisciplinas
    {
        public NotasDisciplinas()
        {

        }
        public NotasDisciplinas(int id,
                                int trimestre,
                                //Avaliacao avaliacao,
                                string avaliacaoUm,
                                string segundaChamadaAvaliacaoUm,
                                string avaliacaoDois,
                                string segundaChamadaAvaliacaoDois,
                                string avaliacaoTres,
                                string segundaChamadaAvaliacaoTres,
                                int materiaId,
                                string materiaDescricao,
                                int alunoId,
                                int turmaId,

                                ResultadoNotas resultado)
        {
            Id = id;
            Trimestre = trimestre;
            //Avaliacao = avaliacao;
            AvaliacaoUm = avaliacaoUm;
            SegundaChamadaAvaliacaoUm = segundaChamadaAvaliacaoUm;
            AvaliacaoDois = avaliacaoDois;
            SegundaChamadaAvaliacaoDois = segundaChamadaAvaliacaoDois;
            AvaliacaoTres = avaliacaoTres;
            SegundaChamadaAvaliacaoTres = segundaChamadaAvaliacaoTres;
            MateriaId = materiaId;
            MateriaDescricao = materiaDescricao;
            AlunoId = alunoId;
            TurmaId = turmaId;

            Resultado = resultado.DisplayName;

        }
        public int Id { get; private set; }
        public int Trimestre { get; private set; }
        public string AvaliacaoUm { get; private set; }
        public string SegundaChamadaAvaliacaoUm { get; private set; }
        public string AvaliacaoDois { get; private set; }
        public string SegundaChamadaAvaliacaoDois { get; private set; }
        public string AvaliacaoTres { get; private set; }
        public string SegundaChamadaAvaliacaoTres { get; private set; }
        public int MateriaId { get; private set; }
        public string MateriaDescricao { get; private set; }

        public string Resultado { get; private set; }

        public int AlunoId { get; private set; }
        public int TurmaId { get; private set; }
        //public int BoletimEscolarId { get; private set; }
        //public virtual BoletimEscolar BoletimEscolar { get; private set; }

        public void SetTurmaId(int turmaId)
        {
            TurmaId = turmaId;
        }
        public void VerificarStatusResultado()
        {
            var media = 7;
            decimal resultFinal = 0;
            if(AvaliacaoUm != null)
            {
                if(AvaliacaoDois != null)
                {
                    resultFinal = (Convert.ToDecimal(AvaliacaoUm) + Convert.ToDecimal(AvaliacaoDois)) / 2;
                    if(resultFinal >= media)
                    {
                        Resultado = ResultadoNotas.Aprovado.DisplayName;
                    }
                    else
                    {
                        Resultado = ResultadoNotas.Reprovado.DisplayName;
                    }
                       
                }
                else
                {
                    Resultado = ResultadoNotas.Aguardo.DisplayName;
                }
            }
            else
            {
                Resultado = ResultadoNotas.Aguardo.DisplayName;
            }
        }

        public List<NotasDisciplinas> CreateNotasDisciplinas(List<Materia> materias, int alunoId, int turmaId)
        {
            var notasDisciplinas = new List<NotasDisciplinas>();
            foreach (var mat in materias)
            {
                //var situa = Situacao.Aguardo;
                notasDisciplinas.Add(new NotasDisciplinas(0, 0, null, null, null, null, null, null,
                    mat.Id, mat.Descricao, alunoId, turmaId, ResultadoNotas.Aguardo));
            }

            return notasDisciplinas;

        }

    }
}
