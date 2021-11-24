using Invictus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo.TurmaAggregate
{
    public class Previsoes : Entity
    {
        public Previsoes() { }
        public Previsoes(
                         DateTime previsionStartOne,
                         DateTime previsionStartTwo,
                         DateTime previsionStartThree,
                         DateTime previsionEndingOne,
                         DateTime previsionEndingTwo,
                         DateTime previsionEndingThree,
                         Guid turmaId)
        {
           
            PrevisionStartOne = previsionStartOne;
            PrevisionStartTwo = previsionStartTwo;
            PrevisionStartThree = previsionStartThree;
            PrevisionEndingOne = previsionEndingOne;
            PrevisionEndingTwo = previsionEndingTwo;
            PrevisionEndingThree = previsionEndingThree;
            TurmaId = turmaId;
        }
       
        public DateTime PrevisionStartOne { get; private set; }       
        public DateTime PrevisionStartTwo { get; private set; }       
        public DateTime PrevisionStartThree { get; private set; }               
        public DateTime PrevisionEndingOne { get; private set; }        
        public DateTime PrevisionEndingTwo { get; private set; }      
        public DateTime PrevisionEndingThree { get; private set; }
        public Guid TurmaId { get; private set; }

    }

}
