using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.InfraEstrutura.Helpers;
using Plataforma.Repository.Interfaces.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Services.Sistema
{
    /// <summary>
    /// Service de PaginaService
    /// </summary>
    public class PaginaService : ServiceBase<Pagina>, IPaginaService
    {
        public IPaginaRepository PaginaRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="paginaRepository"></param>
        /// <param name="context"></param>
        public PaginaService(IPaginaRepository paginaRepository, IHttpContextAccessor context) : base(paginaRepository, context)
        {
            PaginaRepository = paginaRepository;
        }

        /// <summary>
        /// Retorna o total de registro da tabela
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return PaginaRepository.Count();
        }

        /// <summary>
        /// Retorna uma lista IEnumerable de Paroquia
        /// </summary>
        /// <param name="id_pagina"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Pagina> GetList(Guid id_pagina, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            return PaginaRepository.GetList(id_pagina, parametros_Busca_Grid);
        }
    }
}