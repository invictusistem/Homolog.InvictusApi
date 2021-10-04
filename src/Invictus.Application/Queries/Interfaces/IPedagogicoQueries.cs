using Invictus.Application.Dtos;
using Invictus.Application.Dtos.Pedagogico;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.Application.Queries.Interfaces
{
    public interface IPedagogicoQueries
    {
        Task<IEnumerable<TurmaViewModel>> GetTurmas(int unidadeId);
        Task<IEnumerable<MateriaDto>> GetMaterias();
        Task<IEnumerable<MateriaDto>> GetMateriasDoProfessor(int turmaId, int profId);
        Task<IEnumerable<AgendasProvasDto>> GetAgendasProvas(int turmaId);
        Task<List<NotasViewModel>> GetNotaAlunos(int turmaId);
        Task<TransfViewModel> GetDadosParaTransferencia(int alunoId);
        Task<IEnumerable<NotasDisciplinasDto>> GetNotaByMateriaAndTurmaId(int materiaId, int turmaId);
        Task<ListaPresencaViewModel> GetInfoDiaPresencaLista(int materiaId, int turmaId, int calendarioId);
        Task<IEnumerable<AlunoDto>> GetDocsPendenciasLista();

    }   

    
}
