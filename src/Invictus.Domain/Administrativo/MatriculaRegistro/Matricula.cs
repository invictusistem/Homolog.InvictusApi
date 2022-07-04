using Invictus.Core;
using Invictus.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.RegistroMatricula
{
    public class Matricula : Entity
    {

        public Matricula() { }
        public Matricula(//int id,
                           Guid alunoId,
                           string nome,
                           string cpf,
                           StatusMatricula status,
                           Guid turmaId)
        {
           // Id = id;
            AlunoId = alunoId;
            Nome = nome;
            CPF = cpf;
            Status = status.DisplayName;
            TurmaId = turmaId;
            //LivroMatriculaId = lvroMatId;
        }

       // public int Id { get; private set; }
        public Guid AlunoId { get; private set; }
        public string NumeroMatricula { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Status { get; private set; }
        public Guid TurmaId { get; private set; }
        public Guid ColaboradorResponsavelMatricula { get; private set; }
        public DateTime DiaMatricula { get; private set; }
        public Guid BolsaId { get; private set; }
        public string Ciencia { get; private set; }
        public Guid CienciaAlunodId { get; private set; }
        public bool MatriculaConfirmada { get; private set; }

        public void TransfTurma(Guid turmaId)
        {
            TurmaId = turmaId;
        }

        public void SetConfirmacaoMatricula(bool confirmada)
        {
            MatriculaConfirmada = confirmada;
        }

        public void SetStatus(string status)
        {
            Status = StatusMatricula.TryParse(status).DisplayName;
        }

        public static StatusMatricula SetMatriculaStatus(bool status)
        {
            if (status) return StatusMatricula.AguardoConfirmacao;

            return StatusMatricula.Regular;

        }

        public void SetCiencia(string ciencia, string alunoId = null)
        {
            Ciencia = ciencia;

            if(ciencia == "Indicação Aluno")
            {
                if(!String.IsNullOrEmpty(alunoId))
                {
                    CienciaAlunodId = new Guid(alunoId);
                }
            }
        }

        public void SetBolsaId(string bolsaId)
        {
            if (bolsaId != "")
            {
                BolsaId = new Guid(bolsaId);
            }
        }

        public void SetColaboradorResponsavelMatricula(Guid colaboradorId)
        {
            ColaboradorResponsavelMatricula = colaboradorId;
        }
        public void SetTurmaId(Guid turmaId)
        {
            TurmaId = turmaId;
        }
        public void SetDiaMatricula()
        {
            DiaMatricula = DateTime.Now;
        }

        public string SetNumeroMatricula(int totalAlunosMatriculados)
        {
            var anoAtual = DateTime.Now.Year;
            var atual = totalAlunosMatriculados + 1;
            var totalChars = 8;
            var totalLength = Convert.ToString(atual).Length;

            string matr = anoAtual.ToString();
            for (int i = 0; i < totalChars - totalLength; i++)
            {
                matr += "0";
            }

            matr += Convert.ToString(atual);

            NumeroMatricula = matr;

            return NumeroMatricula;

        }

    }
}
