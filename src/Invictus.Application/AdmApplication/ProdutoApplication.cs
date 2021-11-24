using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Domain.Administrativo.AdmProduto.Interfaces;
using Invictus.Domain.Administrativo.Models;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using System;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class ProdutoApplication : IProdutoApplication
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtolRepository;
        private readonly IProdutoQueries _produtoQueries;
        public ProdutoApplication(IMapper mapper, IProdutoRepository produtolRepository, 
            IProdutoQueries produtoQueries)
        {
            _mapper = mapper;
            _produtolRepository = produtolRepository;
            _produtoQueries = produtoQueries;
        }
        public async Task AddProduto(ProdutoDto newProduto)
        {
            var produto = _mapper.Map<Produto>(newProduto);

            //var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefaultAsync();
            var produtosCount = await _produtoQueries.ProdutosCount();// _db.Produtos.ToList();
            produto.AddCodigoProduto(produtosCount);
            //produto.SetUnidadeId(unidadeId);
            produto.SetDataCadastro();

            await _produtolRepository.AddProduto(produto);

            _produtolRepository.Commit();
            // TODO LOG QUEM SALVOU
        }

        public async Task DoarEntreUnidades(DoacaoCommand command)
        {
            var produto = _mapper.Map<Produto>(await _produtoQueries.GetProdutobyId(command.produtoId));

            produto.RemoveProduto(command.qntDoada);

            await _produtolRepository.UpdateProduto(produto);

            var newProduto = new Produto("", produto.Nome, produto.Descricao, produto.Preco, produto.PrecoCusto, 
                command.qntDoada, 0, command.unidadeDonatariaId, DateTime.Now, produto.Observacoes);

            var qntProduto = await _produtoQueries.ProdutosCount();
            newProduto.AddCodigoProduto(qntProduto);

            await _produtolRepository.AddProduto(newProduto);

            _produtolRepository.Commit();
        }

        public async Task EditProduto(ProdutoDto editedProduto)
        {
            var produto = _mapper.Map<Produto>(editedProduto);

            await _produtolRepository.UpdateProduto(produto);
            _produtolRepository.Commit();
        }
    }
}
