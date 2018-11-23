using System;

namespace Plataforma.Domain.Entities.NotMapped
{
    /// <summary>
    /// 
    /// </summary>
    public class Dados_Usuario_Empresa
    {
        public Guid id { get; set; }
        public Guid id_pessoa { get; set; }
        public string razao_social_nome { get; set; }
        public Guid id_pessoa_empresa { get; set; }
        public int totalRegistro { get; set; }
    }
}
