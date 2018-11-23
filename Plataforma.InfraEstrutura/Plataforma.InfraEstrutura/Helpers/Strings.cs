using System.Text;

namespace Plataforma.InfraEstrutura.Helpers
{
    /// <summary>
    /// Classe que implementa métodos de manipulação de strings.
    /// </summary>
    public class Strings
    {
        /// <summary>
        /// Recebe um a string formatando-a para SEO.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string RetornaTextoSeo(string texto)
        {
            var textoSeo = texto.ToLower().Replace("+", "-").Replace(".", "-").Replace("/", "-").Replace("'", "-")
                           .Replace(" ", "-").Replace("&", "").Replace("%", "porcento");

            return Encoding.ASCII.GetString(
                Encoding.GetEncoding("Cyrillic").GetBytes(textoSeo)
            );
        }

        /// <summary>
        /// Retorna o DDD e o telefone com máscara para exibição em view.
        /// </summary>
        /// <param name="ddd"></param>
        /// <param name="telefone"></param>
        /// <returns></returns>
        public static string RetornaTelefone(string ddd, string telefone) {

            if ((ddd != null) && (telefone != null)) {

                return "(" + ddd + ") " + telefone;
            }

            return "";
        }        
    }
}
