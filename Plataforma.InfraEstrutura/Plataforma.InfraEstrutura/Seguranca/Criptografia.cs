using Rijndael256;

namespace Plataforma.InfraEstrutura.Seguranca
{
    /// <summary>
    /// Classe usada para criptografar e descriptogarafar dados.
    /// </summary>
    public class Criptografia
    {
        /// <summary>
        /// Senha usada para criptografar os dados. 
        /// </summary>
        private static string password = "Ed&Rolling5t0n&svWzUK3Ag3P2prxdA";

        /// <summary>
        /// Recebe uma string em texto plano e devolve uma string criptografada.
        /// </summary>
        /// <param name="textoPlano"></param>
        /// <returns></returns>
        public static string Criptografar(string textoPlano)
        {

            string textoCriptografado = Rijndael.Encrypt(textoPlano, password, KeySize.Aes256);

            return textoCriptografado;
        }

        /// <summary>
        /// Recebe um texto criptografado e retorna uma string em texto plano.
        /// </summary>
        /// <param name="textoCriptografado"></param>
        /// <returns></returns>
        public static string Descriptografar(string textoCriptografado)
        {

            var textoPlano = Rijndael.Decrypt(textoCriptografado, password, KeySize.Aes256);

            return textoPlano;
        }
    }
}
