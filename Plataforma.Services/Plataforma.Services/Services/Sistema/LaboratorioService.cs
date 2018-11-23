using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Interfaces.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Services.Sistema
{
    /// <summary>
    /// Service de LaboratorioService
    /// </summary>
    public class LaboratorioService : ServiceBase<Laboratorio>, ILaboratorioService
    {
        public ILaboratorioRepository LaboratorioRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="computadorRepository"></param>
        /// <param name="context"></param>
        public LaboratorioService(ILaboratorioRepository laboratorioRepository, IHttpContextAccessor context) : base(laboratorioRepository, context)
        {
            LaboratorioRepository = laboratorioRepository;
        }

        /// <summary>
        /// Lista o total de registro da tabela
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return LaboratorioRepository.Count();
        }

        /// <summary>
        /// Lista os Laboratorio cadastrados
        /// </summary>
        /// <param name="id_computador"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Laboratorio> GetList(Guid id_laboratorio, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            return LaboratorioRepository.GetList(id_laboratorio, parametros_Busca_Grid);
        }
    }
}
