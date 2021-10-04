using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Dtos.Financeiro
{
    public class BoletoPJBankReturnDto
    {
        public string id_unico { get; set; }
        public string id_unico_original { get; set; }
        public string status { get; set; }
        public string msg { get; set; }
        public string nossonumero { get; set; }
        public string linkBoleto { get; set; }
        public string linkGrupo { get; set; }
        public string linhaDigitavel { get; set; }
        public string pedido_numero { get; set; }
        public string banco_numero { get; set; }
        public string token_facilitador { get; set; }
        public string credencial { get; set; }

    }

    public class BoletoLotePJBankReturnDto
    {
        public string id_unico { get; set; }
        public string id_unico_original { get; set; }
        public string status { get; set; }
        public string msg { get; set; }
        public string nossonumero { get; set; }
        public string linkBoleto { get; set; }
        public string linkGrupo { get; set; }
        public string linhaDigitavel { get; set; }
        public string pedido_numero { get; set; }
        public string banco_numero { get; set; }
        public string token_facilitador { get; set; }
        public string credencial { get; set; }
        public string vencimento_original { get; set; }
        public string vencimento_atualizado { get; set; }

    }
}
