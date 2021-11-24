using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
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
        public AlunoApplication(IMapper mapper, IAlunoRepo alunoRepo, IUnidadeQueries unidadeQueries)
        {
            _mapper = mapper;
            _alunoRepo = alunoRepo;
            _unidadeQueries = unidadeQueries;
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

            aluno.SetDataCadastro();
            

            await _alunoRepo.SaveAluno(aluno);

            _alunoRepo.Commit();

        }
    }
}
