using AutoMapper;
using Invictus.Application.AdmApplication;
using Invictus.Core.Enumerations;
using Invictus.Core.Enums;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Administrativo.Calendarios;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Invictus.Domain.Administrativo.ContratoAggregate;
using Invictus.Domain.Administrativo.ContratosAggregate;
using Invictus.Domain.Administrativo.DocumentacaoTemplateAggregate;
using Invictus.Domain.Administrativo.FuncionarioAggregate;
using Invictus.Domain.Administrativo.Models;
using Invictus.Domain.Administrativo.PacoteAggregate;
using Invictus.Domain.Administrativo.Parametros;
using Invictus.Domain.Administrativo.ProfessorAggregate;
using Invictus.Domain.Administrativo.RequerimentoAggregate;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Administrativo.UnidadeAggregate;
using Invictus.Domain.Comercial;
using Invictus.Domain.Financeiro;
using Invictus.Domain.Financeiro.Bolsas;
using Invictus.Domain.Financeiro.Configuracoes;
using Invictus.Domain.Financeiro.Fornecedores;
using Invictus.Domain.Padagogico.Estagio;
using Invictus.Domain.Padagogico.NotasTurmas;
using Invictus.Domain.Padagogico.Requerimento;
using Invictus.Domain.Pedagogico.AlunoAggregate;
using Invictus.Domain.Pedagogico.Responsaveis;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.Comercial;
using Invictus.Dtos.Financeiro;
using Invictus.Dtos.Financeiro.Configuracoes;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries;
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
                 .ConstructUsing(u => new Unidade(u.sigla, u.cnpj, u.descricao, u.ativo, u.isUnidadeGlobal,// (c.Nome, c.DescricaoCurta, c.DescricaoLonga, c.DataInicio, c.DataFim, c.Gratuito, c.Valor, c.Online, c.NomeEmpresa, c.OrganizadorId, c.CategoriaId,
                     new Invictus.Domain.Administrativo.UnidadeAggregate.Endereco(u.bairro, u.cep, u.complemento, u.logradouro, u.numero, u.cidade, u.uf)));// (c.Endereco.Id, c.Endereco.Logradouro, c.Endereco.Numero, c.Endereco.Complemento, c.Endereco.Bairro, c.Endereco.CEP, c.Endereco.Cidade, c.Endereco.Estado, c.Id)));

            //CreateMap<ColaboradorDto, Colaborador>()
            //     .ConstructUsing(c => new Colaborador(c.nome, c.email, c.cpf, c.celular, c.cargoId, c.unidadeId, c.ativo,// (u.sigla, u.descricao, u.ativo,// (c.Nome, c.DescricaoCurta, c.DescricaoLonga, c.DataInicio, c.DataFim, c.Gratuito, c.Valor, c.Online, c.NomeEmpresa, c.OrganizadorId, c.CategoriaId,
            //         new ColaboradorEndereco(c.bairro, c.cep, c.complemento, c.logradouro, c.numero, c.cidade, c.uf)));// (c.Endereco.Id, c.Endereco.Logradouro, c.Endereco.Numero, c.Endereco.Complemento, c.Endereco.Bairro, c.Endereco.CEP, c.Endereco.Cidade, c.Endereco.Estado, c.Id)));

            //CreateMap<ColaboradorDto, Pessoa>()
            //     .ConstructUsing(c => Pessoa.ColaboradorFactory(c.nome, c.email, c.cpf, c.celular, c.cargoId, c.unidadeId, c.ativo,
            //     c.bairro, c.cep, c.complemento, c.logradouro, c.numero, c.cidade, c.uf));// (c.Endereco.Id, c.Endereco.Logradouro, c.Endereco.Numero, c.Endereco.Complemento, c.Endereco.Bairro, c.Endereco.CEP, c.Endereco.Cidade, c.Endereco.Estado, c.Id)));


            //CreateMap<AlunoDto, Aluno>()
            //     .ConstructUsing(c => new Aluno(c.nome, c.nomeSocial, c.cpf, c.rg, c.nomePai, c.nomeMae, c.nascimento, c.naturalidade, c.naturalidadeUF,
            //     c.email, c.telReferencia, c.nomeContatoReferencia, c.telCelular, c.telResidencial, c.telWhatsapp,// (u.sigla, u.descricao, u.ativo,// (c.Nome, c.DescricaoCurta, c.DescricaoLonga, c.DataInicio, c.DataFim, c.Gratuito, c.Valor, c.Online, c.NomeEmpresa, c.OrganizadorId, c.CategoriaId,
            //         new AlunoEndereco(c.bairro, c.cep, c.complemento, c.logradouro, c.numero, c.cidade, c.uf)));// (c.Endereco.Id, c.Endereco.Logradouro, c.Endereco.Numero, c.Endereco.Complemento, c.Endereco.Bairro, c.Endereco.CEP, c.Endereco.Cidade, c.Endereco.Estado, c.Id)));

            CreateMap<MatForm, Responsavel>()
                 .ConstructUsing(c => new Responsavel(TipoResponsavel.TryParse(c.tipo), c.nome, c.parentesco, c.cpf, c.rg, c.nascimento, c.naturalidade, c.naturalidadeUF,
                 c.email, c.telCelular, c.telResidencial, c.telWhatsapp, c.matriculaId,// (u.sigla, u.descricao, u.ativo,// (c.Nome, c.DescricaoCurta, c.DescricaoLonga, c.DataInicio, c.DataFim, c.Gratuito, c.Valor, c.Online, c.NomeEmpresa, c.OrganizadorId, c.CategoriaId,
                     new ResponsavelEndereco(c.bairro, c.cep, c.complemento, c.logradouro, c.numero, c.cidade, c.uf)));

            //CreateMap<ProfessorDto, Professor>()
            //     .ConstructUsing(c => new Professor(c.nome, c.email, c.cpf, c.celular, c.cnpj, c.telefoneContato, c.nomeContato, c.unidadeId, c.ativo,// (u.sigla, u.descricao, u.ativo,// (c.Nome, c.DescricaoCurta, c.DescricaoLonga, c.DataInicio, c.DataFim, c.Gratuito, c.Valor, c.Online, c.NomeEmpresa, c.OrganizadorId, c.CategoriaId,
            //         new ProfessorEndereco(c.bairro, c.cep, c.complemento, c.logradouro, c.numero, c.cidade, c.uf),
            //         new DadosBancarios(c.bancoNumero, c.agencia, c.conta, TipoConta.TryParse(c.tipoConta))));// (c.Endereco.Id, c.Endereco.Logradouro, c.Endereco.Numero, c.Endereco.Complemento, c.Endereco.Bairro, c.Endereco.CEP, c.Endereco.Cidade, c.Endereco.Estado, c.Id)));

            CreateMap<MateriaTemplateDto, MateriaTemplate>()
                .ConstructUsing(m => new MateriaTemplate(m.nome, m.descricao, ModalidadeCurso.TryParse(m.modalidade), m.cargaHoraria, m.typePacoteId, m.ativo));

            CreateMap<PacoteMateriaDto, PacoteMateria>()
                .ConstructUsing(m => new PacoteMateria(m.nome, m.materiaId, ModalidadeCurso.TryParse(m.modalidade), m.cargaHoraria));

            CreateMap<DocumentacaoExigidaDto, DocumentacaoExigencia>()
                .ConstructUsing(d => new DocumentacaoExigencia(d.descricao, d.comentario, TitularDoc.TryParse(d.titular), d.validadeDias, d.obrigatorioParaMatricula));

            CreateMap<DiasSemanaCommand, Horario>()
                .ConstructUsing(h => new Horario(DiaDaSemana.TryParseStringToString(h.diaSemana), h.horarioInicio, h.horarioFim));

            CreateMap<TurmaMateriasDto, TurmaMaterias>()
                .ConstructUsing(t => new TurmaMaterias(t.nome, t.descricao, ModalidadeCurso.TryParse(t.modalidade), t.cargaHoraria, t.typePacoteId, t.materiaId, t.ordem, t.ativo));

            CreateMap<ParametroValueDto, ParametrosValue>()
                .ConstructUsing(p => new ParametrosValue(p.value, p.descricao, p.comentario, p.ativo, p.parametrosKeyId));

            CreateMap<TurmaDto, Turma>()
                .ConstructUsing(t => new Turma(t.descricao, t.totalAlunos, t.minimoAlunos, t.unidadeId, t.salaId, t.pacoteId, t.typePacoteId,
                new Previsao(t.previsaoAtual, t.previsaoTerminoAtual, t.previsaoInfo, t.dataCriacao)));

            CreateMap<ListaPresencaDto, Presenca>()
                .ConstructUsing(p => new Presenca(p.calendarioId, p.isPresent, p.alunoId, p.matriculaId, p.isPresentToString));

            CreateMap<ListaPresencaViewModel, Presenca>()
                .ConstructUsing(p => new Presenca(p.id, p.calendarioId, p.isPresent, p.alunoId, p.matriculaId, p.isPresentToString));

            CreateMap<RequerimentoDto, Requerimento>()
                .ConstructUsing(src => new Requerimento(src.matriculaRequerenteId, src.dataRequerimento, src.descricao, src.observacao, src.unidadeId));

            CreateMap<TypeEstagioDto, TypeEstagio>()
                .ConstructUsing(src => new TypeEstagio(src.id, src.nome, src.observacao, src.nivel, src.ativo));

            CreateMap<BoletoLoteResponse, BoletoResponseInfo>()
                .ConstructUsing(b => new BoletoResponseInfo(b.id_unico, b.id_unico_original, b.status, b.msg, b.nossonumero, b.linkBoleto, b.linkGrupo, b.linhaDigitavel,
                b.pedido_numero, b.banco_numero, b.token_facilitador, b.credencial));

            CreateMap<ResponsavelDto, Responsavel>()
                 .ConstructUsing(c => new Responsavel(TipoResponsavel.TryParse(c.tipo), c.nome, c.parentesco, c.cpf, c.rg, c.nascimento, c.naturalidade, c.naturalidadeUF,
                 c.email, c.telCelular, c.telResidencial, c.telWhatsapp, c.matriculaId,// (u.sigla, u.descricao, u.ativo,// (c.Nome, c.DescricaoCurta, c.DescricaoLonga, c.DataInicio, c.DataFim, c.Gratuito, c.Valor, c.Online, c.NomeEmpresa, c.OrganizadorId, c.CategoriaId,
                     new ResponsavelEndereco(c.bairro, c.cep, c.complemento, c.logradouro, c.numero, c.cidade, c.uf)));

            //CreateMap<AlunoExcelDto, Aluno>()
            //     .ConstructUsing(c => new Aluno(c.Nome, c.NomeSocial, c.CPF, c.RG, c.NomePai, c.NomeMae, c.Nascimento, c.Naturalidade, c.NaturalidadeUF,
            //     c.Email, c.TelReferencia, c.NomeContatoReferencia, c.TelCelular, c.TelResidencial, c.TelWhatsapp,// (u.sigla, u.descricao, u.ativo,// (c.Nome, c.DescricaoCurta, c.DescricaoLonga, c.DataInicio, c.DataFim, c.Gratuito, c.Valor, c.Online, c.NomeEmpresa, c.OrganizadorId, c.CategoriaId,
            //         new AlunoEndereco(c.Bairro, c.CEP, c.Complemento, c.Logradouro, c.Numero, c.Cidade, c.UF)));// (c.Endereco.Id, c.Endereco.Logradouro, c.Endereco.Numero, c.Endereco.Complemento, c.Endereco.Bairro, c.Endereco.CEP, c.Endereco.Cidade, c.Endereco.Estado, c.Id)));
            CreateMap<BoletoViewModel, Boleto>();

            CreateMap<BoletoResponseViewModel, BoletoResponseInfo>();

            CreateMap<PessoaDto, Pessoa>();

            CreateMap<EnderecoDto, Invictus.Domain.Administrativo.FuncionarioAggregate.Endereco>();

            CreateMap<ParametrosKeyDto, ParametrosKey>();

            CreateMap<BoletoViewModel, Boleto>();

            CreateMap<BoletoResponseViewModel, BoletoResponseInfo>();

            CreateMap<BoletoDto, Boleto>();

            CreateMap<LeadDto, Lead>();

            CreateMap<SalaDto, Sala>();

            CreateMap<PacoteDto, Pacote>();

            CreateMap<AnotacaoDto, AlunoAnotacao>();

            CreateMap<AlunoDocumentoDto, AlunoDocumento>();

            CreateMap<DisponibilidadeDto, Disponibilidade>();

            CreateMap<TurmaProfessoresDto, TurmaProfessor>();

            CreateMap<TipoRequerimentoDto, TipoRequerimento>();

            CreateMap<TurmaNotasDto, TurmaNotas>();

            CreateMap<CategoriaDto, Categoria>();
            CreateMap<TipoDto, Tipo>();

            //CreateMap<FornecedorDto, Fornecedor>();

            CreateMap<TurmaCalendarioViwModel, Calendario>();

            CreateMap<EstagioDto, Estagio>();

            CreateMap<ContratoView, Contrato>();

            CreateMap<BancoDto, Banco>();

            CreateMap<CentroCustoDto, CentroCusto>();

            CreateMap<MeioPagamentoDto, MeioPagamento>();

            CreateMap<PlanoContaDto, PlanoConta>();

            CreateMap<SubContaDto, SubConta>();

            CreateMap<FormaRecebimentoDto, FormaRecebimento>();

            CreateMap<BolsaDto, Bolsa>();

            CreateMap<ContratoDto, Contrato>();

            CreateMap<ContratoConteudoDto, Conteudo>();

            CreateMap<ProdutoDto, Produto>();

            CreateMap<PlanoPagamentoDto, PlanoPagamentoTemplate>();

            CreateMap<AgendaTrimestreDto, AgendaTrimestre>();

            CreateMap<DocumentacaoTemplateDto, DocumentacaoTemplate>();


        }
    }
}