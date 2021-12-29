using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.BackgroundTasks;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Administrativo.AlunoAggregate.Interface;
using Invictus.Domain.Administrativo.MatriculaRegistro;
using Invictus.Domain.Administrativo.RegistroMatricula;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Administrativo.TurmaAggregate.Interfaces;
using Invictus.Domain.Financeiro;
using Invictus.Domain.Financeiro.Interfaces;
using Invictus.Domain.Padagogico.AlunoAggregate.Interfaces;
using Invictus.Domain.Padagogico.NotasTurmas;
using Invictus.Domain.Padagogico.NotasTurmas.Interface;
using Invictus.Domain.Pedagogico.AlunoAggregate;
using Invictus.Domain.Pedagogico.Responsaveis;
using Invictus.Domain.Pedagogico.Responsaveis.Interfaces;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class MatriculaApplication : IMatriculaApplication
    {
        private readonly IPlanoPagamentoQueries _planoQueries;
        private readonly IBoletoService _boletoService;
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
        private readonly IDebitosRepos _debitoRepos;
        private readonly ILogger<MatriculaApplication> _logger;
        private readonly BackgroundWorkerQueue _backgroundWorkerQueue;
        private readonly InvictusDbContext _db;
        private Guid _turmaId;
        private Guid _alunoId;
        private MatriculaCommand _command;
        private List<AlunoDocumento> _alunoDocs;
        private List<TurmaNotas> _turmaNotas;
        private Guid _newMatriculaId;
        private Guid _pacoteId;
        private bool _temRespMenor;
        private bool _temRespFinanc;
        private Turma _turma;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public MatriculaApplication(IPlanoPagamentoQueries planoQueries, IAlunoQueries alunoQueries, ITurmaQueries turmaQueries, IMapper mapper,
            IPacoteQueries pacoteQueries, ITurmaRepo turmaRepo, IMatriculaRepo matRepo, IAlunoRepo alunoRepo, ITurmaNotasRepo turmaNotasRepo,
            IMatriculaQueries matQueries, IRespRepo respRepo, IAspNetUser aspNetUser, IAlunoPedagRepo alunoPedagRepo,
            IBoletoService boletoService, IDebitosRepos debitoRepos, BackgroundWorkerQueue backgroundWorkerQueue, ILogger<MatriculaApplication> logger,
            InvictusDbContext db, IServiceScopeFactory serviceScopeFactory)
        {
            _planoQueries = planoQueries;
            _alunoQueries = alunoQueries;
            _turmaQueries = turmaQueries;
            _turmaRepo = turmaRepo;
            _matRepo = matRepo;
            _matQueries = matQueries;
            _debitoRepos = debitoRepos;
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
            _turma = new Turma();
            _boletoService = boletoService;
            _temRespMenor = false;
            _temRespFinanc = false;
            _backgroundWorkerQueue = backgroundWorkerQueue;
            _logger = logger;
            _db = db;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void AddParams(Guid turmaId, Guid alunoId, MatriculaCommand command) { _turmaId = turmaId; _alunoId = alunoId; _command = command; }

        public async Task Matricular()
        {
            await VerificarResponsaveis();

            await AdicionarAlunoNaTurma();

            await CreateMatriculaDoAluno();

            await CreatePlanoDePagamentoDoAluno();

            await CreateDocsDoAlunosNaMatricula();

            await CreateNotasDoAlunoNaTurma();

            await CreateResponsaveis();

            //SaveContratoAluno();

            //SaveFichaMatricula();

            // TODO
            //await CreateInfoFinanceirasDoAluno(_turmaId, _newMatriculaId, _alunoId, _command);
            _backgroundWorkerQueue.QueueBackgroundWorkItem(async token =>
            {

                await CreateInfoFinanceirasDoAluno(_turmaId, _newMatriculaId, _alunoId, _command);
                _logger.LogInformation($"Done at {DateTime.UtcNow.TimeOfDay}");
            });

            //await CreateInfoFinanceirasDoAluno();

            await _turmaRepo.Edit(_turma);
            _matRepo.Commit();
        }

        private async Task VerificarResponsaveis()
        {
            var menorDeIdade = await _alunoQueries.GetIdadeAluno(_alunoId);
            int age = 0;
            age = DateTime.Now.Subtract(menorDeIdade).Days;
            age = age / 365;
            var menor = true;
            if (age >= 18) menor = false;

            if (menor)
            {
                _temRespMenor = true;
            }

            if (_command.temRespFin)
            {
                _temRespFinanc = true;
            }
        }

        private async Task AdicionarAlunoNaTurma()
        {
            _turma = _mapper.Map<Turma>(await _turmaQueries.GetTurma(_turmaId));
            _turma.AddAlunoNaTurma();
            //await _turmaRepo.Edit(_turma);
            _pacoteId = _turma.PacoteId;
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
            foreach (var doc in documentacao.Where(d => d.titular == "Aluno"))
            {
                _alunoDocs.Add(new AlunoDocumento(_newMatriculaId, doc.descricao, doc.comentario, false, false, false, doc.validadeDias, _turmaId));
            } 

            if (_temRespMenor)
            {
                foreach (var doc in documentacao.Where(d => d.titular == "Responsável menor"))
                {
                    _alunoDocs.Add(new AlunoDocumento(_newMatriculaId, doc.descricao, doc.comentario, false, false, false, doc.validadeDias, _turmaId));
                }
            }

            if (_temRespFinanc)
            {
                foreach (var doc in documentacao.Where(d => d.titular == "Responsável financeiro"))
                {
                    _alunoDocs.Add(new AlunoDocumento(_newMatriculaId, doc.descricao, doc.comentario, false, false, false, doc.validadeDias, _turmaId));
                }
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











        public async Task CreateInfoFinanceirasDoAluno(Guid turmaId, Guid newMatriculaId, Guid alunoId, MatriculaCommand comand)
        {
            var turma = await _turmaQueries.GetTurma(turmaId);
            var infoFin = new InformacaoDebito(comand.plano.parcelas, comand.plano.valor, "", StatusPagamento.EmAberto, DebitoOrigem.Curso, turma.id, "", newMatriculaId, DateTime.Now);

            var menorDeIdade = await _alunoQueries.GetIdadeAluno(alunoId);
            int age = 0;
            age = DateTime.Now.Subtract(menorDeIdade).Days;
            age = age / 365;
            var menor = true;
            if (age >= 18) menor = false;

            var pessoa = new DadosPessoaDto();
            // definir resp no boleto

            if (comand.temRespFin)
            {
                pessoa.nome = comand.respFin.nome;
                pessoa.telefone = comand.respFin?.telCelular;
                pessoa.cpf = comand.respFin.cpf;
                pessoa.logradouro = comand.respFin.logradouro;
                pessoa.bairro = comand.respFin.bairro;
                pessoa.cidade = comand.respFin.cidade;
                pessoa.estado = comand.respFin.uf;
                pessoa.cep = comand.respFin.cep;

            }
            else// if(menor)
            {
                if (menor)
                {
                    pessoa.nome = comand.respMenor.nome;
                    pessoa.telefone = comand.respMenor?.telCelular;
                    pessoa.cpf = comand.respMenor.cpf;
                    pessoa.logradouro = comand.respMenor.logradouro;
                    pessoa.bairro = comand.respMenor.bairro;
                    pessoa.cidade = comand.respMenor.cidade;
                    pessoa.estado = comand.respMenor.uf;
                    pessoa.cep = comand.respMenor.cep;
                }
                else
                {
                    var aluno = await _alunoQueries.GetAlunoById(alunoId);

                    pessoa.nome = aluno.nome;
                    pessoa.telefone = aluno?.telCelular;
                    pessoa.cpf = aluno.cpf;
                    pessoa.logradouro = aluno.logradouro;
                    pessoa.bairro = aluno.bairro;
                    pessoa.cidade = aluno.cidade;
                    pessoa.estado = aluno.uf;
                    pessoa.cep = aluno.cep;
                }

            }

            var boletosResponse = await _boletoService.GerarBoletosUnicos(comand.plano.infoParcelas, pessoa);
            var turmaX = await _turmaQueries.GetTurma(turmaId);
            var boletos = new List<Boleto>();

            for (int i = 1; i <= comand.plano.infoParcelas.Count(); i++)
            {
                var boletoResp = boletosResponse.Where(b => b.pedido_numero == i.ToString()).FirstOrDefault();

                //var boleto = _mapper.Map<BoletoResponseInfo>(boletoResp);
                //var boleto = _mapper.Map<BoletoResponseInfo>(boletosResponse[i - 1]);
                //var boletoResp = boletosResponse[i - 1];
                var boleto = new BoletoResponseInfo(boletoResp.id_unico, boletoResp.id_unico_original, boletoResp.status, boletoResp.msg, boletoResp.nossonumero,
                    boletoResp.linkBoleto, boletoResp.linkGrupo, boletoResp.linhaDigitavel, boletoResp.pedido_numero, boletoResp.banco_numero,
                    boletoResp.token_facilitador, boletoResp.credencial);

                var parc = i.ToString();

                var parcela = comand.plano.infoParcelas.Where(i => i.parcelaNo == parc).FirstOrDefault();//.FirstOrDefault();

                //var dataVencimento = comand.plano.infoParcelas.Where(i => i.parcelaNo == parc).Select(i => i.vencimento).FirstOrDefault();

                boletos.Add(new Boleto(parcela.vencimento, parcela.valor, 0, 0, "", "",
                    comand.plano.bonusPontualidade.ToString(), "", StatusPagamento.EmAberto, turmaX.unidadeId, infoFin.Id, boleto));

            }

            infoFin.AddBoletos(boletos);
            try
            {


                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var db = scope.ServiceProvider.GetService<InvictusDbContext>();
                    db.InformacoesDebito.Add(infoFin);
                    db.SaveChanges();
                }
                //await _debi toRepos.SaveInfoFinanceira(infoFin);
                //_db.InformacoesDebito.Add(infoFin);
            }catch(Exception ex)
            {
                Debug.WriteLine("error");
            }
            //_db.SaveChanges();
            //_debitoRepos.Commit();

            //trth

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
