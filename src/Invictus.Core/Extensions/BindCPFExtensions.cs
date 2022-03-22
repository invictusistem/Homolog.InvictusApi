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

            string substr = cpf.Substring(6, 3); // 095521 287 14
            string newCPF = "******." + substr + "-**";


            return newCPF;
        }

        public static string MaskingCPF(this string cpf)
        {

            //string substr = cpf.Substring(0, 3); // 09552128714
            string maskingCPF = cpf.Substring(0, 3) + ".";
            maskingCPF += cpf.Substring(3, 3) + ".";
            maskingCPF += cpf.Substring(6, 3) + "-";
            maskingCPF += cpf.Substring(9, 2);
            


            return maskingCPF;
        }
    }
}
