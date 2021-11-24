using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enumerations
{
    public class UsuarioPerfil : Enumeration
    {
        public static UsuarioPerfil MasterAdm = new(1, "MasterAdm");
        public static UsuarioPerfil SuperAdm = new(2, "SuperAdm");
        public static UsuarioPerfil Professor = new(3, "Professor");
        public static UsuarioPerfil Pedagogia = new(4, "Pedagogia");
        public static UsuarioPerfil Administrador = new(5, "Administrador");
        public static UsuarioPerfil Comercial = new(6, "Comercial");
        public static UsuarioPerfil Aluno = new(7, "Aluno");


        public UsuarioPerfil() { }
        public UsuarioPerfil(int id, string name) : base(id, name) { }

        public static UsuarioPerfil TryParse(string compare)
        {
            if (compare == "MasterAdm")
            {
                return MasterAdm;

            }
            else if (compare == "SuperAdm")
            {
                return SuperAdm;

            }
            else if (compare == "Professor")
            {
                return Professor;

            }
            else if (compare == "Pedagogia")
            {
                return Pedagogia;

            }
            else if (compare == "Administrador")
            {
                return Administrador;

            }
            else if (compare == "Comercial")
            {
                return Comercial;

            }

            throw new NotImplementedException();
        }
    }
}
