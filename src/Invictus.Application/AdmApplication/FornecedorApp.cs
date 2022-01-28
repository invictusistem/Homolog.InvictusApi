using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
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
        public FornecedorApp(IFornecedorRepo fornecedorRepo, IMapper mapper)
        {
            _fornecedorRepo = fornecedorRepo;
            _mapper = mapper;
        }

        public async Task CreateFornecedor(FornecedorDto newFornecedor)
        {
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
