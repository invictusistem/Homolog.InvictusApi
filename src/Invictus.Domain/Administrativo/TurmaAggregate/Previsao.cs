using Invictus.Core;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Invictus.Domain.Administrativo.TurmaAggregate
{
    public class Previsao : ValueObject
    {
        public Previsao() { }
        public Previsao(//bool iniciada,
                        //string previsaoAtual,
                         DateTime previsionStartOne,
                         DateTime previsionStartTwo,
                         DateTime previsionStartThree,
                         DateTime previsionEndingOne,
                         DateTime previsionEndingTwo,
                         DateTime previsionEndingThree)
        {
            //Iniciada = iniciada;
            //PrevisaoAtual = previsaoAtual;
            PrevisionStartOne = previsionStartOne;
            PrevisionStartTwo = previsionStartTwo;
            PrevisionStartThree = previsionStartThree;
            PrevisionEndingOne = previsionEndingOne;
            PrevisionEndingTwo = previsionEndingTwo;
            PrevisionEndingThree = previsionEndingThree;
        }
       // public bool Iniciada { get; private set; }
        //public string PrevisaoAtual { get; private set; }
        //[JsonConverter(typeof(DateTimeOffsetConverter))]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTime PrevisionStartOne { get; private set; }
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTime PrevisionStartTwo { get; private set; }
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTime PrevisionStartThree { get; private set; }

        // [JsonConverter(typeof(DateTimeOffsetConverter))]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTime PrevisionEndingOne { get; private set; }
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTime PrevisionEndingTwo { get; private set; }
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTime PrevisionEndingThree { get; private set; }
        //[JsonConverter(typeof(DateTimeOffsetConverter))]
        //public DateTime AccomplishedStartTime { get; private set; }
        // [JsonConverter(typeof(DateTimeOffsetConverter))]
        //public DateTime AccomplishedEndTime { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // yield return Iniciada;
            // yield return PrevisaoAtual;
            yield return PrevisionStartOne;
            yield return PrevisionStartTwo;
            yield return PrevisionStartThree;
            yield return PrevisionEndingOne;
            yield return PrevisionEndingTwo;
            yield return PrevisionEndingThree;
        }
    }

}
