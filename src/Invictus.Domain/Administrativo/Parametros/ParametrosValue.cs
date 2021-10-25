using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.Parametros
{
    public class ParametrosValue
    {
        public ParametrosValue(string nome)
        {
            Nome = nome;
            //ParametrosValues = new List<ParametrosValue>();
        }
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public int ParametrosTypeId { get; private set; }
        public virtual ParametrosType  ParametrosType { get; private set; }
    }
}
