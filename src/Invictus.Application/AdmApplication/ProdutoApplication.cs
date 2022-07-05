using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Enumerations.Logs;
using Invictus.Core.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.AdmProduto.Interfaces;
using Invictus.Domain.Administrativo.Logs;
using Invictus.Domain.Administrativo.Models;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class ProdutoApplication : IProdutoApplication
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtolRepository;
        private readonly IProdutoQueries _produtoQueries;
        private readonly IAspNetUser _user;
        private readonly InvictusDbContext _db;
        public ProdutoApplication(IMapper mapper, IProdutoRepository produtolRepository, 
            IProdutoQueries produtoQueries, IAspNetUser user, InvictusDbContext db)
        {
            _mapper = mapper;
            _produtolRepository = produtolRepository;
            _produtoQueries = produtoQueries;
            _user = user;
            _db = db;
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
            var userId = _user.ObterUsuarioId();
            var unidadeId = _user.GetUnidadeIdDoUsuario();
            var produtoJson = JsonConvert.SerializeObject(produto);

            var logProduto = new LogProduto(DateTime.Now, LogProdutoAcao.Criacao, produto.Id, userId, produtoJson, "", unidadeId);
            _db.LogProdutos.Add(logProduto);

            _db.SaveChanges();


        }

        public async Task DoarEntreUnidades(DoacaoCommand command)
        {
            var produto = _mapper.Map<Produto>(await _produtoQueries.GetProdutobyId(command.produtoId));

            produto.RemoveProduto(command.qntDoada);

            await _produtolRepository.UpdateProduto(produto);

            var newProduto = new Produto("", produto.Nome, produto.Descricao, produto.Preco, produto.PrecoCusto, 
                command.qntDoada, 0, command.unidadeDonatariaId, DateTime.Now, produto.Ativo, produto.Observacoes);

            var qntProduto = await _produtoQueries.ProdutosCount();
            newProduto.AddCodigoProduto(qntProduto);

            await _produtolRepository.AddProduto(newProduto);

            _produtolRepository.Commit();
        }

        public async Task EditProduto(ProdutoDto editedProduto)
        {
            var oldProduct = await _produtoQueries.GetProdutobyId(editedProduto.id);           

            var produto = _mapper.Map<Produto>(editedProduto);

            await _produtolRepository.UpdateProduto(produto);
            _produtolRepository.Commit();

            // TOG
            var userId = _user.ObterUsuarioId();
            var unidadeId = _user.GetUnidadeIdDoUsuario();
            var produtoJson = JsonConvert.SerializeObject(produto);

            var logProduto = new LogProduto(DateTime.Now, LogProdutoAcao.Edicao, produto.Id, userId, produtoJson, "", unidadeId);

            _db.LogProdutos.Add(logProduto);

            if (oldProduct.quantidade != editedProduto.quantidade)
            {
                if(editedProduto.quantidade > oldProduct.quantidade)
                {
                    var log = new LogProduto(DateTime.Now, LogProdutoAcao.AumentoEstoque, produto.Id, userId, produtoJson, "", unidadeId);

                    _db.LogProdutos.Add(log);
                }
                else
                {
                    var log = new LogProduto(DateTime.Now, LogProdutoAcao.DiminuicaoEstoque, produto.Id, userId, produtoJson, "", unidadeId);
                    _db.LogProdutos.Add(log);
                }
            }
            // ver se teve mudança no estoque

            _db.SaveChanges();
        }
    }
}
