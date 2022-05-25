using Invictus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.FuncionarioAggregate
{
    public abstract class Funcionario : Entity
    {
        public string Nome { get; private set; }
    }

    public class Colab : Funcionario
    {
        public string Email { get; private set; }


        public void Create()
        {
            //var func = new Funcionario();
            //func.
        }
    }


}
