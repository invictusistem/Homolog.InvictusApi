using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Domain.Administrativo.Parametros;
using Invictus.Domain.Administrativo.Parametros.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class ParametroApplication : IParametroApplication
    {
        private readonly IMapper _mapper;
        private readonly IParametroRepo _paramRepo;
        private readonly IParametrosQueries _paramQueries;
        public ParametroApplication(IMapper mapper, IParametroRepo paramRepo, IParametrosQueries paramQueries)
        {
            _mapper = mapper;
            _paramRepo = paramRepo;
            _paramQueries = paramQueries;
        }

        public async Task EditCargo(ParametroValueDto paramValue)
        {
            // var paramKey = await _paramQueries.GetParamKey(key);

            // paramValue.parametrosKeyId = paramKey.id;

            //var parametroValue = new ParametrosValue(paramValue.value, paramValue.descricao, paramValue.comentario, paramKey.id);// _mapper.Map<ParametrosValue>(paramKey);

            var value = _mapper.Map<ParametrosValue>(paramValue);

            await _paramRepo.EditParamValue(value);

            _paramRepo.Commit();
        }

        public async Task RemoeValueById(Guid paramId)
        {
            await _paramRepo.RemoveParamValue(paramId);

            _paramRepo.Commit();
        }

        public async Task SaveCargo(string key, ParametroValueDto paramValue)
        {
            var paramKey = await _paramQueries.GetParamKey(key);

            paramValue.parametrosKeyId = paramKey.id;

            var parametroValue = new ParametrosValue(paramValue.value, paramValue.descricao, paramValue.comentario, paramValue.ativo, paramKey.id);// _mapper.Map<ParametrosValue>(paramKey);

            await _paramRepo.AddParamValue(parametroValue);

            _paramRepo.Commit();
        }
    }
}
