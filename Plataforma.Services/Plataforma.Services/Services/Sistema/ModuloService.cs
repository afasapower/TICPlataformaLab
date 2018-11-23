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
    /// Service de ModuloService
    /// </summary>
    public class ModuloService : ServiceBase<Modulo>, IModuloService
    {
        public IModuloRepository ModuloRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="moduloRepository"></param>
        /// <param name="context"></param>
        public ModuloService(IModuloRepository moduloRepository, IHttpContextAccessor context) : base(moduloRepository, context)
        {
            ModuloRepository = moduloRepository;
        }

        /// <summary>
        /// Lista o Total de Registro na tabela
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return ModuloRepository.Count();
        }

        /// <summary>
        /// Lista os Módulos
        /// </summary>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Modulo> GetList(Parametros_Busca_Grid parametros_Busca_Grid)
        {
            return ModuloRepository.GetList(parametros_Busca_Grid);
        }

        /// <summary>
        /// Lista os Módulos disponíveis para o usuário
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        public IEnumerable<Modulo> GetList_Modulo_Menu(Guid id_usuario)
        {
            return ModuloRepository.GetList_Modulo_Menu(id_usuario);
        }
    }
}