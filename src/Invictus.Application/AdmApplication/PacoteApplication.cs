using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.PacoteAggregate;
using Invictus.Domain.Administrativo.PacoteAggregate.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class PacoteApplication : IPacoteApplication
    {
        private readonly IMapper _mapper;
        private readonly IPacoteRepository _pacoteRepo;
        private readonly IPacoteQueries _pacoteQueries;
        private readonly IAspNetUser _aspNetuser;
        private readonly IUnidadeQueries _unidadeQueries;
        private List<string> _validationResult;
        public PacoteApplication(IMapper mapper, IPacoteRepository pacoteRepo, IPacoteQueries pacoteQueries, IAspNetUser aspNetuser, IUnidadeQueries unidadeQueries)
        {
            _mapper = mapper;
            _pacoteRepo = pacoteRepo;
            _pacoteQueries = pacoteQueries;
            _validationResult = new List<string>();
            _aspNetuser = aspNetuser;
            _unidadeQueries = unidadeQueries;
        }

        public async Task<IEnumerable<string>> SavePacote(PacoteDto newPacote)
        {
            var unidadeSigla = _aspNetuser.ObterUnidadeDoUsuario();

            var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);

            newPacote.unidadeId = unidade.id;

            var seachForPacoteName = await _pacoteQueries.GetPacoteByDescricao(newPacote.descricao);

            if (seachForPacoteName.Any())
            {
                _validationResult.Add("Já existe um pacote salvo com a mesma descrição.");
                return _validationResult;
            }

            var pacote = _mapper.Map<Pacote>(newPacote);
            var i = 0;

            foreach (var item in pacote.Materias)
            {
                item.SetOrdem(i+1);
                i++;
            }
            pacote.SetCreateDate();
            await _pacoteRepo.Save(pacote);
            _pacoteRepo.Commit();

            return _validationResult;
        }

        public async Task EditPacote(PacoteDto editPacote)
        {
            var pacote = _mapper.Map<Pacote>(editPacote);
            await _pacoteRepo.Edit(pacote);
            _pacoteRepo.Commit();
        }

    }
}
