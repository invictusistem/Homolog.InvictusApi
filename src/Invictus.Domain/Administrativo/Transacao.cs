using Invictus.Core;
using Invictus.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Administrativo
{
    public class Transacao : Entity
    {
        //public TipoTrans Tipo { get; set; }
        public string Tipo { get; set; }

        public Transacao()
        {

        }
    }


    public enum TipoTrans
    {
        [Description("Cartão de Crédito")]
        Cartao,

        [Description("Débito")]
        Debito
    }
}
