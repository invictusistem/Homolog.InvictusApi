using Invictus.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Invictus.Domain.Administrativo.ContratosAggregate
{
    public class Contrato : IAggregateRoot
    {
        public Contrato() { }
        public Contrato(//long id,
                        int totalContractsInDataBase,
                        //int unidadeId,
                        int pacoteId,
                        string titulo,
                        bool ativo,
                        //string conteudoOne,
                        //string conteudoDois,
                        //string conteudoTres,
                        //string conteudoQuatro,
                       // bool podeEditar,
                        string observacao
                       //DateTime dataCriacao
)
        {
            //Id = id;
            CodigoContrato = CreateCodigoContrato(totalContractsInDataBase);// codigoContrato;
          //  UnidadeId = unidadeId;
            PacoteId = pacoteId;
            Titulo = titulo;
            Ativo = ativo;
            PodeEditar = true;
            //ConteudoUm = conteudoOne;
            //ConteudoDois = conteudoDois;
            //ConteudoTres = conteudoTres;
            //ConteudoQuatro = conteudoQuatro;
            //PodeEditar = podeEditar;
            Observacao = observacao;
           // DataCriacao = dataCriacao;
            
           // DataCriacao = SetDataCriacao();
            Conteudos = new List<Conteudo>();
        }

        public long Id { get; private set; }
        public string CodigoContrato { get; private set; }
       // public int UnidadeId { get; private set; }
        public int PacoteId { get; private set; }
        public string Titulo { get; private set; }
        //public string ConteudoUm { get; private set; }
        //public string ConteudoDois { get; private set; }
        //public string ConteudoTres { get; private set; }
        //public string ConteudoQuatro { get; private set; }
        public List<Conteudo> Conteudos { get; private set; }
        public bool PodeEditar { get; private set; }
        public string Observacao { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public string CreateCodigoContrato(int totalContrato)
        {
            var total = totalContrato + 1;
            var totalChars = total.ToString().Length;
            var length = 5;
            var numeracaoToString = "";
            for (int i = 0; i < length - totalChars; i++)
            {
                numeracaoToString += "0";
            }

            numeracaoToString += total.ToString();

            return  numeracaoToString;

        }

        public void EditContrato(string titulo, bool ativo)
        {
            Titulo = titulo;
            Ativo = ativo;

        }

        public void EditConteudo(string conteudo)
        {
            Conteudos = new List<Conteudo>();
            decimal length = conteudo.Length;

            //decimal qntconteudo = length / 2999;
            //decimal count = _fileParams.ResourceCodes.Count(); // 3063

            var loops = Math.Floor(length / 7999);
            var resto = length - (loops * 7999);

            var conteudoList = new List<Conteudo>();
            for (int i = 1; i <= loops; i++)
            {
                var ini = (i - 1) * 7999;
                var end = 2999 * i;

                conteudoList.Add(new Conteudo(i, conteudo.Substring(ini, 7999)));

            }

            if (resto > 0)
            {
                var ini = loops * 7999;
                var end = (length - 1) - ini;

                conteudoList.Add(new Conteudo(Convert.ToInt32(loops) + 1, conteudo.Substring(Convert.ToInt32(ini), Convert.ToInt32(end))));

            }

            Conteudos.AddRange(conteudoList);

        }

        public void AddConteudos(string conteudo)
        {
            decimal length = conteudo.Length;

            //decimal qntconteudo = length / 2999;
            //decimal count = _fileParams.ResourceCodes.Count(); // 3063

            var loops = Math.Floor(length / 7999);
            var resto = length - (loops * 7999);

            var conteudoList = new List<Conteudo>();
            for (int i = 1; i <= loops; i++)
            {
                var ini = (i - 1) * 7999;
                var end = 2999 * i;

                conteudoList.Add(new Conteudo(i, conteudo.Substring(ini, 7999)));

            }

            if (resto > 0)
            {
                var ini = loops * 7999;
                var end = (length - 1) - ini;

                conteudoList.Add(new Conteudo(Convert.ToInt32(loops) + 1, conteudo.Substring(Convert.ToInt32(ini), Convert.ToInt32(end))));

            }

            Conteudos.AddRange(conteudoList);

           
        }

        public DateTime SetDataCriacao()
        {
            return DateTime.Now;
        }

    }
    
}
