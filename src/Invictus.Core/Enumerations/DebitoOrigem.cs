using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enumerations
{
    public class DebitoOrigem : Enumeration
    {
        public static DebitoOrigem Curso = new(1, "Curso");
        public static DebitoOrigem Transferencia = new(2, "Transferência");
        public static DebitoOrigem CompraProduto = new(3, "Compra Produto");
        public static DebitoOrigem Reparcelamento = new(4, "Reparcelamento");


        //public static Status Encerrada = new(4, "Encerrada");
        public DebitoOrigem()
        {

        }
        public DebitoOrigem(int id, string name) : base(id, name) { }
        public static DebitoOrigem TryParseStringToString(string origem)
        {
            if (origem == "Curso")
            {
                return Curso;

            }
            else if (origem == "Transferência")
            {
                return Transferencia;

            }
            else if (origem == "Compra Produto")
            {
                return CompraProduto;

            }           

            throw new NotImplementedException();
        }
        
    }
}
