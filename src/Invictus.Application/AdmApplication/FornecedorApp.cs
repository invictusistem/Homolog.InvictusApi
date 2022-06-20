using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.FuncionarioAggregate;
using Invictus.Domain.Administrativo.FuncionarioAggregate.Interfaces;
using Invictus.Domain.Financeiro.Fornecedores.Interfaces;
using Invictus.Dtos.AdmDtos;
using System;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class FornecedorApp : IFornecedorApp
    {
        private readonly IFornecedorRepo _fornecedorRepo;
        private readonly IMapper _mapper;
        private readonly IAspNetUser _aspNetUser;
        private readonly IPessoaRepo _pessoaRepo;
        public FornecedorApp(IFornecedorRepo fornecedorRepo, IMapper mapper, IAspNetUser aspNetUser, IPessoaRepo pessoaRepo)
        {
            _fornecedorRepo = fornecedorRepo;
            _mapper = mapper;
            _aspNetUser = aspNetUser;
            _pessoaRepo = pessoaRepo;
        }

        public async Task CreateFornecedor(PessoaDto newFornecedor)
        {
            newFornecedor.razaoSocial = newFornecedor.nome;

            newFornecedor.unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();

            newFornecedor.dataCadastro = DateTime.Now;

            newFornecedor.pessoaRespCadastroId = _aspNetUser.ObterUsuarioId();

            var fornecedor = _mapper.Map<Pessoa>(newFornecedor);

            fornecedor.SetTipoPessoa(TipoPessoa.Fornecedor);

            await _pessoaRepo.AddPessoa(fornecedor);

            _pessoaRepo.Commit();
        }

        public async Task UpdateFornecedor(PessoaDto editedFornecedor)
        {
            var fornecedor = _mapper.Map<Pessoa>(editedFornecedor);

            await _pessoaRepo.EditPessoa(fornecedor);

            _pessoaRepo.Commit();
        }
    }
}
