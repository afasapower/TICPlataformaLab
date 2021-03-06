﻿using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Interfaces.Sistema
{
    /// <summary>
    /// Inteface de IUsuario_Empresa_AtivoRepository
    /// </summary>
    public interface IUsuario_Empresa_AtivoRepository : IRepositoryBase<Usuario_Empresa_Ativo>
    {
        /// <summary>
        /// Retorna Empresa ativa na navegação do usuário
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IEnumerable<Usuario_Empresa_Ativo> GetList_Empresa_Ativa(Guid id_usuario, Parametros_Busca_Grid parametros_Busca_Grid);
    }
}