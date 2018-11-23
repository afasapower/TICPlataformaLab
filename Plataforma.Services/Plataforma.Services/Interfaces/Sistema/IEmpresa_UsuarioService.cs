using Plataforma.Domain.Entities.NotMapped;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Interfaces.Sistema
{
    public interface IEmpresa_UsuarioService 
    {
        /// <summary>
        /// Retorna os usuarios por Empresa
        /// </summary>
        /// <param name="id_usuario_logado"></param>
        /// <param name="id_empresa"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        IList<Dados_Usuario_Empresa> GetList(Guid id_usuario_logado, Guid id_empresa, Parametros_Busca_Grid parametros_Busca_Grid, Configuracao_Sistema _Configuracao_Sistema);        
    }
}
