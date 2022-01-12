using AutoMapper;
using Invictus.Application.AdmApplication;
using Invictus.Core.Enumerations;
using Invictus.Core.Enums;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Invictus.Domain.Administrativo.ContratoAggregate;
using Invictus.Domain.Administrativo.ContratosAggregate;
using Invictus.Domain.Administrativo.DocumentacaoTemplateAggregate;
using Invictus.Domain.Administrativo.Models;
using Invictus.Domain.Administrativo.PacoteAggregate;
using Invictus.Domain.Administrativo.Parametros;
using Invictus.Domain.Administrativo.ProfessorAggregate;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Administrativo.UnidadeAggregate;
using Invictus.Domain.Financeiro;
using Invictus.Domain.Padagogico.NotasTurmas;
using Invictus.Domain.Pedagogico.AlunoAggregate;
using Invictus.Domain.Pedagogico.Responsaveis;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.Financeiro;
using Invictus.Dtos.PedagDto;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Invictus.Application.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));
        }
    }

    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
        }
    }

    public class ViewModelToDomainMappingProfile : Profile
    {

        public ViewModelToDomainMappingProfile()
        {

            CreateMap<UnidadeDto, Unidade>()
                 .ConstructUsing(u => new Unidade(u.sigla, u.cnpj, u.descricao, u.ativo,// (c.Nome, c.DescricaoCurta, c.DescricaoLonga, c.DataInicio, c.DataFim, c.Gratuito, c.Valor, c.Online, c.NomeEmpresa, c.OrganizadorId, c.CategoriaId,
                     new Endereco(u.bairro, u.cep, u.complemento, u.logradouro, u.numero, u.cidade, u.uf)));// (c.Endereco.Id, c.Endereco.Logradouro, c.Endereco.Numero, c.Endereco.Complemento, c.Endereco.Bairro, c.Endereco.CEP, c.Endereco.Cidade, c.Endereco.Estado, c.Id)));

            CreateMap<SalaDto, Sala>();

            CreateMap<ColaboradorDto, Colaborador>()
                 .ConstructUsing(c => new Colaborador(c.nome, c.email, c.cpf, c.celular, c.cargoId, c.unidadeId, c.ativo,// (u.sigla, u.descricao, u.ativo,// (c.Nome, c.DescricaoCurta, c.DescricaoLonga, c.DataInicio, c.DataFim, c.Gratuito, c.Valor, c.Online, c.NomeEmpresa, c.OrganizadorId, c.CategoriaId,
                     new ColaboradorEndereco(c.bairro, c.cep, c.complemento, c.logradouro, c.numero, c.cidade, c.uf)));// (c.Endereco.Id, c.Endereco.Logradouro, c.Endereco.Numero, c.Endereco.Complemento, c.Endereco.Bairro, c.Endereco.CEP, c.Endereco.Cidade, c.Endereco.Estado, c.Id)));

            CreateMap<AlunoDto, Aluno>()
                 .ConstructUsing(c => new Aluno(c.nome, c.nomeSocial, c.cpf, c.rg, c.nomePai, c.nomeMae, c.nascimento, c.naturalidade, c.naturalidadeUF,
                 c.email, c.telReferencia, c.nomeContatoReferencia, c.telCelular, c.telResidencial, c.telWhatsapp,// (u.sigla, u.descricao, u.ativo,// (c.Nome, c.DescricaoCurta, c.DescricaoLonga, c.DataInicio, c.DataFim, c.Gratuito, c.Valor, c.Online, c.NomeEmpresa, c.OrganizadorId, c.CategoriaId,
                     new AlunoEndereco(c.bairro, c.cep, c.complemento, c.logradouro, c.numero, c.cidade, c.uf)));// (c.Endereco.Id, c.Endereco.Logradouro, c.Endereco.Numero, c.Endereco.Complemento, c.Endereco.Bairro, c.Endereco.CEP, c.Endereco.Cidade, c.Endereco.Estado, c.Id)));

            CreateMap<MatForm, Responsavel>()
                 .ConstructUsing(c => new Responsavel(TipoResponsavel.TryParse(c.tipo), c.nome, c.cpf, c.rg, c.nascimento, c.naturalidade, c.naturalidadeUF,
                 c.email, c.telCelular, c.telResidencial, c.telWhatsapp, c.matriculaId,// (u.sigla, u.descricao, u.ativo,// (c.Nome, c.DescricaoCurta, c.DescricaoLonga, c.DataInicio, c.DataFim, c.Gratuito, c.Valor, c.Online, c.NomeEmpresa, c.OrganizadorId, c.CategoriaId,
                     new ResponsavelEndereco(c.bairro, c.cep, c.complemento, c.logradouro, c.numero, c.cidade, c.uf)));


            CreateMap<ProfessorDto, Professor>()
                 .ConstructUsing(c => new Professor(c.nome, c.email, c.cpf, c.celular, c.unidadeId, c.ativo,// (u.sigla, u.descricao, u.ativo,// (c.Nome, c.DescricaoCurta, c.DescricaoLonga, c.DataInicio, c.DataFim, c.Gratuito, c.Valor, c.Online, c.NomeEmpresa, c.OrganizadorId, c.CategoriaId,
                     new ProfessorEndereco(c.bairro, c.cep, c.complemento, c.logradouro, c.numero, c.cidade, c.uf),
                     new DadosBancarios(c.bancoNumero, c.agencia, c.conta, TipoConta.TryParse(c.tipoConta))));// (c.Endereco.Id, c.Endereco.Logradouro, c.Endereco.Numero, c.Endereco.Complemento, c.Endereco.Bairro, c.Endereco.CEP, c.Endereco.Cidade, c.Endereco.Estado, c.Id)));


            CreateMap<MateriaTemplateDto, MateriaTemplate>()
                .ConstructUsing(m => new MateriaTemplate(m.nome, m.descricao, ModalidadeCurso.TryParse(m.modalidade), m.cargaHoraria, m.typePacoteId, m.ativo));

            CreateMap<PacoteDto, Pacote>();

            CreateMap<PacoteMateriaDto, PacoteMateria>()
                .ConstructUsing(m => new PacoteMateria(m.nome, m.materiaId, ModalidadeCurso.TryParse(m.modalidade), m.cargaHoraria));

            CreateMap<DocumentacaoExigidaDto, DocumentacaoExigencia>()
                .ConstructUsing(d => new DocumentacaoExigencia(d.descricao, d.comentario, TitularDoc.TryParse(d.titular), d.validadeDias, d.obrigatorioParaMatricula));

            CreateMap<DiasSemanaCommand, Horario>()
                .ConstructUsing(h => new Horario(DiaDaSemana.TryParseStringToString(h.diaSemana), h.horarioInicio, h.horarioFim));

            CreateMap<TurmaMateriasDto, TurmaMaterias>()
                .ConstructUsing(t => new TurmaMaterias(t.nome, t.descricao, ModalidadeCurso.TryParse(t.modalidade), t.cargaHoraria, t.typePacoteId, t.materiaId, t.ativo));

            CreateMap<ParametrosKeyDto, ParametrosKey>();

            CreateMap<ParametroValueDto, ParametrosValue>()
                .ConstructUsing(p => new ParametrosValue(p.value, p.descricao, p.comentario, p.parametrosKeyId));

            CreateMap<TurmaDto, Turma>()
                .ConstructUsing(t => new Turma(t.descricao, t.totalAlunos, t.minimoAlunos, t.unidadeId, t.salaId, t.pacoteId, t.typePacoteId,
                new Previsao(t.previsaoAtual, t.previsaoTerminoAtual, t.previsaoInfo, t.dataCriacao)));

            CreateMap<AnotacaoDto, AlunoAnotacao>();

            CreateMap<AlunoDocumentoDto, AlunoDocumento>();

            CreateMap<DisponibilidadeDto, Disponibilidade>();

            CreateMap<TurmaProfessoresDto, TurmaProfessor>();

            CreateMap<TurmaNotasDto, TurmaNotas>();

            CreateMap<BoletoLoteResponse, BoletoResponseInfo>()
                .ConstructUsing(b => new BoletoResponseInfo(b.id_unico, b.id_unico_original, b.status, b.msg, b.nossonumero, b.linkBoleto, b.linkGrupo, b.linhaDigitavel,
                b.pedido_numero, b.banco_numero, b.token_facilitador, b.credencial));

            CreateMap<ContratoView, Contrato>();
            CreateMap<ContratoDto, Contrato>();
            CreateMap<ContratoConteudoDto, Conteudo>();
            CreateMap<ProdutoDto, Produto>();
            CreateMap<PlanoPagamentoDto, PlanoPagamentoTemplate>();
            CreateMap<AgendaTrimestreDto, AgendaTrimestre>();
            CreateMap<DocumentacaoTemplateDto, DocumentacaoTemplate>();

            CreateMap<AlunoExcelDto, Aluno>()
                 .ConstructUsing(c => new Aluno(c.Nome, c.NomeSocial, c.CPF, c.RG, c.NomePai, c.NomeMae, c.Nascimento, c.Naturalidade, c.NaturalidadeUF,
                 c.Email, c.TelReferencia, c.NomeContatoReferencia, c.TelCelular, c.TelResidencial, c.TelWhatsapp,// (u.sigla, u.descricao, u.ativo,// (c.Nome, c.DescricaoCurta, c.DescricaoLonga, c.DataInicio, c.DataFim, c.Gratuito, c.Valor, c.Online, c.NomeEmpresa, c.OrganizadorId, c.CategoriaId,
                     new AlunoEndereco(c.Bairro, c.CEP, c.Complemento, c.Logradouro, c.Numero, c.Cidade, c.UF)));// (c.Endereco.Id, c.Endereco.Logradouro, c.Endereco.Numero, c.Endereco.Complemento, c.Endereco.Bairro, c.Endereco.CEP, c.Endereco.Cidade, c.Endereco.Estado, c.Id)));

        }
    }
}