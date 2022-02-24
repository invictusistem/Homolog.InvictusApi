using Invictus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.Calendarios
{
    public class Calendario : Entity
    {
        public Calendario() { }
        public Calendario(
            // int id,
            DateTime diaAula,
            //Turno turno,
            string diaDaSemana,
            string horaInicial,
string horaFinal,

            //string materia,
            //Guid materiaId,
            //Guid professorId,
            //string turma,
            Guid turmaId,
            //string unidade,
            Guid unidadeId,
            bool aulaIniciada,
            bool aulaConcluida,
            Guid salaId

            )
        {
            // Id = id;
            DiaAula = diaAula;
            //Turno = turno;
            DiaDaSemana = diaDaSemana;
            HoraInicial = horaInicial;
            HoraFinal = horaFinal;

            //Materia = materia;
            //MateriaId = materiaId;
            //ProfessorId = professorId;
            //Turma = turma;
            TurmaId = turmaId;
            //Unidade = unidade;
            UnidadeId = unidadeId;
            AulaIniciada = aulaIniciada;
            AulaConcluida = aulaConcluida;
            SalaId = salaId;

        }

        // public int Id { get; private set; }
        public DateTime DiaAula { get; private set; }
        //public Turno Turno { get; private set; }
        //public DayOfWeek DiaDaSemana { get; private set; }
        public string DiaDaSemana { get; private set; }

        public Guid MateriaId { get; private set; }
        public string HoraInicial { get; private set; }
        public string HoraFinal { get; private set; }
        //public string HoraInicial { get; set; }
        //public string HoraFinal { get; set; }
        public Guid ProfessorId { get; private set; }
        //public string Turma { get; private set; }
        public Guid TurmaId { get; private set; }
        //public string Unidade { get; set; }
        //public string SalaId { get; set; }
        public Guid UnidadeId { get; private set; }
        public bool AulaIniciada { get; private set; }
        public bool TemReposicao { get; private set; }
        public DateTime DateAulaIniciada { get; private set; }
        public bool AulaConcluida { get; private set; }
        public DateTime DateAulaConcluida { get; private set; }
        public string Observacoes { get; private set; }
        public bool EhSubstituto { get; private set; }
        public Guid SalaId { get; private set; }
        // public virtual Turma Turma { get; private set; }
        public void VerificarSeSubstituto(Guid professorDaMateria, Guid professorPretendido)
        {
            if(professorDaMateria == professorPretendido)
            {
                EhSubstituto = false;
            }
            else
            {
                EhSubstituto = true;
            }
        }
        public void SetHoraInicial(string inicio)
        {
            HoraInicial = inicio;
        }
        public void SetHoraFinal(string fim)
        {
            HoraFinal = fim;
        }

        public void RemoveProfessorDaAula()
        {
            ProfessorId = new Guid("00000000-0000-0000-0000-000000000000");
            EhSubstituto = false;
        }
        public void SetProfessorId(Guid profId)
        {
            ProfessorId = profId;
            EhSubstituto = false;
        }
        public void SetMateriaId(Guid id)
        {
            MateriaId = id;
        }
        public void SetObservacoes(string obs)
        {
            Observacoes = obs;
        }

        public void IniciarAula()
        {
            DateAulaIniciada = DateTime.Now;
            AulaIniciada = true;
        }

        public void ConcluirAula()
        {
            AulaConcluida = true;
        }

        public void SetDataConclusaoAula()
        {
            DateAulaConcluida = DateTime.Now;
        }
        public void SetUnidadeId(Guid id)
        {
            UnidadeId = id;
        }

        public void SetTurmaId(Guid id)
        {
            TurmaId = id;
        }

        public void SetSalaId(Guid id)
        {
            SalaId = id;
        }

        public void SetDiaAula(DateTime dia)
        {
            DiaAula = dia;
        }

        public void SetHorarios(string horaIni, string horaFinal, DayOfWeek diaSemana)
        {
            //HoraInicial = horaIni;
            //HoraFinal = horaFinal;
            //var dia = DiaDaSemana.TryParse(diaSemana);
            //DiaDaSemana = dia.DisplayName;// .T diaSemana;
        }

        public void SetFalseIniAula()
        {
            AulaIniciada = false;
            AulaConcluida = false;
        }
    }
    //public List<DateTime> GerarDias(DateTime startDate, DateTime endDate, string diaSemanaUm, string diaSemanaDois)
    //{


    //    //DayOfWeek weekDois;// = new DayOfWeek();
    //    //DayOfWeek.TryParse(newCurso.dia1, out weekUm);
    //    var datas = new List<DateTime>();

    //    if (endDate < startDate)
    //        throw new ArgumentException("endDate must be greater than or equal to startDate");

    //    while (startDate <= endDate)
    //    {
    //        datas.Add(startDate);
    //        startDate = startDate.AddDays(1);
    //    }

    //    if (diaSemanaDois == "" || diaSemanaDois == null)
    //    {
    //        DayOfWeek week;// = new DayOfWeek();
    //        DayOfWeek.TryParse(diaSemanaUm, out week);
    //        return datas.Where(d => d.DayOfWeek == week).ToList();
    //    }
    //    else
    //    {
    //        DayOfWeek week1;// = new DayOfWeek();
    //        DayOfWeek.TryParse(diaSemanaUm, out week1);
    //        DayOfWeek week2;// = new DayOfWeek();
    //        DayOfWeek.TryParse(diaSemanaDois, out week2);

    //        return datas.Where(d => d.DayOfWeek == week1 || d.DayOfWeek == week2).ToList();
    //    }

    //}

    //public void SetDatas(List<DateTime> datas, List<Calendario> calendarios)
    //{
    //    foreach (var data in datas)
    //    {
    //        calendarios.Add(new Calendario() { DiaAula = data });
    //    }
    //}

    //public void SetSala(int salaId, List<Calendario> calendarios)
    //{
    //    foreach (var calendario in calendarios)
    //    {
    //        calendario.SalaId = salaId;
    //    }
    //}

    //public void SetTurma(string turma, int turmaId, List<Calendario> calendarios)
    //{
    //    foreach (var calendario in calendarios)
    //    {
    //        //calendario.Turma = turma;
    //        calendario.TurmaId = turmaId;
    //    }
    //}

    //public void SetUnidadeID(int unidadeId, List<Calendario> calendarios)
    //{
    //    foreach (var calendario in calendarios)
    //    {
    //        calendario.UnidadeId = unidadeId;
    //    }
    //}

    //public Turno DefineTurno(TimeSpan inicio, TimeSpan fim)
    //{
    //    Turno turno = new Turno();
    //    TimeSpan hora1 = new TimeSpan(13, 0, 0);
    //    TimeSpan hora2 = new TimeSpan(18, 0, 0);
    //    //TimeSpan hora3 = new TimeSpan(9, 30, 0);

    //    if (inicio < hora1) /// inicia manha
    //    {

    //        if (fim <= hora1) // termina manha
    //        {
    //            turno = Turno.manha;
    //        }
    //        else if (fim > hora1 && fim <= hora2)// termina antes ou igual 18
    //        {
    //            turno = Turno.IntegralManhaTarde;
    //        }

    //        if (fim > hora2)
    //        {
    //            turno = Turno.IntegralManhaTardeNoite;
    //        }

    //        //termina depois das 18
    //    }
    //    else if (inicio >= hora1 && inicio <= hora2)// inicia tarde
    //    {
    //        if (fim <= hora2) // termina a tarde
    //        {
    //            turno = Turno.tarde;
    //        }
    //        else
    //        {
    //            turno = Turno.IntegralTardeNoite;
    //        }

    //    }

    //    if (inicio >= hora2)
    //    {
    //        turno = Turno.noite;
    //    }

    //    return turno;

    //}

    //public void SetHorariosAndTurnos(List<Calendario> calendarios, string dia1, string dia2, string horarioIni1, string horarioIni2,
    //    string horarioFim1, string horarioFim2)
    //{
    //    // pegar dias da semana
    //    if (dia2 == null)
    //    {
    //        DayOfWeek week;// = new DayOfWeek();
    //        DayOfWeek.TryParse(dia1, out week);

    //        ///var abc = "09:30";
    //        TimeSpan timeIni;
    //        TimeSpan.TryParse(horarioIni1, out timeIni);

    //        TimeSpan timeFim;
    //        TimeSpan.TryParse(horarioFim1, out timeFim);

    //        Turno turno = DefineTurno(timeIni, timeFim);

    //        foreach (var item in calendarios)
    //        {
    //           // item.Turno = turno;
    //            item.HoraInicial = horarioIni1;
    //            item.HoraFinal = horarioFim1;
    //            item.DiaDaSemana = week;
    //        }


    //    }
    //    else
    //    {
    //        DayOfWeek week1;// = new DayOfWeek();
    //        DayOfWeek.TryParse(dia1, out week1);

    //        DayOfWeek week2;// = new DayOfWeek();
    //        DayOfWeek.TryParse(dia2, out week2);

    //        ///var abc = "09:30";
    //        TimeSpan timeIni1;
    //        TimeSpan.TryParse(horarioIni1, out timeIni1);
    //        TimeSpan timeFim1;
    //        TimeSpan.TryParse(horarioFim1, out timeFim1);
    //        Turno turno1 = DefineTurno(timeIni1, timeFim1);

    //        TimeSpan timeIni2;
    //        TimeSpan.TryParse(horarioIni2, out timeIni2);
    //        TimeSpan timeFim2;
    //        TimeSpan.TryParse(horarioFim2, out timeFim2);
    //        Turno turno2 = DefineTurno(timeIni2, timeFim2);

    //        foreach (var item in calendarios)
    //        {
    //            // get DiaAula

    //            if (item.DiaAula.DayOfWeek == week1)
    //            {
    //                //item.Turno = turno1;
    //                item.HoraInicial = horarioIni1;
    //                item.HoraFinal = horarioFim1;
    //                item.DiaDaSemana = week1;
    //            }
    //            else
    //            {
    //                //item.Turno = turno2;
    //                item.HoraInicial = horarioIni2;
    //                item.HoraFinal = horarioFim2;
    //                item.DiaDaSemana = week2;
    //            }
    //        }
    //    }
    //}
}

/*
 manha,
    tarde,
    noite,
    sabado,
    domingo,
    IntegralManhaTarde,
    IntegralManhaTardeNoite,
    IntegralTardeNoite
 */
//}
