using Invictus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain
{
    public class TestandoEnum
    {
        public TestandoEnum() { }
        public TestandoEnum(string descricao, CardType cardtype)
        {
            Descricao = descricao;
            Cardtype = cardtype.DisplayName;
        }
        public string Descricao { get; private set; }
        public string Cardtype { get; private set; }
    }

    public class CardType
    : Enumeration
    {
        public static CardType Amex = new(1, "American Express");
        public static CardType Visa = new(2, nameof(Visa));
        public static CardType MasterCard = new(3, nameof(MasterCard));
        public CardType()
        {

        }
        public CardType(int id, string name) : base(id, name) { }


    }

    
}
