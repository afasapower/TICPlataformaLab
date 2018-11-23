using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plataforma.Domain.Entities.Sistema
{
    [Table("modulo_empresa")]
    public class Modulo_Empresa
    {
        public Guid id { get; set; }
        //
        public Guid id_pessoa_empresa { get; set; }
        [ForeignKey("id_pessoa_empresa")]
        public virtual Pessoa Pessoa { get; set; }
        //
        public Guid id_modulo { get; set; }
        [ForeignKey("id_modulo")]
        public virtual Modulo Modulo { get; set; }
        //
        public DateTime? data_validade { get; set; }
        public DateTime? data_inclusao { get; set; }
        public string usuario { get; set; }
        public bool excluido { get; set; }
    }
}