﻿using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication.Interfaces
{
    public interface IColaboradorApplication
    {
        Task SaveColaborador(ColaboradorDto newColaborador);
        Task EditColaborador(ColaboradorDto colaborador);
    }
}
