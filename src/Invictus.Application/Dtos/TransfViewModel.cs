using System;

namespace Invictus.Application.Dtos
{
    public class TransfViewModel
    {
        public int id {get;set;}
        public string nome {get;set;}
        public string cpf  { get; set; }
        public string rg  { get; set; }
        public DateTime nascimento { get; set; }
        public string nomeSocial { get; set; }
        public string naturalidade { get; set; }
        public string uf { get; set; }
        public string email { get; set; }
        public int turmaId { get; set; }
        public string identificador { get; set; }
        public string descricao { get; set; }
        public string unidade { get; set; }

    }
}
