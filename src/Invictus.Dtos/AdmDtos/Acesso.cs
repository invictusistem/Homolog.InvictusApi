using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Dtos.AdmDtos
{
    public class Acesso
    {
        public IEnumerable<Setor> financeiro { get; set; }
    }

    public class Setor
    {
        public IEnumerable<Aba> configuracoes { get; set; }
    }

    public class Aba
    {
        public IEnumerable<Tela> bancos { get; set; }
    }

    public class Tela
    {

    }
}



/*
`{
    "financeiro":[
      "configuracoes":[
        "bancos":[
          "create":true,
          "edit": false
        ]
      ]
    ],
    "formarecebimento":
    }`

*/
