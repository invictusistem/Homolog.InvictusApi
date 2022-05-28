using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enumerations
{
    public class TipoPessoa : Enumeration
    {
        public static TipoPessoa Colaborador = new(1, "Colaborador");
        public static TipoPessoa Professor = new(2, "Professor");
        public static TipoPessoa Fornecedor = new(3, "Fornecedor");
        public static TipoPessoa Aluno = new(4, "Aluno");
        public static TipoPessoa Matriculado = new(5, "Matriculado");


        public TipoPessoa()
        {

        }
        public TipoPessoa(int id, string name) : base(id, name) { }
    }
}