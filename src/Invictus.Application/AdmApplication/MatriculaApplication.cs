using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Administrativo.AlunoAggregate.Interface;
using Invictus.Domain.Administrativo.MatriculaRegistro;
using Invictus.Domain.Administrativo.RegistroMatricula;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Administrativo.TurmaAggregate.Interfaces;
using Invictus.Domain.Padagogico.AlunoAggregate.Interfaces;
using Invictus.Domain.Padagogico.NotasTurmas;
using Invictus.Domain.Padagogico.NotasTurmas.Interface;
using Invictus.Domain.Pedagogico.AlunoAggregate;
using Invictus.Domain.Pedagogico.Responsaveis;
using Invictus.Domain.Pedagogico.Responsaveis.Interfaces;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class MatriculaApplication : IMatriculaApplication
    {
        private readonly IPlanoPagamentoQueries _planoQueries;
        private readonly IAlunoQueries _alunoQueries;
        private readonly ITurmaQueries _turmaQueries;
        private readonly IMatriculaQueries _matQueries;
        private readonly IMapper _mapper;
        private readonly IPacoteQueries _pacoteQueries;
        private readonly ITurmaRepo _turmaRepo;
        private readonly IMatriculaRepo _matRepo;
        private readonly IAlunoRepo _alunoRepo;
        private readonly ITurmaNotasRepo _turmaNotasRepo;
        private readonly IRespRepo _respRepo;
        private readonly IAspNetUser _aspNetUser;
        private readonly IAlunoPedagRepo _alunoPedagRepo;
        private Guid _turmaId;
        private Guid _alunoId;
        private MatriculaCommand _command;
        private List<AlunoDocumento> _alunoDocs;
        private List<TurmaNotas> _turmaNotas;
        private Guid _newMatriculaId;
        private Guid _pacoteId;

        public MatriculaApplication(IPlanoPagamentoQueries planoQueries, IAlunoQueries alunoQueries, ITurmaQueries turmaQueries, IMapper mapper, 
            IPacoteQueries pacoteQueries, ITurmaRepo turmaRepo, IMatriculaRepo matRepo, IAlunoRepo alunoRepo, ITurmaNotasRepo turmaNotasRepo,
            IMatriculaQueries matQueries, IRespRepo respRepo, IAspNetUser aspNetUser, IAlunoPedagRepo alunoPedagRepo)
        {
            _planoQueries = planoQueries;
            _alunoQueries = alunoQueries;
            _turmaQueries = turmaQueries;
            _turmaRepo = turmaRepo;
            _matRepo = matRepo;
            _matQueries = matQueries;
            _respRepo = respRepo;
            _mapper = mapper;
            _turmaNotasRepo = turmaNotasRepo;
            _alunoRepo = alunoRepo;
            _aspNetUser = aspNetUser;
            _alunoPedagRepo = alunoPedagRepo;
            _turmaId = Guid.NewGuid();
            _alunoId = Guid.NewGuid();
            _newMatriculaId = Guid.NewGuid();
            _pacoteId = Guid.NewGuid();
            _command = new MatriculaCommand();
            _pacoteQueries = pacoteQueries;
            _alunoDocs = new List<AlunoDocumento>();
            _turmaNotas = new List<TurmaNotas>();
        }

        public void AddParams(Guid turmaId, Guid alunoId, MatriculaCommand command) { _turmaId = turmaId; _alunoId = alunoId; _command = command; }

        public async Task Matricular()
        {
            await AdicionarAlunoNaTurma();

            await CreateMatriculaDoAluno();

            await CreatePlanoDePagamentoDoAluno();

            await CreateDocsDoAlunosNaMatricula();

            await CreateNotasDoAlunoNaTurma();

            await CreateResponsaveis();

            //SaveContratoAluno();

            //SaveFichaMatricula();

            // TODO
            // CreateInfoFinanceirasDoAluno()

            _matRepo.Commit();
        }

        private async Task CreateResponsaveis()
        {
            var menorDeIdade = await _alunoQueries.GetIdadeAluno(_alunoId);
            int age = 0;
            age = DateTime.Now.Subtract(menorDeIdade).Days;
            age = age / 365;
            var menor = true;
            if (age >= 18) menor = false;

            if (menor)
            {
                _command.respMenor.matriculaId = _newMatriculaId;
                var respForm = _command.respMenor;
                var resp = _mapper.Map<Responsavel>(respForm);
                resp.SetRespFinanceiro(_command.temRespFin);
                await _respRepo.Save(resp);
            }

            if (_command.temRespFin)
            {
                _command.respFin.matriculaId = _newMatriculaId;
                var respForm = _command.respFin;
                var resp = _mapper.Map<Responsavel>(respForm);
                await _respRepo.Save(resp);
            }

        }

        private async Task AdicionarAlunoNaTurma()
        {
            var turma = _mapper.Map<Turma>(await _turmaQueries.GetTurma(_turmaId));
            turma.AddAlunoNaTurma();
            await _turmaRepo.Edit(turma);
            _pacoteId = turma.PacoteId;
        }

        private async Task CreateMatriculaDoAluno()
        {
            var aluno = await _alunoQueries.GetAlunoById(_alunoId);
            var status = SetMatriculaStatus(_command.plano.confirmacaoPagmMat);
            var newMatricula = new Matricula(_alunoId, aluno.nome, aluno.cpf, status, _turmaId);
            newMatricula.SetDiaMatricula();
            var totalMatriculados = await _matQueries.TotalMatriculados();
            newMatricula.SetNumeroMatricula(totalMatriculados);
            await _matRepo.Save(newMatricula);//  newMatricula.Repo
            _newMatriculaId = newMatricula.Id;
        }

        private async Task CreatePlanoDePagamentoDoAluno()
        {
            var plano = await _planoQueries.GetPlanoById(_command.plano.planoId);
            var newPlanoAluno = new AlunoPlanoPagamento(plano.descricao, plano.valor, _command.plano.taxaMatricula, _command.plano.parcelas, plano.materialGratuito, plano.valorMaterial,
                _command.plano.bonusPontualidade, _newMatriculaId);
            await _alunoRepo.SaveAlunoPlano(newPlanoAluno);
            
        }

        private async Task CreateDocsDoAlunosNaMatricula()
        {
            var documentacao = await _pacoteQueries.GetDocsByPacoteId(_pacoteId);
            foreach (var doc in documentacao)
            {
                _alunoDocs.Add(new AlunoDocumento(_newMatriculaId, doc.descricao, doc.comentario, false, false, false, doc.validadeDias, _turmaId));
            }

            await _alunoRepo.SaveAlunoDocs(_alunoDocs);
        }

        private async Task CreateNotasDoAlunoNaTurma()
        {
            var turmasMaterias = await _turmaQueries.GetMateriasDaTurma(_turmaId);
            foreach (var materia in turmasMaterias)
            {
                var nota = new TurmaNotas();
                _turmaNotas.Add(nota.CreateNotaDisciplinas(materia.nome, materia.id, _newMatriculaId, _turmaId));
            }

            await _turmaNotasRepo.SaveList(_turmaNotas);
        }

        private StatusMatricula SetMatriculaStatus(bool status)
        {
            if (status) return StatusMatricula.AguardoConfirmacao;

            return StatusMatricula.Regular;

        }

        public async Task SetAnotacao(AnotacaoDto anotacao)
        {
            anotacao.dataRegistro = DateTime.Now;
            // setar dataRegistro = DateTime.Now
            // buscar Id do usuario
            anotacao.userId = _aspNetUser.ObterUsuarioId();
            var anot = _mapper.Map<AlunoAnotacao>(anotacao);

            await _alunoPedagRepo.SaveAnotacao(anot);

            _alunoPedagRepo.Commit();
        }
    }
}
