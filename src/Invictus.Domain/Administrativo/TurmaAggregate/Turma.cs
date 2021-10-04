using Invictus.Core;
using Invictus.Core.Enums;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.PacoteAggregate;
using Invictus.Domain.Administrativo.UnidadeAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Invictus.Domain.Administrativo.TurmaAggregate
{
    public class Turma : IAggregateRoot
    {
        public Turma() { }
        public Turma(
                    int id,
                    string identificador,
                    string descricao,
                    //string modulo,
                    int moduloId,
                    Previsao previsoes,
                   // int vagas,
                    int totalAlunos,
                    int semetreAtual,
                    int minimoAlunos,
                    Status statusDaTurma,
                    bool iniciada,
                    //Turno turno,
                    HorarioBase horarios,
                    string previsao,
                    DateTime previsaoAtual,
                    DateTime previsaoTerminoAtual
                )
        {
            Id = id;
            Identificador = identificador;
            Descricao = descricao;
            //Modulo = modulo;
            ModuloId = moduloId;
            Previsoes = previsoes;
            //Vagas = vagas;
            TotalAlunos = totalAlunos;
            SemetreAtual = semetreAtual;
            MinimoAlunos = minimoAlunos;
            StatusDaTurma = statusDaTurma.DisplayName;
            Iniciada = iniciada;
            //Turno = turno;
            Horarios = horarios;
            Previsao = previsao;
            PrevisaoAtual = previsaoAtual;
            PrevisaoTerminoAtual = previsaoTerminoAtual;
            Calendarios = new List<Calendario>();
        }

        public int Id { get; private set; }
        public string Identificador { get; private set; }
        public string Descricao { get; private set; }
        //public string Modulo { get; private set; }
        public int ModuloId { get; private set; }
        //public int Vagas { get; private set; }
        public int TotalAlunos { get; private set; }
        public int SemetreAtual { get; private set; }
        public int MinimoAlunos { get; private set; }
        //public Status Status { get; private set; }
        public string StatusDaTurma { get; private set; }
        // public Unidades Unidade { get; private set; }
        public int UnidadeId { get; private set; }
        public bool Iniciada { get; private set; }
        // public Turno Turno { get; private set; }
        public DateTime PrevisaoAtual { get; private set; }
        public DateTime PrevisaoTerminoAtual { get; private set; }
        public string Previsao { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public int SalaId { get; private set; }
        public int PlanoPagamentoId { get; private set; }
        public int PacoteId { get; private set; }
        public virtual Previsao Previsoes { get; private set; }
        public HorarioBase Horarios { get; private set; }
        //public PlanoPagamento PlanoPgm { get; private set; }
        public List<Calendario> Calendarios { get; private set; }


        public void SetPlanoPagamento(int planoPagamentoId)
        {
            PlanoPagamentoId = planoPagamentoId;
        }
        /*
        public void Factory(Modulo modulo, int vagas, int minAlunos, int unidadeId, string siglaUnidade, int turmasExistentes,
            DateTime prevIni1, DateTime prevIni2, DateTime prevIni3,
            string dia1, string dia2, string horarioIni1, string horarioFim1, int salaId)
        {   
            Identificador = GerarIdentificador(siglaUnidade, turmasExistentes);
            Descricao = modulo.Descricao;
            ModuloId = modulo.Id;
            //Vagas = vagas;
            TotalAlunos = 0;
            StatusDaTurma = Status.AguardandoInicio.DisplayName;
            //Unidade = unidade;
            UnidadeId = unidadeId;
            MinimoAlunos = minAlunos;
            Iniciada = false;
            //Turno = turno;
            SemetreAtual = 1;
            
            //Modulo = moduloDescricao;
            PrevisaoAtual = prevIni1;
            PrevisaoTerminoAtual = prevIni1.AddMonths(modulo.DuracaoMeses);
            Previsao = "1ª previsão";
            //PrevisaoTerminoAtual = prevIni1.AddMonths(20);
            

            SalaId = salaId;

            //Previsoes = PrevisoesFactory(
            //    prevIni1, prevIni2, prevIni3, 
            //    modulo.DuracaoMeses);

            Horarios = CreateHorariosTurma(prevIni1, dia1, dia2, horarioIni1, horarioFim1);

            Calendarios = CalendarioFactory(prevIni1,Previsoes.PrevisionEndingThree,dia1, dia2, horarioIni1, horarioFim1);
            
        }
        */

        public void FactoryInitialValuesOne(Unidade unidade, int turmasExistentes, Sala sala, int minimoAulnos, int pacoteId)
        {
            Identificador = GerarIdentificador(unidade.Sigla, turmasExistentes);
            //Descricao = modulo.Descricao;
            //Vagas = sala.Capacidade;// vagas;
            TotalAlunos = 0;
            StatusDaTurma = Status.AguardandoInicio.DisplayName;
            //Unidade = unidade;
            UnidadeId = unidade.Id;// unidadeId;
            MinimoAlunos = minimoAulnos;
            Iniciada = false;
            //Turno = turno;
            SemetreAtual = 1;
            Previsao = "1ª previsão";
            DataCriacao = DateTime.Now;
            //Modulo = moduloDescricao;
            //PrevisaoAtual = prevIni1;
            //PrevisaoTerminoAtual = prevIni1.AddMonths(modulo.DuracaoMeses);
            //PrevisaoTerminoAtual = prevIni1.AddMonths(20);
            //ModuloId = modulo.Id;

            SalaId = sala.Id;// salaId;

            PacoteId = pacoteId;
            //Previsoes = PrevisoesFactory(prevIni1, prevIni2, prevIni3, modulo.DuracaoMeses);

            //Horarios = CreateHorariosTurma(prevIni1, dia1, dia2, horarioIni1, horarioFim1);

            //Calendarios = CalendarioFactory(prevIni1, Previsoes.PrevisionEndingThree, dia1, dia2, horarioIni1, horarioFim1);

        }

        public void FactoryInitialValuesTwo(
            Pacote modulo, 
            DateTime prevIni1,DateTime prevIni2, DateTime prevIni3,
            DateTime prevTerm1, DateTime prevTerm2, DateTime prevTerm3,
            string descricao)
        {

            Descricao = descricao ;// modulo.Descricao;
            PrevisaoAtual = prevIni1;
            PrevisaoTerminoAtual = prevIni1.AddMonths(modulo.DuracaoMeses);
            ModuloId = modulo.Id;
            Previsoes = PrevisoesFactory(prevIni1, prevIni2, prevIni3, 
                prevTerm1, prevTerm2, prevTerm3);

            //Horarios = CreateHorariosTurma(prevIni1, dia1, dia2, horarioIni1, horarioFim1);

            //Calendarios = CalendarioFactory(prevIni1,
            //    Previsoes.PrevisionEndingThree, dia1, dia2, horarioIni1, horarioFim1);

        }

        public void CreateCalendarioDaTurma(
            DateTime startDate, DateTime endDate, 
            string diaSemanaUm, string diaSemanaDois, 
            string horarioIni1, string horarioFim1,
            string horarioIni2, string horarioFim2, 
            List<DataFeriado> feriados)
        {
            var datas = GerarDias(startDate, endDate, diaSemanaUm, diaSemanaDois, feriados);
            var calendarios = new List<Calendario>();

            SetDatas(datas, calendarios);
            
            SetSalaId(SalaId, calendarios);
            //SetTurmaId(TurmaId, calendarios);
            SetUnidadeId(UnidadeId, calendarios);
            
            SetHorariosAndTurnos(calendarios, 
                diaSemanaUm, diaSemanaDois, 
                horarioIni1,horarioIni2,
                horarioFim1,horarioFim2);
            
            SetFalseTurmaIniciada(calendarios);
            
            Calendarios = calendarios;
            //return calendarios;
        }
        public void CreateHorariosDaTurma(DateTime previIniOne,
            string dia1, string dia2, string horarioIni1, string horarioFim1)
        {
            // TODO: refact verificação do turno se sabado ou domingo

            if (previIniOne.DayOfWeek.ToString() == Turno.sabado.ToString() || previIniOne.DayOfWeek.ToString() == Turno.domingo.ToString())
            {

                var horarios =  new HorarioBase(dia1, horarioIni1, horarioFim1, null, null, null);
                Horarios = horarios;

                //horarios.WeekDayOne = horarioFDS1;
            }
            else
            {
                var horarios = new HorarioBase(dia1, horarioIni1, horarioFim1, dia2, null, null);
                Horarios = horarios;
            }


            //return horarios;
        }

        /*
        private List<Calendario> CalendarioFactory(DateTime startDate, DateTime endDate, string diaSemanaUm, string diaSemanaDois,string horarioIni1, string horarioFim1)
        {
            var datas = GerarDias(startDate, endDate, diaSemanaUm, diaSemanaDois);
            var calendarios = new List<Calendario>();

            SetDatas(datas,calendarios);
            SetSalaId(SalaId, calendarios);
            //SetTurma(turma.Identificador, turma.Id, calendarios);
            SetUnidadeId(UnidadeId, calendarios);
            SetHorariosAndTurnos(calendarios, diaSemanaUm, diaSemanaDois, horarioIni1, null,
                horarioFim1, null);
            return calendarios;
        }
        */

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

        //public void SetTurma(string turma, int turmaId, List<Calendario> calendarios)
        //{
        //    foreach (var calendario in calendarios)
        //    {
        //        //calendario.Turma = turma;
        //        calendario.TurmaId = turmaId;
        //    }
        //}

        public void SetSalaId(int salaId, List<Calendario> calendarios)
        {
            foreach (var calendario in calendarios)
            {
                // calendario.SalaId = sala;
                calendario.SetSalaId(salaId);
            }
        }

        public void SetTurmaId(int turmaId, List<Calendario> calendarios)
        {
            foreach (var calendario in calendarios)
            {
                // calendario.SalaId = sala;
                calendario.SetTurmaId(turmaId);
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

        public bool filter(DateTime datetime, List<DataFeriado> feriados)
        {
            bool match = false;

            foreach (var item in feriados)
            {
                var tem = (item.dia == datetime.Day) & (item.mes == datetime.Month);
                match = tem;
                if (tem == true) break;
            }

            return !match;
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
        private string GerarIdentificador(/*Unidades unidade*/string siglaUnidade, int turmasExistentes)
        {
            int totalCaracteres = 4;

            var length = turmasExistentes.ToString().Length;// ; ToString().Length

            int numeroTurma = turmasExistentes + 1;

            var identificador = $"{siglaUnidade} ";

            for (int i = 0; i < totalCaracteres - numeroTurma.ToString().Length; i++)
            {
                identificador += "0";
            }

            identificador += Convert.ToString(numeroTurma);

            return identificador;            
        }

        private Previsao PrevisoesFactory(
            DateTime prevIni1, DateTime prevIni2, DateTime prevIni3,
            DateTime prevTerm1, DateTime prevTerm2, DateTime prevTerm3)
        {
            var prev = new Previsao(prevIni1, prevIni2, prevIni3, prevTerm1, prevTerm2, prevTerm3);

            return prev;
        }

        /*
        private HorarioBase CreateHorariosTurma(DateTime previIniOne, string dia1, string dia2, string horario1, string horarioFim1)
        {

            // TODO: refact verificação do turno se sabado ou domingo

            if (previIniOne.DayOfWeek.ToString() == Turno.sabado.ToString() || previIniOne.DayOfWeek.ToString() == Turno.domingo.ToString())
            {

                return new HorarioBase(dia1, horario1, horarioFim1, null, null, null);

                //horarios.WeekDayOne = horarioFDS1;
            }
            else
            {
                return new HorarioBase(dia1, horario1, horarioFim1, dia2, null, null);
                //return horarios;
            }


            //return horarios;
        }
        */

        public void AbrirTurma(DateTime previsao_inicio_turmas, int previsao_minima_alunos, string curso)
        {
            DateTime abertura_inscricoes = DateTime.Now.AddDays(1);
            //string curso
        }

        public void IniciarTurma()
        {
            StatusDaTurma = Status.EmAndamento.DisplayName;
            Previsao = "Em andamento";
            Iniciada = true;

        }

        public void AdiarInicio(string prevAtual, Previsao previsoes)
        {
            if (prevAtual == "1ª previsão")
            {
                Previsao = "2ª previsão";
                PrevisaoAtual = previsoes.PrevisionStartTwo;

            }
            else if (prevAtual == "2ª previsão")
            {
                Previsao = "3ª previsão";
                PrevisaoAtual = previsoes.PrevisionStartThree;

            }
            else if (prevAtual == "3ª previsão")
            {
                //throw new NotImplementedException();
            }

        }

        public void AddAlunoNaTurma()
        {
            TotalAlunos++;
        }

        public void RemoveAlunoFromTurma()
        {
            TotalAlunos--;
        }

        //public IEnumerable<Tuple<int, int, int>> SetProfessoresTurma(int[] professorId, int turmaId)
        //{
        //    List<Tuple<int, int, int>> professores = new List<Tuple<int, int, int>>();
        //    foreach (var item in professorId)
        //    {
        //        professores.Add(new Tuple<int, int, int>(0, item, turmaId));
        //    }

        //    return professores;
        //}
    }

    public class DataFeriado
    {
        public int dia { get; set; }
        public int mes { get; set; }
    }
}
