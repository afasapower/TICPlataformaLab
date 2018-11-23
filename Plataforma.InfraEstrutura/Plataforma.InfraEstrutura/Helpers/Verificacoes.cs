using System.Collections.Generic;
using System.Dynamic;

namespace Plataforma.InfraEstrutura.Helpers
{
    /// <summary>
    /// Implementa métodos usados para verificações diversas.
    /// </summary>
    public class Verificacoes
    {
        /// <summary>
        /// Verifica se existe determinada propriedade em um objeto dinamico.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool VerificaPropriedadeExiste(dynamic settings, string name)
        {
            if (settings is ExpandoObject)
                return ((IDictionary<string, object>)settings).ContainsKey(name);

            return settings.GetType().GetProperty(name) != null;
        }
    }
}
