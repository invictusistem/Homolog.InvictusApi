using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication.Interfaces
{
    public interface IPedagDocApp
    {
        Task ADdFileToDocument(Guid documentId, IFormFile file);
    }
}
