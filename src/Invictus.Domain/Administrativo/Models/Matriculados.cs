using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.Models
{
    public class Matriculados
    {
        public Matriculados() { }
        public Matriculados(int id,
                           int alunoId,
                           string nome,
                           string cpf,
                           string status,
                           int turmaId)
        {
            Id = id;
            AlunoId = alunoId;
            Nome = nome;
            CPF = cpf;
            Status = status;
            TurmaId = turmaId;
            //LivroMatriculaId = lvroMatId;
        }

        public int Id { get; private set; }
        public int AlunoId { get; private set; }
        public string NumeroMatricula { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Status { get; private set; }
        public int TurmaId { get; private set; }
        public DateTime DiaMatricula { get; private set; }

        public void SetTurmaId(int turmaId)
        {
            TurmaId = turmaId;
        }
        public void SetDiaMatricula()
        {
            DiaMatricula = DateTime.Now;
        }

        public void SetNumeroMatricula(int totalAlunosBase)
        {
            var atual = totalAlunosBase + 1;
            var totalChars = 7;
            var totalLength = Convert.ToString(atual).Length;

            string matr = "";
            for (int i = 0; i < totalChars - totalLength; i++)
            {
                matr += "0";
            }

            matr += Convert.ToString(atual);

            NumeroMatricula = matr;

        }
    }
}
