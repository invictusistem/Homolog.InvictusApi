using System.Collections.Generic;

namespace Invictus.Domain.Administrativo.Models
{
    public class MateriasDaTurma
    {
        public MateriasDaTurma() { }
        public MateriasDaTurma(int id,
                       string descricao,
                       int materiaId,
                       int profId,
                      // int turmaPedagId
            int professorId
            )
        {
            Id = id;
            Descricao = descricao;
            MateriaId = materiaId;
            ProfId = profId;
            //TurmaPedagId = turmaPedagId;
            ProfessorId = professorId;            
        }

        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public int MateriaId { get; private set; }
        public int ProfId { get; private set; }
        //public List<Professor> Professores { get; private set; }
        public int ProfessorId { get; private set; }
        public virtual ProfessorNew Professor { get; private set; }
    }
}

/* 
 professor
    materia 1
    materia 2
    materia 3
       
 professor
    materia 4

********************
*
materia 1
materia 2
materia 3
materia 4
materia 5
materia 6
materia 7
materia 8

materia 1
    true
materia 2
    true
materia 3
    true
materia 4
    true
materia 5
    true
materia 6
    true
materia 7
    true
materia 8
    true
 
 
 */
