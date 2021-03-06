using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.Utilitarios.Interface
{
    public interface IUtils
    {
        Task<IEnumerable<string>> ValidaDocumentosAluno(string cpf, string rg, string email);
        Task<IEnumerable<string>> ValidaDocumentosColaborador(string cpf, string rg, string email);
        Task<IEnumerable<string>> ValidaDocumentoPessoa(string cpf, string rg, string email);
        Task<IEnumerable<string>> ValidaUnidade(UnidadeDto newUnidade);
        Task<IEnumerable<string>> ValidaUnidadeEdit(UnidadeDto newUnidade);
    }
}
