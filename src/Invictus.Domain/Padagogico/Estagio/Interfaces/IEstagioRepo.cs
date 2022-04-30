using System;
using System.Threading.Tasks;

namespace Invictus.Domain.Padagogico.Estagio.Interfaces
{
    public interface IEstagioRepo : IDisposable
    {
        Task CreateEstagio(Estagio estagio);
        Task EditEstagio(Estagio estagio);
        Task CreateEstagioType(TypeEstagio type);
        Task EditEstagioType(TypeEstagio type);
        Task DeleteEstagioType(Guid typeEstagio);
        Task CreateMatricula(MatriculaEstagio matricula);
        void Commit();
    }
}
