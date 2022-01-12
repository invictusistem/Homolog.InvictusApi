using Invictus.Core;
using Invictus.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Padagogico.NotasTurmas
{
    public class TurmaNotas : Entity
    {
        public TurmaNotas()
        {

        }
        public TurmaNotas(//int id,
                               // int trimestre,
                                //Avaliacao avaliacao,
                                string avaliacaoUm,
                                string segundaChamadaAvaliacaoUm,
                                string avaliacaoDois,
                                string segundaChamadaAvaliacaoDois,
                                string avaliacaoTres,
                                string segundaChamadaAvaliacaoTres,
                                Guid materiaId,
                                string materiaDescricao,
                                Guid matriculaId,
                                Guid turmaId,

                                ResultadoNotas resultado)
        {
           // Id = id;
           // Trimestre = trimestre;
            //Avaliacao = avaliacao;
            AvaliacaoUm = avaliacaoUm;
            SegundaChamadaAvaliacaoUm = segundaChamadaAvaliacaoUm;
            AvaliacaoDois = avaliacaoDois;
            SegundaChamadaAvaliacaoDois = segundaChamadaAvaliacaoDois;
            AvaliacaoTres = avaliacaoTres;
            SegundaChamadaAvaliacaoTres = segundaChamadaAvaliacaoTres;
            MateriaId = materiaId;
            MateriaDescricao = materiaDescricao;
            MatriculaId = matriculaId;
            TurmaId = turmaId;

            Resultado = resultado.DisplayName;

        }
       // public int Id { get; private set; }
        //public int Trimestre { get; private set; }
        public string AvaliacaoUm { get; private set; }
        public string SegundaChamadaAvaliacaoUm { get; private set; }
        public string AvaliacaoDois { get; private set; }
        public string SegundaChamadaAvaliacaoDois { get; private set; }
        public string AvaliacaoTres { get; private set; }
        public string SegundaChamadaAvaliacaoTres { get; private set; }
        public Guid MateriaId { get; private set; }
        public string MateriaDescricao { get; private set; }
        public string Resultado { get; private set; }
        public Guid MatriculaId { get; private set; }
        public Guid TurmaId { get; private set; }
        //public int BoletimEscolarId { get; private set; }
        //public virtual BoletimEscolar BoletimEscolar { get; private set; }

        //public void SetTurmaId(int turmaId)
        //{
        //    TurmaId = turmaId;
        //}
        public static TurmaNotas CreateNota(Guid id,
                                string avaliacaoUm,
                                string segundaChamadaAvaliacaoUm,
                                string avaliacaoDois,
                                string segundaChamadaAvaliacaoDois,
                                string avaliacaoTres,
                                string segundaChamadaAvaliacaoTres,
                                Guid materiaId,
                                string materiaDescricao,
                                Guid matriculaId,
                                Guid turmaId,
                                ResultadoNotas resultado)
        {

            var notas = new TurmaNotas();

            notas.Id = id;
            notas.AvaliacaoUm = avaliacaoUm;
            notas.SegundaChamadaAvaliacaoUm = segundaChamadaAvaliacaoUm;
            notas.AvaliacaoDois = avaliacaoDois;
            notas.SegundaChamadaAvaliacaoDois = segundaChamadaAvaliacaoDois;
            notas.AvaliacaoTres = avaliacaoTres;
            notas.SegundaChamadaAvaliacaoTres = segundaChamadaAvaliacaoTres;
            notas.MateriaId = materiaId;
            notas.MateriaDescricao = materiaDescricao;
            notas.MatriculaId = matriculaId;
            notas.TurmaId = turmaId;

            notas.Resultado = resultado.DisplayName;

            return notas;
        }
        public void VerificarStatusResultado()
        {
            var media = 7;
            decimal resultFinal = 0;
            if (AvaliacaoUm != null)
            {
                if (AvaliacaoDois != null)
                {
                    resultFinal = (Convert.ToDecimal(AvaliacaoUm) + Convert.ToDecimal(AvaliacaoDois)) / 2;
                    if (resultFinal >= media)
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

        //public List<TurmaNotas> CreateNotasDisciplinas(List<Materia> materias, int alunoId, int turmaId)
        //{
        //    var notasDisciplinas = new List<TurmaNotas>();
        //    foreach (var mat in materias)
        //    {
        //        //var situa = Situacao.Aguardo;
        //        notasDisciplinas.Add(new TurmaNotas(0, 0, null, null, null, null, null, null,
        //            mat.Id, mat.Descricao, alunoId, turmaId, ResultadoNotas.Aguardo));
        //    }

        //    return notasDisciplinas;

        //}

        public TurmaNotas CreateNotaDisciplinas(string materiaDescricao, Guid materiaTurmaId, Guid matriculaId, Guid turmaId)
        {
            var notaDisciplina = new TurmaNotas(null, null, null, null, null, null,
                    materiaTurmaId, materiaDescricao, matriculaId, turmaId, ResultadoNotas.Aguardo);
            

            return notaDisciplina;

        }

    }
}