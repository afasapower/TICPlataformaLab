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
    /// Service de CidadeService
    /// </summary>
    public class ComputadorService : ServiceBase<Computador>, IComputadorService
    {
        public IComputadorRepository ComputadorRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="computadorRepository"></param>
        /// <param name="context"></param>
        public ComputadorService(IComputadorRepository computadorRepository, IHttpContextAccessor context) : base(computadorRepository, context)
        {
            ComputadorRepository = computadorRepository;
        }

        /// <summary>
        /// Lista o total de registro da tabela
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return ComputadorRepository.Count();
        }

        /// <summary>
        /// Lista os computadores cadastrados
        /// </summary>
        /// <param name="id_computador"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Computador> GetList(Guid id_computador, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            return ComputadorRepository.GetList(id_computador, parametros_Busca_Grid);
        }
    }
}
