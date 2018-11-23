using System;

namespace Plataforma.InfraEstrutura.Helpers
{
    /// <summary>
    /// Classe que implementa métodos de operações matemáticas.
    /// </summary>
    public class Operacoes
    {
      
        /// <summary>
        /// Calcula valor de desconto ou acrescimo ou desconto.
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="porcentagem"></param>
        /// <param name="acrescimo"></param>
        /// <returns></returns>
        public static Decimal CalculaValorPorcentagem(Decimal valor, Decimal porcentagem, bool acrescimo) {

            if(acrescimo)
            {
                return valor + valor * porcentagem / 100;
            }
            else
            {
                return valor - valor * porcentagem / 100;
            }
        }
    }
}
