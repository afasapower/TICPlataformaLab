using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Interfaces.Sistema
{
    /// <summary>
    /// Interface de IModuloService
    /// </summary>
    public interface IModuloService : IServiceBase<Modulo>
    {
        /// <summary>
        /// Lista o Total de Registro na tabela
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Lista os Módulos
        /// </summary>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Modulo> GetList(Parametros_Busca_Grid parametros_Busca_Grid);

        /// <summary>
        /// Lista os Módulos disponíveis para o usuário
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        IEnumerable<Modulo> GetList_Modulo_Menu(Guid id_usuario);
    }
}