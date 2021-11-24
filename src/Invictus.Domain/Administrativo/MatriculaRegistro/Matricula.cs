﻿using Invictus.Core;
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
        public DateTime DiaMatricula { get; private set; }

        public void SetTurmaId(Guid turmaId)
        {
            TurmaId = turmaId;
        }
        public void SetDiaMatricula()
        {
            DiaMatricula = DateTime.Now;
        }

        public void SetNumeroMatricula(int totalAlunosMatriculados)
        {
            var atual = totalAlunosMatriculados + 1;
            var totalChars = 8;
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