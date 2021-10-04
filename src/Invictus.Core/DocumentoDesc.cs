using System.ComponentModel;

namespace Invictus.Core
{
    public enum DocumentoDesc
    {
        [Description("Seguro acidentes pessoais")]
        AP,
        [Description("Cartão vacinação")]
        CV,
        [Description("Tipo sanguíneo")]
        TP,
        [Description("HCG")]
        HC
    }
}
