using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Interfaces;
using Invictus.Domain.Financeiro.Fornecedores;
using Invictus.Domain.Financeiro.Fornecedores.Interfaces;
using Invictus.Dtos.Financeiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class FornecedorApp : IFornecedorApp
    {
        private readonly IFornecedorRepo _fornecedorRepo;
        private readonly IMapper _mapper;
        private readonly IAspNetUser _aspNetUser;
        public FornecedorApp(IFornecedorRepo fornecedorRepo, IMapper mapper, IAspNetUser aspNetUser)
        {
            _fornecedorRepo = fornecedorRepo;
            _mapper = mapper;
            _aspNetUser = aspNetUser;
        }

        public async Task CreateFornecedor(FornecedorDto newFornecedor)
        {
            newFornecedor.unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();

            var fornecedor = _mapper.Map<Fornecedor>(newFornecedor);

            await _fornecedorRepo.SaveFornecedor(fornecedor);

            _fornecedorRepo.Commit();
        }

        public async Task UpdateFornecedor(FornecedorDto editedFornecedor)
        {
            var fornecedor = _mapper.Map<Fornecedor>(editedFornecedor);
            
            await _fornecedorRepo.Edit(fornecedor);

            _fornecedorRepo.Commit();
        }
    }
}
