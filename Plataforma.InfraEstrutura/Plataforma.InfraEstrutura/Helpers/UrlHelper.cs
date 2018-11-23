using Microsoft.AspNetCore.Http;

namespace Plataforma.InfraEstrutura.Helpers
{
    /// <summary>
    /// Classe que implementa métodos envolvendo a URL da aplicação.
    /// </summary>
    public class UrlHelper
    {
        /// <summary>
        /// Retorna a url do dominio da aplicação.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static HostString GetUri(HttpContext request)
        {            
            return request.Request.Host;
        }
    }
}
