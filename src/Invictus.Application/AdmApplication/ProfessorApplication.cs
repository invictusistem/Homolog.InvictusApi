using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.Calendarios;
using Invictus.Domain.Administrativo.Calendarios.Interfaces;
using Invictus.Domain.Administrativo.FuncionarioAggregate;
using Invictus.Domain.Administrativo.FuncionarioAggregate.Interfaces;
using Invictus.Domain.Administrativo.ProfessorAggregate;
using Invictus.Domain.Administrativo.ProfessorAggregate.Interfaces;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Administrativo.TurmaAggregate.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MateriaHabilitada = Invictus.Domain.Administrativo.ProfessorAggregate.MateriaHabilitada;

namespace Invictus.Application.AdmApplication
{
    public class ProfessorApplication : IProfessorApplication
    {
        public UserManager<IdentityUser> UserManager { get; set; }
        private readonly IProfessorRepository _profRepository;
        private readonly ITurmaRepo _turmaRepo;
        private readonly ICalendarioQueries _calendariosQueries;
        private readonly ICalendarioRepo _calendarioRepo;
        private readonly IPessoaRepo _pessoaRepo;
        private readonly IProfessorQueries _profQueries;
        private readonly ITurmaQueries _turmaQueries;
        private readonly IMapper _mapper;
        private readonly IAspNetUser _aspNetUser;
        private readonly IUnidadeQueries _unidadeQueries;
        public ProfessorApplication(IProfessorRepository profRepository, IMapper mapper, IAspNetUser aspNetUser, IUnidadeQueries unidadeQueries,
            ICalendarioQueries calendariosQueries, ICalendarioRepo calendarioRepo, IProfessorQueries profQueries, ITurmaQueries turmaQueries,
            ITurmaRepo turmaRepo, UserManager<IdentityUser> userMgr, IPessoaRepo pessoaRepo)
        {
            _profRepository = profRepository;
            _mapper = mapper;
            _aspNetUser = aspNetUser;
            _unidadeQueries = unidadeQueries;
            _calendariosQueries = calendariosQueries;
            _profQueries = profQueries;
            _calendarioRepo = calendarioRepo;
            _turmaQueries = turmaQueries;
            _turmaRepo = turmaRepo;
            UserManager = userMgr;
            _pessoaRepo = pessoaRepo;
        }

        public async Task AddDisponibilidade(DisponibilidadeDto disponibilidadeDto)
        {
            disponibilidadeDto.dataAtualizacao = DateTime.Now;

            var dispo = _mapper.Map<Disponibilidade>(disponibilidadeDto);

            await _profRepository.AddDisponibilidade(dispo);

            _profRepository.Save();
        }

        public async Task AddProfessorMateria(Guid profId, Guid materiaId)
        {
            var materia = new MateriaHabilitada(materiaId, profId);

            await _profRepository.AddProfessorMateria(materia);

            _profRepository.Save();
        }

        public async Task EditDisponibilidade(DisponibilidadeDto dispoDto)
        {
            if (dispoDto.domingo == false &
                dispoDto.segunda == false &
                dispoDto.terca == false &
                dispoDto.quarta == false &
                dispoDto.quinta == false &
                dispoDto.sexta == false &
                dispoDto.sabado == false)
            {
                var dispo = _mapper.Map<Disponibilidade>(dispoDto);
                await _profRepository.RemoveDisponibilidade(dispo);
                await RemoverProfessorDoCalendarioDaUnidade(dispo.UnidadeId, dispo.PessoaId);
            }
            else
            {

                var dispo = _mapper.Map<Disponibilidade>(dispoDto);

                await _profRepository.EditDisponibilidade(dispo);
                await AtualizarAulasProfessor(dispoDto, dispoDto.unidadeId, dispoDto.pessoaId);
            }

            _profRepository.Save();
        }

        private async Task RemoverProfessorDoCalendarioDaUnidade(Guid unidadeId, Guid professorId)
        {
            IEnumerable<TurmaCalendarioViwModel> calendarios = await _calendariosQueries.GetFutureCalendarsByProfessorIdAndUnidadeId(unidadeId, professorId);

            if (calendarios.Any())
            {
                IEnumerable<Calendario> calend = _mapper.Map<IEnumerable<Calendario>>(calendarios);

                foreach (Calendario calendario in calend)
                {
                    calendario.RemoveProfessorDaAula();
                }

                _calendarioRepo.UpdateCalendarios(calend.ToList());

            }
        }

        private async Task AtualizarAulasProfessor(DisponibilidadeDto dispoDto, Guid unidadeId, Guid professorId)
        {
            IEnumerable<TurmaCalendarioViwModel> calendariosDto = await _calendariosQueries.GetFutureCalendarsByProfessorIdAndUnidadeId(unidadeId, professorId);

            IEnumerable<Calendario> calendarios = _mapper.Map<IEnumerable<Calendario>>(calendariosDto);

            if (calendarios.Any())
            {
                //var calendFilter = calendarios.Where(c => c.unidadeId == dispoDto.unidadeId);
                foreach (var calend in calendarios)
                {
                    if (calend.DiaDaSemana.ToLower() == "domingo")
                    {
                        if (dispoDto.domingo == false)
                        {
                            calend.RemoveProfessorDaAula();
                        }

                    }

                    if (calend.DiaDaSemana.ToLower() == "segunda-feira")
                    {
                        if (dispoDto.segunda == false)
                        {
                            calend.RemoveProfessorDaAula();
                        }
                    }

                    if (calend.DiaDaSemana.ToLower() == "terça-feira")
                    {
                        if (dispoDto.terca == false)
                        {
                            calend.RemoveProfessorDaAula();
                        }
                    }

                    if (calend.DiaDaSemana.ToLower() == "quarta-feira")
                    {
                        if (dispoDto.quarta == false)
                        {
                            calend.RemoveProfessorDaAula();
                        }
                    }

                    if (calend.DiaDaSemana.ToLower() == "quinta-feira")
                    {
                        if (dispoDto.quinta == false)
                        {
                            calend.RemoveProfessorDaAula();
                        }
                    }

                    if (calend.DiaDaSemana.ToLower() == "sexta-feira")
                    {
                        if (dispoDto.sexta == false)
                        {
                            calend.RemoveProfessorDaAula();
                        }
                    }

                    if (calend.DiaDaSemana.ToLower() == "sábado")
                    {
                        if (dispoDto.sabado == false)
                        {
                            calend.RemoveProfessorDaAula();
                        }
                    }

                }

                _calendarioRepo.UpdateCalendarios(calendarios.ToList());

                _calendarioRepo.Commit();

            }

        }

        public async Task EditProfessor(PessoaDto editedProfessor)
        {
            var newEmail = editedProfessor.email;

            var oldEmail = await _profQueries.GetEmailDoProfessorById(editedProfessor.id);

            var professor = _mapper.Map<Pessoa>(editedProfessor);

            // professor.SetDataEntrada(editedProfessor.dataEntrada);

            // professor.SetDataSaida(editedProfessor.dataSaida);

            await _pessoaRepo.EditPessoa(professor);

            _pessoaRepo.Commit();

            if (newEmail != oldEmail)
            {
                if (!String.IsNullOrEmpty(newEmail))
                {
                    // update se existir
                    var user = await UserManager.FindByEmailAsync(oldEmail);
                    if (user != null)
                    {
                        await UserManager.UpdateAsync(user);

                        var claims = await UserManager.GetClaimsAsync(user);

                        var claim = claims.Where(c => c.Type == "IsActive").FirstOrDefault();

                        await UserManager.RemoveClaimAsync(user, claim);

                        await UserManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsActive", editedProfessor.ativo.ToString()));
                    }

                }
                else
                {
                    var user = await UserManager.FindByEmailAsync(oldEmail);
                    if (user != null)
                    {
                        await UserManager.DeleteAsync(user);
                    }
                }
            }


        }

        public async Task RemoveProfessorMateria(Guid profMateriaId)
        {
            // get ProfessorMateria
            var materiaDoProfessor = await _profQueries.GetProfessorMateria(profMateriaId);

            await _profRepository.RemoveProfessorMateria(profMateriaId);

            // remover o prof da materia da turma (turmasMaterias conforme seu id e a matériaId
            var profTurma = _mapper.Map<IEnumerable<TurmaMaterias>>(await _turmaQueries.GetTurmasMateriasByProfessorAndMateriaId(materiaDoProfessor.PacoteMateriaId, materiaDoProfessor.ProfessorId));

            if (profTurma.Any())
            {
                // set prf guid 00000000 no turmasMaterias
                foreach (TurmaMaterias turmaMateria in profTurma)
                {
                    turmaMateria.RemoveProfessorDaMateria();
                }

                _turmaRepo.AtualizarTurmasMaterias(profTurma);
            }

            //remover do calendario futuro conforme seu id e matéria Id
            IEnumerable<Calendario> calendarios = _mapper.Map<IEnumerable<Calendario>>(await _calendariosQueries.GetFutureCalendarsByProfessorIdAndMateriaId(materiaDoProfessor.PacoteMateriaId, materiaDoProfessor.ProfessorId));
            if (calendarios.Any())
            {
                foreach (Calendario calendario in calendarios)
                {
                    calendario.RemoveProfessorDaAula();
                }

                _calendarioRepo.UpdateCalendarios(calendarios.ToList());
            }

            _profRepository.Save();
        }

        public async Task SaveProfessorV2(PessoaDto newProfessor)
        {
            var unidade = await _unidadeQueries.GetUnidadeBySigla(_aspNetUser.ObterUnidadeDoUsuario());

            newProfessor.dataCadastro = DateTime.Now;

            newProfessor.unidadeId = unidade.id;

            var professor = _mapper.Map<Pessoa>(newProfessor);

            professor.SetTipoPessoa(TipoPessoa.Professor);

            professor.SetRespCadastroId(_aspNetUser.ObterUsuarioId());

            await _pessoaRepo.AddPessoa(professor);

            _pessoaRepo.Commit();
        }

        //public async Task SaveProfessor(ProfessorDto newProfessor)
        //{
        //    var unidade = await _unidadeQueries.GetUnidadeBySigla(_aspNetUser.ObterUnidadeDoUsuario());

        //    newProfessor.unidadeId = unidade.id;

        //    var professor = _mapper.Map<Professor>(newProfessor);

        //    //var sdfd = newProfessor.dataEntrada;
        //    //var wewfwf = newProfessor.dataEntrada.Value;
        //    professor.SetDataCadastro();

        //    professor.SetDataEntrada(newProfessor.dataEntrada);

        //    await _profRepository.AddProfessor(professor);

        //    _profRepository.Save();
        //}
    }
}
