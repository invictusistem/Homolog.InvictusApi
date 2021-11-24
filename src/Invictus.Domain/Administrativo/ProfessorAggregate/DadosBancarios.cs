using Invictus.Core;
using Invictus.Core.Enumerations;
using System;
using System.Collections.Generic;

namespace Invictus.Domain.Administrativo.ProfessorAggregate
{
    public class DadosBancarios : ValueObject
    {
        public DadosBancarios(string bancoNumero,
                            string agencia,
                            string conta,
                            TipoConta tipoConta)
        {
            BancoNumero = bancoNumero;
            Agencia = agencia;
            Conta = conta;
            if(tipoConta != null ) TipoConta = tipoConta.DisplayName;

        }

        public string BancoNumero { get; private set; }
        public string Agencia { get; private set; }
        public string Conta { get; private set; }
        public string TipoConta { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return BancoNumero;
            yield return Agencia;
            yield return Conta;
            yield return TipoConta;
        }


        public DadosBancarios() { }
    }
}
