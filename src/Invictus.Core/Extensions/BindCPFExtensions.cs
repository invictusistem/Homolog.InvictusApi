using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Extensions
{
    public static class BindCPFExtensions
    {
        public static string BindingCPF(this string cpf)
        {

            string substr = cpf.Substring(6, 3);
            string newCPF = "******." + substr + "-**";


            return newCPF;
        }
    }
}
