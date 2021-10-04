using Invictus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.TurmaAggregate
{
    public class HorarioBase : ValueObject
    {
        public HorarioBase() { }

        public HorarioBase(//bool ehFinaldeSemana,
                            string weekDayOne,
                            string initialHourOne,
                            string finalHourOne,
                            string weekDayTwo,
                            string initialHourTwo,
                            string finalHourTwo
                            //string weekDayThree,
                            //string initialHourThree,
                            //string finalHourThree
                            )
        {
            //EhFinalDeSemana = ehFinaldeSemana;
            WeekDayOne = weekDayOne;
            InitialHourOne = initialHourOne;
            FinalHourOne = finalHourOne;
            WeekDayTwo = weekDayTwo;
            InitialHourTwo = initialHourTwo;
            FinalHourTwo = finalHourTwo;
            //WeekDayThree = weekDayThree;
            //InitialHourThree = initialHourThree;
            //FinalHourThree = finalHourThree;
        }

        //public bool EhFinalDeSemana { get; private set; }
        public string WeekDayOne { get; private set; }
        public string InitialHourOne { get; private set; }
        public string FinalHourOne { get; private set; }
        public string WeekDayTwo { get; private set; }
        public string InitialHourTwo { get; private set; }
        public string FinalHourTwo { get; private set; }
        // public string WeekDayThree { get; private set; }
        //public string InitialHourThree { get; private set; }
        // public string FinalHourThree { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            //yield return EhFinalDeSemana;
            yield return WeekDayOne;
            yield return InitialHourOne;
            yield return FinalHourOne;
            yield return WeekDayTwo;
            yield return InitialHourTwo;
            yield return FinalHourTwo;
            //yield return WeekDayThree;
            //yield return InitialHourThree;
            //yield return FinalHourThree;
        }
    }
}
