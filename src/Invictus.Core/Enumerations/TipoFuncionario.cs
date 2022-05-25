using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enumerations
{
    public class TipoFuncionario : Enumeration
    {
        public static TipoFuncionario Colaborador = new(1, "Colaborador");
        public static TipoFuncionario Professor = new(2, "Professor");
        public static TipoFuncionario Fornecedor = new(3, "Fornecedor");
        

        public TipoFuncionario()
        {

        }
        public TipoFuncionario(int id, string name) : base(id, name) { }        
    }
}