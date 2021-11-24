using Invictus.Core;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.ContratoAggregate;
using System;
using System.Collections.Generic;

namespace Invictus.Domain.Administrativo.ContratosAggregate
{
    public class Contrato : Entity, IAggregateRoot
    {       
        public Contrato(
                        Guid typePacoteId,
                        string titulo,
                        bool ativo,   
                        bool podeEditar,
                        string observacao)
        {
            TypePacoteId = typePacoteId;
            Titulo = titulo;
            Ativo = ativo;
            PodeEditar = podeEditar; 
            Observacao = observacao;
            Conteudos = new List<Conteudo>();
        }

        
        public string CodigoContrato { get; private set; }
        public Guid TypePacoteId { get; private set; }
        public string Titulo { get; private set; }
        public bool PodeEditar { get; private set; }
        public string Observacao { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public List<Conteudo> Conteudos { get; private set; }

        public void CreateCodigoContrato(int totalContrato)
        {
            var total = totalContrato + 1;
            var totalChars = total.ToString().Length;
            var length = 8;
            var numeracaoToString = "";
            for (int i = 0; i < length - totalChars; i++)
            {
                numeracaoToString += "0";
            }

            numeracaoToString += total.ToString();

            CodigoContrato = numeracaoToString;

        }

        public void AddConteudos(string conteudo)
        {
            Conteudos = new List<Conteudo>();

            decimal length = conteudo.Length;
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

                var content = new Conteudo(i, conteudo.Substring(ini, 7999));
                content.SetContratoId(Id);
                conteudoList.Add(content);

            }

            if (resto > 0)
            {
                var ini = loops * 7999;
                var end = (length - 1) - ini;

                var content = new Conteudo(Convert.ToInt32(loops) + 1, conteudo.Substring(Convert.ToInt32(ini), Convert.ToInt32(end)));
                content.SetContratoId(Id);

                conteudoList.Add(content);

            }

            Conteudos.AddRange(conteudoList);

        }

        public void SetDataCriacao()
        {
            DataCriacao = DateTime.Now;
        }

        #region EF
        public Contrato() { }

        #endregion

    }

}
