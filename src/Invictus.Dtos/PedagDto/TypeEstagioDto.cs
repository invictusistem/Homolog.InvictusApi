using System;

namespace Invictus.Dtos.PedagDto
{
    public class TypeEstagioDto
    {       
        public Guid id { get; set; }
        public string nome { get; set; }
        public string nivel { get; set; }
        public bool ativo { get; set; }
        public string observacao { get; set; }     
    }
}
