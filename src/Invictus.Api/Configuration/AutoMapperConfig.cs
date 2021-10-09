using AutoMapper;
using Invictus.Api.Controllers;
using Invictus.Application.Dtos;
using Invictus.Application.Dtos.Administrativo;
using Invictus.Application.Services;
using Invictus.Core.Enums;
using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Invictus.Domain.Administrativo.Models;
using Invictus.Domain.Administrativo.PacoteAggregate;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Administrativo.UnidadeAggregate;
using Invictus.Domain.Comercial.Leads;
using Invictus.Domain.Financeiro.Fornecedor;
using Invictus.Domain.Financeiro.NewFolder;
using Invictus.Domain.Financeiro.Transacoes;
using Invictus.Domain.Pedagogico.HistoricoEscolarAggregate;
using Invictus.Domain.Pedagogico.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Invictus.Api.Configuration
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
            CreateMap<Colaborador, ColaboradorDto>();
            CreateMap<Pacote, ModuloDto>();
            CreateMap<Sala, SalaDto>();
            //CreateMap<Pacote, PacoteDto>();
            CreateMap<PlanoPagamento, PlanoPagamentoDto>();
        }
    }

    public class ViewModelToDomainMappingProfile : Profile
    {

        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ColaboradorDto, Colaborador>();
            CreateMap<LeadDto, Lead>();
            CreateMap<CreateCursoDto, Turma>();
            CreateMap<AlunoDto, Aluno>();
            CreateMap<RespFinancDto, Responsavel>();
            CreateMap<RespMenorDto, Responsavel>();
            CreateMap<ModuloDto, Pacote>();
            CreateMap<AgendasProvasDto, ProvasAgenda>();
            CreateMap<EstagioDto, Estagio>();
            CreateMap<SalaDto, Sala>();
            CreateMap<ProdutoDto, Produto>();
            CreateMap<UnidadeDto, Unidade>();
            CreateMap<FornecedorDto, Fornecedor>();
            CreateMap<AgendasProvasDto, ProvasAgenda>();
            CreateMap<NotasDisciplinasDto, NotasDisciplinas>();
            CreateMap<NotasDisciplinasDtoV2, NotasDisciplinas>();
            CreateMap<FornecedorSaidaDto, FornecedorSaida>();
            CreateMap<BoletoLoteResponse, Boleto>();
            CreateMap<MateriaDto, Materia>();
            CreateMap<PlanoPagamentoDto, PlanoPagamento>();
            CreateMap<AlunoExcelDto, Aluno>();
            CreateMap<CargoDto, Cargo>();
            CreateMap<DocumentacaoExigidaDto, DocumentacaoExigencia>();
            //.ForMember(dest => dest.MeioPagamento, opt => opt.MapFrom<CustomResolver>());


            //CreateMap<string, int>().ConvertUsing(s => Convert.ToInt32(s));
            // CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
            //CreateMap<string, ModalidadeCurso.>().ConvertUsing<TypeTypeConverter>();


        }


    }

    //public class TypeTypeConverter : ITypeConverter<string, Type>
    //{
    //    public Type Convert(string source, Type destination, ResolutionContext context)
    //    {
    //        return Assembly.GetExecutingAssembly().GetType(source);
    //    }
    //}



    //public interface ITypeConverter<in TSource, TDestination>
    //{
    //    TDestination Convert(TSource source, TDestination destination, ResolutionContext context);
    //}

    //public class CustomResolver : IValueResolver<FornecedorSaidaDto, FornecedorSaida, MeioPagamento>
    //{
    //    public MeioPagamento Resolve(FornecedorSaidaDto source, FornecedorSaida destination, MeioPagamento member, ResolutionContext context)
    //    {

    //        var meioPag = MeioPagamento.TryParse(source.meioPagamento);
    //        return meioPag;
    //    }
    //}

    //public interface IValueResolver<in TSource, in TDestination, TDestMember>
    //{
    //    TDestMember Resolve(TSource source, TDestination destination, TDestMember destMember, ResolutionContext context);
    //}
}