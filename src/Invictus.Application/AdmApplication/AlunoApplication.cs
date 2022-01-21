using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Administrativo.AlunoAggregate.Interface;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class AlunoApplication : IAlunoApplication
    {
        private readonly IMapper _mapper;
        private readonly IAlunoRepo _alunoRepo;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IAspNetUser _aspNetUser;
        public AlunoApplication(IMapper mapper, IAlunoRepo alunoRepo, IUnidadeQueries unidadeQueries, IAspNetUser aspNetUser)
        {
            _mapper = mapper;
            _alunoRepo = alunoRepo;
            _unidadeQueries = unidadeQueries;
            _aspNetUser = aspNetUser;
        }

        public async Task EditAluno(AlunoDto editedAluno)
        {
            var aluno = _mapper.Map<Aluno>(editedAluno);

            await _alunoRepo.Edit(aluno);

            _alunoRepo.Commit();
        }

        public async Task saveAlunos(AlunoDto newAluno)
        {
            var unidade = await _unidadeQueries.GetUnidadeDoUsuario();
            newAluno.unidadeId = unidade.id;

            var aluno = _mapper.Map<Aluno>(newAluno);

            var colaboradorId =  _aspNetUser.ObterUsuarioId();
            aluno.SetColaboradorResponsavelPeloCadastro(colaboradorId);

            aluno.SetDataCadastro();
            

            await _alunoRepo.SaveAluno(aluno);

            _alunoRepo.Commit();

        }
    }
}
