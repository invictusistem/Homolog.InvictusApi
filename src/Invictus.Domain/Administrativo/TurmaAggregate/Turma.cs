using Invictus.Core;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.Calendarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.TurmaAggregate
{
    public class Turma : Entity, IAggregateRoot
    {
        public Turma() { }
        public Turma(
                    string descricao,
                    int totalAlunos,
                    int minimoAlunos,
                    Guid unidadeId, 
                    Guid salaId,
                    Guid pacoteId,
                    Guid typePacoteId, 
                    Previsao previsao)
        {
           
           // Identificador = identificador;
            Descricao = descricao;
            TotalAlunos = totalAlunos;
            MinimoAlunos = minimoAlunos;
            UnidadeId = unidadeId;
            SalaId = salaId;
            PacoteId = pacoteId;
            TypePacoteId = typePacoteId;
            Previsao = previsao;
            //StatusAndamento = statusAndamento.DisplayName;
            Materias = new List<TurmaMaterias>();
            Horarios = new List<Horario>();
        }

        public string Identificador { get; private set; }
        public string Descricao { get; private set; }
        public int TotalAlunos { get; private set; }
        public int MinimoAlunos { get; private set; }
        public string StatusAndamento { get; private set; }
        public Guid UnidadeId { get; private set; }
        public Guid SalaId { get; private set; }
        public Guid PacoteId { get; private set; }  
        public Guid TypePacoteId { get; private set; }
        public Previsao Previsao { get; private set; }
        public List<Horario> Horarios { get; private set; }
        public List<TurmaMaterias> Materias { get; private set; }

        public void CreateIdentificador(int turmasExistentes, string siglaUnidade)
        {
            int totalCaracteres = 6;

            //var length = turmasExistentes.ToString().Length;// ; ToString().Length

            int numeroTurma = turmasExistentes + 1;

            var identificador = $"{siglaUnidade} ";

            for (int i = 0; i < totalCaracteres - numeroTurma.ToString().Length; i++)
            {
                identificador += "0";
            }

            identificador += Convert.ToString(numeroTurma);

            Identificador = identificador;
        }

        public void SetStatusAndamentoInitial()
        {
            StatusAndamento = StatusTurma.AguardandoInicio.DisplayName;
        }
        public void AddPrevisao(DateTime previAtual, 
                                DateTime previTerminoAtual)
        {
            var date = DateTime.Now;
            var previ = new Previsao(previAtual, previTerminoAtual, "1ª previsão",date);

            Previsao = previ;
        }

        public void AddHorarios(IEnumerable<Horario> horarios)
        {
            Horarios.AddRange(horarios);
        }

        public void AddMaterias(IEnumerable<TurmaMaterias> materias)
        {
            Materias.AddRange(materias);
        }

        public void AddAlunoNaTurma()
        {
            TotalAlunos++;
        }

        public void RemoveAlunoFromTurma()
        {
            TotalAlunos--;
        }

       /*
        public void CreateCalendarioDaTurma(
            DateTime startDate, DateTime endDate
            //string diaSemanaUm, string diaSemanaDois,
            //string horarioIni1, string horarioFim1,
            //string horarioIni2, string horarioFim2,
            //List<DataFeriado> feriados
            )
        {

            

            var datas = GerarDias(startDate, endDate, diaSemanaUm, diaSemanaDois, feriados);
            var calendarios = new List<Calendario>();

            SetDatas(datas, calendarios);

            SetSalaId(SalaId, calendarios);
            //SetTurmaId(TurmaId, calendarios);
            SetUnidadeId(UnidadeId, calendarios);

            SetHorariosAndTurnos(calendarios,
                diaSemanaUm, diaSemanaDois,
                horarioIni1, horarioIni2,
                horarioFim1, horarioFim2);

            SetFalseTurmaIniciada(calendarios);

            Calendarios = calendarios;
            //return calendarios;
        }      

        public void SetFalseTurmaIniciada(List<Calendario> calendarios)
        {
            foreach (var item in calendarios)
            {
                item.SetFalseIniAula();
            }

        }
        public void SetHorariosAndTurnos(List<Calendario> calendarios,
            string dia1, string dia2,
            string horarioIni1, string horarioIni2,
            string horarioFim1, string horarioFim2)
        {
            // pegar dias da semana
            if (dia2 == null)
            {
                //DayOfWeek week;// = new DayOfWeek();
                //DayOfWeek.TryParse(dia1, out week);
                //DayOfWeek week;// = new DayOfWeek();
                //DayOfWeek.TryParse(diaSema naUm, out week);
                DayOfWeek week = DiaDaSemana.TryParseToDayofWeek(dia1);

                ///var abc = "09:30";
                TimeSpan timeIni;
                TimeSpan.TryParse(horarioIni1, out timeIni);

                TimeSpan timeFim;
                TimeSpan.TryParse(horarioFim1, out timeFim);

                //Turno turno = DefineTurno(timeIni, timeFim);

                foreach (var item in calendarios)
                {
                    //item.Turno = turno;
                    //item.HoraInicial = horarioIni1;
                    //item.HoraFinal = horarioFim1;
                    //item.DiaDaSemana = week;
                    item.SetHorarios(horarioIni1, horarioFim1, week);
                }


            }
            else
            {
                //DayOfWeek week1;// = new DayOfWeek();
                //DayOfWeek.TryParse(dia1, out week1);
                DayOfWeek week1 = DiaDaSemana.TryParseToDayofWeek(dia1);

                //DayOfWeek week2;// = new DayOfWeek();
                //DayOfWeek.TryParse(dia2, out week2);
                DayOfWeek week2 = DiaDaSemana.TryParseToDayofWeek(dia2);

                ///var abc = "09:30";
                TimeSpan timeIni1;
                TimeSpan.TryParse(horarioIni1, out timeIni1);
                TimeSpan timeFim1;
                TimeSpan.TryParse(horarioFim1, out timeFim1);
                // Turno turno1 = DefineTurno(timeIni1, timeFim1);

                TimeSpan timeIni2;
                TimeSpan.TryParse(horarioIni2, out timeIni2);
                TimeSpan timeFim2;
                TimeSpan.TryParse(horarioFim2, out timeFim2);
                //Turno turno2 = DefineTurno(timeIni2, timeFim2);

                foreach (var item in calendarios)
                {
                    // get DiaAula

                    if (item.DiaAula.DayOfWeek == week1)
                    {
                        //item.Turno = turno1;
                        //item.HoraInicial = horarioIni1;
                        //item.HoraFinal = horarioFim1;
                        //item.DiaDaSemana = week1;
                        item.SetHorarios(horarioIni1, horarioFim1, week1);
                    }
                    else
                    {
                        //item.Turno = turno2;
                        //item.HoraInicial = horarioIni2;
                        //item.HoraFinal = horarioFim2;
                        //item.DiaDaSemana = week2;
                        item.SetHorarios(horarioIni1, horarioFim1, week2);
                    }
                }
            }
        }

        public void SetUnidadeId(int unidadeId, List<Calendario> calendarios)
        {
            foreach (var calendario in calendarios)
            {
                //calendario..UnidadeId = unidadeId;
                calendario.SetUnidadeId(unidadeId);
            }
        }

        public void SetSalaId(int salaId, List<Calendario> calendarios)
        {
            foreach (var calendario in calendarios)
            {
                // calendario.SalaId = sala;
                calendario.SetSalaId(salaId);
            }
        }

        public void SetDatas(List<DateTime> datas, List<Calendario> calendarios)
        {
            foreach (var data in datas)
            {
                var calend = new Calendario();
                calend.SetDiaAula(data);
                calendarios.Add(calend);
            }
        }

        private List<DateTime> GerarDias(DateTime startDate, DateTime endDate, string diaSemanaUm, string diaSemanaDois, List<DataFeriado> feriados)
        {
            //DayOfWeek weekDois;// = new DayOfWeek();
            //DayOfWeek.TryParse(newCurso.dia1, out weekUm);
            endDate = endDate.AddMonths(1);
            var datas = new List<DateTime>();

            if (endDate < startDate)
                throw new ArgumentException("endDate must be greater than or equal to startDate");

            while (startDate <= endDate)
            {
                if (filter(startDate, feriados))
                {
                    datas.Add(startDate);
                }

                startDate = startDate.AddDays(1);
            }

            if (diaSemanaDois == "" || diaSemanaDois == null)
            {
                //DayOfWeek week;// = new DayOfWeek();
                //DayOfWeek.TryParse(diaSema naUm, out week);
                DayOfWeek week = DiaDaSemana.TryParseToDayofWeek(diaSemanaUm);
                return datas.Where(d => d.DayOfWeek == week).ToList();
                //



            }
            else
            {
                //DayOfWeek week1;// = new DayOfWeek();
                //DayOfWeek.TryParse(diaSemanaUm, out week1);
                DayOfWeek week1 = DiaDaSemana.TryParseToDayofWeek(diaSemanaUm);
                //DayOfWeek week2;// = new DayOfWeek();
                //DayOfWeek.TryParse(diaSemanaDois, out week2);
                DayOfWeek week2 = DiaDaSemana.TryParseToDayofWeek(diaSemanaDois);

                return datas.Where(d => d.DayOfWeek == week1 || d.DayOfWeek == week2).ToList();
            }

        }
        */
        
        

        public void IniciarTurma()
        {
            StatusAndamento = StatusTurma.EmAndamento.DisplayName;
           // PrevisaoInfo = "Em andamento";
           // Iniciada = true;

        }

       
        

    }

    public class DataFeriado
    {
        public int dia { get; set; }
        public int mes { get; set; }
    }
}
