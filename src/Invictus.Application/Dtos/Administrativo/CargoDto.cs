using System;


namespace Invictus.Application.Dtos.Administrativo
{
    public class CargoDto
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public bool ativo { get; set; }
        public DateTime dataCriacao { get; set; }
    }    
}
