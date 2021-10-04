using System.Threading.Tasks;

namespace Invictus.Application.Queries.Interfaces
{
    public interface IExportExcel
    {
        Task<byte[]> ExportFile();
    }
}
