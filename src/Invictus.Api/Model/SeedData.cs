using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Invictus.Application.Dtos;
using Invictus.Core.Enums;
using Invictus.Core.Util;
using Invictus.Data.Context;
using Invictus.Domain;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Administrativo.UnidadeAggregate;
using Invictus.Domain.Pedagogico.TurmaAggregate;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Invictus.Api.Model
{
    public static class SeedData
    {

        public static void EnsurePopulated(IApplicationBuilder app, IConfiguration Configuration)
        {
            InvictusDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<InvictusDbContext>();
                      
            /*
            var idUnidadeCriada = 0;
            var idModuloCriado = 0;

            #region UnidadeAggregate 
            //var unidade = new Unidade(0, "CGI", "Invictus Campo Grande", "Campo Grande", "23088000", "", "Estrada da Posse 3.700", "Rio de Janeiro", "RJ");
           

            //context.Unidades.Add(unidade);
           
            context.SaveChanges();

            //var modulo = new Modulo(0, "Curso Técnico em Enfermagem","Enfermagem", unidade.Id, 20, 4290);
           // context.Modulos.Add(modulo);
            context.SaveChanges();

            //context.Materias.AddRange(
            //    new Domain.Administrativo.UnidadeAggregate.Materia(0, "Ética", 6, 0,ModalidadeCurso.Presencial, 0,modulo.Id),
            //    new Domain.Administrativo.UnidadeAggregate.Materia(0, "Psicologia", 6, 0, ModalidadeCurso.Presencial, 0, modulo.Id),
            //    new Domain.Administrativo.UnidadeAggregate.Materia(0, "Microbiologia", 12, 0, ModalidadeCurso.Presencial, 0, modulo.Id),
            //    new Domain.Administrativo.UnidadeAggregate.Materia(0, "Anatomia", 12, 0, ModalidadeCurso.Presencial, 0, modulo.Id),
            //    new Domain.Administrativo.UnidadeAggregate.Materia(0, "Saúde Coletiva + SUS", 12, 0, ModalidadeCurso.Presencial, 0, modulo.Id),
            //    new Domain.Administrativo.UnidadeAggregate.Materia(0, "Fundamentos de Enfermagem I", 12, 0, ModalidadeCurso.Presencial, 0, modulo.Id),
            //    new Domain.Administrativo.UnidadeAggregate.Materia(0, "Fundamentos de Enfermagem II", 12, 0, ModalidadeCurso.Presencial, 0, modulo.Id),
            //    new Domain.Administrativo.UnidadeAggregate.Materia(0, "Clínica Médica", 12, 0, ModalidadeCurso.Presencial, 0, modulo.Id),
            //    new Domain.Administrativo.UnidadeAggregate.Materia(0, "Clínica Cirúrgica", 12, 0, ModalidadeCurso.Presencial, 0, modulo.Id),
            //    new Domain.Administrativo.UnidadeAggregate.Materia(0, "Saúde da Mulher", 12, 0, ModalidadeCurso.Presencial, 0, modulo.Id),
            //    new Domain.Administrativo.UnidadeAggregate.Materia(0, "Pacientes Graves", 1, 0, ModalidadeCurso.Presencial, 0, modulo.Id));

            context.SaveChanges();

            context.Salas.AddRange(
                new Sala(0, "Sala 1", "com ar-condicionado | Com projetor", 30, unidade.Id),
                new Sala(0, "Sala 1", "com ar-condicionado | Com projetor", 35, unidade.Id),
                new Sala(0, "Sala 1", "sem ar-condicionado", 25, unidade.Id));

            idUnidadeCriada = unidade.Id;
            idModuloCriado = modulo.Id;

            #endregion

            #region Colaboradores

            context.Colaboradores.AddRange(
                new Colaborador(
                    0, "Desenolvedor", "invictus@master.com", "12345678912",
                    "21555555555", "Desenvolvedor", unidade.Id, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "João da Silva", "joao@gmail.com", "58795248975",
                    "21548957898", "Professor", unidade.Id, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "João Teixeira", "joaoteix@gmail.com", "42961742072",
                    "21548957898", "Professor", unidade.Id, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "Joaquim José", "joaquim@gmail.com", "04605705015",
                    "21548957898", "Professor", unidade.Id, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "Andre Marques", "andre@gmail.com", "20511464037",
                    "21548957898", "Professor", unidade.Id, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "Silvio Santos", "silvio@gmail.com", "75448137032",
                    "21548957898", "Professor", unidade.Id, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "Fausto Silva", "fausto@gmail.com", "53272733000",
                    "21548957898", "Professor", unidade.Id, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "João Almeida", "joao2@gmail.com", "50409942065",
                    "21548957898", "Administrador", unidade.Id, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ")
                //new Colaborador(
                //    0, "Mévio Tício", "mevio@gmail.com", "26902480001",
                //    "21548957898", "Professor", unidade2.Id, null,
                //    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                //    "Campo Grande", "Rio de Janeiro", "RJ"),
                //new Colaborador(
                //    0, "Antonio Carlos", "antonio@gmail.com", "37490462045",
                //    "21548957898", "Professor", unidade3.Id, null,
                //    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                //    "Campo Grande", "Rio de Janeiro", "RJ"),
                //new Colaborador(
                //    0, "Mario Silva", "mario@gmai.com", "52368455051",
                //    "21548957898", "Administrador", unidade3.Id, null,
                //    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                //    "Campo Grande", "Rio de Janeiro", "RJ"),
                //new Colaborador(
                //    0, "Luciano Huck", "luciano@gmai.com", "14269876093",
                //    "21548957898", "Administrador", unidade3.Id, null,
                //    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                //    "Campo Grande", "Rio de Janeiro", "RJ")
            );
            context.SaveChanges();

            #endregion

            #region AlunoAggregate
            var aluno1 = new Domain.Administrativo.AlunoAggregate.Aluno(0, "Jade Morão Barrocas", "202000001", null, "64550166029", "239257042", new DateTime(2006, 8, 15, 0, 0, 0),
                "Rio de Janeiro", "RJ", "jade.morao@gmail.com", "(21)99999-9999", "Maria", "(21)99999-5555", null, null, "23087283", "Estrada do Mendanha", "casa 1",
                "Rio de Janeiro", "RJ", "Campo Grande", null, "observaçoes etc...", unidade.Id);


            var aluno2 = new Domain.Administrativo.AlunoAggregate.Aluno(0, "Vitória Carneiro Álvares", "202000002", null, "96595951070", "505449158", new DateTime(2000, 1, 1, 0, 0, 0),
            "Rio de Janeiro", "RJ", "vitoria.carneiro.morao@gmail.com", "(21)99999-9999", "Maria", "(21)99999-5555", null, null, "23087283", "Estrada do Mendanha", "casa 1",
            "Rio de Janeiro", "RJ", "Campo Grande", null, "observaçoes etc...", unidade.Id);

            var aluno3 = new Domain.Administrativo.AlunoAggregate.Aluno(0, "Azael Godoi Santana", "202000003", null, "47410950021", "209151973", new DateTime(2005, 1, 1, 0, 0, 0),
            "Rio de Janeiro", "RJ", "azael.godoi@gmail.com", "(21)99999-9999", "Maria", "(21)99999-5555", null, null, "23087283", "Estrada do Mendanha", "casa 1",
            "Rio de Janeiro", "RJ", "Campo Grande", null, "observaçoes etc...", unidade.Id);

            //var aluno4 = new Domain.Administrativo.AlunoAggregate.Aluno(0, "Vitória da Silva", "202000004", null, "69173153036", "120239759", new DateTime(1999, 6, 10, 0, 0, 0),
            //    "Rio de Janeiro", "RJ", "vitoria.silva@gmail.com", "(21)99999-9999", "Maria", "(21)99999-5555", null, null, "23087283", "Estrada do Mendanha", "casa 1",
            //    "Rio de Janeiro", "RJ", "Campo Grande", null, "observaçoes etc...", unidade3.Id);

            var aluno5 = new Domain.Administrativo.AlunoAggregate.Aluno(0, "Álvaro Carlos Camargo", "202000005", null, "12345678912", "123456789", new DateTime(1999, 6, 10, 0, 0, 0),
                "Rio de Janeiro", "RJ", "invictus@teste.com", "(21)99999-9999", "Maria", "(21)99999-5555", null, null, "23087283", "Estrada do Mendanha", "casa 1",
                "Rio de Janeiro", "RJ", "Campo Grande", null, "observaçoes etc...", unidade.Id);

            context.Alunos.Add(aluno1);
            context.SaveChanges();

            context.Alunos.Add(aluno2);
            context.SaveChanges();

            context.Alunos.Add(aluno3);
            context.SaveChanges();

            //context.Alunos.Add(aluno4);
            //context.SaveChanges();

            context.Alunos.Add(aluno5);
            context.SaveChanges();

            var resp1 = new Responsavel(0, TipoResponsavel.ResponsavelMenor, "Kayden Sodré Argolo", "37881456094", "480416369", new DateTime(1995, 10, 10, 0, 0, 0), "Rio de Janeiro",
                "RJ", "kayden.sodre@gmail.com", "(21)99999-5555", null, null, "23087283", "Estrada do Mendanha", "casa 1",
                "Rio de Janeiro", "RJ", "Campo Grande", "observaçoes etc...", aluno1.Id);

            var resp2 = new Responsavel(0, TipoResponsavel.ResponsavelFinanceiro, "Veronika Melancia Talhão", "33263486063", "204059719", new DateTime(1995, 10, 10, 0, 0, 0), "Rio de Janeiro",
                "RJ", "veronika.melancia@gmail.com", "(21)99999-5555", null, null, "23087283", "Estrada do Mendanha", "casa 1",
                "Rio de Janeiro", "RJ", "Campo Grande", "observaçoes etc...", aluno1.Id);

            var resp3 = new Responsavel(0, TipoResponsavel.ResponsavelMenor, "Deise Rodovalho Bezerra", "45152266067", "383287285", new DateTime(1995, 10, 10, 0, 0, 0), "Rio de Janeiro",
                "RJ", "deise.rodovalho@gmail.com", "(21)99999-5555", null, null, "23087283", "Estrada do Mendanha", "casa 1",
                "Rio de Janeiro", "RJ", "Campo Grande", "observaçoes etc...", aluno2.Id);

            var resp4 = new Responsavel(0, TipoResponsavel.ResponsavelFinanceiro, "Josué Vasques Amorim", "13635753092", "272649892", new DateTime(1995, 10, 10, 0, 0, 0), "Rio de Janeiro",
                "RJ", "josue.vasques@gmail.com", "(21)99999-5555", null, null, "23087283", "Estrada do Mendanha", "casa 1",
                "Rio de Janeiro", "RJ", "Campo Grande", "observaçoes etc...", aluno3.Id);

            context.Responsaveis.Add(resp1);
            context.SaveChanges();

            context.Responsaveis.Add(resp2);
            context.SaveChanges();

            context.Responsaveis.Add(resp3);
            context.SaveChanges();

            context.Responsaveis.Add(resp4);
            context.SaveChanges();

            context.SaveChanges();

            #endregion

            #region TurmaAggregate
            var newTurma = new CreateCursoDto();
            newTurma.minVagas = 25;
            newTurma.prevInicio_1 = new DateTime(2021, 9, 12, 0, 0, 0);
            newTurma.prevInicio_2 = new DateTime(2021, 9, 19, 0, 0, 0);
            newTurma.prevInicio_3 = new DateTime(2021, 9, 26, 0, 0, 0);
            newTurma.vagas = 35;
            var modulo1 = new Modulo(idModuloCriado, "Curso Técnico em Enfermagem", "Enfermagem", idUnidadeCriada, 20,4290);
            //newTurma.modulo = modulo1;

            var horarios = new HorariosDto();
            horarios.horarioIni_1 = "08:00";
            horarios.horarioFim_1 = "12:30";
            horarios.dia1 = "Monday";
            horarios.dia2 = "Monday";

            //newTurma.horarios = horarios;
            // TODO: validar se da p salvar no dia
            // temp:
            //var unidade1 = Unidades.CGI;
            var turma = new Turma();

            var turmasExistetes = 0;// _queries.GetQuantidadeTurma(unidade.ToString()).GetAwaiter().GetResult();

            //Turno turno;
            //Enum.TryParse(newCurso.turno, out turno);
            // turma.GerarIdentificador(unidade, turmasExistetes);
            //Mapper _mapper = new Mapper()
            //var modulo1 = _mapper.Map<ModuloDto, Modulo>(newTurma.modulo);

            //turma.Factory(modulo1, newTurma.vagas, newTurma.minVagas, unidade.Id, unidade.Sigla, turmasExistetes, newTurma.prevInicio_1, newTurma.prevInicio_2, newTurma.prevInicio_3,
            //    newTurma.horarios.dia1, newTurma.horarios.dia2, newTurma.horarios.horarioIni_1, newTurma.horarios.horarioFim_1, 1);


            //_turmaRepository.AddTurma(turma);
            context.Turmas.Add(turma);
            //DbSet.Add(turma);
            context.SaveChanges();


            var turmaPedag = new TurmaPedagogico();
            turmaPedag.CreateTurmaPedagogico(turma.Id, idModuloCriado);
            //_turmaPedagRepository.CreateTurmaPedag(turmaPedag);
            context.TurmaPedag.Add(turmaPedag);
            context.SaveChanges();


            var materias = new List<Materia>();// _unidadeQueries.GetMaterias(newTurma.modulo.id).GetAwaiter().GetResult();

            var query = @"select * from materias where moduloId = " + idModuloCriado;
            //select * from agendaprovas where turmaId = 1070
            using (var connection = new SqlConnection(
                Configuration.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                materias = connection.Query<Materia>(query).ToList();


            }
            var listaPedagMAterias = new List<MateriaPedag>();

            foreach (var item in materias)
            {
                listaPedagMAterias.Add(new MateriaPedag(0, item.Descricao, item.Id, turmaPedag.Id));
            }

            context.ProfMaterias.AddRange(listaPedagMAterias);
            context.SaveChanges();
            #endregion
            */
        }

    }
}


