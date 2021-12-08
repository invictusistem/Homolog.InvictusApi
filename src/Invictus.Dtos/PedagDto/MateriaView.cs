using System;
using System.Collections.Generic;

namespace Invictus.Dtos.PedagDto
{
    public class MateriaView
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public Guid professorId { get; set; }
        public bool isProfessor { get; set; }
    
        public void SetIsProfessor(List<MateriaView> listaMateriasTurma)
        {

            foreach (var mat in listaMateriasTurma)
            {
                if(mat.professorId.ToString() == "00000000-0000-0000-0000-000000000000")
                {
                    isProfessor = false;
                }
                else
                {
                    isProfessor = true;
                }
            }
        }
    
    }
}
