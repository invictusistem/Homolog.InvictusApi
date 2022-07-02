using AutoMapper;
using Invictus.Application.PedagApplication.Interfaces;
using Invictus.Domain.Padagogico.Requerimento;
using Invictus.Domain.Padagogico.Requerimento.interfaces;
using Invictus.Dtos.PedagDto;
using System.Threading.Tasks;

namespace Invictus.Application.PedagApplication
{
    public class PedagReqService : IPedagReqService
    {
        private readonly ICategoriaRepo _catRepo;
        private readonly IMapper _mapper;
        public PedagReqService(ICategoriaRepo catRepo, IMapper mapper)
        {
            _catRepo = catRepo;
            _mapper = mapper;

        }

        public async Task SaveCategoria(CategoriaDto categoriaDto)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDto);

            await _catRepo.AddCategoria(categoria);

            _catRepo.Commit();
        }

        public async Task SaveTipo(TipoDto tipoDto)
        {
            var tipo = _mapper.Map<Tipo>(tipoDto);

            await _catRepo.AddTipo(tipo);

            _catRepo.Commit();
        }
        public async Task EditCategoria(CategoriaDto categoriaDto)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDto);

            await _catRepo.EditCategoria(categoria);

            _catRepo.Commit();
        }

        public async Task EditTipo(TipoDto tipoDto)
        {
            var tipo = _mapper.Map<Tipo>(tipoDto);

            await _catRepo.EditTipo(tipo);

            _catRepo.Commit();
        }

        
    }
}
