
using System;

namespace Invictus.Core.Enums
{
    public class ModalidadeCurso : Enumeration
    {
        public static ModalidadeCurso OnLine = new(1, "On-line");
        public static ModalidadeCurso Presencial = new(2, "Presencial");
        public static ModalidadeCurso Estagio = new(3, "Estágio");

        public ModalidadeCurso() { }
        public ModalidadeCurso(int id, string name) : base(id, name) { }

        public static ModalidadeCurso TryParse(string compare)
        {
            if (compare == "online")
            {
                return OnLine;

            }
            else if (compare == "presencial")
            {
                return Presencial;

            }
            else if (compare == "estagio")
            {
                return Estagio;

            }

            if (compare == "On-line")
            {
                return OnLine;

            }
            else if (compare == "Presencial")
            {
                return Presencial;

            }
            else if (compare == "Estágio")
            {
                return Estagio;

            }

            throw new NotImplementedException();
        }
    }
}
