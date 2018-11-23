using System;

namespace Plataforma.Domain.Entities.NotMapped
{
    /// <summary>
    /// Classe de configuração do Sistema
    /// </summary>
    public class Configuracao_Sistema
    {
        /// <summary>
        /// 
        /// </summary>
        public DadosConexaoEmail DadosConexaoEmail { get; set; }
        public Guid id_empresa_unifacef { get; set; }
    }

    public class DadosConexaoEmail
    {
        public string UserEmail { get; set; }
        public string PassEmail { get; set; }
        public string FromAddress { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPortNumber { get; set; }
    }

}
