using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Domain.Administrativo.ContratoAggregate.Interfaces;
using Invictus.Domain.Administrativo.ContratosAggregate;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class ContratoApplication : IContratoApplication
    {
        private readonly IMapper _mapper;
        private readonly IContratoQueries _contratoQueries;
        private readonly IContratoRepository _contratoRepo;
        public ContratoApplication(
            IMapper mapper,
            IContratoQueries contratoQueries,
            IContratoRepository contratoRepo)
        {
            _mapper = mapper;
            _contratoQueries = contratoQueries;
            _contratoRepo = contratoRepo;
        }
        public async Task AddContrato(ContratoDto newContrato)
        {
            var totalContratos = await _contratoQueries.CountContratos();// _context.Contratos.Count();
            var contrato = new Contrato(newContrato.typepacoteId, newContrato.titulo, newContrato.ativo, true, newContrato.observacao);
            contrato.CreateCodigoContrato(totalContratos);
            contrato.AddConteudos(newContrato.conteudo);
            contrato.SetDataCriacao();

            await _contratoRepo.SaveContrato(contrato);

            _contratoRepo.Commit();
        }

        public async Task EditContrato(ContratoDto newContrato)
        {
            var contrato = await _contratoQueries.GetContratoCompletoById(newContrato.id);//  _context.Contratos.Include(c => c.Conteudos).Where(c => c.Id == editedContrato.id).SingleOrDefault();

            var oldcontrato = _mapper.Map<ContratoDto, Contrato>(contrato);

            oldcontrato.EditContrato(newContrato.titulo, newContrato.ativo);

            _contratoRepo.RemoveConteudos(oldcontrato.Conteudos);
           
            oldcontrato.EditConteudo(newContrato.conteudo);

            await _contratoRepo.UpdateContrato(oldcontrato);

            await _contratoRepo.SaveConteudo(oldcontrato.Conteudos);

            _contratoRepo.Commit();
        }
    }
}
