using Invictus.Core;
using Invictus.Core.Interfaces;
using System;
using System.Globalization;
using System.Text;

namespace Invictus.Domain.Administrativo.ColaboradorAggregate
{
    public class Colaborador : Entity, IAggregateRoot
    {
        public Colaborador() { }
        public Colaborador(string nome,
                           string email,
                           string cpf,
                           string celular,
                           //string cargo,
                           Guid cargoId,
                           Guid unidadeId,                           
                           bool ativo,
                           ColaboradorEndereco endereco)
        {
           
            Nome = nome;
            Email = email;
            CPF = cpf;
            Celular = celular;
           // Cargo = cargo;
            CargoId = cargoId;
            UnidadeId = unidadeId;           
            Ativo = ativo;
            Endereco = endereco;
        }
       
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public string Celular { get; private set; }
        //public string Cargo { get; private set; }
        public Guid CargoId { get; private set; }
        public Guid UnidadeId { get; private set; }       
        public bool Ativo { get; private set; }       
        public DateTime DataCriacao { get; private set; }
        public ColaboradorEndereco Endereco { get; private set; }

        public void DesativarColaborador()
        {
            Ativo = false;
        }

        public void SetDataCriacao()
        {
            DataCriacao = DateTime.Now;
        }

        public void TratarEmail(string email)
        {
            Email = RemoveDiacritics(email);
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
