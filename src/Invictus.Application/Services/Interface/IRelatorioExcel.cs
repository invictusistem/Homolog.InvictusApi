using Invictus.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Services.Interface
{
    public interface IRelatorioExcel
    {

        //Task<byte[]> ExportLivroMatriculaTodos(IEnumerable<AlunoDto> alunos);
        Task<byte[]> ExportLivroMatricula(IEnumerable<AlunoDto> alunos);
    }
}
