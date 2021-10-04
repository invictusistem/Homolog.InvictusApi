using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core
{
    public enum Turno
    {
        [Description("Manhã")]
        manha,
        tarde,
        noite,
        sabado,
        domingo,
        IntegralManhaTarde,
        IntegralManhaTardeNoite,
        IntegralTardeNoite
    }

    public enum DiaSemana
    {
        segunda,
        terca,
        quarta,
        quinta,
        sexta
    }
}


