using AutoMapper;
using Invictus.Application.Dtos;
using Invictus.Application.Dtos.Administrativo;
using Invictus.Application.Dtos.Financeiro;
using Invictus.Application.FinanceiroAppication.interfaces;
using Invictus.Application.Queries.Interfaces;
using Invictus.Core.Enums;
using Invictus.Data.Context;
using Invictus.Domain.Financeiro.Aluno;
using Invictus.Domain.Financeiro.Fornecedor;
using Invictus.Domain.Financeiro.NewFolder;
using Invictus.Domain.Financeiro.VendaProduto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [ApiController]
    // [Authorize(Roles = "SuperAdm")]// SuperAdm
    [Route("api/financeiro")]
    public class FinanceiroController : Controller
    {
        private readonly IFinanceiroQueries _financeiroQueries;
        private readonly IUnidadeQueries _unidadeQueries;
        private IFinanceiroApp _financApplication;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _userHttpContext;
        private readonly UserManager<IdentityUser> _userManager;
        private InvictusDbContext _db;
        private readonly string unidade;
        public FinanceiroController(IFinanceiroQueries financeiroQueries, InvictusDbContext db, IMapper mapper, IHttpContextAccessor userHttpContext,
            IFinanceiroApp financApplication, UserManager<IdentityUser> userManager, IUnidadeQueries unidadeQueries)
        {
            _financeiroQueries = financeiroQueries;
            _mapper = mapper;
            _db = db;
            _userHttpContext = userHttpContext;
            unidade = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
            _userManager = userManager;
            _financApplication = financApplication;
            _unidadeQueries = unidadeQueries;
        }

        #region GET


        [HttpGet]
        [Route("alunos")]
        //public async Task<IActionResult> BuscarCadastroAluno([FromQuery] string email, [FromQuery] string cpf, [FromQuery] string nome)
        //public async Task<IActionResult> BuscarCadastroAluno(string email, int cpf, string nome)
        public async Task<IActionResult> BuscarCadastroAluno([FromQuery] string query)
        {
            var param = JsonConvert.DeserializeObject<QueryDto>(query);
            var pessoas = await _financeiroQueries.GetUsuarios(param);// _matriculaQueries.BuscaAlunos(param.email, param.cpf, param.nome);

            //BindCPF(ref pessoas);

            return Ok();
        }

        [HttpGet]
        [Route("alunosfin")]
        public async Task<IActionResult> BuscarCadastroAlunoFin([FromQuery] int itemsPerPage, [FromQuery] int currentPage, [FromQuery] string paramsJson)
        {
            var param = JsonConvert.DeserializeObject<ParametrosDTO>(paramsJson);
            var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefaultAsync();
            var pessoas = await _financeiroQueries.GetAlunoFin(itemsPerPage, currentPage, param, unidadeId);// _matriculaQueries.BuscaAlunos(param.email, param.cpf, param.nome);
            //  var pessoas = await _matriculaQueries.BuscaAlunos(itemsPerPage, currentPage, parametros, unidadeId);
            pessoas.Data = SetCPFBind(pessoas.Data);
            return Ok(pessoas);
            //return Ok(pessoas);
        }

        public List<AlunoDto> SetCPFBind(List<AlunoDto> datas)
        {
            foreach (var data in datas)
            {   

                var newValue = "***.***." + data.cpf.Substring(6, 3) + "-**";

                data.cpf = newValue;

            }
            //var newValue = "***.***." + CPF.Substring(6, 3) + "-**";

            //CPF = newValue;

            return datas;
        }

        [HttpGet]
        [Route("aluno-debitos/{alunoId}")]
        //public async Task<IActionResult> BuscarCadastroAluno([FromQuery] string email, [FromQuery] string cpf, [FromQuery] string nome)
        //public async Task<IActionResult> BuscarCadastroAluno(string email, int cpf, string nome)
        public async Task<IActionResult> AlunoDebitos(int alunoId)
        {
            //var param = JsonConvert.DeserializeObject<QueryDto>(query);
            //var pessoas = await _financeiroQueries.GetAlunoFin(param);// _matriculaQueries.BuscaAlunos(param.email, param.cpf, param.nome);

            var debst = await _db.InfoFinanceiras.Include(c => c.Debitos).Where(c => c.AlunoId == alunoId).FirstOrDefaultAsync();
            //BindCPF(ref pessoas);

            return Ok(debst);
        }

        [HttpGet]
        [Route("aluno-debitos/v2/{alunoId}")]
        public async Task<IActionResult> AlunoDebitosV2(int alunoId)
        {
            //var param = JsonConvert.DeserializeObject<QueryDto>(query);
            //var pessoas = await _financeiroQueries.GetAlunoFin(param);// _matriculaQueries.BuscaAlunos(param.email, param.cpf, param.nome);
            var turmaId = await _db.Matriculados.Where(m => m.AlunoId == alunoId).Select(m => m.TurmaId).SingleOrDefaultAsync();
            if (turmaId == 0) return NotFound(new { message = "O aluno não está matriculado em nenhuma turma." });
            var debitos = await _financeiroQueries.GetDebitoAlunos(alunoId, turmaId);
            var dateNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            foreach (var deb in debitos)
            {
                var debito = await _db.Debitos.FindAsync(deb.id);
                if (debito.Status == StatusPagamento.EmAberto.DisplayName)
                {
                    if (debito.DataVencimento < dateNow)
                    {
                        debito.ChangeStatusToVencido();
                        _db.Debitos.Update(debito);
                        _db.SaveChanges();
                    }
                }
            }

            //turmaId
            // TODO? set and update Status
            //var debst = await _db.InfoFinanceiras.Include(c => c.Entrada).Include(c => c.Debitos).Where(c => c.AlunoId == alunoId).FirstOrDefaultAsync();
            //BindCPF(ref pessoas);
            //turmaId == 0 ? true : false;
            return Ok(new { debitos = debitos.OrderBy(c => c.competencia) });
        }

        // PRODUTOS aluno-getboletomoq
        [HttpGet]
        [Route("aluno-debitos/aluno-getboletomoq/{boletoId}")]
        public IActionResult AlunoBoleto(int boletoId)
        {

            var urlBoleto = _db.Boletos.Where(b => b.Id == boletoId).Select(b => b.linkBoleto).FirstOrDefault();

            var url = $"https://pagseguro.uol.com.br/checkout/payment/booklet/print.jhtml?c=7f5dfc4fc2cfdbfd61b54550808790ab79b442b8b1bf7f59130bb068b055791ceb069165f105b9af";

            return Ok(new { boletoUrl = urlBoleto });
        }


        [HttpGet]
        [Route("produtos")]
        public async Task<IActionResult> Produtos()
        {
            //var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefaultAsync();

            var produtos = await _unidadeQueries.GetProdutosViewModel();//  _db.Produtos.ToListAsync();

            return Ok(produtos);
        }

        [HttpGet]
        [Route("produtos-buscar/{nome}")]
        public async Task<IActionResult> ProdutosBuscar(string nome)
        {
            var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefaultAsync();
            var produtos = await _financApplication.SearchProduct(nome, unidadeId);

            return Ok(produtos);
        }


        [HttpGet]
        [Route("cursos-buscar")]
        public async Task<IActionResult> CursosVendas([FromQuery] string start, [FromQuery] string end)
        {
            var rangeIni = Convert.ToDateTime(start);
            var rangeEnd = Convert.ToDateTime(end);

            rangeEnd = rangeEnd.AddMinutes(1439);

            var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefaultAsync();

            var vendas = await _financeiroQueries.GetBalancoCursos(rangeIni.ToString("yyyy-MM-dd HH:mm:ss"), rangeEnd.ToString("yyyy-MM-dd HH:mm:ss"), unidadeId);
            //Console.WriteL ine(start);
            return Ok(new { vendas = vendas, total = vendas.Sum(t => t.valorPago) });
        }


        [HttpGet]
        [Route("produtos-venda")]
        public async Task<IActionResult> ProdutosBuscar([FromQuery] string start, [FromQuery] string end)
        {
            var rangeIni = Convert.ToDateTime(start);
            var rangeEnd = Convert.ToDateTime(end);

            rangeEnd = rangeEnd.AddMinutes(1439);

            var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefaultAsync();

            var vendas = await _financeiroQueries.GetBalancoProdutos(rangeIni.ToString("yyyy-MM-dd HH:mm:ss"), rangeEnd.ToString("yyyy-MM-dd HH:mm:ss"), unidadeId);
            ////Console.WriteL ine(start);
            ///GetBalancoProdutos
            return Ok(new { vendas = vendas, total = vendas.Sum(c => c.valorTotal) });
        }

        #endregion

        #region POST


        [HttpPost]
        [Route("reparcelar")]
        public IActionResult SaveReparcelas([FromBody] ReparcelaCommand reparcelasCommand)
        {
            var debitosOld = new List<Debito>();
            foreach (var item in reparcelasCommand.debitosIds)
            {
                debitosOld.Add(_db.Debitos.Where(d => d.Id == item).FirstOrDefault());
            }
            foreach (var item in debitosOld)
            {
                item.ChangeStatusToReparcelado();
            }

            var newDebitos = new List<Debito>();
            var i = 0;
            foreach (var item in reparcelasCommand.parcelas)
            {
                newDebitos.Add(new Debito(0, item.vencimento, debitosOld[i].IdUnidadeResponsavel, StatusPagamento.EmAberto, reparcelasCommand.parcelas.ToArray()[i].valor,
                    reparcelasCommand.parcelas.ToArray()[i].vencimento, MeioPagamento.Boleto, i + 1, 0, "Reparcelamento", "Reparcelamento mensalidade"));
                i++;
            }

            if (reparcelasCommand.valorEntrada > 0)
            {
                newDebitos.Add(new Debito(0, reparcelasCommand.parcelas[0].vencimento, debitosOld[0].IdUnidadeResponsavel, StatusPagamento.Pago, reparcelasCommand.parcelas.ToArray()[0].valor,
                    reparcelasCommand.parcelas.ToArray()[0].vencimento, MeioPagamento.Dinheiro, 1, 0, "Reparcelamento", "Reparcelamento mensalidade"));

            }


            _db.Debitos.UpdateRange(debitosOld);
            _db.SaveChanges();
            var infoFin = _db.InfoFinanceiras.Include(i => i.Debitos).Where(d => d.Id == debitosOld[0].InfoFinancId).FirstOrDefault();
            infoFin.AddDebitos(newDebitos);
            _db.Debitos.AddRange(newDebitos);

            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("fornecedor-contas-pagar")]
        public IActionResult Save([FromQuery] int fornecedorId, [FromBody] FornecedorSaidaDto saida)
        {

            saida.dataCriacao = DateTime.Now;
            saida.fornecedorId = fornecedorId;
            saida.unidadeId = _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).SingleOrDefault();



            var fornecedorSaida = new FornecedorSaida(0, DateTime.Now, saida.dataVencimento, saida.valor, MeioPagamento.TryParse(saida.meioPagamento), StatusPagamento.EmAberto,
                saida.comentario, saida.descricaoTransacao, fornecedorId, saida.unidadeId);// _mapper.Map<FornecedorSaida>(saida);


            _db.FornecedoresSaidas.Add(fornecedorSaida);
            _db.SaveChanges();
            // TODO LOG QUEM SALVOU
            return Ok();
        }

        [HttpPost]
        [Route("fornecedor-contas-receber")]
        public IActionResult UpdateContasPagar([FromQuery] int fornecedorId, [FromBody] FornecedorEntradaDto saida)
        {

            saida.dataCriacao = DateTime.Now;
            saida.fornecedorId = fornecedorId;
            saida.unidadeId = _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).SingleOrDefault();



            var fornecedorEntrada = new FornecedorEntrada(0, DateTime.Now, saida.dataVencimento, saida.valor, saida.valorComDesconto, MeioPagamento.TryParse(saida.meioPagamento), StatusPagamento.EmAberto,
                saida.comentario, saida.descricaoTransacao, fornecedorId, saida.unidadeId);// _mapper.Map<FornecedorSaida>(saida);


            _db.FornecedoresEntradas.Add(fornecedorEntrada);
            _db.SaveChanges();
            // TODO LOG QUEM SALVOU
            return Ok();
        }

        [HttpPost]
        [Route("produto")]
        public async Task<IActionResult> Save(ProdutoDto newProduto)
        {
            var produto = _mapper.Map<Produto>(newProduto);

            var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefaultAsync();
            var produtosCount = _db.Produtos.ToList();
            produto.AddCodigoProduto(Convert.ToString(produtosCount.Count() + 1));
            produto.SetUnidadeId(unidadeId);
            produto.SetDataCadastro();

            _db.Produtos.Add(produto);
            _db.SaveChanges();
            // TODO LOG QUEM SALVOU
            return Ok();
        }


        [HttpPost]
        [Route("produto-venda")]
        public IActionResult Venda([FromBody] ProdutoVendaCommand vendaProdutoCommand)
        {
            //TODO
            // gerar a transação do cartao e salvar no banco com status
            // fazer isso antes de tudo
            // se der erro, informar na tela
            //; ; MeioPagamento meio;


            var meioPag = MeioPagamento.TryParse(vendaProdutoCommand.meioPagamento);
            //var unidadeId = _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefault();
            var unidadeId = _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefault();
            //var usuario =  _userManager.FindByEmailAsync(user.Email);
            var userId = _userHttpContext.HttpContext.User.FindFirst("ColaboradorId").Value;

            //var userId = _db.Colaboradores.Where(c => c.Email == usuarioEmail).Select(c => c.Id).FirstOrDefault();
            var totalParcelas = 1;
            if (vendaProdutoCommand.meioPagamento == "dinheiro" ||
               vendaProdutoCommand.meioPagamento == "debito" ||
               vendaProdutoCommand.meioPagamento == "pix")
            {
                totalParcelas = 1;
            }
            else
            {
                if (vendaProdutoCommand.parcelas != "vista")
                {
                    totalParcelas = Convert.ToInt32(vendaProdutoCommand.parcelas);
                }
            }



            var vendaAggregate = new VendaProdutoAggregate(0, DateTime.Now, vendaProdutoCommand.valorTotal, Convert.ToInt32(userId), unidadeId,
                vendaProdutoCommand.cpf_comprador, 1, meioPag, " auto cartao ou deb", totalParcelas);

            var produtos = new List<ProdutoVenda>();
            foreach (var item in vendaProdutoCommand.produtos)
            {
                var valorTotal = item.quantidadeComprada * item.preco;
                var produtoVenda = new ProdutoVenda(0, item.id, item.quantidadeComprada, item.preco,
                    valorTotal);
                produtos.Add(produtoVenda);

                var produto = _db.Produtos.Find(item.id);
                produto.RemoveProduto(item.quantidadeComprada);
                _db.Produtos.Update(produto);

            }

            vendaAggregate.ProdutosVenda.AddRange(produtos);

            _db.VendasProdutos.Add(vendaAggregate);

            _db.SaveChanges();


            return Ok();
        }

        [HttpPost]
        [Route("produto-venda-unidades/{unidadeCompradoraId}")]
        public IActionResult VendaUnidades(int unidadeCompradoraId, [FromBody] ProdutoVendaCommand vendaProdutoCommand)
        {
            //TODO
            // gerar a transação do cartao e salvar no banco com status
            // fazer isso antes de tudo
            // se der erro, informar na tela
            //; ; MeioPagamento meio;


            var meioPag = MeioPagamento.TryParse(vendaProdutoCommand.meioPagamento);
            //var unidadeId = _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefault();
            var unidadeId = _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefault();
            //var usuario =  _userManager.FindByEmailAsync(user.Email);
            var userId = _userHttpContext.HttpContext.User.FindFirst("ColaboradorId").Value;

            //var userId = _db.Colaboradores.Where(c => c.Email == usuarioEmail).Select(c => c.Id).FirstOrDefault();
            var totalParcelas = 1;
            if (vendaProdutoCommand.meioPagamento == "dinheiro" ||
               vendaProdutoCommand.meioPagamento == "debito" ||
               vendaProdutoCommand.meioPagamento == "pix")
            {
                totalParcelas = 1;
            }
            else
            {
                if (vendaProdutoCommand.parcelas != "vista")
                {
                    totalParcelas = Convert.ToInt32(vendaProdutoCommand.parcelas);
                }
            }



            var vendaAggregate = new VendaProdutoAggregate(0, DateTime.Now, vendaProdutoCommand.valorTotal, Convert.ToInt32(userId), unidadeId,
                vendaProdutoCommand.cpf_comprador, 1, meioPag, " auto cartao ou deb", totalParcelas);

            var produtos = new List<ProdutoVenda>();
            foreach (var item in vendaProdutoCommand.produtos)
            {
                var valorTotal = item.quantidadeComprada * item.preco;
                var produtoVenda = new ProdutoVenda(0, item.id, item.quantidadeComprada, item.preco,
                    valorTotal);
                produtos.Add(produtoVenda);

                var produto = _db.Produtos.Find(item.id);
                produto.RemoveProduto(item.quantidadeComprada);
                _db.Produtos.Update(produto);

            }

            vendaAggregate.VendaEntreUnidadesInfo(unidadeCompradoraId);

            vendaAggregate.ProdutosVenda.AddRange(produtos);

            _db.VendasProdutos.Add(vendaAggregate);

            _db.SaveChanges();


            return Ok();
        }


        [HttpPost]
        [Route("produto-doacao-unidades/{unidadeDonatariaId}")]
        public IActionResult DoacaoUnidades(int unidadeDonatariaId, [FromBody] ProdutoVendaCommand vendaProdutoCommand)
        {

            // pegar o produto mudar apenas o IdUnidade
            var produto = _db.Produtos.Find(vendaProdutoCommand.produtos[0].id);

            produto.SetUnidadeId(unidadeDonatariaId);

            _db.Produtos.Update(produto);

            _db.SaveChanges();

            //var meioPag = MeioPagamento.TryParse(vendaProdutoCommand.meioPagamento);
           
            //var unidadeId = _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefault();
            
            //var userId = _userHttpContext.HttpContext.User.FindFirst("ColaboradorId").Value;

           
            //var totalParcelas = 1;
            //if (vendaProdutoCommand.meioPagamento == "dinheiro" ||
            //   vendaProdutoCommand.meioPagamento == "debito" ||
            //   vendaProdutoCommand.meioPagamento == "pix")
            //{
            //    totalParcelas = 1;
            //}
            //else
            //{
            //    if (vendaProdutoCommand.parcelas != "vista")
            //    {
            //        totalParcelas = Convert.ToInt32(vendaProdutoCommand.parcelas);
            //    }
            //}

            //var vendaAggregate = new VendaProdutoAggregate(0, DateTime.Now, vendaProdutoCommand.valorTotal, Convert.ToInt32(userId), unidadeId,
            //    vendaProdutoCommand.cpf_comprador, 1, meioPag, " auto cartao ou deb", totalParcelas);

            //var produtos = new List<ProdutoVenda>();
            //foreach (var item in vendaProdutoCommand.produtos)
            //{
            //    var valorTotal = item.quantidadeComprada * item.preco;
            //    var produtoVenda = new ProdutoVenda(0, item.id, item.quantidadeComprada, item.preco,
            //        valorTotal);
            //    produtos.Add(produtoVenda);

            //    var produto = _db.Produtos.Find(item.id);
            //    produto.RemoveProduto(item.quantidadeComprada);
            //    _db.Produtos.Update(produto);

            //}

            //vendaAggregate.VendaEntreUnidadesInfo(unidadeCompradoraId);

            //vendaAggregate.ProdutosVenda.AddRange(produtos);

            //_db.VendasProdutos.Add(vendaAggregate);

            //_db.SaveChanges();


            return Ok();
        }


        #endregion

        #region PUT



        [HttpPut]
        [Route("boleto-pagar/{idDebito}")]
        public IActionResult BoletoPagar(int idDebito)
        {
            var debito = _db.Debitos.Find(idDebito);
            debito.PagarBoleto();


            //// TTOOOOOOOOOOODOOOOOOOOOOOO  //// MUDAR DADOS TABELA TRANSACAO
            _db.Debitos.Update(debito);
            _db.SaveChanges();

            return Ok();
        }


        [HttpPut]
        [Route("produto")]
        public IActionResult Edit(ProdutoDto editedProduto)
        {
            var produto = _mapper.Map<Produto>(editedProduto);

            //var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefaultAsync();
            //var produtosCount = _db.Produtos.ToList();
            //produto.AddCodigoProduto(Convert.ToString(produtosCount.Count() + 1));
            //produto.SetUnidadeId(unidadeId);
            //produto.SetDataCadastro();

            _db.Produtos.Update(produto);
            _db.SaveChanges();
            // TODO LOG QUEM SALVOU
            return Ok();
        }

        #endregion
    }

    public class ProdutoVendaCommand
    {
        public string meioPagamento { get; set; }
        public string parcelas { get; set; }
        public decimal valorTotal { get; set; }
        public string cpf_comprador { get; set; }
        public string matriculaAluno { get; set; }
        public List<ProdutosCommand> produtos { get; set; }
    }

    public class ProdutosCommand
    {
        public int id { get; set; }
        public string codigoProduto { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public decimal preco { get; set; }
        public int quantidadeComprada { get; set; }
        public int nivelMinimo { get; set; }
        public int unidadeId { get; set; }
        public DateTime dataCadastro { get; set; }
    }
}
