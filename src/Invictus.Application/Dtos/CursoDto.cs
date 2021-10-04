using System;
using System.Collections.Generic;

namespace Invictus.Application.Dtos
{
    public class CreateCursoDto
    {

        //public int cursoId { get; set; }
        // public string identificador { get; set; }
        //public string descricao { get; set; }
        public int modulo { get; set; } // new
        public int curso { get; set; }
        public string descricao { get; set; }
        public int vagas { get; set; }
        public int minVagas { get; set; }
        public int planoId { get; set; }
        //public int quantPrevisoes { get; set; }
        //public DateTime prevInicio { get; set; }
        // [JsonConverter(typeof(DateTimeOffsetConverter))]

        public int salaId { get; set; }
        public DateTime prevInicio_1 { get; set; }
        public DateTime prevTermino_1 { get; set; }
        public DateTime prevInicio_2 { get; set; }
        public DateTime prevTermino_2 { get; set; }
        public DateTime prevInicio_3 { get; set; }
        public DateTime prevTermino_3 { get; set; }        
        //public string turno { get; set; }
        //public HorariosDto horarios { get; set; }
        public string horarioIni_1 { get; set; }
        public string horarioFim_1 { get; set; }
        
        public string horarioIni_2 { get; set; }
        public string horarioFim_2 { get; set; }

        public int pacoteId { get; set; }

        public DateTime segundoDiaAula { get; set; }
        //public string horarioIni_3 { get; set; }
        //public string horarioFim_3 { get; set; }
        //public string horarioIni_FDS { get; set; }
        //public string horarioFim_FDS { get; set; }
        //public string dia1 { get; set; }
        //public string dia2 { get; set; }
        // public string dia3 { get; set; }
        //public int totalAlunos { get; set; }


        // public List<ProfessoresDTO> listaProfessores { get; set; }
        //public bool iniciada { get; set; }
        //public string preco { get; set; }
        //public string turno { get; set; }
        //public int? duracao { get; set; }
        //public int? idadeMinima { get; set; }
        //public List<int> professores { get; set; }
        //public List<MateriaDto> gradeCurricular { get; set; }
    }

    //public class ProfessoresDTO
    //{
    //    public int id { get; set; }
    //}

    //public class MateriaDto
    //{
    //    public int materiaId { get; set; }
    //    public string descricao { get; set; }
    //}
}
