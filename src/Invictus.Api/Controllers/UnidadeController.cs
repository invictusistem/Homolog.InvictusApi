using AutoMapper;
using Dapper;
using Invictus.Application.Dtos;
using Invictus.Application.Dtos.Administrativo;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Invictus.Domain.Administrativo.ContratosAggregate;
using Invictus.Domain.Administrativo.Models;
using Invictus.Domain.Administrativo.PacoteAggregate;
using Invictus.Domain.Administrativo.UnidadeAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    
    [Route("api/unidade")]
    public class UnidadeController : BaseController
    {
        private readonly IConfiguration _config;

        private readonly IHttpContextAccessor _userHttpContext;
        private readonly IMapper _mapper;
        private readonly InvictusDbContext _context;
        private readonly string unidade;
        public UnidadeController(IHttpContextAccessor userHttpContext, InvictusDbContext context,
            IMapper mapper, IConfiguration config)
        {
            _userHttpContext = userHttpContext;
            _context = context;
            unidade = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
            _mapper = mapper;
            _config = config;
        }


        #region GET


        // Contrato

        [HttpGet]
        [Route("contrato/{contratoId}")]
        public async Task<ActionResult> GetContratoById(int contratoId)
        {
            var unidadeId = await _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefaultAsync();

            var id = await _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).SingleOrDefaultAsync();
            var query = @"select 
                            *
                            from Contratos 
                            Where Contratos.Id = @contratoId ";
            //where Contratos.Unidadeid = @id";
            var queryConteudo = @"select 
                            *
                            from ContratoConteudo 
                            Where ContratoConteudo.ContratoId = @contratoId ";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                var contrato = await connection.QuerySingleAsync<ContratoDto>(query, new { contratoId = contratoId });

                var contratoConteudo = await connection.QueryAsync<ContratoConteudoDto>(queryConteudo, new { contratoId = contratoId });

                foreach (var item in contratoConteudo.OrderBy(c => c.order))
                {
                    contrato.conteudo += item.content;
                }

                return Ok(new { contrato = contrato });

            }


           // return Ok(contratos);
        }

        [HttpGet]
        [Route("contrato")]
        public async Task<ActionResult> GetContratos()
        {
            var unidadeId = await _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefaultAsync();

            var contratos = await _context.Contratos.ToListAsync();

            return Ok(contratos);
        }

        [HttpGet]
        [Route("contrato-info")]
        public async Task<ActionResult> GetContratosView()
        {
            var id = await _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).SingleOrDefaultAsync();
            var query = @"select 
                            Contratos.Id,
                            Contratos.CodigoContrato, 
                            Contratos.Titulo, 
                            Contratos.PodeEditar,
                            Contratos.DataCriacao,
                            TypePacote.Nome as pacoteNome 
                            from Contratos 
                            inner join TypePacote on Contratos.PacoteId = TypePacote.Id ";
            //where Contratos.Unidadeid = @id";


            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();
                //var countItems = await connection.QuerySingleAsync<int>(queryCount);
                var contratos = await connection.QueryAsync<ContratoDto>(query, new { id = id });

                return Ok(new { contratos = contratos });

            }

            //return Ok(contratos);
        }

        // Cargo

        [HttpGet]
        [Route("typepacote")]
        public async Task<IActionResult> GetTypePacote()
        {
            var types = await _context.TypePacotes.ToListAsync();

            return Ok(new { types = types });
        }

        [HttpGet]
        [Route("cargo")]
        public async Task<IActionResult> GetCargos()
        {
            var cargos = await _context.Cargos.ToListAsync();

            return Ok(cargos);
        }

        [HttpGet]
        [Route("plano-pagamento")]
        public async Task<ActionResult> GetPlanos()
        {
            //var unidadeId = await _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefaultAsync();

            var planos = await _context.Planos.ToListAsync();

            return Ok(planos);
        }

        [HttpGet]
        public async Task<ActionResult> GetUnidades()
        {
            //Console.WriteLine(unidade);
            //var unidadeId = _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefault();
            //var salas = _context.Salas.Where(s => s.UnidadeId == unidadeId).ToList();
            //var modulos = await _moduloQueries.GetModulos(unidadeCode);

            var unidades = await _context.Unidades.Include(s => s.Salas).ToListAsync();
            //var msg = "Ok"
            return Ok(unidades);
        }

        [HttpGet]
        [Route("salas")]
        public IActionResult GetSalas()
        {
            //Console.WriteLine(unidade);
            var unidadeId = _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefault();
            var salas = _context.Salas.Where(s => s.UnidadeId == unidadeId).ToList();
            //var modulos = await _moduloQueries.GetModulos(unidadeCode);
            //var msg = "Ok"
            return Ok(salas);
        }

        [HttpGet]
        [Route("modulos")]
        public async Task<ActionResult> GetModulos()
        {
            //Console.WriteLine(unidade);
            var unidadeId = _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefault();
            //var salas = _context.Salas.Where(s => s.UnidadeId == unidadeId).ToList();
            var modulos = await _context.Modulos.Include(m => m.Materias).Where(m => m.UnidadeId == unidadeId).ToListAsync();
            //var msg = "Ok"
            return Ok(modulos);
        }

        [HttpGet]
        [Route("materias/{moduloId}")]
        public async Task<ActionResult> GetMMaterias(int moduloId)
        {

            var materias = await _context.Materias.Where(m => m.ModuloId == moduloId).ToListAsync();

            return Ok(materias);
        }




        #endregion


        #region POST


        [HttpPost]
        [Route("cargo")]
        public IActionResult SaveCargo([FromBody] CargoDto newCargo)
        {
            var cargo = _mapper.Map<Cargo>(newCargo);

            cargo.SetDataCriacao();

            _context.Cargos.Add(cargo);

            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("contrato")]
        public IActionResult SaveContrato([FromBody] ContratoDto newContrato)
        {
            var totalContratos = _context.Contratos.Count();
            var contrato = new Contrato(totalContratos, newContrato.pacoteId, newContrato.titulo,newContrato.ativo, newContrato.observacao);
            contrato.AddConteudos(newContrato.conteudo);
            contrato.SetDataCriacao();
            /*
            decimal length = newContrato.conteudo.Length;
            

            var loops = Math.Floor(length / 7999);
            var resto = length - (loops * 7999);

            var conteudoList = new List<Conteudo>();
            for (int i = 1; i <= loops; i++)
            {
                var ini = (i - 1) * 7999;
                var end = 2999 * i;

                conteudoList.Add(new Conteudo(i, newContrato.conteudo.Substring(ini, 7999)));

            }

            if (resto > 0)
            {
                var ini = loops * 7999;
                var end = (length - 1) - ini;

                conteudoList.Add(new Conteudo(Convert.ToInt32(loops) + 1, newContrato.conteudo.Substring(Convert.ToInt32(ini), Convert.ToInt32(end))));

            }


            var qntcontrato = _context.Contratos.Count();
            var unidadeId = _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefault();
            var contrato = new Contrato(0, unidade + Convert.ToInt32(qntcontrato), unidadeId, newContrato.pacoteId, newContrato.titulo, true,
                null, DateTime.Now);

            contrato.Conteudos.AddRange(conteudoList);
            */
            _context.Contratos.Add(contrato);

            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("modulo")]
        public IActionResult SaveModulo([FromBody] ModuloDto newModulo)
        {
            var modulo = _mapper.Map<Pacote>(newModulo);

            modulo.SetCreateDate();
            modulo.SetTotalHours(modulo.Materias);
            var unidadeId = _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefault();
            modulo.SetUnidadeId(unidadeId);

            _context.Modulos.Add(modulo);

            _context.SaveChanges();

            var listDocumentacaoExigida = new List<DocumentacaoExigencia>();


            foreach (var item in newModulo.documentos)
            {
                var doc = new DocumentacaoExigencia(item.descricao, item.comentario, modulo.Id);
                doc.SetTitular(item.titular);
                listDocumentacaoExigida.Add(doc);
            }
            if (newModulo.documentos.Count > 0)
            {
                _context.DocsExigencias.AddRange(listDocumentacaoExigida);

                _context.SaveChanges();
            }

            return Ok();
        }

        [HttpPost]
        [Route("plano-pagamento")]
        public IActionResult SavePlanoPagamento([FromBody] PlanoPagamentoDto newPlanoPagamento)
        {
            var planoPagm = _mapper.Map<PlanoPagamento>(newPlanoPagamento);

            _context.Planos.Add(planoPagm);

            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult SaveUnidade([FromBody] CreateUnidadeCommand command)
        {
            return Ok();
            var unidade = _mapper.Map<Unidade>(command.unidade);

            var qntSalas = _context.Salas.Count();

            var i = 0;
            foreach (var sala in unidade.Salas)
            {
                sala.SetSalaTitulo(qntSalas + i);
                i++;
            }

            _context.Unidades.Add(unidade);

            _context.SaveChanges();

            var colaborador = _mapper.Map<Colaborador>(command.colaborador);
            colaborador.SetUnidadeId(unidade.Id);
            colaborador.AtivarPerfil(true);

            _context.Colaboradores.Add(colaborador);

            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("sala-create/{unidadeId}")]
        public IActionResult SaveSala([FromBody] SalaDto newSala, int unidadeId)
        {
            var unidade = _context.Unidades.Include(u => u.Salas).Where(u => u.Id == unidadeId).FirstOrDefault();

            var sala = _mapper.Map<Sala>(newSala);

            var qntSalas = _context.Salas.Where(u => u.UnidadeId == unidadeId).ToList();

            sala.SetSalaTitulo(qntSalas.Count());

            unidade.AddSala(sala);

            _context.Unidades.Update(unidade);//.Include(u => u.Salas).Up

            _context.SaveChanges();

            return Ok();
        }


        [HttpPost]
        [Route("doscexigencia")]
        public IActionResult DocsExigencia([FromBody] UnidadeDto editedSala)
        {
            var unidade = _mapper.Map<Unidade>(editedSala);

            _context.Unidades.Add(unidade);

            _context.SaveChanges();

            return Ok();
        }

        #endregion

        #region PUT

        // Contrato;

        [HttpPut]
        [Route("contrato-edit")]
        public IActionResult EditContrato([FromBody] ContratoDto editedContrato)
        {
            var oldcontrato = _context.Contratos.Include(c => c.Conteudos).Where(c => c.Id == editedContrato.id).SingleOrDefault();
            oldcontrato.EditContrato(editedContrato.titulo, editedContrato.ativo);
            oldcontrato.EditConteudo(editedContrato.conteudo);

            _context.Contratos.Update(oldcontrato);

            _context.SaveChanges();

            return Ok();
        }


        // Cargo
        [HttpPut]
        [Route("cargo")]
        public IActionResult EditCargo([FromBody] CargoDto editedCargo)
        {
            var cargo = _mapper.Map<Cargo>(editedCargo);

            _context.Cargos.Update(cargo);

            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateUnidade([FromBody] UnidadeDto editedUnidade)
        {
            var unidade = _mapper.Map<Unidade>(editedUnidade);

            _context.Unidades.Update(unidade);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("sala-editar")]
        public IActionResult UpdateSala([FromBody] SalaDto editedSala)
        {
            var sala = _mapper.Map<Sala>(editedSala);

            _context.Salas.Update(sala);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("plano-editar")]
        public IActionResult UpdatePlano([FromBody] PlanoPagamentoDto editedPlano)
        {
            var plano = _mapper.Map<PlanoPagamento>(editedPlano);
            
            _context.Planos.Update(plano);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("pacote-editar")]
        public IActionResult UpdatePacote([FromBody] ModuloDto editedPacote)
        {
            var pacote = _mapper.Map<Pacote>(editedPacote);

            _context.Modulos.Update(pacote);
            _context.SaveChanges();

            return Ok();
        }



        #endregion

        #region DELETE

        [HttpDelete]
        [Route("sala-deletar/{salaId}")]
        public IActionResult DeleteSala(int salaId)
        {
            var sala = _context.Salas.Find(salaId);
            _context.Salas.Remove(sala);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteUnidade(int unidadeId)
        {
            var unidade = _context.Unidades.Find(unidadeId);
            _context.Unidades.Remove(unidade);
            _context.SaveChanges();

            return Ok();
        }

        #endregion



    }
}
