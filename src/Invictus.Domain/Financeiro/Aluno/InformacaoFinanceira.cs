using Invictus.Core.Enums;
using Invictus.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Invictus.Domain.Financeiro.Aluno
{
    public class InformacaoFinanceiraAggregate : IAggregateRoot
    {
        public InformacaoFinanceiraAggregate()
        {

        }
        public InformacaoFinanceiraAggregate(int id,
                                    int alunoId,
                                    int turmaId,
                                    decimal valorCurso,
                                    DateTime dataRegistro,
                                    int idUnidadeCadastroInicial,
                                    int parcelas,
                                    bool matConfirmada
                                    )
        {
            Id = id;
            AlunoId = alunoId;
            TurmaId = turmaId;
            ValorCurso = valorCurso;
            DataRegistro = dataRegistro;
            IdUnidadeCadastroInicial = idUnidadeCadastroInicial;
            Parcelas = parcelas;
            MatConfirmada = matConfirmada;
            Debitos = new List<Debito>();
        }
        public int Id { get; private set; }
        public int AlunoId { get; private set; }
        public int TurmaId { get; private set; }
        public decimal ValorCurso { get; private set; }
        public DateTime DataRegistro { get; private set; }
        public int IdUnidadeCadastroInicial { get;private set; }
        public int Parcelas { get; private set; }
        public bool MatConfirmada { get; private set; }
        //public ValorEntrada Entrada { get; private set; }
        public List<Debito> Debitos { get; private set; } // reference toTransacoesBoleto etc...

        //public void AddEntrada(ValorEntrada entrada)
        //{
        //    Entrada = entrada;
        //}
        public void SetTurmaId(int turmaId)
        {
            TurmaId = turmaId;
        }
        public void AddDebitos(IEnumerable<Debito> debitos)
        {
            Debitos.AddRange(debitos);
        }
    }

    
    
}