using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Administrativo.AlunoAggregate.Interface;
using Invictus.Domain.Administrativo.FuncionarioAggregate;
using Invictus.Domain.Administrativo.FuncionarioAggregate.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using System;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class AlunoApplication : IAlunoApplication
    {
        private readonly IMapper _mapper;
        private readonly IAlunoRepo _alunoRepo;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IAspNetUser _aspNetUser;
        private readonly IPessoaRepo _pessoaRepo;
        public AlunoApplication(IMapper mapper, IAlunoRepo alunoRepo, IUnidadeQueries unidadeQueries, IAspNetUser aspNetUser, IPessoaRepo pessoaRepo)
        {
            _mapper = mapper;
            _alunoRepo = alunoRepo;
            _unidadeQueries = unidadeQueries;
            _aspNetUser = aspNetUser;
            _pessoaRepo = pessoaRepo;
        }

        public async Task EditAluno(PessoaDto editedAluno)
        {
            var aluno = _mapper.Map<Pessoa>(editedAluno);

            await _pessoaRepo.EditPessoa(aluno);

            //TODO:  verificar ativo e acesso ?

            _pessoaRepo.Commit();
        }

        public async Task SaveAluno(PessoaDto newAluno)
        {

            var unidade = await _unidadeQueries.GetUnidadeDoUsuario();
            
            newAluno.unidadeId = unidade.id;
            newAluno.dataCadastro = DateTime.Now;
            
            var aluno = _mapper.Map<Pessoa>(newAluno);

            //var colaboradorId = _aspNetUser.ObterUsuarioId();

            aluno.SetRespCadastroId(_aspNetUser.ObterUsuarioId());

            aluno.SetTipoPessoa(TipoPessoa.Aluno);

            await _pessoaRepo.AddPessoa(aluno);

            _pessoaRepo.Commit();
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
