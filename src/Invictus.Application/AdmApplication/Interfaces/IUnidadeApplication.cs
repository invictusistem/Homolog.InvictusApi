using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication.Interfaces
{
    public interface IUnidadeApplication
    {
        Task CreateUnidade(UnidadeDto unidadeDto);
        Task AddSala(SalaDto newSala);
        Task EditUnidade(UnidadeDto editedUnidade);
        Task EditSala(SalaDto editedSala);
    }
}
