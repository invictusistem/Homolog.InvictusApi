using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication.Interfaces
{
    public interface IRelatorioApp
    {
        void ReadAndSaveExcel();
        void SaveFornecedores();
        List<MatriculaCommand> MatriculaExcel(MatriculaPlanilha matricula);
        void DeleteExcel();
    }

    public class MatriculaPlanilha
    {
        public string planilhaNome { get; set; }
        public string planilhaFin { get; set; }
        public Guid turmaId { get; set; }
        public Guid planoId { get; set; }
    }
}
