using System;

namespace Plataforma.Domain.Entities.NotMapped
{
    /// <summary>
    /// 
    /// </summary>
    public class DocumentosProcessos
    {
        public Guid id { get; set; }
        public Guid id_documento_sistema { get; set; }
        public string titulo { get; set; }
        public string tituloView { get; set; }
        public string conteudoDocumento { get; set; }
    }
}
